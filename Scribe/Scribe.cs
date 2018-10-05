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
        private readonly IFieldFormatter _fieldFormatter;

        /// <summary>
        /// Creates Scribe with ability to write to a stream
        /// </summary>
        /// <param name="textWriter"></param>
        public Scribe(TextWriter textWriter, IFieldFormatter formatter)
        {
            _textWriter = textWriter;
            _fieldFormatter = formatter;
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
            Dictionary<int, string> fieldsToWrite = new Dictionary<int, string>();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<FixedLengthField>();

                if (attr == null)
                {
                    continue;
                }

                var fieldValue = prop.GetValue(record) == null ? string.Empty : prop.GetValue(record).ToString();

                fieldsToWrite.Add(attr.FieldOrder, _fieldFormatter.WriteField(fieldValue, attr.FieldLength, attr.Alignment, attr.Padding, attr.CustomPaddingValue));
            }

            foreach (var field in fieldsToWrite.OrderBy(d => d.Key))
            {
                file += field.Value;
            }

            var fixedFileAttr = record.GetType().GetCustomAttribute<FixedLengthFile>();

            if (fixedFileAttr != null)
            {
                file = _fieldFormatter.FormatLineEndings(file, fixedFileAttr.LineLength);
            }

            _textWriter.Write(file);

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