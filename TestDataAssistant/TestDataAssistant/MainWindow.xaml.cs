using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private Label ErrorLbl
        {
            get { return (Label)this.FindName("ErrorLabel"); }
        }

        private string Query { get { return GetBoxContent("QueryInput"); } }

        private TextBox Output { get { return (TextBox)this.FindName("OutputBox"); } }

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
                connectionString = string.Format("server={0};database={1};uid={2};password={3}", Server, Database, User, Password);
            }
            return connectionString;
        }

        private bool IsNull(string property)
        {
            return string.IsNullOrEmpty(property);
        }

        private void ReadSchemaButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

