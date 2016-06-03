using System.Collections.Generic;
using System.IO;

namespace PackageUseScrutiniser.Core
{
    public class FileFinder : IFileFinder
    {
        public IEnumerable<string> GetFiles(string path, string filePattern)
        {
            return Directory.EnumerateFiles(path, filePattern, SearchOption.AllDirectories);
        }
    }
}