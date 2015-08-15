#include "stdafx.h"
#include <CppUnitTest.h>
#include "..\AlgorithmsPrepare\Solution.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace AlgorithmPrepareTest
{		
	TEST_CLASS(TestSolution)
	{
	public:
		
		TEST_METHOD(TestCombinationSum3)
		{
			// TODO: Your test code here
			//Assert::AreEqual(1, 1, L"Test Failed!");
			auto s = Solution();
			auto test_template = vector < vector<int> > {vector < int > {1, 2, 4}};
			auto results = s.CombinationSum3(3, 7);

			for (size_t i = 0; i < test_template.size(); ++i)
			{
				for (size_t j = 0; j < test_template[i].size(); ++j)
				{
					Assert::AreEqual(test_template[i][j], results[i][j], L"Test Failed!");
				}
			}
		}

		TEST_METHOD(TestFindKthLargest)
		{
			auto s = Solution();
			auto &test_template = vector<int>{3, 2, 1, 5, 6, 4};
			auto result = s.FindKthLargest(test_template, 2);
			Assert::AreEqual(5, result);
		}

		TEST_METHOD(TestShortestPalindrome)
		{
			auto s = Solution();
			auto shortest_palindrome = s.ShortestPalindrome("aacecaaa");
			string template_str = "aaacecaaa";
			Assert::AreEqual(template_str.c_str(), shortest_palindrome.c_str(), true);
		}

		TEST_METHOD(TestContainsDuplicate)
		{
			auto s = Solution();
			auto &ref_test1 = vector<int>{ 3, 2, 3, 5, 4 };
			auto &ref_test2 = vector<int>{ 3, 1, 2 };
			Assert::AreEqual(true, s.ContainsDuplicate(ref_test1));
			Assert::AreEqual(false, s.ContainsDuplicate(ref_test2));
		}

		TEST_METHOD(TestMinSubArrayLen)
		{
			auto s = Solution();
			auto& test_template = vector < int > {2, 3, 1, 2, 4, 3};
			Assert::AreEqual(2, s.MinSubArrayLen(7, test_template));
		}

		TEST_METHOD(TestIsIsomorphic)
		{
			auto s = Solution();
			Assert::AreEqual(true, s.IsIsomorphic("egg", "add"));
			Assert::AreEqual(false, s.IsIsomorphic("foo", "bar"));
			Assert::AreEqual(true, s.IsIsomorphic("paper", "title"));
		}

		TEST_METHOD(TestTwoSum)
		{
			auto s = Solution();
			auto test_template = vector < int > {2, 7, 11, 15};
			auto result_template = vector < int > {1, 2};
			auto result = s.TwoSum(test_template, 9);
			for (auto i = 0; i < result_template.size(); ++i)
			{
				Assert::AreEqual(result_template[i], result[i]);
			}
		}

		TEST_METHOD(TestLengthOfLongestSubstring)
		{
			auto s = Solution();
			Assert::AreEqual(1, s.LengthOfLongestSubstring("aaaaaa"));
			Assert::AreEqual(3, s.LengthOfLongestSubstring("abca"));
			Assert::AreEqual(0, s.LengthOfLongestSubstring(""));
		}
	};
}