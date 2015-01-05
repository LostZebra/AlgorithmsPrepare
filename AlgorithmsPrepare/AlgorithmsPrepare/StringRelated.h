#include <string>
#include <map>

using namespace std;

class StringRelated
{
public:
	int firstMissingPositive(int A[], int n) {
		if (n == 0) {
			return 1;
		}

		for (int i = 0; i < n; ++i) {
			if (A[i] <= 0) {
				A[i] = n + 2;
			}
		}

		for (int i = 0; i < n; ++i) {
			int absi = abs(A[i]);
			if (absi <= n) {
				A[absi - 1] = -abs(A[absi - 1]);
			}
		}

		for (int i = 1; i <= n; ++i) {
			if (A[i - 1] > 0) {
				return i;
			}
		}

		return n + 1;
	}

	int lengthOfLongestSubstring(string s) {
		int strLen = s.length();
		if (strLen == 0 || strLen == 1) {
			return strLen;
		}

		int longest = 0;
		int beginLoc = 0;
		map<char, int> set;

		for (int curLoc = 0; curLoc < strLen; ++curLoc) {
			char temp = s[curLoc];
			if (set.find(temp) == set.end()) {
				set[temp] = curLoc;
				longest = (curLoc - beginLoc + 1 > longest) ? curLoc - beginLoc + 1 : longest;
			}
			else {
				if (set[temp] >= beginLoc) {
					beginLoc = set[temp] + 1;
				};
				longest = (curLoc - beginLoc + 1 > longest) ? curLoc - beginLoc + 1 : longest;
				set[temp] = curLoc;
			}
		}
		return longest;
	}
};