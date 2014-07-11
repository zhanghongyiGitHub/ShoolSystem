using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace HelperTest
{


    /// <summary>
    ///这是 DataSetHelperTest 的测试类，旨在
    ///包含所有 DataSetHelperTest 单元测试
    ///</summary>
    [TestClass()]
    public class DataSetHelperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private DataTable initialSourceTable()
        {

            DataTable sourceTable = new DataTable();
            sourceTable.Columns.Add("A");
            sourceTable.Columns.Add("B");
            sourceTable.Columns.Add("C");
            DataRow dr = sourceTable.NewRow();
            dr["A"] = 1;
            dr["B"] = 2;
            dr["C"] = 3;
            sourceTable.Rows.Add(dr);
            dr = sourceTable.NewRow();
            dr["A"] = 1;
            dr["B"] = 2;
            dr["C"] = 4;
            sourceTable.Rows.Add(dr);
            dr = sourceTable.NewRow();
            dr["A"] = 2;
            dr["B"] = 2;
            dr["C"] = 5;
            sourceTable.Rows.Add(dr);
            dr = sourceTable.NewRow();
            dr["A"] = 2;
            dr["B"] = 2;
            dr["C"] = 6;
            sourceTable.Rows.Add(dr);
            dr = sourceTable.NewRow();
            dr["A"] = 3;
            dr["B"] = 3;
            dr["C"] = 7;
            sourceTable.Rows.Add(dr);
            dr = sourceTable.NewRow();
            dr["A"] = 1;
            dr["B"] = 2;
            dr["C"] = 8;
            sourceTable.Rows.Add(dr);

            return sourceTable;
        }

        /// <summary>
        ///Distinct 的测试
        ///</summary>
        [TestMethod()]
        public void DistinctTest()
        {
            DataSetHelper target = new DataSetHelper(); // TODO: 初始化为适当的值
            string tableName = "TestTable";
            DataTable sourceTable = initialSourceTable();
            string[] fieldNames = { "A", "B" };

            /***********************************重点************************************/
            sourceTable.Columns.Remove("C");
            /***************************************************************************/

            DataTable expected = null; // TODO: 初始化为适当的值
            DataTable actual;
            actual = target.Distinct(tableName, sourceTable, fieldNames);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///SelectDistinct 的测试
        ///</summary>
        [TestMethod()]
        public void SelectDistinctTest()
        {
            DataSetHelper target = new DataSetHelper(); // TODO: 初始化为适当的值
            string tableName = "TestTable";
            DataTable sourceTable = initialSourceTable();
            string[] fieldNames = { "A", "B" };
            DataTable expected = null; // TODO: 初始化为适当的值
            DataTable actual;
            actual = target.SelectDistinct(tableName, sourceTable, fieldNames);
            Assert.AreEqual(expected, actual);
        }
    }
}
