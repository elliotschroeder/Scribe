using System;

namespace ScribeLibrary
{
    public interface IFieldFormatter
    {
        /// <summary>
        /// Takes a string value and returns a formatted string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fieldLength"></param>
        /// <param name="alignment"></param>
        /// <param name="padding"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        string WriteField(string value, int fieldLength, Alignment alignment, Padding padding, char paddingChar);

        /// <summary>
        /// Formats a string with given alignment and padding
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fieldLength"></param>
        /// <param name="paddingValue"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        string FormatField(string value, int fieldLength, char paddingValue, Alignment alignment);

        /// <summary>
        /// Formats a file in string form and inserts line endings
        /// </summary>
        /// <param name="file"></param>
        /// <param name="lineLength"></param>
        /// <returns></returns>
        string FormatLineEndings(string file, int lineLength);
    }

    public class FieldFormatter : IFieldFormatter
    {
        public string FormatField(string value, int fieldLength, char paddingValue, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Left:
                    return value.PadRight(fieldLength, paddingValue);
                case Alignment.Right:
                    return value.PadLeft(fieldLength, paddingValue);
                default:
                    return value;
            }
        }

        public string FormatLineEndings(string file, int lineLength)
        {
            var numberOfNewLineCharcters = file.Length / lineLength;

            var index = lineLength;
            for (int i = 0; i < numberOfNewLineCharcters; i++)
            {
                file = file.Insert(index, Environment.NewLine);
                index += lineLength + Environment.NewLine.Length;
            }

            return file;
        }

        public string WriteField(string value, int fieldLength, Alignment alignment, Padding padding, char paddingChar)
        {
            if (value.Length > fieldLength)
            {
                throw new ArgumentException();
            }

            if (fieldLength - value.Length > 0)
            {
                switch (padding)
                {
                    case Padding.Spaces:
                        return FormatField(value, fieldLength, ' ', alignment);
                    case Padding.Zeros:
                        return FormatField(value, fieldLength, '0', alignment);
                    case Padding.Custom:
                        if (paddingChar == default(char))
                        {
                            paddingChar = ' ';
                        }
                        return FormatField(value, fieldLength, paddingChar, alignment);
                    default:
                        return FormatField(value, fieldLength, ' ', alignment);
                }
            }
            else
            {
                return value;
            }
        }


    }
}
