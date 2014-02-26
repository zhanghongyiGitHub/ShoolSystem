using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class StringHelper
    {
        public static String implode(String separator, Hashtable data)
        {
            String result = "";

            foreach (String key in data.Keys)
            {
                result += key + separator;
            }

            result = result.TrimEnd(separator.ToCharArray());

            return result;
        }

        public static String implode(String separator, String[] data)
        {
            String result = "";

            foreach (String item in data)
            {
                result += item + separator;
            }

            result = result.TrimEnd(separator.ToCharArray());

            return result;
        }

        public static String implode(String separator, String data)
        {
            string temp = "";

            string[] arrary = data.Split(',');
            foreach (string arr in arrary)
            {
                temp += separator + arr + separator + ",";
            }

            return delBackChars(temp);
        }

        static public String delBackChars(string raw, int len = 1)
        {
            if (raw.Length - len > 0)
            {
                return raw.Substring(0, raw.Length - len);
            }

            return "";
        }

        static public String delfontChars(string raw, int len = 1)
        {
            if (raw.Length - len > 0)
            {
                return raw.Substring(len, raw.Length - len);
            }

            return "";
        }

        public static int[] numbericSequence(String str)
        {
            int number = 0;

            String[] stringNmberSequence = str.Split(',');
            int[] result = new int[stringNmberSequence.Length];

            for (int i = 0; i < stringNmberSequence.Length; i++)
            {
                if (!int.TryParse(stringNmberSequence[i], out number))
                {
                    return null;
                }
                result[i] = number;
            }

            return result;
        }

        public static Boolean arrayContains(String expected, String[] scope)
        {
            if (null == scope)
            {
                return false;
            }

            foreach (String item in scope)
            {
                if (item.ToLower() == expected.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

    }
}
