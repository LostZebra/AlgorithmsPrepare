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

        public static void ReverseArray<TItem>(this TItem[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }

            int i, j;
            for (i = 0, j = array.Length - 1; i < j; ++i, --j)
            {
                TItem temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        public static void Swap<TItem>(this TItem[] array, int first, int second)
        {
            var temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }

        public static void Swap<TItem>(this IList<TItem> array, int first, int second)
        {
            var temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
    }
}
