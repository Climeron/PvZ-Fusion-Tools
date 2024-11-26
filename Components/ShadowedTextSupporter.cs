using Il2CppSystem.Text.RegularExpressions;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClimeronToolsForPvZ.Components
{
    public class ShadowedTextSupporter : MonoBehaviour
    {
        public enum TextType { Normal, Shadow }
        public Canvas Canvas { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public Image PivotImage { get; set; }
        public bool ShowPivot
        {
            get => PivotImage.gameObject.activeSelf;
            set => PivotImage.gameObject.SetActive(value);
        }
        public TextMeshProUGUI NormalText { get; private set; }
        public TextMeshProUGUI ShadowText { get; private set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NormalText.name = $"{value}Normal";
                ShadowText.name = $"{value}Shadow";
            }
        }
        public Vector3 Position
        {
            get => NormalText.transform.position;
            set
            {
                NormalText.transform.position = value;
                ShadowText.transform.position = value + new Vector3(ShadowOffsetX, ShadowOffsetY, 0);
            }
        }
        public Vector3 LocalPosition
        {
            get => NormalText.transform.localPosition;
            set
            {
                NormalText.transform.localPosition = value;
                ShadowText.transform.localPosition = value + new Vector3(ShadowOffsetX, ShadowOffsetY, 0);
            }
        }
        public string Text
        {
            get => NormalText.text;
            set
            {
                NormalText.text = value;
                ShadowText.text = Regex.Replace(value, @"<color=#([0-9a-fA-F]{8})>(.*?)<\/color>", "$2");
            }
        }
        public float Size
        {
            get => NormalText.fontSize;
            set
            {
                NormalText.fontSize = value;
                ShadowText.fontSize = value;
            }
        }
        public Color Color
        {
            get => NormalText.color;
            set => NormalText.color = value;
        }
        public Color ShadowColor
        {
            get => NormalText.color;
            set => NormalText.color = value;
        }
        public TextAlignmentOptions Alignment
        {
            get => NormalText.alignment;
            set
            {
                NormalText.alignment = value;
                ShadowText.alignment = value;
            }
        }
        public TMP_FontAsset Font
        {
            get => NormalText.font;
            set
            {
                NormalText.font = value;
                ShadowText.font = value;
            }
        }
        public FontStyles FontStyle
        {
            get => NormalText.fontStyle;
            set
            {
                NormalText.fontStyle = value;
                ShadowText.fontStyle = value;
            }
        }
        public float ShadowOffsetX
        {
            get => ShadowText.transform.position.x - NormalText.transform.position.x;
            set => ShadowText.transform.position = new(NormalText.transform.position.x + value, ShadowText.transform.position.y, ShadowText.transform.position.z);
        }
        public float ShadowOffsetY
        {
            get => ShadowText.transform.position.y - NormalText.transform.position.y;
            set => ShadowText.transform.position = new(ShadowText.transform.position.x, NormalText.transform.position.y + value, ShadowText.transform.position.z);
        }
        public bool WordWrapping
        {
            get => NormalText.enableWordWrapping;
            set
            {
                NormalText.enableWordWrapping = value;
                ShadowText.enableWordWrapping = value;
            }
        }
        public float OutlineWidth
        {
            get => NormalText.fontMaterial.GetFloat(ShaderUtilities.ID_OutlineWidth);
            set => NormalText.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, value);
        }
        public Color OutlineColor
        {
            get => NormalText.fontMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
            set => NormalText.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, value);
        }
        public bool DrawAlwaysOnTop { get; set; }

        private void Awake()
        {
            InitializeCanvas();
            RectTransform = GetComponent<RectTransform>();
            ShadowText = CreateText(TextType.Shadow);
            NormalText = CreateText(TextType.Normal);
        }
        private void Update()
        {
            if (DrawAlwaysOnTop)
                transform.SetAsLastSibling();
        }
        private void InitializeCanvas()
        {
            Canvas = GetComponent<Canvas>() ?? gameObject.AddComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Canvas.overrideSorting = true;
            Canvas.sortingOrder = 1;
            Canvas.sortingLayerID = -1823021693;
            Canvas.sortingLayerName = "UI";
            CreatePivotImage();
        }
        private void CreatePivotImage()
        {
            PivotImage = new GameObject(nameof(PivotImage)).AddComponent<Image>();
            PivotImage.color = Color.white;
            RectTransform pivotRectTransform = PivotImage.gameObject.GetComponent<RectTransform>();
            pivotRectTransform.SetParent(Canvas.transform);
            pivotRectTransform.sizeDelta = new(10, 10);
            pivotRectTransform.localPosition = Vector3.zero;
            ShowPivot = false;
        }
        private TextMeshProUGUI CreateText(TextType textType)
        {
            GameObject textObject = new();
            Transform textTransform = textObject.transform;
            textTransform.name = $"Text{(textType == TextType.Normal ? "Normal" : "Shadow")}";
            textTransform.SetParent(RectTransform);
            textTransform.localPosition = Vector3.zero;
            textTransform.localScale = new(1, 1, 1);
            if (textType == TextType.Shadow)
                textTransform.localPosition += new Vector3(1, -1, 0);
            RectTransform rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = Vector2.zero;
            TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
            Material outlineMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Outline");
            textComponent.fontSharedMaterial = outlineMaterial;
            textComponent.color = textType switch
            {
                TextType.Normal => Color.white,
                TextType.Shadow => Color.black,
                _ => new()
            };
            return textComponent;
        }
    }
}
