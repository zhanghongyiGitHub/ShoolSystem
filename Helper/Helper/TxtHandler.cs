using System;
using System.Collections.Generic;
using System.Linq;

namespace Helper
{
    public class TxtHandler
    {
        /// 净化id_suite,把001,001002,002净化为001,002;
        /// <summary>
        /// 净化id_suite,把001,001002,002净化为001,002;
        /// </summary>
        /// <param name="id_suite"></param>
        /// <returns></returns>
        public static void clean_ID_bulk(ref string id_suite)
        {
            ////净化前,检查用户查看宿舍,单位权限
            if (string.IsNullOrEmpty(id_suite))
            {
                return;
            }
            string[] id_suite_s = id_suite.Split(',');
            if (id_suite_s.Length > 1)
            {
                List<string> myid = new List<string>();
                List<string> reid = new List<string>();
                foreach (string id in id_suite_s)
                {
                    if (string.IsNullOrEmpty(id))
                        continue;
                    myid.Add(id);
                    reid.Add(id);
                }
                //不重复的
                myid = myid.Distinct().ToList();
                reid = reid.Distinct().ToList();
                foreach (string id in myid)
                {
                    //搜索存在上级,并删除之
                    List<string> find = reid.Where(p => p.StartsWith(id) & p != id).ToList();
                    if (find.Count() > 0)
                    {
                        foreach (string s in find)
                        {
                            for (int i = reid.Count - 1; i >= 0; i--)
                            {
                                if (reid[i] == s)
                                    reid.RemoveAt(i);
                            }
                        }
                    }
                }
                //重新组织字符串
                string re_id_suite = "";
                foreach (string s in reid)
                {
                    re_id_suite += s + ",";
                }
                if (reid.Count > 0)
                {
                    re_id_suite = re_id_suite.Remove(re_id_suite.Length - 1);
                    id_suite = re_id_suite;
                }
            }
        }

        public static string clearSpace(string txt)
        {
            txt = txt.Trim();
            txt = txt.Replace(" ", "");
            return txt;
        }
    }
}