using System.Collections.Generic;
using System.IO;
using System.Text;
using BL.Reader.Interfaces;

namespace BL.Reader
{
    public class CsvReader : IReader
    {
        public IEnumerable<string> Read(string path)
        {
            using (var reader = new StreamReader(path, Encoding.GetEncoding("windows-1251")))
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