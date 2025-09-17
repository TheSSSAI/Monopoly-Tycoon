using Microsoft.Extensions.Options;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Abstractions;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Configuration;
using System;
using System.IO;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Services
{
    /// <summary>
    /// Encapsulates the logic for determining the physical file paths for save game files.
    /// This centralizes path logic, making it consistent and easily testable/configurable.
    /// It ensures the save directory exists before providing a path.
    /// </summary>
    public class SaveFilePathProvider : ISaveFilePathProvider
    {
        private readonly SaveGameSettings _settings;
        private readonly string _saveDirectory;

        public SaveFilePathProvider(IOptions<SaveGameSettings> settingsOptions)
        {
            _settings = settingsOptions?.Value ?? throw new ArgumentNullException(nameof(settingsOptions));
            
            if(string.IsNullOrWhiteSpace(_settings.AppName))
            {
                throw new InvalidOperationException("AppName cannot be null or whitespace in SaveGameSettings.");
            }
            
            // Per REQ-1-075, log/save files are in %APPDATA%/MonopolyTycoon/
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string companyPath = string.IsNullOrWhiteSpace(_settings.CompanyName) 
                ? appDataPath 
                : Path.Combine(appDataPath, _settings.CompanyName);

            string appPath = Path.Combine(companyPath, _settings.AppName);
            _saveDirectory = Path.Combine(appPath, "saves");

            // Ensure the directory exists. This is a safe operation that does nothing if it already exists.
            Directory.CreateDirectory(_saveDirectory);
        }

        public string GetSaveDirectory()
        {
            return _saveDirectory;
        }

        public string GetSaveFilePath(int slot)
        {
            if (slot <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slot), "Save slot number must be positive.");
            }
            
            // Per REQ-1-086, there are a minimum of five slots. The naming convention is defined here.
            var fileName = $"save_slot_{slot}.json";
            return Path.Combine(_saveDirectory, fileName);
        }
    }
}