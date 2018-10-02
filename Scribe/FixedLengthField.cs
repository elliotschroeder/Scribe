namespace Scribe
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FixedLengthField : System.Attribute
    {
        private readonly string _fieldLength;
        private readonly Alignment _alignment;
        private readonly Padding _paddingValue;
        private readonly string _customPaddingValue;

        /// <summary>
        /// Sets Length of Field, Alignment, Padding, and Custom Padding value.
        /// </summary>
        /// <param name="fieldLength"></param>
        /// <param name="alignment"></param>
        /// <param name="padding"></param>
        /// <param name="customPaddingValue"></param>
        public FixedLengthField(string fieldLength, Alignment alignment, Padding padding, string customPaddingValue)
        {
            _fieldLength = fieldLength;
            _alignment = alignment;
            _paddingValue = padding;
            _customPaddingValue = customPaddingValue;
        }

        /// <summary>
        /// Sets Field Length of Record. Defaults Padding to Spaces and Alignment to Left
        /// </summary>
        /// <param name="fieldLength"></param>
        public FixedLengthField(string fieldLength)
        {
            _fieldLength = fieldLength;
            _alignment = Alignment.Left;
            _paddingValue = Padding.Spaces;
        }

        /// <summary>
        /// Sets Field Length, Alignment, and Padding. Note, will not set Custom Padding value
        /// </summary>
        /// <param name="fieldLength"></param>
        /// <param name="alignment"></param>
        /// <param name="padding"></param>
        public FixedLengthField(string fieldLength, Alignment alignment, Padding padding)
        {
            _fieldLength = fieldLength;
            _alignment = alignment;
            _paddingValue = padding;
        }


    }
}
