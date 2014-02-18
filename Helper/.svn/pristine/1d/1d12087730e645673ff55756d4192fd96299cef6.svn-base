using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Laugh.Models
{
    public abstract class Database<T>
    {
        static protected String primaryKey;
        static protected String tableName { get; set; }

        protected SqlConnection con;

        public SqlConnection Con
        {
            get { return con; }
            set { con = value; }
        }
        protected SqlCommand cmd;

        public SqlCommand Cmd
        {
            get { return cmd; }
            set { cmd = value; }
        }

        protected SqlDataReader reader;

        public SqlDataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        protected String sql;


        public abstract int add(T obj, PropertyInfo[] properties = null);
        public abstract int delete(T obj, PropertyInfo[] properties = null);
        public abstract SqlCommand select(String fields, String conditions = null, String[] values = null);
        public abstract List<T> select(T obj);
        public abstract List<T> getList(String fields = "*", String conditions = null, String[] values = null);
        public abstract int getCount(String conditions = null, String[] values = null);
        public abstract List<T> paging(int page, int size, String conditions = null, String[] values = null, String sort = "ASC");
    }
}