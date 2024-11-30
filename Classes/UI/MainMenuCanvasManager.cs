using ClimeronToolsForPvZ.Components;
using ClimeronToolsForPvZ.Classes.AssetsManagement;
using ClimeronToolsForPvZ.Extensions;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

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

        private void Update() => UpdateSupportersScale();
        private void OnDestroy() => Instance = null;
        internal static void InitializeCanvas()
        {
            HideLogo();
            Instance = CanvasCreator.CreateCanvasIfNotExisted(_MAIN_MENU_CANVAS_NAME)
                .gameObject.AddComponent<MainMenuCanvasManager>();
            Instance.Canvas = Instance.GetComponent<Canvas>();
            Instance.CreateGameText();
            Instance.CreateLoadedModsText();
            Instance.AddModsInfoToText();
        }
        internal void CreateGameText()
        {
            GameTextSupporterComponent = ShadowedTextCreator.CreateText(_GAME_TEXT_NAME, Canvas.transform);
            GameTextSupporter.Text = $"<color=#A1D460FF>Plants</color> <color=#D3D5CBFF>vs</color> <color=#818D73FF>Zombies</color> <color=#20C9FAFF>Fusion v{MelonLoader.InternalUtils.UnityInformationHandler.GameVersion}</color>";
            GameTextSupporter.Size = 34;
            GameTextSupporter.Color = Color.cyan;
            GameTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            GameTextSupporter.Font = ModAssetsManager.MainTextFont;
            GameTextSupporter.FontStyle = FontStyles.Italic | FontStyles.SmallCaps | FontStyles.Bold;
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
        internal void CreateLoadedModsText()
        {
            LoadedModsTextSupporterComponent = ShadowedTextCreator.CreateText(_LOADED_MODS_TEXT_NAME, Canvas.transform);
            LoadedModsTextSupporter.Text = "<color=#00AAAAFF>Loaded mods:</color>";
            LoadedModsTextSupporter.Size = 20;
            LoadedModsTextSupporter.Color = Color.cyan;
            LoadedModsTextSupporter.Alignment = TextAlignmentOptions.BottomLeft;
            LoadedModsTextSupporter.Font = ModAssetsManager.MainTextFont;
            LoadedModsTextSupporter.FontStyle = FontStyles.Bold;
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
        internal void AddModsInfoToText()
        {
            if (!LoadedModsTextSupporter)
                return;
            foreach (MelonMod mod in MelonMod.RegisteredMelons)
                LoadedModsTextSupporter.Text += $"\n{mod.ToInfoString()}";
        }
        private void UpdateSupportersScale()
        {
            float scaleFactor = (float)(Screen.height + 150) / 1080;
            if (GameTextSupporter)
                GameTextSupporter.transform.localScale = new(scaleFactor, scaleFactor, 1);
            if (LoadedModsTextSupporter)
            {
                LoadedModsTextSupporter.transform.position = new(10, 10 + scaleFactor * 35, 0);
                LoadedModsTextSupporter.transform.localScale = new(scaleFactor, scaleFactor, 1);
            }
        }
        internal static void Destroy()
        {
            if (Instance)
                Destroy(Instance.gameObject);
        }
        private static void HideLogo()
        {
            GameObject logo = GameObject.Find("MainMenuCanvas/MainMenuFHD/Leaves/Logo");
            if (logo)
                logo.SetActive(false);
        }
    }
}
