using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using ClimeronToolsForPvZ.Classes;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ.Extensions
{
    using ANSIColorsEnum = ANSIColors.ColorsEnum;
    public static class ObjectExtensions
    {
        public static void Print(this object obj) => MelonLogger.Msg(obj.ModifyOutput(false));
        public static void PrintWarning(this object obj) => MelonLogger.Warning(obj.ModifyOutput(true));
        public static void PrintError<EXCEPTION_TYPE>(this object obj) where EXCEPTION_TYPE : Exception => obj.ModifiedPrint<EXCEPTION_TYPE>();
        /// <summary>Модифицирует сообщение с указанием места вывода.</summary>
        internal static string ModifyOutput(this object output, bool isWarning) => GetEntryPointAndContentString(output, isWarning);
        /// <summary>Модифицирует сообщение об исключении с указанием места вывода.</summary>
        internal static void ModifiedPrint<EXCEPTION_TYPE>(this object output) where EXCEPTION_TYPE : Exception
        {
            string contentString = $"{GetEntryPointAndContentString(output, false)}{ANSIColorsEnum.LightRed.Set()}\n{new StackTrace()}";
            MelonLogger.Error(contentString, Activator.CreateInstance(typeof(EXCEPTION_TYPE)) as EXCEPTION_TYPE);
        }
        /// <summary>Выводит строку с информацией о месте вывода и возвращает отформатированное сообщение.</summary>
        private static string GetEntryPointAndContentString(object output, bool isWarning)
        {
            const int frameIndex = 3;
            StringBuilder outputString = new(new StackTrace(true)
                .GetEntryPoint(frameIndex, ANSIColorsEnum.Violet, ANSIColorsEnum.Emerald, ANSIColorsEnum.Violet));
            outputString.Append('\n');
            if (isWarning)
                outputString.Append(ANSIColorsEnum.Yellow.Set());
            switch (output)
            {
                case null:
                    outputString.Append("null");
                    break;
                case Transform transform:
                    outputString.Append(transform.GetPath());
                    break;
                case GameObject gameObject:
                    outputString.Append(gameObject.transform.GetPath());
                    break;
                case IEnumerable collection and not string:
                    outputString.Append(collection.ToNumberedFormat());
                    break;
                default:
                    outputString.Append(output);
                    break;
            }
            if (isWarning)
                outputString.Append(ANSIColorsEnum.Default.Set());
            return outputString.ToString();
        }
    }
}
