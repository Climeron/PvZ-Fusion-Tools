using System.Text;
using System.Linq;
using ClimeronToolsForPvZ.Extensions;

namespace ClimeronToolsForPvZ.Classes
{
    public static class ANSIColors
    {
        public enum ColorsEnum
        {
            Default = 7,
            Black = 0,
            Gray = 8,
            White = 15,
            DarkRed = 88,
            Red = 196,
            LightRed = 167,
            Orange = 202,
            Yellow = 226,
            LightGreen = 46,
            Green = 34,
            DarkGreen = 22,
            Emerald = 42,
            Turquoise = 51,
            DarkTurquoise = 30,
            Cyan = 39,
            Blue = 33,
            DarkBlue = 4,
            Purple = 93,
            Magenta = 201,
            Violet = 177,
            Pink = 219
        }
        /// <summary>Выводит в консоль спектр всех возможных значений перечисления <see cref="ColorsEnum"/> с использованием соответствующих ANSI цветов.</summary>
        public static void PrintSpectrum()
        {
            ColorsEnum[] values = typeof(ColorsEnum).GetValues().Cast<ColorsEnum>().ToArray();
            StringBuilder builder = new();
            foreach (ColorsEnum value in values)
                builder.Append($"{value.Set()}■ {value}{ColorsEnum.Default.Set()}{(value == values[^1] ? "" : "\n")}");
            builder.ToString().Print();
        }
        /// <summary>Устанавливает цвет текста в консоли, используя ANSI цвет из 256-цветной палитры.</summary>
        /// <param name="ansiColor">Значение перечисления, представляющее индекс ANSI цвета (от 0 до 255).</param>
        /// <returns>Строка, содержащая escape-последовательность для установки цвета текста в консоли.</returns>
        public static string Set(this ColorsEnum ansiColor) => $"\u001b[38;5;{(int)ansiColor}m";
    }
}