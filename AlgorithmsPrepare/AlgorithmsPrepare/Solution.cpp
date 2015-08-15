#include "Solution.h"

#include <set>
#include <algorithm>
#include <map>

// Constructor
Solution::Solution(){}

vector<vector<int>> Solution::CombinationSum3(int k, int n) {
	vector<vector<int>> retVector;
	vector<int> tmpVector;
	FindAllCombinations(retVector, tmpVector, 0, 0, 1, k, n);
	return retVector;
}

int Solution::FindKthLargest(vector<int>& nums, int k) {
	auto start_index = 0;
	int end_index = nums.size() - 1;
	while (true) {
		SwapElementsInVector(nums, start_index, end_index);
		auto first_high = start_index;
		auto pivot = nums[end_index];
		for (auto i = start_index; i < end_index; ++i) {
			if (nums[i] < pivot) {
				SwapElementsInVector(nums, i, first_high);
				first_high++;
			}
		}
		SwapElementsInVector(nums, first_high, end_index);
		auto right = end_index - first_high + 1;
		if (right == k) {
			return nums[first_high];
		}
		if (right < k) {
			end_index -= right;
			k -= right;
		}
		else {
			start_index = first_high + 1;
		}
	}
}

string Solution::ShortestPalindrome(string s) {
	string reversed = "";
	for (auto c : s) {
		reversed = c + reversed;
	}

	auto composed = s + '.' + reversed;
	
	vector<int> pi(composed.size());

	for (decltype(composed.size()) i = 1; i < composed.size(); ++i) {
		auto temp = pi[i - 1];
		while (temp != 0 && composed[temp] != composed[i]) {
			temp = pi[temp - 1];
		}
		temp += composed[temp] == composed[i] ? 1 : 0;
		pi[i] = temp;
	}

	return reversed.substr(0, reversed.size() - pi[composed.size() - 1]) + s;
}

bool Solution::ContainsDuplicate(vector<int>& nums) {
	/*sort(nums.begin(), nums.end());
	auto last_not_dup = unique(nums.begin(), nums.end());
	return (last_not_dup != nums.end());*/

	set<int> s;
	auto len = nums.size();
	for (auto i = 0; i != len; ++i) {
		if (s.find(nums[i]) != s.end()) {
			return true;
		}
		s.insert(nums[i]);
	}

	return false;
}

//vector<string> Solution::FindWords(vector<vector<char>>& board, vector<string>& words)
//{
//	auto row = board.size();
//	auto col = board[0].size();
//
//	vector<vector<bool>> is_visited(row, vector<bool>(col, false));
//
//	auto trie = new Trie();
//	set<string> words_set;
//	for (auto i = 0; i < words.size(); ++i)
//	{
//		trie->Insert(words[i]);
//		words_set.insert(words[i]);
//	}
//
//	set<string> found_set;
//
//	for (auto i = 0; i < row; ++i)
//	{
//		for (auto j = 0; j < col; ++j)
//		{
//			FindAllWords("", board, i, j, row, col, *trie, words_set, found_set, is_visited);
//		}
//	}
//
//	vector<string> list;
//	for (auto iter = found_set.begin(); iter != found_set.end(); ++iter)
//	{
//		list.push_back(*iter);
//	}
//	return list;
//}

void Solution_backup::Fuck()
{
	// DO FUCK
}

int Solution::MinSubArrayLen(int s, vector<int>& nums)
{
	int min_diff = INT_MAX;
	int min_len = INT_MAX;

	auto tmp_total = 0;
	auto index = 0;
	auto start = 0;

	while (index < nums.size()) {
		tmp_total += nums[index];
		if (tmp_total >= s) {
			if (tmp_total - s < min_diff) {
				min_diff = tmp_total - s;
			}
			while (tmp_total >= s) {
				tmp_total -= nums[start++];
			}
			min_len = min(min_len, index - start + 2);
		}
		index++;
	}

	return min_diff != INT_MAX ? min_len : 0;
}

ListNode* Solution::ReverseList(ListNode* head)
{
	if (head == nullptr || head->next == nullptr) {
		return head;
	}

	return Reverse(head)[0];
}

bool Solution::IsIsomorphic(string s, string t)
{
	map<char, char> mapping;
	set<char> value_set;

	for (decltype(s.length()) i = 0; i < s.length(); ++i) {
		if (mapping.find(s[i]) == mapping.end())
		{
			if (value_set.find(t[i]) != value_set.end()) {
				return false;
			}
			mapping[s[i]] = t[i];
			value_set.insert(t[i]);
		}
		else {
			if (mapping[s[i]] != t[i]) {
				return false;
			}
		}
	}

	return true;
}

int Solution::CountPrimes(int n)
{
	if (n <= 2) {
		return 0;
	}

	auto non_prime = static_cast<bool *>(malloc(sizeof(bool) * n));
	auto limit = static_cast<int>(sqrt(n)) + 1;

	for (auto i = 2; i < limit; ++i) {
		if (!non_prime[i]) {
			for (auto j = i * i; j < n; j += i) {
				non_prime[j] = true;
			}
		}
	}

	auto count = 0;
	for (auto i = 2; i < n; ++i) {
		if (!non_prime[i]) {
			count++;
		}
	}

	return count;
}

vector<int> Solution::TwoSum(vector<int>& nums, int target)
{
	map<int, int> dict;
	for (auto i = 0; i < nums.size(); ++i) {
		if (dict.find(target - nums[i]) != dict.end()) {
			return vector<int>{dict[target - nums[i]], i + 1};
		}
		if (dict.find(nums[i]) == dict.end()) {
			dict[nums[i]] = i + 1;
		}
	}

	return vector<int>{0, 0};
}

vector<pair<int, int>> Solution::TwoSumSorted(vector<int>& nums, int target)
{
	vector<pair<int, int>> ret;
	
	int left = 0;
	int right = nums.size() - 1;

	while (left < right)
	{
		if (left != 0 && nums[left] == nums[left - 1])
		{
			left++;
			continue;
		}
		if (right != nums.size() - 1 && nums[right] == nums[right + 1])
		{
			right--;
			continue;
		}

		auto total = nums[left] + nums[right];
		if (total < target)
		{
			left++;
		}
		else if (total > target)
		{
			right--;
		}
		else
		{
			pair<int, int> new_pair(nums[left], nums[right]);
			ret.push_back(new_pair);
			left++;
			right--;
		}
	}

	return ret;
}

ListNode* Solution::AddTwoNumbers(ListNode* l1, ListNode* l2)
{
	if (l1 == nullptr || l2 == nullptr) {
		return l1 != nullptr ? l1 : l2;
	}

	auto iter1 = l1;
	auto iter2 = l2;
	ListNode *head = nullptr, *iter3 = nullptr;
	int advance = 0;
	while (iter1 != nullptr && iter2 != nullptr) {
		int total = iter1->val + iter2->val + advance;
		if (total >= 10) {
			total -= 10;
			advance = 1;
		}
		else {
			advance = 0;
		}
		auto new_node = new ListNode(total);
		if (head == nullptr) {
			head = iter3 = new_node;
		}
		else {
			iter3->next = new_node;
			iter3 = new_node;
		}
		iter1 = iter1->next;
		iter2 = iter2->next;
	}

	while (iter1 != nullptr) {
		int total = iter1->val + advance;
		if (total >= 10) {
			total -= 10;
			advance = 1;
		}
		else {
			advance = 0;
		}
		auto new_node = new ListNode(total);
		iter3->next = new_node;
		iter3 = new_node;
		iter1 = iter1->next;
	}

	while (iter2 != nullptr) {
		int total = iter2->val + advance;
		if (total >= 10) {
			total -= 10;
			advance = 1;
		}
		else {
			advance = 0;
		}
		auto new_node = new ListNode(total);
		iter3->next = new_node;
		iter3 = new_node;
		iter2 = iter2->next;
	}

	if (advance == 1) {
		iter3->next = new ListNode(1);
	}

	return head;
}

bool Solution::IsSymmetric(TreeNode* root)
{
	if (root == nullptr)
	{
		return true;
	}
	return IsEqual(root->left, root->right);
}

int Solution::LengthOfLongestSubstring(string s)
{
	auto str_len = s.length();

	if (str_len <= 1)
	{
		return str_len;
	}
	
	int longest_path = INT_MIN;
	int begin_loc = 0;
	
	map<char, int> dict;

	for (auto i = 0; i < str_len; ++i)
	{
		if (dict.find(s[i]) == dict.end())
		{
			dict[s[i]] = i;
		}
		else
		{
			if (dict[s[i]] < begin_loc)
			{
				dict[s[i]] = i;
			}
			else
			{
				longest_path = max(longest_path, i - begin_loc);
				begin_loc = dict[s[i]] + 1;
				dict[s[i]] = i;
			}
		}
	}

	return max(longest_path, static_cast<int>(str_len) - begin_loc);
}

double Solution::FindMedianSortedArrays(vector<int>& nums1, vector<int>& nums2)
{
	auto len_nums1 = nums1.size();
	auto len_nums2 = nums2.size();

	auto total = len_nums1 + len_nums2;

	return total % 2 == 0 ? (FindKthElementInTwoSortedArray(nums1, 0, len_nums1 - 1, nums2, 0, len_nums2 - 1, total / 2) + FindKthElementInTwoSortedArray(nums1, 0, len_nums1 - 1, nums2, 0, len_nums2 - 1, total / 2 + 1)) / 2.0
						  : FindKthElementInTwoSortedArray(nums1, 0, len_nums1 - 1, nums2, 0, len_nums2, total / 2 + 1);
}

// Private methods
void Solution::SwapElementsInVector(vector<int>& nums, int index1, int index2) {
	auto temp = nums[index1];
	nums[index1] = nums[index2];
	nums[index2] = temp;
}

void Solution::FindAllCombinations(vector<vector<int>>& ret_vector, vector<int>& tmp_vector, int cur_index, int cur_sum, int last, int k, int n) {
	if (cur_sum > n) {
		return;
	}
	if (cur_index == k) {
		if (cur_sum != n) return;
		ret_vector.push_back(vector<int>(tmp_vector));
		return;
	}

	for (auto i = last; i <= 9; ++i) {
		tmp_vector.push_back(i);
		FindAllCombinations(ret_vector, tmp_vector, cur_index + 1, cur_sum + i, i + 1, k, n);
		tmp_vector.pop_back();
	}
}

vector<ListNode *> Solution::Reverse(ListNode *rest_head)
{
	if (rest_head->next == nullptr) {
		return vector<ListNode *>{rest_head, rest_head};
	}

	auto ret = Reverse(rest_head->next);
	auto tail = ret[1];
	tail->next = rest_head;
	rest_head->next = nullptr;

	return vector<ListNode *>{ret[0], rest_head};
}

bool Solution::IsEqual(TreeNode* left, TreeNode* right)
{
	if (left == nullptr && right == nullptr) {
		return true;
	}
	if (left != nullptr && right == nullptr || left == nullptr && right != nullptr) {
		return false;
	}
	return left->val == right->val && IsEqual(left->left, right->right) && IsEqual(left->right, right->left);
}

int Solution::FindKthElementInTwoSortedArray(vector<int>& nums1, int s1, int e1, vector<int>& nums2, int s2, int e2, int k)
{
	if (e1 - s1 > e2 - s2)
	{
		return FindKthElementInTwoSortedArray(nums2, s2, e2, nums1, s1, e1, k);
	}
	// No element exists in nums1
	if (e1 < s1)
	{
		return nums2[s2 + k - 1];
	}
	// Get the first element
	if (k == 1)
	{
		return min(nums1[s1], nums2[s2]);
	}

	auto index1 = min(e1 - s1 + 1, k / 2);
	auto index2 = k - index1;
	if (nums1[s1 + index1 - 1] == nums2[s2 + index2 - 1])
	{
		return nums1[s1 + index1 - 1];
	}
	if (nums1[s1 + index1 - 1] < nums2[s2 + index2 - 1])
	{
		return FindKthElementInTwoSortedArray(nums1, s1 + index1, e1, nums2, s2, e2, k - index1);
	}
	return FindKthElementInTwoSortedArray(nums1, s1, e1, nums2, s2 + index2, e2, k - index2);
}


string Solution::LongestPalindrome(string s)
{
	auto s_len = s.size();
	if (s_len <= 1)
	{
		return s;
	}

	int max_len = INT_MIN;
	int tmp_len;
	int start = -1;

	for (int i = 1; i < s_len; ++i)
	{
		int low = i - 1;
		int high = i;
		while (low != -1 && high != s_len && s[low] == s[high])
		{
			tmp_len = high - low + 1;
			if (tmp_len > max_len)
			{
				start = low;
				max_len = tmp_len;
			}
			low--;
			high++;
		}

		low = i - 1;
		high = i + 1;
		while (low != -1 && high != s_len && s[low] == s[high])
		{
			tmp_len = high - low + 1;
			if (tmp_len > max_len)
			{
				start = low;
				max_len = tmp_len;
			}
			low--;
			high++;
		}
	}

	return s.substr(start, max_len);
}

bool Solution::ContainsNearbyDuplicate(vector<int>& nums, int k)
{
	map<int, int> dict;

	bool has_found = false;
	for (int i = 0; i < nums.size(); ++i) {
		if (dict.find(nums[i]) == dict.end()) {
			dict[nums[i]] = i;
		}
		else {
			if (has_found && i - dict[nums[i]] <= k) {
				return false;
			}
			if (!has_found && i - dict[nums[i]] <= k) {
				has_found = true;
			}
			dict[nums[i]] = i;
		}
	}

	return has_found;
}

//void Solution::FindAllWords(string cur_str, vector<vector<char>>& board, int cur_row, int cur_col, int row, int col, Trie& trie, set<string>& words_set, set<string>& found_set, vector<vector<bool>>& is_visited)
//{
//	if (cur_row == -1 || cur_row == row || cur_col == -1 || cur_col == col || is_visited[cur_row][cur_col])
//	{
//		return;
//	}
//
//	is_visited[cur_row][cur_col] = true;
//	cur_str += board[cur_row][cur_col];
//
//	if (!trie.StartWith(cur_str))
//	{
//		is_visited[cur_row][cur_col] = false;
//		return;
//	}
//
//	if (words_set.find(cur_str) != words_set.end())
//	{
//		found_set.insert(cur_str);
//	}
//
//	FindAllWords(cur_str, board, cur_row + 1, cur_col, row, col, trie, words_set, found_set, is_visited);
//	FindAllWords(cur_str, board, cur_row - 1, cur_col, row, col, trie, words_set, found_set, is_visited);
//	FindAllWords(cur_str, board, cur_row, cur_col + 1, row, col, trie, words_set, found_set, is_visited);
//	FindAllWords(cur_str, board, cur_row, cur_col - 1, row, col, trie, words_set, found_set, is_visited);
//
//	is_visited[cur_row][cur_col] = false;
//}

// Destructor
Solution::~Solution(){};
