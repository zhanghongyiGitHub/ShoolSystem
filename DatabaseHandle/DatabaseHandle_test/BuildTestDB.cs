using DatabaseHandle;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandle_test
{
    class BuildTestDB
    {

       
        public static void createTestTable()
        {
            String sql = SQLHelper.isExistTableSQL(@"CREATE TABLE TestDB(
                id INT IDENTITY  NOT NULL,
                name VARCHAR(50),
                password VARCHAR(50)
            )", "TestDB");


            DB.executeSQLDirect(sql);
        }

        public static void dropTestTable()
        {
            String sql = "DROP TABLE TestDB";

            DB.executeSQLDirect(sql);
        }

    }
}
