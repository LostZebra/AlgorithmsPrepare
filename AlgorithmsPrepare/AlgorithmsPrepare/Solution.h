#pragma once

#include <vector>

#include "ListNode.h"
#include "TreeNode.h"

using namespace std;

class Solution_backup
{
public:
	void Fuck();
};

class Solution
{
public:
	// Constructor
	Solution();
	// Destructor
	~Solution();
	// Instance methods
	friend void Solution_backup::Fuck();
	vector<vector<int>> CombinationSum3(int k, int n);
	int FindKthLargest(vector<int>& nums, int k);
	string ShortestPalindrome(string s);
	bool ContainsDuplicate(vector<int>& nums);
	// vector<string> FindWords(vector<vector<char>>& board, vector<string>& words);
	int MinSubArrayLen(int s, vector<int>& nums);
	ListNode* ReverseList(ListNode *head);
	bool IsIsomorphic(string s, string t);
	int CountPrimes(int n);
	vector<int> TwoSum(vector<int>& nums, int target);
	vector<pair<int, int>> TwoSumSorted(vector<int>& nums, int target);
	ListNode* AddTwoNumbers(ListNode* l1, ListNode* l2);
	bool IsSymmetric(TreeNode *root);
	int LengthOfLongestSubstring(string s);
	double FindMedianSortedArrays(vector<int>& nums1, vector<int>& nums2);
	string LongestPalindrome(string s);
	bool ContainsNearbyDuplicate(vector<int>& nums, int k);
private:
	void SwapElementsInVector(vector<int>& nums, int index1, int index2);
	void FindAllCombinations(vector<vector<int>>& ret_vector, vector<int>& tmp_vector, int cur_index, int cur_sum, int last, int k, int n);
	vector<ListNode *> Reverse(ListNode *rest_head);
	bool IsEqual(TreeNode *left, TreeNode *right);
	int FindKthElementInTwoSortedArray(vector<int>& nums1, int s1, int e1, vector<int>& nums2, int s2, int e2, int k);
	// void FindAllWords(string cur_str, vector<vector<char>>& board, int cur_row, int cur_col, int row, int col, Trie& trie, set<string>& words_set, set<string>& foundSet, vector<vector<bool>>& is_visited);
};
