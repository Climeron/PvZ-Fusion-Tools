using ClimeronToolsForPvZ.Components;
using UnityEngine;

namespace ClimeronToolsForPvZ.Classes
{
    public static class ShadowedTextCreator
    {
        public static ShadowedTextSupporter CreateText(string name, Transform parent = null)
        {
            ShadowedTextSupporter textSupporter = new GameObject(name).AddComponent<ShadowedTextSupporter>();
            if (parent)
                textSupporter.transform.SetParent(parent);
            return textSupporter;
        }
    }
}
