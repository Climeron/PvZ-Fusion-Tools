using System;
using ClimeronToolsForPvZ.Classes;
using ClimeronToolsForPvZ.Extensions;
using UnityEngine;

namespace ClimeronToolsForPvZ.Components
{
    /// <summary>Компонент, блокирующий клики по интерфейсу. Полезен при работе с UnityExplorer.</summary>
    public class MouseBlocker : MonoBehaviour
    {
        public static MouseBlocker Instance { get; private set; }
        public static bool Initialized { get; private set; }

        public static void Initialize()
        {
            if (!UnityExplorerIntegration.Initialized)
                return;
            CreateOrDestroy(UnityExplorerIntegration.ShowMenu);
            if (!Initialized)
                UnityExplorerIntegration.onShowMenuChanged += CreateOrDestroy;
            Initialized = true;
        }
        private static void CreateOrDestroy(bool value)
        {
            if (value)
                Create();
            else
                TryDestroy();
        }
        private static MouseBlocker Create()
        {
            if (Instance)
            {
                "Object 'MouseBlocker' already created.".PrintError<InvalidOperationException>();
                return null;
            }
            Instance = new GameObject("MouseBlocker").AddComponent<MouseBlocker>();
            return Instance;
        }
        private static void TryDestroy()
        {
            if (!Instance)
                return;
            Destroy(Instance.gameObject);
        }
        private void Awake()
        {
            transform.position = new(0, 0, -150);
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("UI");
            ResizeColliderToScreen(collider);
        }
        private void ResizeColliderToScreen(BoxCollider2D collider)
        {
            if (Camera.main == null)
            {
                "Main camera not found!".PrintError<NullReferenceException>();
                return;
            }
            collider.size = Camera.main.ScreenToWorldPoint(new(Screen.width, Screen.height)) * 2;
            collider.offset = Vector2.zero;
        }
    }
}