using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class NameValue
    {
        object name, value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public object Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
