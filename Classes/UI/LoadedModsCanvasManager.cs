using ClimeronToolsForPvZ.Components;
using ClimeronToolsForPvZ.Classes.AssetsManagement;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes.UI
{
    public static class LoadedModsCanvasManager
    {
        private const string _LOADED_MODS_CANVAS_NAME = "LoadedModsCanvas";
        private const string _LOADED_MODS_TEXT_NAME = "LoadedModsText";
        public static Canvas LoadedModsCanvas { get; set; }
        public static ShadowedTextSupporter ShadowedTextSupporter { get; private set; }
        public static RectTransform ShadowedTextRectTransform { get; private set; }
        internal static void CreateLoadedModsText()
        {
            HideLogo();
            LoadedModsCanvas = CanvasCreator.CreateCanvasIfNotExisted(_LOADED_MODS_CANVAS_NAME);
            ShadowedTextSupporter = ShadowedTextCreator.CreateText(_LOADED_MODS_TEXT_NAME, LoadedModsCanvas.transform);
            ShadowedTextSupporter.Text = "<color=#00AAAAFF>Loaded mods:</color>";
            ShadowedTextSupporter.Color = Color.cyan;
            ShadowedTextSupporter.AutoSize = true;
            ShadowedTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            ShadowedTextSupporter.Font = ModAssetsManager.LoadedModsTextFont;
            ShadowedTextSupporter.FontStyle = FontStyles.Bold;
            ShadowedTextSupporter.ShadowOffsetX = 2;
            ShadowedTextSupporter.ShadowOffsetY = -2;
            ShadowedTextSupporter.Font.material.EnableKeyword("OUTLINE_ON");
            ShadowedTextSupporter.OutlineWidth = 0.4f;
            ShadowedTextSupporter.WordWrapping = false;
            ShadowedTextRectTransform = ShadowedTextSupporter.GetComponent<RectTransform>();
            ShadowedTextRectTransform.anchorMin = new(0, 0);
            ShadowedTextRectTransform.anchorMax = new(0, 0);
            ShadowedTextRectTransform.offsetMin = Vector2.zero;
            ShadowedTextRectTransform.offsetMax = Vector2.zero;
            ShadowedTextRectTransform.position = new(10, 10, 0);
        }
        internal static void AddModsInfoToText()
        {
            if (!ShadowedTextSupporter)
                return;
            foreach (MelonMod mod in MelonMod.RegisteredMelons)
                ShadowedTextSupporter.Text += $"\n{mod.ToInfoString()}";
        }
        internal static void DestroyCanvas()
        {
            if (LoadedModsCanvas)
                Object.Destroy(LoadedModsCanvas.gameObject);
        }
        private static void HideLogo()
        {
            GameObject logo = GameObject.Find("MainMenuCanvas/MainMenuFHD/Leaves/Logo");
            if (logo)
                logo.SetActive(false);
        }
    }
}
