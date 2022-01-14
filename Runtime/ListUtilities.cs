using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace GI.UnityToolkit.Utilities
{
    public static class ListUtilities
    {
        public static void ForEachBackwards<T>(this List<T> array, Action<T> action)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (action == null) throw new ArgumentNullException(nameof(action));
            for (var i = array.Count - 1; i >= 0; i--) action(array[i]);
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default;
            return list[Random.Range(0, list.Count)];
        }
    }
}