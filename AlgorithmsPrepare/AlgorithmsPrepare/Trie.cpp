#include "Trie.h"

void Trie::Insert(string word)
{
	if (word.length() == 0)
	{
		return;
	}
	this->root->Insert(word, 0);
}

bool Trie::StartWith(string word)
{
	if (word.length() == 0)
	{
		return true;
	}
	return this->root->StartWith(word, 0);
}

bool Trie::Search(string word)
{
	if (word.length() == 0)
	{
		return true;
	}
	return this->root->Search(word, 0);
}