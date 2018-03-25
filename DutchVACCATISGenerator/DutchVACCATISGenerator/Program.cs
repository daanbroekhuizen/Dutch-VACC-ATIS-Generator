using DutchVACCATISGenerator.Extensions;
using SimpleInjector;
using System;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    static class Program
    {
        private static readonly Container container;

        /// <summary>
        /// Constructor.
        /// </summary>
        static Program()
        {
            container = container.RegisterDependency();
        }

        /// <summary>
        /// Main entry point of application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DutchVACCATISGenerator());
        }
    }
}
