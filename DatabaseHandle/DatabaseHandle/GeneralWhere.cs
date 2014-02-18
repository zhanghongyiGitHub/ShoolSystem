using Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandle
{
    public class GeneralWhere
    {
        protected String _tablePrimaryKey;
        protected String _where;

        public Hashtable _bindData = new Hashtable();

        public Hashtable getBindData()
        {
            return _bindData;
        }

        public void mergeBindData(Hashtable bindData)
        {
            _bindData = HashTableHelper.mergeHashtable(_bindData, bindData);
        }

        public GeneralWhere(String tablePrimaryKey)
        {
            _tablePrimaryKey = tablePrimaryKey;

            _bindData = new Hashtable();
        }

        public String getWhere(params Object[] data)
        {
            _where = " WHERE ";

            if (0 == data.Length)
            {
                return "";
            }
            else if (String.IsNullOrEmpty(data[0].ToString()))
            {
                return "";
            }

            this.assembleWhere(data);

            return _where;
        }
        private void assembleWhere(Object[] data)
        {

            foreach(Object item in data)
            {
                Type type = item.GetType();

                switch (type.Name)
                {
                    case "String":
                        stringCondition((String)item);
                        break;
                    case "Hashtable":
                        hashtableCondition((Hashtable)item);
                        break;
                }
            }

            _where = _where.Remove(_where.Length - 4);
        }

        private void stringCondition(String data)
        {
            int temp = 0;

            if ("" == data)
            {
                _where = "";
            }
            else if (int.TryParse(data, out temp))
            {
                _where += String.Format("{0}=@{0}", _tablePrimaryKey);
                _bindData[_tablePrimaryKey] = data;
            }
            else
            {
                _where += data;
            }

            _where += " AND ";
        }

        private void hashtableCondition(Hashtable data)
        {
            String key = "";
            String value = "";
 
            foreach (DictionaryEntry item in data)
            {
                key = item.Key.ToString();
                value = item.Value.ToString();

                if (null != StringHelper.numbericSequence(value))
                {
                    numbericSequenceCondition(key, value);
                }
                else if(key.IndexOf(' ') > 0)
                {
                    keyWithOperatorHashtableCondition(key ,value);
                }
                else if (startOrEndWithPercent(value))
                {
                    fuzzyHashtableCondition(key, value);
                }
                else
                {
                    normalHashtableCondition(key, value);
                }

                _where += " AND ";
            }

            _where = _where.Remove(_where.Length - 5);

            if(_where == " WHERE ")
			{
				_where = ""; 
			}

            _where += " OR ";

        }
        private Boolean startOrEndWithPercent(String str)
        {
            str = str.Trim();
            int index = str.IndexOf('%');

            if(0 == index || index == str.Length - 1)
            {
                return true;
            }


            return false;
        }

        private void numbericSequenceCondition(String key, String primayKeySequence)
        {
            String placeholder = GeneralPlaceholder.getPlaceholderAndOutputBindData(ref _bindData, primayKeySequence);

            _where += String.Format("{0} IN ({1})", key, placeholder);
        }

        private void keyWithOperatorHashtableCondition(String key, String value)
        {
            String[] split = key.Split(' ');
            _where += String.Format("{0} @{1}", key, split[0]);
            _bindData[split[0]] = value;
        }

        private void fuzzyHashtableCondition(String key, String value)
        {
            _where += String.Format("{0} LIKE @{0}", key);
            _bindData[key] = value;
        }


        private void normalHashtableCondition(String key, String value)
        {
            _where += GeneralPlaceholder.getPlaceholderAndOutputBindData(key, value, ref _bindData);
        }
        
    }

    
}
