using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using SimpleInjector;

namespace DutchVACCATISGenerator.Extensions
{
    public static class SimpleInjectorExtensions
    {
        public static Container Bootstrap(this Container container)
        {
            container = new Container();

            container.RegisterHelpers();
            container.RegisterLogic();
            container.RegisterSingletons();

            container.Verify();

            return container;
        }

        private static void RegisterHelpers(this Container container)
        {
            container.RegisterSingleton<IFormOpenerHelper, FormOpenerHelper>();
        }

        private static void RegisterLogic(this Container container)
        {
            container.Register<IATISLogic, ATISLogic>();
            container.Register<IAutoUpdateLogic, AutoUpdateLogic>();
            container.Register<IFileLogic, FileLogic>();
            container.Register<IMETARLogic, METARLogic>();
            container.Register<IRunwayLogic, RunwayLogic>();
            container.Register<ITerminalAerodromeForecastLogic, TerminalAerodromeForecastLogic>();
            container.Register<IVersionLogic, VersionLogic>();
        }

        private static void RegisterSingletons(this Container container)
        {
            container.RegisterSingleton<ApplicationVariables>();
            container.RegisterSingleton<ISoundLogic, SoundLogic>();
        }
    }
}
