using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PackageUseScrutiniser.Core;

namespace PackageUseScrutiniser.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var package in new PackageFinder().GetPackages(@"C:\_Source\IAM\FIN-Genesis\AMGStocks\Development", "Gns.Sly.LocalStock.Contracts"))
            {
                Console.WriteLine(package);
            }
        }        
    }
}
