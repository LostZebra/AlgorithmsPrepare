using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AlgorithmsPrepareCSharp
{
    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public TreeNode Parent { get; set; }

        public int LeftSum { get; set; }   // The total distance of all nodes on the left subtree to the root
        public int RightSum { get; set; }  // The total distance of all nodes on the right subtree to the root

        public int NodesOnTheLeft { get; set; }   // Number of nodes on the left subtree
        public int NodesOnTheRight { get; set; }  // Number of nodes on the right subtree

        public bool LeftOrRight { get; set; }

        public TreeNode(int data)
        {
            this.Data = data;
        }
    }

    public class MultiChildrenTreeNode
    {
        public int Id { get; private set; }
        public MultiChildrenTreeNode Parent { get; set; }

        public MultiChildrenTreeNode(int id)
        {
            this.Id = id;
        }
    }

    public class HackerRank
    {
        private static readonly HackerRank Hr = new HackerRank();
        
        private HackerRank()
        {
            // Private constructor
        }

        public static HackerRank GetInstance()
        {
            return Hr;
        }

        public static void Insert(TreeNode root, int newVal, ref long total)
        {
            TreeNode parent = null;
            while (root != null)
            {
                parent = root;
                if (newVal <= root.Data)
                {
                    root.LeftOrRight = true; // Going left
                    root.NodesOnTheLeft++;
                    total += root.RightSum;
                    root = root.Left;
                }
                else
                {
                    root.LeftOrRight = false; // Going right
                    root.NodesOnTheRight++;
                    total += root.LeftSum;
                    root = root.Right;
                }
            }

            Debug.Assert(parent != null, "parent != null");   // Assertion, please ignore
            
            TreeNode newInsert = new TreeNode(newVal);
            newInsert.Parent = parent;
            if (newVal <= parent.Data)
            {
                parent.Left = newInsert;
            }
            else
            {
                parent.Right = newInsert;
            }

            // Distance change
            int count = 1;
            TreeNode p = newInsert.Parent;
            while (p != null)
            {
                if (p.LeftOrRight)
                {
                    p.LeftSum += count;
                    total += (long) count * (p.NodesOnTheRight + 1);
                }
                else
                {
                    p.RightSum += count;
                    total += (long) count * (p.NodesOnTheLeft + 1);
                }
                p.LeftOrRight = false; 
                count++;
                p = p.Parent;
            }
        }

        public static void FindAllPairs(MultiChildrenTreeNode[] nodeList, int t, ref int count)
        {
            foreach (var node in nodeList.Where(node => node != null))
            {
                var parentNode = node.Parent;
                while (parentNode != null)
                {
                    if (Math.Abs(parentNode.Id - node.Id) <= t)
                    {
                        ++count;
                    }
                    parentNode = parentNode.Parent;
                }
            }
        }

        public static int BestPalindrome(string sentence)
        {
            int[,] dp = new int[sentence.Length + 1, sentence.Length + 1];
            for (int i = 1; i <= sentence.Length; i++)
            {
                dp[1, i] = 1;
            }

            dp[1, sentence.Length] = 0;

            for (int i = 2; i < sentence.Length; i++)
            {
                for (int j = 0; j < sentence.Length - i + 1; j++)
                {
                    if (sentence[j] == sentence[j + i - 1])
                    {
                        dp[i, j + 1] = 2 + dp[i - 2, j + 2];
                    }
                    else
                    {
                        dp[i, j + 1] = Math.Max(dp[i - 1, j + 1], dp[i - 1, j + 2]);
                    }
                }
            }

            int price = int.MinValue;
            for (int i = 1; i < sentence.Length - 1; i++)
            {
                price = Math.Max(dp[i, 1] * dp[sentence.Length - i, i + 1], price);
            }

            return price;
        }

        public void PrintLcs(int[,] direction, int[] a, int[] b, int i, int j)
        {
            if (i == 0 || j == 0)
            {
                return;
            }

            switch (direction[i, j])
            {
                case 0:
                    PrintLcs(direction, a, b, i - 1, j - 1);
                    Console.Write(a[i - 1]);
                    Console.Write(' ');
                    break;
                case 1:
                    PrintLcs(direction, a, b, i - 1, j);
                    break;
                case 2:
                    PrintLcs(direction, a, b, i, j - 1);
                    break;
            }
        }

        public static void PrintLcs(int[,] direction, string a, string b, int i, int j, ref string ret)
        {
            if (i == 0 || j == 0)
            {
                return;
            }

            switch (direction[i, j])
            {
                case 0:
                    PrintLcs(direction, a, b, i - 1, j - 1, ref ret);
                    ret += a[i - 1];
                    break;
                case 1:
                    PrintLcs(direction, a, b, i - 1, j, ref ret);
                    break;
                case 2:
                    PrintLcs(direction, a, b, i, j - 1, ref ret);
                    break;
            }
        }

        public void FindLcs(int[] a, int[] b)
        {
            int[,] dp = new int[a.Length + 1, b.Length + 1];

            /* This part is not necessary
             */
            for (int i = 0; i <= b.Length; ++i)
            {
                dp[0, i] = 0;
            }
            for (int i = 0; i <= a.Length; ++i)
            {
                dp[i, 0] = 0;
            }

            int[,] direction = new int[a.Length + 1, b.Length + 1];

            for (int i = 1; i <= a.Length; ++i)
            {
                for (int j = 1; j <= b.Length; ++j)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        direction[i, j] = 0;
                    }
                    else
                    {
                        if (dp[i - 1, j] > dp[i, j - 1])
                        {
                            direction[i, j] = 1;
                            dp[i, j] = dp[i - 1, j];
                        }
                        else
                        {
                            direction[i, j] = 2;
                            dp[i, j] = dp[i, j - 1];
                        }
                    }
                }
            }

            // Print a LCS route
            PrintLcs(direction, a, b, a.Length, b.Length);
        }

        public static string FindLcs(string a, string b)
        {
            int[,] dp = new int[a.Length + 1, b.Length + 1];

            /* This part is not necessary
             */
            for (int i = 0; i <= b.Length; ++i)
            {
                dp[0, i] = 0;
            }
            for (int i = 0; i <= a.Length; ++i)
            {
                dp[i, 0] = 0;
            }

            int[,] direction = new int[a.Length + 1, b.Length + 1];

            for (int i = 1; i <= a.Length; ++i)
            {
                for (int j = 1; j <= b.Length; ++j)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        direction[i, j] = 0;
                    }
                    else
                    {
                        if (dp[i - 1, j] > dp[i, j - 1])
                        {
                            direction[i, j] = 1;
                            dp[i, j] = dp[i - 1, j];
                        }
                        else
                        {
                            direction[i, j] = 2;
                            dp[i, j] = dp[i, j - 1];
                        }
                    }
                }
            }

            string ret = string.Empty;
            // Print a LCS route
            PrintLcs(direction, a, b, a.Length, b.Length, ref ret);
            return ret;
        }

        public static string AddStr(string s, string t)
        {
            int i = s.Length - 1;
            int j = t.Length - 1;
            int advance = 0;
            StringBuilder sb = new StringBuilder();
            while (i != -1 && j != -1)
            {
                int res = (int)char.GetNumericValue(s[i--]) + (int)char.GetNumericValue(t[j--]) + advance;
                if (res >= 10)
                {
                    advance = 1;
                    res -= 10;
                }
                else
                {
                    advance = 0;
                }
                sb.Insert(0, res);
            }

            while (i != -1)
            {
                int res = (int)char.GetNumericValue(s[i--]) + advance;
                if (res >= 10)
                {
                    advance = 1;
                    res -= 10;
                }
                else
                {
                    advance = 0;
                }
                sb.Insert(0, res);
            }
            while (j != -1)
            {
                int res = (int)char.GetNumericValue(t[j--]) + advance;
                if (res >= 10)
                {
                    advance = 1;
                    res -= 10;
                }
                else
                {
                    advance = 0;
                }
                sb.Insert(0, res);
            }
            if (advance != 0)
            {
                sb.Insert(0, '1');
            }

            return sb.ToString();
        }

        public static string Multi(string s, char c)
        {
            int advance = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = s.Length - 1; i != -1; --i)
            {
                int res = (int) char.GetNumericValue(s[i]) * (int) char.GetNumericValue(c) + advance;
                sb.Insert(0, res % 10);
                advance = res / 10;
            }
            if (advance != 0)
            {
                sb.Insert(0, advance);
            }

            return sb.ToString();
        }

        public static string Multi(string s, string t)
        {
            List<string> resList = new List<string>();
            for (int i = t.Length - 1; i != -1; --i)
            {
                string localRes = Multi(s, t[i]);
                for (int j = 0; j < t.Length - i - 1; ++j)
                {
                    localRes += '0';
                }
                resList.Add(localRes);
            }

            string res = resList[0];
            if (resList.Count != 1)
            {
                for (int i = 1; i < resList.Count; ++i)
                {
                    res = AddStr(res, resList[i]);
                }
            }

            return res;
        }

        public static void FindNthFi(string start1, string start2, int n)
        {
            for (int i = 2; i < n; ++i)
            {
                string cur = AddStr(Multi(start2, start2), start1);
                start1 = start2;
                start2 = cur;
            }

            Console.WriteLine(start2);
        }

        public static void CanBePalindrome(string s, int i, int j, ref int deletePos, bool hasDeleted)
        {
            if (i >= j)
            {
                return;
            }

            if (s[i] != s[j] && hasDeleted)
            {
                deletePos = -1;
                return;
            }

            if (s[i] != s[j])
            {
                deletePos = i;
                CanBePalindrome(s, i + 1, j, ref deletePos, true);
                if (deletePos != -1)
                {
                    return;
                }
                deletePos = j;
                CanBePalindrome(s, i, j - 1, ref deletePos, true);
                if (deletePos != -1)
                {
                    return;
                }
            }
            if (s[i] == s[j])
            {
                CanBePalindrome(s, i + 1, j - 1, ref deletePos, hasDeleted);
            }
        }

        public static string Rearrange(char[] s)
        {
            if (s.Length == 1)
            {
                return "no answer";
            }

            int exchangePos = s.Length - 2;
            while (exchangePos != -1 && s[exchangePos].CompareTo(s[exchangePos + 1]) > 0)
            {
                exchangePos--;
            }
            if (exchangePos == -1)
            {
                return "no answer";
            }

            int smallestGreaterPos = exchangePos + 1;
            for (int i = exchangePos + 1; i < s.Length; ++i)
            {
                if (s[i].CompareTo(s[exchangePos]) > 0)
                {
                    smallestGreaterPos = i;
                }
                else
                {
                    break;
                }
            }

            // Swap
            char temp = s[exchangePos];
            s[exchangePos] = s[smallestGreaterPos];
            s[smallestGreaterPos] = temp;

            // ReOrder
            char[] firstHalf = new char[exchangePos + 1];
            char[] secondHalf = new char[s.Length - exchangePos - 1];
            Array.Copy(s, firstHalf, exchangePos + 1);
            Array.Copy(s, exchangePos + 1, secondHalf, 0, secondHalf.Length);
            return new string(firstHalf) + new string(secondHalf.OrderBy(ch => ch).ToArray());
        }

        public static int GetChildLen(ref string temp, string s, string t)
        {
            int[,] dp = new int[s.Length + 1, t.Length + 1];
            for (int i = 1; i <= t.Length; ++i)
            {
                dp[0, i] = i;
                dp[i, 0] = i;
            }
            for (int i = 1; i <= s.Length; ++i)
            {
                for (int j = 1; j <= t.Length; ++j)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        temp += s[i - 1];
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + 1;
                    }
                }
            }

            return s.Length - dp[s.Length, t.Length] / 2;
        }

        public static int SubseqOccurrence(string seq, string sub)
        {
            int[,] dp = new int[seq.Length + 1, sub.Length + 1];

            for (int i = 0; i <= seq.Length; ++i)
            {
                for (int j = 0; j <= sub.Length; ++j)
                {
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        if (seq[i - 1] == sub[j - 1])
                        {
                            dp[i, j] += dp[i - 1, j - 1];
                        }
                        dp[i, j] += dp[i - 1, j];
                    }
                }
            }

            return dp[seq.Length, sub.Length];
        }

        public static int LongestIncreasingSub(int[] a)
        {
            /* This approach takes too mach memory
            int[,] max = new int[a.Length + 1, a.Length + 1];
            int[,] dp = new int[a.Length + 1, a.Length + 1];
            dp[a.Length, a.Length] = 1;
            max[a.Length, a.Length] = a[a.Length - 1];
            for (int i = 1; i < a.Length; ++i)
            {
                dp[i, i] = 1;
                max[i, i] = a[i - 1];
                dp[i, i + 1] = a[i] > a[i - 1] ? 2 : 1;
                max[i, i + 1] = a[i] > a[i - 1] ? a[i] : a[i - 1];
            }

            int maxSeq = int.MinValue;

            for (int i = 1; i <= a.Length - 2; ++i)
            {
                for (int j = i + 2; j <= a.Length; ++j)
                {
                    for (int m = i + 1; m < j; ++m)
                    {
                        if (a[j - 1] > max[i, m])
                        {
                            dp[i, j] = Math.Max(dp[i, j], dp[i, m] + 1);
                            max[i, j] = a[j - 1];
                        }
                        else
                        {
                            if (dp[i, m] == dp[i, j])
                            {
                                max[i, j] = Math.Min(max[i, m], max[i, j]);
                            }
                        }
                        maxSeq = Math.Max(maxSeq, dp[i, j]);
                    }
                }
            }
            */
            int maxSeq = 0;
            int[] dp = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                dp[i] = 1;
                int maxSub = 0;
                for (int j = 0; j < i; j++)
                {
                    if (a[j] < a[i] && dp[j] > maxSub)
                    {
                        maxSub = dp[j];
                    }
                }
                dp[i] = dp[i] + maxSub;
                maxSeq = Math.Max(maxSeq, dp[i]);
            }

            return maxSeq;
        }

        public static string MinStrWindow(string ori, string window)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (char ch in window)
            {
                if (!dict.ContainsKey(ch))
                {
                    dict.Add(ch, 1);
                }
                else
                {
                    dict[ch]++;
                }
            }

            int beginLoc = 0;
            int minWindowLen = int.MaxValue;
            int index;
            int count = 0;
            string minWindowStr = string.Empty;
            
            for (index = 0; index < ori.Length; ++index)
            {
                char ch = ori[index];
                if (dict.ContainsKey(ch))
                {
                    dict[ch]--;
                    if (dict[ch] >= 0)
                    {
                        count++;
                    }
                    while (count == window.Length)
                    {
                        if (dict.ContainsKey(ori[beginLoc]))
                        {
                            dict[ori[beginLoc]]++;
                            if (dict[ori[beginLoc]] > 0)
                            {
                                if (index - beginLoc + 1 < minWindowLen)
                                {
                                    minWindowLen = index - beginLoc + 1;
                                    minWindowStr = ori.Substring(beginLoc, minWindowLen);
                                }
                                --count;
                            }
                        }
                        ++beginLoc;
                    }
                }
            }

            return minWindowStr;
        }

        public static long MaxDiffSum(int[] b)
        {
            long[,] dp = new long[b.Length, 2];
            for (int i = 1; i < b.Length; ++i)
            {
                int[] tempCur = {1, b[i]};
                int[] tempPrev = {1, b[i - 1]};
                for (int j = 0; j <= 1; ++j)
                {
                    long maxDiff = 0;
                    for (int k = 0; k <= 1; ++k)
                    {
                        maxDiff = Math.Max(maxDiff, dp[i - 1, k] + Math.Abs(tempCur[j] - tempPrev[k]));
                    }
                    dp[i, j] = maxDiff;
                }
            }

            return Math.Max(dp[b.Length - 1, 0], dp[b.Length - 1, 1]);
        }

        public static void AngryChildren2(long[] packets, int k)
        {
            Array.Sort(packets);
            long[] sum = new long[packets.Length + 1];
            for (int i = 1; i < sum.Length; ++i)
            {
                sum[i] = sum[i - 1] + packets[i - 1];
            }

            long minDiff = 0;
            for (int i = 1; i <= k - 1; ++i)
            {
                minDiff += packets[i] * i - sum[i];
            }

            long curMinDiff = minDiff;
            for (int i = 1; i <= packets.Length - k; ++i)
            {
                curMinDiff += (k - 1) * (packets[i + k - 1] + packets[i - 1]) - 2 * (sum[i + k - 1] - sum[i]);
                minDiff = Math.Min(curMinDiff, minDiff);
            }

            Console.WriteLine(minDiff);
        }

        public static void DoNothing()
        {
            Debug.WriteLine("Do nothing");
        }
    }
}
