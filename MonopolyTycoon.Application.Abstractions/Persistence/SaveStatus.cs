namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Provides a clear, type-safe contract for representing the integrity status
    /// of a save file, as required by sequence diagrams and UI feedback mechanisms.
    /// Fulfills requirements: REQ-1-088.
    /// </summary>
    public enum SaveStatus
    {
        /// <summary>
        /// Indicates the save slot is available and does not contain a save file.
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Indicates the save file is present and has passed all integrity checks.
        /// </summary>
        Valid = 1,

        /// <summary>
        /// Indicates the save file exists but failed an integrity check (e.g., checksum mismatch).
        /// </summary>
        Corrupted = 2,

        /// <summary>
        /// Indicates the save file is from an older, incompatible version of the game.
        /// </summary>
        Incompatible = 3
    }
}