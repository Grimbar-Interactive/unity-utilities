using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class LogUtilities
    {
        public static void Log(this Object obj, string message)
        {
            Debug.Log($"{(obj != null ? $"[{obj.GetType().Name}]" : "")} {message}", obj);
        }
        
        public static void LogWarning(this Object obj, string message)
        {
            Debug.LogWarning($"{(obj != null ? $"[{obj.GetType().Name}]" : "")} {message}", obj);
        }
        
        public static void LogError(this Object obj, string message)
        {
            Debug.LogError($"{(obj != null ? $"[{obj.GetType().Name}]" : "")} {message}", obj);
        }
    }
}