using Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandle
{
    public class GeneralPlaceholder
    {
        public static String getPlaceholderAndOutputBindData(Hashtable data, ref Hashtable bindData, String[] except = null, Boolean isAssorciate = false)
        {
            String placeholder = "";

            foreach (DictionaryEntry item in data)
            {
                if (StringHelper.arrayContains(item.Key.ToString(), except))
                {
                    continue;
                }

                if (isAssorciate)
                {
                    placeholder += String.Format("{0}=@{0},", item.Key);
                }
                else
                {
                    placeholder += String.Format("@{0},", item.Key);
                }
                
                bindData.Add(item.Key, item.Value);
            }

            return StringHelper.delBackChars(placeholder);
        }
        /*********************/

        public static String getPlaceholderAndOutputBindData(String key, String value, ref Hashtable bindData)
        {
            String placeholder = "";
            String hashtableKey = key;

            if(bindData.ContainsKey(key))
            {
                hashtableKey = getUniqueKey(bindData);
            }
            placeholder += String.Format("{0}=@{1}", key, hashtableKey);
            bindData.Add(hashtableKey, value);

            return placeholder;
        }

        

        public static String getPlaceholderAndOutputBindData(ref Hashtable bindData, String data)
        {
            String placeholder = "";

            foreach (String item in data.Split(','))
            {
                String key = getUniqueKey(bindData);

                placeholder += String.Format("@{0},", key);
                bindData.Add(key, item);
            }

            return StringHelper.delBackChars(placeholder);
        }

        private static String getUniqueKey(Hashtable bindData)
        {
            int i = 1;

            String key = "k" + i;

            while (bindData.ContainsKey(key))
            {
                key = "k" + ++i;
            }

            return key;
        }
    }
}
