using System;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public class GizmoDrawer : MonoBehaviour
    {
        private enum GizmoType
        {
            Sphere,
            Cube,
            WireSphere,
            WireCube
        }

        private enum GizmoColor
        {
            Red,
            Blue
        }

        [SerializeField] private GizmoType type = default;
        [SerializeField] private GizmoColor color = default;
        [SerializeField, Range(0.05f, 1f)] private float size = 1f;

        private void OnDrawGizmos()
        {
            Gizmos.color = color switch
            {
                GizmoColor.Red => Color.red,
                GizmoColor.Blue => Color.blue,
                _ => Gizmos.color
            };

            switch (type)
            {
                case GizmoType.Sphere:
                    Gizmos.DrawSphere(transform.position, size);
                    break;
                case GizmoType.Cube:
                    Gizmos.DrawCube(transform.position, Vector3.one * size);
                    break;
                case GizmoType.WireSphere:
                    Gizmos.DrawWireSphere(transform.position, size);
                    break;
                case GizmoType.WireCube:
                    Gizmos.DrawWireCube(transform.position, Vector3.one * size);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}