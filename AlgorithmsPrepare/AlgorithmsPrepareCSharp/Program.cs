using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    public delegate int AsynchronousCaller(int a, int b);

    internal class Program
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Demonstration for asynchronous method callback, callback method must have a parameter
        /// of type IAsyncReult.
        /// </summary>
        /// <param name="ar"></param>
        public static void CallbackMethod(IAsyncResult ar)
        {
            /*
            AsynchronousCaller ac = (ar as AsyncResult)?.AsyncDelegate as AsynchronousCaller;
            if (ac == null)
            {
                return;
            }

            string state = (string)ar.AsyncState;
            Console.WriteLine(state);

            int retVal = ac.EndInvoke(ar);
            Console.WriteLine(retVal);
            */
        }

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

            return a[startIndexA + midA - 1] < b[startIndexB + midB - 1] ? FindNthElementInArray(a, startIndexA + midA, endIndexA, b, startIndexB, endIndexB, n - midA) : FindNthElementInArray(a, startIndexA, endIndexA, b, startIndexB + midB, endIndexB, n - midB);
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
            public string Start { get; set; }
            public string Finish { get; set; }

            public Step(string start, string finish)
            {
                this.Start = start;
                this.Finish = finish;
            }
        }

        internal class Node
        {
            public string Id { get; set; }
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

        /// <summary>
        /// This is a demonstration for pass class type by reference. Remove "ref" keyword won't work.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        public static void SwapStrings(ref string s1, ref string s2)
        {
            var temp = s1;
            s1 = s2;
            s2 = temp;
        }

        /*
        /// <summary>
        /// This class this a test class for C# 6.0
        /// </summary>
        private class Rectangle
        {
            public double Width { get; } = 10.0;
            public double Height { get; } = 15.0;

            public Rectangle()
            {
                // Do nothing
            }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public double Area() => Width * Height;
            public double Perimeter() => 2 * (Width + Height);
        }
        */

        // Interesting example of Interface.
        private interface IHasText
        {
            string Text { get; }
        }

        public class Base : IHasText
        {
            string IHasText.Text
            {
                get
                {
                    Console.WriteLine("Get called: Base.IHasText.Text");
                    return "Base class!";
                }
            }

            public string Text
            {
                get
                {
                    Console.WriteLine("Get called: Base.Text");
                    // If somehow a derived class inherits from this base class, and invoke the .Text property 'as' a base class type,
                    // then '(this as IHasText)' will override the type of the base class and replace it with the implemented .Text by
                    // that derived class, this example illustrates how derived class is able to affect the behavior of its base class
                    // using 'Interface'.
                    return (this as IHasText).Text;
                }
            }
        }

        public class Derived : Base, IHasText
        {
            public new string Text {
                get
                {
                    Console.WriteLine("Get called: Derived.Text");
                    return "Derived class";
                }
            }
        }

        /*
        private class Product
        {
            public int Price { get; } = 10;
            public string Key { get; } = "Product Default";

            public Product()
            {
                // Do nothing
            }

            public Product(int price, string key)
            {
                this.Price = price;
                this.Key = key;
            }
        }
        */
        /*
         * 程序验证入口
         */
        private static void Main(string[] args)
        {
            //var s = Solution.CreateInstance();
            var lc = LeetCode.CreateInstance();
            var result = lc.SearchMatrix(new[,] { {1, 2, 3, 4, 5} }, 0);
            Console.WriteLine(result.ToString());

            //int threads = Environment.ProcessorCount;
            //int numCount = 500000000 * 2 / threads;

            //Task[] tasks = new Task[threads];

            //FasterTextWriterUtils fwu = new FasterTextWriterUtils();
            //MyTextWriter mtw = new MyTextWriter();

            //for (int i = 0; i < threads; ++i)
            //{
            //    var iCopy = i;
            //    int start = -500000000 + numCount * iCopy;
            //    int end = -500000000 + numCount * (iCopy + 1);
            //    tasks[i] = Task.Factory.StartNew(() =>
            //    {
            //        for (int j = start; j < end; ++j)
            //        {
            //            fwu.Write(mtw, j);
            //        }
            //    }, TaskCreationOptions.LongRunning); 
            //}

            //Task.WaitAll(tasks);

            /*
            var list = new List<Product>
            {
                new Product(),
                new Product(),
                new Product(12, "Product1"),
                new Product(12, "Product1"),
                new Product(13, "Product2"),
            };


            var retList = list.GroupBy(product => product.Key).Select(productGroup => new {productGroup.Key, TotalPrice = productGroup.Sum(product => product.Price)});
            int total = 0;
            foreach (var productType in retList)
            {
                Console.WriteLine(productType.Key);
                total += productType.TotalPrice;
            }
            Console.WriteLine(total);

            var rect = new Rectangle(20.0, 10.0);
            Console.WriteLine("The information of this rectangle is:\n" +
                              $"{nameof(rect)}:{rect.GetType().Name}, {rect.Height}, {rect.Width}, {rect.Area()}, {rect.Perimeter()}");
            */

            // Demostration for asynchronous method in C#
            // AsynchronousCaller ac = Add;
            // ac.BeginInvoke(3, 5, CallbackMethod, null);
        }
    }
}
