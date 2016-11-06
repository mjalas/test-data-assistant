using System;
using System.Data.SqlClient;

namespace TestDataAssistant
{
    public class DBCommunicator
    {
        public string ConnectionString { get; set; }



        public DBCommunicator(string connectionString)
        {
            ConnectionString = connectionString;
        }

        ~DBCommunicator() { }

        public TOP ExecuteQuery<TOP>(string query, Func<SqlConnection, string, TOP> executeQuery)
        {
            TOP result;
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    result = executeQuery(connection, query);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }



    }
}
