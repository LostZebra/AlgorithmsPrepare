using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    internal class UtilityAlgorithm
    {
        public static void Swap<T>(IList<T> array, int firstIndex, int secondIndex) where T: struct
        {
            T temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        public static void QuickSort<T>(IList<T> array, int startIndex, int endIndex) where T : struct, IComparable<T>
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }

            if (startIndex >= endIndex)
            {
                return;
            }

            int ranPos = new Random().Next(startIndex, endIndex + 1);
            array.Swap(ranPos, endIndex);
            int firstHigh = startIndex;
            int i = firstHigh;
            T pivot = array[endIndex];
            for (; i <= endIndex; ++i)
            {
                if (array[i].CompareTo(pivot) >= 0) continue;
                Swap(array, i, firstHigh);
                ++firstHigh;
            }
            array.Swap(firstHigh, endIndex);

            QuickSort(array, startIndex, firstHigh - 1);
            QuickSort(array, firstHigh + 1, endIndex);
        }
    }
}
