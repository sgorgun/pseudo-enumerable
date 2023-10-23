using System;
using System.Collections.Generic;

namespace Comparers
{
    public class StringByLengthComparer : IComparer<string>
    {
        public int Compare(string x, string y) => x?.Length.CompareTo(y?.Length) ?? -1;
    }
}