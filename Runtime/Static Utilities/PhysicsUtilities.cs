using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class PhysicsUtilities
    {
        public static void AccelerateTo(this Rigidbody body, Vector3 targetVelocity, float maxAcceleration)
        {
            var deltaVelocity = targetVelocity - body.velocity;
            var acceleration = deltaVelocity / Time.deltaTime;

            if (acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
            {
                acceleration = acceleration.normalized * maxAcceleration;
            }

            body.AddForce(acceleration, ForceMode.Acceleration);
        }

        public static float GetAngleTo(this Vector3 from, Vector3 to, Vector3 up)
        {
            var referenceRight = Vector3.Cross(up, from);
            var angle = Vector3.Angle(to, from);
            var sign = (Vector3.Dot(to, referenceRight) > 0.0f) ? 1.0f : -1.0f;
            var finalAngle = sign * angle;
            return finalAngle;
        }

        public static Vector3 DiscardXZ(this Vector3 vector)
        {
            vector.x = 0;
            vector.z = 0;
            return vector;
        }

        public static Vector3 DiscardY(this Vector3 vector)
        {
            vector.y = 0;
            return vector;
        }

        public static Vector3 Clamp01(this Vector3 vector)
        {
            return new Vector3(Mathf.Clamp01(vector.x), Mathf.Clamp01(vector.y), Mathf.Clamp01(vector.z));
        }

        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}