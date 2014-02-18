using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentUnit.Common
{
    interface ITerm
    {
        DataTable getTerm();
        String getCurrentTerm();

        void validateTerm();
    }
}
