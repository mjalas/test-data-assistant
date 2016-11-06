using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestDataAssistant
{
    [DataContract]
    public class DataObject
    {
        [DataMember(Name = "properties")]
        public Hashtable Properties { get; set; }

        public DataObject()
        {
            Properties = new Hashtable();
        }

        public void LoadProperties(Dictionary<string, string> data)
        {
            foreach (var item in data)
            {
                Properties[item.Key] = item.Value;
            }
        }
    }
}
