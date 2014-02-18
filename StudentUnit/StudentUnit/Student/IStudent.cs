using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StudentUnit.Student
{
    interface IStudent
    {
        String add(Hashtable data);
        int del(Hashtable data);
        int update(Hashtable data);
        DataTable query(Hashtable data);

        
    }
}
