using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Utilities
{
    public static class ColorUtils
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            var temp = image.color;
            temp = new Color(temp.r, temp.g, temp.b, Mathf.Clamp01(alpha));
            image.color = temp;
        }
    }
}