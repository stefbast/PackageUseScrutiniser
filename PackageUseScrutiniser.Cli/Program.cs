using System;
using System.Collections.Generic;
using System.IO;
using PackageUseScrutiniser.Core;

namespace PackageUseScrutiniser.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IEnumerable<string> packages = new List<string>();
            var packageFinder = new PackageFinder();
            switch (args.Length)
            {                
                case 1:
                    if (args[0] == "--help")
                    {
                        PrintHelp();
                    }
                    packages = packageFinder.GetPackages(Directory.GetCurrentDirectory(), args[0]);
                    break;
                case 2:
                    packages = packageFinder.GetPackages(args[0], args[1]);
                    break;
                default:
                    PrintHelp();
                    break;
            }

            foreach (var package in packages)
            {
                Console.WriteLine(package);
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("usage: pus <package id> [<path>]");
            Console.WriteLine("\tIf no path is defined, uses the current path.");
        }
    }
}
