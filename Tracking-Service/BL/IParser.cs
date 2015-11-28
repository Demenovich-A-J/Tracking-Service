namespace BL
{
    public interface IParser
    {
        SaleInfo Parse(string line);
        TitleInfo ParseTitle(string title);
    }
}