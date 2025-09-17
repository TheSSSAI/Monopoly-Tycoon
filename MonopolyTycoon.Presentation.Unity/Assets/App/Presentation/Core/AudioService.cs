using UnityEngine;
using UnityEngine.Audio;
using MonopolyTycoon.Presentation.Core;
using System.Collections.Generic;
using VContainer;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Concrete implementation of IAudioService. Manages all audio playback including music, sound effects,
    /// and volume control via Unity's AudioMixer.
    /// This service is designed to be a persistent singleton managed by the DI container.
    /// Fulfills REQ-1-079, REQ-1-094.
    /// </summary>
    public class AudioService : IAudioService
    {
        private readonly AudioMixer mixer;
        private readonly AudioSource musicSource;
        private readonly List<AudioSource> sfxSources;
        private int sfxSourceIndex = 0;
        private const int SfxSourcePoolSize = 10;

        // Exposed parameter names in the AudioMixer asset
        private const string MasterVolumeParam = "MasterVolume";
        private const string MusicVolumeParam = "MusicVolume";
        private const string SfxVolumeParam = "SfxVolume";

        [Inject]
        public AudioService(AudioMixer mainMixer)
        {
            mixer = mainMixer;

            // Create a persistent GameObject to host our AudioSources
            var audioHost = new GameObject("AudioServiceHost");
            Object.DontDestroyOnLoad(audioHost);

            // Setup Music Source
            musicSource = audioHost.AddComponent<AudioSource>();
            musicSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
            musicSource.loop = true;

            // Setup SFX Source Pool
            sfxSources = new List<AudioSource>(SfxSourcePoolSize);
            var sfxGroup = mixer.FindMatchingGroups("SFX")[0];
            for (int i = 0; i < SfxSourcePoolSize; i++)
            {
                var sfxSource = audioHost.AddComponent<AudioSource>();
                sfxSource.outputAudioMixerGroup = sfxGroup;
                sfxSource.loop = false;
                sfxSource.playOnAwake = false;
                sfxSources.Add(sfxSource);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (musicSource.clip == clip && musicSource.isPlaying)
            {
                return; // Already playing this clip
            }

            if (clip == null)
            {
                musicSource.Stop();
                musicSource.clip = null;
                return;
            }

            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PlaySfx(AudioClip clip, float volumeScale = 1.0f)
        {
            if (clip == null)
            {
                Debug.LogWarning("[AudioService] Attempted to play a null SFX clip.");
                return;
            }

            // Find an available source from the pool
            var source = GetAvailableSfxSource();
            source.PlayOneShot(clip, volumeScale);
        }

        public void SetMasterVolume(float linearVolume)
        {
            SetVolume(MasterVolumeParam, linearVolume);
        }

        public void SetMusicVolume(float linearVolume)
        {
            SetVolume(MusicVolumeParam, linearVolume);
        }

        public void SetSfxVolume(float linearVolume)
        {
            SetVolume(SfxVolumeParam, linearVolume);
        }

        /// <summary>
        /// Sets the volume of a specified mixer group.
        /// Converts a linear value (0.0 to 1.0) to the logarithmic decibel scale used by the mixer.
        /// </summary>
        /// <param name="parameterName">The exposed parameter name in the AudioMixer.</param>
        /// <param name="linearVolume">The volume level from 0.0 (silent) to 1.0 (full).</param>
        private void SetVolume(string parameterName, float linearVolume)
        {
            // Clamp the value to prevent errors
            linearVolume = Mathf.Clamp(linearVolume, 0.0001f, 1.0f);
            
            // Convert linear to dB. The formula is: dB = 20 * log10(linear).
            // A value of 0.0001 is used for silence to avoid log10(0) which is -infinity.
            float dbVolume = Mathf.Log10(linearVolume) * 20;
            mixer.SetFloat(parameterName, dbVolume);
        }
        
        /// <summary>
        /// Retrieves an available AudioSource from the pool for playing a one-shot sound effect.
        /// This prevents sounds from cutting each other off.
        /// </summary>
        /// <returns>An available AudioSource component.</returns>
        private AudioSource GetAvailableSfxSource()
        {
            // Simple round-robin approach for the pool
            sfxSourceIndex = (sfxSourceIndex + 1) % SfxSourcePoolSize;
            return sfxSources[sfxSourceIndex];
        }
    }
}