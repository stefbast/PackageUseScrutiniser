using System.Xml.Linq;

namespace PackageUseScrutiniser.Core
{
    public interface IXmlReader
    {
        XDocument Read(string fileName);
    }
}