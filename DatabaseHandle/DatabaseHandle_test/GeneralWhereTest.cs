using DatabaseHandle;
using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace DatabaseHandle_test
{
    
    
    /// <summary>
    ///这是 GeneralWhereTest 的测试类，旨在
    ///包含所有 GeneralWhereTest 单元测试
    ///</summary>
    [TestClass()]
    public class GeneralWhereTest
    {
        private GeneralWhere target;
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

        [TestInitialize()]
        public void MyTestInitialize()
        {
            target = new GeneralWhere("id");
        }
        

        [TestMethod()]
        public void emptyCondtionTest()
        {

            String args = "";
            string expected = ""; // TODO: 初始化为适当的值
            string actual;
            actual = target.getWhere(args);
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void stringCondtionTest()
        {
            String args = "size > 15 AND size < 25";
            string expected = " WHERE size > 15 AND size < 25 "; // TODO: 初始化为适当的值
            string actual;
            actual = target.getWhere(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void singlePrimaryKeyCondtionTest()
        {
            String args = "5207";
            string expected = " WHERE id=@id "; // TODO: 初始化为适当的值
            string actual;
            actual = target.getWhere(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void normalHashtableCondtionTest()
        {
            Hashtable args = new Hashtable();
            args.Add("a", "A");
            args.Add("b", "B");
            args.Add("c", "C");

            Hashtable bindDataExpected = new Hashtable();
            bindDataExpected.Add("a", "A");
            bindDataExpected.Add("b", "B");
            bindDataExpected.Add("c", "C");
            bindDataExpected.Add("d", "D");

            Hashtable temp = new Hashtable();
            temp.Add("d", "D");
            target.mergeBindData(temp);

            string expected = " WHERE a=@a AND b=@b AND c=@c"; // TODO: 初始化为适当的值
            string actual;
            actual = target.getWhere(args);
            Assert.AreEqual(expected, actual);

            Boolean flag = HashTableHelper.hashTableEquals(target.getBindData(), bindDataExpected);
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void primayKeySequenceConditionTest()
        {
            Hashtable args = new Hashtable();
            args.Add("id", "0022,055,067");
            args.Add("a", "小红,小明今天踩到大便!");

            Hashtable bindDataExpected = new Hashtable();
            bindDataExpected.Add("k1", "0022");
            bindDataExpected.Add("k2", "055");
            bindDataExpected.Add("k3", "067");
            bindDataExpected.Add("a", "小红,小明今天踩到大便!");

            String actual = target.getWhere(args);
            Assert.AreEqual(" WHERE id IN (@k1,@k2,@k3) AND a=@a", actual);

            Boolean flag = HashTableHelper.hashTableEquals(target.getBindData(), bindDataExpected);
            Assert.IsTrue(flag);
        }
        

        [TestMethod()]
        public void orConditionTest()
        {
            Hashtable args1 = new Hashtable();
            args1.Add("name", "a");
            args1.Add("password", "b");

            Hashtable args2 = new Hashtable();
            args2.Add("name", "b");

            String actual = target.getWhere(args1, args2);
            String expected = " WHERE password=@password AND name=@name OR name=@k1";
            Assert.AreEqual(expected, actual);

            Hashtable bindDataExpected = new Hashtable();
            bindDataExpected.Add("name", "a");
            bindDataExpected.Add("password", "b");
            bindDataExpected.Add("k1", "b");

            Boolean flag = HashTableHelper.hashTableEquals(target.getBindData(), bindDataExpected);
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void ConditionsWithOperatorTest()
	    {
            Hashtable data = new Hashtable();
            data.Add("start_date >", "2012-12-12");
            data.Add("end_date <", "2013-12-12");
		    
		    String actual = target.getWhere(data);

            Assert.AreEqual(" WHERE end_date < @end_date AND start_date > @start_date", actual);
	    }

        [TestMethod()]
        public void testFuzzyConditionTest()
	    {
            Hashtable data = new Hashtable();
            data.Add("name", "小明%");
            data.Add("nickname", "牛人%");
		    
		    String actual = target.getWhere(data);

            Assert.AreEqual(" WHERE nickname LIKE @nickname AND name LIKE @name", actual);
	    }
    }
}
