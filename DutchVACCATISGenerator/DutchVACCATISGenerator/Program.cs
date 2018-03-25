using DutchVACCATISGenerator.Extensions;
using SimpleInjector;
using System;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    static class Program
    {
        private static Container container;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Bootstrap SimpleInjector.
            container = container.Bootstrap();

            Application.Run(container.GetInstance<DutchVACCATISGenerator>());
        }
    }
}
