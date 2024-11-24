using System;
using System.IO;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader.Utils;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes.UI
{
    public static class CustomFontsManager
    {
        /// <summary>Загружает шрифт в формате Font по указанному пути из папки Mods.</summary>
        /// <param name="pathFromModsFolder">Путь к файлу шрифта относительно папки Mods.</param>
        /// <returns>Возвращает объект Font, либо null, если шрифт не найден или произошла ошибка.</returns>
        public static Font LoadFont(this string pathFromModsFolder, bool throwException)
        {
            string fontFilePath = Path.Combine(MelonEnvironment.ModsDirectory, pathFromModsFolder);
            if (!File.Exists(fontFilePath))
            {
                if (throwException)
                    $"Font not found at path: '{fontFilePath}'.".PrintError<FileNotFoundException>();
                return null;
            }
            Font font = new();
            Font.Internal_CreateFontFromPath(font, fontFilePath);
            return font;
        }
        /// <summary>Конвертирует стандартный шрифт Unity в шрифт TextMeshPro (TMP_FontAsset).</summary>
        /// <param name="font">Исходный шрифт Unity (Font), который нужно преобразовать.</param>
        /// <returns>Объект TMP_FontAsset, созданный на основе исходного шрифта.<br/>
        /// Возвращает <c>null</c>, если создание TMP_FontAsset не удалось.</returns>
        public static TMP_FontAsset ConvertToTMP(this Font font)
        {
            TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(font);
            if (fontAsset == null)
            {
                $"Failed to create TMP_FontAsset from font '{font.name}'.".PrintError<NullReferenceException>();
                return null;
            }
            return fontAsset;
        }
        /// <summary>Загружает шрифт в формате TMP_FontAsset по указанному пути из папки Mods.</summary>
        /// <param name="pathFromModsFolder">Путь к файлу шрифта относительно папки Mods.</param>
        /// <returns>Возвращает объект TMP_FontAsset, либо null, если шрифт не найден или произошла ошибка.</returns>
        public static TMP_FontAsset LoadTMPFont(this string pathFromModsFolder, bool throwException)
        {
            Font font = pathFromModsFolder.LoadFont(throwException);
            if (!font)
                return null;
            TMP_FontAsset fontAsset = font.ConvertToTMP();
            return fontAsset;
        }
    }
}
