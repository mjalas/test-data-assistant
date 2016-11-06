using System.Collections.Generic;
using System.Data.SqlClient;

namespace TestDataAssistant
{
    public class SchemaReader : SqlServerReader<IEnumerable<string>>
    {

        public IEnumerable<string> GetColumnNames(string connectionString, string tableName)
        {
            var query = QueryCreator.ColumnNamesQuery(tableName);
            var result = FetchData(connectionString, query);
            return result;
        }

        protected override IEnumerable<string> RunQuery(SqlConnection connection, string query)
        {
            var columnNames = new List<string>();

            using (SqlCommand command = connection.CreateCommand())
            {
                //command.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = 'Usuarios' and t.type = 'U'";                
                command.CommandText = query;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columnNames.Add(reader.GetString(0));
                    }
                }
            }
            return columnNames;
        }

    }
}
