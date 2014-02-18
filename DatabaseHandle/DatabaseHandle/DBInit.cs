using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections;

namespace DatabaseHandle
{
    /// <summary>
    /// Initialization.useTable('test').setConnectionString(constr).del();
    /// </summary>
    public class DBInit
    {
        public static SqlConnection _conn = null;
        protected static String _connectionString;

        protected String _tableName;
        protected String _primaryKey;
        protected GeneralWhere _generalWhere;
        protected Hashtable _sqlPart;

        protected Boolean _isOpen = false;


        protected DBInit()
        {
            _generalWhere = new GeneralWhere(getPrimartKey());
            resetSqlPart();
        }

        protected void resetSqlPart()
        {
            _sqlPart = new Hashtable();

            _sqlPart.Add("where", "");
            _sqlPart.Add("field", "");
            _sqlPart.Add("join", "");
            _sqlPart.Add("order", "");
            _sqlPart.Add("limit", "");
            _sqlPart.Add("page", "");
            _sqlPart.Add("group", "");
            _sqlPart.Add("having", "");
            
        }
        
        

        public static void connect()
        {
            if(null == _conn)
            {
                _conn = new SqlConnection(_connectionString);
            }

            _conn.ConnectionString = _connectionString;

        }
        public String getPrimartKey()
        {
            return this._primaryKey;
        }

        
    }
}
