namespace ScribeLibrary
{
    public class Field : IField
    {
        public string Value { get; set; }
        public int FieldLength { get; set; }
        public int FieldIndex { get; set; }
        public Alignment Alignment { get; set; }
        public Padding Padding { get; set; }
        public string CustomPaddingValue { get; set; }
    }
}
