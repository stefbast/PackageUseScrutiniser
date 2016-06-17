using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PackageUseScrutiniser.Core;

namespace PackageUseScrutiniser.Cli
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--help")
            {
                PrintHelp();
                return;
            }
       
            var paths = GetPaths(args);
            var packageId = GetPackageId(args);

            Console.WriteLine();
            Console.WriteLine("Searching for {0}", packageId);
            Console.WriteLine();

            foreach (var path in paths)
            {
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Path {0} does not exist", path);
                    continue;
                }

                var packageFinder = new PackageFinder(new FileFinder(), new XmlReader());

                foreach (var package in packageFinder.GetPackages(packageId, path))
                {
                    Console.WriteLine(FormatResult(package));
                }
            }
        }

        private static string FormatResult(FindResult package)
        {
            return string.Format("version: {0}, found in: {1}", package.PackageVersion, package.PackageName);
        }

        private static IList<string> GetPaths(string[] args)
        {
            if (args.Length >= 2)
            {
                return args.Skip(1).ToList();
            }
            
            return new List<string> { Directory.GetCurrentDirectory() };
        }

        private static string GetPackageId(IList<string> args)
        {
            if (args.Count >= 1)
            {
                return args[0];
            }

            return null;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("usage: pus <package id> [<path1> <path2> ...]");
            Console.WriteLine("\tIf no path is defined, uses the current path.");
        }
    }
}
