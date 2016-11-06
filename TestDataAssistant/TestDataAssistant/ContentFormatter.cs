using System.Collections.Generic;
using System.Text;

namespace TestDataAssistant
{
    public class ContentFormatter
    {
        public static string ListAsColumns(IEnumerable<string> data)
        {
            var items = new List<string>();
            foreach (var item in data)
            {
                items.Add(item);
            }
            return string.Join(" | ", items);
        }

        public static string ListToJoinedString(IEnumerable<string> data, string separator)
        {
            var items = new List<string>();
            foreach (var item in data)
            {
                items.Add(item);
            }
            return string.Join(separator, items);
        }

        public static string RowsToJoinedText(List<List<string>> rows, string separator)
        {
            var builder = new StringBuilder();
            foreach (var row in rows)
            {
                var line = string.Join(separator, row);
                builder.Append(line + "\n");
            }
            return builder.ToString();
        }
    }
}
