using MonopolyTycoon.Presentation.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Concrete implementation of IAudioService for managing game audio within Unity.
    /// Handles loading audio clips, playing sound effects and music, and controlling volume levels.
    /// Fulfills requirements: REQ-1-079, REQ-1-094.
    /// </summary>
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly AudioMixer _masterMixer;
        private readonly AudioSource _musicSource;
        private readonly AudioSource _sfxSource;

        private const string MasterVolumeParam = "MasterVolume";
        private const string MusicVolumeParam = "MusicVolume";
        private const string SfxVolumeParam = "SfxVolume";

        public AudioService(IAssetProvider assetProvider, AudioMixer masterMixer, AudioSource musicSource, AudioSource sfxSource)
        {
            _assetProvider = assetProvider;
            _masterMixer = masterMixer;
            
            _musicSource = musicSource;
            _musicSource.loop = true;

            _sfxSource = sfxSource;
            _sfxSource.loop = false;
        }

        public async Task PlayMusicAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _musicSource.Stop();
                _musicSource.clip = null;
                return;
            }

            var clip = await _assetProvider.LoadAssetAsync<AudioClip>(key);
            if (clip != null)
            {
                if (_musicSource.clip == clip && _musicSource.isPlaying)
                {
                    return; // Avoid restarting the same track
                }

                _musicSource.clip = clip;
                _musicSource.Play();
            }
            else
            {
                Debug.LogError($"[AudioService] Failed to load music clip with key: {key}");
            }
        }

        public async Task PlaySfxAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            var clip = await _assetProvider.LoadAssetAsync<AudioClip>(key);
            if (clip != null)
            {
                _sfxSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError($"[AudioService] Failed to load SFX clip with key: {key}");
            }
        }

        public void SetMasterVolume(float volume)
        {
            SetVolume(MasterVolumeParam, volume);
        }

        public void SetMusicVolume(float volume)
        {
            SetVolume(MusicVolumeParam, volume);
        }

        public void SetSfxVolume(float volume)
        {
            SetVolume(SfxVolumeParam, volume);
        }

        /// <summary>
        /// Sets the volume on the AudioMixer.
        /// Converts a linear volume scale (0.0 to 1.0) to a logarithmic decibel scale.
        /// </summary>
        /// <param name="parameterName">The exposed parameter name on the AudioMixer.</param>
        /// <param name="volume">The linear volume level (0.0 to 1.0).</param>
        private void SetVolume(string parameterName, float volume)
        {
            if (_masterMixer == null)
            {
                Debug.LogError("[AudioService] Master Mixer is not assigned.");
                return;
            }

            // Clamp volume to avoid issues with log(0)
            volume = Mathf.Clamp(volume, 0.0001f, 1.0f);
            
            // Convert linear volume to decibels for the AudioMixer
            float dbVolume = Mathf.Log10(volume) * 20;
            _masterMixer.SetFloat(parameterName, dbVolume);
        }
    }
}