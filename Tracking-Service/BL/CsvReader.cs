using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BL
{
    public class CsvReader : IReader
    {
        public IEnumerable<string> Read(string path)
        {
            using (var reader = new StreamReader(path, Encoding.Default))
            {
                string currentString;
                while ((currentString = reader.ReadLine()) != null)
                {
                    yield return currentString;
                }
            }
        }
    }
}