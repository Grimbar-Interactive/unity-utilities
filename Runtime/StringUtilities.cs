using UnityEngine;

namespace GI.UnityToolkit.Utilities
{
    public static class StringUtilities
    {
        private const string ALPHA_NUMERIC = "abcdefghijklmnopqrstuvwxyz1234567890";

        public static string GetRandomAlphaNumeric(int length)
        {
            var str = "";
            for (var i = 0; i < length; i++)
            {
                str += ALPHA_NUMERIC[Random.Range(0, ALPHA_NUMERIC.Length)];
            }

            return str;
        }
    }
}