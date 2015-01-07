using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsPrepareCSharp
{
    internal class Tc
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

            int numOfTurns = 0;
            int curMax = Math.Max(num[0], num[1]);
            int curMin = Math.Min(num[0], num[1]);
            
            for (int i = 2; i < num.Length; ++i)
            {
                if (num[i] > num[i - 1] && num[i - 1] < num[i - 2] || num[i] < num[i - 1] && num[i - 1] > num[i - 2])
                {
                    numOfTurns++;   
                }
                else if (num[i - 1] == num[i - 2])
                {
                    if (num[i] < num[i - 1] && num[i - 1] == curMax || num[i] > num[i - 1] && num[i - 1] == curMin)
                    {
                        numOfTurns++;
                    }
                }
                curMin = Math.Min(curMin, num[i]);
                curMax = Math.Max(curMax, num[i]);
            }

            return numOfTurns + 2;
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
    }
}
