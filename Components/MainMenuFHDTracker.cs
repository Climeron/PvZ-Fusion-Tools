using System.Collections;
using ClimeronToolsForPvZ.Classes.UI;
using MelonLoader;
using UnityEngine;

namespace ClimeronToolsForPvZ.Components
{
    public class MainMenuFHDTracker : MonoBehaviour
    {
        private void Start()
        {
            MelonCoroutines.Start(PostStart());
        }
        private static IEnumerator PostStart()
        {
            yield return null;
            MainMenuCanvasManager.InitializeCanvas();
        }
    }
}
