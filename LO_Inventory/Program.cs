using LO_Inventory.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LO_Inventory
{
    internal static class Program
    {
        private const string DomainName = "sgvf.sgcf";
        private static bool _skipDomainCheck = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] agrs)
        {
            SetFlags(agrs);
            if (string.Compare(DomainName, AdHelper.GetDomainName(), true) != 0 && !_skipDomainCheck)
            {
                MessageBox.Show("Unauthorized enviroment. Exit now...");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new LoginForm();
            Application.Run(loginForm);
            if (!loginForm.Authorized) return;
            var menu = new FormMenu(loginForm.Username, loginForm.Role);
            Application.Run(menu);

        }

        public static string ExeDir
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        static void SetFlags(string[] agrs)
        {
            foreach (var agr in agrs)
            {
                if(string.Compare(agr, "-skipcheck", 0) == 0)
                {
                    _skipDomainCheck = true;
                }
            }
        }
        public static string GetVer
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }

    }
}