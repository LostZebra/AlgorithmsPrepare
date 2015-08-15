using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsPrepareCSharp;

namespace AlgorithmPrepareCSharpTest
{
    [TestClass]
    public class TcTest
    {
        [TestMethod]
        public void TestMinPathSum()
        {
            Tc tc = Tc.GetInstance();
            int[] test1 = {19, 50, 10, 39, 12, 4, 3, 2, 2, 1};
            int[] test2 = {13, 18, 24, 11, 25, 100, 93, 92, 79};
            Tree<int> root1 = new Tree<int>(test1[0]);
            for (int i = 1; i < test1.Length; ++i)
            {
                root1.InsertWithRecursion(root1, test1[i]);
            }
            Tree<int> root2 = new Tree<int>(test2[0]);
            for (int i = 1; i < test2.Length; ++i)
            {
                root2.InsertWithRecursion(root2, test2[i]);
            }

            Assert.AreEqual(3, tc.MinPathSum(root1, 41));
            Assert.AreEqual(3, tc.MinPathSum(root1, 108));

            Assert.AreEqual(2, tc.MinPathSum(root2, 24));
        }

        [TestMethod]
        public void TestSuffixTree()
        {
            const string testStr = "Thisisabeautifulworld";
            SuffixTree tree = new SuffixTree(testStr);

            Assert.AreEqual(0, tree.FindStr("This")[0]);
            Assert.AreEqual(6, tree.FindStr("abeautifulworld")[0]);
        }
    }
}
