using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public class Screenshotter : MonoBehaviour
    {
        [SerializeField] private string filePath = null;

        [Button("Take Screenshot")]
        private void Screenshot()
        {
            Debug.Log("Screenshot taken!");
            ScreenCapture.CaptureScreenshot($"{filePath}{DateTime.UtcNow.ToFileTimeUtc().ToString()}.png", 4);
        }
    }
}