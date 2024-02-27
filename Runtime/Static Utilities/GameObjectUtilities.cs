using UnityEngine;

namespace GI.UnityToolkit.Utilities 
{
    public static class GameObjectUtilities
    {
        public static void SetLayerRecursively(this GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
    }
}