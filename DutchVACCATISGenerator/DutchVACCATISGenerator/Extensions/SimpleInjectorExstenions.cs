using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Logic;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;

namespace DutchVACCATISGenerator.Extensions
{
    public static class SimpleInjectorExstenions
    {
        public static Container Bootstrap(this Container container)
        {
            try
            {
                container = new Container();

                container.RegisterHelpers();
                container.RegisterLogic();
                container.RegisterSingletons();

                container.SuppressDutchVACCATISGeneratorDiagnosticWarning();

                container.Verify();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return container;
        }

        private static void RegisterHelpers(this Container container)
        {
            container.RegisterSingleton<IFormOpenerHelper, FormOpenerHelper>();
        }

        private static void RegisterLogic(this Container container)
        {
            container.Register<ITerminalAerodromeForecastLogic, TerminalAerodromeForecastLogic>();
        }

        private static void RegisterSingletons(this Container container)
        {
            container.RegisterSingleton<DutchVACCATISGenerator>();
        }

        private static void SuppressDutchVACCATISGeneratorDiagnosticWarning(this Container container)
        {
            var registration = container.GetRegistration(typeof(DutchVACCATISGenerator)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Main form (DutchVACCATISGenerator) will be automatically disposed by runtime as it is registered using Application.Run()");
        }
    }
}
