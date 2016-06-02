using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PackageUseScrutiniser.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var packagesConfigs = Directory.EnumerateFiles(@"C:\_Source\IAM\FIN-Genesis\AMGStocks\Development", "packages.config", SearchOption.AllDirectories);
            foreach (var packageConfig in packagesConfigs)
            {
                XDocument doc = XDocument.Load(packageConfig);
                var packages = doc.Root
                                  .Elements("package")
                                  .Where(package => package.Attribute("id").Value == "Gns.Sly.LocalStock.Contracts")
                                  .ToList();
                if (packages.Count > 0)
                {
                    Console.WriteLine(packageConfig);    
                }
                
            }

        }
    }
}
