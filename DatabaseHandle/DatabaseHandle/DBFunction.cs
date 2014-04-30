using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Helper;

namespace DatabaseHandle
{
    public class DBFunction : DBInit
    {

        protected enum Method { add, del, update, query, find, setInc, setDec, max, min, total };

        protected String _sqlString = "";

        /************************CRUD******************************/
        public String add(Hashtable data)
        {
            String fields = getFieldsFromData(data);

            String placeholder = GeneralPlaceholder.getPlaceholderAndOutputBindData(data, ref _generalWhere._bindData);

            _sqlString = String.Format("INSERT INTO {0} ({1}) VALUES ({2}) SELECT CAST(IDENT_CURRENT('{0}') AS VARCHAR(100))", _tableName, fields, placeholder);

            return (String)execute(Method.add);
        }

        public int del(Hashtable data = null)
        {
            String where = "";

            if (null != data)
            {
                where = _generalWhere.getWhere(data);
            }
            else
            {
                where = _generalWhere.getWhere(_sqlPart["where"]);
            }

            if (String.IsNullOrEmpty(where))
            {
                throw new Exception("Delete condition can not be null!");
            }

            _sqlString = String.Format("DELETE FROM {0}{1}", _tableName, where);

            return (int)execute(Method.del);

        }

        public int update(Hashtable data)
        {
            if (0 == data.Count)
            {
                throw new Exception("The update dataList can not be empty!");
            }

            String where = "";
            String[] primaryKey = { getPrimartKey() };


            String placeholder = GeneralPlaceholder.getPlaceholderAndOutputBindData(data, ref _generalWhere._bindData, primaryKey, true);

            if ("" != _sqlPart["where"].ToString())
            {
                where = _generalWhere.getWhere(_sqlPart["where"]);
            }
            else
            {
                String pri = getPrimartKey();

                where = String.Format(" WHERE {0}=@{0}", pri);
                _generalWhere._bindData[pri] = data[pri];
            }

            _sqlString = String.Format("UPDATE {0} SET {1} {2}", _tableName, placeholder, where);

            return (int)execute(Method.update);

        }

        /// <summary>
        /// 有一条数据时更新,
        /// 有零条数据时添加,
        /// 有多条数据时报错.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public String addOrUpdate(Hashtable data, params string[] screen)
        {
            DataTable result = query(data.screen(screen));
            int count = result.Rows.Count;
            if (count == 0)
                return add(data);
            else if (count == 1)
            {
                string primartKey = getPrimartKey();
                if (!data.ContainsKey(primartKey))
                { data.Add(primartKey, result.Rows[0][primartKey]); }
                else
                { data[primartKey] = result.Rows[0][primartKey]; }
                return (update(data).ToString());
            }
            else
                throw new Exception("The data row is greater then one!");
        }

        public DataRow find(Hashtable data = null)
        {
            DataTable result;

            result = query(data);

            if (0 == result.Rows.Count)
            {
                return null;
            }

            return result.Rows[0];
        }

        public DataRow findUniqueData(Hashtable data = null)
        {
            DataTable result;

            result = query();

            if (1 != result.Rows.Count)
            {
                throw new Exception("The data is not unique!");
            }

            return result.Rows[0];
        }

        public DataTable query(Hashtable data = null)
        {
            String where = "";

            if (null == data)
            {
                where = _generalWhere.getWhere(_sqlPart["where"]);
            }
            else
            {
                where = _generalWhere.getWhere(data);
            }

            _sqlString = combineCondition(where);

            return (DataTable)execute(Method.query);
        }
        private String combineCondition(String where)
        {
            String sql = "";

            String fields = String.IsNullOrEmpty(_sqlPart["field"].ToString()) ? "*" : _sqlPart["field"].ToString();
            String join = genetateJoin();
            String order = generateOrderBy();
            String limit = generateLimit();
            String group = String.IsNullOrEmpty(_sqlPart["group"].ToString()) ? "" : " GROUP BY " + _sqlPart["group"].ToString();
            String having = String.IsNullOrEmpty(_sqlPart["having"].ToString()) ? "" : " HAVING " + _sqlPart["having"].ToString();

            sql = String.Format("SELECT {0} FROM {1}{2}{3}{4}{5}{6}{7}",
                            fields,
                            _tableName,
                            join,
                            where,
                            group,
                            having,
                            order,
                            limit
                         );
            return sql;
        }
        private String genetateJoin()
        {
            if ("" == _sqlPart["join"].ToString())
            {
                return "";
            }
            else
            {
                return " LEFT JOIN " + StringHelper.implode(" LEFT JOIN ", (String[])_sqlPart["join"]);
            }
        }
        private String generateOrderBy()
        {
            if (String.IsNullOrEmpty(_sqlPart["order"].ToString()))
            {
                return String.Format(" ORDER BY {0}.{1} ASC", _tableName, getPrimartKey());
            }
            else
            {
                return " ORDER BY " + _sqlPart["order"].ToString();
            }
        }
        private String generateLimit()
        {
            return "";
        }

        public int total(Hashtable data)
        {
            String where = "";

            if (null == data)
            {
                where = _generalWhere.getWhere(_sqlPart["where"]);
            }
            else
            {
                where = _generalWhere.getWhere(data);
            }

            _sqlString = String.Format("SELECT COUNT(*) as count FROM {0}{1}", _tableName, where);

            return (int)execute(Method.total);
        }

        public int setInc(String field, int value = 1)
        {
            String expectedfields = String.Format("{0},{1}", getPrimartKey(), field);

            String where = _generalWhere.getWhere(_sqlPart["where"]);

            _sqlString = String.Format("UPDATE {0} SET {1}={1}+{2} {3}", _tableName, field, value, where);

            return (int)execute(Method.setInc);
        }

        public int setDec(String field, int value = 1)
        {
            String expectedfields = String.Format("{0},{1}", getPrimartKey(), field);

            String where = _generalWhere.getWhere(_sqlPart["where"]);

            _sqlString = String.Format("UPDATE {0} SET {1}={1}-{2} {3}", _tableName, field, value, where);

            return (int)execute(Method.setInc);
        }

        public Object max(String field)
        {
            String where = _generalWhere.getWhere(_sqlPart["where"]);
            _sqlString = String.Format("SELECT MAX({0}) as MAX FROM {1} {2}", field, _tableName, where);

            return execute(Method.max);
        }

        public Object min(String field)
        {
            String where = _generalWhere.getWhere(_sqlPart["where"]);
            _sqlString = String.Format("SELECT MIN({0}) as MIN FROM {1} {2}", field, _tableName, where);

            return execute(Method.min);
        }

        public static DataSet executeSQLDirect(String sql)
        {
            DBExecute.setConnectString();

            _conn = new SqlConnection(_connectionString);

            using (_conn)
            {
                SqlCommand cmd = new SqlCommand(sql, _conn);

                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
        }

        /************************辅助函数******************************/
        private String getFieldsFromData(Hashtable data)
        {
            String fields = StringHelper.implode(",", data);


            return fields;
        }

        virtual protected Object execute(Method medthodcode)
        {
            return new Object();
        }


    }
}
