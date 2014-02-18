using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HelperTest
{
    
    [TestClass()]
    public class HashTableHelperTest
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

        [TestMethod()]
        public void changedValueKeyTest()
        {
            Hashtable compared = new Hashtable();
            compared.Add("a", "A");
            compared.Add("b", "B");
            compared.Add("c", "C");

            Hashtable data = new Hashtable();
            data.Add("a", "A1");
            data.Add("b", "B");
            data.Add("c", "C2");


            List<String> expected = new List<String>();
            expected.Add("a");
            expected.Add("c");

            List<String> actual = HashTableHelper.changedValueKey(compared, data);

            Boolean flag =  ListHelper.ListEquals(expected, actual);

            Assert.IsTrue(flag);
           
        }
    }
}
