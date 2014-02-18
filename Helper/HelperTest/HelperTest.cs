using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using Helper;
using System.Collections.Generic;
using System.Data;

namespace HelperTest
{
    [TestClass]
    public class MyHelperTest
    {
        [TestMethod]
        public void implode()
        {
            Hashtable data = new Hashtable();

            data.Add("a", "A");
            data.Add("b", "B");
            data.Add("c", "C");

            String result = StringHelper.implode(",", data);

            Assert.AreEqual("a,b,c", result);
        }


        [TestMethod()]
        public void numbericSequence_success_Test()
        {
            string str = "12,3,4,5";
            int[] expected = { 12, 3, 4, 5 }; // TODO: 初始化为适当的值
            int[] actual;

            actual = StringHelper.numbericSequence(str);

            Assert.IsTrue(ArrayHelper.arrayEquals(expected, actual));
        }

        [TestMethod()]
        public void numbericSequence_fail_Test()
        {
            string str = "ss,rr,sf,t";
            int[] expected = null; ; // TODO: 初始化为适当的值
            int[] actual;
            actual = StringHelper.numbericSequence(str);
            Assert.AreEqual(expected, actual);

        }

        public void delfontCharsTest()
        {
            string raw = "abcdef"; // TODO: 初始化为适当的值
            int len = 2; // TODO: 初始化为适当的值
            string expected = "cdef"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.delfontChars(raw, len);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void mergeHashtableTest()
        {
            Hashtable a = new Hashtable();
            a.Add("a", "A");
            a.Add("c", "C");

            Hashtable b = new Hashtable();
            b.Add("b", "B");
            b.Add("d", "D");

            Hashtable expected = new Hashtable();
            expected.Add("a", "A");
            expected.Add("c", "C");
            expected.Add("b", "B");
            expected.Add("d", "D");

            Hashtable actual;
            actual = HashTableHelper.mergeHashtable(a, b);

            Boolean flag = HashTableHelper.hashTableEquals(expected, actual);
            Assert.IsTrue(flag);

        }

        [TestMethod()]
        public void containsTest()
        {
            string expected = "haha"; // TODO: 初始化为适当的值
            string[] scope = { "hehe", "xixi", "haha" }; // TODO: 初始化为适当的值

            bool actual;
            actual = StringHelper.arrayContains(expected, scope);
            Assert.IsTrue(actual);

            expected = "buzai";
            actual = StringHelper.arrayContains(expected, scope);
            Assert.IsFalse(actual);

        }


        [TestMethod()]
        public void dataTableConverToHashtableTest()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");

            DataRow row = dt.NewRow();
            row["A"] = "a1";
            row["B"] = "b1";

            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "a2";
            row["B"] = "b2";

            dt.Rows.Add(row);

            List<Hashtable> expected = new List<Hashtable>();

            Hashtable item = new Hashtable();
            item.Add("A", "a1");
            item.Add("B", "b1");

            expected.Add(item);

            item = new Hashtable();
            item.Add("A", "a2");
            item.Add("B", "b2");

            expected.Add(item);

            List<Hashtable> actual;

            actual = TypeConverterHelper.dataTableConverToHashtable(dt);

            Boolean flag = HashTableHelper.hashTableListEquals(actual, expected);

            Assert.IsTrue(flag);


        }
    }
}
