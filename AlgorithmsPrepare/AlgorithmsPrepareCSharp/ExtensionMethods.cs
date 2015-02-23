using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    public static class ExtensionMethods
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

        public static int FirstOccurrenceOf(this string oriStr, string searchStr)
        {
            if (oriStr.IsNullOrEmpty() || searchStr.IsNullOrEmpty())
            {
                return -1;
            }

            for (int i = 0; i <= oriStr.Length - searchStr.Length; ++i)
            {
                for (int j = i; j < i + searchStr.Length; ++j)
                {
                    if (oriStr[j] == searchStr[j - i])
                    {
                        if (j == i + searchStr.Length - 1) return i;
                        continue;
                    }
                    break;
                }
            }

            return -1;
        }

        public static string Xor(this string ori, string k)
        {
            char[] oriCharArray = ori.ToCharArray();
            char[] kCharArray = k.ToCharArray();

            int i = 0;
            for (; i != k.Length; ++i)
            {
                oriCharArray[i] = oriCharArray[i] != kCharArray[i] ? '1' : '0';
            }

            return new string(oriCharArray);
        }

        public static TItem SelectionRank<TItem>(this TItem[] array, int rank, int start, int end) where TItem : struct, IComparable<TItem>
        {
            if (start >= end)
            {
                return default(TItem);
            }

            Random rand = new Random();
            int randPos = rand.Next(0, 1) * (end - start + 1) + start;

            Swap(array, randPos, end);

            TItem pivot = array[end];
            int firstHigh = start;
            for (int i = start; i < end; ++i)
            {
                if (array[i].CompareTo(pivot) <= 0)
                {
                    Swap(array, i, firstHigh);
                    firstHigh++;
                }
            }
            Swap(array, firstHigh, end);
            if (firstHigh - start == rank - 1)
            {
                return array[firstHigh];
            }
            if (firstHigh - start < rank - 1)
            {
                return SelectionRank(array, rank - (firstHigh - start + 1), firstHigh + 1, end);
            }
            return SelectionRank(array, rank, start, firstHigh - 1);
        }
    }
}
