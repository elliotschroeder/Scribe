using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScribeLibrary
{
    public class Scribe : IScribe
    {
        private readonly TextWriter _textWriter;

        /// <summary>
        /// Creates Scribe with ability to write to a stream
        /// </summary>
        /// <param name="textWriter"></param>
        public Scribe(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public Scribe()
        {

        }

        public T Read<T>(string record)
        {
            throw new NotImplementedException();
        }

        public void Write<T>(T record)
        {
            var file = string.Empty;
            PropertyInfo[] properties = record.GetType().GetProperties();
            Dictionary<int, string> dict = new Dictionary<int, string>();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<FixedLengthField>();

                if (attr == null)
                {
                    continue;
                }

                var fieldValue = prop.GetValue(record) == null ? string.Empty : prop.GetValue(record).ToString();

                dict.Add(attr.FieldOrder, WriteField(fieldValue, attr.FieldLength, attr.Alignment, attr.Padding));
            }

            foreach (var field in dict.OrderBy(d => d.Key))
            {
                //_textWriter.Write(field.Value);
                file += field.Value;
            }

            var fixedFileAttr = record.GetType().GetCustomAttribute<FixedLengthFile>();

            if (fixedFileAttr != null)
            {
                file = AddLineEndingsToFixedFile(file, fixedFileAttr.LineLength);
            }

            _textWriter.Write(file);

        }

        private string AddLineEndingsToFixedFile(string file, int lineLength)
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

        public string WriteField(string value, int fieldLength, Alignment alignment, Padding padding)
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
                    default:
                        return FormatField(value, fieldLength, ' ', alignment);
                }
            }
            else
            {
                return value;
            }

        }

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

        public void Write<T>(IList<T> records)
        {
            foreach (var record in records)
            {
                Write(record);
            }
        }
    }
}