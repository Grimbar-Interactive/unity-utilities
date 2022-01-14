using System;
using System.Linq;

namespace GI.UnityToolkit.Utilities
{
    public static class ArrayUtilities
    {
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var t in array) action(t);
        }

        public static void ForEachBackwards<T>(this T[] array, Action<T> action)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (action == null) throw new ArgumentNullException(nameof(action));
            for (var i = array.Length - 1; i >= 0; i--) action(array[i]);
        }

        public static void ForEach<T>(this T[,] matrix, Action<T[]> action)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (action == null) throw new ArgumentNullException(nameof(action));
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                action(matrix.GetRow(i));
            }
        }

        public static void ForEachIndividual<T>(this T[,] matrix, Action<T> action)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var t in matrix) action(t);
        }

        public static void ForEachIndexed<T>(this T[,] matrix, Action<T, int, int> action)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (action == null) throw new ArgumentNullException(nameof(action));
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    action(matrix[row, col], row, col);
                }
            }
        }

        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }
    }
}