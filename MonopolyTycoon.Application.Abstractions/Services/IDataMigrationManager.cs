using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines the contract for a service responsible for upgrading data structures,
    /// such as game save files, from older versions to the current version.
    /// This is critical for maintaining backward compatibility across application updates.
    /// Fulfills requirements: REQ-1-090.
    /// </summary>
    public interface IDataMigrationManager
    {
        /// <summary>
        /// Takes the raw data of a legacy save file and applies the necessary
        /// transformations to upgrade it to the current application version.
        /// </summary>
        /// <param name="rawData">The raw byte content of the legacy save file.</param>
        /// <param name="sourceVersion">The version of the save file to be migrated.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the raw byte content of the successfully migrated save file.
        /// </returns>
        /// <exception cref="MonopolyTycoon.Domain.Exceptions.DataMigrationException">
        /// Thrown if the migration fails for any reason, allowing the caller to handle rollback logic.
        /// </exception>
        Task<byte[]> MigrateSaveDataAsync(byte[] rawData, string sourceVersion);
    }
}