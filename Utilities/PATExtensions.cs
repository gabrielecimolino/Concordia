using System;

namespace PAT.Utilities
{
    public static class PATExtensions
    {
        public static bool LessThan<T>(this IComparable<T> lhs, T rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool GreaterThan<T>(this IComparable<T> lhs, T rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
    }
}
