using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsPrepareCSharp
{
    public class Tc
    {
        private static readonly Tc TcInstance = new Tc();

        private Tc()
        {
            // Private constructor
        }

        public static Tc GetInstance()
        {
            return TcInstance;
        }

        /*
         * TopCoder: Return the most repeated char in a string
         */
        public char? GetMax(string str)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }

            char[] chars = str.ToCharArray();
            UtilityAlgorithm.QuickSort(chars, 0, chars.Length - 1);
            str = new string(chars);

            char maxLenChar = str[0];
            int maxLength = 1;
            int tempLength = 1;
            for (int i = 1; i < str.Length; ++i)
            {
                if (str[i] == str[i - 1])
                {
                    tempLength++;
                }
                else
                {
                    if (tempLength > maxLength)
                    {
                        maxLength = tempLength;
                        maxLenChar = str[i - 1];
                    }
                    tempLength = 1;
                }
            }
            return maxLenChar;
        }

        /*
         * TopCoder: Get angle of each clock hand given a time represented as a formatted string
         */

        private static int ComputeAngle(int timeComponent, IList<int> timeComponents)
        {
            double[] divider = {12.0, 60.0, 60.0};
            if (timeComponent == timeComponents.Count)
            {
                return 0;
            }

            var percentage = 360*timeComponents[timeComponent]/divider[timeComponent] +
                             1/divider[timeComponent]*ComputeAngle(timeComponent + 1, timeComponents);
            return (int) percentage;
        }

        public int[] GetAngles(string clockString)
        {
            string[] timeComponents = clockString.Split(':');
            int[] timeComponentsAsInt = new int[timeComponents.Length];
            for (int i = 0; i < timeComponents.Length; ++i)
            {
                timeComponentsAsInt[i] = int.Parse(timeComponents[i]);
            }

            for (int i = 0; i < timeComponentsAsInt.Length; ++i)
            {
                timeComponentsAsInt[i] = ComputeAngle(i, timeComponentsAsInt);
            }
            return timeComponentsAsInt;
        }

        /*
         * Return the length of longest strictly increasing sequence
         */

        public int IncreasingLength(int[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return 0;
            }

            if (array.Length == 1)
            {
                return 1;
            }

            int maxLength = 1;
            int tempMaxLength = 1;

            for (int i = 1; i < array.Length; ++i)
            {
                if (array[i] > array[i - 1])
                {
                    tempMaxLength++;
                }
                else
                {
                    if (tempMaxLength > maxLength)
                    {
                        maxLength = tempMaxLength;
                    }
                    tempMaxLength = 1;
                }
            }
            return maxLength;
        }

        /*
         * Get the difficulty level (represented as point) of each problem given their problem statement
         */

        public int PointVal(string problemStatement)
        {
            if (problemStatement.IsNullOrEmpty())
            {
                return 250;
            }

            int totalWordLength = 0;
            int totalWordNum = 0;
            int tempWordLength = 0;

            // Flags
            bool isBeginningOfWord = false;
            bool isStillAWord = false;
            bool isPeriodAppeared = false;

            foreach (char ch in problemStatement)
            {
                if (ch == ' ')
                {
                    if (isStillAWord)
                    {
                        totalWordLength += tempWordLength;
                        totalWordNum++;
                        tempWordLength = 0;
                    }
                    isBeginningOfWord = false;
                    isStillAWord = false;
                    isPeriodAppeared = false;
                    continue;
                }

                if (!isStillAWord && isBeginningOfWord) continue;

                // Indicate the it's the beginning of the word
                if (!isBeginningOfWord)
                {
                    isBeginningOfWord = true;
                }

                if (!char.IsLetter(ch))
                {
                    if (ch != '.' && isStillAWord || ch == '.' && isPeriodAppeared)
                    {
                        isStillAWord = false;
                        tempWordLength = 0;
                    }
                    else if (ch == '.')
                    {
                        isPeriodAppeared = true;
                    }
                }
                else
                {
                    isStillAWord = true;
                    tempWordLength++;
                }
            }

            // Wrap up the last word if exists
            if (isStillAWord)
            {
                totalWordLength += tempWordLength;
                totalWordNum++;
            }

            if (totalWordLength == 0 || totalWordNum == 0)
            {
                return 250;
            }

            int averWordLen = totalWordLength/totalWordNum;
            if (averWordLen <= 3)
            {
                return 250;
            }
            return averWordLen <= 5 ? 500 : 1000;
        }

        // Replace tags
        public string ReplaceTag(string tag, int code, string toParse)
        {
            if (toParse.IsNullOrEmpty())
            {
                return toParse;
            }

            toParse = toParse.ToLower();
            tag = tag.ToLower();

            Stack<char> charStack = new Stack<char>();
            string strNested = string.Empty;
            bool nestingBegin = false;

            int startIndex = 0;

            int i = 0;
            while (i < toParse.Length)
            {
                char ch = toParse[i];
                switch (ch)
                {
                    case '<':
                        nestingBegin = true;
                        break;
                    case '>':
                        nestingBegin = false;
                        if (!charStack.IsEmpty())
                        {
                            // Pop out all chars
                            while (!charStack.IsEmpty())
                            {
                                strNested = charStack.Pop() + strNested;
                            }
                            int indexOfTag;
                            int offset = 0;
                            while ((indexOfTag = strNested.IndexOf(tag)) >= 0)
                            {
                                strNested = strNested.Substring(0, indexOfTag) + code +
                                            strNested.Substring(indexOfTag + tag.Length);
                                offset += tag.Length - code.ToString().Length;
                            }
                            toParse = toParse.Substring(0, startIndex) + strNested + toParse.Substring(i);
                            strNested = string.Empty;
                            i -= offset;
                        }
                        break;
                    default:
                        if (nestingBegin)
                        {
                            if (charStack.IsEmpty())
                            {
                                startIndex = i;
                            }
                            charStack.Push(ch);
                        }
                        break;
                }
                ++i;
            }

            return toParse;
        }

        /*
         * Return the weekday a certain date is in
         */

        private readonly Dictionary<int, int> _monthToDays = new Dictionary<int, int>
        {
            {1, 31},
            {3, 31},
            {4, 30},
            {5, 31},
            {6, 30},
            {7, 31},
            {8, 31},
            {9, 30},
            {10, 31},
            {11, 30},
            {12, 31},
        };

        private readonly string[] _weekdays =
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday",
            "Sunday"
        };

        private int DaysBetween(int year1, int month1, int day1, int year2, int month2, int day2)
        {
            int totalDays = 0;
            for (int i = year1 + 1; i <= year2 - 1; i++)
            {
                totalDays += i%4 != 0 ? 365 : 366;
            }

            int leftDays = 0;
            int pastDays = 0;
            // Year1
            for (int i = month1 + 1; i <= 12; ++i)
            {
                if (i == 2)
                {
                    leftDays += year1%4 != 0 ? 28 : 29;
                }
                else
                {
                    leftDays += _monthToDays[i];
                }
            }
            leftDays += (month1 != 2) ? _monthToDays[month1] - day1 : year1%4 != 0 ? 28 - day1 : 29 - day1;
            // Year2
            for (int i = 1; i <= month2 - 1; ++i)
            {
                if (i == 2)
                {
                    pastDays += year2%4 != 0 ? 28 : 29;
                }
                else
                {
                    pastDays += _monthToDays[i];
                }
            }
            pastDays += day2 - 1;

            if (year1 != year2)
            {
                totalDays = totalDays + leftDays + pastDays;
            }
            else
            {
                totalDays = leftDays + pastDays - (year1%4 != 0 ? 365 : 366);
            }

            return totalDays;
        }

        public string GetDay(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);
            DateTime pivotTime = new DateTime(1990, 1, 1);

            if (dt <= pivotTime)
            {
                int daysBetween = DaysBetween(year, month, day, 1990, 1, 1);
                return daysBetween == 0
                    ? _weekdays[_weekdays.Length - 1]
                    : _weekdays[_weekdays.Length - 1 - daysBetween%6];
            }
            else
            {
                int daysBetween = DaysBetween(1990, 1, 1, year, month, day);
                return daysBetween == 0 ? _weekdays[1] : _weekdays[daysBetween%6 + 1];
            }
        }

        /*
         * TopCoder: Compute the minimun steps needed to reach the opposite end of the chess plate
         */

        public void ComputeMinSteps(int[,] checkersPlane, int curX, int curY, int tempSteps, bool isConsecutive,
            ref int minSteps)
        {
            if (curY >= 7)
            {
                minSteps = Math.Min(minSteps, tempSteps);
                return;
            }

            if (curX >= 1)
            {
                if (checkersPlane[curX - 1, curY + 1] == 0)
                {
                    ComputeMinSteps(checkersPlane, curX - 1, curY + 1, tempSteps + 1, false, ref minSteps);
                }
                if (curX > 1 && curY < 6 && checkersPlane[curX - 1, curY + 1] == 2 &&
                    checkersPlane[curX - 2, curY + 2] == 0)
                {
                    ComputeMinSteps(checkersPlane, curX - 2, curY + 2, isConsecutive ? tempSteps : tempSteps + 1, true,
                        ref minSteps);
                }
            }
            if (curX <= 6)
            {
                if (checkersPlane[curX + 1, curY + 1] == 0)
                {
                    ComputeMinSteps(checkersPlane, curX + 1, curY + 1, tempSteps + 1, false, ref minSteps);
                }
                if (curX < 6 && curY < 6 && checkersPlane[curX + 1, curY + 1] == 2 &&
                    checkersPlane[curX + 2, curY + 2] == 0)
                {
                    ComputeMinSteps(checkersPlane, curX + 2, curY + 2, isConsecutive ? tempSteps : tempSteps + 1, true,
                        ref minSteps);
                }
            }
        }

        public int Compute(string startPos, string[] checkersPos)
        {
            int[,] checkersPlane = new int[8, 8];
            var startPosAsInt = startPos.Split(',');
            foreach (var checkerPos in checkersPos.Select(str => str.Split(',')))
            {
                checkersPlane[int.Parse(checkerPos[0]), int.Parse(checkerPos[1])] = 2;
            }

            int minSteps = int.MaxValue;

            int curX = int.Parse(startPosAsInt[0]);
            int curY = int.Parse(startPosAsInt[1]);

            ComputeMinSteps(checkersPlane, curX, curY, 0, false, ref minSteps);

            return minSteps == int.MaxValue ? -1 : minSteps;
        }

        /*
         * TopCoder: Find best matched mate
         */

        public string[] GetBestMatches(string[] list, string curUsr, int sf)
        {
            Dictionary<string, string[]> nameToInfo = new Dictionary<string, string[]>();

            foreach (var infoComponents in list.Select(infoStr => infoStr.Split(' ')))
            {
                nameToInfo[infoComponents[0]] = infoComponents;
            }

            var curUsrInfo = nameToInfo[curUsr];
            nameToInfo.Remove(curUsr);

            var retList = nameToInfo.Where(entry =>
            {
                if (!entry.Value[1].Equals(curUsrInfo[2]))
                {
                    return false;
                }
                var targetUsrInfo = entry.Value;
                int numOfCommonAnswer = 0;
                for (int i = 3; i < targetUsrInfo.Length; ++i)
                {
                    numOfCommonAnswer += curUsrInfo[i] == targetUsrInfo[i] ? 1 : 0;
                }
                return numOfCommonAnswer >= sf;
            }).Select(entry => entry.Key);

            return retList.ToArray();
        }

        /*
         * TopCoder: Determine if certain die is loaded
         */
        public int[] BadValues(int[] num)
        {
            if (num.IsNullOrEmpty())
            {
                return new int[] {};
            }

            Dictionary<int, int> numToCount = new Dictionary<int, int>(num.Length);

            foreach (int i in num)
            {
                if (!numToCount.ContainsKey(i))
                {
                    numToCount[i] = 1;
                }
                else
                {
                    numToCount[i]++;
                }
            }

            List<int> retList = new List<int>();
            for (int i = 1; i <= 6; ++i)
            {
                if (numToCount.ContainsKey(i))
                {
                    double freq = (double) numToCount[i]/num.Length;
                    if (freq > 0.25 || freq < 0.1)
                    {
                        retList.Add(i);
                    }
                }
                else
                {
                    retList.Add(i);
                }
            }

            return retList.ToArray();
        }

        /*
         * TopCoder: Get satisfied problem sets
         */
        public void ComputeMatchedProblemSet(ref int count, int startSet, int curSum, Dictionary<int, int[]> setToNums)
        {
            if (curSum > 75)
            {
                return;
            }

            if (startSet == 4 )
            {
                if (curSum <= 75 && curSum >= 60)
                {
                    count++;
                }
                return;
            }

            int[] nums = setToNums[startSet];
            foreach (int t in nums)
            {
                ComputeMatchedProblemSet(ref count, startSet + 1, curSum + t, setToNums);
            }
        }

        public int NumSets(int[] easy, int[] medium, int[] hard)
        {
            if (easy.Length == 0 || medium.Length == 0 || hard.Length == 0)
            {
                return 0;
            }

            Array.Sort(easy);
            Array.Sort(medium);
            Array.Sort(hard);

            int count = 0;

            Dictionary<int, int[]> setToNums = new Dictionary<int, int[]>();
            setToNums[1] = easy;
            setToNums[2] = medium;
            setToNums[3] = hard;

            ComputeMatchedProblemSet(ref count, 1, 0, setToNums);

            return count;
        }

        /*
         * TopCoder: Compute letter ranges
         */
        public string[] Ranges(string str)
        {
            if (str.IsNullOrEmpty())
            {
                return new string[] {};
            }

            bool[] isLetterAppeared = new bool[26];

            foreach (char chAsInt in str.Where(chAsInt => chAsInt >= 97 && chAsInt <= 122))
            {
                isLetterAppeared[chAsInt - 97] = true;
            }

            List<string> retList = new List<string>();
            bool isConsecutiveBegin = false;
            int startIndex = 0;

            for (int i = 0; i < 26; ++i)
            {
                if (isLetterAppeared[i] && !isConsecutiveBegin)
                {
                    isConsecutiveBegin = true;
                    startIndex = i;
                }
                else if (!isLetterAppeared[i] && isConsecutiveBegin)
                {
                    isConsecutiveBegin = false;
                    retList.Add(string.Format("{0}:{1}", (char) (97 + startIndex), (char) (97 + i - 1)));
                }
            }

            return retList.ToArray();
        }

        /*
         * TopCoder: Get satisfied token
         */
        public string[] Tokenize(string[] tokens, string inputStr)
        {
            if (inputStr.IsNullOrEmpty() || tokens.IsNullOrEmpty())
            {
                return new string[] {};
            }

            List<string> retList = new List<string>();
            List<string> tokenList = new List<string>(tokens);
            var sortedTokenList = tokenList.OrderBy(token => tokens.Length);
            while (inputStr.Length != 0)
            {
                string longestToken = string.Empty;
                // ReSharper disable once AccessToModifiedClosure
                var tokenMatched = sortedTokenList.Where(token => inputStr.StartsWith(token));
                if (longestToken.Length != 0)
                {
                    inputStr = inputStr.Substring(tokenMatched.Last().Length);
                    retList.Add(longestToken);
                }
                else
                {
                    inputStr = inputStr.Substring(1);
                }
            }

            return retList.ToArray();
        }

        /*
         * TopCoder: Compute longest zigzag
         */
        public int LongestZigZag(int[] num)
        {
            if (num.IsNullOrEmpty())
            {
                return 0;
            }
            if (num.Length <= 2)
            {
                return num.Length;
            }

            int[,] dp = new int[num.Length, 2];
            for (int i = 0; i < num.Length; ++i)
            {
                dp[i, 0] = dp[i, 1] = 1;
                for (int j = 0; j < i; ++j)
                {
                    if (num[i] - num[j] > 0)
                    {
                        dp[i, 0] = Math.Max(dp[j, 1] + 1, dp[j, 0]);
                    }
                    else if (num[i] - num[j] < 0)
                    {
                        dp[i, 1] = Math.Max(dp[j, 0] + 1, dp[j, 1]);
                    }
                }
            }

            return Math.Max(dp[num.Length - 1, 0], dp[num.Length - 1, 1]);
        }

        /*
         * TopCoder: Determine the type of an array
         */
        private int GetGreatestCommonDenominator(int x, int y)
        {
            if (x < y)
            {
                return GetGreatestCommonDenominator(y, x);
            }
            if (y == 0)
            {
                return x;
            }
            return GetGreatestCommonDenominator(x - y, y);
        }

        public string GetType(int[] num)
        {
            if (num.IsNullOrEmpty())
            {
                return "NOTHING";
            }

            bool hasRepeat = false;
            bool isAscending = false;
            int longestOccurring = 1;
           
            int startIndex = 1;
            for (; startIndex < num.Length; ++startIndex)
            {
                if (num[startIndex] == num[startIndex - 1])
                {
                    if (!hasRepeat)
                    {
                        hasRepeat = true;
                    }
                    longestOccurring++;
                }
                else
                {
                    isAscending = num[startIndex] > num[startIndex - 1];
                    break;
                }
            }

            int tempOccurring = 1;

            for (int i = startIndex + 1; i < num.Length; ++i)
            {
                if (num[i] - num[i - 1] > 0 && !isAscending || num[i] - num[i - 1] < 0 && isAscending)
                {
                    return "NOTHING";
                }
                if (num[i] == num[i - 1])
                {
                    if (!hasRepeat)
                    {
                        hasRepeat = true;
                    }
                    tempOccurring++;
                }
                else
                {
                    if (hasRepeat)
                    {
                        longestOccurring = Math.Max(tempOccurring, longestOccurring);
                        tempOccurring = 1;
                        hasRepeat = false;
                    }
                }
            }

            if (isAscending)
            {
                if (longestOccurring > 1)
                {
                    return string.Format("NONDESCENDING {0}", longestOccurring);
                }
                int sum = num.Sum();
                int gcd = GetGreatestCommonDenominator(sum, num.Length);
                return string.Format("ASCENDING {0}/{1}", num.Sum() / gcd, num.Length / gcd);
            }
            else
            {
                if (longestOccurring > 1)
                {
                    return string.Format("NONASCENDING {0}", longestOccurring);
                }
                int sum = num.Sum();
                int gcd = GetGreatestCommonDenominator(sum, num.Length);
                return string.Format("ASCENDING {0}/{1}", num.Sum() / gcd, num.Length / gcd);
            }
            
        }

        /*
         * TopCoder: Sort names based on last name and importance
         */
        /*
        private void QuicksortNames(string[] names, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex)
            {
                return;
            }

            string pivot = names[endIndex];
            int firstHigh = startIndex;
            for (int i = startIndex; i <= endIndex; ++i)
            {
                if (names[i].Split(' ').Last().ToLower().CompareTo(pivot.Split().Last().ToLower()) < 0)
                {
                    names.Swap(firstHigh, i);
                    firstHigh++;
                }
            }
            names.Swap(firstHigh, endIndex);

            QuicksortNames(names, startIndex, firstHigh - 1);
            QuicksortNames(names, firstHigh + 1, endIndex);
        }
        */

        private void MergeSortNames(string[] names, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex)
            {
                return;
            }

            int middleIndex = (startIndex + endIndex) / 2;
            MergeSortNames(names, startIndex, middleIndex);
            MergeSortNames(names, middleIndex + 1, endIndex);
            // Merge
            string[] temp = new string[endIndex - startIndex + 1];
            int i = startIndex;
            int j = middleIndex + 1;
            int k = 0;
            while (i <= middleIndex && j <= endIndex)
            {
                temp[k++] = names[i].Split(' ').Last().ToLower().CompareTo(names[j].Split(' ').Last().ToLower()) <= 0
                    ? names[i++]
                    : names[j++];
            }

            while (i <= middleIndex)
            {
                temp[k++] = names[i++];
            }

            while (j <= endIndex)
            {
                temp[k++] = names[j++];
            }

            for (int index = 0; index < temp.Length; ++index)
            {
                names[startIndex + index] = temp[index];
            }
        }

        public string[] NewList(string[] names)
        {
            names.ReverseArray();

            MergeSortNames(names, 0, names.Length - 1);

            return names;
        }

        /*
         * TopCoder: Simulation of Champagne tower
         */
        public String HowMuch(int height, int glass, int units)
        {
            double[,] leftOver = new double[height + 1, height + 1];
            double[,] remained = new double[height + 1, height + 1];
            leftOver[1, 1] = units > 2 ? units - 2 : 0;
            remained[1, 1] = units > 2 ? 2 : units;

            if (height == 1)
            {
                return remained[1, 1].ToString();
            }

            int curPos = 2;
            for (int i = 2; i <= height; ++i)
            {
                for (int j = 1; j <= i; ++j)
                {
                    if (j == 1)
                    {
                        leftOver[i, j] = leftOver[i - 1, j] > 4 ? leftOver[i - 1, j] / 2 - 2 : 0;
                        remained[i, j] = leftOver[i - 1, j] > 4 ? 2 : leftOver[i - 1, j] / 2;
                    }
                    else if (j == i)
                    {
                        leftOver[i, j] = leftOver[i - 1, i - 1] > 4 ? leftOver[i - 1, i - 1] / 2 - 2 : 0;
                        remained[i, j] = leftOver[i - 1, i - 1] > 4 ? 2 : leftOver[i - 1, i - 1] / 2;
                    }
                    else
                    {
                        leftOver[i, j] = leftOver[i - 1, j - 1] + leftOver[i - 1, j] > 4
                            ? (leftOver[i - 1, j - 1] + leftOver[i - 1, j]) / 2 - 2
                            : 0;
                        remained[i, j] = leftOver[i - 1, j - 1] + leftOver[i - 1, j] > 4
                            ? 2
                            : (leftOver[i - 1, j - 1] + leftOver[i - 1, j]) / 2;
                    }
                    if (curPos == glass)
                    {
                        return remained[i, j].ToString();
                    }
                    curPos++;
                }
            }

            return null;
        }

        /*
         * TopCoder: Validate string with parentheses 
         */
        public bool Match(string input)
        {
            if (input.IsNullOrEmpty())
            {
                return true;
            }

            Dictionary<char, int> leftParenToNum = new Dictionary<char, int>
            {
                {'(', 1},
                {'[', 2},
                {'{', 3}
            };
            Dictionary<char, int> rightParenToNum = new Dictionary<char, int>
            {
                {')', 1},
                {']', 2},
                {'}', 3}
            };

            Stack<char> parenStack = new Stack<char>();
            foreach (char ch in input.Where(ch => !char.IsLetter(ch)))
            {
                if (leftParenToNum.ContainsKey(ch))
                {
                    parenStack.Push(ch);
                }
                else if (rightParenToNum.ContainsKey(ch))
                {
                    if (parenStack.IsEmpty())
                    {
                        return false;
                    }
                    if (leftParenToNum[parenStack.Pop()] != rightParenToNum[ch])
                    {
                        return false;
                    }
                }
            }

            return parenStack.IsEmpty();
        }

        /*
         * TopCoder: Strip existed string
         */
        public string Strip(string template, string searchStr)
        {
            int index;
            while (template.Length >= searchStr.Length && (index = template.ToLower().FirstOccurrenceOf(searchStr.ToLower())) != -1)
            {
                template = template.Remove(index, searchStr.Length);
            }
            return template;
        }

        /*
         * TopCoder: Calculate the max sum spliting a int array
         */
        private void CalculateMaxSplit(ref int max, int curPos, int curSum, int target, int[] sourceArray)
        {
            if (curPos == sourceArray.Length)
            {
                if (curSum <= target && curSum > max)
                {
                    max = curSum;
                }
                return;
            }
            if (curSum >= target)
            {
                return;
            }

            for (int i = curPos; i != sourceArray.Length; ++i)
            {
                int thisPart = 0;
                for (int j = curPos; j <= i; ++j)
                {
                    thisPart += (int)Math.Pow(10, i - j) * sourceArray[j];
                }
                curSum += thisPart;
                CalculateMaxSplit(ref max, i + 1, curSum, target, sourceArray);
                curSum -= thisPart;
            }
        }

        public int MaxSplit(int source, int target)
        {
            int sourceLen = 0;
            int tempSource = source;
            while (tempSource != 0)
            {
                tempSource /= 10;
                sourceLen++;
            }

            int[] sourceArray = new int[sourceLen];
            for (int i = sourceArray.Length - 1; i >= 0; --i)
            {
                sourceArray[i] = source % 10;
                source /= 10;
            }

            int max = int.MinValue;
            CalculateMaxSplit(ref max, 0, 0, target, sourceArray);

            return max;
        }

        /*
         * TopCoder: Max path sum down a tree diagonally
         */
        public int BestWayDown(int[] nodes)
        {
            if (nodes.IsNullOrEmpty())
            {
                return 0;
            }
            if (nodes.Length == 1)
            {
                return nodes[0];
            }

            int[,] dp = new int[10, 10];
            dp[1, 1] = nodes[0];
            
            int height = 1;

            for (int i = 2; i <= 10; ++i)
            {
                int numOfNodes = 0;
                for (int j = 1; j <= i; ++j)
                {
                    numOfNodes = (1 + i - 1) * (i - 1) / 2 + j;
                    if (j == 1)
                    {
                        dp[i, j] = dp[i - 1, j] + nodes[numOfNodes - 1];
                    }
                    else if (j == i)
                    {
                        dp[i, j] = dp[i - 1, j - 1] + nodes[numOfNodes - 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j] + nodes[numOfNodes - 1], dp[i - 1, j - 1] + nodes[numOfNodes - 1]);
                    }
                }
                if (numOfNodes == nodes.Length)
                {
                    height = i;
                    break;
                }
            }

            int pathMax = int.MinValue;
            for (int i = 1; i <= height; ++i)
            {
                pathMax = Math.Max(dp[height, i], pathMax);
            }

            return pathMax;
        }

        private static readonly int[,] Data =
        {
            {10, 100, 170, 40, 68, 92, 220, 30, 60, 85, 92, 109, 230, 60, 65, 72, 80, 82, 120, 130, 180, 222, 400, 800},
            {5, 20, 10, 12, 20, 40, 70, 30, 40, 30, 30, 60, 100, 51, 52, 53, 54, 70, 20, 40, 40, 90, 200, 600},
            {3, 20, 90, 90, 80, 12, 50, 1, 70, 20, 20, 30, 120, 40, 40, 40, 20, 80, 90, 100, 60, 30, 200, 50}
        };

        /*
         * TopCoder: Calculate max calories one can take given limit volume and weight
         */
        public int MaxCalories(int maxWeight, int maxVolume)
        {
            if (maxVolume == 0 || maxWeight == 0)
            {
                return 0;
            }

            int numOfItems = Data.Length / 3 + 1;
            int[,,] dp = new int[numOfItems, maxWeight + 1, maxVolume + 1];
            for (int i = 1; i != numOfItems; ++i)
            {
                for (int j = 1; j != maxWeight + 1; ++j)
                    for (int k = 1; k != maxVolume + 1; ++k)
                    {
                        if (Data[1, i - 1] <= j && Data[2, i - 1] <= k)
                        {
                            if (Data[0, i - 1] + dp[i - 1, j - Data[1, i - 1], k - Data[2, i - 1]] > dp[i - 1, j, k])
                            {
                                dp[i, j, k] = Data[0, i - 1] + dp[i - 1, j - Data[1, i - 1], k - Data[2, i - 1]];
                            }
                            else
                            {
                                dp[i, j, k] = dp[i - 1, j, k];
                            }
                        }
                        else
                        {
                            dp[i, j, k] = dp[i - 1, j, k];
                        }
                    }
            }

            return dp[numOfItems - 1, maxWeight, maxVolume];
        }

        /*
         * TopCoder: Implement a bitchecker
         */
        public string GetResidue(string m, string k)
        {
            int firsrIndexOfOne;
            while ((firsrIndexOfOne = m.IndexOf('1')) != -1 && firsrIndexOfOne <= m.Length - k.Length)
            {
                m = m.Remove(0, firsrIndexOfOne).Xor(k);
            }

            if (m.Length >= k.Length - 1)
            {
                return m.Substring(m.Length - k.Length + 1);
            }
            int mLen = m.Length;
            for (int i = 0; i < k.Length - mLen - 1; ++i)
            {
                m = '0' + m;
            }
            return m;
        }

        /*
         * TopCoder: AngParse
         */
        public string[] Parse(string input)
        {
            if (!input.StartsWith("{") || !input.EndsWith("}"))
            {
                return new List<string> {"INVALID"}.ToArray();
            }

            List<string> items = new List<string>();
            Stack<char> tempCharStack = new Stack<char>(input.Length - 2);
            string str = string.Empty;
            string temp;
            for (int i = 1; i != input.Length - 1; ++i)
            {
                char ch = input[i];
                switch (ch)
                {
                    case ',':
                        if (!tempCharStack.IsEmpty() && tempCharStack.Peek() == '\'')
                        {
                            tempCharStack.Pop();
                            tempCharStack.Push(ch);
                        }
                        else
                        {
                            temp = string.Empty;
                            while (!tempCharStack.IsEmpty())
                            {
                                temp = tempCharStack.Pop() + temp;
                            }
                            str += temp;
                            items.Add(str);
                            str = string.Empty;
                        }
                        break;
                    case '\'':
                        tempCharStack.Push(ch);
                        break;
                    case ' ':
                        if (!tempCharStack.IsEmpty())
                        {
                            tempCharStack.Push(ch);
                        }
                        else if ((i == 1 || input[i - 1] != ','))
                        {
                            str += ch;
                        }
                        break;
                    case '}':
                    case '{':
                        if (tempCharStack.IsEmpty())
                        {
                            return new List<string> {"INVALID"}.ToArray();
                        }
                        if (tempCharStack.Peek() == '\'')
                        {
                            tempCharStack.Pop();
                            tempCharStack.Push(ch);
                        }
                        else
                        {
                            tempCharStack.Push(ch);
                        }
                        break;
                    default:
                        if (tempCharStack.IsEmpty())
                        {
                            str += ch;
                        }
                        else
                        {
                            tempCharStack.Push(ch);
                        }
                        break;
                }
            }
            // Wrap up last word
            temp = string.Empty;
            while (!tempCharStack.IsEmpty())
            {
                temp = tempCharStack.Pop() + temp;
            }
            str += temp;
            items.Add(str);

            return items.ToArray();
        }

        /*
         * TopCoder: GreedyChange
         */
        private void ConstructOptimalChange(int[] coins, int totalAmount, int[] dp)
        {
            for (int i = 1; i <= totalAmount; ++i)
            {
                dp[i] = int.MaxValue;
                for (int j = coins.Length - 1; j != -1; --j)
                {
                    if (coins[j] == i)
                    {
                        dp[i] = 1;
                    }
                    else if (coins[j] < i)
                    {
                        dp[i] = Math.Min(dp[i], 1 + dp[i - coins[j]]);
                    }
                }
            }
        }

        private int GetLargest(int[] coins, int upperBound)
        {
            for (int i = coins.Length - 1; i != -1; --i)
            {
                if (coins[i] <= upperBound)
                {
                    return coins[i];
                }
            }
            return -1;
        }

        public int Smallest(int[] coins)
        {
            Array.Sort(coins);
            int sum = coins.Sum();
            int[] dp = new int[sum + 1];
            ConstructOptimalChange(coins, sum, dp);

            int[] greedyArray = new int[sum + 1];
            greedyArray[0] = greedyArray[1] = 1;
            for (int i = 2; i != sum + 1; ++i)
            {
                greedyArray[i] = 1 + greedyArray[i - coins.Where(coin => coin <= i).Last()];
            }

            for (int i = sum; i != -1; --i)
            {
                if (dp[i] != greedyArray[i])
                {
                    return i;
                }
            }
            return -1;
        }

        /*
         * TopCoder: ContractWork
         */
        public void CalculateMinimumCost(Dictionary<int, int[]> companyToCost, int[] taskCharger, ref int minCost, int curCost,
            int curTask)
        {
            if (curTask == taskCharger.Length)
            {
                minCost = Math.Min(curCost, minCost);
                return;
            }

            for (int i = 0; i < companyToCost.Count; ++i)
            {
                if (curTask < 2)
                {
                    curCost += companyToCost[i][curTask];
                    taskCharger[curTask] = i;
                    CalculateMinimumCost(companyToCost, taskCharger, ref minCost, curCost, curTask + 1);
                    taskCharger[curTask] = -1;
                    curCost -= companyToCost[i][curTask];
                }
                else if (taskCharger[curTask - 1] != i || taskCharger[curTask - 2] != i)
                {
                    curCost += companyToCost[i][curTask];
                    taskCharger[curTask] = i;
                    CalculateMinimumCost(companyToCost, taskCharger, ref minCost, curCost, curTask + 1);
                    taskCharger[curTask] = -1;
                    curCost -= companyToCost[i][curTask];
                }
            }

        }

        public int MinimumCost(string[] costs, int numOfTasks)
        {
            Dictionary<int, int[]> companyToCost = new Dictionary<int, int[]>();
            for (int i = 0; i != costs.Length; ++i)
            {
                companyToCost[i] = costs[i].Split(' ').Select(int.Parse).ToArray();
            }

            int numOfCompany = costs.Length;
            bool[,,] isContinueByTwo = new bool[numOfTasks, numOfCompany, numOfCompany];
            int[,,] costContinueByTwo = new int[numOfTasks, numOfCompany, numOfCompany];

            for (int i = 0; i != numOfCompany; ++i)
            {
                for (int j = 0; j != numOfCompany; ++j)
                {
                    costContinueByTwo[1, i, j] = companyToCost[i][0] + companyToCost[j][1];
                    isContinueByTwo[1, i, j] = true;
                }
            }

            for (int i = 2; i < numOfTasks; ++i)
            {
                for (int j = 0; j < numOfCompany; ++j)
                {
                    for (int k = 0; k < numOfCompany; ++k)
                    {
                        for (int l = 0; l < numOfCompany; ++l)
                        {
                            if (k == j && l == j) continue;
                            if (isContinueByTwo[i, l, j] &&
                                costContinueByTwo[i - 1, k, l] + companyToCost[j][i] >= costContinueByTwo[i, l, j])
                                continue;
                            costContinueByTwo[i, l, j] = costContinueByTwo[i - 1, k, l] + companyToCost[j][i];
                            isContinueByTwo[i, l, j] = true;
                        }
                    }
                }
            }

            int minCost = int.MaxValue;
            for (int i = 0; i < numOfCompany; ++i)
            {
                for (int j = 0; j < numOfCompany; ++j)
                {
                    minCost = Math.Min(minCost, costContinueByTwo[numOfTasks - 1, i, j]);
                }
            }

            return minCost;
            /*
            int[] taskCharger = new int[numOfTasks];
            for (int i = 0; i != numOfTasks; ++i)
            {
                taskCharger[i] = -1;
            }

            int minCost = int.MaxValue;

            CalculateMinimumCost(companyToCost, taskCharger, ref minCost, 0, 0);
            
            return minCost;
            */
        }

        /*
         * TopCoder: MessageMess
         */
        private void CalculateParsingWays(int curPos, HashSet<string> dictSet, List<string> words, List<string> sentence, string input)
        {
            if (curPos == input.Length)
            {
                sentence.Add(string.Join(" ", words.ToArray()));
                return;
            }

            for (int i = curPos; i != input.Length; ++i)
            {
                string word = input.Substring(curPos, i + 1 - curPos);
                if (dictSet.Contains(word))
                {
                    words.Add(word);
                    CalculateParsingWays(i + 1, dictSet, words, sentence, input);
                    words.RemoveAt(words.Count - 1);
                }
            }
        }

        public string Restore(string[] dict, string input)
        {
            HashSet<string> dictSet = new HashSet<string>(dict);
            int[] dp = new int[input.Length];
            string sentence = string.Empty;

            for (int i = 0; i != input.Length; ++i)
            {
                if (dictSet.Contains(input.Substring(0, i + 1)))
                {
                    dp[i]++;
                }
                for (int j = 1; j <= i; ++j)
                {
                    if (dictSet.Contains(input.Substring(j, i - j + 1)))
                    {
                        if (dp[j - 1] == 1)
                        {
                            sentence += sentence.Length == 0
                                ? input.Substring(0, j) + " " + input.Substring(j, i - j + 1)
                                : " " + input.Substring(j, i - j + 1);
                        }
                        dp[i] = dp[j - 1];
                    }
                }
            }

            if (dp[input.Length - 1] == 1)
            {
                return sentence;
            }
            return dp[input.Length - 1] == 0 ? "IMPOSSIBLE" : "AMBIGUOUS";

            /*
            List<string> words = new List<string>();
            List<string> sentence = new List<string>();

            CalculateParsingWays(0, dictSet, words, sentence, input);

            if (sentence.Count == 1)
            {
                return sentence[0];
            }
            return sentence.Count == 0 ? "IMPOSSIBLE!" : "AMBIGUOUS";
            */
        }

        /*
         * TopCoder: MoneyRun
         */
        private void CalculateHighestPrice(ref int max, int curRowA, int curColA, int curRowB, int curColB, int curPrice, int[,] priceGrid, int row, int col)
        {
            if (curRowA == row - 1 && curColA == col - 1)
            {
                curPrice += priceGrid[curRowA, curColA];
                max = Math.Max(curPrice, max);
                return;
            }

            if (curRowA < row - 1)
            {
                if (curRowB < row - 1)
                {
                    CalculateHighestPrice(ref max, curRowA + 1, curColA, curRowB + 1, curColB,
                            curRowA == curRowB && curColA == curColB
                                ? curPrice + priceGrid[curRowA, curColA]
                                : curPrice + priceGrid[curRowA, curColA] + priceGrid[curRowB, curColB], priceGrid, row,
                            col);
                }
                if (curColB < col - 1)
                {
                    CalculateHighestPrice(ref max, curRowA + 1, curColA, curRowB, curColB + 1,
                            curRowA == curRowB && curColA == curColB
                                ? curPrice + priceGrid[curRowA, curColA]
                                : curPrice + priceGrid[curRowA, curColA] + priceGrid[curRowB, curColB], priceGrid, row,
                            col);
                }
            }

            if (curColA < col - 1)
            {
                if (curRowB < row - 1)
                {
                    CalculateHighestPrice(ref max, curRowA, curColA + 1, curRowB + 1, curColB,
                            curRowA == curRowB && curColA == curColB
                                ? curPrice + priceGrid[curRowA, curColA]
                                : curPrice + priceGrid[curRowA, curColA] + priceGrid[curRowB, curColB], priceGrid, row,
                            col);
                }
                if (curColB < col - 1)
                {
                    CalculateHighestPrice(ref max, curRowA, curColA + 1, curRowB, curColB + 1,
                            curRowA == curRowB && curColA == curColB
                                ? curPrice + priceGrid[curRowA, curColA]
                                : curPrice + priceGrid[curRowA, curColA] + priceGrid[curRowB, curColB], priceGrid, row,
                            col);
                }
            }
        }

        public int GetMost(string[] grid)
        {
            int row = grid.Length;
            int col = grid[0].Length;

            int[,] val = new int[row, col];

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    val[i, j] = grid[i][j] - 48;
                }
            }

            int max = int.MinValue;
            CalculateHighestPrice(ref max, 0, 0, 0, 0, 0, val, row, col);

            return max;
        }

        /*
         * TopCoder: BusinessPlan
         */
        private void QuickSortArrayAlongWithOthers(int[] revenueToTimeRatio, int[] expense, int[] revenue, int[] time, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex)
            {
                return;
            }

            int firstHigh = startIndex;
            int pivot = revenueToTimeRatio[endIndex];
            for (int i = startIndex; i < endIndex; ++i)
            {
                if (revenueToTimeRatio[i] > pivot)
                {
                    revenueToTimeRatio.Swap(firstHigh, i);
                    expense.Swap(firstHigh, i);
                    revenue.Swap(firstHigh, i);
                    time.Swap(firstHigh, i);
                    firstHigh++;
                }
            }
            revenueToTimeRatio.Swap(firstHigh, endIndex);
            expense.Swap(firstHigh, endIndex);
            revenue.Swap(firstHigh, endIndex);
            time.Swap(firstHigh, endIndex);

            QuickSortArrayAlongWithOthers(revenueToTimeRatio, expense, revenue, time, startIndex, firstHigh - 1);
            QuickSortArrayAlongWithOthers(revenueToTimeRatio, expense, revenue, time, firstHigh + 1, endIndex);
        }

        public int HowLong(int[] expense, int[] revenue, int[] time, int capital, int debit)
        {
            if (capital >= debit)
            {
                return 0;
            }

            int[] revenueTimeRatio = new int[revenue.Length];
            for (int i = 0; i < revenue.Length; ++i)
            {
                revenueTimeRatio[i] = revenue[i] / time[i];
            }

            QuickSortArrayAlongWithOthers(revenueTimeRatio, expense, revenue, time, 0, revenue.Length - 1);

            int span = 0;
            while (capital < debit)
            {
                int i = 0;
                for (; i < expense.Length; ++i)
                {
                    if (capital < expense[i]) continue;
                    capital = capital - expense[i] + revenue[i];
                    span += time[i];
                    break;
                }
                if (i == expense.Length)
                {
                    return -1;
                }
            }

            return span;
        }

        /*
         * TopCoder: BagsOfGod
         */
        public int GetNetGain(int[] bags)
        {
            int numOfBags = bags.Length; 
            int[,] netGain = new int[numOfBags, numOfBags]; 
            
            for (int i = 0; i < numOfBags; i++) 
            { 
                netGain[i, i] = bags[i]; 
            } 
            for (int l = 1; l < numOfBags; l++) 
            {
                for (int i = 0; i + l < numOfBags; i++)
                {
                    int j = i + l;
                    netGain[i, j] = Math.Max(bags[i] - netGain[i + 1, j], bags[j] - netGain[i, j - 1]);
                }
            }  
            return netGain[0, numOfBags - 1]; 
        }

        /*
         * TopCoder: TreeSpreading
         */
        private long Plant(long[, , , ,] ways, int a, int b, int c, int d, int last)
        {
            if (a == 0 && b == 0 && c == 0 && d == 0)
            {
                return 1;
            }

            if (ways[a, b, c, d, last] != -1)
            {
                return ways[a, b, c, d, last];
            }

            long ret = 0;
            if (last != 0 && a > 0) ret += Plant(ways, a - 1, b, c, d, 0);
            if (last != 1 && b > 0) ret += Plant(ways, a, b - 1, c, d, 1);
            if (last != 2 && c > 0) ret += Plant(ways, a, b, c - 1, d, 2);
            if (last != 3 && d > 0) ret += Plant(ways, a, b, c, d - 1, 3);
            ways[a, b, c, d, last] = ret;
            return ret;
        }

        public long CountArrangements(int a, int b, int c, int d)
        {
            long[,,,,] ways = new long[a + 1, b + 1, c + 1, d + 1, 5];
            for (int i = 0; i < ways.GetLength(0); i++)
            {
              for (int j = 0; j < ways.GetLength(1); j++)
              {
                for (int k = 0; k < ways.GetLength(2); k++)
                {
                  for (int l = 0; l < ways.GetLength(3); l++)
                  {
                      for (int m = 0; m < ways.GetLength(4); ++m)
                      {
                          ways[i, j, k, l, m] = -1;
                      }
                  }
                }
              }
            }

            return Plant(ways, a, b, c, d, 4);
        }

        /*
         * TopCoder: TCU
         */
        public int[] Majors(string[] percentages, int[] start, int years)
        {
            if (years == 0)
            {
                return start;
            }

            Dictionary<int, int[]> majorToPercentages = new Dictionary<int, int[]>();
            for (int i = 0; i < percentages.Length; ++i)
            {
                majorToPercentages[i] = percentages[i].Split(' ').Select(int.Parse).ToArray();
            }

            for (int i = 1; i <= years; ++i)
            {
                int[,] transfer = new int[percentages.Length, percentages.Length];
                for (int j = 0; j < percentages.Length; ++j)
                {
                    int total = 0;
                    for (int k = 0; k < percentages.Length; ++k)
                    {
                        if (k == j) continue;
                        transfer[j, k] = start[j] * majorToPercentages[j][k] / 100;
                        total += start[j] * majorToPercentages[j][k] / 100;
                    }
                    start[j] -= total;
                }
                for (int j = 0; j < percentages.Length; ++j)
                {
                    int total = 0;
                    for (int k = 0; k < percentages.Length; ++k)
                    {
                        if (k == j) continue;
                        total += transfer[k, j];

                    }
                    start[j] += total;
                }
            }

            return start;
        }

        /*
         * TopCoder: MakeUnique
         */
        private bool IsAlphbetical(string input)
        {
            char prevChar = '.';
            foreach (char ch in input.Where(char.IsLetter))
            {
                if (prevChar == '.')
                {
                    prevChar = ch;
                }
                else if (ch < prevChar)
                {
                    return false;
                }
                else
                {
                    prevChar = ch;
                }
            }
            return true;
        }

        private void GenerateEliminatedStr(string input, int curPos,
            Dictionary<char, int> charFreq, Dictionary<char, bool> charKeeped, List<string> eliminatedStrList)
        {
            if (curPos == input.Length)
            {
                eliminatedStrList.Add(input);
                return;
            }

            char ch = input[curPos];
            if (charFreq[ch] > 1)
            {
                if (!charKeeped[ch])
                {
                    // Keep eliminating
                    charFreq[ch]--;
                    input = input.Remove(curPos, 1);
                    input = input.Insert(curPos, ".");
                    GenerateEliminatedStr(input, curPos + 1, charFreq, charKeeped,
                        eliminatedStrList);
                    charFreq[ch]++;
                    input = input.Substring(0, curPos) + ch + input.Substring(curPos + 1);
                    // Keep it
                    charKeeped[ch] = true;
                    GenerateEliminatedStr(input, curPos + 1, charFreq, charKeeped,
                        eliminatedStrList);
                    charKeeped[ch] = false;
                }
                else
                {
                    charFreq[ch]--;
                    input = input.Remove(curPos, 1);
                    input = input.Insert(curPos, ".");
                    GenerateEliminatedStr(input, curPos + 1, charFreq, charKeeped,
                        eliminatedStrList);
                    charFreq[ch]++;
                    // ReSharper disable once RedundantAssignment
                    input = input.Substring(0, curPos) + ch + input.Substring(curPos + 1);
                }
            }
            else
            {
                GenerateEliminatedStr(input, curPos + 1, charFreq, charKeeped,
                        eliminatedStrList);
            }
        }

        public string Eliminated(string input)
        {
            Dictionary<char, int> charFreq = new Dictionary<char, int>();
            foreach (char ch in input)
            {
                charFreq[ch] = !charFreq.ContainsKey(ch) ? 1 : charFreq[ch] + 1;
            }
            Dictionary<char, bool> charKeeped = new Dictionary<char, bool>();
            foreach (var key in charFreq.Keys.Where(key => charFreq[key] > 1))
            {
                charKeeped[key] = false;
            }

            List<string> elimitedStrList = new List<string>();

            GenerateEliminatedStr(input, 0, charFreq, charKeeped, elimitedStrList);
            // Do processing initial list

            elimitedStrList = elimitedStrList.Where(IsAlphbetical).ToList();

            return elimitedStrList.First();
        }

        /*
         * TopCoder: BadNeighbors
         */
        private void CalculateMaxDonations(ref int maxDonation, int curMax, int curPos, bool[] isDonated, int[] donations)
        {
            if (curPos == isDonated.Length)
            {
                maxDonation = Math.Max(maxDonation, curMax);
                return;
            }

            if (curPos == 0)
            {
                isDonated[curPos] = true;
                CalculateMaxDonations(ref maxDonation, curMax + donations[curPos], curPos + 1, isDonated, donations);
                isDonated[curPos] = false;
                CalculateMaxDonations(ref maxDonation, curMax, curPos + 1, isDonated, donations);
            }
            else if (curPos == isDonated.Length - 1)
            {
                if (!isDonated[curPos - 1] && !isDonated[0])
                {
                    isDonated[curPos] = true;
                    CalculateMaxDonations(ref maxDonation, curMax + donations[curPos], curPos + 1, isDonated, donations);
                    isDonated[curPos] = false;
                }
                CalculateMaxDonations(ref maxDonation, curMax, curPos + 1, isDonated, donations);
            }
            else
            {
                if (!isDonated[curPos - 1])
                {
                    isDonated[curPos] = true;
                    CalculateMaxDonations(ref maxDonation, curMax + donations[curPos], curPos + 1, isDonated, donations);
                    isDonated[curPos] = false;
                }
                CalculateMaxDonations(ref maxDonation, curMax, curPos + 1, isDonated, donations);
            }
        }

        public int MaxDonations(int[] neighbors)
        {
            /*
            bool[] isDonated = new bool[neighbors.Length];

            int maxDonations = int.MinValue;
            CalculateMaxDonations(ref maxDonations, 0, 0, isDonated, neighbors);

            return maxDonations;
            */
            int[,] dp = new int[neighbors.Length, 2];
            dp[0, 0] = 0;
            dp[0, 1] = neighbors[0];

            for (int i = 1; i < neighbors.Length - 1; ++i)
            {
                dp[i, 0] = Math.Max(dp[i - 1, 1], dp[i - 1, 0]);
                dp[i, 1] = dp[i - 1, 0] + neighbors[i];
            }

            int max1 = Math.Max(dp[neighbors.Length - 2, 0], dp[neighbors.Length - 2, 1]);

            for (int i = 0; i < neighbors.Length; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    dp[i, j] = 0;
                }
            }

            dp[1, 0] = 0;
            dp[1, 1] = neighbors[1];

            for (int i = 2; i < neighbors.Length; ++i)
            {
                dp[i, 0] = Math.Max(dp[i - 1, 1], dp[i - 1, 0]);
                dp[i, 1] = dp[i - 1, 0] + neighbors[i];
            }

            int max2 = Math.Max(dp[neighbors.Length - 1, 0], dp[neighbors.Length - 1, 1]);

            return Math.Max(max1, max2);
        }

        /*
         * TopCoder: CoinWeight
         */
        private void CalcuteDistinctValues(bool[,] valLeftWeightPair, bool[] hasVal, Dictionary<int, int[]> valToRange, int curLeft, int curVal)
        {
            if (curLeft < 0)
            {
                return;
            }
            if (curLeft == 0)
            {
                hasVal[curVal] = true;
                return;
            }
            if (valLeftWeightPair[curVal, curLeft])
            {
                return;
            }

            foreach (var key in valToRange.Keys)
            {
                for (int i = valToRange[key][0]; i <= valToRange[key][1]; ++i)
                {
                    CalcuteDistinctValues(valLeftWeightPair, hasVal, valToRange, curLeft - i, curVal + key);
                }
            }
            valLeftWeightPair[curVal, curLeft] = true;
        }

        public int PossibleValues(int weight, string[] coins)
        {
            Dictionary<int, int[]> valToRange = new Dictionary<int, int[]>();
            foreach (string t in coins)
            {
                var val = t.Split(' ');
                int[] range = new int[2];
                range[0] = int.Parse(val[1]);
                range[1] = int.Parse(val[2]);
                valToRange[int.Parse(val[0])] = range;
            }

            bool[] hasVal = new bool[2501];
            bool[,] valLeftWeightPair = new bool[2501, weight + 1];

            CalcuteDistinctValues(valLeftWeightPair, hasVal, valToRange, weight, 0);

            int distinctVal = hasVal.Sum(boolVal => boolVal ? 1 : 0);

            return distinctVal;
        }

        /*
         * TopCoder: BadStrings (Right now this method takes too much time)
         */
        private void CalculateMatchedStr(ref long matched, int curPos, int len, string curStr, string bad1, string bad2)
        {
            if (curPos == len)
            {
                matched++;
                return;
            }

            for (char ch = 'A'; ch != 'D'; ++ch)
            {
                if (curPos < bad1.Length - 1)
                {
                    CalculateMatchedStr(ref matched, curPos + 1, len, curStr + ch, bad1, bad2);
                }
                else if (curPos < bad2.Length - 1)
                {
                    if ((curStr + ch).Contains(bad1))
                    {
                        continue;
                    }
                    CalculateMatchedStr(ref matched, curPos + 1, len, curStr + ch, bad1, bad2);
                }
                else
                {
                    if ((curStr + ch).Contains(bad1) || (curStr + ch).Contains(bad2))
                    {
                        continue;
                    }
                    CalculateMatchedStr(ref matched, curPos + 1, len, curStr + ch, bad1, bad2);
                }
            }
        }

        public long HowMany(int len, string bad1, string bad2)
        {
            long matched = 0;
            if (bad1.Length <= bad2.Length)
            {
                CalculateMatchedStr(ref matched, 0, len, string.Empty, bad1, bad2);
            }
            else
            {
                CalculateMatchedStr(ref matched, 0, len, string.Empty, bad2, bad1);
            }

            return matched;
        }

        /*
         * TopCoder: LastStone
         */
        public int NumWins(int[] turns, int m, int n)
        {
            bool[] wins = new bool[n + 101];
            for (int i = 0; i <= n; ++i)
            {
                if (!wins[i])
                {
                    foreach (int turn in turns)
                    {
                        wins[i + turn] = true;
                    }
                }
            }

            int res = 0;
            for (int i = m; i <= n; ++i)
            {
                if (wins[i])
                {
                    ++res;
                }
            }

            return res;
        }

        /*
         * TopCoder: Alignment
         */
        public int Align(string a, string b, int x)
        {
            int[,] dp = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; ++i)
            {
                dp[i, 0] = x + i;
            }
            for (int i = 0; i <= b.Length; ++i)
            {
                dp[0, i] = x + i;
            }

            for (int i = 1; i <= a.Length; ++i)
            {
                var chA = a[i - 1];
                for (int j = 1; j <= b.Length; ++j)
                {
                    int min = int.MaxValue;
                    var chB = b[j - 1];
                    if (chA == chB)
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        for (int k = j - 1; k > 0; --k)
                        {
                            min = Math.Min(min, dp[i, k] + (j - k) + x);
                        }
                        for (int k = i - 1; k > 0; --k)
                        {
                            min = Math.Min(min, dp[k, j] + (i - k) + x);
                        }
                        dp[i, j] = min;
                    }
                }
            }

            return dp[a.Length, b.Length];
        }

        /*
         * TopCoder: FenceRepairing
         */
        public double CalculateCost(string[] boards)
        {
            if (boards.Length == 1 && boards[0].Length == 1)
            {
                return boards[0][0] == '.' ? 0 : 1;
            }

            string board = boards[0];
            for (int i = 1; i < boards.Length; ++i)
            {
                board += boards[i];
            }

            double[,] dp = new double[board.Length, board.Length];
            dp[0, 0] = board[0] == 'X' ? 1 : 0;
            for (int i = 1; i < board.Length; ++i)
            {
                dp[0, i] = board[i] == 'X' ? Math.Sqrt(i + 1) : dp[0, i - 1];
            }

            for (int i = 1; i < board.Length; ++i)
            {
                if (board[i] == 'X')
                {
                    for (int j = 0; j < i; ++j)
                    {
                        dp[0, i] = Math.Min(dp[0, i], dp[0, j] + Math.Sqrt(i - j));
                    }
                    dp[0, i] = Math.Min(Math.Sqrt(i + 1), dp[0, i]);
                }
                else
                {
                    for (int j = 0; j < i; ++j)
                    {
                        dp[0, i] = Math.Min(dp[0, i], dp[0, j] + Math.Sqrt(i - j));
                    }
                    dp[0, i] = Math.Min(dp[0, i - 1], dp[0, i]);
                }
            }

            return dp[0, board.Length - 1];
        }

        /*
         * TopCoder: ForbiddenStrings
         */
        public long CountNotForbidden(int n)
        {
            if (n <= 2)
            {
                return 9;
            }

            int[,,] dp = new int[n + 1, 3, 3];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    dp[2, i, j] = 1;
                }
            }

            for (int i = 3; i <= n; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        for (int l = 0; l < 3; ++l)
                        {
                            if (k == l || j == l || j == k)
                            {
                                dp[i, k, l] += dp[i - 1, j, k];
                            }
                        }
                    }
                }
            }

            long totalUnforbidden = 0;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    totalUnforbidden += dp[n, i, j];
                }
            }

            return totalUnforbidden;
        }

        /*
         * TopCoder: CasketOfStar
         */
        private void CalculateMaxEnergy(ref int maxEnergy, int curLeft, int curEnergy, bool[] ifStillLight, int[] weights)
        {
            if (curLeft == 0)
            {
                maxEnergy = Math.Max(maxEnergy, curEnergy);
            }

            for (int i = 1; i < weights.Length - 1; ++i)
            {
                if (ifStillLight[i])
                {
                    ifStillLight[i] = false;
                    int left = 0;
                    int right = 0;
                    for (int j = i - 1; j >= 0; --j)
                    {
                        if (ifStillLight[j])
                        {
                            left = weights[j];
                            break;
                        }
                    }
                    for (int j = i + 1; j < weights.Length; ++j)
                    {
                        if (ifStillLight[j])
                        {
                            right = weights[j];
                            break;
                        }
                    }
                    CalculateMaxEnergy(ref maxEnergy, curLeft - 1, curEnergy + left * right, ifStillLight, weights);
                    ifStillLight[i] = true;
                }
            }
        }

        public int MaxEnergy(int[] weights)
        {
            if (weights.Length == 3)
            {
                return weights[0] * weights[2];
            }

            /*
            bool[] ifStillLight = new bool[weights.Length];
            for (int i = 0; i < ifStillLight.Length; ++i)
            {
                ifStillLight[i] = true;
            }

            int maxEnergy = int.MinValue;
            CalculateMaxEnergy(ref maxEnergy, weights.Length - 2, 0, ifStillLight, weights);

            return maxEnergy;
            */
            int[,] dp = new int[weights.Length, weights.Length];
            for (int i = 0; i < weights.Length - 2; ++i)
            {
                dp[i, i + 2] = weights[i] * weights[i + 2];
            }

            for (int j = 3; j < weights.Length; ++j)
            {
                for (int i = 0; i + j < weights.Length; ++i)
                {
                    for (int k = i + 1; k < i + j; ++k)
                    {
                        dp[i, i + j] = Math.Max(dp[i, i + j], dp[i, k] + dp[k, i + j] + weights[i] * weights[i + j]);
                    }
                }
            }

            return dp[0, weights.Length - 1];
        }

        /*
         * TopCoder: RowAndCoins
         */
        public string GetWinner(string cells)
        {
            int alice = 0;
            int bob = 0;
            char prev = 'X';
            foreach (char ch in cells)
            {
                if (ch != prev)
                {
                    if (prev == 'A')
                    {
                        alice++;
                    }
                    else
                    {
                        bob++;
                    }
                }
                prev = ch;
            }

            return alice >= bob ? "Alice" : "Bob";
        }

        /*
         * TopCoder: BankLottery
         */
        public double BankLottery(int[] accounts, int weeklyJacpot, int weeks)
        {
            double[] accountsAsDouble = new double[accounts.Length];
            for (int i = 0; i < accounts.Length; ++i)
            {
                accountsAsDouble[i] = accounts[i];
            }

            for (int i = 1; i <= weeks; ++i)
            {
                double sum = accountsAsDouble.Sum();
                for (int j = 0; j < accounts.Length; ++j)
                {
                    double chances = accountsAsDouble[j] / sum;
                    accountsAsDouble[j] = chances * (accountsAsDouble[j] + weeklyJacpot) + (1 - chances) * accountsAsDouble[j];
                }
            }

            return accountsAsDouble[0];
        }

        /*
         * TopCoder: Assemble
         */
        public int MinCost(int[] connect)
        {
            int numOfComponents = connect.Length - 1;
            int[,] dp = new int[numOfComponents, numOfComponents];

            for (int i = 1; i != numOfComponents; ++i)
            {
                dp[i - 1, i] = (connect[i - 1] + 1) * connect[i] * (connect[i + 1] + 1);
            }

            for (int start = numOfComponents - 3; start != -1; --start)
            {
                for (int end = start + 2; end != numOfComponents; ++end)
                {
                    int minConnection = int.MaxValue;
                    for (int split = start; split != end; ++split)
                    {
                        minConnection = Math.Min(minConnection, dp[start, split] + dp[split + 1, end] +
                                                 connect[split + 1] * (connect[start] + split - start + 1) * (connect[end + 1] + end - split));
                    }
                    dp[start, end] = minConnection;
                }  
            }

            return dp[0, numOfComponents - 1];
        }

        /*
         * TopCoder: Robot
         */
        public int GetPro(string[] floor, int x, int y, int time)
        {
            if (floor[x][y] == 'X')
            {
                return 0;
            }

            int row = floor.Length;
            int col = floor[0].Length;

            double[,,] dp = new double[row, col, time];
            dp[0, 0, 0] = 1000;

            const double multi = 0.015625;
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    for (int t = 1; t < time; ++t)
                    {
                        if (floor[i][j] == 'X')
                        {
                            dp[i, j, t] = 0.0;
                        }
                        else
                        {
                            if (i > 0 && i < row - 1 && j > 0 && j < col - 1)
                            {
                                dp[i, j, t] = dp[i - 1, j, t - 1] + dp[i + 1, j, t - 1] + dp[i, j - 1, t - 1] + dp[i, j + 1, t - 1]
                                              + dp[i - 1, j - 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0)
                                              + dp[i + 1, j + 1, t - 2] * (floor[i][j + 1] != 'X' ? multi : 0 + floor[i + 1][j] != 'X' ? multi : 0)
                                              + dp[i - 1, j + 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j + 1] != 'X' ? multi : 0)
                                              + dp[i + 1, j - 1, t - 2] * (floor[i + 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0);
                            }
                            else if (j == col - 1)
                            {
                                if (i == 0) {
                                    dp[i, j, t] = dp[i + 1, j, t - 1] + dp[i, j - 1, t - 1]
                                                  + dp[i + 1, j - 1, t - 2] * (floor[i + 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0);
                                }
                                else if (i == row - 1)
                                {
                                    dp[i, j, t] = dp[i - 1, j, t - 1] + dp[i, j - 1, t - 1]
                                                  + dp[i - 1, j - 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0);
                                }
                                else 
                                {
                                    dp[i, j, t] = dp[i - 1, j, t - 1] + dp[i + 1, j, t - 1] + dp[i, j - 1, t - 1]
                                                  + dp[i - 1, j - 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0)
                                                  + dp[i + 1, j - 1, t - 2] * (floor[i + 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0);
                                }
                            }
                            else if (j == 0)
                            {
                                if (i == row - 1)
                                {
                                    dp[i, j, t] = dp[i - 1, j, t - 1] + dp[i, j + 1, t - 1]
                                                  + dp[i - 1, j + 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j + 1] != 'X' ? multi : 0);
                                }
                                else 
                                {
                                    dp[i, j, t] = dp[i - 1, j, t - 1] + dp[i + 1, j, t - 1] + dp[i, j + 1, t - 1]
                                                  + dp[i + 1, j + 1, t - 2] * (floor[i][j + 1] != 'X' ? multi : 0 + floor[i + 1][j] != 'X' ? multi : 0)
                                                  + dp[i - 1, j + 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j + 1] != 'X' ? multi : 0);
                                }
                            }
                            else if (i == 0)
                            {
                                dp[i, j, t] = dp[i + 1, j, t - 1] + dp[i, j - 1, t - 1] + dp[i, j + 1, t - 1]
                                              + dp[i + 1, j + 1, t - 2] * (floor[i][j + 1] != 'X' ? multi : 0 + floor[i + 1][j] != 'X' ? multi : 0)
                                              + dp[i + 1, j - 1, t - 2] * (floor[i + 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0);
                            }
                            else if (i == row - 1)
                            {
                                dp[i, j, t] = dp[i - 1, j, t - 1] + dp[i, j - 1, t - 1] + dp[i, j + 1, t - 1]
                                              + dp[i - 1, j - 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j - 1] != 'X' ? multi : 0)              
                                              + dp[i - 1, j + 1, t - 2] * (floor[i - 1][j] != 'X' ? multi : 0 + floor[i][j + 1] != 'X' ? multi : 0);
                            }
                        }
                    }
                }
            }

            return (int)(dp[x - 1, y - 1, time - 1] * 1000);
        }

        public int MinPathSum(Tree<int> root, int sum)
        {
            if (root == null)
            {
                return sum == 0 ? 0 : int.MaxValue / 2;
            }

            if (root.Left != null && root.Right == null)
            {
                return 1 + MinPathSum(root.Left, sum - root.Data);
            }
            if (root.Right != null && root.Left == null)
            {
                return 1 + MinPathSum(root.Right, sum - root.Data);
            }

            /* Take advantage of the BST
             */
            if (root.Data * 2 > sum)
            {
                return 1 + MinPathSum(root.Left, sum - root.Data);
            }
            return 1 + Math.Min(MinPathSum(root.Right, sum - root.Data), MinPathSum(root.Left, sum - root.Data));
        }
    }
}
