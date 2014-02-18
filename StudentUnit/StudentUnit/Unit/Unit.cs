using DatabaseHandle;
using Helper;
using StudentUnit.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentUnit.Unit
{
    public class Unit: CommonAction, IUnit
    {
        private String[] cascadeStrings = { 
                                            "StudentType",
                                            "ManageSystem",
                                            "StateDate",
                                            "Stata",
                                            "DurationStudy"
                                          }; 

        private Term term = new Term();

        public Unit()
            : base("T_SCS_SchoolStructure")
        {
           
        }

        public override String add(Hashtable data)
        {
            isParentUnitExist(data);

            data.Add("Term", term.getCurrentTerm());
            data.Add("UnitID", generateUnitIDbyParentID(data["ParentID"].ToString()));

            return base.add(data);
        }
        private void isParentUnitExist(Hashtable data)
        {
            if (!data.ContainsKey("ParentID") || String.IsNullOrEmpty(data["ParentID"].ToString()))
            {
                throw new Exception("The ParentID can not be null!");
            }

            Hashtable parentIDHashtable = new Hashtable();
            parentIDHashtable.Add("UnitID", data["ParentID"]);

            if ("000" != data["ParentID"].ToString())
            {
                DataRow dr = _db.find(parentIDHashtable);
                if(null == dr)
                {
                    throw new Exception("Can not find Parent!");
                }
            }
        }
        private String generateUnitIDbyParentID(String parentID)
        {
            String maxSiblingObj = findMaxSiblingID(parentID);

            if (String.IsNullOrEmpty(maxSiblingObj))
            {
                return parentID + "001";
            }

            String maxSiblingStr = maxSiblingObj.ToString();

            String sub3 = maxSiblingStr.Substring(maxSiblingStr.Length - 3, 3);

            int SiblingCode = 0;
            int.TryParse(sub3, out SiblingCode);

            SiblingCode++;

            return parentID + SiblingCode.ToString().PadLeft(3, '0');

        }
        private String findMaxSiblingID(String parentID)
        {
            Hashtable condition = new Hashtable();
            condition.Add("parentID", parentID);

            return _db.setSqlPart("where", condition)
                      .max("UnitID")
                      .ToString();
        }

        public override int del(Hashtable data)
        {
            if (!hasChildren(data))
            {
                return base.del(data);
            }
            else
            {
                throw new Exception("存在下级单位,无法删除,请先清空下级单位!");
            }
        }
        private Boolean hasChildren(Hashtable data)
        {
            Hashtable condition = new Hashtable();

            condition.Add("parentID", data["UnitID"]);

            DataRow dr = _db.find(condition);

            if(null == dr)
            {
                return true;
            }

            return false;
        }

        public override int update(Hashtable data)
        {
            Hashtable condition = new Hashtable();
            condition.Add("UnitID", data["UnitID"]);
            data.Remove("UnitID");

            DataRow dr = _db.find(condition);
            Hashtable original = TypeConverterHelper.dataRowToHashtable(dr);

            List<String> changedKey = HashTableHelper.changedValueKey(original, data);

            Hashtable filtedData = filtCascadeUpdateField(changedKey, ref data);

            int effectedRows = _db.setSqlPart("where", condition).update(data);

            if (0 == filtedData.Count)
            {
                return effectedRows;
            }

            condition["UnitID"] = condition["UnitID"] + "%";

            return _db.setSqlPart("where", condition).update(filtedData);
        }
       
        private Hashtable filtCascadeUpdateField(List<String> keys, ref Hashtable original)
        {
            Hashtable filtedHashtable = new Hashtable();


            foreach (String key in keys)
            {
                if (StringHelper.arrayContains(key, this.cascadeStrings))
                {
                    filtedHashtable[key] = original[key];
                    original.Remove(key);
                }
            }

            return filtedHashtable;
        }

        public StringBuilder buildTree(String unitID)
        {
            return new StringBuilder();
        }
    }
}
