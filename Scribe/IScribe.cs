using System;
using System.Collections.Generic;

namespace ScribeLibrary
{
    public interface IScribe
    {
        T Read<T>(string record);
        void Write<T>(T records);
        void Write<T>(IList<T> record);

    }
}