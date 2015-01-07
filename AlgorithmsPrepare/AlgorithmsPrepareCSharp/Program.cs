using System;

namespace AlgorithmsPrepareCSharp
{
    class Program
    {
        /*
         * LeetCode: Reverse Integer
         */
        public static int Reverse(int x)
        {
            if (x == 0) return x;
            bool isNegative = x < 0;
            long tempInt = (isNegative) ? -(long)x : x;
            string intToStr = string.Empty;
            while (tempInt != 0)
            {
                intToStr += (tempInt % 10).ToString();
                tempInt /= 10;
            }
            int i = 0;
            while (intToStr[i] == '0')
            {
                ++i;
            }
            long total = 0;
            while (i < intToStr.Length)
            {
                total += (long)(Math.Pow(10, intToStr.Length - i - 1) * char.GetNumericValue(intToStr[i]));
                ++i;
            }
            if (total - int.MaxValue >= 0 && !isNegative) return 0;
            if (total + (-int.MaxValue - 1) >= 0 && isNegative) return 0;
            return (int)((isNegative) ? -total : total);
        }

        /*
         * 编程之美：求区间内频率最小值
         */
        public static int MinimumFrequency(int[] ori, int l, int r)
        {
            int[] count = new int[ori.Length];
            int[] value = new int[ori.Length];
            int[] numInSection = new int[ori.Length];
            int[] left = new int[ori.Length];
            int[] right = new int[ori.Length];

            int numOfOccur = 1;
            int indexOfSection = 0;
            for (int i = 1; i < ori.Length; ++i)
            {
                if (ori[i] == ori[i - 1])
                {
                    ++numOfOccur;
                }
                else
                {
                    count[indexOfSection] = numOfOccur;
                    value[indexOfSection++] = ori[i - 1];
                    numOfOccur = 1;
                }
            }
            count[indexOfSection] = numOfOccur;
            value[indexOfSection] = ori[ori.Length - 1];

            numInSection[0] = 0;
            left[0] = 0;
            right[0] = count[0] - 1;
            for (int i = 1; i < ori.Length; ++i)
            {
                if (right[i - 1] >= i)
                {
                    numInSection[i] = numInSection[i - 1];
                    left[i] = left[i - 1];
                    right[i] = right[i - 1];
                }
                else
                {
                    numInSection[i] = numInSection[i - 1] + 1;
                    left[i] = right[i - 1] + 1;
                    right[i] = left[i] + count[numInSection[i]] - 1;
                }
            }

            if (numInSection[l] == numInSection[r])
            {
                return r - l + 1;
            }

            int leftFrequency = right[l] - l + 1;
            int rightFrequency = r - left[r] + 1;
            Rmq rmq = new Rmq(count);
            int midFrequency = rmq.GetSmallestInRange(numInSection[l] + 1, numInSection[r] - 1);

            return Math.Min(Math.Min(leftFrequency, rightFrequency), midFrequency);
        }

        /*
         * 程序验证入口
         */
        static void Main(string[] args)
        {
            Tc tc = Tc.GetInstance();
            var remained = tc.HowMuch(20, 204, 300);
        }
    }
}
