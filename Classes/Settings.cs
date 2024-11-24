using System;
using System.IO;
using ClimeronToolsForPvZ.Extensions;
using ClimeronToolsForPvZ.Classes.AssetsManagement;

namespace ClimeronToolsForPvZ.Classes
{
    [Serializable]
    public class Settings : Il2CppSystem.Object
    {
        #region Serializable
        public string loadedModsTextFont;
        #endregion
        public const string SETTINGS_FILE_NAME = "Settings.json";
        public static string SettingsFilePath => Path.Combine(ModAssetsManager.ModAssetsFolderPath, SETTINGS_FILE_NAME);

        public static Settings Load()
        {
            if (!Directory.Exists(ModAssetsManager.ModAssetsFolderPath))
                Directory.CreateDirectory(ModAssetsManager.ModAssetsFolderPath);
            if (!File.Exists(SettingsFilePath))
                SettingsFilePath.WriteJson(new Settings());
            return SettingsFilePath.ReadJsonFileExceptionally<Settings>();
        }
        public void Save() => SettingsFilePath.WriteJson(this);
    }
}
