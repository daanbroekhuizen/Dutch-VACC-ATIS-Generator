using DutchVACCATISGenerator.Logic;
using SimpleInjector;

namespace DutchVACCATISGenerator.Extensions
{
    public static class SimpleInjectorExstenions
    {
        public static Container RegisterDependency(this Container container)
        {
            container = new Container();

            container.Register<ITerminalAerodromeForecastLogic, TerminalAerodromeForecastLogic>();

            container.Verify();

            return container;
        }
    }
}
