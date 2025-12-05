using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COM5113_Assignment_WinForm.Path;
using COM5113_Assignment_WinForm.Algorithms;
using COM5113_Assignment_WinForm.MapInfo;

namespace COM5113_Assignment_WinForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            Application.Run(new MainForm());
        }
    }
}
