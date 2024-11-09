using ClimeronToolsForPvZ.Components;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes
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
            LoadedModsCanvas = CanvasCreator.CreateCanvasIfNotExisted(_LOADED_MODS_CANVAS_NAME);
            ShadowedTextSupporter = ShadowedTextCreator.CreateText(_LOADED_MODS_TEXT_NAME, LoadedModsCanvas.transform);
            ShadowedTextRectTransform = ShadowedTextSupporter.GetComponent<RectTransform>();
            ShadowedTextSupporter.Text = "Loaded mods:";
            ShadowedTextSupporter.Size = 26;
            ShadowedTextSupporter.Color = Color.gray;
            ShadowedTextSupporter.Alignment = TextAlignmentOptions.TopRight;
            ShadowedTextSupporter.FontStyle = FontStyles.Bold;
            ShadowedTextSupporter.ShadowOffsetX = 2;
            ShadowedTextSupporter.ShadowOffsetY = -1;
            ShadowedTextSupporter.WordWrapping = false;
            ShadowedTextSupporter.OutlineWidth = 0.3f;
            ShadowedTextRectTransform.anchorMin = Vector2.one;
            ShadowedTextRectTransform.anchorMax = Vector2.one;
            ShadowedTextRectTransform.pivot = Vector2.one;
            ShadowedTextRectTransform.offsetMin = Vector3.zero;
            ShadowedTextRectTransform.offsetMax = Vector3.zero;
            ShadowedTextRectTransform.anchoredPosition = new(-20, 0);
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
    }
}
