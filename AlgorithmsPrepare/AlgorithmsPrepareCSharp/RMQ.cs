using System;

namespace AlgorithmsPrepareCSharp
{
    class Rmq
    {
        private readonly int[,] _sectionMin;

        public Rmq(int[] ori)
        {
            _sectionMin = new int[ori.Length, ori.Length];
            for (int i = 0; i < ori.Length; ++i)
            {
                _sectionMin[i, 0] = ori[i];
            }
            for (int i = 1; (1 << i) <= ori.Length; ++i)
            {
                for (int j = 0; j + (1 << i) - 1 < ori.Length; ++j)
                {
                    _sectionMin[j, i] = Math.Min(_sectionMin[j, i - 1], _sectionMin[j + (1 << i - 1), i - 1]);
                }
            }
        }

        public int GetSmallestInRange(int left, int right)
        {
            int k = 0;
            while (1 << (k + 1) <= right - left + 1)
            {
                ++k;
            }
            return Math.Min(_sectionMin[left, k], _sectionMin[right - (1 << k) + 1, k]);
        }
    }
}
