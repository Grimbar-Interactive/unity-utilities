using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    /// <summary>
    /// Used for starting Unity coroutines from non-MonoBehaviour classes.
    /// Credit to CykesDev: https://forum.unity.com/members/cykesdev.1703063/
    /// Unity forum post: https://forum.unity.com/threads/c-coroutines-in-static-functions.134546/
    /// </summary>
    public class StaticCoroutine : MonoBehaviour
    {
        private static StaticCoroutine _instance;

        // OnDestroy is called when the MonoBehaviour will be destroyed.
        // Coroutines are not stopped when a MonoBehaviour is disabled, but only when it is definitely destroyed.
        private void OnDestroy()
        {
            _instance.StopAllCoroutines();
        }

        // OnApplicationQuit is called on all game objects before the application is closed.
        // In the editor it is called when the user stops playmode.
        private void OnApplicationQuit()
        {
            _instance.StopAllCoroutines();
        }

        // Build will attempt to retrieve the class-wide instance, returning it when available.
        // If no instance exists, attempt to find another StaticCoroutine that exists.
        // If no StaticCoroutines are present, create a dedicated StaticCoroutine object.
        private static StaticCoroutine Build()
        {
            if (_instance != null)
            {
                return _instance;
            }

#if UNITY_6000_0_OR_NEWER
            _instance = (StaticCoroutine) FindFirstObjectByType(typeof(StaticCoroutine));
#else
            _instance = (StaticCoroutine) FindObjectOfType(typeof(StaticCoroutine));
#endif

            if (_instance != null) return _instance;

            var instanceObject = new GameObject("[StaticCoroutine]");
            instanceObject.AddComponent<StaticCoroutine>();
            _instance = instanceObject.GetComponent<StaticCoroutine>();

            if (_instance != null) return _instance;
            Debug.LogError("[StaticCoroutine] Build did not generate a replacement instance. Method Failed!");
            return null;
        }

        // Overloaded Static Coroutine Methods which use Unity's default Coroutines.
        // Polymorphism applied for best compatibility with the standard engine.
        [UsedImplicitly]
        public static Coroutine StartRoutine(string methodName)
        {
            return Build().StartCoroutine(methodName);
        }

        [UsedImplicitly]
        public static Coroutine StartRoutine(string methodName, object value)
        {
            return Build().StartCoroutine(methodName, value);
        }

        [UsedImplicitly]
        public static Coroutine StartRoutine(IEnumerator routine)
        {
            return Build().StartCoroutine(routine);
        }

        [UsedImplicitly]
        public static void StopRoutine(Coroutine routine)
        {
            Build().StopCoroutine(routine);
        }

        [UsedImplicitly]
        public static void StopRoutine(string methodName)
        {
            Build().StopCoroutine(methodName);
        }
    }
}