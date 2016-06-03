using System.Collections.Generic;

namespace PackageUseScrutiniser.Core
{
    public interface IFileFinder
    {
        IEnumerable<string> GetFiles(string path, string filePattern);
    }
}