using System;
using System.Data;
namespace DatabaseHandle
{
    public interface IDatabase
    {
        DBExecute close();
        string getPrimartKey();
        DBExecute open();
        DBExecute setSqlPart(string key, object data);

        //
        String add(System.Collections.Hashtable data);
        int del(System.Collections.Hashtable data = null);
        System.Data.DataRow find(System.Collections.Hashtable data = null);
        System.Data.DataRow findUniqueData(System.Collections.Hashtable data = null);
        System.Data.DataTable query(System.Collections.Hashtable data = null);
        int setDec(string field, int value = 1);
        int setInc(string field, int value = 1);
        int total(System.Collections.Hashtable data);
        int update(System.Collections.Hashtable data);
        Object max(String field);
        Object min(String field);
        
    }
}
