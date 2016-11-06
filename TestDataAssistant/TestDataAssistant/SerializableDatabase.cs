using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestDataAssistant
{
    [DataContract]
    public class SerializableDatabase
    {
        [DataMember(Name = "tables")]
        public Dictionary<string, SerializableTable> Tables { get; set; }

        public SerializableDatabase()
        {
            Tables = new Dictionary<string, SerializableTable>();
        }

        public void AddTable(string tableName, SerializableTable table)
        {
            Tables[tableName] = table;
        }
    }
}
