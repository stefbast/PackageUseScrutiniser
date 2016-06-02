using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PackageUseScrutiniser.Core
{
    public class PackageFinder
    {
        public IEnumerable<string> GetPackages(string path, string packageId)
        {
            var packagesConfigs = Directory.EnumerateFiles(path, "packages.config", SearchOption.AllDirectories);
            foreach (var packageConfig in packagesConfigs)
            {
                XDocument doc = XDocument.Load(packageConfig);
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
                    yield return packageConfig;
                }
            }
        }
    }
}