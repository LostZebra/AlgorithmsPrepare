#pragma once
#include <string>

using namespace std;

class TrieNode
{
public:
	TrieNode()
	{
		for (auto i = 0; i < 26; ++i)
		{
			children[i] = nullptr;
		}
	}
	~TrieNode()
	{
		for (auto i = 0; i < 26; ++i)
		{
			if (children[i] != nullptr)
			{
				delete children[i];
			}
		}
	}
	// Instance variables
	bool value_node{false};
	// Instance methods
	void Insert(string word, int index);
	bool StartWith(string word, int index);
	bool Search(string word, int index);
private:
	TrieNode* children[26];
};
