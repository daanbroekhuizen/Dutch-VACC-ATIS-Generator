using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Logic;
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

                container.RegisterSingleton<IFormOpener, FormOpener>();
                container.Register<ITerminalAerodromeForecastLogic, TerminalAerodromeForecastLogic>();

                container.Verify();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return container;
        }
    }
}
