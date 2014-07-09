using DatabaseHandle;
using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace DatabaseHandle_test
{


    /// <summary>
    ///这是 DBVirtualTest 的测试类，旨在
    ///包含所有 DBVirtualTest 单元测试
    ///</summary>
    [TestClass()]
    public class DBTest
    {

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

        public static IDatabase _db;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            BuildTestDB.createTestTable();

            _db = DB.useTable("TestDB");
        }



        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            BuildTestDB.dropTestTable();
        }





        [TestInitialize()]
        public void MyTestInitialize()
        {

        }
        /**************************/

        [TestMethod()]
        public void dbTest()
        {
            String id = add();
            update(id);
            update_bywhere(id);
            query(id);
            add();
            max();
            min();
            del(id);
        }

        public String add()
        {
            Hashtable args = new Hashtable();

            args.Add("name", "A");
            args.Add("password", "A");
            args.Add("aaa", "a");

            String insertid = _db.add(args);

            Assert.IsTrue("0" != insertid);

            return insertid;
        }

        public void update(String id)
        {
            Hashtable args = new Hashtable();

            args.Add("id", id);
            args.Add("name", "C");
            args.Add("password", "C");

            int effectRows = _db.update(args);

            Assert.IsTrue(0 != effectRows);
        }

        public void update_bywhere(String id)
        {
            Hashtable args = new Hashtable();

            args.Add("name", "D");
            args.Add("password", "D");

            Hashtable condition = new Hashtable();
            condition.Add("id", id);

            int effectRows = _db.setSqlPart("where", condition).update(args);

            Assert.IsTrue(0 != effectRows);
        }

        public void query(String id)
        {
            List<Hashtable> expected = new List<Hashtable>();

            Hashtable item = new Hashtable();
            item.Add("id", id);
            item.Add("name", "D");
            item.Add("password", "D");

            expected.Add(item);


            Hashtable args = new Hashtable();

            args.Add("name", "D");
            args.Add("password", "D");

            DataTable result = _db.query(args);

            List<Hashtable> actual = TypeConverterHelper.dataTableConverToHashtable(result);

            Boolean flag = HashTableHelper.hashTableListEquals(expected, actual);

            Assert.IsTrue(flag);
        }

        public void max()
        {
            String excepted = "D";

            Object actual = _db.max("password");
            Assert.AreEqual(excepted, actual);


        }

        public void min()
        {
            String excepted = "A";

            Object actual = _db.min("password");
            Assert.AreEqual(excepted, actual);
        }

        public void del(String id)
        {
            Hashtable args = new Hashtable();
            args.Add("id", id);

            int effectRow = _db.del(args);

            Assert.AreEqual(1, effectRow);
        }
        [TestMethod()]
        public void add2table()
        {
            //db
        }


        /// <summary>
        ///DB 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void DBConstructorTest()
        {
            DB target = new DB();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
            //DB.
        }
    }
}
