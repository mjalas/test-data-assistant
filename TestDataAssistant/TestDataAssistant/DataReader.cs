using System.Collections.Generic;
using System.Data.SqlClient;

namespace TestDataAssistant
{
    public class DataReader : SqlServerReader<SerializableTable>
    {
        private int _maxRowAmount = 50;

        protected override SerializableTable RunQuery(SqlConnection connection, string query)
        {
            var className = "";
            var table = new SerializableTable();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                int id = 0;

                using (var reader = command.ExecuteReader())
                {
                    var counter = 0;
                    while (reader.Read())
                    {

                        if (counter >= _maxRowAmount) break;
                        var row = new Dictionary<string, string>();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            if (i == 0)
                            {
                                id = reader.GetInt32(i);
                                className = reader.GetName(i).Replace("RID", "");
                                table.ClassName = className;
                            }
                            var obj = reader.GetValue(i);
                            var propertyName = string.Format("{0}.{1}", className, reader.GetName(i));
                            row.Add(propertyName, obj.ToString());
                        }
                        table.AddRow(id, row);
                        counter++;
                    }
                }
            }
            return table;
        }
    }
}
