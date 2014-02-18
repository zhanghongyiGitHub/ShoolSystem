using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    public class SQLDatabase_Class
    {
        private string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(this._ConnectionString))
                {
                    throw new Exception("没有设置连接串");
                    //return ConfigurationManager.ConnectionStrings["SchoolSystem"].ConnectionString;
                }
                else { return this._ConnectionString; }
            }
            set { this._ConnectionString = value; }
        }
        public SQLDatabase_Class()
        {

        }
        public SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
        }
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                int count = cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                return count;
            }
        }
        public int ExecuteNonQuery(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    int count = cmd.ExecuteNonQuery();
                    conn.Close();
                    return count;
                }
            }
        }
        public int ExecuteNonQuery(string cmdText, SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    foreach (SqlParameter par in pars)
                    {
                        cmd.Parameters.Add(par);
                    }
                    cmd.CommandType = CommandType.Text;
                    int count = cmd.ExecuteNonQuery();
                    conn.Close();
                    return count;
                }
            }
        }
        public DataSet ExecuteDataSet(string cmdText)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    cmd.Connection = conn;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    conn.Close();
                    conn.Dispose();
                    return ds;
                }
            }
        }
        public DataSet ExecuteDataSet(string cmdText, SqlParameter[] pars)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    foreach (SqlParameter par in pars)
                    {
                        cmd.Parameters.Add(par);
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    cmd.Connection = conn;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    conn.Close();
                    conn.Dispose();
                    return ds;
                }
            }
        }

        public DataTable ExecuteDataTable(string cmdText)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    cmd.Connection = conn;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    conn.Close();
                    conn.Dispose();
                    return ds.Tables[0];
                }
            }
        }
        public DataTable ExecuteDataTable(string cmdText, SqlParameter[] pars)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    foreach (SqlParameter par in pars)
                    {
                        cmd.Parameters.Add(par);
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    cmd.Connection = conn;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    conn.Close();
                    conn.Dispose();
                    return ds.Tables[0];
                }
            }
        }

        public object ExecuteScalar(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;

                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    conn.Close();
                    conn.Dispose();
                    return val;
                }
            }
        }

        public object ExecuteScalar(string cmdText, SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    foreach (SqlParameter par in pars)
                    {
                        cmd.Parameters.Add(par);
                    }
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    conn.Close();
                    conn.Dispose();
                    return val;
                }
            }
        }
    }
}