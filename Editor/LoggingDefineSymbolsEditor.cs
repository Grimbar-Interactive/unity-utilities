using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GI.UnityToolkit.Utilities.Editor
{
    /// <summary>
    /// Adds the given define symbols to PlayerSettings define symbols.
    /// Just add your own define symbols to the Symbols property at the below.
    /// </summary>
    [InitializeOnLoad]
    public class LoggingDefineSymbolsEditor : UnityEditor.Editor
    {
        static LoggingDefineSymbolsEditor()
        {
            ToggleScriptingSymbol(LogUtilities.LOGGING_DEBUG_DEFINE, LoggingDebugEnabled);
            ToggleScriptingSymbol(LogUtilities.LOGGING_RELEASE_DEFINE, LoggingReleaseEnabled);
        }
        
        #region Debug Logging Toggle
        private const string LoggingDebugMenuName = "Grimbar Interactive/Logging/Log in Development Builds and Editor";

        private static bool LoggingDebugEnabled
        {
            get => EditorPrefs.GetBool(LogUtilities.LOGGING_DEBUG_DEFINE, true);
            set => EditorPrefs.SetBool(LogUtilities.LOGGING_DEBUG_DEFINE, value);
        }
        
        [MenuItem(LoggingDebugMenuName)]
        private static void ToggleLoggingDebug()
        {
            LoggingDebugEnabled = !LoggingDebugEnabled;
            ToggleScriptingSymbol(LogUtilities.LOGGING_DEBUG_DEFINE, LoggingDebugEnabled);
        }

        [MenuItem(LoggingDebugMenuName, true)]
        private static bool ToggleLoggingDebugValidate()
        {
            Menu.SetChecked(LoggingDebugMenuName, LoggingDebugEnabled);
            return !Application.isPlaying;
        }
        #endregion
        
        #region Release Logging Toggle
        private const string LoggingReleaseMenuName = "Grimbar Interactive/Logging/Log in Release Builds";

        private static bool LoggingReleaseEnabled
        {
            get => EditorPrefs.GetBool(LogUtilities.LOGGING_RELEASE_DEFINE, false);
            set => EditorPrefs.SetBool(LogUtilities.LOGGING_RELEASE_DEFINE, value);
        }
        
        [MenuItem(LoggingReleaseMenuName)]
        private static void ToggleLoggingRelease()
        {
            LoggingReleaseEnabled = !LoggingReleaseEnabled;
            ToggleScriptingSymbol(LogUtilities.LOGGING_RELEASE_DEFINE, LoggingReleaseEnabled);
        }

        [MenuItem(LoggingReleaseMenuName, true)]
        private static bool ToggleLoggingReleaseValidate()
        {
            Menu.SetChecked(LoggingReleaseMenuName, LoggingReleaseEnabled);
            return !Application.isPlaying;
        }
        #endregion
        
        private static void ToggleScriptingSymbol(string define, bool active)
        {
            var definesString =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            var allDefines = definesString.Split(';').ToList();
            if (active)
            {
                allDefines.AddRange(new[]{define}.Except(allDefines));
            }
            else
            {
                allDefines.Remove(define);
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }
    }
}