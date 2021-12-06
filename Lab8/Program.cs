using System;
using System.Linq;
using System.Reflection;

namespace Lab8
{
    class Program
    {
        static void PrintAssemblyExportedClassFullNames(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            var sorted = allTypes.Where(type => type.IsPublic && type.IsDefined(typeof(ExportClass), false));
            foreach (var type in sorted)
            {
                Console.WriteLine(type.FullName);
            }
        }

        static void Main(string[] args)
        {
            var path = "/home/anton/RiderProjects/ModernProgrammingPlatforms/Lab8TestAssembly/bin/Debug/net5.0/Lab8TestAssembly.dll";
            var assembly = Assembly.LoadFrom(path);
            PrintAssemblyExportedClassFullNames(assembly);
        }
    }
}