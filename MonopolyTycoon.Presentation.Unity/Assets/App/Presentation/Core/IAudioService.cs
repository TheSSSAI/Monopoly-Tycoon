using UnityEngine.Audio;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Defines the contract for a service that manages all audio playback,
    /// including background music, sound effects, and volume controls.
    /// Fulfills requirement REQ-1-079 and REQ-1-094.
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Plays a sound effect once.
        /// </summary>
        /// <param name="sfxKey">The key or name of the sound effect clip to play.</param>
        void PlaySfx(string sfxKey);

        /// <summary>
        /// Plays a background music track, typically on a loop.
        /// </summary>
        /// <param name="musicKey">The key or name of the music clip to play.</param>
        void PlayMusic(string musicKey);

        /// <summary>
        /// Stops the currently playing background music.
        /// </summary>
        void StopMusic();

        /// <summary>
        /// Sets the master volume level.
        /// </summary>
        /// <param name="level">The volume level, typically from 0.0f to 1.0f.</param>
        void SetMasterVolume(float level);

        /// <summary>
        /// Sets the music-specific volume level.
        /// </summary>
        /// <param name="level">The volume level, typically from 0.0f to 1.0f.</param>
        void SetMusicVolume(float level);

        /// <summary>
        /// Sets the sound effects-specific volume level.
        /// </summary>
        /// <param name="level">The volume level, typically from 0.0f to 1.0f.</param>
        void SetSfxVolume(float level);
    }
}