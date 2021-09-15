using System;
using System.IO;
using Lab1;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check directories
            if (!CheckParams(args, out var srcDir, out var destDir))
            {
                return;
            }

            // Copy
            var taskQueue = new TaskQueue(10);
            CopyDirectory(srcDir, destDir, taskQueue);
            // Stop on finish
            taskQueue.Stop();
        }

        public static bool CheckParams(string[] args, out DirectoryInfo srcDir, out DirectoryInfo destDir)
        {
            srcDir = new DirectoryInfo(args[0]);
            destDir = new DirectoryInfo(args[1]);
            return CheckSrcDir(srcDir) && CheckDestDir(destDir);
        }

        public static bool CheckSrcDir(DirectoryInfo srcDir)
        {
            if (srcDir.Exists)
            {
                return true;
            }

            Console.Error.WriteLine("Directory '{0}' doesn't exist!", srcDir.FullName);
            return false;
        }

        public static bool CheckDestDir(DirectoryInfo destDir)
        {
            if (destDir.Exists)
            {
                return true;
            }

            Console.Out.WriteLine("Directory '{0}' doesn't exist!", destDir.FullName);
            Console.Out.WriteLine("Do you want to create new directory?");
            // Read input
            while (true)
            {
                var input = Console.In.ReadLine();
                switch (input)
                {
                    case "y":
                        // Create new directory
                        try
                        {
                            destDir.Create();
                            return true;
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            Console.Error.WriteLine(destDir.FullName + ": no access");
                            return false;
                        }
                    case "n":
                        // Exit
                        return false;
                    default:
                        // Write prompt
                        Console.Out.WriteLine("Enter 'y' (yes) or 'n' (no)");
                        break;
                }
            }
        }

        public static void CopyDirectory(DirectoryInfo srcDir, DirectoryInfo destDir, TaskQueue taskQueue)
        {
            // Files
            var enumerateFiles = srcDir.EnumerateFiles();
            foreach (var file in enumerateFiles)
            {
                {
                    taskQueue.EnqueueTask(delegate
                    {
                        try
                        {
                            file.CopyTo(destDir.FullName + "/" + file.Name, true);
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            Console.Error.WriteLine(file.FullName + ": no access");
                        }
                    });
                }
            }

            // Directories
            var enumerateDirectories = srcDir.EnumerateDirectories();
            foreach (var directory in enumerateDirectories)
            {
                try
                {
                    var nextDestDir = new DirectoryInfo(destDir.FullName + "/" + directory.Name);
                    // Check if the destination directory exists
                    if (!nextDestDir.Exists)
                    {
                        nextDestDir.Create();
                    }

                    // Copy
                    CopyDirectory(directory, nextDestDir, taskQueue);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.Error.WriteLine(directory.FullName + ": no access");
                }
            }
        }
    }
}