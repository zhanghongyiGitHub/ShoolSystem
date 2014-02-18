using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Helper
{
    public class TypeConverterHelper
    {
        public static List<Hashtable> dataTableConverToHashtable(DataTable data)
        {
            List<Hashtable> hashtableList = new List<Hashtable>();


            foreach (DataRow row in data.Rows)
            {
                Hashtable item = new Hashtable();

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    item.Add(data.Columns[i].ColumnName, row[i]);
                }

                hashtableList.Add(item);
            }

            return hashtableList;
        }

        public static Hashtable dataRowToHashtable(DataRow data)
        {
            Hashtable result = new Hashtable();
            
            DataTable table = data.Table;

            foreach(DataColumn column in table.Columns)
            {
                result.Add(column.ColumnName, data[column.ColumnName]);
            }

            return result;
        }

        public static List<T> dataTableToList<T>(DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
    }
}
