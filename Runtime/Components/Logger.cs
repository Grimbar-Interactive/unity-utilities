using UnityEngine;

#if GI_UTILITIES
using GI.UnityToolkit.Utilities;
#endif

namespace GI.UnityToolkit.Components.Utilities
{
    /// <summary>
    /// Allows debug statements to be logged from UnityEvents in the inspector.
    /// </summary>
    public class Logger : MonoBehaviour
    {
        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message"></param>
        public void LogMessage(string message) => this.Log(message);
        
        /// <summary>
        /// Logs a warning to the console.
        /// </summary>
        /// <param name="warning"></param>
        public void LogWarningMessage(string warning) => this.LogWarning(warning);
        
        /// <summary>
        /// Logs an error to the console.
        /// </summary>
        /// <param name="error"></param>
        public void LogErrorMessage(string error) => this.LogError(error);
    }
}
