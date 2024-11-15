﻿using ClimeronToolsForPvZ.Components;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ
{
    public class Main : MelonMod
    {
        private static readonly bool _debug = false;

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
            RegisterComponents();
        }
        private static void RegisterComponents()
        {
            ClassInjector.RegisterTypeInIl2Cpp<ShadowedTextSupporter>();
        }
    }
}
