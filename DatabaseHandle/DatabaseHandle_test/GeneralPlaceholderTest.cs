using DatabaseHandle;
using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace DatabaseHandle_test
{
    
    
    /// <summary>
    ///这是 GeneralPlaceholderTest 的测试类，旨在
    ///包含所有 GeneralPlaceholderTest 单元测试
    ///</summary>
    [TestClass()]
    public class GeneralPlaceholderTest
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


        /// <summary>
        ///getPlaceholderAndOutputBindData 的测试
        ///</summary>
        [TestMethod()]
        public void getPlaceholderAndOutputBindData_string_Test()
        {
            
            String data = "a,b,c"; // TODO: 初始化为适当的值
            Hashtable bindData = new Hashtable(); // TODO: 初始化为适当的值
            bindData.Add("d", "D");

            String actual;
            actual = GeneralPlaceholder.getPlaceholderAndOutputBindData(ref bindData, data);

            Boolean flag = checkRamdonHashtable(actual, bindData, data);

            Assert.IsTrue(flag);

        }

        private Boolean checkRamdonHashtable(String randomPlaceholder, Hashtable bindData, String data)
        {
            String[] placeholderSequence = randomPlaceholder.Split(',');
            String[] dataSequence = data.Split(',');

            for(int i = 0; i<placeholderSequence.Length;i++)
            {
                placeholderSequence[i] = StringHelper.delfontChars(placeholderSequence[i]);

                if(bindData[placeholderSequence[i]].ToString() != dataSequence[i])
                {
                    return false;
                }
                
            }

            return true;
        }

        /// <summary>
        ///getPlaceholderAndOutputBindData 的测试
        ///</summary>
        [TestMethod()]
        public void getPlaceholderAndOutputBindData_item_Test()
        {
            String key = "a";
            String value = "A";

            Hashtable bindData = new Hashtable(); // TODO: 初始化为适当的值
            bindData.Add("d", "D");

            Hashtable bindDataExpected = new Hashtable(); // TODO: 初始化为适当的值
            bindDataExpected.Add("d", "D");
            bindDataExpected.Add("a", "A");
            

            string expected = "a=@a"; // TODO: 初始化为适当的值
            string actual;
            actual = GeneralPlaceholder.getPlaceholderAndOutputBindData(key, value, ref bindData);

            Boolean flag = HashTableHelper.hashTableEquals(bindDataExpected, bindData);

            Assert.IsTrue(flag);

            Assert.AreEqual(expected, actual);
           
        }
    }
}
