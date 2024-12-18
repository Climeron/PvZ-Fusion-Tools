using MelonLoader;

namespace ClimeronToolsForPvZ.Extensions
{
    public static class MelonModExtensions
    {
        public static string ToInfoString(this MelonMod mod)
        {
            string output = $"{mod.Info.Name}_v{mod.Info.SemanticVersion}_by_{mod.Info.Author}";
            output += !string.IsNullOrEmpty(mod.Info.DownloadLink) ? $"\n\t<i>(Source: { mod.Info.DownloadLink})</i>" : string.Empty;
            return output;
        }
    }
}
