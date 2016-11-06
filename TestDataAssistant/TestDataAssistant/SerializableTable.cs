using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestDataAssistant
{
    [DataContract]
    public class SerializableTable
    {
        [DataMember(Name = "rows")]
        public Dictionary<int, Hashtable> Rows { get; set; }
        [DataMember(Name = "idIndex")]
        public Dictionary<string, int> IDIndex { get; set; }

        [DataMember(Name = "className")]
        public string ClassName { get; set; }

        public SerializableTable()
        {
            Rows = new Dictionary<int, Hashtable>();
            IDIndex = new Dictionary<string, int>();
        }

        public void AddRow(int rid, Hashtable databaseObject)
        {
            Rows[rid] = databaseObject;
        }

        public void AddIDIndex(string id, int rid)
        {
            IDIndex[id] = rid;
        }

        public void AddRow(int rid, Dictionary<string, string> row)
        {
            var obj = new DataObject();
            obj.LoadProperties(row);
            Rows[rid] = obj.Properties;
        }

        public void AddRows(Dictionary<int, Dictionary<string, string>> rows)
        {
            foreach (var row in rows)
            {
                AddRow(row.Key, row.Value);
            }
        }
    }
}
