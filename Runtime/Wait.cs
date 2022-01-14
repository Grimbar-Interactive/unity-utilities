using System;
using System.Collections.Generic;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class Wait
    {
        private const float TOLERANCE = 0.0001f;
        
        private class FloatComparer : IEqualityComparer<float>
        {
            bool IEqualityComparer<float>.Equals(float x, float y)
            {
                return Math.Abs(x - y) < TOLERANCE;
            }

            int IEqualityComparer<float>.GetHashCode(float obj)
            {
                return obj.GetHashCode();
            }
        }

        private static readonly Dictionary<float, WaitForSeconds> WaitTimes =
            new Dictionary<float, WaitForSeconds>(100, new FloatComparer());

        public static WaitForSeconds Time(float seconds)
        {
            if (!WaitTimes.TryGetValue(seconds, out var wfs))
            {
                WaitTimes.Add(seconds, wfs = new WaitForSeconds(seconds));
            }
            return wfs;
        }
    }
}