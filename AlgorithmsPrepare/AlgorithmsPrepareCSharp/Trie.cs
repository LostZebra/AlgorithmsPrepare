namespace AlgorithmsPrepareCSharp
{
    class TrieNode
    {
        private readonly TrieNode[] _children = new TrieNode[26];
        bool _valueNode;

        public void Insert(char[] chars, int index)
        {
            int i = chars[index] - 'a';

            if (_children[i] == null)
            {
                _children[i] = new TrieNode();
            }

            if (index == chars.Length - 1)
            {
                _children[i]._valueNode = true;
                return;
            }

            _children[i].Insert(chars, index + 1);
        }

        public bool Search(char[] chars, int index)
        {
            if (index == chars.Length)
            {
                return _valueNode;
            }

            int i = chars[index] - 'a';

            return _children[i] != null && _children[i].Search(chars, index + 1);
        }

        public bool StartsWith(char[] chars, int index)
        {
            if (index == chars.Length)
            {
                return true;
            }

            int i = chars[index] - 'a';

            return _children[i] != null && _children[i].StartsWith(chars, index + 1);
        }
    }

    public class Trie
    {
        private readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode();
        }

        public void Insert(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return;
            }
            _root.Insert(word.ToCharArray(), 0);
        }

        public bool Search(string word)
        {
            return !string.IsNullOrEmpty(word) && _root.Search(word.ToCharArray(), 0);
        }

        public bool StartsWith(string word)
        {
            return !string.IsNullOrEmpty(word) && _root.StartsWith(word.ToCharArray(), 0);
        }
    }
}
