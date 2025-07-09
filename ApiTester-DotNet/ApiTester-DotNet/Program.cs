using System;
using System.Windows.Forms;

namespace ApiTester_DotNet
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new ApiTester());
        }
    }
}