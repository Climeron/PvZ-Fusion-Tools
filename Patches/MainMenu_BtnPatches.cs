using ClimeronToolsForPvZ.Classes;
using HarmonyLib;
using Il2Cpp;

namespace ClimeronToolsForPvZ.Patches
{
    [HarmonyPatch(typeof(MainMenu_Btn))]
    public static class MainMenu_BtnPatches
    {
        [HarmonyPatch(nameof(MainMenu_Btn.OnMouseUp))]
        [HarmonyPostfix]
        private static void PostUpdate(MainMenu_Btn __instance)
        {
            switch (__instance.buttonNumber)
            {
                case int n when n >= 0 && n <= 3:
                case 5:
                    LoadedModsCanvasManager.DestroyCanvas();
                    break;
            }
        }
    }
}
