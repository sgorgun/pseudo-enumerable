using System;
using System.Collections.Generic;

namespace Comparers
{
    public class IntegerByAbsComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            x = x < 0 ? -x : x;
            y = y < 0 ? -y : y;

            if (x == y)
            {
                return 0;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}