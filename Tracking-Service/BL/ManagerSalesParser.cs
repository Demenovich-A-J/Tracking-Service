using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BL
{
    public class ManagerSalesParser : IParser
    {
        private readonly Regex _saleInfoRegex = new Regex(@"(\d{2}.\d{2}.\d{4} \d{2}:\d{2}:\d{2});(\w+);(\w+);(\d*)", RegexOptions.Compiled);
        private readonly Regex _titleInfoRegex = new Regex(@"(\w+)_(\d{2}\d{2}\d{4})", RegexOptions.Compiled);

        public SaleInfo Parse(string line)
        {
            if (!_saleInfoRegex.IsMatch(line)) throw new FormatException("File has wrong inner format;");

            var match = _saleInfoRegex.Match(line);

            var date = Convert.ToDateTime(match.Groups[1]);
            var summ = Convert.ToDouble(match.Groups[4]);

            return new SaleInfo(match.Groups[3].Value,match.Groups[2].Value,date,summ);
        }

        public TitleInfo ParseTitle(string title)
        {
            if (!_titleInfoRegex.IsMatch(title)) throw new FormatException("File has wrong name format;");

            var match = _saleInfoRegex.Match(title);

            DateTime date;
            DateTime.TryParseExact(match.Groups[3].Value, "ddMMyyyy", null, DateTimeStyles.None, out date);

            return new TitleInfo(match.Groups[2].Value, DateTime.MaxValue);
        }
    }
}