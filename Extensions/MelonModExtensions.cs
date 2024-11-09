using MelonLoader;

namespace ClimeronToolsForPvZ.Extensions
{
    public static class MelonModExtensions
    {
        public static string ToInfoString(this MelonMod mod) =>
            $"{mod.Info.Name}_v{mod.Info.SemanticVersion}_by_{mod.Info.Author}";
    }
}
