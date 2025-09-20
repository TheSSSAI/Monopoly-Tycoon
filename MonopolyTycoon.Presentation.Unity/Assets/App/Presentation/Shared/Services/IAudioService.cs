namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Contract for a service that handles playing all audio in the game.
    /// This abstracts the underlying audio engine implementation.
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Plays a one-shot sound effect.
        /// </summary>
        /// <param name="sfxKey">The addressable asset key for the AudioClip.</param>
        void PlaySfx(string sfxKey);

        /// <summary>
        /// Plays a background music track, stopping any currently playing music.
        /// </summary>
        /// <param name="musicKey">The addressable asset key for the AudioClip.</param>
        void PlayMusic(string musicKey);

        /// <summary>
        /// Stops the currently playing background music.
        /// </summary>
        void StopMusic();

        /// <summary>
        /// Sets the volume for a specific audio channel.
        /// </summary>
        /// <param name="channel">The name of the channel (e.g., "Master", "Music", "SFX").</param>
        /// <param name="volume">The volume level from 0.0 to 1.0.</param>
        void SetVolume(string channel, float volume);
    }
}