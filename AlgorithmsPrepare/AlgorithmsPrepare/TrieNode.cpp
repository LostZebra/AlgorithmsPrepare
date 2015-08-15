#include "TrieNode.h"

void TrieNode::Insert(string word, int index)
{
	auto ch_index = word[index] - 'a';

	if (children[ch_index] == nullptr)
	{
		children[ch_index] = new TrieNode();
	}

	if (index == word.size() - 1)
	{
		children[ch_index]->value_node = true;
		return;
	}

	auto node = children[ch_index];
	node->Insert(word, index + 1);
}

bool TrieNode::StartWith(string word, int index)
{
	if (index == word.length())
	{
		return true;
	}

	auto ch_index = word[index] - 'a';

	if (children[ch_index] == nullptr)
	{
		return false;
	}

	return children[ch_index]->StartWith(word, index + 1);
}

bool TrieNode::Search(string word, int index)
{
	if (index == word.length()) {
		return value_node;
	}

	if (word[index] == '.') {
		for (auto i = 0; i < 26; ++i) {
			if (children[i] != nullptr) {
				auto has = children[i]->Search(word, index + 1);
				if (has) {
					return has;
				}
			}
		}

		return false;
	}

	auto i = word[index] - 'a';

	return children[i] == nullptr ? false : children[i]->Search(word, index + 1);
}
