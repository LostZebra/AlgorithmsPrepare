using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    internal class Program
    {
        /*
         * LeetCode: Reverse Integer
         */

        public static int Reverse(int x)
        {
            if (x == 0) return x;
            bool isNegative = x < 0;
            long tempInt = (isNegative) ? -(long) x : x;
            string intToStr = string.Empty;
            while (tempInt != 0)
            {
                intToStr += (tempInt%10).ToString();
                tempInt /= 10;
            }
            int i = 0;
            while (intToStr[i] == '0')
            {
                ++i;
            }
            long total = 0;
            while (i < intToStr.Length)
            {
                total += (long) (Math.Pow(10, intToStr.Length - i - 1)*char.GetNumericValue(intToStr[i]));
                ++i;
            }
            if (total - int.MaxValue >= 0 && !isNegative) return 0;
            if (total + (-int.MaxValue - 1) >= 0 && isNegative) return 0;
            return (int) ((isNegative) ? -total : total);
        }

        /*
         * 编程之美：求区间内频率最小值
         */

        public static int MinimumFrequency(int[] ori, int l, int r)
        {
            int[] count = new int[ori.Length];
            int[] value = new int[ori.Length];
            int[] numInSection = new int[ori.Length];
            int[] left = new int[ori.Length];
            int[] right = new int[ori.Length];

            int numOfOccur = 1;
            int indexOfSection = 0;
            for (int i = 1; i < ori.Length; ++i)
            {
                if (ori[i] == ori[i - 1])
                {
                    ++numOfOccur;
                }
                else
                {
                    count[indexOfSection] = numOfOccur;
                    value[indexOfSection++] = ori[i - 1];
                    numOfOccur = 1;
                }
            }
            count[indexOfSection] = numOfOccur;
            value[indexOfSection] = ori[ori.Length - 1];

            numInSection[0] = 0;
            left[0] = 0;
            right[0] = count[0] - 1;
            for (int i = 1; i < ori.Length; ++i)
            {
                if (right[i - 1] >= i)
                {
                    numInSection[i] = numInSection[i - 1];
                    left[i] = left[i - 1];
                    right[i] = right[i - 1];
                }
                else
                {
                    numInSection[i] = numInSection[i - 1] + 1;
                    left[i] = right[i - 1] + 1;
                    right[i] = left[i] + count[numInSection[i]] - 1;
                }
            }

            if (numInSection[l] == numInSection[r])
            {
                return r - l + 1;
            }

            int leftFrequency = right[l] - l + 1;
            int rightFrequency = r - left[r] + 1;
            Rmq rmq = new Rmq(count);
            int midFrequency = rmq.GetSmallestInRange(numInSection[l] + 1, numInSection[r] - 1);

            return Math.Min(Math.Min(leftFrequency, rightFrequency), midFrequency);
        }

        public static bool SearchForNode(Tree<int> root, int sum)
        {
            if (root == null)
            {
                return false;
            }
            if (sum == root.Data)
            {
                return true;
            }

            if (sum < root.Data)
            {
                return SearchForNode(root.Left, sum);
            }
            return SearchForNode(root.Right, sum);
        }

        public static void FindRange(ref int upperBound, ref int lowerBound, int d, int k)
        {
            bool isUpperBoundReached = false;
            bool isLowerBoundReached = false;
            int count = 0;
            for (int i = 1; !isUpperBoundReached; ++i)
            {
                int temp = i;
                while (temp != 0)
                {
                    count += (temp%10 == d) ? 1 : 0;
                    temp /= 10;
                    if (count == k && !isLowerBoundReached)
                    {
                        lowerBound = i;
                        isLowerBoundReached = true;
                    }
                    else if (count > k)
                    {
                        isUpperBoundReached = true;
                        upperBound = i - 1;
                        break;
                    }
                }
            }
        }

        public static string GetResult(int numerator, int denominator)
        {
            string result = string.Empty;

            // Get the integral-part representation
            result += (numerator/denominator).ToString();
            result += ".";
            // Already mods 0
            if (numerator == 0)
            {
                result += "(0)";
                return result;
            }

            // Get the decimal part
            Dictionary<int, int> firstModValToPos = new Dictionary<int, int>();
            while (true)
            {
                while (numerator/denominator == 0)
                {
                    numerator *= 10;
                }

                result += (numerator/denominator).ToString();
                numerator %= denominator;
                if (!firstModValToPos.ContainsKey(numerator))
                {
                    firstModValToPos[numerator] = result.Length - 1;
                }
                else
                {
                    result = result.Remove(result.Length - 1);
                    result = result.Substring(0, firstModValToPos[numerator]) + "(" +
                             result.Substring(firstModValToPos[numerator]) + ")";
                    return result;
                }
                if (numerator == 0)
                {
                    result += "(0)";
                    return result;
                }
            }
        }

        public static int FindNthElementInArray(int[] a, int startIndexA, int endIndexA, int[] b, int startIndexB,
            int endIndexB, int n)
        {
            if (endIndexB - startIndexB < endIndexA - startIndexA)
            {
                return FindNthElementInArray(b, startIndexB, endIndexB, a, startIndexA, endIndexA, n);
            }
            if (endIndexA < startIndexA)
            {
                return b[startIndexB + n - 1];
            }
            if (n == 1)
            {
                return a[startIndexA] < b[startIndexB] ? a[startIndexA] : b[startIndexB];
            }

            int midA = Math.Min(n/2, endIndexA - startIndexA + 1);
            int midB = n - midA;
            if (a[startIndexA + midA - 1] == b[startIndexB + midB - 1])
            {
                return a[startIndexA + midA - 1];
            }
            if (a[startIndexA + midA - 1] < b[startIndexB + midB - 1])
            {
                return FindNthElementInArray(a, startIndexA + midA, endIndexA, b, startIndexB, endIndexB, n - midA);
            }
            return FindNthElementInArray(a, startIndexA, endIndexA, b, startIndexB + midB, endIndexB, n - midB);
        }

        public static void PrintTopN(string startPoint, int n)
        {
            if (startPoint.Length != 0 && int.Parse(startPoint) > n)
            {
                return;
            }

            if (startPoint.Length == 0)
            {
                for (int i = 1; i < 10; ++i)
                {
                    PrintTopN(startPoint + i, n);
                }
            }
            else
            {
                Console.WriteLine(startPoint);  // Current number is not empty, print it out
                for (int i = 0; i < 10; ++i)
                {
                    PrintTopN(startPoint + i, n);
                }
            }
        }

        internal class Step
        {
            public string Start { get; private set; }
            public string Finish { get; private set; }

            public Step(string start, string finish)
            {
                this.Start = start;
                this.Finish = finish;
            }
        }

        internal class Node
        {
            public string Id { get; private set; }
            public Node Next { get; set; }

            public Node (string id)
            {
                this.Id = id;
            }
        }

        public static List<string> FindPath(List<Step> steps)
        {
            Dictionary<string, Node> idToStep = new Dictionary<string, Node>();
            foreach (var step in steps)
            {
                if (idToStep.ContainsKey(step.Start))
                {
                    if (idToStep.ContainsKey(step.Finish))
                    {
                        idToStep[step.Start].Next = idToStep[step.Finish];
                    }
                    else
                    {
                        Node newFinish = new Node(step.Finish);
                        idToStep[step.Start].Next = newFinish;
                        idToStep.Add(newFinish.Id, newFinish);
                    }
                }
                else
                {
                    Node newNode = new Node(step.Start);
                    idToStep.Add(newNode.Id, newNode);
                    if (idToStep.ContainsKey(step.Finish))
                    {
                        idToStep[newNode.Id].Next = idToStep[step.Finish];
                    }
                    else
                    {
                        Node newFinish = new Node(step.Finish);
                        idToStep[step.Start].Next = newFinish;
                        idToStep.Add(newFinish.Id, newFinish);
                    }
                }
            }

            List<string> route = new List<string>();
            foreach (var key in idToStep.Keys)
            {
                var start = idToStep[key];
                while (start != null)
                {
                    route.Add(start.Id);
                    start = start.Next;
                }
                if (route.Count == 4)
                {
                    return route;
                }
                route.Clear();
            }

            return route;
        }

        public static string TrimSentence(string ori)
        {
            int i = 0;

            while (i < ori.Length)
            {
                char ch = ori[i];
                if (ch == ' ')
                {
                    ori = ori.Remove(i, 1);
                    i++;
                }
                else
                {
                    break;
                }
            }

            while (i < ori.Length)
            {
                char ch = ori[i];
                if (ch == ' ' && ori[i - 1] != ' ')
                {
                    i++;
                }
                else if (ch == ' ' && ori[i - 1] == ' ')
                {
                    ori = ori.Remove(i, 1);
                }
                else
                {
                    i++;
                }
            }

            return ori;
        }

        public static long Pow(int a, int b)
        {
            long total = 1;
            while (b > 0)
            {
                int tempCount = 1;
                int tempTotal = a;
                while (tempCount < b >> 1)
                {
                    tempTotal *= tempTotal;
                    tempCount <<= 1;
                }
                b -= tempCount;
                total *= tempTotal;
            }
            return total;
        }

        public static void MergeWaveSort(ref bool isDec, int[] a, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int mid = (start + end) / 2;
            MergeWaveSort(ref isDec, a, start, mid);
            MergeWaveSort(ref isDec, a, mid + 1, end);

            // Merge as wave
            int i = start;
            int j = mid + 1;
            int k = 0;
            int[] temp = new int[end - start + 1];
            while (i <= mid && j <= end)
            {
                if (isDec)
                {
                    temp[k++] = a[i] >= a[j] ? a[i++] : a[j++];
                }
                else
                {
                    temp[k++] = a[i] <= a[j] ? a[i++] : a[j++];
                }
                isDec = !isDec;
            }
            while (i <= mid)
            {
                temp[k++] = a[i++];
            }
            while (j <= end)
            {
                temp[k++] = a[j++];
            }
            temp.CopyTo(a, start);
        }

        /*
         * 程序验证入口
         */
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
        }
    }
}
