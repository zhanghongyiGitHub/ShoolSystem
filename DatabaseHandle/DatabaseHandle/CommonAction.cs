using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DatabaseHandle
{
    public class CommonAction
    {
        private String _tableName;
        protected IDatabase _db;

        public CommonAction(String tableName)
        {
            _tableName = tableName;
            _db = DB.useTable(_tableName);
        }

        public virtual String add(Hashtable data)
        {
            return _db.add(data);
        }

        public virtual int del(Hashtable data)
        {
            return _db.del(data);
        }

        public virtual int update(Hashtable data)
        {
            return _db.update(data);
        }

        public virtual DataTable query(Hashtable data)
        {
            return _db.query(data);
        }

        /// <summary>
        /// 有一条数据时更新,
        /// 有零条数据时添加,
        /// 有多条数据时报错.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual String addOrUpdate(Hashtable data, params string[] screen)
        {
            return _db.addOrUpdate(data, screen);
        }
    }
}
