using System.Text.Json.Serialization;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Persistence;

/// <summary>
/// Represents the top-level structure of a save game file on disk.
/// This class acts as a persistence model, separating the file's metadata from the
/// core domain data. It encapsulates the game state data as a raw string, along with
/// versioning and integrity-checking information.
/// </summary>
/// <remarks>
/// This design is critical for robustly implementing checksum validation (REQ-1-088)
/// and data migration (REQ-1-090).
/// </remarks>
public sealed class SaveFileWrapper
{
    /// <summary>
    /// The version of the application that created the save file.
    /// Used to determine if data migration is necessary.
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; init; }

    /// <summary>
    /// A cryptographic hash (e.g., SHA256) of the <see cref="GameStateData"/> string.
    /// Used to verify file integrity and detect corruption.
    /// </summary>
    [JsonPropertyName("checksum")]
    public required string Checksum { get; init; }

    /// <summary>
    /// The serialized JSON string representing the complete GameState domain object.
    /// Storing it as a string within the wrapper allows for checksum validation and
    /// migration to occur before deserializing into the final domain model.
    /// </summary>
    [JsonPropertyName("gameStateData")]
    public required string GameStateData { get; init; }
}