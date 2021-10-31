using System;
using System.Linq;
using System.Reflection;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            var path =
                "C:\\Users\\Anton\\RiderProjects\\modern-programming-platforms\\Lab1\\bin\\Debug\\net5.0\\Lab1.dll";
            var assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes()
                .Where(t => t.IsPublic)
                .OrderBy(t => t.Namespace + t.Name);
            foreach (var type in types)
            {
                Console.Out.WriteLine(type.FullName);
            }
        }
    }
}