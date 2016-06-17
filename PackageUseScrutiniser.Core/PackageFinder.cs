using System.Collections.Generic;
using System.Linq;

namespace PackageUseScrutiniser.Core
{
    public class PackageFinder
    {
        private const string PackagesConfigFileName = "packages.config";
        private readonly int _packagesConfigFileNameLenght = PackagesConfigFileName.Length;
        private readonly IFileFinder _fileFinder;
        private readonly IXmlReader _xmlReader;

        public PackageFinder(IFileFinder fileFinder, IXmlReader xmlReader)
        {
            _fileFinder = fileFinder;
            _xmlReader = xmlReader;
        }

        public IEnumerable<FindResult> GetPackages(string packageId, string path)
        {
            IEnumerable<string> packagesConfigs = _fileFinder.GetFiles(path, PackagesConfigFileName);
            foreach (var packageConfig in packagesConfigs)
            {
                if (!packageConfig.EndsWith(PackagesConfigFileName))
                {
                    yield break;
                }

                var doc = _xmlReader.Read(packageConfig);
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
                    var location = packageConfig.Substring(0, packageConfig.Length - _packagesConfigFileNameLenght);
                    yield return new FindResult { PackagesConfigLocation = location, PackageVersion = version };
                }
            }
        }
    }
}