using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public static partial class HashTableHelper
    {
        public static Boolean hashTableEquals(Hashtable a, Hashtable b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            foreach (DictionaryEntry item in a)
            {
                if (!b.ContainsKey(item.Key))
                {
                    return false;
                }
                else if (item.Value.ToString() != b[item.Key].ToString())
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean hashTableListEquals(List<Hashtable> a, List<Hashtable> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++)
            {
                if (!hashTableEquals(a[i], b[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static Hashtable mergeHashtable(params Hashtable[] a)
        {
            Hashtable result = new Hashtable();

            foreach (Hashtable hash in a)
            {
                foreach (DictionaryEntry item in hash)
                {
                    if (!result.ContainsKey(item.Key))
                    {
                        result.Add(item.Key, item.Value);
                    }
                }
            }
            return result;
        }

        public static List<String> changedValueKey(Hashtable compared, Hashtable data)
        {
            List<String> changedValueKeys = new List<String>();

            //if (compared.Count != data.Count)
            //{
            //    throw new Exception("The compared Hashtables do not have the same Keys!");
            //}

            foreach (DictionaryEntry item in compared)
            {
                if (!data.Contains(item.Key))
                {
                    continue;
                }

                if (item.Value.ToString() != data[item.Key].ToString())
                {
                    changedValueKeys.Add(item.Key.ToString());
                }

            }

            return changedValueKeys;
        }
        /// <summary>
        /// 过滤属于key的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Hashtable filtrate(this Hashtable data, params string[] key)
        {
            foreach (string k in key)
            {
                if (data.ContainsKey(k))
                    data.Remove(k);
            }
            return data;
        }
        /// <summary>
        /// 筛选属于key的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Hashtable screen(this Hashtable data, params string[] key)
        {
            Hashtable temp = new Hashtable();
            foreach (string k in key)
            {
                if (data.ContainsKey(k))
                {
                    temp.Add(k, data[k]);
                }
            }
            return temp;
        }

    }
}
