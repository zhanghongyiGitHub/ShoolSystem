using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Helper.Class
{
    public class JsonHandler<T>
    {
        private Object jsonObject;
        private Type type;

        public JsonHandler(Object json)
        {
            jsonObject = json;
            type = jsonObject.GetType();
        }

        public Object getValue(String name)
        {
            PropertyInfo p = type.GetProperty(name);

            return p.GetValue(jsonObject, null);
        }
    }
}