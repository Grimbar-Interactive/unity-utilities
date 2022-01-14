using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class UnityUtilities
    {
        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha);
            return color;
        }

        public static void SetTo(this Transform transform, Transform other, bool ignorePosition = false,
            bool ignoreRotation = false, bool ignoreScale = false)
        {
            if (!ignorePosition) transform.position = other.position;
            if (!ignoreRotation) transform.rotation = other.rotation;
            if (!ignoreScale) transform.localScale = other.localScale;
        }
    }
}