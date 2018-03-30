using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types.Application;
using SimpleInjector;
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
            container.Register<IAutoUpdateLogic, AutoUpdateLogic>();
            container.Register<ISoundLogic, SoundLogic>();
            container.Register<ITerminalAerodromeForecastLogic, TerminalAerodromeForecastLogic>();
        }

        private static void RegisterSingletons(this Container container)
        {
            container.RegisterSingleton<ApplicationVariables>();
        }
    }
}
