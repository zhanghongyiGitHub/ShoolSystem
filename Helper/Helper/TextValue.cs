using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class TextValue
    {
        string _text, _value;

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

    }
}
