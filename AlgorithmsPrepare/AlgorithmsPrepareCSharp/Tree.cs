using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    class Tree<TItem> where TItem : struct, IComparable<TItem>
    {
        private readonly TItem _data;
        public TItem Data {
            get { return this._data; }
        }

        private readonly Tree<TItem> _left;
        public Tree<TItem> Left {
            get { return this._left; }
        }

        private readonly Tree<TItem> _right;
        public Tree<TItem> Right {
            get { return this._right; }
        }

        public Tree()
        {
            this._data = default(TItem);
            this._left = null;
            this._right = null;
        }

        public Tree(TItem data, Tree<TItem> left = null, Tree<TItem> right = null)
        {
            this._data = data;
            this._left = left;
            this._right = right;
        }

        public Tree<TItem> InsertWithRecursion(Tree<TItem> root, TItem nodeData)
        {
            if (root == null)
            {
                return new Tree<TItem>(nodeData);
            }

            return nodeData.CompareTo(root.Data) <= 0 ? 
                new Tree<TItem>(root.Data, InsertWithRecursion(root.Left, nodeData), root.Right) 
                : new Tree<TItem>(root.Data, root.Left, InsertWithRecursion(root.Right, nodeData));
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

        public static void PreorderTraverseWithoutRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                return;
            }

            Stack<Tree<TItem>> nodeStack = new Stack<Tree<TItem>>();
            nodeStack.Push(root);
            while (!nodeStack.IsEmpty())
            {
                Console.WriteLine("Node data type is: {0}, its value is: {1}", root.Data.GetType(), root.Data);
                if (root.Left != null)
                {
                    nodeStack.Push(root.Left);
                    root = root.Left;
                }
                else
                {
                    Tree<TItem> topNode = nodeStack.Pop();
                    while (!nodeStack.IsEmpty() && topNode.Right == null)
                    {
                        topNode = nodeStack.Pop();
                    }
                    if (topNode.Right != null)
                    {
                        root = topNode.Right;
                        nodeStack.Push(root);
                    }
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

        public static void PostorderTraverseWithoutRecursion(Tree<TItem> root)
        {
            if (root == null)
            {
                return;
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
                else if (root.Right != null)
                {
                    root = root.Right;
                    nodeStack.Push(root);
                }
                else
                {
                    Tree<TItem> topNode = nodeStack.Pop();
                    while (!nodeStack.IsEmpty() && (topNode.Right == null || root == topNode.Right))
                    {
                        root = topNode;
                        Console.WriteLine("Node data type is: {0}, its value is: {1}", topNode.Data.GetType(), topNode.Data);
                        topNode = nodeStack.Pop();
                    }
                    if (topNode.Right != null && root == topNode.Left)
                    {
                        nodeStack.Push(topNode);
                        root = topNode.Right;
                        nodeStack.Push(root);
                    }
                    else
                    {
                        Console.WriteLine("Node data type is: {0}, its value is: {1}", topNode.Data.GetType(), topNode.Data);
                    }
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
