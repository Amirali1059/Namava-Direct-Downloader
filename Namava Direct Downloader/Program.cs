using System;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Windows.Forms;
//using Namava_Direct_Downloader.API;

namespace Namava_Direct_Downloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ResourceManager rm = Properties.Resources_main_en.ResourceManager;
            string CrashReport = "App Information:\n\n" + Utils.AppInformation() + "\n\nError:\n\n" + e.ExceptionObject.ToString();
            string logfile = ".\\crash-report.txt";
            using (StreamWriter logWriter = new StreamWriter(File.Create(@logfile)))
            {
                string ErrorMessage = $"{rm.GetString("msgUnhandledError")}";
                logWriter.Write($">>>> {rm.GetString("msgPleaseReport")} <<<<\n\n{CrashReport}");
            }
            Process.Start(new ProcessStartInfo(logfile) { WindowStyle = ProcessWindowStyle.Maximized });
            Environment.Exit(1);
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(true);
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm myForm = null;
#if DEBUG
            myForm = new MainForm();
            if (Utils.argv.Length == 2)
            {
                Application.Run(myForm);
            }
            else if (Utils.argv.Length == 1)
            {
                MessageBox.Show("This app does not start with a double click!\nThis program starts automatically when you click on the download logo at the bottom left of the movie at www.namava.ir.", "a little help!");
                Environment.Exit(1);
            }
            else
            {
                MessageBox.Show("Invalid argumant variables,\nThis program starts automatically when you click on the download logo at the bottom left of the movie at www.namava.ir.", "a little help!");
                Environment.Exit(1);
            }
#else


            try
            {
                myForm = new MainForm();
            }
            catch (Exception ex)
            {
                var exa = new UnhandledExceptionEventArgs(ex, true);
                UnhandledException(null, exa);
            }

            try
            {
                if (Utils.argv.Length == 2)
                {
                    Application.Run(myForm);

                }
                else if (Utils.argv.Length == 1)
                {
                    MessageBox.Show("This app does not start with a double click!\nThis program starts automatically when you click on the download logo at the bottom left of the movie at www.namava.ir.", "a little help!");
                    Environment.Exit(1);
                }
                else
                {
                    MessageBox.Show("Invalid argumant variables,\nThis program starts automatically when you click on the download logo at the bottom left of the movie at www.namava.ir.", "a little help!");
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                var exa = new UnhandledExceptionEventArgs(ex, true);
                myForm.UnhandledException(myForm, exa);
            }
#endif
        }
    }
}
