using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class RandomUtilities
    {
        public static float RandomSign()
        {
            return Mathf.Sign(Random.value - 0.5f);
        }

        public static void RandomIntegersFromRange(int min, int max, ref int[] output, bool unique = false)
        {
            if (output.IsNullOrEmpty()) return;

            if (!unique || output.Length > max - min)
            {
                for (var i = 0; i < output.Length; i++)
                {
                    output[i] = Random.Range(min, max);
                }
            }
            else
            {
                var randomList = new List<int>();
                for (var i = 0; i < max - min; i++)
                {
                    randomList.Add(i);
                }

                for (var outputIndex = 0; outputIndex < output.Length; outputIndex++)
                {
                    var randomIndex = Random.Range(0, randomList.Count);
                    output[outputIndex] = randomList[randomIndex];
                    randomList.RemoveAt(randomIndex);
                }
            }
        }

        public static List<T> Randomize<T>(this IEnumerable<T> list)
        {
            var copy = list.ToList();
            for (var i = copy.Count - 1; i >= 0; i--)
            {
                var k = Random.Range(0, i + 1);
                var value = copy[k];
                copy[k] = copy[i];
                copy[i] = value;
            }

            return copy;
        }
    }
}