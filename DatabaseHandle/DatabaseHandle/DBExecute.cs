using Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandle
{
    public class DBExecute : DBFunction, DatabaseHandle.IDatabase
    {
        protected static List<DBTableStruct> _tableStruct = null;

        protected static DBExecute _singleton = null;

        private Method _medthodEnum;
        private SqlCommand _cmd;

        public DBExecute setSqlPart(String key, Object data)
        {
            key = key.ToLower();

            if (_sqlPart.ContainsKey(key))
            {
                _sqlPart[key] = data;
            }
            else
            {
                throw new Exception("The sqlPart is not exists!");
            }

            return this;
        }


        public static DBExecute useTable(String tableName, String connectionString = "")
        {
            if (null == _singleton)
            {
                _singleton = new DBExecute();
            }

            setConnectString(connectionString);

            _singleton.setTableName(tableName);

            return _singleton;
        }
        public static void setConnectString(String connectionString = null)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                try
                {
                    connectionString = System.Configuration.ConfigurationSettings.AppSettings["connectstring"];
                }
                catch(Exception e)
                {
                    throw new Exception("You must create an App.config! And write your ConnectString AS name connectstring in it!");
                }
                
            };

            _connectionString = connectionString;
        }
        private void setTableName(String tableName)
        {
            _tableName = tableName;

            if (null == _tableStruct || 0 == _tableStruct.Count)
            {
                this.getFieldsFromDB();
            }

            setPrimaykey();

            _generalWhere = new GeneralWhere(_primaryKey);
        }
        public void getFieldsFromDB()
        {
            String sql = @"SELECT D.Name as TableName, A.colorder AS ColOrder, A.name AS Name,  
                          COLUMNPROPERTY(A.ID,A.Name, 'IsIdentity') AS IsIdentity,  
                          CASE WHEN EXISTS
                          (SELECT 1
                          FROM dbo.sysobjects
                          WHERE Xtype = 'PK' AND Name IN
                          (SELECT Name
                          FROM sysindexes
                          WHERE indid IN
                          (SELECT indid
                          FROM sysindexkeys
                          WHERE ID = A.ID AND colid = A.colid)))  
                          THEN 1 ELSE 0 END AS [PrimaryKey]
                        FROM dbo.syscolumns A LEFT OUTER JOIN
                          dbo.systypes B ON A.xtype = B.xusertype INNER JOIN
                          dbo.sysobjects D ON A.id = D.id AND D.xtype = 'U' AND  
                          D.name <> 'dtproperties' LEFT OUTER JOIN
                          dbo.syscomments E ON A.cdefault = E.id 
                        left join sys.extended_properties g 
                        on a.id=g.class and 
                        a.colid=g.minor_id
                        left join sys.extended_properties f on d.id=f.class and f.minor_id=0
                        ORDER BY 1, 2
                        ";
            _conn = new SqlConnection(_connectionString);

            using (_conn)
            {
                SqlCommand cmd = new SqlCommand(sql, _conn);

                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                _tableStruct = TypeConverterHelper.dataTableToList<DBTableStruct>(ds.Tables[0]);
            }

        }
        private void setPrimaykey()
        {
            DBTableStruct structtable = _tableStruct.Where(p => p.TableName == _tableName & p.PrimaryKey == 1).FirstOrDefault();

            if (null == structtable)
            {
                structtable = _tableStruct.Where(p => p.TableName == _tableName & p.ColOrder == 1).FirstOrDefault();
            }

            if (null == structtable)
            {
                throw new Exception("The table [" + _tableName + "] does not exists!");
            }

            _primaryKey = structtable.Name;
        }
       

        public DBExecute open()
        {
            if (null == _conn)
            {
                throw new Exception("The connection has not been inited!");
            }
            connect();
            _conn.Open();
            _isOpen = true;

            return this;
        }

        public DBExecute close()
        {
            if (null == _conn)
            {
                throw new Exception("The connection has not been inited!");
            }

            _conn.Close();
            _isOpen = false;

            return this;
        }

        /*****************************************************/
        override protected Object execute(Method medthodEnum)
        {
            Object result;

            _medthodEnum = medthodEnum;
            connect();

            if (_isOpen)
            {
                result = executeBranch();
            }
            else
            {
                using (_conn)
                {
                    _conn.Open();
                    result = executeBranch();
                }
            }

            reset();

            return result;
        }

        private Object executeBranch()
        {

            _cmd = new SqlCommand();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = _sqlString;
            _cmd.Connection = _conn;

            bindData();

            switch (_medthodEnum)
            {
                case Method.query: 
                    return returnDataTable(); 
                case Method.del:
                case Method.update:
                case Method.setInc:
                case Method.setDec:
                    return returnEffectRows();
                case Method.total:
                case Method.max:
                case Method.min:
                    return returnScalar();       
                case Method.add:
                    return returnLastInsertID();
               
                default: throw new Exception("The Database function is not exists!");
                
            }
        }

        private void bindData()
        {
            foreach (DictionaryEntry item in _generalWhere._bindData)
            {
                _cmd.Parameters.AddWithValue("@" + item.Key.ToString(), item.Value.ToString());
            }
        }
        
        private DataTable returnDataTable()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(_cmd);
            sda.Fill(ds);

            return ds.Tables[0];
        }

        private int returnEffectRows()
        {
            return _cmd.ExecuteNonQuery();
        }

        private Object returnScalar()
        {
           return _cmd.ExecuteScalar();
        }

        private String returnLastInsertID()
        {
            return _cmd.ExecuteScalar().ToString();
        }

        private void reset()
        {
            resetSqlPart();

            _isOpen = false;
            _sqlString = "";
            _generalWhere._bindData = new Hashtable();
            
        }
    }
}
