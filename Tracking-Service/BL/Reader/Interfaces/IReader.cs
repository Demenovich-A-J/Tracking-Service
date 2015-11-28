using System.Collections.Generic;

namespace BL.Reader.Interfaces
{
    public interface IReader
    {
        IEnumerable<string> Read(string path);
    }
}