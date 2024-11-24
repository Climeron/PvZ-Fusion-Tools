using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Il2CppInterop.Runtime;

namespace ClimeronToolsForPvZ.Extensions
{
    public static partial class TypeExtensions
    {
        /// <summary>Преобразовывает экземпляр <see cref="Type"/> в эквивалентный <see cref="Il2CppSystem.Type"/>.</summary>
        /// <param name="type">Экземпляр <see cref="Type"/>, который нужно преобразовать.</param>
        /// <returns>Экземпляр <see cref="Il2CppSystem.Type"/>, соответствующий переданному <see cref="Type"/>.</returns>
        public static Il2CppSystem.Type ToIl2CppType(this Type type) => Il2CppType.From(type);

        /// <summary>Преобразовывает экземпляр <see cref="Il2CppSystem.Type"/> в эквивалентный <see cref="Type"/></summary>
        /// <param name="type">Экземпляр <see cref="Il2CppSystem.Type"/>, который нужно преобразовать.</param>
        /// <returns>Экземпляр <see cref="Type"/>, соответствующий переданному <see cref="Il2CppSystem.Type"/>.</returns>
        public static Type ToNormalType(this Il2CppSystem.Type type) => Type.GetType(type.FullName);

        /// <summary>Возвращает массив строковых значений указанного типа <b>перечисления</b> в порядке объявления.</summary>
        public static Enum[] GetValues(this Type type)
        {
            if (!type.IsEnum)
                $"The specified type must be an <Enum> type.".PrintError<ArgumentException>();
            FieldInfo[] fieldsArray = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            IEnumerable<FieldInfo> orderedFields = fieldsArray.OrderBy(field => field.MetadataToken);
            return orderedFields.Select(field => (Enum)field.GetValue(null)).ToArray();
        }
    }
}