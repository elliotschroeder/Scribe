using System.Collections.Generic;

namespace ScribeLibrary
{
    public interface IScribe 
    {
        T Read<T>(string record);
    
        /// <summary>
        /// Takes in a class of some type and writes out property values of those with
        /// the Fixed Length Field Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        void Write<T>(T records);

        /// <summary>
        /// Takes in a list of classes of some type and writes out the property values of those with 
        /// the Fixed Length Field Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        void Write<T>(IList<T> record);

    }
}