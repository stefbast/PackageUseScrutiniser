using System;
using System.Collections.Generic;
using System.IO;
using PackageUseScrutiniser.Core;

namespace PackageUseScrutiniser.Cli
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--help" || args.Length > 2)
            {
                PrintHelp();
                return;
            }
       
            var path = GetPath(args);
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Path {0} does not exist", path);
                return;
            }

            var packageId = GetPackageId(args);            
            var packageFinder = new PackageFinder();
            
            foreach (var package in packageFinder.GetPackages(packageId, path))
            {
                Console.WriteLine(package);
            }
        }

        private static string GetPath(string[] args)
        {
            if (args.Length >= 2)
            {
                return args[1];
            }
            
            return Directory.GetCurrentDirectory();                        
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
            Console.WriteLine("usage: pus <package id> [<path>]");
            Console.WriteLine("\tIf no path is defined, uses the current path.");
        }
    }
}
