using ScribeLibrary;
using System;
using Xunit;

namespace ScribeTests.TestObjects
{
    public class FieldFormatterTests
    {
        private readonly IFieldFormatter _testObject;

        public FieldFormatterTests()
        {
            _testObject = new FieldFormatter();
        }       

        [Theory]
        [InlineData("VALUE", "VALUE     ", Alignment.Left, Padding.None, 10, default(char))]
        [InlineData("VALUE", "     VALUE", Alignment.Right, Padding.None, 10, default(char))]
        public void WriteField_Alignment_No_Padding_Defaults_To_Spaces(string value, string expected, Alignment alignment, Padding padding, int fieldLength, char paddingChar)
        {
            var result = _testObject.WriteField(value, fieldLength, alignment, padding, paddingChar);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WriteField_Value_Larger_Than_Field_Length_Throws()
        {
            Assert.Throws<ArgumentException>(() => _testObject.WriteField("VALUE", 1, Alignment.None, Padding.None, default(char)));
        }

        [Fact]
        public void WriteField_Value_Matches_Field_Length_Does_Not_Pad_Value()
        {

            var result = _testObject.WriteField("9107", 4, Alignment.None, Padding.None, default(char));

            Assert.Equal("9107", result);
        }

        [Theory]
        [InlineData("VALUE     ", Padding.Spaces)]
        [InlineData("VALUE00000", Padding.Zeros)]
        public void WriteField_Left_Alignment_Pads_Value_To_Right_If_Value_Does_Not_Match_Field_Length(string expected, Padding padding)
        {

            var result = _testObject.WriteField("VALUE", 10, Alignment.Left, padding, default(char));

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("VALUE   ", 8, ' ', Alignment.Left)]
        [InlineData("VALUE000", 8, '0', Alignment.Left)]
        public void FormatField_Left_Alignment_Pads_Values_To_Right_If_Value_Does_Not_Match_Field_Length(string expected, int fieldLength, char padValue, Alignment alignment)
        {

            var result = _testObject.FormatField("VALUE", fieldLength, padValue, alignment);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("     VALUE", Padding.Spaces)]
        [InlineData("00000VALUE", Padding.Zeros)]
        public void WriteField_RightAlignment_Pads_Value_To_Left_If_Value_Does_Not_Match_Field_Length(string expected, Padding padding)
        {
            var result = _testObject.WriteField("VALUE", 10, Alignment.Right, padding, default(char));

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("     VALUE", 10, ' ', Alignment.Right)]
        [InlineData("00000VALUE", 10, '0', Alignment.Right)]
        public void FormatField_Right_Alignment_Pads_Values_To_Right(string expected, int fieldLength, char padValue, Alignment alignment)
        {

            var result = _testObject.FormatField("VALUE", fieldLength, padValue, alignment);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("VALUEXXXXX", 10, Alignment.Left, Padding.Custom, 'X')]
        [InlineData("ZZZZZVALUE", 10, Alignment.Right, Padding.Custom, 'Z')]
        public void FormatField_Pads_Values_To_With_Custom_Padding_Char(string expected, int fieldLength, Alignment alignment, Padding padding, char paddingChar)
        {

            var result = _testObject.WriteField("VALUE", fieldLength, alignment, padding, paddingChar);

            Assert.Equal(expected, result);
        }
    }
}
