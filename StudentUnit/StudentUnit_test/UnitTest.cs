using StudentUnit.Unit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Text;
using System.Data;
using Helper;
using System.Collections.Generic;

namespace StudentUnit_test
{
    
    [TestClass()]
    public class UnitTest
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

        private Unit target;

        //使用 TestInitialize 在运行每个测试前先运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            target = new Unit();
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestDB.dropTestTable();
            TestDB.createTestTable();
        }

        

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestDB.dropTestTable();
            TestDB.createTestTable();
        }

        [TestMethod()]
        public void add_Null_ParentID_Test()
        {
            
            Hashtable data = new Hashtable();

            data.Add("UnitName", "全校");
            data.Add("ParentID", "888");
            data.Add("UnitCode", "00");

            String expexted = "Can not find Parent!";
            
            try
            {
                target.add(data);
                Assert.Fail("null ParentID can not be accepted!");
            }
            catch(Exception e)
            {
                Assert.AreEqual(expexted, e.Message);
            }
        }

        [TestMethod()]
        public void addTest()
        {
            
            Hashtable data = new Hashtable();

            data.Add("UnitName", "全校");
            data.Add("ParentID", "000");
            data.Add("UnitCode", "00");

            String expected = "1"; // TODO: 初始化为适当的值
            String actual = target.add(data);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void addChildrenTest()
        {
            Hashtable data = new Hashtable();

            data.Add("UnitName", "某校区");
            data.Add("ParentID", "000001");
            data.Add("UnitCode", "01");

            String expected = "2";
            String actual = target.add(data);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void updateTest()
        {
            Hashtable data = new Hashtable();

            data.Add("id", 1);
            data.Add("UnitName", "某大学");
            data.Add("UnitID", "000001");
            data.Add("UnitCode", "00");

            int expected = 1;
            int actual = target.update(data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void updateCascade()
        {
            Hashtable data = new Hashtable();

            data.Add("id", 1);
            data.Add("UnitName", "某大学");
            data.Add("UnitID", "000001");
            data.Add("UnitCode", "00");
            data.Add("StudentType", "研究生");

            int expected = 2;
            int actual = target.update(data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void queryTest()
        {
            Hashtable data = new Hashtable();

            data.Add("UnitName", "某大学");
            data.Add("UnitID", "000001");
            data.Add("UnitCode", "00");

            List<Hashtable> actual = TypeConverterHelper.dataTableConverToHashtable(target.query(data));

            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod()]
        public void delTest()
        {
            Hashtable data = new Hashtable();

            data.Add("id", 1);
            data.Add("UnitName", "某大学");
            data.Add("UnitID", "000001");

            int expected = 1; // TODO: 初始化为适当的值
            int actual = target.del(data);
            Assert.AreEqual(expected, actual);
        }

    }
}
