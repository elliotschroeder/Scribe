using System;
using System.Collections.Generic;
using System.Text;

namespace Scribe
{
    public interface IScribe
    {
        T Read<T>(string record);
        void Write<T>(T record);

    }
}
