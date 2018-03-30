using DutchVACCATISGenerator.Extensions;
using SimpleInjector;
using System;
using System.Threading;
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

            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);

            Application.Run(container.GetInstance<DutchVACCATISGenerator>());
        }

        private static void MyCommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            MessageBox.Show("An unexpected error has occurred. The application will now exit.", "Error");

            Application.Exit();
        }
    }
}
