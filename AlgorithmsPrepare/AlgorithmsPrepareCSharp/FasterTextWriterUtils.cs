using System;
using System.Globalization;

namespace AlgorithmsPrepareCSharp
{
    public interface ITextWriter
    {
        void Write(string text);
        void Write(string text, int startIndex, int endIndex);
        void Write(char[] chars);
        void Write(char[] chars, int startIndex, int endIndex);
    }

    internal class MyTextWriter : ITextWriter
    {
        public void Write(string text)
        {
            // Do nothing
        }

        public void Write(string text, int startIndex, int endIndex)
        {
            // Do nothing
        }

        public void Write(char[] chars)
        {
            // Do nothing
        }

        public void Write(char[] chars, int startIndex, int endIndex)
        {
            // Do nothing
        }
    }

    public interface ITextWriterUtils
    {
        void Write(ITextWriter writer, int value);
    }

    internal class SampleTextWriterUtils : ITextWriterUtils
    {
        public void Write(ITextWriter writer, int value)
        {
            writer.Write(value.ToString(CultureInfo.InvariantCulture));
        }
    }

    class FasterTextWriterUtils: ITextWriterUtils
    {
        private static readonly char[] Dict = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        [ThreadStatic] 
        private static char[] _chars;

        public void Write(ITextWriter writer, int value)
        {
            if (_chars == null)
            {
                _chars = new char[11];
                _chars[10] = '0';
            }

            bool isPositive = value > 0;
            if (!isPositive)
            {
                value = -value;
            }

            int i = 10;
            for (; i != -1 && value != 0; --i)
            {
                _chars[i] = Dict[value % 10];
                value /= 10;
            }
            if (!isPositive)
            {
                _chars[i] = '-';
            }
            writer.Write(_chars, isPositive ? i + 1 : i, 10);
        }
    }
}
