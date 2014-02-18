using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text;

namespace Helper
{
    public class TreeJsonClass
    {
        StringBuilder json = new StringBuilder();
        PropertyInfo[] nodeProperties = null;
        public TreeJsonClass()
        {

        }
        string ID = "ID";
        string FartherID = "FartherID";
        string replace_id = "id";
        string replace_text = "text";
        public TreeJsonClass(string ID_key, string FartherID_key, string replace_id_key, string replace_text_key)
        {
            ID = ID_key;
            FartherID = FartherID_key;
            replace_id = replace_id_key;
            replace_text = replace_text_key;
        }

        private void constructorJson<T>(List<T> nodeList, T node)
        {
            if (hasChild(nodeList, node))
            {
                jsonAppend<T>(node, false);
                jsonAppendChild<T>(nodeList, node);
            }
            else
            {
                jsonAppend<T>(node, true);
            }
        }
        private void jsonAppend<T>(T node, bool leaf)
        {
            if (nodeProperties == null)
                nodeProperties = node.GetType().GetProperties();
            Type type = node.GetType();
            for (int i = 0; i < nodeProperties.Length; i++)
            {
                object propertyValueObj = type.GetProperty(nodeProperties[i].Name).GetValue(node, null);
                string propertyValue = propertyValueObj == null ? "" : propertyValueObj.ToString();
                string propertyName = nodeProperties[i].Name;

                if (nodeProperties[i].Name.ToLower() == "id")
                {
                    propertyName = "id_1";
                }
                if (nodeProperties[i].Name.ToLower() == "text")
                {
                    propertyName = "text_1";
                }
                if (nodeProperties[i].Name == replace_id)
                {
                    propertyName = "id";
                }
                if (nodeProperties[i].Name == replace_text)
                {
                    propertyName = "text";
                }

                if (i == 0)
                {
                    json.Append("{" + string.Format("'{0}':", propertyName));
                    json.Append(string.Format("'{0}'", propertyValue));
                }
                else
                {
                    json.Append(string.Format(",'{0}':", propertyName));
                    json.Append(string.Format("'{0}'", propertyValue));
                }
            }
            if (leaf)
            {
                json.Append(",'leaf':'true'},");
            }
            else
            {
                json.Append(",'leaf':'false'");
            }
        }
        private void jsonAppendChild<T>(List<T> nodeList, T node)
        {
            json.Append(",'children':[");
            List<T> childList = getChildList(nodeList, node);
            foreach (T childNode in childList)
            {
                constructorJson(nodeList, childNode);
            }
            json.Append("]},");
        }

        private bool hasChild<T>(List<T> nodeList, T node)
        {
            return getChildList(nodeList, node).Count > 0 ? true : false;
        }
        private List<T> getChildList<T>(List<T> nodeList, T node)
        {
            List<T> li = new List<T>();

            PropertyInfo t = node.GetType().GetProperty(ID);
            if (t == null)
                throw new Exception("没有上级代码属性:" + FartherID);
            string nodeID = t.GetValue(node, null).ToString();

            foreach (T n in nodeList)
            {
                string nodeFartherID = n.GetType().GetProperty(FartherID).GetValue(n, null).ToString();
                if (nodeFartherID == nodeID)
                {
                    li.Add(n);
                }
            }
            return li;
        }

        public String getJson<T>(List<T> nodeList, T node)
        {
            json = new StringBuilder();
            constructorJson(nodeList, node);
            String jsonDate = json.ToString();
            jsonDate = ("[" + jsonDate + "]").Replace(",]", "]");

            int indexOfChildren = jsonDate.IndexOf("children");
            indexOfChildren += "children':".Length;
            jsonDate = jsonDate.Remove(jsonDate.Length - 2, 2);
            jsonDate = jsonDate.Substring(indexOfChildren);
            return jsonDate;
        }
        public String getJson<T>(List<T> nodeList, string topNodeID)
        {
            T node = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo nodeProperty = node.GetType().GetProperty(ID);
            switch (nodeProperty.PropertyType.Name)
            {
                case "Int32":
                    nodeProperty.SetValue(node, Int32.Parse(topNodeID), null);
                    break;
                default:
                    nodeProperty.SetValue(node, topNodeID, null);
                    break;
            }
            return getJson<T>(nodeList, node);
        }
    }
    public class TreeJsonClassModel
    {
        public string id { get; set; }
        public string fartherid { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }
}