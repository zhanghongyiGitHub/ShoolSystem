using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Laugh.Models
{
    public class DBFactory<T>
    {
        //private Database<T> db;

        static public Database<T> getDBOperator(String dbms, String tableName)
        {

            Type type = Type.GetType("Laugh.Models." + dbms, true);
            

            //设定泛型
            Type genericType = typeof(T);


            type = type.MakeGenericType(genericType);
            Database<T> DBOperator = (Database<T>)Activator.CreateInstance(type, new Object[] { tableName });
            
            return DBOperator;
        }
    }

    public class DBType
    {
        public static String SQLServer = "SQLServer`1";
    }

}

//Assembly 