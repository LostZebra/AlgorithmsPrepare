using System;

namespace AlgorithmsPrepareCSharp
{
    class Kmp
    {
        private readonly int[] _jumpIndex;

        public Kmp(String pattern)
        {
            if (String.IsNullOrEmpty(pattern))
            {
                throw new NullReferenceException("Pattern null or empty!");
            }

            _jumpIndex = new int[pattern.Length + 1];
            _jumpIndex[0] = _jumpIndex[1] = 0;
            for (int i = 1; i < pattern.Length; ++i)
            {
                int j = _jumpIndex[i];
                while (j != 0 && pattern[i] != pattern[j])
                {
                    j = _jumpIndex[j];
                }
                _jumpIndex[i + 1] = pattern[i] == pattern[j] ? j + 1 : 0;
            }
        }

        public void FindPattern(String template, String pattern)
        {
            int j = 0;
            for (int i = 0; i < template.Length; ++i)
            {
                while (j != 0 && template[i] != pattern[j])
                {
                    j = _jumpIndex[j];
                }

                if (pattern[j] == template[i])
                {
                    ++j;
                }

                if (j == pattern.Length)
                {
                    Console.WriteLine("Match found: {0}", i - pattern.Length + 1);
                    j = 0;
                }
            }
        }
    }
}
