using UnityEngine;

namespace ClimeronToolsForPvZ.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>Возвращает полный путь до объекта в иерархии объектов сцены.</summary>
        public static string GetPath(this Transform transform) =>
            transform.parent == null ? "/" + transform.name : transform.parent.GetPath() + "/" + transform.name;
    }
}
