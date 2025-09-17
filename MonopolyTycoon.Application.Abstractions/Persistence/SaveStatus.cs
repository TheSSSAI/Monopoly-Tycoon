namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Represents the integrity status of a saved game file.
    /// Used to communicate the state of a save slot to the UI layer.
    /// </summary>
    public enum SaveStatus
    {
        /// <summary>
        /// Indicates the save slot is available and does not contain a save file.
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Indicates the save file is present and has passed all integrity checks (e.g., checksum validation).
        /// </summary>
        Valid = 1,

        /// <summary>
        /// Indicates the save file exists but failed an integrity check and cannot be loaded.
        /// </summary>
        Corrupted = 2,

        /// <summary>
        /// Indicates the save file is valid but was created with an older, incompatible version of the game.
        /// </summary>
        IncompatibleVersion = 3
    }
}