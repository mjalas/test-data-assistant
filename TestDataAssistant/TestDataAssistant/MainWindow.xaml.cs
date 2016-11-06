using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TestDataAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Server => GetBoxContent("ServerBox");

        private string Database => GetBoxContent("DatabaseBox");

        private string User => GetBoxContent("UserBox");

        private string Password => GetBoxContent("PasswordBox");

        private string ConnectionString => GetBoxContent("ConnectionStringBox");

        private string TableName => GetBoxContent("TableNameBox");

        private Label ErrorLbl => (Label)this.FindName("ErrorLabel");

        private string Query => GetBoxContent("QueryInput");

        private TextBox Output => (TextBox)this.FindName("OutputBox");

        private string GetBoxContent(string boxName)
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
            
        }


        private List<string> GetQueries(string queryInput)
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
    }
}

