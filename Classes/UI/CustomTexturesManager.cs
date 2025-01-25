using UnityEngine;
using MelonLoader.Utils;
using System.IO;
using ClimeronToolsForPvZ.Extensions;

namespace ClimeronToolsForPvZ.Classes.UI
{
    public static class CustomTexturesManager
    {
        /// <summary>Сохраняет текстуру в файл PNG.</summary>
        /// <param name="texture">Текстура, которую нужно сохранить.</param>
        /// <param name="pathFromModsFolder">Путь к файлу текстуры относительно папки Mods.</param>
        public static void SaveAsPNG(this Texture2D texture, string pathFromModsFolder)
		{
			byte[] bytes = ImageConversion.EncodeToPNG(texture);
			string textureFilePath = Path.Combine(MelonEnvironment.ModsDirectory, pathFromModsFolder);
			File.WriteAllBytes(textureFilePath, bytes);
		}
        /// <summary>Загружает текстуру из указанного пути относительно папки Mods.</summary>
        /// <param name="pathFromModsFolder">Путь к файлу текстуры относительно папки Mods.</param>
        /// <returns>Загруженный объект <see cref="Texture2D"/>, либо null, если файл не найден или произошла ошибка загрузки.</returns>
        public static Texture2D LoadTexture(this string pathFromModsFolder)
		{
			string textureFilePath = Path.Combine(MelonEnvironment.ModsDirectory, pathFromModsFolder);
            if (!File.Exists(textureFilePath))
			{
				$"Texture file not found at path: '{textureFilePath}'.".PrintError<FileNotFoundException>();
				return null;
			}
			byte[] array = File.ReadAllBytes(textureFilePath);
			Texture2D texture2D = new(1, 1, TextureFormat.RGBA32, false);
            ImageConversion.LoadImage(texture2D, array);
			return texture2D;
		}
        /// <summary>Преобразует текстуру в спрайт с заданным именем и одинаковыми по ширине и высоте границами.</summary>
        /// <param name="texture">Текстура, которую нужно преобразовать в спрайт.</param>
        /// <param name="borderValue">Значение границы для всех сторон.</param>
        /// <returns>Новый спрайт, созданный из текстуры.</returns>
        public static Sprite ToSprite(this Texture2D texture, float borderValue = -1) =>
            texture.ToSprite(new Vector4(borderValue, borderValue, borderValue, borderValue));
        /// <summary>Преобразует текстуру в спрайт с заданным именем и границами.</summary>
        /// <param name="texture">Текстура, которую нужно преобразовать в спрайт.</param>
        /// <param name="border">Вектор, определяющий границы спрайта.</param>
        /// <returns>Новый спрайт, созданный из текстуры.</returns>
        public static Sprite ToSprite(this Texture2D texture, Vector4 border)
        {
            texture.wrapMode = TextureWrapMode.Clamp;
            return Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(0, 1), 100, 0, SpriteMeshType.FullRect, texture.GetFixedBorder(border));
        }
        /// <summary>Заменяет значения границы, в случае, если они не заданы или не корректны.</summary>
        /// <param name="texture">Текстура, от которой берутся размеры.</param>
        /// <param name="border">Вектор, определяющий границы спрайта.</param>
        /// <returns>Вектор с исправленными значениями границ.</returns>
        public static Vector4 GetFixedBorder(this Texture2D texture, Vector4 border)
        {
            if (border.x < 0) border.x = texture.width / 2;
            if (border.y < 0) border.y = texture.height / 2;
            if (border.z < 0) border.z = texture.width / 2;
            if (border.w < 0) border.w = texture.height / 2;
            return border;
        }

    }
}
