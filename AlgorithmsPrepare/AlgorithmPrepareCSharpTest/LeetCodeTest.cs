using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsPrepareCSharp;

namespace AlgorithmPrepareCSharpTest
{
    [TestClass]
    public class LeetCodeTest
    {
        [TestMethod]
        public void TestShortestPalindrome()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual("aaacecaaa", lc.ShortestPalindrome("aacecaaa"));
            Assert.AreEqual("dcbabcd", lc.ShortestPalindrome("abcd"));
        }

        [TestMethod]
        public void TestKthLargest()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual(6, lc.FindKthLargest(new[] { 3, 2, 7, 5, 6, 4 }, 2));
        }

        [TestMethod]
        public void TestCombinationSum()
        {
            var lc = LeetCode.CreateInstance();
            var template = new List<IList<int>> {new List<int> {1, 2, 6}, new List<int>{1, 3, 5}, new List<int>{2, 3, 4}};
            var results = lc.CombinationSum3(3, 9);
            for (int i = 0; i < template.Count; ++i)
            {
                for (int j = 0; j < template[i].Count; ++j)
                {
                    Assert.AreEqual(template[i][j], results[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMinWindow()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual("BANC", lc.MinWindow("ADOBECODEBANC", "ABC"));
            Assert.AreEqual("aa", lc.MinWindow("aa", "aa"));
            Assert.AreEqual("", lc.MinWindow("a", "aa"));
        }

        [TestMethod]
        public void TestStrStr()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual(1, lc.StrStr("abcd", "bcd"));
            Assert.AreEqual(2, lc.StrStr("cdfg", "fg"));
            Assert.AreEqual(0, lc.StrStr("a", ""));
            Assert.AreEqual(-1, lc.StrStr("", "a"));
        }

        [TestMethod]
        public void TestSearchInsertWithDuplicates()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual(0, lc.SearchInsertWithDuplicates(new[] {1, 2, 2, 3}, 0));
            Assert.AreEqual(9, lc.SearchInsertWithDuplicates(new[] {1, 2, 3, 4, 4, 5, 5, 6, 6}, 7));
            Assert.AreEqual(3, lc.SearchInsertWithDuplicates(new[] {1, 2, 2, 3, 3, 3, 5}, 3));
            Assert.AreEqual(1, lc.SearchInsertWithDuplicates(new[] { 1, 2, 2, 3, 3, 3, 5 }, 2));
        }

        [TestMethod]
        public void TestReverseWords()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual("b a", lc.ReverseWords("   a   b "));
        }

        [TestMethod]
        public void TestPathSum()
        {
            var lc = LeetCode.CreateInstance();
            var root = new TreeNode(1)
            {
                Left = new TreeNode(2),
                Right = new TreeNode(3)
            };

            var retList = lc.PathSum(root, 3);
            var testTemplate = new List<List<int>> {new List<int> {1, 2}};
            for (int i = 0; i < testTemplate.Count; ++i)
            {
                for (int j = 0; j < testTemplate[i].Count; ++j)
                {
                    Assert.AreEqual(testTemplate[i][j], retList[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMaximalSquare()
        {
            var lc = LeetCode.CreateInstance();
            var maximum1 = lc.MaximalSquare(new[,] { { '1', '0', '1', '0' }, { '1', '0', '1', '1' }, { '1', '0', '1', '1' }, { '1', '1', '1', '1' } });
            var maximum2 = lc.MaximalSquare(new[,] { { '1' }, { '1' } });
            Assert.AreEqual(4, maximum1);
            Assert.AreEqual(1, maximum2);
        }

        [TestMethod]
        public void TestBasicCalculator2()
        {
            var lc = LeetCode.CreateInstance();
            Assert.AreEqual(7, lc.Calculate("3+2*2"));
            Assert.AreEqual(1, lc.Calculate(" 3/2 "));
            Assert.AreEqual(5, lc.Calculate(" 3+5 / 2 "));
            Assert.AreEqual(12, lc.Calculate(" 11 + 1 "));
            Assert.AreEqual(27, lc.Calculate("100000000/1/2/3/4/5/6/7/8/9/10"));
            Assert.AreEqual(2623, lc.Calculate("123-8*5-57/3+148+1*3/2*14*11*2*5/4*3/3/3+2283"));
        }
    }
}
