using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class ListHelper
    {
        public static Boolean ListEquals<T>(List<T> a, List<T> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++ )
            {
                if(!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
            

            return true;
        }
    }
}
