using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TestDataAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Server => GetTextBoxContent("ServerBox");

        private string Database => GetTextBoxContent("DatabaseBox");

        private string User => GetTextBoxContent("UserBox");

        private string Password => GetTextBoxContent("PasswordBox");

        private string ConnectionString => GetTextBoxContent("ConnectionStringBox");

        private string TableName => GetTextBoxContent("TableNameBox");

        private Label ErrorLbl => (Label)this.FindName("ErrorLabel");

        private string Query => GetTextBoxContent("QueryInput");

        private TextBox Output => (TextBox)this.FindName("OutputBox");

        private string GetTextBoxContent(string boxName)
        {
            var box = (TextBox)this.FindName(boxName);
            if (box == null) return null;
            return string.IsNullOrEmpty(box.Text) ? null : box.Text;
        }

        public MainWindow()
        {
            
        }


        public void QueryInput_OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {

        }

        
        private void ExecuteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var connectionString = GetConnectionString();
                var reader = new DataReader();
                var queries = GetQueries(Query);
                var database = new SerializableDatabase();
                foreach (var query in queries)
                {
                    var table = reader.FetchData(connectionString, query);
                    var queryParts = query.Split(' ').ToList();
                    var index = queryParts.IndexOf("FROM");
                    var tableName = queryParts.ElementAt(index + 1);
                    database.AddTable(tableName, table);
                }

                Output.Text = ObjectSerializer<SerializableDatabase>.ToJSON(database);
            }
            catch (ArgumentNullException exception)
            {
                ErrorLbl.Content = exception.Message;
            }
        }


        private List<string> GetQueries(string queryInput) // TODO: Remove expectation that a query is only on one row! - MJ 11.06.2016
        {
            if (string.IsNullOrEmpty(queryInput))
            {
                throw new ArgumentNullException("No query given!");
            }
            var queryList = new List<string>();
            var queries = queryInput.Split('\n');
            foreach (var query in queries)
            {
                var tmp = query;
                if (!tmp.EndsWith(";"))
                {
                    tmp += ";";
                }
                queryList.Add(tmp);
            }
            return queryList;
        }

        private string GetConnectionString()
        {
            var connectionString = ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                if (IsNull(Server) || IsNull(Database) || IsNull(User) || IsNull(Password)) throw new ArgumentNullException("Please enter content to all connection fields or to connection string field!");
                connectionString = $"server={Server};database={Database};uid={User};password={Password}";
            }
            return connectionString;
        }

        private bool IsNull(string property)
        {
            return string.IsNullOrEmpty(property);
        }

        private void ReadSchemaButton_OnClick(object sender, RoutedEventArgs e)
        {
            string connectionString;
            try
            {
                connectionString = GetConnectionString();
            }
            catch (ArgumentNullException exception)
            {
                ErrorLbl.Content = exception.Message;
                return;
            }


            var reader = new SchemaReader();
            if (string.IsNullOrEmpty(TableName))
            {
                ErrorLbl.Content = "No table was specified!";
                return;
            }
            try
            {
                var result = reader.GetColumnNames(connectionString, TableName);
                Output.Text = ContentFormatter.ListAsColumns(result);
            }
            catch (Exception exception)
            {
                ErrorLbl.Content = exception.Message;
            }

        }

        private void Quit_MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

