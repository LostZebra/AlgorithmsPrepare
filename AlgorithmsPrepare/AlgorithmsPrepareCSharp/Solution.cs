using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace AlgorithmsPrepareCSharp
{
    /// <summary>
    /// Code for some interviews
    /// </summary>
    public class Solution
    {
        public static readonly Lazy<Solution> S = new Lazy<Solution>();

        public static Solution CreateInstance()
        {
            return S.Value;
        }

        public string TaskOne(string s) {
            // write your code in C# 5.0 with .NET 4.5 (Mono)
            if (!s.Contains("A") && !s.Contains("C"))
            {
                return s;
            }

            char[] chars = s.ToCharArray();

            int prev = 0;
            int index = 1;
            int offset = 0;

            while (index < s.Length)
            {
                if (s[index] != s[prev])
                {
                    chars[index - offset] = chars[index];
                    prev = index;
                    index++;
                }
                else
                {
                    offset++;
                    index++;
                }
            }

            int limit = s.Length - offset;
            for (int i = 1; i < limit; ++i)
            {
                if (chars[i] == 'B')
                {
                    chars[i] = chars[i - 1];
                }
            }
            if (chars[0] == 'B' && limit >= 2)
            {
                chars[0] = chars[1];
            }

            prev = 0;
            index = 1;
            offset = 0;

            while (index < limit)
            {
                if (s[index] != s[prev])
                {
                    chars[index - offset] = chars[index];
                    prev = index;
                    index++;
                }
                else
                {
                    offset++;
                    index++;
                }
            }

            limit -= offset;

            char[] copy = new char[limit];
            Array.Copy(chars, copy, limit);

            return new string(copy);
        }

        public int TaskTwo(int a, int b)
        {
            var stackA = new Stack<int>();
            var stackB = new Stack<int>();

            if (a == 0)
            {
                stackA.Push(0);
            }
            if (b == 0)
            {
                stackB.Push(0);
            }

            while (a != 0)
            {
                stackA.Push(a % 10);
                a /= 10;
            }

            while (b != 0)
            {
                stackB.Push(b % 10);
                b /= 10;
            }

            var stackC = new Stack<int>();

            bool isANow = true;
            while (stackA.Count != 0 && stackB.Count != 0)
            {
                stackC.Push(isANow ? stackA.Pop() : stackB.Pop());
                isANow = !isANow;
            }
            while (stackA.Count != 0)
            {
                stackC.Push(stackA.Pop());
            }
            while (stackB.Count != 0)
            {
                stackC.Push(stackB.Pop());
            }

            long c = 0;
            int count = 0;
            while (stackC.Count != 0)
            {
                c += stackC.Pop() * (long)Math.Pow(10, count);
                count++;
                if (c > 100000000)
                {
                    return -1;
                }
            }

            return (int)c;
        }

        public int TaskThree(int[] a, int x, int d)
        {
            if (x <= d)
            {
                return 0;
            }

            bool[] leaves = new bool[x];
            int farest = -1;
            for (int i = 0; i < a.Length; ++i)
            {
                int index = a[i];
                if (leaves[index]) continue;

                leaves[index] = true;

                if (index > farest)
                {
                    farest = index;
                }

                bool isCrossable = true;
                if (farest + d <= x)
                {
                    int first = -1;
                    int second = -1;
                    for (int j = 0; j < farest; ++j)
                    {
                        if (!leaves[j]) continue;
                        if (first == -1)
                        {
                            first = j;
                        }
                        else
                        {
                            second = j;
                            if (second - first > d)
                            {
                                isCrossable = false;
                                break;
                            }
                            first = second;
                        }
                    }
                    if (second == -1)
                    {
                        if (first <= d)
                        {
                            return i;
                        }
                        isCrossable = false;
                    }
                }

                if (isCrossable)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool TwoSumTwelve(int[] nums)
        {
            if (nums.Length == 0)
            {
                return false;
            }
            var set = new HashSet<int>();

            foreach (int num in nums)
            {
                if (set.Contains(12 - num))
                {
                    return true;
                }
                if (!set.Contains(num))
                {
                    set.Add(num);
                }
            }

            return false;
        }

        public int MinimumMeetingRoom(List<Interval> meetings)
        {
            if (meetings.Count <= 1)
            {
                return meetings.Count;
            }

            meetings.Sort((m1, m2) => m1.Start.CompareTo(m2.Start));

            var priorityQueue = new OrderedBag<Interval>((i1, i2) => i1.End.CompareTo(i2.End));

            int minimum = int.MinValue;

            foreach (Interval meeting in meetings)
            {

                while (priorityQueue.Count != 0 && priorityQueue.GetFirst().End <= meeting.Start)
                {
                    priorityQueue.RemoveFirst();
                }
                priorityQueue.Add(meeting);
                if (priorityQueue.Count > minimum)
                {
                    minimum = priorityQueue.Count;
                }
            }

            return minimum;
        }

        public string TrimAndReverse(string s)
        {
            if (s.Length == 0)
            {
                return s;
            }

            // O(N)
            s = s.Trim();

            var tmpStr = string.Empty;
            var retStr = string.Empty;
            var isNewWord = false;

            foreach (var ch in s)
            {
                if (ch == ' ')
                {
                    if (isNewWord)
                    {
                        isNewWord = false;
                        retStr += tmpStr.Reverse() + " ";
                    }
                }
                else
                {
                    if (!isNewWord)
                    {
                        isNewWord = true;
                        tmpStr = string.Empty;
                    }
                    tmpStr += ch;
                }
            }

            retStr += tmpStr.Reverse();

            return retStr;
        }

        public bool FindSum(int[] nums, int t)
        {
            if (nums.Length == 0 && t != 0)
            {
                return false;
            }
            if (nums.Length != 0 && t == 0)
            {
                return false;
            }

            int total = 0;
            int j = 0;
            foreach (int num in nums)
            {
                while (j != nums.Length && total < t)
                {
                    total += nums[j++];
                }
                if (total == t)
                {
                    return true;
                }
                total -= num;
            }

            return false;
        }

        private void FindAllSteps(List<string> retList, int cur, int end, string curStr)
        {
            if (cur == end)
            {
                retList.Add(curStr);
                return;
            }
            if (cur > end)
            {
                return;
            }

            for (int i = 1; i != 3; ++i)
            {
                FindAllSteps(retList, cur + i, end, curStr + i);
            }
        }

        public List<string> Stairs(int n)
        {
            var retList = new List<string>();
            if (n == 0)
            {
                return retList;
            }

            FindAllSteps(retList, 0, n, string.Empty);

            return retList;
        }

        public bool CheckPalindrome(string s)
        {
            if (s.Length == 0)
            {
                return false;
            }
            if (s.Length == 1)
            {
                return char.IsLetterOrDigit(s[0]);
            }

            int left = 0;
            int right = s.Length - 1;
            while (left < right)
            {
                if (!char.IsLetterOrDigit(s[left])) 
                {
                    left++;
                    continue;
                }
                if (!char.IsLetterOrDigit(s[right]))
                {
                    right--;
                    continue;
                }
                if (char.ToLower(s[left]) != char.ToLower(s[right])) 
                {
                    return false;
                }
                left++;
                right--;
            }

            return true;
        }

        public int Calculate(string formula)
        {
            var numStack = new Stack<int>();
            var operatorStack = new Stack<char>();

            for (int i = 0; i < formula.Length; ++i)
            {
                if (char.IsDigit(formula[i]))
                {
                    numStack.Push((int)char.GetNumericValue(formula[i]));
                }
                else 
                {
                    operatorStack.Push(formula[i]);
                }
            }

            while (!operatorStack.IsEmpty())
            {
                var top = operatorStack.Pop();
                if (top == '*')
                {
                    numStack.Push(numStack.Pop() * numStack.Pop());
                }
                // More important
                else 
                {
                    if (operatorStack.Count == 0 || operatorStack.Peek() != '*')
                    {
                        numStack.Push(numStack.Pop() + numStack.Pop());
                    }
                    else 
                    {
                        numStack.Push(numStack.Pop() + numStack.Pop() * numStack.Pop());
                        operatorStack.Pop();
                    }
                }
            }

            return numStack.Pop();
        }

        public string ShortestSubstringWithCharacters(string t, string s)
        {
            if (s.Length == 0 || t.Length == 0 || s.Length < t.Length)
            {
                return string.Empty;
            }

            var set = new HashSet<char>();
            foreach (var ch in t)
            {
                set.Add(ch);
            }

            var dict = new Dictionary<char, int>();
            int beginLoc = 0;
            int minLen = s.Length + 1;
            var minStr = string.Empty;

            for (int i = 0; i < s.Length; ++i)
            {
                char c = s[i];
                if (!set.Contains(c)) continue;
                if (!dict.ContainsKey(c))
                {
                    dict.Add(c, 1);
                }
                else
                {
                    dict[c]++;
                }

                if (dict.Keys.Count != set.Count) continue;
                while (beginLoc < s.Length)
                {
                    if (!set.Contains(s[beginLoc]))
                    {
                        beginLoc++;
                        continue;
                    }

                    dict[s[beginLoc]]--;
                    if (dict[s[beginLoc]] == 0)
                    {
                        if (i - beginLoc + 1 < minLen)
                        {
                            minLen = i - beginLoc + 1;
                            minStr = s.Substring(beginLoc, minLen);
                        }
                        dict.Remove(s[beginLoc++]);
                        break;
                    }
                    beginLoc++;
                }
            }

            return minStr;
        }

        public List<char> GetLongestConsecutiveChar(string s)
        {
            var retList = new List<char>();
            if (s.Length == 0)
            {
                return retList;
            }

            var dict = new Dictionary<int, List<char>>();
            int offset = 1;
            int maxLen = 1;
            for (int i = 1; i < s.Length; ++i)
            {
                if (s[i] == s[i - 1])
                {
                    offset++;
                }
                else if (offset != 1)
                {
                    if (offset < maxLen)
                    {
                        offset = 1;
                        continue;
                    }
                    if (offset != maxLen)
                    {
                        dict.Add(offset, new List<char> {s[i - 1]});
                    }
                    else
                    {
                        dict[offset].Add(s[i - 1]);
                    }
                    maxLen = offset;
                    offset = 1;
                }
            }

            if (offset != 1 && offset >= maxLen)
            {
                if (offset != maxLen)
                {
                    dict.Add(offset, new List<char> { s[s.Length - 1] });
                }
                else
                {
                    dict[offset].Add(s[s.Length - 1]);
                }
                maxLen = offset;
            }

            if (maxLen == 1)
            {
                retList.AddRange(s.Where(ch => ch != ' '));
                return retList;
            }

            return dict[maxLen];
        }

        public int GetMaximumDiff(int[] nums)
        {
            if (nums.Length <= 1)
            {
                return 0;
            }

            int maxDiff = int.MinValue;
            int beginLoc = 0;
            for (int i = 1; i < nums.Length; ++i)
            {
                if (nums[i] > nums[beginLoc])
                {
                    maxDiff = Math.Max(maxDiff, nums[i] - nums[beginLoc]);
                }
                else
                {
                    beginLoc = i;
                }
            }

            return maxDiff;
        }

        public string InplaceReverse(StringBuilder s)
        {
            if (s.Length <= 1)
            {
                return s.ToString();
            }

            s.Append(' ');
            int beginLoc = -1;
            bool newWord = false;
            for (int i = 0; i < s.Length; ++i)
            {
                if (char.IsLetterOrDigit(s[i]))
                {
                    if (!newWord)
                    {
                        newWord = true;
                        beginLoc = i;
                    }
                }
                else
                {
                    if (newWord)
                    {
                        newWord = false;
                        int left = beginLoc;
                        int right = i - 1;
                        while (left < right)
                        {
                            var temp = s[left];
                            s[left] = s[right];
                            s[right] = temp;
                            left++;
                            right--;
                        }
                    }
                }
            }

            s.Remove(s.Length - 1, 1);
            
            var begin = 0;
            var end = s.Length - 1;
            while (begin < end)
            {
                var temp = s[begin];
                s[begin] = s[end];
                s[end] = temp;
                begin++;
                end--;
            }

            return s.ToString();
        }

        private TreeNode[] ConvertTreeToDListRecur(TreeNode root)
        {
            if (root == null)
            {
                return new TreeNode[] { null, null };
            }

            if (root.Left == null && root.Right == null)
            {
                return new[] { root, root };
            }

            var leftSibling = ConvertTreeToDListRecur(root.Left);
            root.Left = leftSibling[1];
            leftSibling[1].Right = root;

            var rightSibling = ConvertTreeToDListRecur(root.Right);
            root.Right = rightSibling[0];
            rightSibling[0].Left = root;

            return new[] {leftSibling[0], rightSibling[1]};
        }

        public TreeNode ConvertTreeToDList(TreeNode root)
        {
            if (root == null)
            {
                return null;
            }

            var headAndTail = ConvertTreeToDListRecur(root);

            headAndTail[1].Right = headAndTail[0];

            return headAndTail[0];
        }

        private void AgeCountRecur(int[] nums, int[] count, int begin, int end)
        {
            if (nums[begin] == nums[end])
            {
                count[nums[begin]] += end - begin + 1;
                return;
            }

            int mid = (begin + end) / 2;
            AgeCountRecur(nums, count, begin, mid);
            AgeCountRecur(nums, count, mid + 1, end);
        }

        // O(mlogn) where m is the distinct number of ages, and m will at most be constant
        public int[] AgeCount(int[] nums)
        {
            int[] count = new int[nums[nums.Length - 1] + 1];

            AgeCountRecur(nums, count, 0, nums.Length - 1);

            return count;
        }

        /// <summary>
        /// HackerRank
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int Evaluate(string expression)
        {
            var stack = new Stack<int>();

            int result = 0;
            int number = 0;
            int sign = 1;
            
            foreach (var c in expression)
            {
                if (char.IsDigit(c))
                {
                    number = 10 * number + (c - '0');
                }
                else if (c == '+' || c == '-')
                {
                    result += sign * number;
                    number = 0;
                    sign = c == '+' ? 1 : -1;
                }
                else if (c == '(')
                {
                    stack.Push(result);
                    stack.Push(sign);
                    // Reset the sign and result for the value in the parenthesis
                    sign = 1;
                    result = 0;
                }
                else if (c == ')')
                {
                    result += sign * number;
                    number = 0;
                    result *= stack.Pop();   // stack.pop() is the sign before the parenthesis
                    result += stack.Pop();   // stack.pop() now is the result calculated before the parenthesis
                }
            }

            return number != 0 ? result + sign * number : result;
        }

        public string DoesCircleExists(string commands)
        {
            if (commands.Length == 0)
            {
                return "YES";
            }

            for (int i = 0; i < 2; ++i)
            {
                commands += commands;
            }

            int x = 0;
            int y = 0;
            int xCopy = x;
            int yCopy = y;

            var directionToMovements = new Dictionary<int, int[]>
            {
                {1, new[] {0, 1}},
                {2, new[] {1, 0}},
                {3, new[] {0, -1}},
                {4, new[] {-1, 0}}
            };
            var curDirection = 1;

            for (int i = 0; i < commands.Length; ++i)
            {
                var ch = commands[i];
                switch (ch)
                {
                    case 'R':
                        curDirection += 1;
                        if (curDirection == 5)
                        {
                            curDirection = 1;
                        }
                        break;
                    case 'L':
                        curDirection -= 1;
                        if (curDirection == 0)
                        {
                            curDirection = 4;
                        }
                        break;
                    default:
                        xCopy += directionToMovements[curDirection][0];
                        yCopy += directionToMovements[curDirection][1];
                        break;
                }
            }

            return xCopy == x && yCopy == y ? "YES" : "NO";
        }
    }
}
