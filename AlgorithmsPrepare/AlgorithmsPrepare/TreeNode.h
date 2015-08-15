#pragma once
class TreeNode
{
public:
	TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
    int val;
	TreeNode *left, *right;
};
