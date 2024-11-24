using ClimeronToolsForPvZ.Classes.UI;
using HarmonyLib;
using Il2Cpp;

namespace ClimeronToolsForPvZ.Patches
{
    [HarmonyPatch(typeof(MainMenu_Btn))]
    public static class MainMenu_BtnPatches
    {
        [HarmonyPatch(nameof(MainMenu_Btn.OnMouseUp))]
        [HarmonyPostfix]
        private static void PostOnMouseUp(MainMenu_Btn __instance)
        {
            switch (__instance.buttonNumber)
            {
                case int n when n >= 0 && n <= 6:
                case 11:
                    LoadedModsCanvasManager.DestroyCanvas();
                    break;
            }
        }
    }
}
