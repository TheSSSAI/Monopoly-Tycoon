using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines the contract for a service responsible for upgrading data from older formats
    /// to the current format, such as for save game files.
    /// </summary>
    public interface IDataMigrationManager
    {
        /// <summary>
        /// Takes the raw data of a legacy save file and applies the necessary transformations
        /// to upgrade it to the current application version's format.
        /// </summary>
        /// <param name="rawData">The raw byte content of the legacy save file.</param>
        /// <param name="sourceVersion">The version of the save file to be migrated.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the raw byte content of the migrated data.
        /// </returns>
        /// <exception cref="DataMigrationException">Thrown if the migration fails for any reason, allowing the caller to handle rollback.</exception>
        Task<byte[]> MigrateSaveDataAsync(byte[] rawData, string sourceVersion);
    }

    /// <summary>
    /// Represents an error that occurs during a data migration process.
    /// </summary>
    public class DataMigrationException : Exception
    {
        public DataMigrationException() { }
        public DataMigrationException(string message) : base(message) { }
        public DataMigrationException(string message, Exception inner) : base(message, inner) { }
    }
}