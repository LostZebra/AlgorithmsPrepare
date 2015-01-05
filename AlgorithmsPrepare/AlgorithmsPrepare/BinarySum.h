#include <string>

using namespace std;

class BinarySum
{
public:
	string addBinary(string a, string b) {
		if (a.empty() && b.empty()) {
			return "";
		}

		int aLen = a.length();
		int bLen = b.length();

		int maxNumOfDigits = (aLen >= bLen) ? aLen + 1 : bLen + 1;
		int* biStr = (int *)malloc(sizeof(int) * maxNumOfDigits);
		for (int i = 0; i < maxNumOfDigits; ++i) {
			biStr[i] = 0;
		}
		// memset(biStr, 0, sizeof(int) * maxNumOfDigits);

		int i, j;
		int advance = 0;
		int k = maxNumOfDigits - 1;
		for (i = aLen - 1, j = bLen - 1; i >= 0 && j >= 0; --i, --j) {
			int total = (a[i] - '0') + (b[j] - '0') + advance;
			if (total < 2) {
				biStr[k--] = total;
				advance = 0;
			}
			else {
				biStr[k--] = total - 2;
				advance = 1;
			}
		}
		while (i >= 0) {
			int total = (a[i] - '0') + advance;
			if (total < 2) {
				biStr[k--] = total;
				advance = 0;
			}
			else {
				biStr[k--] = total - 2;
				advance = 1;
			}
			--i;
		}
		while (j >= 0) {
			int total = (b[j] - '0') + advance;
			if (total < 2) {
				biStr[k--] = total;
				advance = 0;
			}
			else {
				biStr[k--] = total - 2;
				advance = 1;
			}
			--j;
		}
		while (k >= 0) {
			int total = biStr[k] + advance;
			if (total < 2) {
				biStr[k--] = total;
				advance = 0;
			}
			else {
				biStr[k--] = total - 2;
				advance = 1;
			}
		}

		int firstNonZero = 0;
		while (firstNonZero < maxNumOfDigits && biStr[firstNonZero] == 0) {
			++firstNonZero;
		}
		if (firstNonZero == maxNumOfDigits) {
			return "0";
		}

		string retStr;
		for (int index = firstNonZero; index < maxNumOfDigits; ++index) {
			retStr += to_string(biStr[index]);
		}
		free(biStr);
		return retStr;
	}
};