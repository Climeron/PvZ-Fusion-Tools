using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClimeronToolsForPvZ.Classes.NewtonsoftJsonIntegration;
using Newtonsoft.Json;

namespace ClimeronToolsForPvZ.Extensions
{
    public static class StringExtensions
    {
        #region JSON
        /// <summary>Читает JSON-строку и пытается преобразовать её в указанный обобщённый тип с обработкой исключений.</summary>
        /// <typeparam name="T">Тип объекта, в который нужно преобразовать строку.</typeparam>
        /// <param name="jsonString">JSON-строка для десериализации.</param>
        /// <returns>Объект указанного типа.</returns>
        public static T ReadJsonStringExceptionally<T>(this string jsonString) => (T)jsonString.ReadJsonStringExceptionally(typeof(T));
        /// <summary>Читает JSON-строку и пытается преобразовать её в указанный тип с обработкой исключений.</summary>
        /// <param name="jsonString">JSON-строка для десериализации.</param>
        /// <param name="type">Тип объекта, в который нужно преобразовать строку.</param>
        /// <returns>Объект указанного типа или <c>null</c> в случае ошибки.</returns>
        public static object ReadJsonStringExceptionally(this string jsonString, Type type)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                jsonString = "{\n}";
            try
            {
                return JsonConvert.DeserializeObject(jsonString, type);
            }
            catch (ArgumentException)
            {
                if (type.IsSubclassOf(typeof(UnityEngine.Object)))
                    $"Type <{type}> inherited from <{typeof(UnityEngine.Object).ToIl2CppType()}> and cannot be filled.".PrintError<ArgumentException>();
                else
                    $"JSON-string has invalid syntax:\n'{jsonString}'.".PrintError<ArgumentException>();
                return null;
            }
        }
        /// <summary>Читает содержимое JSON-файла и пытается преобразовать его в указанный обобщённый тип с обработкой исключений.</summary>
        /// <typeparam name="T">Тип объекта, в который нужно преобразовать данные.</typeparam>
        /// <param name="path">Путь к файлу JSON.</param>
        /// <returns>Объект указанного типа.</returns>
        public static T ReadJsonFileExceptionally<T>(this string path) where T : Il2CppSystem.Object => (T)path.ReadJsonFileExceptionally(typeof(T));
        /// <summary>Читает содержимое JSON-файла и пытается преобразовать его в указанный тип с обработкой исключений.</summary>
        /// <param name="path">Путь к файлу JSON.</param>
        /// <param name="type">Тип объекта, в который нужно преобразовать данные.</param>
        /// <returns>Объект указанного типа или <c>null</c> в случае исключения при чтении файла.</returns>
        public static object ReadJsonFileExceptionally(this string path, Type type)
        {
            object obj = null;
            if (File.Exists(path))
                obj = File.ReadAllText(path).ReadJsonStringExceptionally(type);
            else
                $"File at path '{path}' not found.".PrintError<FileNotFoundException>();
            return obj;
        }
        /// <summary>Упрощённая запись данных объекта в JSON файл по указанному пути.</summary>
        public static void WriteJson(this string path, Il2CppSystem.Object instance, bool prettyPrint = true) =>
            File.WriteAllText(path.CreateDirectories(false), JsonConvert.SerializeObject(instance, new CustomJsonSerializerSettings(prettyPrint)));
        #endregion
        /// <summary>Создаёт все необходимые директории на указанном пути. Если последний сегмент пути не является директорией, он игнорируется.</summary>
        /// <param name="path">Путь, по которому нужно создать директории.</param>
        /// <param name="lastIsDirectory">Указывает, является ли последний сегмент пути директорией.<br/>
        /// Если <c>true</c>, весь путь будет обработан как директория. Если <c>false</c>, последний сегмент пути будет пропущен.</param>
        /// <returns>Возвращает исходный путь.</returns>
        public static string CreateDirectories(this string path, bool lastIsDirectory)
        {
            if (lastIsDirectory)
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                List<string> foldersList = path.Split('/', '\\').ToList();
                foldersList.RemoveAt(foldersList.Count - 1);
                Directory.CreateDirectory(foldersList.Join("/"));
            }
            return path;
        }
    }
}
