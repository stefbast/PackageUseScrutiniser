using System.Xml.Linq;

namespace PackageUseScrutiniser.Core
{
    public class XmlReader : IXmlReader
    {
        public XDocument Read(string fileName)
        {
            return XDocument.Load(fileName);
        }
    }
}