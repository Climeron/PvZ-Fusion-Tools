using ClimeronToolsForPvZ.Components;
using ClimeronToolsForPvZ.Classes.AssetsManagement;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes.UI
{
    public static class MainMenuCanvasManager
    {
        private const string _MAIN_MENU_CANVAS_NAME = "ClimeronToolsCanvas";
        private const string _GAME_TEXT_NAME = "GameNameText";
        private const string _LOADED_MODS_TEXT_NAME = "LoadedModsText";
        public static Canvas Canvas { get; set; }
        public static ShadowedTextSupporter GameTextSupporter { get; private set; }
        public static ShadowedTextSupporter LoadedModsTextSupporter { get; private set; }
        public static RectTransform GameTextRectTransform { get; private set; }
        public static RectTransform LoadedModsTextRectTransform { get; private set; }
        internal static void InitializeCanvas()
        {
            HideLogo();
            Canvas = CanvasCreator.CreateCanvasIfNotExisted(_MAIN_MENU_CANVAS_NAME);
            CreateGameText();
            CreateLoadedModsText();
            AddModsInfoToText();
        }
        internal static void CreateGameText()
        {
            GameTextSupporter = ShadowedTextCreator.CreateText(_GAME_TEXT_NAME, Canvas.transform);
            GameTextSupporter.Text = $"<color=#A1D460FF>Plants</color> <color=#D3D5CBFF>vs</color> <color=#818D73FF>Zombies</color> <color=#20C9FAFF>Fusion v{MelonLoader.InternalUtils.UnityInformationHandler.GameVersion}</color>";
            GameTextSupporter.Color = Color.cyan;
            GameTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            GameTextSupporter.Font = ModAssetsManager.MainTextFont;
            GameTextSupporter.FontStyle = FontStyles.Italic | FontStyles.SmallCaps | FontStyles.Bold;
            GameTextSupporter.Size = 36;
            GameTextSupporter.ShadowOffsetX = 3;
            GameTextSupporter.ShadowOffsetY = -3;
            GameTextSupporter.Font.material.EnableKeyword("OUTLINE_ON");
            GameTextSupporter.OutlineWidth = 0.3f;
            GameTextSupporter.WordWrapping = false;
            GameTextRectTransform = GameTextSupporter.GetComponent<RectTransform>();
            GameTextRectTransform.anchorMin = new(0, 0);
            GameTextRectTransform.anchorMax = new(0, 0);
            GameTextRectTransform.offsetMin = Vector2.zero;
            GameTextRectTransform.offsetMax = Vector2.zero;
            GameTextRectTransform.position = new(10, 10, 0);
        }
        internal static void CreateLoadedModsText()
        {
            LoadedModsTextSupporter = ShadowedTextCreator.CreateText(_LOADED_MODS_TEXT_NAME, Canvas.transform);
            LoadedModsTextSupporter.Text = "<color=#00AAAAFF>Loaded mods:</color>";
            LoadedModsTextSupporter.Color = Color.cyan;
            LoadedModsTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            LoadedModsTextSupporter.Font = ModAssetsManager.MainTextFont;
            LoadedModsTextSupporter.FontStyle = FontStyles.Bold;
            LoadedModsTextSupporter.Size = 22;
            LoadedModsTextSupporter.ShadowOffsetX = 2;
            LoadedModsTextSupporter.ShadowOffsetY = -2;
            LoadedModsTextSupporter.Font.material.EnableKeyword("OUTLINE_ON");
            LoadedModsTextSupporter.OutlineWidth = 0.4f;
            LoadedModsTextSupporter.WordWrapping = false;
            LoadedModsTextRectTransform = LoadedModsTextSupporter.GetComponent<RectTransform>();
            LoadedModsTextRectTransform.anchorMin = new(0, 0);
            LoadedModsTextRectTransform.anchorMax = new(0, 0);
            LoadedModsTextRectTransform.offsetMin = Vector2.zero;
            LoadedModsTextRectTransform.offsetMax = Vector2.zero;
            LoadedModsTextRectTransform.position = new(10, 55, 0);
        }
        internal static void AddModsInfoToText()
        {
            if (!LoadedModsTextSupporter)
                return;
            foreach (MelonMod mod in MelonMod.RegisteredMelons)
                LoadedModsTextSupporter.Text += $"\n{mod.ToInfoString()}";
        }
        internal static void DestroyCanvas()
        {
            if (Canvas)
                Object.Destroy(Canvas.gameObject);
        }
        private static void HideLogo()
        {
            GameObject logo = GameObject.Find("MainMenuCanvas/MainMenuFHD/Leaves/Logo");
            if (logo)
                logo.SetActive(false);
        }
    }
}
