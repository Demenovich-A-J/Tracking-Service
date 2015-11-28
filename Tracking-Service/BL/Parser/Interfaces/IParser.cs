using BL.Info;

namespace BL.Parser.Interfaces
{
    public interface IParser
    {
        SaleInfo Parse(string line);
        TitleInfo ParseTitle(string title);
    }
}