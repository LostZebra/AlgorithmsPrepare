using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    class Kmp
    {
        private readonly int[] _jumpIndex;
        private readonly string _pattern;

        public Kmp(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new NullReferenceException("Pattern null or empty!");
            }

            this._pattern = pattern;

            _jumpIndex = new int[pattern.Length];
            for (int i = 1; i < pattern.Length; ++i)
            {
                int j = _jumpIndex[i - 1];
                while (j != 0 && pattern[i] != pattern[j])
                {
                    j = _jumpIndex[j - 1];
                }
                j += pattern[j] == pattern[i] ? 1 : 0;
                _jumpIndex[i] = j;
            }
        }

        public IEnumerable<int> FindPattern(string template)
        {
            int j = 0;
            for (int i = 0; i < template.Length; ++i)
            {
                while (j > 0 && template[i] != _pattern[j])
                {
                    j = _jumpIndex[j - 1];
                }

                if (_pattern[j] == template[i])
                {
                    j++;
                }

                if (j == _pattern.Length)
                {
                    yield return i - _pattern.Length + 1;
                    j = _jumpIndex[j - 1];
                }
            }
        }
    }
}
