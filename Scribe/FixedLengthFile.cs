namespace ScribeLibrary
{
    /// <summary>
    /// Attribute that denotes a class a file to be written or parsed by Scribe
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class FixedLengthFile : System.Attribute
    {
        private readonly int _lineLength;

        /// <summary>
        /// Sets the maximum length for each line within a file for writing and parsing with Scribe
        /// </summary>
        /// <param name="lineLength"></param>
        public FixedLengthFile(int lineLength)
        {
            _lineLength = lineLength;
        }

        public int LineLength => _lineLength;
    }
}