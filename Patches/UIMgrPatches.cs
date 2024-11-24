using ClimeronToolsForPvZ.Classes.UI;
using HarmonyLib;
using Il2Cpp;

namespace ClimeronToolsForPvZ.Patches
{
    [HarmonyPatch(typeof(UIMgr))]
    public static class UIMgrPatches
    {
        [HarmonyPatch(nameof(UIMgr.EnterMainMenu))]
        [HarmonyPostfix]
        public static void PostEnterMainMenu(UIMgr __instance)
        {
            LoadedModsCanvasManager.CreateLoadedModsText();
            LoadedModsCanvasManager.AddModsInfoToText();
        }
    }
}
