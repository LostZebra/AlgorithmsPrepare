using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    internal static class ExtensionMethods
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrEmpty<TItem>(this TItem[] ori)
        {
            return ori == null || ori.Length == 0;
        }

        public static bool IsNullOrEmpty<TItem>(this IList<TItem> ori)
        {
            return ori == null || ori.Count == 0;
        }

        public static bool IsEmpty<TItem>(this Stack<TItem> stack)
        {
            return stack.Count == 0;
        }
    }
}
