using System.Data.SqlClient;

namespace TestDataAssistant
{
    public abstract class SqlServerReader<TOP>
    {

        public virtual TOP FetchData(string connectionString, string query)
        {
            var dbCommunicator = new DBCommunicator(connectionString);
            var result = dbCommunicator.ExecuteQuery(query, RunQuery);
            return result;
        }

        protected abstract TOP RunQuery(SqlConnection connection, string query);
    }
}
