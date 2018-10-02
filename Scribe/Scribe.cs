using System;

namespace Scribe
{
    public class Scribe : IScribe
    {
        public T Read<T>(string record)
        {
            throw new NotImplementedException();
        }

        public void Write<T>(T record)
        {
            throw new NotImplementedException();
        }
    }
}
