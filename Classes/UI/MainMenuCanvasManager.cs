using ClimeronToolsForPvZ.Components;
using ClimeronToolsForPvZ.Classes.AssetsManagement;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using Il2Cpp;
using System;

namespace ClimeronToolsForPvZ.Classes.UI
{
    public class MainMenuCanvasManager : MonoBehaviour
    {
        public static MainMenuCanvasManager Instance { get; private set; }
        private const string _MAIN_MENU_CANVAS_NAME = "ClimeronToolsCanvas";
        private const string _GAME_TEXT_NAME = "GameNameText";
        private const string _LOADED_MODS_TEXT_NAME = "LoadedModsText";
        public Canvas Canvas { get; set; }
        public ShadowedTextSupporter GameTextSupporter => GameTextSupporterComponent as ShadowedTextSupporter;
        public Component GameTextSupporterComponent { get; private set; }
        public ShadowedTextSupporter LoadedModsTextSupporter => LoadedModsTextSupporterComponent as ShadowedTextSupporter;
        public Component LoadedModsTextSupporterComponent { get; private set; }
        public RectTransform GameTextRectTransform { get; private set; }
        public RectTransform LoadedModsTextRectTransform { get; private set; }

        private void OnDestroy() => Instance = null;
        internal static void InitializeCanvas()
        {
            Transform leaves = GameAPP.TravelMenu ? GameAPP.mainMenuCanvas.transform.Find("MainMenuFHD2/Leaves") : GameAPP.mainMenuCanvas.transform.Find("MainMenuFHD/Leaves");
            if (!leaves)
            {
                "Unable to position mod text.".PrintError<NullReferenceException>();
                return;
            }
            Instance = CanvasCreator.CreateCanvasIfNotExisted(_MAIN_MENU_CANVAS_NAME, leaves)
                .gameObject.AddComponent<MainMenuCanvasManager>();
            Instance.transform.localScale = Vector3.one;
            Instance.transform.localPosition = Vector3.zero;
            Instance.Canvas = Instance.GetComponent<Canvas>();
            Instance.CreateGameText();
            Instance.CreateLoadedModsText();
            Instance.AddModsInfoToText();
        }
        internal void CreateGameText()
        {
            GameTextSupporterComponent = ShadowedTextCreator.CreateText(_GAME_TEXT_NAME, Canvas.transform);
            GameTextSupporter.Text = $"<color=#A1D460FF>Plants</color> <color=#D3D5CBFF>vs</color> <color=#818D73FF>Zombies</color> <color=#20C9FAFF>Fusion v{MelonLoader.InternalUtils.UnityInformationHandler.GameVersion}</color>";
            GameTextSupporter.Size = 28;
            GameTextSupporter.Color = Color.cyan;
            GameTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            GameTextSupporter.Font = ModAssetsManager.MainTextFont;
            GameTextSupporter.FontStyle = FontStyles.Italic | FontStyles.SmallCaps | FontStyles.Bold;
            GameTextSupporter.ShadowOffsetX = 2;
            GameTextSupporter.ShadowOffsetY = -2;
            GameTextSupporter.Font.material.EnableKeyword("OUTLINE_ON");
            GameTextSupporter.OutlineWidth = 0.3f;
            GameTextSupporter.WordWrapping = false;
            GameTextRectTransform = GameTextSupporter.GetComponent<RectTransform>();
            GameTextRectTransform.localScale = Vector3.one;
            GameTextRectTransform.anchorMin = Vector2.zero;
            GameTextRectTransform.anchorMax = Vector2.zero;
            GameTextRectTransform.offsetMin = Vector2.zero;
            GameTextRectTransform.offsetMax = Vector2.zero;
            GameTextRectTransform.localPosition = new(-510, -300, 0);
        }
        internal void CreateLoadedModsText()
        {
            LoadedModsTextSupporterComponent = ShadowedTextCreator.CreateText(_LOADED_MODS_TEXT_NAME, Canvas.transform);
            LoadedModsTextSupporter.Text = "<color=#00AAAAFF>Loaded mods:</color>";
            LoadedModsTextSupporter.Size = 12;
            LoadedModsTextSupporter.Color = Color.cyan;
            LoadedModsTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            LoadedModsTextSupporter.Font = ModAssetsManager.MainTextFont;
            LoadedModsTextSupporter.FontStyle = FontStyles.Bold;
            LoadedModsTextSupporter.ShadowOffsetX = 1.5f;
            LoadedModsTextSupporter.ShadowOffsetY = -1f;
            LoadedModsTextSupporter.Font.material.EnableKeyword("OUTLINE_ON");
            LoadedModsTextSupporter.OutlineWidth = 0.4f;
            LoadedModsTextSupporter.WordWrapping = false;
            LoadedModsTextRectTransform = LoadedModsTextSupporter.GetComponent<RectTransform>();
            LoadedModsTextRectTransform.localScale = Vector3.one;
            LoadedModsTextRectTransform.anchorMin = Vector2.zero;
            LoadedModsTextRectTransform.anchorMax = Vector2.zero;
            LoadedModsTextRectTransform.offsetMin = Vector2.zero;
            LoadedModsTextRectTransform.offsetMax = Vector2.zero;
            LoadedModsTextRectTransform.localPosition = new(-510, -140, 0);
        }
        internal void AddModsInfoToText()
        {
            if (!LoadedModsTextSupporter)
                return;
            foreach (MelonMod mod in MelonMod.RegisteredMelons)
                LoadedModsTextSupporter.Text += $"\n{mod.ToInfoString()}";
        }
        internal static void Destroy()
        {
            if (Instance)
                Destroy(Instance.gameObject);
        }
    }
}
