using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsPrepareCSharp;

namespace AlgorithmPrepareCSharpTest
{
    [TestClass]
    public class TestUtilityAlgorithm
    {
        [TestMethod]
        public void TestRemoveDuplicatesUnsorted()
        {
            var ret = UtilityAlgorithm.RemoveDuplicatesUnsorted(new[] {3, 2, 1, 2, 5, 4, 3});
            Assert.AreEqual(5, ret.Length);

            var template = new[]{3, 2, 1, 5, 4};
            for (int i = 0; i < ret.Length; ++i)
            {
                Assert.AreEqual(template[i], ret[i]);
            }
        }

        [TestMethod]
        public void TestRemoveDuplicate()
        {
            var ret = UtilityAlgorithm.RemoveDuplicates(new[] {1, 1, 2, 2, 3, 3});
            Assert.AreEqual(3, ret.Length);

            var template = new[] {1, 2, 3};
            for (int i = 0; i < template.Length; ++i)
            {
                Assert.AreEqual(template[i], ret[i]);
            }
        }
    }
}
