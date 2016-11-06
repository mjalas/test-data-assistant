using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TestDataAssistant
{
    public class ObjectSerializer<TO>
    {

        public static TO LoadFromFile(string filename)
        {
            var serializer = new DataContractJsonSerializer(typeof(TO));
            using (Stream stream = new FileStream(filename, FileMode.Open))
            {
                var obj = (TO)serializer.ReadObject(stream);
                return obj;
            }
        }

        public static void StoreToFile(TO obj, string filename)
        {
            var ser = new DataContractJsonSerializer(typeof(TO), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true, "    "))
                {
                    ser.WriteObject(writer, obj);
                    writer.Flush();
                }
            }
        }

        public static string ToJSON(TO obj)
        {
            string output = "";
            var path = Directory.GetCurrentDirectory();
            if (path.Contains("Debug")) path = path.Replace("\\Debug", "");
            if (path.Contains("\\bin")) path = path.Replace("\\bin", "");
            path += "\\data\\tmp.json";
            var ser = new DataContractJsonSerializer(typeof(TO), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true, "    "))
                {
                    ser.WriteObject(writer, obj);
                    writer.Flush();

                }

                //stream.Position = 0;


            }
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    output = reader.ReadToEnd();
                }
            }
            if (File.Exists(path)) File.Delete(path);

            return output;
        }
    }
}
