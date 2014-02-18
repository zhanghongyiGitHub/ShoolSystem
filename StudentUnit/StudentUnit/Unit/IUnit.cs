using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentUnit.Unit
{
    public interface IUnit
    {
        String add(Hashtable data);
        int del(Hashtable data);
        int update(Hashtable data);
        DataTable query(Hashtable data);
    }
}
