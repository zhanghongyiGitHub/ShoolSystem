using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class SQLHelper
    {
        public static String isExistTableSQL(String sql, String tablename, String flag = "not")
        {
            String result = String.Format(@"
                if {0} exists (SELECT   name   FROM   sysobjects   where   name= '{1}')

                begin
                    {2}
                end",
            flag, tablename, sql);

            return result;
        }

    }
}
