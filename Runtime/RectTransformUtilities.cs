using System;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    /// <summary>
    /// Some useful utility methods for Unity's RectTransform.
    /// Modified from this Unity forum post: https://forum.unity.com/threads/test-if-ui-element-is-visible-on-screen.276549/
    /// </summary>
    public static class RectTransformUtilities
    {
        /// <summary>
        /// Determines if this RectTransform is at least partially visible on screen.
        /// If the RectTransform is on a "Screen Space - Overlay" canvas don't include the <c>camera</c> parameter.
        /// Otherwise, a <c>camera</c> must be included.
        /// </summary>
        /// <returns><c>true</c> if is at least partially visible on screen; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">The RectTransform to check.</param>
        /// <param name="camera">Camera (required for Camera and World space canvases).</param>
        public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera = null)
        {
            if (rectTransform.gameObject.activeInHierarchy == false) return false;
            if (rectTransform.CountCornersVisibleFrom(camera) > 0) return true;
            var (surroundsHorizontally, surroundsVertically) = rectTransform.SurroundsScreen();
            return surroundsHorizontally || surroundsVertically;
        }
        
        /// <summary>
        /// Determines if this RectTransform is fully visible on screen.
        /// If the RectTransform is on a "Screen Space - Overlay" canvas don't include the <c>camera</c> parameter.
        /// Otherwise, a <c>camera</c> must be included.
        /// </summary>
        /// <returns><c>true</c> if is fully visible on screen; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">The RectTransform to check.</param>
        /// <param name="camera">Camera (required for Camera and World space canvases).</param>
        public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera = null)
        {
            if (rectTransform.gameObject.activeInHierarchy == false) return false;
            return rectTransform.CountCornersVisibleFrom(camera) == 4;
        }

        /// <summary>
        /// Counts the bounding box corners of the given RectTransform that are visible on screen.
        /// If the RectTransform is on a "Screen Space - Overlay" canvas don't include the <c>camera</c> parameter.
        /// Otherwise, a <c>camera</c> must be included.
        /// </summary>
        /// <returns>The number of bounding box corners that are visible on screen.</returns>
        /// <param name="rectTransform">The RectTransform to check.</param>
        /// <param name="camera">Camera (required for Camera and World space canvases).</param>
        public static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera = null)
        {
            // Screen space bounds (assumes camera renders across the entire screen)
            var screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
            
            var objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            var count = 0;
            foreach (var corner in objectCorners)
            {
                // Convert corner to screen space when using a camera.
                // Otherwise we assume the corner is already in screen space.
                var correctedCorner = (camera ? camera.WorldToScreenPoint(corner) : corner).Round();
                if (screenBounds.ContainsIncludeEdges(correctedCorner)) count++;
            }
            return count;
        }
        
        /// <summary>
        /// Determines if this RectTransform surrounds the screen or camera view on one or both axes.
        /// "Surrounds" means that the RectTransform's minimum and maximum values meet or exceed
        /// the Rect's minimum and maximum values on a given axis AND the other axis' span surrounds or partially
        /// covers the Rect's span in that axis.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to check.</param>
        /// <param name="camera">Camera (required for Camera and World space canvases).</param>
        /// <returns><c>true</c> if the left-most and right-most parts of the RectTransform are beyond the screen's X-axis limits.</returns>
        public static (bool surroundsHorizontally, bool surroundsVertically) SurroundsScreen(this RectTransform rectTransform, Camera camera = null)
        {
            // Screen space bounds (assumes camera renders across the entire screen)
            var screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
            return rectTransform.SurroundsRect(screenBounds, camera);
        }

        /// <summary>
        /// "Surrounds" means that the RectTransform's span in a given axis encompasses the Rect's span in that same axis
        /// AND the other axis' span surrounds or partially covers the Rect's span in that axis.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to check.</param>
        /// <param name="rect">The Rect to check.</param>
        /// <param name="camera">Required for RectTransforms on Camera or World space canvases.</param>
        /// <returns><c>true</c> if the left-most and right-most parts of the RectTransform are beyond the screen's X-axis limits.</returns>
        public static (bool surroundsHorizontally, bool surroundsVertically) SurroundsRect(this RectTransform rectTransform, Rect rect, Camera camera = null)
        {
            var objectCorners = new Vector3[4];
            var (minX, maxX, minY, maxY) = rectTransform.GetCornerMinAndMaxes(camera);

            var couldSurroundHorizontally = minX < rect.xMin && maxX > rect.xMax;
            var couldSurroundVertically = minY < rect.yMin && maxY > rect.yMax;

            if (couldSurroundHorizontally && couldSurroundVertically) return (true, true);

            if (couldSurroundHorizontally)
            {
                // Check the Y coordinates put the RectTransform as least partially overlapping the Rect.
                var xMiddle = rect.xMin + (rect.xMax - rect.xMin) / 2f;
                var hasOverlap = rect.ContainsIncludeEdges(new Vector3(xMiddle, minY)) ||
                                 rect.ContainsIncludeEdges(new Vector3(xMiddle, maxY));
                return (hasOverlap, false);
            }

            if (couldSurroundVertically)
            {
                // Check the X coordinates put the RectTransform as least partially overlapping the Rect.
                var yMiddle = rect.yMin + (rect.yMax - rect.yMin) / 2f;
                var hasOverlap = rect.ContainsIncludeEdges(new Vector3(minX, yMiddle)) ||
                                 rect.ContainsIncludeEdges(new Vector3(maxX, yMiddle));
                return (false, hasOverlap);
            }
            
            return (false, false);
        }

        // Needs reworked
        // /// <summary>
        // /// Gets the offset of the RectTransform outside the screen (returns Vector3.zero if fully inside).
        // /// </summary>
        // /// <param name="rectTransform">The RectTransform to check.</param>
        // /// <param name="camera">Required for RectTransforms on Camera or World space canvases.</param>
        // /// <returns>The offset outside the screen, or Vector3.zero if fully within the screen.</returns>
        // public static Vector3 GetOffsetFromScreen(this RectTransform rectTransform, Camera camera = null)
        // {
        //     // Screen space bounds (assumes camera renders across the entire screen)
        //     var screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
        //     return rectTransform.GetOffsetFromRect(screenBounds, camera);
        // }
        //
        // /// <summary>
        // /// Gets the offset of the RectTransform outside the screen (returns Vector3.zero if fully inside).
        // /// </summary>
        // /// <param name="rectTransform">The RectTransform to check.</param>
        // /// <param name="camera">Required for RectTransforms on Camera or World space canvases.</param>
        // /// <param name="rect">The Rect to check.</param>
        // /// <returns>The offset outside the screen, or Vector3.zero if fully within the screen.</returns>
        // public static Vector3 GetOffsetFromRect(this RectTransform rectTransform, Rect rect, Camera camera = null)
        // {
        //     var (minX, maxX, minY, maxY) = rectTransform.GetCornerMinAndMaxes(camera);
        //
        //     var leftOffset = minX - rect.xMin;
        //     var rightOffset = maxX - rect.xMax;
        //     var bottomOffset = minY - rect.yMin;
        //     var topOffset = maxY - rect.yMax;
        //
        //     float xOffset;
        //     if (Mathf.Abs(leftOffset) <= Mathf.Abs(rightOffset))
        //     {
        //         xOffset = -leftOffset;
        //     }
        //     else
        //     {
        //         xOffset = -rightOffset;
        //     }
        //
        //     float yOffset;
        //     if (Mathf.Abs(bottomOffset) <= Mathf.Abs(topOffset))
        //     {
        //         yOffset = -bottomOffset;
        //     }
        //     else
        //     {
        //         yOffset = -topOffset;
        //     }
        //     
        //     return new Vector3(xOffset, yOffset, 0f);
        // }
        
        /// <summary>
        /// Helper method to reduce floating point issues.
        /// </summary>
        /// <returns>The Vector3 rounded to 4 decimal places.</returns>
        private static Vector3 Round(this Vector3 vector)
        {
            return new Vector3((float)Math.Round(vector.x, 4), (float)Math.Round(vector.y, 4), vector.z);
        }

        /// <summary>
        /// Determines the minimum and maximum X and Y values for the RectTransform's corners.
        /// </summary>
        private static (float minX, float maxX, float minY, float maxY) GetCornerMinAndMaxes(this RectTransform rectTransform, Camera camera = null)
        {
            var objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            foreach (var corner in objectCorners)
            {
                // Convert corner to screen space when using a camera.
                // Otherwise we assume the corner is already in screen space.
                var correctedCorner = (camera ? camera.WorldToScreenPoint(corner) : corner).Round();

                if (correctedCorner.x < minX)
                    minX = correctedCorner.x;
                else if (correctedCorner.x > maxX) 
                    maxX = correctedCorner.x;
                
                if (correctedCorner.y < minY)
                    minY = correctedCorner.y;
                else if (correctedCorner.y > maxY)
                    maxY = correctedCorner.y;
            }

            return (minX, maxX, minY, maxY);
        }

        /// <summary>
        /// Similar to Rect.Contains() but includes points that lie approximately on the edges as well.
        /// </summary>
        /// <returns><c>true</c> if the point is within or on the edge of the rect, otherwise <c>false</c>.</returns>
        private static bool ContainsIncludeEdges(this Rect rect, Vector3 point)
        {
            var inBoundsX = (point.x > rect.xMin && point.x < rect.xMax) ||
                            Mathf.Approximately(rect.xMin, point.x) ||
                            Mathf.Approximately(rect.xMax, point.x);
            var inBoundsY = (point.y > rect.yMin && point.y < rect.yMax) ||
                            Mathf.Approximately(rect.yMin, point.y) ||
                            Mathf.Approximately(rect.yMax, point.y);
            return inBoundsX && inBoundsY;
        }
    }
}