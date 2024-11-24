using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ClimeronToolsForPvZ.Classes;

namespace ClimeronToolsForPvZ.Extensions
{
    using MelonColorsEnum = ANSIColors.ColorsEnum;
    public static partial class StackTraceExtensions
    {
        /// <summary>Возвращает данные об указанном кадре стека вызовов.</summary>
        /// <param name="stackTrace">Стек вызовов, с которого возвращается информация.</param>
        /// <param name="frameIndex">Номер кадра стека, с которого нужно получить информацию.</param>
        /// <param name="typeName">Имя типа, в котором находится метод, указанный в кадре.</param>
        /// <param name="methodName">Имя метода, вызов которого привел к данному кадру в стеке вызовов.</param>
        /// <param name="parameters">Параметры метода, вызов которого привел к данному кадру в стеке вызовов, в формате [тип параметра имя параметра].</param>
        public static void GetStackTraceInfo(this StackTrace stackTrace, int frameIndex, out string typeName, out string methodName, out string parameters)
        {
            StackFrame frame = stackTrace.GetFrameStrictly(frameIndex);
            MethodBase method = frame.GetMethod();
            typeName = method.DeclaringType?.Name;
            methodName = method.Name;
            IEnumerable<string> parametersTypes = method.GetParameters().Select(x => x.ParameterType.Name);
            IEnumerable<string> parametersNames = method.GetParameters().Select(x => x.Name ?? "UNDEFINED_NAME");
            parameters = parametersTypes.PairMerge(parametersNames).Join(" ", ", ");
        }
        /// <summary>Возвращает строку с информацией о точке входа в стек вызовов,
        /// отформатированную с использованием заданных цветов для имени файла, имени метода и имен параметров.</summary>
        /// <param name="stackTrace">Стек вызовов, с которого возвращается информация.</param>
        /// <param name="frameIndex">Номер кадра стека, с которого нужно получить информацию.</param>
        /// <param name="typeNameColor">Цвет, используемый для форматирования имени типа.</param>
        /// <param name="methodNameColor">Цвет, используемый для форматирования имени метода.</param>
        /// <param name="parametersColor">Цвет, используемый для форматирования параметров.</param>
        public static string GetEntryPoint(this StackTrace stackTrace, int frameIndex, MelonColorsEnum typeNameColor, MelonColorsEnum methodNameColor, MelonColorsEnum parametersColor)
        {
            GetStackTraceInfo(stackTrace, frameIndex, out string typeName, out string methodName, out string parameters);
            string coloredTypeName = !string.IsNullOrEmpty(typeName) ? $"{typeNameColor.Set()}{typeName}.cs{MelonColorsEnum.Default.Set()}" : "";
            string coloredMethodName = $"{methodNameColor.Set()}{methodName}{MelonColorsEnum.Default.Set()}";
            string[] parametersArray = parameters.Split(',');
            if (!string.IsNullOrEmpty(parameters))
                for (int i = 0; i < parametersArray.Length; i++)
                    parametersArray[i] = $"{parametersColor.Set()}{parametersArray[i].Trim().Replace(" ", $"{MelonColorsEnum.Default.Set()} ")}";
            string coloredParametersTypes = string.Join(", ", parametersArray);
            return $"{(!string.IsNullOrEmpty(coloredTypeName) ? $"{coloredTypeName} -> " : "")}{coloredMethodName}({coloredParametersTypes})";
        }
        /// <summary>Возвращает строку с информацией о точке входа в стек вызовов.</summary>
        /// <param name="stackTrace">Стек вызовов, с которого возвращается информация.</param>
        /// <param name="frameIndex">Номер кадра, с которого нужно получить информацию.</param>
        public static string GetEntryPoint(this StackTrace stackTrace, int frameIndex)
        {
            GetStackTraceInfo(stackTrace, frameIndex, out string typeName, out string methodName, out string parameters);
            return $"{(!string.IsNullOrEmpty(typeName) ? $"{typeName}.cs -> " : "")}{methodName}({parameters})";
        }
        /// <summary>Получает кадр стека с указанным индексом из данного <see cref="StackTrace"/>. 
        /// Если кадр с таким индексом отсутствует, выбрасывает исключение <see cref="NullReferenceException"/>.</summary>
        /// <param name="stackTrace">Трассировка стека, из которой нужно получить кадр.</param>
        /// <param name="frameIndex">Индекс кадра в трассировке стека, который нужно получить.</param>
        /// <returns>Кадр стека с указанным индексом.</returns>
        /// <exception cref="NullReferenceException">Бросается, если трассировка стека не содержит кадр с указанным индексом.</exception>
        public static StackFrame GetFrameStrictly(this StackTrace stackTrace, int frameIndex) =>
            stackTrace.GetFrame(frameIndex) ?? throw new ArgumentOutOfRangeException($"StackTrace does not contain a frame with a number {frameIndex}.");
    }
}