namespace ScribeLibrary
{
    /// <summary>
    /// Attribute that denotes property as a field within a file to be parsed or written by Scribe
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class FixedLengthField : System.Attribute
    {
        private readonly int _fieldLength;
        private readonly Alignment _alignment;
        private readonly Padding _paddingValue;
        private readonly string _customPaddingValue;
        private readonly int _order;

        /// <summary>
        /// Sets order of a field in relation to a class/classes, length of Field, Alignment, Padding, and Custom Padding value.
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="FieldLength"></param>
        /// <param name="Alignment"></param>
        /// <param name="Padding"></param>
        /// <param name="CustomPaddingValue"></param>
        public FixedLengthField(int fieldOrder, int fieldLength, Alignment alignment, Padding padding, string customPaddingValue)
        {
            _order = fieldOrder;
            _fieldLength = fieldLength;
            _alignment = alignment;
            _paddingValue = padding;
            _customPaddingValue = customPaddingValue;
        }

        /// <summary>
        /// Sets order and field length of record. Defaults Padding to Spaces and Alignment to Left
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="FieldLength"></param>
        public FixedLengthField(int fieldOrder, int fieldLength)
        {
            _order = fieldOrder;
            _fieldLength = fieldLength;
            _alignment = Alignment.Left;
            _paddingValue = Padding.Spaces;
        }

        /// <summary>
        /// Sets Field Length, Alignment, and Padding. Note, will not set Custom Padding value
        /// </summary>
        /// <param name="FieldLength"></param>
        /// <param name="Alignment"></param>
        /// <param name="Padding"></param>
        /// <param name="Order"></param>
        public FixedLengthField(int fieldOrder, int fieldLength, Alignment alignment, Padding padding)
        {
            _order = fieldOrder;
            _fieldLength = fieldLength;
            _alignment = alignment;
            _paddingValue = padding;
        }

        public int FieldOrder => _order;
        public int FieldLength => _fieldLength;
        public Alignment Alignment => _alignment;
        public Padding Padding => _paddingValue;
        public string CustomPaddingValue => _customPaddingValue;


    }
}
