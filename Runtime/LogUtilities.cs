using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if GI_LOGGING_ENABLED_DEBUG
#endif

namespace GI.UnityToolkit.Utilities
{
    public static class LogUtilities
    {
        public const string LOGGING_DEBUG_DEFINE = "GI_LOGGING_ENABLED_DEBUG";
        public const string LOGGING_RELEASE_DEFINE = "GI_LOGGING_ENABLED_RELEASE";

#if DEBUG
        [Conditional(LOGGING_DEBUG_DEFINE)]
#else
        [Conditional(LOGGING_RELEASE_DEFINE)]
#endif
        public static void Log(this Object obj, string message, [CallerMemberName] string callerName = "")
        {
            Debug.Log($"{(obj != null ? $"[{obj.GetType().Name}.{callerName}()]" : "")} {message}", obj);
        }

#if DEBUG
        [Conditional(LOGGING_DEBUG_DEFINE)]
#else
        [Conditional(LOGGING_RELEASE_DEFINE)]
#endif
        public static void LogWarning(this Object obj, string message, [CallerMemberName] string callerName = "")
        {
            Debug.LogWarning($"{(obj != null ? $"[{obj.GetType().Name}.{callerName}()]" : "")} {message}", obj);
        }

#if DEBUG
        [Conditional(LOGGING_DEBUG_DEFINE)]
#else
        [Conditional(LOGGING_RELEASE_DEFINE)]
#endif
        public static void LogError(this Object obj, string message, [CallerMemberName] string callerName = "")
        {
            Debug.LogError($"{(obj != null ? $"[{obj.GetType().Name}.{callerName}()]" : "")} {message}", obj);
        }

#if DEBUG
        [Conditional(LOGGING_DEBUG_DEFINE)]
#else
        [Conditional(LOGGING_RELEASE_DEFINE)]
#endif
        public static void Log(object obj, string message, [CallerMemberName] string callerName = "")
        {
            Debug.Log($"{(obj != null ? $"[{obj.GetType().Name}.{callerName}()]" : "")} {message}");
        }

#if DEBUG
        [Conditional(LOGGING_DEBUG_DEFINE)]
#else
        [Conditional(LOGGING_RELEASE_DEFINE)]
#endif
        public static void LogWarning(object obj, string message, [CallerMemberName] string callerName = "")
        {
            Debug.LogWarning($"{(obj != null ? $"[{obj.GetType().Name}.{callerName}()]" : "")} {message}");
        }

#if DEBUG
        [Conditional(LOGGING_DEBUG_DEFINE)]
#else
        [Conditional(LOGGING_RELEASE_DEFINE)]
#endif
        public static void LogError(object obj, string message, [CallerMemberName] string callerName = "")
        {
            Debug.LogError($"{(obj != null ? $"[{obj.GetType().Name}.{callerName}()]" : "")} {message}");
        }
    }
}