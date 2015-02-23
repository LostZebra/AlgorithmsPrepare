using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    internal class SuffixTreeNode
    {
        private readonly Dictionary<char, SuffixTreeNode> _charToTreeNode = new Dictionary<char, SuffixTreeNode>();
        private readonly List<int> _indices = new List<int>();

        public void InsertStrAt(string str, int index)
        {
            _indices.Add(index);
            if (str.Length > 0)
            {
                char firstChar = str[0];
                SuffixTreeNode child;
                if (_charToTreeNode.ContainsKey(firstChar))
                {
                    child = _charToTreeNode[firstChar];
                }
                else
                {
                    child = new SuffixTreeNode();
                    _charToTreeNode.Add(firstChar, child);
                }
                child.InsertStrAt(str.Substring(1), index);
            }
        }

        public List<int> FindStr(string str)
        {
            if (str.Length == 0)
            {
                List<int> indicesCopy = new List<int>(_indices);
                return indicesCopy;
            }
            if (_charToTreeNode.ContainsKey(str[0]))
            {
                return _charToTreeNode[str[0]].FindStr(str.Substring(1));
            }

            return null;
        }
    }

    public class SuffixTree
    {
        private readonly SuffixTreeNode _root = new SuffixTreeNode();

        public SuffixTree(string str)
        {
            if (str == null)
            {
                throw new NullReferenceException("String value can't be null!");
            }
            if (str.Length != 0)
            {
                for (int i = 0; i < str.Length; ++i)
                {
                    _root.InsertStrAt(str.Substring(i), i);
                }
            }
        }

        public List<int> FindStr(string str)
        {
            return _root.FindStr(str);
        }
    }
}
