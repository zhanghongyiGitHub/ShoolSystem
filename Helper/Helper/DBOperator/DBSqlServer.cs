using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Reflection;
//using Charge.Helper;

/*批量操作可以优化*/

namespace Laugh.Models
{
    public class SQLServer<T> : Database<T>
    {
        static protected String connectionString = WebConfigurationManager.ConnectionStrings["SchoolSystem"].ConnectionString;

        public SQLServer(String table)
        {
            tableName = table;
            primaryKey = WebConfigurationManager.AppSettings[tableName];
            primaryKey = null == primaryKey ? "id" : primaryKey;
        }

        public override SqlCommand select(String fields, String conditions = null, String[] values = null)
        {
            con = new SqlConnection(connectionString);

            sql = "SELECT " + fields + " FROM " + tableName;

            if (conditions != null)
            {
                sql += " WHERE " + conditions;
            }

            cmd = new SqlCommand(sql, con);

            _bindParameter(cmd, sql, values);

            return cmd;
        }

        //查
        public override List<T> getList(String fields = "*", String conditions = null, String[] values = null)
        {
            con = new SqlConnection(connectionString);

            sql = "SELECT " + fields + " FROM " + tableName;

            if (conditions != null)
            {
                sql += " WHERE " + conditions;
            }

            cmd = new SqlCommand(sql, con);

            _bindParameter(cmd, sql, values);

            return _buildList();
        }

        public override List<T> select(T obj)
        {
            con = new SqlConnection(connectionString);

            sql = "SELECT * FROM " + tableName + " WHERE ";
            sql += _getConditionsString(obj);

            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sql, con);

            String[] values = _getConditionsValues(obj);

            _bindParameter(cmd, sql, values);

            return _buildList();

        }

        private String _getConditionsString(T obj)
        {
            String conditionsString = "";
            PropertyInfo[] properties = typeof(T).GetProperties();

            //生成sql语句
            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    conditionsString += (p.Name + "=@" + p.Name + " AND ");
                }
            }
            conditionsString = Helper.TxtHandler.delBackChars(conditionsString, 4);

            return conditionsString;
        }

        private String[] _getConditionsValues(T obj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<String> values = new List<String>();

            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    values.Add(p.GetValue(obj, null).ToString());
                }
            }

            return values.ToArray();
        }

        //分页
        public override List<T> paging(int page, int size, String conditions = null, String[] values = null, String sort = "ASC")
        {
            String sql;
            String compare = ">";
            String extremum = "MAX";
            String filter = "";

            if ("DESC" == sort)
            {
                compare = "<";
                extremum = "MIN";
            }

            if (null != conditions)
            {
                filter = " WHERE " + conditions + " ";
            }

            if (1 == page)
            {
                sql = "SELECT TOP " + size + " * FROM " + tableName + filter + " ORDER BY " + primaryKey + " " + sort;
            }
            else
            {
                sql = "SELECT TOP " + size + " * FROM " + tableName +
                   " WHERE ( " + primaryKey + " " + compare + " (SELECT " + extremum + "(" + primaryKey + ") FROM (SELECT TOP " +
                   (page - 1) * size + " " + primaryKey + " FROM " + tableName + filter + " ORDER BY " + primaryKey + " " + sort + ") AS T)) ORDER BY " + primaryKey + " " + sort;
            }


            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sql, con);

            _bindParameter(cmd, sql, values);

            return _buildList();
        }

        private List<T> _buildList()
        {
            T item;
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<T> result = new List<T>();

            using (con)
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item = (T)Activator.CreateInstance(typeof(T), new Object[] { });
                    foreach (PropertyInfo p in properties)
                    {
                        Object value = reader[p.Name];
                        if (!Convert.IsDBNull(value))
                            p.SetValue(item, value, null);

                    }
                    result.Add(item);
                }
            }

            return result;
        }

        public override int getCount(String conditions = null, String[] values = null)
        {
            int count = -1;

            con = new SqlConnection(connectionString);

            sql = "SELECT COUNT(*) FROM " + tableName;

            if (conditions != null)
            {
                sql += " WHERE " + conditions;
            }

            cmd = new SqlCommand(sql, con);

            _bindParameter(cmd, sql, values);

            using (con)
            {
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }

            return count;
        }

        private SqlCommand _bindParameter(SqlCommand command, String sqlStr, String[] values)
        {
            if (null == sqlStr || null == values)
            {
                return command;
            }

            int start = 0, end = 0;
            String placeholder = "";
            List<String> placeholders = new List<string>();

            do
            {
                start = sqlStr.IndexOf('@', end);
                if (-1 == start)
                {
                    break;
                }

                end = sqlStr.IndexOf(' ', start);
                if (-1 == end)
                {
                    placeholder = sqlStr.Substring(start);
                }
                else
                {
                    placeholder = sqlStr.Substring(start, end - start);
                }

                placeholders.Add(placeholder);

            } while (-1 != end);

            if (placeholders.Count != values.Length)
            {
                throw new IndexOutOfRangeException("绑定参数出错");
            }

            for (int i = 0; i < values.Length; i++)
            {
                command.Parameters.AddWithValue(placeholders[i], values[i]);

            }

            return command;

        }


        //单个增
        public override int add(T obj, PropertyInfo[] properties = null)
        {
            int id = 0;
            String fields = "(";
            String values = "(";
            SqlCommand cmdQuery;
            String sqlQuery;

            con = new SqlConnection(connectionString);
            sql = "INSERT INTO " + tableName + " ";
            sqlQuery = "SELECT " + primaryKey + " FROM " + tableName + " WHERE ";

            //获取属性
            if (properties == null)
            {
                properties = typeof(T).GetProperties();
            }

            //生成sql语句
            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    sqlQuery += (p.Name + "=@" + p.Name + " AND ");
                    fields += (p.Name + ",");
                    values += ("@" + p.Name + ",");
                }
            }

            sqlQuery = Helper.TxtHandler.delBackChars(sqlQuery, 4);

            fields = Helper.TxtHandler.delBackChars(fields);
            fields += ")";
            values = Helper.TxtHandler.delBackChars(values);
            values += ")";

            sql += fields + "values" + values;

            cmd = new SqlCommand(sql, con);
            cmdQuery = new SqlCommand(sqlQuery, con);

            //绑定参数
            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    cmd.Parameters.AddWithValue(("@" + p.Name), p.GetValue(obj, null));
                    cmdQuery.Parameters.AddWithValue(("@" + p.Name), p.GetValue(obj, null));
                }

            }

            //执行插入操作
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                reader = cmdQuery.ExecuteReader();
                while (reader.Read())
                {
                    id = (int)reader[primaryKey];
                }

            }
            return id;
        }


        //批量插入
        public List<int> add(List<T> objs)
        {
            int id;
            List<int> idList = new List<int>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (T obj in objs)
            {
                id = add(obj, properties);
                idList.Add(id);
            }

            return idList;
        }

        //删除
        public override int delete(T obj, PropertyInfo[] properties = null)
        {
            int effectedRow = 0;

            sql = "DELETE FROM " + tableName;

            con = new SqlConnection(connectionString);

            //获取属性
            if (properties == null)
            {
                properties = typeof(T).GetProperties();
            }

            if (0 != properties.Count())
            {
                sql += " WHERE ";
            }

            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    sql += (p.Name + "=@" + p.Name + " AND ");
                }
            }

            sql = Helper.TxtHandler.delBackChars(sql, 4);
            cmd = new SqlCommand(sql, con);

            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    cmd.Parameters.AddWithValue(("@" + p.Name), p.GetValue(obj, null));
                }

            }
            //执行操作
            using (con)
            {
                con.Open();
                effectedRow = cmd.ExecuteNonQuery();

            }

            return effectedRow;

        }

        public int deleteList(List<T> objs)
        {
            int effectedRows = 0;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (T obj in objs)
            {
                delete(obj, properties);
                effectedRows++;
            }

            return effectedRows;
        }


        //改
        public int update(T obj, PropertyInfo[] properties = null)
        {
            int id = 0;
            bool keyFlag = false;
            String conditions = " WHERE " + primaryKey + "='";

            if (null == properties)
            {
                properties = typeof(T).GetProperties();
            }

            sql = "UPDATE " + tableName + " SET ";

            con = new SqlConnection(connectionString);

            foreach (PropertyInfo p in properties)
            {
                if (primaryKey == p.Name)
                {
                    conditions += p.GetValue(obj, null) + "'";
                    keyFlag = true;
                    id = (int)p.GetValue(obj, null);
                    continue;
                }

                if (null != p.GetValue(obj, null))
                {
                    sql += (p.Name + "=@" + p.Name + ", ");
                }
            }

            if (!keyFlag)
            {
                return -1;
            }

            sql = Helper.TxtHandler.delBackChars(sql, 2);
            sql += conditions;

            cmd = new SqlCommand(sql, con);

            foreach (PropertyInfo p in properties)
            {
                if (null != p.GetValue(obj, null))
                {
                    cmd.Parameters.AddWithValue(("@" + p.Name), p.GetValue(obj, null));
                }

            }

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return id;
        }


    }
}