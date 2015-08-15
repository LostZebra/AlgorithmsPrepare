using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    public class Tree<TItem> where TItem : struct, IComparable<TItem>
    {
        private readonly TItem _data;
        public TItem Data {
            get { return this._data; }
        }

        public Tree<TItem> Left { get; set; }

        public Tree<TItem> Right { get; set; }

        public Tree()
        {
            this._data = default(TItem);
            this.Left = null;
            this.Right = null;
        }

        public Tree(TItem data, Tree<TItem> left = null, Tree<TItem> right = null)
        {
            this._data = data;
            this.Left = left;
            this.Right = right;
        }

        public Tree<TItem> InsertWithRecursion(Tree<TItem> root, TItem nodeData)
        {
            /*
            if (root == null)
            {
                return new Tree<TItem>(nodeData);
            }

            return nodeData.CompareTo(root.Data) <= 0 ? 
                new Tree<TItem>(root.Data, InsertWithRecursion(root.Left, nodeData), root.Right) 
                : new Tree<TItem>(root.Data, root.Left, InsertWithRecursion(root.Right, nodeData));
            */
            Tree<TItem> parNode = null;
            Tree<TItem> curNode = root;

            while (curNode != null)
            {
                parNode = curNode;
                curNode = nodeData.CompareTo(curNode.Data) <= 0 ? curNode.Left : curNode.Right;
            }
            if (parNode == null)
            {
                root = new Tree<TItem>(nodeData);
                return root;
            }

            if (nodeData.CompareTo(parNode.Data) <= 0)
            {
                parNode.Left = new Tree<TItem>(nodeData);
            }
            else
            {
                parNode.Right = new Tree<TItem>(nodeData);
            }
            return root;
        }

        public static IEnumerable<TItem> InorderTraverseWithRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                yield break;
            }

            foreach (TItem item in InorderTraverseWithRecursion(root.Left))
            {
                yield return item;
            }
            yield return root.Data;
            foreach (TItem item in InorderTraverseWithRecursion(root.Right))
            {
                yield return item;
            }
        }

        public static IEnumerable<TItem> InorderTraverseWithoutRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                yield break;
            }

            Stack<Tree<TItem>> nodeStack = new Stack<Tree<TItem>>();
            nodeStack.Push(root);
            while (!nodeStack.IsEmpty())
            {
                if (root.Left != null)
                {
                    root = root.Left;
                    nodeStack.Push(root);
                }
                else
                {
                    Tree<TItem> topNode = nodeStack.Pop();
                    yield return topNode.Data;
                    if (topNode.Right != null)
                    {
                        root = topNode.Right;
                        nodeStack.Push(root);
                    }
                }
            }
        }

        public static void PreorderTraverseWithRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                return;
            }

            Console.WriteLine("Node data type is: {0}, its value is: {1}", root.Data.GetType(), root.Data);
            PreorderTraverseWithRecursion(root.Left);
            PreorderTraverseWithRecursion(root.Right);
        }

        public static IEnumerable<Tree<TItem>> PreorderTraverseWithoutRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                yield return null;
            }

            Stack<Tree<TItem>> nodeStack = new Stack<Tree<TItem>>();
            nodeStack.Push(root);
            
            while (!nodeStack.IsEmpty())
            {
                var topNode = nodeStack.Peek();
                
                // This is where preorder traverse happened
                yield return topNode;
                
                while (topNode.Left != null)
                {
                    topNode = topNode.Left;
                    nodeStack.Push(topNode);
                }
                while (nodeStack.Count != 0)
                {
                    topNode = nodeStack.Pop();
                    if (topNode.Right != null)
                    {
                        break;
                    }
                }
                if (topNode.Right != null)
                {
                    topNode = topNode.Right;
                    nodeStack.Push(topNode);
                }
            }
        }

        public static void PostorderTraverseWithRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                return;
            }

            PostorderTraverseWithRecursion(root.Left);
            PostorderTraverseWithRecursion(root.Right);
            Console.WriteLine("Node data type is: {0}, its value is: {1}", root.Data.GetType(), root.Data);
        }

        public static IEnumerable<Tree<TItem>> PostorderTraverseWithoutRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                yield return null;
            }

            var s = new Stack<Tree<TItem>>();
            s.Push(root);

            while (s.Count != 0)
            {
                var topNode = s.Peek();
                while (topNode.Left != null)
                {
                    topNode = topNode.Left;
                    s.Push(topNode);
                }
                while (s.Count != 0 && (topNode == s.Peek().Right || s.Peek().Right == null))
                {
                    topNode = s.Pop();
                    yield return topNode;
                }
                if (s.Count != 0)
                {
                    s.Push(s.Peek().Right);
                }
            }
        }

        public static Tree<TItem> ConstructTreeUsingPreAndInOrder(TItem[] preOrder, int preStart, int preEnd,
            TItem[] inOrder, int inStart, int inEnd)
        {
            if (preOrder.IsNullOrEmpty() || inOrder.IsNullOrEmpty() || preStart > preEnd || inStart > inEnd)
            {
                return null;
            }

            if (inStart == inEnd)
            {
                return new Tree<TItem>(preOrder[preStart]);
            }

            TItem thisData = preOrder[preStart];
            Tree<TItem> root = new Tree<TItem>(thisData);
            int i = inStart;
            for (; i <= inEnd; ++i)
            {
                if (inOrder[i].CompareTo(thisData) == 0)
                {
                    break;
                }
            }
            int leftLength = i - inStart;
            
            return new Tree<TItem>(root.Data, ConstructTreeUsingPreAndInOrder(preOrder, preStart + 1, preStart + leftLength, inOrder, inStart, i - 1), 
                ConstructTreeUsingPreAndInOrder(preOrder, preStart + leftLength + 1, preEnd, inOrder, i + 1, inEnd));
        }

        public static int LongestPathInTree(Tree<TItem> root, ref int maxLen)
        {
            if (root == null || root.Left == null && root.Right == null)
            {
                return 0;
            }

            int leftDepth;
            int rightDepth;

            if (root.Left == null)
            {
                rightDepth = 1 + LongestPathInTree(root.Right, ref maxLen);
                maxLen = Math.Max(rightDepth, maxLen);
                return rightDepth;
            }

            if (root.Right == null)
            {
                leftDepth = 1 + LongestPathInTree(root.Left, ref maxLen);
                maxLen = Math.Max(leftDepth, maxLen);
                return leftDepth;
            }

            leftDepth = 1 + LongestPathInTree(root.Left, ref maxLen);
            rightDepth = 1 + LongestPathInTree(root.Right, ref maxLen);
            maxLen = Math.Max(rightDepth + leftDepth, maxLen);
            return Math.Max(leftDepth, rightDepth);
        }
    }
}
