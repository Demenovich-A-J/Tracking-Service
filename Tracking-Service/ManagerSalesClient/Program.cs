using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManagerSalesClient
{
    class Program
    {
        private static readonly Regex SaleInfoRegex = new Regex(@"(\d{2}.\d{2}.\d{4} \d{2}:\d{2}:\d{2});(\w+);(\w+);(\d*)", RegexOptions.Compiled);
        private static readonly Regex TitleInfoRegex = new Regex(@"(\w+)_(\d{2}\d{2}\d{4})", RegexOptions.Compiled);


        static void Main(string[] args)
        {
            //(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d
            
            //var a = DateTime.Now.ToString();
            //a += ";asd;fff;2356";
            var d = "SeconName_22112015";

            var s = TitleInfoRegex.IsMatch(d);

            var match = TitleInfoRegex.Match(d);
        }
    }
}
