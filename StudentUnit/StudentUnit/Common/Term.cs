using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentUnit.Common
{
    class Term: ITerm
    {

        public System.Data.DataTable getTerm()
        {
            throw new NotImplementedException();
        }

        public string getCurrentTerm()
        {
            return "2013-2014";
        }

        public void validateTerm()
        {
            throw new NotImplementedException();
        }
    }


}
