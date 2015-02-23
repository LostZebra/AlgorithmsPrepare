#include <iostream>
#include <string>
#include "BinarySum.h"
#include "StringRelated.h"

using namespace std;

int main(void) {
	char ch[6];
	strcpy_s(ch, "aabbc");
	int index = 0;
	int offset = 0;
	int lenCh = strlen(ch);
	while (index < lenCh - 1)
	{
		if (ch[index] == ch[index + 1])
		{
			offset++;
			index++;
		}
		else 
		{
			if (offset != 0)
			{
				*(ch + index + 1 - offset) = *(ch + index + 1);
			}
			index++;
		}
	}
	*(ch + lenCh - offset) = '\0';
	return 0;
}
