using UnityEngine;

namespace ClimeronToolsForPvZ.Classes
{
    public static class CanvasCreator
    {
        /// <summary>Возвращает <see cref="Canvas"/> с указанным названием. Если указан родитель, ищет его среди дочерних объектов.<br/>
        /// В случае, если объект не найден, создаёт новый относительно указанного родителя.</summary>
        /// <param name="canvasName">Имя Canvas для поиска или создания.</param>
        /// <param name="parent">(Опционально) Родительский трансформ для Canvas.</param>
        /// <returns>Найденный или только что созданный компонент Canvas.</returns>
        public static Canvas CreateCanvasIfNotExisted(string canvasName, Transform parent = null)
        {
            Transform canvasTransform = parent ? parent.Find(canvasName) : GameObject.Find(canvasName)?.transform;
            if (!canvasTransform)
            {
                GameObject gObject = new(canvasName);
                canvasTransform = gObject.transform;
                gObject.transform.SetParent(parent);
            }
            Canvas canvas = canvasTransform.GetComponent<Canvas>() ?? canvasTransform.gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            return canvas;
        }
    }
}
