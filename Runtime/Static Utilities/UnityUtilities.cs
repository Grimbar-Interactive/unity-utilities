using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Utilities
{
    public static class UnityUtilities
    {
        public static void SetTo(this Transform transform, Transform other, bool ignorePosition = false,
            bool ignoreRotation = false, bool ignoreScale = false)
        {
            if (!ignorePosition) transform.position = other.position;
            if (!ignoreRotation) transform.rotation = other.rotation;
            if (!ignoreScale) transform.localScale = other.localScale;
        }
        
        public static void SetAlpha(this Image image, float alpha)
        {
            var temp = image.color;
            temp = new Color(temp.r, temp.g, temp.b, Mathf.Clamp01(alpha));
            image.color = temp;
        }
    }
}