using System;
using System.Text;
using ClimeronToolsForPvZ.Classes;
using ClimeronToolsForPvZ.Classes.AssetsManagement;
using ClimeronToolsForPvZ.Components;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ
{
    public class Main : MelonMod
    {
        public static Main Instance { get; private set; }
        public Settings Settings { get; private set; }
        private readonly bool _debug;

        public override void OnGUI()
        {
            if (!_debug)
                return;
            GUIStyle debugStyle = GUI.skin.label;
            debugStyle.fontSize = 32;
            if (Camera.main != null)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GUI.Label(new(0, 0, Screen.width, Screen.height), $"Cursor pos: {worldPosition.x:F2}, {worldPosition.y:F2}, {worldPosition.z:F2}", debugStyle);
            }
            else
            {
                GUI.Label(new(0, 0, Screen.width, Screen.height), "Camera not found!", debugStyle);
            }
        }
        public override void OnInitializeMelon()
        {
            Console.OutputEncoding = Encoding.UTF8;
            RegisterComponents();
            Instance = this;
            ModAssetsManager.Initialize();
            Settings = Settings.Load();
            ModAssetsManager.LoadAssets(Settings);
            UnityExplorerIntegration.Initialize();
            MouseBlocker.Initialize();
        }
        private void RegisterComponents()
        {
            ClassInjector.RegisterTypeInIl2Cpp<Settings>();
            ClassInjector.RegisterTypeInIl2Cpp<MouseBlocker>();
            ClassInjector.RegisterTypeInIl2Cpp<ShadowedTextSupporter>();
        }
    }
}