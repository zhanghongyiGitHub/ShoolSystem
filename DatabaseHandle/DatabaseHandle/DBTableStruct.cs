using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandle
{
    public class DBTableStruct
    {
        private String _tableName, _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        private int _colOrder, _isIdentity, _primaryKey;

        public int ColOrder
        {
            get { return _colOrder; }
            set { _colOrder = value; }
        }

        public int IsIdentity
        {
            get { return _isIdentity; }
            set { _isIdentity = value; }
        }

        public int PrimaryKey
        {
            get { return _primaryKey; }
            set { _primaryKey = value; }
        }


    }
}
