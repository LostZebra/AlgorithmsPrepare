using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    public class UtilityAlgorithm
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
            Swap(array, firstHigh, endIndex);

            QuickSort(array, startIndex, firstHigh - 1);
            QuickSort(array, firstHigh + 1, endIndex);
        }

        public static T[] RemoveDuplicates<T>(T[] array) where T: IComparable<T>
        {
            if (array.Length <= 1)
            {
                return array;
            }

            int i = 1;
            int offset = 0;
            while (i < array.Length)
            {
                if (array[i].Equals(array[i - 1]))
                {
                    offset++;
                }
                else
                {
                    array[i - offset] = array[i];
                }
                i++;
            }

            T[] copyArray = new T[array.Length - offset];
            Array.Copy(array, copyArray, array.Length - offset);
            return copyArray;
        }

        public static T[] RemoveDuplicatesUnsorted<T>(T[] array)
        {
            var set = new HashSet<T>();
            int offset = 0;

            for (int i = 0; i < array.Length; ++i)
            {
                if (set.Contains(array[i]))
                {
                    offset++;
                }
                else
                {
                    set.Add(array[i]);
                    array[i - offset] = array[i];
                }
            }

            T[] copyArray = new T[array.Length - offset];
            Array.Copy(array, copyArray, array.Length - offset);
            return copyArray;
        }
    }
}
