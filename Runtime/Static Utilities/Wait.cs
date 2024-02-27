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

        private static readonly Dictionary<float, WaitForSecondsRealtime> WaitTimesRealtime =
            new Dictionary<float, WaitForSecondsRealtime>(100, new FloatComparer());

        /// <summary>
        /// Waits an amount of time.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static WaitForSeconds Time(float seconds)
        {
            if (!WaitTimes.TryGetValue(seconds, out var wfs))
            {
                WaitTimes.Add(seconds, wfs = new WaitForSeconds(seconds));
            }
            return wfs;
        }

        /// <summary>
        /// Waits an unscaled amount of time.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static WaitForSecondsRealtime RealTime(float seconds)
        {
            if (!WaitTimesRealtime.TryGetValue(seconds, out var wfs))
            {
                WaitTimesRealtime.Add(seconds, wfs = new WaitForSecondsRealtime(seconds));
            }
            return wfs;
        }
    }
}