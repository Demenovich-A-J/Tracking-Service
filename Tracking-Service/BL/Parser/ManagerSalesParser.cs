using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BL.Info;
using BL.Parser.Interfaces;

namespace BL.Parser
{
    public class ManagerSalesParser : IParser
    {
        private readonly Regex _saleInfoRegex = new Regex(@"(\d{2}.\d{2}.\d{4} \d{2}:\d{2}:\d{2});(\w+);(\w+);(\d*)", RegexOptions.Compiled);
        private readonly Regex _titleInfoRegex = new Regex(@"(\w+)_(\d{2}\d{2}\d{4})", RegexOptions.Compiled);

        public SaleInfo Parse(string line)
        {
            if (!_saleInfoRegex.IsMatch(line)) throw new FormatException("File has wrong inner format;");

            var match = _saleInfoRegex.Match(line);

            DateTime date;
            DateTime.TryParseExact(match.Groups[1].Value, "dd.MM.yyyy HH:mm:ss", null, DateTimeStyles.None, out date);
            var summ = Convert.ToDouble(match.Groups[4].Value);

            return new SaleInfo(match.Groups[3].Value,match.Groups[2].Value,date,summ);
        }

        public TitleInfo ParseTitle(string title)
        {
            if (!_titleInfoRegex.IsMatch(title)) throw new FormatException("File has wrong name format;");

            var match = _titleInfoRegex.Match(title);

            DateTime date;
            DateTime.TryParseExact(match.Groups[2].Value, "ddMMyyyy", null, DateTimeStyles.None, out date);

            return new TitleInfo(match.Groups[1].Value, date);
        }
    }
}