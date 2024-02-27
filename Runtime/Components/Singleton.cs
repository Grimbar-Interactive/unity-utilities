using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace GI.UnityToolkit.Utilities.Components
{
    /// <summary>
    /// Ensures only one instance of a given MonoBehavior is present.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [UsedImplicitly]
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    // Search inactive objects
                    foreach (var instance in Resources.FindObjectsOfTypeAll<T>())
                    {
                        var isSceneInstance = ((instance.hideFlags & HideFlags.NotEditable) == 0) &&
                                              ((instance.hideFlags & HideFlags.HideAndDontSave) == 0);
#if UNITY_EDITOR
                        isSceneInstance = (isSceneInstance && !EditorUtility.IsPersistent(instance));
#endif
                        if (!isSceneInstance) continue;
                        _instance = instance;
                        break;
                    }
                }

                if (_instance != null) return _instance;

                Debug.LogWarning(
                    $"[Singleton.Instance] An instance of \"{typeof(T)}\" is needed in the scene, but there is none: Attempting to create one!");
                var go = new GameObject(typeof(T).ToString());
                _instance = go.AddComponent<T>();

                return _instance;
            }
        }

        public static bool Exists => _instance != null;
        private static T _instance;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Debug.Log(
                    $"[Singleton.Awake] Instance already exists, destroying awoken object of type \"{typeof(T)}\": \"{name}\"",
                    _instance);
                Destroy(this);
                return;
            }

            OnInitialization();
        }

        protected virtual void OnDestroy()
        {
            if (_instance != this) return;

            _instance = null;
            OnDeinitialization();
        }

        /// <summary>
        /// Called during Awake after the singleton instance has been set (if this is that instance).
        /// </summary>
        protected virtual void OnInitialization()
        {
        }

        /// <summary>
        /// Called during OnDestroy after the instance has been cleared (if this was the instance).
        /// </summary>
        protected virtual void OnDeinitialization()
        {
        }
    }
}