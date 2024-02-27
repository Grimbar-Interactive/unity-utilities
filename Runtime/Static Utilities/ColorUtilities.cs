using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class ColorUtilities
    {
        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha);
            return color;
        }
    }
}