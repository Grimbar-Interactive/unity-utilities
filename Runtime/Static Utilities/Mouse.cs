using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Mouse
{
    /// <summary>
    /// Determines if the mouse intersects any UI elements (objects on the "UI" layer).
    /// </summary>
    /// <returns>True if the mouse is atop a UI element, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsOverUI() => IsOverLayer("UI");
    
    /// <summary>
    /// Determines if the mouse is over any objects on the layer with the specified name.
    /// </summary>
    /// <param name="layerName">The name of the layer to check.</param>
    /// <returns>True if the mouse is over an object in that layer, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsOverLayer(string layerName)
    {
        return IsOverLayer(LayerMask.NameToLayer(layerName));
    }

    /// <summary>
    /// Determines if the mouse is over any objects in the specified layer.
    /// </summary>
    /// <param name="layerID">The layer to check.</param>
    /// <returns>True if the mouse is over an object in that layer, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsOverLayer(int layerID)
    {
        if (EventSystem.current == null || layerID < 0) return false;

        var eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var castResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, castResults);

        return castResults.Any(r => r.gameObject.layer == layerID);
    }
}
