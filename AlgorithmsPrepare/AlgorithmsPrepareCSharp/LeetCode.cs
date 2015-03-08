using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    class LeetCode
    {
        public static readonly LeetCode Lc = new LeetCode();

        private LeetCode()
        {
            // Do nothing
        }

        public static LeetCode CreateInstance()
        {
            return Lc;
        }

        /// <summary>
        /// LeetCode: Two Sum
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Tuple<int, int> TwoSum(int[] numbers, int target)
        {
            if (numbers.Length == 0)
            {
                return null;
            }

            var dict = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Length; ++i)
            {
                if (dict.ContainsKey(target - numbers[i]))
                {
                    return new Tuple<int, int>(dict[target - numbers[i]], i + 1);
                }
                if (!dict.ContainsKey(numbers[i]))
                {
                    dict.Add(numbers[i], i + 1);
                }            
            }

            return null;
        }

        private int GetKthElement(int[] a, int startA, int endA, int[] b, int startB, int endB, int k)
        {
            if (endA - startA > endB - startB)
            {
                return GetKthElement(b, startB, endB, a, startA, endA, k);
            }
            if (endA < startA)
            {
                return b[startB + k - 1];
            }
            if (k == 1)
            {
                return Math.Min(a[startA], b[startB]);
            }

            // Now for the real case
            int indexA = Math.Min(endA - startA + 1, k / 2);
            int indexB = k - indexA;
            if (a[startA + indexA - 1] < b[startB + indexB - 1])
            {
                return GetKthElement(a, startA + indexA, endA, b, startB, endB, k - indexA);
            }
            if (a[startA + indexA] > b[startB + indexB])
            {
                return GetKthElement(a, startA, endA, b, startB + indexB, endB, k - indexB);
            }
            return a[startA + indexA - 1];
        }

        /// <summary>
        /// LeetCode: Median of Two Sorted Arrays
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double FindMedianSortedArrays(int[] a, int[] b)
        {
            int totalLen = a.Length + b.Length;
            if (totalLen % 2 == 0)
            {
                return 0.5 * (GetKthElement(a, 0, a.Length - 1, b, 0, b.Length - 1, totalLen / 2) + GetKthElement(a, 0, a.Length - 1, b, 0, b.Length - 1, totalLen / 2 + 1));
            }
            return GetKthElement(a, 0, a.Length - 1, b, 0, b.Length - 1, totalLen / 2 + 1);
        }

        /// <summary>
        /// LeetCode: Reverse Bits
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public uint ReverseBit(uint n)
        {
            int i = 0;
            int j = 31;
            while (i < j)
            {
                bool posI = (n & (1 << i)) != 0;
                bool posJ = (n & (1 << j)) != 0;
                if (posI)
                {
                    n |= (uint)(1 << j);
                }
                else
                {
                    n &= ~(uint)(1 << j);
                }
                if (posJ)
                {
                    n |= (uint)(1 << i);
                }
                else
                {
                    n &= ~(uint)(1 << i);
                }
                ++i;
                --j;
            }
            return n;
        }
    }
}
