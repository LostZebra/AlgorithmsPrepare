namespace AlgorithmsPrepareCSharp
{
    class UnionSet
    {
        private const int NodeSize = 100001;

        private readonly int[] _parent = new int[NodeSize];
        private readonly int[] _size = new int[NodeSize];

        public UnionSet()
        {
            for (int i = 1; i != NodeSize + 1; ++i)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        public int FindParent(int x)
        {
            if (_parent[x] == x)
            {
                return x;
            }
            return FindParent(_parent[x]);
        }

        public void MakeUnion(int s, int t)
        {
            int parentOfS = FindParent(s);
            _parent[t] = parentOfS;
            _size[parentOfS] += _size[t];
        }
    }
}
