using ClimeronToolsForPvZ.Components;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes.PrefabsEditors
{
    public static class MainMenuFHDPrefabEditor
    {
        public static void ApplyEdits()
        {
            Resources.Load<GameObject>("UI/MainMenu/MainMenuFHD").AddComponent<MainMenuFHDTracker>();
            Resources.Load<GameObject>("UI/MainMenu/MainMenuFHD2").AddComponent<MainMenuFHDTracker>();
        }
    }
}
