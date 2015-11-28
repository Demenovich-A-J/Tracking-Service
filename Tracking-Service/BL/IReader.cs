using System.Collections.Generic;

namespace BL
{
    public interface IReader
    {
        IEnumerable<string> Read(string path);
    }
}