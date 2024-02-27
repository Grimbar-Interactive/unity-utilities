using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace GI.UnityToolkit.Utilities.Components
{
    /// <summary>
    /// Allows notes to be added to GameObjects for development/documentation purposes.
    /// </summary>
    public class Note : MonoBehaviour
    {
#if UNITY_EDITOR
        // Disable warnings about unused vars - this is for Editor viewing only
#pragma warning disable CS0414
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField] [TextArea(1, 10)] private string note = "";
#pragma warning restore CS0414
#endif
    }
}