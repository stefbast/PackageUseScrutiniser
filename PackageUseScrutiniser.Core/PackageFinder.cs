using System.Collections.Generic;
using System.Linq;

namespace PackageUseScrutiniser.Core
{
    public class PackageFinder
    {
        public IFileFinder FileFinder { get; set; }
        public IXmlReader XmlReader { get; set; }

        public PackageFinder(IFileFinder fileFinder, IXmlReader xmlReader)
        {
            FileFinder = fileFinder;
            XmlReader = xmlReader;
        }

        public IEnumerable<FindResult> GetPackages(string packageId, string path)
        {
            IEnumerable<string> packagesConfigs = FileFinder.GetFiles(path, "packages.config");
            foreach (var packageConfig in packagesConfigs)
            {
                var doc = XmlReader.Read(packageConfig);
                if (doc.Root == null)
                {
                    yield break;
                }
                var packages = doc.Root
                    .Elements("package")
                    .Where(package => package.Attribute("id").Value == packageId)
                    .ToList();
                if (packages.Count > 0)
                {
                    var versionAttribute = packages.First().Attribute("version");
                    var version = versionAttribute == null ? string.Empty : versionAttribute.Value;
                    yield return new FindResult {PackageName = packageConfig, PackageVersion = version};
                }
            }
        }
    }
}