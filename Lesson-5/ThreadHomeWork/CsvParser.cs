using System.IO;
using System.Collections.Generic;


namespace ThreadHomeWork
{
    class CsvParser
    {
        public List<Dictionary<string, string>> Parse(string path, char delimiter)
        {
            var result = new List<Dictionary<string, string>>();
            string[] arStr = File.ReadAllLines(path);
            foreach (var record in arStr)
            {
                var fields = record.Split(delimiter);
                var recordItem = new Dictionary<string, string>();
                var i = 0;
                foreach (var field in fields)
                {
                    recordItem.Add(i.ToString(), field);
                    i++;
                }
                result.Add(recordItem);
            }
            return result;
        }

        public void Write(string path, List<Dictionary<string, string>> source)
        {
            using (StreamWriter fstream = new StreamWriter(path))
            {
                foreach (var rows in source)
                {
                    foreach (var column in rows)
                    {
                        fstream.Write($"{column.Value}; "); 
                    }
                    fstream.Write("\n");
                }
            }
        }
    }
}
