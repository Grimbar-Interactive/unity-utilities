using System;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class MathUtilities
    {
        public static bool IsZero(this float value)
        {
            return Math.Abs(value) < 0.0001f;
        }

        public static float Map(this float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
        {
            return (value - inputFrom) / (inputTo - inputFrom) * (outputTo - outputFrom) + outputFrom;
        }

        public static int Mod(this int value, int cap)
        {
            var remainder = value % cap;
            return remainder < 0 ? remainder + cap : remainder;
        }

        public static Vector3 GetUp(this Vector3 direction)
        {
            direction.Normalize();
            if (direction == Vector3.zero) return Vector3.up;
            if (direction == Vector3.up) return Vector3.forward;

            var distance = -Vector3.Dot(direction, Vector3.up);
            return (Vector3.up + direction * distance).normalized;
        }

        public static Vector3 GetNearestPointOnLine(Vector3 origin, Vector3 direction, Vector3 point)
        {
            direction.Normalize();
            var originToPoint = point - origin;
            var dotProduct = Vector3.Dot(originToPoint, direction);
            return origin + direction * dotProduct;
        }

        public static Vector2 ToVector2(this Vector3 vector)
        {
            return vector;
        }
        
        public static Vector3 Forward(this Quaternion rotation)
        {
            return rotation * Vector3.forward;
        }
        
        /// <summary>
        /// Returns the shortest signed angle from the first rotation to the second.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float SignedAngleAboutY(Quaternion from, Quaternion to)
        {
            var forwardA = from * Vector3.forward;
            var forwardB = to * Vector3.forward;
            var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;
            return Mathf.DeltaAngle(angleA, angleB);
        }
    }
}