namespace TestDataAssistant
{
    public class QueryCreator
    {
        
        public static string ColumnNamesQuery(string tableName)
        {
            return string.Format(
                    "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '{0}';",
                    tableName);
        }

    }
}
