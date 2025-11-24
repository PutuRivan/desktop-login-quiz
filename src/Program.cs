using System;
using System.Windows.Forms;

namespace LoginSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Form awal yang dibuka
            Application.Run(new LoginForm());
        }
    }
}
