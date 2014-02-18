using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Helper
{
    class ModelHelper
    {
        public Boolean Equals(object a, object b)
        {
            Type b_gettype = b.GetType();

            foreach (PropertyInfo property in a.GetType().GetProperties())
            {
                object a_value = property.GetValue(a, null);
                object b_value = b_gettype.GetProperty(property.Name).GetValue(b, null);
                if (a_value != b_value)
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean Assert_AreEqual_List<T>(List<T> a, List<T> b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                {
                    return false;
                }

            }

            return true;
        }

    }
}
