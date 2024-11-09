using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ.Extensions
{
    public static class ObjectExtensions
    {
        public static void Print(this object obj) => MelonLogger.Msg(ConvertOutput(obj));
        public static void PrintError(this object obj) => MelonLogger.Error(ConvertOutput(obj));
        private static string ConvertOutput(this object obj) =>
            obj switch
            {
                null => "null",
                Transform transform => transform.GetPath(),
                GameObject gameObject => gameObject.transform.GetPath(),
                _ => obj.ToString()
            };
    }
}
