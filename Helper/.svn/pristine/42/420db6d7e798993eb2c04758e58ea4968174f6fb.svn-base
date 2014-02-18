using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Helper
{
    public class MyReflection
    {
        public static string getPropertyValue(object obj, string PropertyName)
        {
            Type parentType = obj.GetType();
            PropertyInfo parentPro = parentType.GetProperty(PropertyName);
            if (parentPro != null)
            {
                object parentValue = parentPro.GetValue(obj, null);
                if (parentValue != null)
                {
                    return parentValue.ToString();
                }
            }
            return "";
        }
    }
}
