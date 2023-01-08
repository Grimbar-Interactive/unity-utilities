using System;

namespace GI.UnityToolkit.Utilities
{
    public static class EnumUtilities
    {
        public static T Next<T>(this T initialValue) where T : Enum
        {
            return initialValue.Shift(1);
        }

        public static T Previous<T>(this T initialValue) where T : Enum
        {
            return initialValue.Shift(-1);
        }

        public static T Shift<T>(this T initialValue, int amount) where T : Enum
        {
            var enumValues = (T[]) Enum.GetValues(typeof(T));
            var index = (Array.IndexOf(enumValues, initialValue) + amount).Mod(enumValues.Length);
            return enumValues[index];
        }
    }
}
