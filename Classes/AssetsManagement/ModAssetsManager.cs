using System.IO;
using ClimeronToolsForPvZ.Classes.UI;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader.Utils;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes.AssetsManagement
{
    public static class ModAssetsManager
    {
        public const string MOD_ASSETS_FOLDER_NAME = nameof(ClimeronToolsForPvZ);
        public static string ModAssetsFolderPath => Path.Combine(MelonEnvironment.ModsDirectory, MOD_ASSETS_FOLDER_NAME);
        public const string FONTS_FOLDER_NAME = "Fonts";

        #region Assets
        public static TMP_FontAsset MainTextFont { get; private set; }
        #endregion

        public static void Initialize()
        {
            FontsFolderPath.CreateDirectories(true);
        }
        public static void LoadAssets(Settings settings)
        {
            MainTextFont = LoadTMPFont(settings.mainTextFont, false);
            if (!MainTextFont)
                $"The font '{settings.mainTextFont}' specified in the settings file could not be found. A standard font has been loaded.".PrintWarning();
        }
        #region Fonts
        public static string FontsFolderPath => Path.Combine(ModAssetsFolderPath, FONTS_FOLDER_NAME);
        public static string[] FontsPaths => Directory.GetFiles(FontsFolderPath);
        public static Font LoadFont(string fontName, bool throwException) => Path.Combine(MOD_ASSETS_FOLDER_NAME, FONTS_FOLDER_NAME, fontName).LoadFont(throwException);
        public static TMP_FontAsset LoadTMPFont(string fontName, bool throwException) => Path.Combine(MOD_ASSETS_FOLDER_NAME, FONTS_FOLDER_NAME, fontName).LoadTMPFont(throwException);
        #endregion
    }
}
