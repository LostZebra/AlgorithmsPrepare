using System.Collections.Generic;
using System.Text;
using AlgorithmsPrepareCSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmPrepareCSharpTest
{
    [TestClass]
    public class TestSolution
    {
        [TestMethod]
        public void TestTwoSumTwelve()
        {
            var s = Solution.CreateInstance();
            Assert.IsTrue(s.TwoSumTwelve(new[] {1, 3, 9, 10}));
            Assert.IsFalse(s.TwoSumTwelve(new[] {1, 2}));
            Assert.IsFalse(s.TwoSumTwelve(new[] {1}));

            int[] emptyArray = new int[0];
            Assert.IsFalse(s.TwoSumTwelve(emptyArray));

            Assert.IsTrue(s.TwoSumTwelve(new[] {12, 1, 0}));
            Assert.IsFalse(s.TwoSumTwelve(new[] {12, 1, 1}));
        }

        [TestMethod]
        public void TestMinimumMeetingRoom()
        {
            var list1 = new List<Interval>
            {
                new Interval(1, 3),
                new Interval(2, 5),
                new Interval(4, 6),
                new Interval(4, 7),
                new Interval(5, 8)
            };
            var s = Solution.CreateInstance();
            Assert.AreEqual(3, s.MinimumMeetingRoom(list1));
        }

        [TestMethod]
        public void TestTrimAndReverse()
        {
            var testStrs =
                new[]
                {
                    "abc   efg  hij",
                    "abc efg hij",
                    " abc   efg  hij  ",
                    "a b"
                };
            var s = Solution.CreateInstance();
            Assert.AreEqual("cba gfe jih", s.TrimAndReverse(testStrs[0]));
            Assert.AreEqual("cba gfe jih", s.TrimAndReverse(testStrs[1]));
            Assert.AreEqual("cba gfe jih", s.TrimAndReverse(testStrs[2]));
            Assert.AreEqual("a b", s.TrimAndReverse(testStrs[3]));
        }

        [TestMethod]
        public void TestFindSum()
        {
            var s = Solution.CreateInstance();
            Assert.AreEqual(false, s.FindSum(new[] {3, 2, 4, 5}, 18));
            Assert.AreEqual(true, s.FindSum(new[] { 3, 2, 4, 5 }, 11));
            Assert.AreEqual(false, s.FindSum(new[] {3}, 0));
        }

        [TestMethod]
        public void TestStairs()
        {
            var s = Solution.CreateInstance();
            var testTemplate = new[] {"111", "12", "21"};
            var retList = s.Stairs(3);

            for (int i = 0; i < retList.Count; ++i)
            {
                Assert.AreEqual(testTemplate[i], retList[i]);
            }
        }

        [TestMethod]
        public void TestCheckPalindrome()
        {
            var s = Solution.CreateInstance();
            var strTemplate = new[] { "ABA", "A!#A", "A man, a plan, a canal, Panama!", "", "A", "A1221A", "A#A#" };
            var resultsTemplate = new[] { true, true, true, false, true, true, true };

            for (int i = 0; i < strTemplate.Length; ++i)
            {
                Assert.AreEqual(resultsTemplate[i], s.CheckPalindrome(strTemplate[i]));
            }
        }

        [TestMethod]
        public void TestCalculate()
        {
            var s = Solution.CreateInstance();
            Assert.AreEqual(17, s.Calculate("3*4+5"));
            Assert.AreEqual(17, s.Calculate("5+3*4"));
            Assert.AreEqual(12, s.Calculate("4+3+5"));
            Assert.AreEqual(12, s.Calculate("2*3*2"));
            Assert.AreEqual(12, s.Calculate("1*2+2*5"));
            Assert.AreEqual(12, s.Calculate("1+2*5+1"));
        }

        [TestMethod]
        public void TestShortestSubstringWithCharacters()
        {
            var s = Solution.CreateInstance();
            Assert.AreEqual("cba", s.ShortestSubstringWithCharacters("abc", "abbcbcba"));
            Assert.AreEqual("abc", s.ShortestSubstringWithCharacters("abc", "abbccaabc"));
            Assert.AreEqual("BANC", s.ShortestSubstringWithCharacters("ABC", "ADOBECODEBANC"));
            Assert.AreEqual("a", s.ShortestSubstringWithCharacters("a", "a"));
            Assert.AreEqual("", s.ShortestSubstringWithCharacters("abc", "ab"));
        }

        [TestMethod]
        public void TestGetLongestConsecutiveChar()
        {
            var s = Solution.CreateInstance();
            var testTemplate = new List<List<char>>
            {
                new List<char> {'i', 's', 'e', 'e', 'n'},
                new List<char> {'i', 'e', 'n'},
                new List<char> {'e'},
                new List<char> {'t', 'h', 'i', 's', 'i', 's'}
            };
            var testStr = new[]
            {
                "thiis iss a teest seentennce", "thiiis iss aa teeest seentennnce", "thiiiis iss a teeest seeentennncccceeeee", "this is"
            };
            for (int i = 0; i < testStr.Length; ++i)
            {
                var result = s.GetLongestConsecutiveChar(testStr[i]);
                for (int j = 0; j < testTemplate[i].Count; ++j)
                {
                    Assert.AreEqual(testTemplate[i][j], result[j]);
                }
            }
        }

        [TestMethod]
        public void TestGetMaximumDiff()
        {
            var s = Solution.CreateInstance();
            Assert.AreEqual(5, s.GetMaximumDiff(new[] {1, 2, 3, 4, 5, 6}));
            Assert.AreEqual(5, s.GetMaximumDiff(new[] {4, 5, 9, 7, 3, 2}));
            Assert.AreEqual(13, s.GetMaximumDiff(new[] {4, 5, 9, 3, 7, 2, 8, 11, 7, 15}));
            Assert.AreEqual(1, s.GetMaximumDiff(new[] {1, 2, 1, 2, 1}));
        }

        [TestMethod]
        public void TestInPlaceReverse()
        {
            var s = Solution.CreateInstance();
            Assert.AreEqual("Christmas merry a you wish I", s.InplaceReverse(new StringBuilder("I wish you a merry Christmas")));
            Assert.AreEqual("e s", s.InplaceReverse(new StringBuilder("s e")));
        }

        [TestMethod]
        public void TestConvertToDList()
        {
            var s = Solution.CreateInstance();
            TreeNode root = new TreeNode(2);
            root.Left = new TreeNode(1);
            root.Right = new TreeNode(3);

            var head = s.ConvertTreeToDList(root);
            var testTemplate = new[] {1, 2, 3};
            foreach (int num in testTemplate)
            {
                Assert.AreEqual(num, head.Data);
                head = head.Right;
            }
        }

        [TestMethod]
        public void TestAgeCount()
        {
            var s = Solution.CreateInstance();
            var sample = new[] {8, 8, 8, 9, 9, 11, 15, 16, 16, 16};
            var count = new int[17];
            
            foreach (int num in sample)
            {
                count[num]++;
            }

            var res = s.AgeCount(sample);
            for (int i = 1; i < res.Length; ++i)
            {
                Assert.AreEqual(res[i], count[i]);
            }
        }
    }
}
