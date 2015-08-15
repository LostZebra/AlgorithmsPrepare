#pragma once

#include "TrieNode.h"

using namespace std;

class Trie
{
public:
	// Instance variables
	TrieNode* root{new TrieNode()};
	// Instance methods
	void Insert(string word);
	bool StartWith(string word);
	bool Search(string word);
};
