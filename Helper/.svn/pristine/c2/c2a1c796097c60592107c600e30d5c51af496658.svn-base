using System;//as
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
//using System.Web.Script.Serialization;
namespace Helper
{
    public class MyJson
    {
        public static StringBuilder DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            if (dt.Rows.Count == 0)
            {
                jsonBuilder.Append("[]");
                return jsonBuilder;
            }

            jsonBuilder.Append("[");//转换成多个model的形式
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append('"');
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append('"');
                    jsonBuilder.Append(':');
                    jsonBuilder.Append('"');
                    //jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append('"');
                    jsonBuilder.Append(',');
                    //jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder;
            //string temp = jsonBuilder.ToString();
            //return temp;
        }
#if bak
        ///Json序列化,用于发送到客户端
        /// <summary>
        /// Json序列化,用于发送到客户端
        /// </summary>
        public static string ToJsJson(object item)
        {

            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(item.GetType());

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                serializer.WriteObject(ms, item);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
                //sb.Append(ms.ToArray());

                return sb.ToString();

            }

        }
        ///Json反序列化,用于接收客户端Json后生成对应的对象
        /// <summary>
        /// Json反序列化,用于接收客户端Json后生成对应的对象
        /// </summary>
        public static T FromJsonTo<T>(string jsonString)
        {

            System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

            System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString));

            T jsonObject = (T)ser.ReadObject(ms);

            ms.Close();

            return jsonObject;

        }

        /// LINQ返回DataTable类型
        /// <summary>
        /// LINQ返回DataTable类型
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="varlist"> </param>
        /// <returns> </returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null)
                return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        /// 序列化方法（带分页）
        /// <summary>
        /// 序列化方法（带分页）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string Serialize(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            int count = 0;
            try
            {
                count = Convert.ToInt32(dt.TableName);
            }
            catch //(System.Exception ex)
            {
                count = dt.Rows.Count;
            }
            string strReturn = "";
            if (count == 0)
            {
                strReturn = "{\"totalCount\":0,\"data\":[]}";
            }
            else
            {
                strReturn = ConventToJson(list, count);
            }
            return strReturn;
        }
        /// 转换为JSON对象
        /// <summary>
        /// 转换为JSON对象
        /// </summary>
        /// <returns></returns>
        public static string ConventToJson<T>(List<T> list, int count)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string strJson = serializer.Serialize(list);
            //strJson = strJson.Substring(1);
            //strJson = strJson.Insert(0, "{totalCount:" + count + ",data:[");
            //strJson += "}";

            //return strJson;
            return "";
        }
        /// 不需要分页
        /// <summary>
        /// 不需要分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="flag">false</param>
        /// <returns></returns>
        //public static string Serialize(DataTable dt, bool flag)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Dictionary<string, object> result = new Dictionary<string, object>();
        //        foreach (DataColumn dc in dt.Columns)
        //        {
        //            result.Add(dc.ColumnName, dr[dc].ToString());
        //        }
        //        list.Add(result);
        //    }
        //    return serializer.Serialize(list); ;
        //}
        /// DataTableToJson 调用Serialize(DataTable dt, bool flag)
        /// <summary>
        /// DataTableToJson 调用Serialize(DataTable dt, bool flag)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public static string DataTableToJson(DataTable dt)
        //{
        //    return Serialize(dt, false);
        //}
#endif
    }

#if DataTable2Json_bak
    public static class DataTable2Json
    {
        public static string Obj2Json<T>(T data)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }
        public static Object Json2Obj(String json, Type t)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(t);
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {

                    return serializer.ReadObject(ms);
                }
            }
            catch
            {
                return null;
            }
        }

        public static string DataTable2Json(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            // jsonBuilder.Append("{"); 
            //jsonBuilder.Append(dt.TableName.ToString());  
            jsonBuilder.Append("[");//转换成多个model的形式
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            //  jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        public static T Json2Obj<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
    
#endif
    public static class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> toList<T>(this DataTable dt)
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