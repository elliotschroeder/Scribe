namespace ScribeLibrary
{
    public interface IField
    {
        string Value { get; set; }
        int FieldLength { get; set; }
        int FieldIndex { get; set; }
        Alignment Alignment { get; set; }
        Padding Padding { get; set; }
        string CustomPaddingValue { get; set; }
    }
}
