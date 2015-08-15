using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AlgorithmsPrepareCSharp
{
    public class Point
    {
        public int X;
        public int Y;

        public Point(int col, int row)
        {
            this.X = col;
            this.Y = row;
        }
    }

    public class RandomListNode
    {
        public int Label;
        public RandomListNode Next, Random;

        public RandomListNode(int col)
        {
            this.Label = col;
        }
    }

    public class ListNode
    {
        public int Val;
        public ListNode Next;

        public ListNode(int val)
        {
            this.Val = val;
        }
    }

    public class Interval
    {
        public int Start;
        public int End;

        public Interval()
        {
            Start = End = 0;
        }

        public Interval(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }
    }

    public class LeetCode
    {
        public static readonly Lazy<LeetCode> Lc = new Lazy<LeetCode>();

        public static LeetCode CreateInstance()
        {
            return Lc.Value;
        }

        /// <summary>
        /// LeetCode: Two Sum
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Tuple<int, int> TwoSum(int[] numbers, int target)
        {
            if (numbers.Length == 0)
            {
                return null;
            }

            var dict = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Length; ++i)
            {
                if (dict.ContainsKey(target - numbers[i]))
                {
                    return new Tuple<int, int>(dict[target - numbers[i]], i + 1);
                }
                if (!dict.ContainsKey(numbers[i]))
                {
                    dict.Add(numbers[i], i + 1);
                }
            }

            return null;
        }

        private int GetKthElement(int[] a, int startA, int endA, int[] b, int startB, int endB, int k)
        {
            if (endA - startA > endB - startB)
            {
                return GetKthElement(b, startB, endB, a, startA, endA, k);
            }
            if (endA < startA)
            {
                return b[startB + k - 1];
            }
            if (k == 1)
            {
                return Math.Min(a[startA], b[startB]);
            }

            // Now for the real case
            int indexA = Math.Min(endA - startA + 1, k/2);
            int indexB = k - indexA;
            if (a[startA + indexA - 1] < b[startB + indexB - 1])
            {
                return GetKthElement(a, startA + indexA, endA, b, startB, endB, k - indexA);
            }
            return a[startA + indexA] > b[startB + indexB]
                ? GetKthElement(a, startA, endA, b, startB + indexB, endB, k - indexB)
                : a[startA + indexA - 1];
        }

        /// <summary>
        /// LeetCode: Merge Intervals
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            if (intervals.Count <= 1)
            {
                return intervals;
            }

            intervals = intervals.OrderBy(interval => interval.Start).ToList();

            var mergedIntervals = new List<Interval>();

            int i = 1;
            while (i < intervals.Count)
            {
                if (intervals[i].Start <= intervals[i - 1].End)
                {
                    intervals[i].Start = intervals[i - 1].Start;
                    intervals[i].End = Math.Max(intervals[i - 1].End, intervals[i].End);
                }
                else
                {
                    mergedIntervals.Add(intervals[i - 1]);
                }
                if (i == intervals.Count - 1)
                {
                    mergedIntervals.Add(intervals[i]);
                }
                ++i;
            }

            return mergedIntervals;
        }

        /// <summary>
        /// LeetCode: Insert Interval
        /// </summary>
        /// <param name="intervals"></param>
        /// <param name="newInterval"></param>
        /// <returns></returns>
        public IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
        {
            intervals = Merge(intervals);

            var insertedIntervals = new List<Interval>();

            int i = 0;
            while (newInterval.Start > intervals[i].End)
            {
                insertedIntervals.Add(intervals[i++]);
            }

            if (i == intervals.Count)
            {
                insertedIntervals.Add(newInterval);
                return insertedIntervals;
            }

            if (newInterval.End >= intervals[i].Start)
            {
                newInterval.Start = Math.Min(newInterval.Start, intervals[i].Start);
            }

            int j = i;
            while (j < intervals.Count && newInterval.End >= intervals[j].Start)
            {
                j++;
            }
            if (j != 0)
            {
                newInterval.End = Math.Max(newInterval.End, intervals[j - 1].End);
            }

            insertedIntervals.Add(newInterval);

            for (i = j; i < intervals.Count; ++i)
            {
                insertedIntervals.Add(intervals[i]);
            }

            return insertedIntervals;
        }

        /// <summary>
        /// LeetCode: Median of Two Sorted Arrays
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double FindMedianSortedArrays(int[] a, int[] b)
        {
            int totalLen = a.Length + b.Length;
            if (totalLen%2 == 0)
            {
                return 0.5*
                       (GetKthElement(a, 0, a.Length - 1, b, 0, b.Length - 1, totalLen/2) +
                        GetKthElement(a, 0, a.Length - 1, b, 0, b.Length - 1, totalLen/2 + 1));
            }
            return GetKthElement(a, 0, a.Length - 1, b, 0, b.Length - 1, totalLen/2 + 1);
        }

        /// <summary>
        /// LeetCode: Reverse Bits
        /// </summary>
        /// <param name="strLen"></param>
        /// <returns></returns>
        public uint ReverseBit(uint strLen)
        {
            int i = 0;
            int j = 31;
            while (i < j)
            {
                bool posI = (strLen & (1 << i)) != 0;
                bool posJ = (strLen & (1 << j)) != 0;
                if (posI)
                {
                    strLen |= (uint) (1 << j);
                }
                else
                {
                    strLen &= ~(uint) (1 << j);
                }
                if (posJ)
                {
                    strLen |= (uint) (1 << i);
                }
                else
                {
                    strLen &= ~(uint) (1 << i);
                }
                ++i;
                --j;
            }
            return strLen;
        }

        private void FindList(List<int> totalList, int curRes, int curIndex, bool downwards)
        {
            if (curIndex == -1)
            {
                totalList.Add(curRes);
                return;
            }

            if (!downwards)
            {
                for (int i = 0; i <= 1; ++i)
                {
                    if (i == 0)
                    {
                        FindList(totalList, curRes & ~(1 << curIndex), curIndex - 1, false);
                    }
                    else
                    {
                        FindList(totalList, curRes | (1 << curIndex), curIndex - 1, true);
                    }
                }
            }
            else
            {
                for (int i = 1; i >= 0; --i)
                {
                    if (i == 1)
                    {
                        FindList(totalList, curRes | (1 << curIndex), curIndex - 1, false);
                    }
                    else
                    {
                        FindList(totalList, curRes & ~(1 << curIndex), curIndex - 1, true);
                    }
                }
            }
        }

        /// <summary>
        /// LeetCode: Gray Code
        /// </summary>
        /// <param name="strLen"></param>
        /// <returns></returns>
        public IList<int> GrayCode(int strLen)
        {
            if (strLen == 0)
            {
                return new List<int> {0};
            }

            List<int> totalList = new List<int>();
            FindList(totalList, 0, strLen - 1, false);

            return totalList;
        }

        /// <summary>
        /// LeetCode: Maximum Gap
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int MaximumGap(int[] num)
        {
            if (num.Length < 2)
            {
                return 0;
            }
            if (num.Length == 2)
            {
                return Math.Abs(num[1] - num[0]);
            }

            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (int i in num)
            {
                if (i < min)
                {
                    min = i;
                }
                if (i > max)
                {
                    max = i;
                }
            }

            int lenOfBucket = (max - min - 1)/(num.Length - 1) + 1; // ceiling[(max - min) / (len - 1)]
            int numOfBucket = (max - min)/lenOfBucket + 1; // Max number of buckets
            int[] minBuckets = Enumerable.Repeat(int.MaxValue, numOfBucket).ToArray();
            int[] maxBuckets = Enumerable.Repeat(int.MinValue, numOfBucket).ToArray();

            foreach (int i in num)
            {
                int bucketNum = (i - min)/lenOfBucket;
                if (i < minBuckets[bucketNum])
                {
                    minBuckets[bucketNum] = i;
                }
                if (i > maxBuckets[bucketNum])
                {
                    maxBuckets[bucketNum] = i;
                }
            }

            int maxGap = int.MinValue;
            int prevBucket = int.MinValue;

            for (int i = 0; i < numOfBucket; ++i)
            {
                if (minBuckets[i] == int.MaxValue && maxBuckets[i] == int.MinValue)
                {
                    continue;
                }

                if (prevBucket == int.MinValue)
                {
                    prevBucket = maxBuckets[i];
                    continue;
                }

                maxGap = Math.Max(maxGap, minBuckets[i] - prevBucket);
                prevBucket = maxBuckets[i];
            }

            return maxGap;
        }

        /// <summary>
        /// LeetCode: Find Minimum in Rotated Sorted Array
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int FindMin(int[] num)
        {
            if (num.Length == 1)
            {
                return num[0];
            }

            int start = 0;
            int end = num.Length - 1;
            int rL = num[0];
            int oR = num[num.Length - 1];

            while (start <= end)
            {
                int mid = (start + end)/2;
                if (num[mid] <= oR)
                {
                    end = mid - 1;
                    continue;
                }
                if (num[mid] >= rL)
                {
                    start = mid + 1;
                }
            }

            return start > num.Length - 1 ? num[0] : num[start];
        }

        /// <summary>
        /// LeetCode: Max Points on a Line 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public int MaxPoints(IList<Point> points)
        {
            if (points.Count <= 2)
            {
                return points.Count;
            }

            int maxPoints = int.MinValue;

            foreach (Point p in points)
            {
                var slopeToNum = new Dictionary<double, int>();
                int numOfSame = 0;
                int numOfVertical = 1;
                var pCopy = p;
                foreach (Point p2 in points.Where(p2 => p2 != pCopy))
                {
                    if (p.X == p2.X && p.Y == p2.Y)
                    {
                        numOfSame++;
                        continue;
                    }

                    if (p2.X - p.X == 0)
                    {
                        numOfVertical++;
                    }
                    else
                    {
                        double slope = (double) (p2.Y - p.Y)/(p2.X - p.X);
                        if (slopeToNum.ContainsKey(slope))
                        {
                            slopeToNum[slope]++;
                        }
                        else
                        {
                            slopeToNum.Add(slope, 2);
                        }
                    }
                }

                int localMax = 1;
                foreach (var key in slopeToNum.Keys)
                {
                    localMax = Math.Max(slopeToNum[key], localMax);
                }
                maxPoints = Math.Max(localMax + numOfSame, Math.Max(maxPoints, numOfVertical));
            }

            return maxPoints;
        }

        /// <summary>
        /// LeetCode: Repeated DNA Sequences
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            var occurCheck = new HashSet<int>();
            var repeatCheck = new HashSet<int>();
            var resList = new List<string>();

            if (s.Length < 10)
            {
                return resList;
            }

            var map = new Dictionary<char, int> {{'A', 0}, {'C', 1}, {'G', 2}, {'T', 3}};

            int hash = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i < 9)
                {
                    hash = (hash << 2) + map[s[i]];
                }
                else
                {
                    hash = (hash << 2) + map[s[i]];
                    hash &= 0xFFFFF;
                    if (!occurCheck.Contains(hash))
                    {
                        occurCheck.Add(hash);
                    }
                    else if (!repeatCheck.Contains(hash))
                    {
                        resList.Add(s.Substring(i - 9, 10));
                        repeatCheck.Add(hash);
                    }
                }
            }

            return resList;
        }

        /// <summary>
        /// LeetCode: Hamming Weight
        /// </summary>
        /// <param name="strLen"></param>
        /// <returns></returns>
        public int HammingWeight(uint strLen)
        {
            int total = 0;
            while (strLen != 0)
            {
                if ((strLen & 1) != 0)
                {
                    total++;
                }
                strLen >>= 1;
            }

            return total;
        }

        private bool[,] ChangeStatus(bool[,] m, int curRow, int curCol, int maxLine)
        {
            m[curRow, curCol] = true;
            for (int row = curRow + 1; row < maxLine; ++row)
            {
                for (int col = 0; col < maxLine; ++col)
                {
                    if (col == curCol || Math.Abs(curRow - row) == Math.Abs(curCol - col))
                    {
                        m[row, col] = true;
                    }
                }
            }

            return m;
        }

        private void PlaceNext(int curLine, int maxLine, List<string[]> retList, string[] curArray, bool[,] m)
        {
            if (curLine == maxLine)
            {
                retList.Add(curArray.Clone() as string[]);
                return;
            }

            for (int i = 0; i < maxLine; ++i)
            {
                if (!m[curLine, i])
                {
                    var line = new StringBuilder(new string('.', maxLine - 1));
                    line.Insert(i, 'Q');
                    curArray[curLine] = line.ToString();
                    PlaceNext(curLine + 1, maxLine, retList, curArray,
                        ChangeStatus(m.Clone() as bool[,], curLine, i, maxLine));
                    curArray[curLine] = null;
                }
            }
        }

        /// <summary>
        /// LeetCode: strLen-Queens
        /// </summary>
        /// <param name="strLen"></param>
        /// <returns></returns>
        public IList<string[]> SolveNQueens(int strLen)
        {
            var retList = new List<string[]>();
            if (strLen == 0)
            {
                return retList;
            }

            PlaceNext(0, strLen, retList, new string[strLen], new bool[strLen, strLen]);

            return retList;
        }

        /// <summary>
        /// LeetCode: Best Time to Buy and Sell Stock IV
        /// </summary>
        /// <param name="k"></param>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfit(int k, int[] prices)
        {
            if (k == 0 || prices.Length <= 1)
            {
                return 0;
            }
            if (k >= prices.Length)
            {
                int total = 0;
                prices.Aggregate((first, second) =>
                {
                    total += second - first > 0 ? second - first : 0;
                    return second;
                });
                return total;
            }

            int[] benefitBeforeToday = new int[k + 1];
            int[] benefitIncludingToday = new int[k + 1];

            for (int i = 1; i < prices.Length; ++i)
            {
                int sellOnDayI = prices[i] - prices[i - 1];
                for (int j = k; j != 0; --j)
                {
                    benefitIncludingToday[j] = Math.Max(benefitIncludingToday[j] + sellOnDayI,
                        benefitBeforeToday[j - 1] + sellOnDayI > 0 ? sellOnDayI : 0);
                    benefitBeforeToday[j] = Math.Max(benefitBeforeToday[j], benefitIncludingToday[j]);
                }
            }

            return benefitBeforeToday[k];
        }

        private void WordBreakCheck(string s, ISet<string> dict, bool[] exists)
        {
            for (int i = 0; i < s.Length; ++i)
            {
                exists[i] = dict.Contains(s.Substring(0, i + 1));
            }
            for (int i = 1; i < s.Length; ++i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    if (exists[j] && dict.Contains(s.Substring(j + 1, i - j)))
                    {
                        exists[i] = true;
                        break;
                    }
                }
            }
        }

        private void FindAllWordBreak(List<string> list, int curPos, string s, string curStr, ISet<string> dict)
        {
            if (curPos == s.Length)
            {
                list.Add(curStr);
                return;
            }

            for (int i = curPos; i < s.Length; ++i)
            {
                var subStr = s.Substring(curPos, i - curPos + 1);
                if (dict.Contains(subStr))
                {
                    FindAllWordBreak(list, i + 1, s, curStr + (curStr.Length != 0 ? " " : "") + subStr, dict);
                }
            }
        }

        /// <summary>
        /// LeetCode: Word Break II
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public IList<String> WordBreak(string s, ISet<string> dict)
        {
            var list = new List<string>();
            if (dict.Count == 0 || s.Length == 0)
            {
                return list;
            }

            bool[] exists = new bool[s.Length];
            WordBreakCheck(s, dict, exists);
            if (exists[s.Length - 1])
            {
                return list;
            }

            FindAllWordBreak(list, 0, s, "", dict);
            return list;
        }

        /// <summary>
        /// LeetCode: Copy List with Random Pointer
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var oriToNew = new Dictionary<RandomListNode, RandomListNode>();
            var newHead = new RandomListNode(head.Label);
            var prev = head;
            oriToNew.Add(head, newHead);
            var iterNode = head.Next;
            while (iterNode != null)
            {
                var newNode = new RandomListNode(iterNode.Label);
                oriToNew[prev].Next = newNode;
                oriToNew.Add(iterNode, newNode);
                prev = iterNode;
                iterNode = iterNode.Next;
            }

            iterNode = head;
            var newIterNode = newHead;
            while (iterNode != null)
            {
                newIterNode.Random = iterNode.Random != null ? oriToNew[iterNode.Random] : null;
                iterNode = iterNode.Next;
                newIterNode = newIterNode.Next;
            }

            return newHead;
        }

        /// <summary>
        /// LeetCode: Longest Consecutive Sequence
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int LongestConsecutive(int[] num)
        {
            if (num.Length == 0) return 0;

            var set = new HashSet<int>();
            foreach (int i in num)
            {
                set.Add(i);
            }

            int max = 0;
            foreach (int i in num)
            {
                int count = 1;
                int left = i - 1;
                int right = i + 1;
                while (set.Contains(left))
                {
                    count++;
                    set.Remove(left);
                    left--;
                }
                while (set.Contains(right))
                {
                    count++;
                    set.Remove(right);
                    right++;
                }

                if (count > max) max = count;
            }
            return max;
        }

        /// <summary>
        /// LeetCode: Best Time to Buy and Sell Stock III
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfit(int[] prices)
        {
            if (prices.Length <= 1)
            {
                return 0;
            }

            int[,] profitsBeforeToday = new int[prices.Length, 3];
            int[,] profitsIncludingToday = new int[prices.Length, 3];

            for (int i = 1; i < prices.Length; ++i)
            {
                int sellOnDayI = prices[i] - prices[i - 1];
                for (int j = 1; j <= 2; ++j)
                {
                    profitsIncludingToday[i, j] =
                        Math.Max(profitsBeforeToday[i - 1, j - 1] + (sellOnDayI > 0 ? sellOnDayI : 0),
                            profitsIncludingToday[i - 1, j] + sellOnDayI);
                    profitsBeforeToday[i, j] = Math.Max(profitsIncludingToday[i, j], profitsBeforeToday[i - 1, j]);
                }
            }

            return profitsBeforeToday[prices.Length - 1, 2];
        }

        private void ReverseStringBuilderInRange(StringBuilder sb, int start, int end)
        {
            while (start < end)
            {
                var temp = sb[start];
                sb[start] = sb[end];
                sb[end] = temp;
                start++;
                end--;
            }
        }

        /// <summary>
        /// LeetCode: Reverse Words in a String 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReverseWords(string s)
        {
            s = s.Trim();

            if (s.Length <= 1)
            {
                return s;
            }

            var sb = new StringBuilder();

            var newWord = false;
            var beginLoc = 0;

            s += ' ';

            foreach (var ch in s)
            {
                if (ch == ' ')
                {
                    if (newWord)
                    {
                        newWord = false;
                        sb.Append(' ');
                        ReverseStringBuilderInRange(sb, beginLoc, sb.Length - 2);
                    }
                }
                else
                {
                    if (!newWord)
                    {
                        newWord = true;
                        beginLoc = sb.Length;
                    }
                    sb.Append(ch);
                }
            }

            ReverseStringBuilderInRange(sb, 0, sb.Length - 2);
            return sb.ToString().Substring(0, sb.Length - 1);
        }

        /// <summary>
        /// LeetCode: Add Two Numbers
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode newListHead = null;
            ListNode newListPointer = null;

            var l1Head = l1;
            var l2Head = l2;
            int advance = 0;

            while (l1Head != null && l2Head != null)
            {
                int total = advance + l1Head.Val + l2Head.Val;
                if (total >= 10)
                {
                    total -= 10;
                    advance = 1;
                }
                else
                {
                    advance = 0;
                }
                var newNode = new ListNode(total);
                if (newListHead == null)
                {
                    newListHead = newListPointer = newNode;
                }
                else
                {
                    newListPointer.Next = newNode;
                    newListPointer = newListPointer.Next;
                }
                l1Head = l1Head.Next;
                l2Head = l2Head.Next;
            }

            Debug.Assert(newListPointer != null, "Null pointer exception!");

            while (l1Head != null)
            {
                int total = advance + l1Head.Val;
                if (total >= 10)
                {
                    total -= 10;
                    advance = 1;
                }
                else
                {
                    advance = 0;
                }
                newListPointer.Next = new ListNode(total);
                newListPointer = newListPointer.Next;
                l1Head = l1Head.Next;
            }
            while (l2Head != null)
            {
                int total = advance + l2Head.Val;
                if (total >= 10)
                {
                    total -= 10;
                    advance = 1;
                }
                else
                {
                    advance = 0;
                }
                newListPointer.Next = new ListNode(total);
                newListPointer = newListPointer.Next;
                l2Head = l2Head.Next;
            }

            return newListHead;
        }

        /// <summary>
        /// LeetCode: Longest Substring Without Repeating Characters 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring(string s)
        {
            int sLen = s.Length;
            if (sLen <= 1)
            {
                return sLen;
            }

            int start = 0;
            int maxLen = int.MinValue;

            var dic = new Dictionary<char, int>();

            int i = 0;
            while (i < sLen)
            {
                var ch = s[i];
                if (!dic.ContainsKey(ch))
                {
                    dic.Add(ch, i);
                }
                else if (dic[ch] < start)
                {
                    dic[ch] = i;
                }
                else
                {
                    int tempLen = i - start;
                    if (tempLen > maxLen)
                    {
                        maxLen = tempLen;
                    }
                    start = dic[ch] + 1;
                    dic[ch] = i;
                }
                i++;
            }

            return Math.Max(maxLen, sLen - start);
        }

        /// <summary>
        /// LeetCode: Longest Palindromic Substring 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string LongestPalindrome(string s)
        {
            int strLen = s.Length;
            if (strLen <= 1)
            {
                return s;
            }

            int maxLen = 1;
            int maxStart = 0;
            for (int i = 1; i < strLen; ++i)
            {
                int low = i - 1;
                int high = i;
                while (low >= 0 && high < strLen && s[low] == s[high])
                {
                    if (high - low + 1 > maxLen)
                    {
                        maxLen = high - low + 1;
                        maxStart = low;
                    }
                    low--;
                    high++;
                }

                low = i - 1;
                high = i + 1;
                while (low >= 0 && high < strLen && s[low] == s[high])
                {
                    if (high - low + 1 > maxLen)
                    {
                        maxLen = high - low + 1;
                        maxStart = low;
                    }
                    low--;
                    high++;
                }
            }

            return s.Substring(maxStart, maxLen);
        }

        /// <summary>
        /// LeetCode: Reverse Integer
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public int Reverse(int col)
        {
            if (col == 0)
            {
                return col;
            }

            bool isPos = col > 0;
            var q = new Queue<int>();

            long xCopy = Math.Abs((long) col);
            while (xCopy != 0)
            {
                q.Enqueue((int) (xCopy % 10));
                xCopy /= 10;
            }

            const long limit = (long) int.MaxValue + 1;
            long ret = 0;
            while (q.Count != 0)
            {
                ret += q.Dequeue() * (long) Math.Pow(10, q.Count);
                if (ret > limit)
                {
                    return 0;
                }
            }

            if (ret == limit)
            {
                return isPos ? 0 : int.MinValue;
            }

            return isPos ? (int) ret : (int) -ret;
        }

        /// <summary>
        /// LeetCode: String to Integer (atoi)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int Atoi(string str)
        {
            if (str.Length == 0)
            {
                return 0;
            }

            int index = 0;
            while (str[index] == ' ')
            {
                index++;
            }

            int pos = 0;
            while (index < str.Length)
            {
                if (str[index] != '+' && str[index] != '-')
                {
                    if (!char.IsDigit(str[index]))
                    {
                        return 0;
                    }
                    break;
                }
                if ((str[index] == '+' || str[index] == '-') && pos != 0)
                {
                    return 0;
                }

                pos = str[index] == '+' ? 1 : -1;
                index++;
            }

            int endIndex = str.Length - 1;
            while (endIndex >= index)
            {
                if (!char.IsDigit(str[endIndex]))
                {
                    endIndex--;
                }
                else
                {
                    break;
                }
            }

            long ret = 0;
            var tempList = new List<int>();
            while (index <= endIndex)
            {
                if (char.IsDigit(str[index]))
                {
                    tempList.Add((int) char.GetNumericValue(str[index]));
                    index++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < tempList.Count; ++i)
            {
                ret += (long) Math.Pow(10, tempList.Count - i - 1)*tempList[i];
                if (ret < 0)
                {
                    return pos >= 0 ? int.MaxValue : int.MinValue;
                }
            }

            if (pos >= 0 && ret > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (pos < 0 && ret > (long) int.MaxValue + 1)
            {
                return int.MinValue;
            }

            return pos >= 0 ? (int) ret : (int) -ret;
        }

        /// <summary>
        /// LeetCode: Palindrome Number
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public Boolean IsPalindrome(int col)
        {
            if (col == 0)
            {
                return true;
            }
            if (col < 0)
            {
                return false;
            }

            int digits = 0;
            int xCopy = col;
            while (xCopy != 0)
            {
                digits++;
                xCopy /= 10;
            }

            int s = 0;
            int e = digits - 1;
            while (s < e)
            {
                if (col/(int) Math.Pow(10, digits - s - 1)%10 == col/(int) Math.Pow(10, digits - e - 1)%10)
                {
                    s++;
                    e--;
                }
                else
                {
                    break;
                }
            }

            return s >= e;
        }

        /// <summary>
        /// LeetCode: Longest Common Prefix
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return string.Empty;
            }
            if (strs.Length == 1)
            {
                return strs[0];
            }

            string prefix = strs[0];
            for (int i = 1; i < strs.Length; ++i)
            {
                int limit = Math.Min(prefix.Length, strs[i].Length);
                int j = 0;
                while (j < limit && prefix[j] == strs[i][j])
                {
                    j++;
                }
                if (j == prefix.Length)
                {
                    continue;
                }
                prefix = prefix.Substring(0, j);
            }

            return prefix;
        }

        /// <summary>
        /// LeetCode: Container With Most Water
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int MaxArea(IList<int> height)
        {
            if (height.Count == 0)
            {
                return 0;
            }

            int left = 0;
            int right = height.Count - 1;

            int maxArea = 0;

            while (left < right)
            {
                maxArea = Math.Max(maxArea, Math.Min(height[left], height[right])*(right - left));
                if (height[left] <= height[right])
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return maxArea;
        }

        /// <summary>
        /// LeetCode: 3Sum
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<IList<int>> ThreeSum(int[] num)
        {
            var retList = new List<IList<int>>();
            if (num.Length == 0)
            {
                return retList;
            }

            Array.Sort(num);

            for (int i = 0; i < num.Length - 2; ++i)
            {
                if (num[i] > 0)
                {
                    break;
                }
                if (i != 0 && num[i] == num[i - 1])
                {
                    continue;
                }
                for (int j = i + 1; j < num.Length - 1; ++j)
                {
                    if (num[i] + num[j] > 0)
                    {
                        break;
                    }
                    if (j != i + 1 && num[j] == num[j - 1])
                    {
                        continue;
                    }
                    for (int k = j + 1; k < num.Length; ++k)
                    {
                        int total = num[i] + num[j] + num[k];
                        if (total > 0)
                        {
                            break;
                        }
                        if (k != j + 1 && num[k] == num[k - 1])
                        {
                            continue;
                        }
                        if (total == 0)
                        {
                            retList.Add(new List<int> {num[i], num[j], num[k]});
                        }
                    }
                }
            }

            return retList;
        }

        /// <summary>
        /// LeetCode: Swap Nodes in Pairs 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.Next == null)
            {
                return head;
            }

            var fake = new ListNode(-1) {Next = head};
            var cur = head;
            var prev = fake;
            var next = cur.Next;

            while (true)
            {
                var tempNext = next.Next;
                prev.Next = next;
                next.Next = cur;
                cur.Next = null;

                prev = cur;
                cur = tempNext;
                if (cur == null)
                {
                    return fake.Next;
                }
                next = cur.Next;
                if (next == null)
                {
                    prev.Next = cur;
                    return fake.Next;
                }
            }
        }

        /// <summary>
        /// LeetCode: Divide Two Numbers
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public int Divide(int dividend, int divisor)
        {
            if (dividend == 0)
            {
                return 0;
            }
            if (dividend == int.MinValue && divisor == -1)
            {
                return int.MaxValue;
            }

            long result = 0;

            bool pos = dividend > 0 && divisor > 0 || dividend < 0 && divisor < 0;

            long dividendCopy = Math.Abs((long) dividend);
            long divisorCopy = Math.Abs(divisor);

            while (divisorCopy < dividendCopy)
            {
                int tempCount = 1;
                long baseDiv = divisorCopy;
                while (baseDiv < dividendCopy/2)
                {
                    tempCount <<= 1;
                    baseDiv <<= 1;
                }
                result += tempCount;
                dividendCopy -= baseDiv;
            }

            return pos ? (int) result : -(int) result;
        }

        /// <summary>
        /// LeetCode: Next Permutation
        /// </summary>
        /// <param name="num"></param>
        public void NextPermutation(int[] num)
        {
            if (num.Length <= 1)
            {
                return;
            }

            int index = num.Length - 1;
            while (index > 0 && num[index] < num[index - 1])
            {
                index--;
            }

            int exchangePoint = index - 1;

            int minLarge = index;
            for (int i = index; i < num.Length && num[i] > num[exchangePoint]; ++i)
            {
                minLarge = i;
            }

            num.Swap(exchangePoint, minLarge);

            UtilityAlgorithm.QuickSort(num, exchangePoint + 1, num.Length - 1);
        }

        private void SearchRangeRecur(int[] a, int[] ret, int left, int right, int target)
        {
            if (left > right)
            {
                return;
            }

            int mid = (left + right)/2;
            if (a[mid] == target)
            {
                if (mid < ret[0])
                {
                    ret[0] = mid;
                }
                if (mid > ret[1])
                {
                    ret[1] = mid;
                }
                SearchRangeRecur(a, ret, left, mid - 1, target);
                SearchRangeRecur(a, ret, mid + 1, right, target);
            }
            else if (a[mid] < target)
            {
                left = mid + 1;
                SearchRangeRecur(a, ret, left, right, target);
            }
            else
            {
                right = mid - 1;
                SearchRangeRecur(a, ret, left, right, target);
            }
        }

        /// <summary>
        /// LeetCode: Search for a Range
        /// </summary>
        /// <param name="a"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] SearchRange(int[] a, int target)
        {
            int[] ret = {a.Length, -1};

            SearchRangeRecur(a, ret, 0, a.Length - 1, target);

            if (ret[0] == a.Length)
            {
                ret[0] = -1;
            }
            return ret;
        }

        /// <summary>
        /// LeetCode: Search Insert Position
        /// </summary>
        /// <param name="a"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int SearchInsert(int[] a, int target)
        {
            int left = 0;
            int right = a.Length - 1;
            while (left <= right)
            {
                int mid = (left + right)/2;
                if (a[mid] == target)
                {
                    return mid;
                }
                if (a[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return right < 0 ? 0 : left;
        }

        private string Count(string prev)
        {
            var sb = new StringBuilder();

            int count = 1;
            for (int i = 1; i < prev.Length; ++i)
            {
                if (prev[i] == prev[i - 1])
                {
                    count++;
                }
                else
                {
                    sb.Append(count.ToString());
                    sb.Append(prev[i - 1]);
                    count = 1;
                }
            }

            sb.Append(count.ToString());
            sb.Append(prev[prev.Length - 1]);

            return sb.ToString();
        }

        /// <summary>
        /// LeetCode: Count and Say
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public String CountAndSay(int n)
        {
            string prev = "1";
            string next = string.Empty;

            while (n != 1)
            {
                next = Count(prev);
                prev = next;
                n--;
            }

            return next.Length == 0 ? prev : next;
        }

        private void FindAllCombinations(int[] candidates, List<IList<int>> retList, int curPos, int target,
            HashSet<IList<int>> repeatCheck, List<int> tempList)
        {
            if (target < 0)
            {
                return;
            }
            if (target == 0)
            {
                if (!repeatCheck.Contains(tempList))
                {
                    var tempListCopy = new List<int>(tempList);
                    retList.Add(tempListCopy);
                    repeatCheck.Add(tempListCopy);
                }
            }

            for (int i = curPos; i < candidates.Length; ++i)
            {
                tempList.Add(candidates[i]);
                FindAllCombinations(candidates, retList, i, target - candidates[i], repeatCheck, tempList);
                tempList.RemoveAt(tempList.Count - 1);
            }
        }

        /// <summary>
        /// LeetCode: Combination Sum
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var retList = new List<IList<int>>();

            if (candidates.Length == 0)
            {
                return retList;
            }

            Array.Sort(candidates);

            FindAllCombinations(candidates, retList, 0, target, new HashSet<IList<int>>(), new List<int>());

            return retList;
        }

        /// <summary>
        /// LeetCode: Trap Rain Water
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int Trap(int[] a)
        {
            if (a.Length == 0)
            {
                return 0;
            }

            int left = 0;
            int right = a.Length - 1;
            int water = 0;
            int height = Math.Min(a[left], a[right]);

            while (left < right)
            {
                if (a[left] < a[right])
                {
                    water += Math.Max(0, height - a[left + 1]);
                    height = Math.Max(height, Math.Min(a[left + 1], a[right]));
                    left++;
                }
                else
                {
                    water += Math.Max(0, height - a[right - 1]);
                    height = Math.Max(height, Math.Min(a[right - 1], a[left]));
                    right--;
                }
            }

            return water;
        }

        private void Dfs(char[][] grid, int row, int col)
        {
            grid[row][col] = '0';

            var yPos = new Stack<int>();
            var xPos = new Stack<int>();
            yPos.Push(row);
            xPos.Push(col);

            int rows = grid.Length;
            int cols = grid[0].Length;

            int[,] directions =
            {
                {0, 1}, {0, -1}, {1, 0}, {-1, 0}
            };

            while (yPos.Count != 0)
            {
                row = yPos.Pop();
                col = xPos.Pop();
                for (int i = 0; i < 4; ++i)
                {
                    int dRow = row + directions[i, 0];
                    int dCol = col + directions[i, 1];
                    if (dRow != -1 && dRow != rows && dCol != -1 && dCol != cols && grid[dRow][dCol] == '1')
                    {
                        grid[dRow][dCol] = '0';
                        yPos.Push(dRow);
                        xPos.Push(dCol);
                    }
                }
            }
        }

        /// <summary>
        /// LeetCode: Number of Islands
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int NumIslands(char[][] grid)
        {
            int count = 0;
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[0].Length; col++)
                {
                    if (grid[row][col] == '0') continue;
                    Dfs(grid, row, col);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// LeetCode: Bitwise AND of Numbers Range
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public int RangeBitwiseAnd(int m, int n)
        {
            int c = 0;
            while (m != n)
            {
                m >>= 1;
                n >>= 1;
                ++c;
            }

            return n << c;
        }

        /// <summary>
        /// LeetCode: Happy Number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsHappy(int n)
        {
            if (n == 1)
            {
                return true;
            }

            var repeatCheckSet = new HashSet<int> {n};
            var digitsDict = new Dictionary<int, int>();

            while (true)
            {
                while (n != 0)
                {
                    int temp = n%10;
                    if (digitsDict.ContainsKey(temp))
                    {
                        digitsDict[temp]++;
                    }
                    else
                    {
                        digitsDict.Add(temp, 1);
                    }
                    n /= 10;
                }

                foreach (var key in digitsDict.Keys)
                {
                    n += (int) Math.Pow(key, 2)*digitsDict[key];
                }

                if (n == 1)
                {
                    return true;
                }

                if (!repeatCheckSet.Contains(n))
                {
                    repeatCheckSet.Add(n);
                    digitsDict.Clear();
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// LeetCode: Remove Linked List Elements
        /// </summary>
        /// <param name="head"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public ListNode RemoveElements(ListNode head, int val)
        {
            var fakeHead = new ListNode(int.MaxValue) {Next = head};

            var prev = fakeHead;
            var current = head;

            while (current != null)
            {
                if (current.Val != val)
                {
                    prev = current;
                    current = current.Next;
                }
                else
                {
                    prev.Next = current.Next;
                    current = current.Next;
                }
            }

            return fakeHead.Next;
        }

        /// <summary>
        /// Count Primes
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int CountPrimes(int n)
        {
            if (n <= 2)
            {
                return 0;
            }

            bool[] primes = new bool[n];

            int count = 0;

            int limit = (int) Math.Sqrt(n);

            for (int i = 2; i <= limit; i++)
            {
                if (!primes[i])
                {
                    for (int j = (int) Math.Pow(i, 2); j < n; j += i)
                    {
                        primes[j] = true;
                    }
                }
            }

            for (int i = 2; i != n; ++i)
            {
                if (!primes[i])
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// LeetCode: Isomorphic Strings
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsIsomorphic(string s, string t)
        {
            if (s.Length != t.Length)
            {
                return false;
            }

            var dict = new Dictionary<char, char>();
            var targetSet = new HashSet<char>();

            for (int i = 0; i < s.Length; ++i)
            {
                if (dict.ContainsKey(s[i]))
                {
                    if (dict[s[i]] != t[i])
                    {
                        return false;
                    }
                }
                else if (targetSet.Contains(t[i]))
                {
                    return false;
                }
                else
                {
                    dict.Add(s[i], t[i]);
                    targetSet.Add(t[i]);
                }
            }

            return true;
        }

        private bool IsMatchRecur(string s, string p, int i, int j)
        {
            if (j >= p.Length)
            {
                return i >= s.Length;
            }

            if (j == p.Length - 1)
            {
                return i == s.Length - 1 && (p[j] == s[i] || p[j] == '.');
            }

            if (p[j + 1] != '*')
            {
                return i < s.Length && (p[j] == s[i] || p[j] == '.') && IsMatchRecur(s, p, i + 1, j + 1);
            }

            while (i < s.Length && (p[j] == s[i] || p[j] == '.'))
            {
                // If '*' is corresponding to zero preceding element
                if (IsMatchRecur(s, p, i, j + 2))
                {
                    return true;
                }
                i++;
            }

            return IsMatchRecur(s, p, i, j + 2);
        }

        /// <summary>
        /// LeetCode: Regular Expression Matching
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsMatch(string s, string p)
        {
            return IsMatchRecur(s, p, 0, 0);
        }

        private ListNode[] ReverseInRecursion(ListNode root)
        {
            if (root.Next == null)
            {
                return new[] {root, root};
            }

            var reversed = ReverseInRecursion(root.Next);
            reversed[1].Next = root;
            root.Next = null;

            return new[] {reversed[0], root};
        }

        /// <summary>
        /// LeetCode: Reverse Linked List
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            return ReverseInRecursion(head)[0];
        }

        private void DfsForCourse(Dictionary<int, List<int>> courseAndRequ, Dictionary<int, bool> isVisited,
            Dictionary<int, bool> isProcessed,
            Dictionary<int, int> parent, int courseNum, ref bool willTakeAll)
        {
            if (!willTakeAll || !courseAndRequ.ContainsKey(courseNum))
            {
                return;
            }

            isVisited[courseNum] = true;

            // Do process_early here

            foreach (var requ in courseAndRequ[courseNum])
            {
                if (!isVisited[requ])
                {
                    if (parent.ContainsKey(requ))
                    {
                        parent[requ] = courseNum;
                    }
                    else
                    {
                        parent.Add(requ, courseNum);
                    }
                    DfsForCourse(courseAndRequ, isVisited, isProcessed, parent, requ, ref willTakeAll);
                }
                else if (!isProcessed[requ])
                {
                    willTakeAll = false;
                    return;
                }
                if (!willTakeAll)
                {
                    return;
                }
            }

            isProcessed[courseNum] = true;
        }

        private bool TopSortCourses(Dictionary<int, List<int>> courseAndRequ, Dictionary<int, bool> isVisited,
            Dictionary<int, bool> isProcessed)
        {
            var parent = new Dictionary<int, int>();

            bool willTakeAll = true;

            foreach (var courseNum in courseAndRequ.Keys)
            {
                if (!isVisited[courseNum])
                {
                    DfsForCourse(courseAndRequ, isVisited, isProcessed, parent, courseNum, ref willTakeAll);
                    if (!willTakeAll)
                    {
                        break;
                    }
                }
            }

            return willTakeAll;
        }

        /// <summary>
        /// LeetCode: Course Schedule
        /// </summary>
        /// <param name="numCourses"></param>
        /// <param name="prerequisites"></param>
        /// <returns></returns>
        public bool CanFinish(int numCourses, int[,] prerequisites)
        {
            var courseAndRequ = new Dictionary<int, List<int>>();
            var isVisited = new Dictionary<int, bool>();
            var isProcessed = new Dictionary<int, bool>();

            int len = prerequisites.GetLength(0);

            for (int i = 0; i != len; ++i)
            {
                var courseNum = prerequisites[i, 0];
                var requ = prerequisites[i, 1];
                if (!isVisited.ContainsKey(courseNum))
                {
                    isVisited.Add(courseNum, false);
                    isProcessed.Add(courseNum, false);
                }

                if (!isVisited.ContainsKey(requ))
                {
                    isVisited.Add(requ, false);
                    isProcessed.Add(requ, false);
                }

                if (courseAndRequ.ContainsKey(courseNum))
                {
                    courseAndRequ[courseNum].Add(requ);
                }
                else
                {
                    courseAndRequ.Add(courseNum, new List<int> {requ});
                }
            }

            return TopSortCourses(courseAndRequ, isVisited, isProcessed);
        }

        /// <summary>
        /// LeetCode: Minimum Size Subarray Sum
        /// </summary>
        /// <param name="s"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MinSubArrayLen(int s, int[] nums)
        {
            if (nums.Length == 1)
            {
                return nums[0] >= s ? 1 : 0;
            }

            int start = 0;
            int total = 0;
            int i = 0;

            int minLen = int.MaxValue;
            int minDiff = int.MaxValue;

            while (i < nums.Length)
            {
                total += nums[i];
                if (total >= s)
                {
                    if (total - s <= minDiff)
                    {
                        minDiff = total - s;
                    }
                    while (total >= s)
                    {
                        total -= nums[start++];
                    }
                    minLen = Math.Min(i - start + 2, minLen);
                }
                i++;
            }

            return minDiff == int.MaxValue ? 0 : minLen;
        }

        private void DfsBoard(string curStr, char[,] board, int curRow, int curCol, int row, int col, Trie trie,
            HashSet<string> wordsSet, HashSet<string> foundSet, bool[,] isVisited)
        {
            if (curRow == -1 || curRow == row || curCol == -1 || curCol == col || isVisited[curRow, curCol])
            {
                return;
            }

            isVisited[curRow, curCol] = true;
            curStr += board[curRow, curCol];

            if (!trie.StartsWith(curStr))
            {
                isVisited[curRow, curCol] = false;
                return;
            }

            if (wordsSet.Contains(curStr) && !foundSet.Contains(curStr))
            {
                foundSet.Add(curStr);
            }

            DfsBoard(curStr, board, curRow + 1, curCol, row, col, trie, wordsSet, foundSet, isVisited);
            DfsBoard(curStr, board, curRow - 1, curCol, row, col, trie, wordsSet, foundSet, isVisited);
            DfsBoard(curStr, board, curRow, curCol + 1, row, col, trie, wordsSet, foundSet, isVisited);
            DfsBoard(curStr, board, curRow, curCol - 1, row, col, trie, wordsSet, foundSet, isVisited);

            // Exit current cell, mark as unvisited
            isVisited[curRow, curCol] = false;
        }

        /// <summary>
        /// LeetCode: Word Search II
        /// </summary>
        /// <param name="board"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public IList<string> FindWords(char[,] board, string[] words)
        {
            int row = board.GetLength(0);
            int col = board.GetLength(1);

            // Insert all words to the Trie
            Trie trie = new Trie();
            foreach (var str in words)
            {
                trie.Insert(str);
            }

            bool[,] isVisited = new bool[row, col];
            var wordsSet = new HashSet<string>(words);
            var foundSet = new HashSet<string>();

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    DfsBoard(string.Empty, board, i, j, row, col, trie, wordsSet, foundSet, isVisited);
                }
            }

            return foundSet.ToList();
        }

        /// <summary>
        /// LeetCode: House Robber II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Rob(int[] nums)
        {
            int len = nums.Length;
            if (len == 0)
            {
                return 0;
            }

            int[,] dp = new int[2, len];

            for (int i = 0; i < 2; ++i)
            {
                for (int j = i; j < len + i; ++j)
                {
                    dp[i, j%len] = Math.Max(
                        j >= 1 ? dp[i, j - 1] : 0,
                        (j >= 2 ? dp[i, j - 2] : 0) + (dp[i, (j + 1)%len] == 0 ? nums[j%len] : 0));
                }
            }

            return Math.Max(dp[0, len - 1], dp[1, 0]);
        }

        /// <summary>
        /// LeetCode: Shortest Palindrome 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ShortestPalindrome(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            string reversed = new string(chars);

            string str = s + "." + reversed;
            int[] size = new int[str.Length];

            for (int i = 1; i < str.Length; i++)
            {
                int temp = size[i - 1];
                while (temp != 0 && str[temp] != str[i])
                {
                    temp = size[temp - 1];
                }
                temp += str[temp] == str[i] ? 1 : 0;
                size[i] = temp;
            }

            return reversed.Substring(0, s.Length - size[str.Length - 1]) + s;
        }

        private void SwapElementInArray(int[] nums, int index1, int index2)
        {
            int temp = nums[index1];
            nums[index1] = nums[index2];
            nums[index2] = temp;
        }

        /// <summary>
        /// LeetCode: Kth Largest Element in an Array
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int FindKthLargest(int[] nums, int k)
        {
            int startIndex = 0;
            int endIndex = nums.Length - 1;

            while (true)
            {
                // Swap two values
                SwapElementInArray(nums, startIndex, endIndex);
                // Partition
                int firstHigh = startIndex;
                int pivot = nums[endIndex];
                for (int i = startIndex; i < endIndex; ++i)
                {
                    if (nums[i] <= pivot)
                    {
                        SwapElementInArray(nums, firstHigh, i);
                        firstHigh++;
                    }
                }
                SwapElementInArray(nums, firstHigh, endIndex);
                int right = endIndex - firstHigh + 1;
                if (right == k)
                {
                    return nums[firstHigh];
                }
                if (right < k)
                {
                    endIndex -= right;
                    k -= right;
                }
                else
                {
                    startIndex = firstHigh + 1;
                }
            }
        }

        private void FindAllCombinations(List<IList<int>> retList, List<int> tempList, int curIndex, int curSum,
            int last, int totalCount, int sum)
        {
            if (curSum > sum)
            {
                return;
            }

            if (curIndex == totalCount)
            {
                if (curSum != sum) return;
                retList.Add(new List<int>(tempList));
                return;
            }

            for (int i = last; i <= 9; ++i)
            {
                tempList.Add(i);
                FindAllCombinations(retList, tempList, curIndex + 1, curSum + i, i + 1, totalCount, sum);
                tempList.RemoveAt(tempList.Count - 1);
            }
        }

        /// <summary>
        /// LeetCode: Combination Sum III
        /// </summary>
        /// <param name="k"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            var retList = new List<IList<int>>();

            FindAllCombinations(retList, new List<int>(), 0, 0, 1, k, n);
            return retList;
        }

        /// <summary>
        /// LeetCode: Contains Duplicate II
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>();

            bool hasFound = false;
            for (int i = 0; i < nums.Length; ++i)
            {
                if (!dict.ContainsKey(nums[i]))
                {
                    dict.Add(nums[i], i);
                }
                else
                {
                    if (hasFound && i - dict[nums[i]] <= k)
                    {
                        return false;
                    }
                    if (!hasFound && i - dict[nums[i]] <= k)
                    {
                        hasFound = true;
                    }
                    dict[nums[i]] = i;
                }
            }

            return hasFound;
        }

        /// <summary>
        /// LeetCode: Minimum Window Substring
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public string MinWindow(string s, string t)
        {
            if (s.Length == 0 || t.Length == 0 || s.Length < t.Length)
            {
                return string.Empty;
            }

            var dictT = new Dictionary<char, int>();
            foreach (var ch in t)
            {
                if (dictT.ContainsKey(ch))
                {
                    dictT[ch]++;
                }
                else
                {
                    dictT.Add(ch, 1);
                }
            }

            var dictS = new Dictionary<char, int>();
            var setS = new HashSet<char>();
            int beginLoc = 0;
            int minLen = s.Length + 1;
            var minStr = string.Empty;

            for (int i = 0; i < s.Length; ++i)
            {
                var c = s[i];
                if (!dictT.ContainsKey(c)) continue;

                if (!dictS.ContainsKey(c))
                {
                    dictS.Add(c, 1);
                }
                else
                {
                    dictS[c]++;
                }
                if (dictS[c] == dictT[c])
                {
                    setS.Add(c);
                }

                if (setS.Count != dictT.Keys.Count) continue;
                while (beginLoc < s.Length)
                {
                    if (!dictT.ContainsKey(s[beginLoc]))
                    {
                        beginLoc++;
                        continue;
                    }

                    dictS[s[beginLoc]]--;
                    if (dictS[s[beginLoc]] < dictT[s[beginLoc]])
                    {
                        if (i - beginLoc + 1 < minLen)
                        {
                            minLen = i - beginLoc + 1;
                            minStr = s.Substring(beginLoc, minLen);
                        }
                        setS.Remove(s[beginLoc++]);
                        break;
                    }
                    beginLoc++;
                }
            }

            return minStr;
        }

        private void ReverseInRange(ListNode head, ListNode tail, ListNode fakeHead)
        {
            var iterNode = head;
            var endNode = tail.Next;
            while (iterNode != endNode)
            {
                var tmpNext = iterNode.Next;
                iterNode.Next = fakeHead.Next;
                fakeHead.Next = iterNode;
                iterNode = tmpNext;
            }
        }

        /// <summary>
        /// LeetCode: Reverse Nodes in k-Group
        /// </summary>
        /// <param name="head"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null || head.Next == null)
            {
                return head;
            }

            var fakeHead = new ListNode(-1) {Next = head};
            var prev = fakeHead;
            var cur = head;

            while (true)
            {
                ListNode iter = cur;
                int count = 1;
                while (count < k && iter != null)
                {
                    count++;
                    iter = iter.Next;
                }
                if (iter == null) break;
                var tmpNext = iter.Next;
                prev.Next = tmpNext;
                ReverseInRange(cur, iter, prev);
                prev = cur;
                cur = tmpNext;
            }

            return fakeHead.Next;
        }

        /// <summary>
        /// LeetCode: Remove Element
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int RemoveElement(int[] nums, int val)
        {
            int offset = 0;
            for (int i = 0; i < nums.Length; ++i)
            {
                if (nums[i] == val)
                {
                    offset++;
                }
                else if (offset != 0)
                {
                    nums[i - offset] = nums[i];
                }
            }

            return nums.Length - offset;
        }

        //private void BuildIndexArray(string needle, int[] pos)
        //{
        //    for (int i = 1; i < needle.Length; ++i)
        //    {
        //        int j = pos[i - 1];
        //        while (j != 0 && needle[i] != needle[j])
        //        {
        //            j = pos[j - 1];
        //        }
        //        if (needle[i] == needle[j])
        //        {
        //            j++;
        //        }
        //        pos[i] = j;
        //    }
        //}

        /// <summary>
        /// LeetCode: strStr()
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public int StrStr(string haystack, string needle)
        {
            if (needle.Length == 0)
            {
                return 0;
            }

            var limit = haystack.Length - needle.Length + 1;
            for (int i = 0; i < limit; ++i)
            {
                bool isFound = true;
                for (int j = 0; j < needle.Length; ++j)
                {
                    if (haystack[i + j] == needle[j]) continue;

                    isFound = false;
                    break;
                }
                if (isFound)
                {
                    return i;
                }
            }

            return -1;

            //int[] pos = new int[needle.Length];

            //BuildIndexArray(needle, pos);

            //int j = 0;
            //for (int i = 0; i < haystack.Length; ++i)
            //{
            //    while (j != 0 && haystack[i] != needle[j])
            //    {
            //        j = pos[j - 1];
            //    }
            //    if (haystack[i] == needle[j])
            //    {
            //        j++;
            //    }
            //    if (j == needle.Length)
            //    {
            //        return i - needle.Length + 1;
            //    }
            //}

            //return -1;
        }

        private void SearchInsertRecur(int[] a, int target, int left, int right, ref int minPos)
        {
            if (left > right)
            {
                return;
            }

            int mid = (left + right)/2;
            if (a[mid] == target)
            {
                minPos = mid;
                SearchInsertRecur(a, target, left, mid - 1, ref minPos);
            }
            else if (a[mid] < target)
            {
                SearchInsertRecur(a, target, mid + 1, right, ref minPos);
            }
            else
            {
                SearchInsertRecur(a, target, left, mid - 1, ref minPos);
            }
        }

        /// <summary>
        /// LeetCode: Search Insert Position
        /// </summary>
        /// <param name="a"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int SearchInsertWithDuplicates(int[] a, int target)
        {
            if (a.Length == 0)
            {
                return 0;
            }

            int minPos = a.Length + 1;

            SearchInsertRecur(a, target, 0, a.Length - 1, ref minPos);

            return minPos == a.Length + 1 ? target < a[0] ? 0 : a.Length : minPos;
        }

        /// <summary>
        /// LeetCode: Linked List Cycle II
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode DetectCycle(ListNode head)
        {
            if (head == null || head.Next == null)
            {
                return null;
            }

            var iter1 = head;
            var iter2 = head.Next;

            while (iter1 != null && iter2 != null)
            {
                if (iter1 == iter2) break;
                iter1 = iter1.Next;
                if (iter2.Next == null)
                {
                    return null;
                }
                iter2 = iter2.Next.Next;
            }

            if (iter1 == null || iter2 == null)
            {
                return null;
            }

            var fake = new ListNode(-1) {Next = head};
            iter1 = fake;
            while (iter1 != iter2)
            {
                iter1 = iter1.Next;
                iter2 = iter2.Next;
            }

            return iter1;
        }

        private void SwapNodes(ListNode node1, ListNode node2)
        {
            var temp = node1.Val;
            node1.Val = node2.Val;
            node2.Val = temp;
        }

        private void QuickSortList(ListNode head, ListNode end)
        {
            if (end.Next == head || end == head)
            {
                return;
            }

            var firstHigh = head;
            var prev = head;
            for (var iter = head; iter != end; iter = iter.Next)
            {
                if (iter.Val < end.Val)
                {
                    SwapNodes(iter, firstHigh);
                    prev = firstHigh;
                    firstHigh = firstHigh.Next;
                }
            }
            SwapNodes(firstHigh, end);

            QuickSortList(head, prev);
            QuickSortList(firstHigh.Next, end);
        }

        /// <summary>
        /// LeetCode: Sort List (O(nlgn))
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode SortList(ListNode head)
        {
            if (head == null || head.Next == null)
            {
                return head;
            }

            var iter = head;
            while (iter.Next != null)
            {
                iter = iter.Next;
            }

            QuickSortList(head, iter);

            return head;
        }

        private void FindAllPaths(List<IList<int>> retList, List<int> curList, TreeNode curNode, int curSum, int sum)
        {
            if (curNode.Left == null && curNode.Right == null)
            {
                if (curSum == sum)
                {
                    retList.Add(new List<int>(curList));
                }
                return;
            }

            if (curNode.Left != null)
            {
                curList.Add(curNode.Left.Data);
                FindAllPaths(retList, curList, curNode.Left, curSum + curNode.Left.Data, sum);
                curList.RemoveAt(curList.Count - 1);
            }
            if (curNode.Right != null)
            {
                curList.Add(curNode.Right.Data);
                FindAllPaths(retList, curList, curNode.Right, curSum + curNode.Right.Data, sum);
                curList.RemoveAt(curList.Count - 1);
            }
        }

        /// <summary>
        /// LeetCode: Path Sum II
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            var retList = new List<IList<int>>();
            if (root == null || root.Left == null && root.Right == null && root.Data != sum)
            {
                return retList;
            }

            FindAllPaths(retList, new List<int> {root.Data}, root, root.Data, sum);

            return retList;
        }

        /// <summary>
        /// LeetCode: Maximal Square
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public int MaximalSquare(char[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            int[,] rowSum = new int[row, col + 1];
            for (int i = 0; i < row; ++i)
            {
                for (int j = 1; j <= col; ++j)
                {
                    rowSum[i, j] = rowSum[i, j - 1] + (matrix[i, j - 1] == '1' ? 1 : -1000);
                }
            }

            int maxArea = 0;
            for (int j = 1; j <= col; ++j)
            {
                for (int k = 0; k < j; ++k)
                {
                    int tmpSum = 0;
                    int top = 0;
                    for (int i = 0; i < row; ++i)
                    {
                        tmpSum += rowSum[i, j] - rowSum[i, k];
                        if (tmpSum > 0)
                        {
                            int height = i - top + 1;
                            int width = j - k;
                            if (height == width)
                            {
                                maxArea = Math.Max(maxArea, width*width);
                            }
                        }
                        else
                        {
                            tmpSum = 0;
                            top = i + 1;
                        }
                    }
                }
            }

            return maxArea;
        }

        /// <summary>
        /// LeetCode: Invert Binary Tree
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return null;
            }

            var tmp = root.Left;
            root.Left = root.Right;
            root.Right = tmp;

            InvertTree(root.Left);
            InvertTree(root.Right);

            return root;
        }

        /// <summary>
        /// LeetCode: Basic Calculator II
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int Calculate(string s)
        {
            int[] stack = new int[s.Length];
            int maxIndex = -1;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '+' || s[i] == '-' || s[i] == '*' || s[i] == '/')
                {
                    stack[++maxIndex] = s[i];
                }
                else if (s[i] >= '0' && s[i] <= '9')
                {
                    int tmp = 0;
                    while (i < s.Length && s[i] >= '0' && s[i] <= '9')
                    {
                        tmp = tmp*10 + s[i++] - '0';
                    }
                    i--;
                    if (maxIndex > 0)
                    {
                        char tmpCh = (char) stack[maxIndex];
                        if (tmpCh == '*' || tmpCh == '/' || tmpCh == '-')
                        {
                            maxIndex--;
                            switch (tmpCh)
                            {
                                case '*':
                                    tmp *= stack[maxIndex--];
                                    break;
                                case '/':
                                    tmp = stack[maxIndex--]/tmp;
                                    break;
                                case '-':
                                    stack[++maxIndex] = '+';
                                    tmp = -tmp;
                                    break;
                            }
                        }
                    }
                    stack[++maxIndex] = tmp;
                }
            }

            while (maxIndex > 1)
            {
                stack[maxIndex - 2] = stack[maxIndex] + stack[maxIndex - 2];
                maxIndex -= 2;
            }

            return stack[maxIndex];
        }

        /// <summary>
        /// LeetCode: Majority Element II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<int> MajorityElement2(int[] nums)
        {
            var retList = new List<int>();
            int limit = nums.Length/3 + 1;

            int a = 0;
            int b = 0;
            int aCount = 0;
            int bCount = 0;

            foreach (var num in nums)
            {
                if (num == a)
                {
                    aCount++;
                }
                else if (num == b)
                {
                    bCount++;
                }
                else
                {
                    aCount--;
                    bCount--;
                }

                if (aCount < 0)
                {
                    a = num;
                }
                if (bCount < 0)
                {
                    b = num;
                }
            }

            aCount = bCount = 0;
            foreach (var num in nums)
            {
                if (num == a)
                {
                    aCount++;
                }
                if (num == b)
                {
                    bCount++;
                }
            }

            if (aCount >= limit)
            {
                retList.Add(a);
            }
            if (bCount >= limit && b != a)
            {
                retList.Add(b);
            }

            return retList;
        }

        /// <summary>
        /// LeetCode: Majority Element I
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MajorityElement(int[] nums)
        {
            int majority = 0;
            int count = 0;

            foreach (var num in nums)
            {
                if (count == 0)
                {
                    majority = num;
                    count = 1;
                }
                else
                {

                    if (majority == num)
                    {
                        count++;
                    }
                    else
                    {
                        count--;
                    }
                }
            }

            return majority;
        }

        /// <summary>
        /// LeetCode: Kth Smallest Element in a BST
        /// </summary>
        /// <param name="root"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int KthSmallest(TreeNode root, int k)
        {
            var s = new Stack<TreeNode>();
            s.Push(root);

            int count = 0;
            while (s.Count != 0)
            {
                if (root.Left != null)
                {
                    root = root.Left;
                    s.Push(root);
                }
                else
                {
                    var topNode = s.Pop();
                    count++;
                    if (count == k)
                    {
                        return topNode.Data;
                    }
                    if (topNode.Right != null)
                    {
                        root = topNode.Right;
                        s.Push(root);
                    }
                }
            }

            // This part will never be reached.
            return -1;
        }

        /// <summary>
        /// LeetCode: Power of Two
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsPowerOfTwo(int n)
        {
            return n > 0 && (n & (n - 1)) == 0;
            //if (n < 2 || (n & 1) == 1)
            //{
            //    return false;
            //}

            //int dividend = 2;

            //while (dividend < n)
            //{
            //    dividend <<= 1;
            //}

            //return dividend == n;
        }

        /// <summary>
        /// LeetCode: Palindrome Linked List
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public bool IsPalindrome(ListNode head)
        {
            if (head == null)
            {
                return true;
            }

            int count = 1;
            var iter = head;
            while (iter != null)
            {
                iter = iter.Next;
                count++;
            }

            if (count == 1)
            {
                return true;
            }

            int secondHalfCount = count % 2 == 0 ? count / 2 : (count + 1) / 2;

            iter = head;
            count = 1;
            while (count < secondHalfCount)
            {
                iter = iter.Next;
                count++;
            }

            var secondHalf = ReverseList(iter);

            iter = head;
            var secondIter = secondHalf;
            while (secondIter != null)
            {
                if (iter.Val != secondIter.Val) return false;
                iter = iter.Next;
                secondIter = secondIter.Next;
            }

            return true;
        }

        /// <summary>
        /// LeetCode: Lowest Common Ancestor of a Binary Search Tree
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
            {
                return null;
            }

            if (p.Data <= root.Data && q.Data >= root.Data || p.Data >= root.Data && q.Data <= root.Data)
            {
                return root;
            }

            return p.Data < root.Data && q.Data < root.Data
                ? LowestCommonAncestor(root.Left, p, q)
                : LowestCommonAncestor(root.Right, p, q);
        }

        /// <summary>
        /// LeetCode: Number of Digit One 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int CountDigitOne(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            int j = 1;
            int k = n;
            int constant;
            while (k >= 10)
            {
                k /= 10;
                j *= 10;
            }
            if (k > 1)
            {
                constant = j;
            }
            else
            {
                constant = n % j + 1;
            }
            return constant + k * CountDigitOne(j - 1) + CountDigitOne(n % j);
        }

        /// <summary>
        /// LeetCode: Delete Node in a Linked List
        /// </summary>
        /// <param name="node"></param>
        public void DeleteNode(ListNode node)
        {
            if (node == null)
            {
                return;
            }
            if (node.Next == null)
            {
                node = null;
                return;
            }

            node.Val = node.Next.Val;
            node.Next = node.Next.Next;
        }

        /// <summary>
        /// LeetCode: Lowest Common Ancestor of a Binary Tree
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public TreeNode LowestCommonAncestorForBinaryTree(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
            {
                return null;
            }

            if (root == p || root == q)
            {
                return root;
            }

            var left = LowestCommonAncestorForBinaryTree(root.Left, p, q);
            var right = LowestCommonAncestorForBinaryTree(root.Right, p, q);

            return left != null && right != null ? root : left ?? right;
        }

        /// <summary>
        /// LeetCode: Product of Array Except Self
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] res = new int[nums.Length];
            int prev = 1;

            for (int i = 0; i < nums.Length; prev *= nums[i], ++i)
            {
                res[i] = prev;
            }

            prev = 1;

            for (int i = nums.Length - 1; i != -1; prev *= nums[i], --i)
            {
                res[i] *= prev;
            }

            return res;
        }

        private bool BinarySearchOnRow(int[,] matrix, int row, int col, int target)
        {
            int start = 0;
            int end = col - 1;
            
            while (start <= end)
            {
                int mid = (start + end) / 2;
                if (matrix[row, mid] == target)
                {
                    return true;
                }
                if (matrix[row, mid] < target)
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }

            return false;
        }

        /// <summary>
        /// LeetCode: Search a 2D Matrix II
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool SearchMatrix(int[,] matrix, int target)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            if (row == 0)
            {
                return false;
            }

            for (int r = 0; r < row; ++r)
            {
                if (matrix[r, col - 1] < target)
                {
                    continue;
                }
                
                bool found = BinarySearchOnRow(matrix, r, col, target);
                if (found)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// LeetCode: Valid Anagram
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;

            var dic = new Dictionary<char, int>();

            foreach (var ch in s)
            {
                if (!dic.ContainsKey(ch))
                {
                    dic.Add(ch, 1);
                }
                else
                {
                    dic[ch]++;
                }
            }

            foreach (var ch in t)
            {
                if (!dic.ContainsKey(ch) || dic[ch] == 0) return false;
                dic[ch]--;
            }

            return true;
        }

        private int CalExpression(int left, int right, char op)
        {
            switch (op)
            {
                case '+':
                    return left + right;
                case '-':
                    return left - right;
                case '*':
                    return left * right;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// LeetCode: Different Ways to Add Parentheses 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IList<int> DiffWaysToCompute(string input)
        {
            if (input.Length == 0)
            {
                return new List<int>();
            }

            var defaultOperators = new HashSet<char> { '+', '-', '*' };

            var numbers = new List<int>();
            var operators = new List<char>();

            var tempNumber = 0;

            foreach (var ch in input)
            {
                if (defaultOperators.Contains(ch))
                {
                    operators.Add(ch);
                    numbers.Add(tempNumber);
                    tempNumber = 0;
                }
                else
                {
                    tempNumber = tempNumber * 10 + (ch - '0');
                }
            }

            numbers.Add(tempNumber);

            if (numbers.Count == 0)
            {
                return new List<int> { numbers[0] };
            }

            var dp = new List<int>[numbers.Count, numbers.Count];
            for (int i = 0; i < numbers.Count; ++i)
            {
                dp[i, i] = new List<int> {numbers[i]};
            }

            for (int gap = 1; gap < numbers.Count; ++gap)
            {
                for (int i = 0; i < numbers.Count - gap; ++i)
                {
                    for (int t = i; t < i + gap; ++t)
                    {
                        foreach (var leftNum in dp[i, t])
                        {
                            foreach (var rightNum in dp[t + 1, i + gap])
                            {
                                if (dp[i, i + gap] == null) dp[i, i + gap] = new List<int>();
                                dp[i, i + gap].Add(CalExpression(leftNum, rightNum, operators[t]));
                            }
                        }
                    }
                }
            }

            return dp[0, numbers.Count - 1];
        }
    }
}
