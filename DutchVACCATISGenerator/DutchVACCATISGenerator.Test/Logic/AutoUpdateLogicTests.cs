using DutchVACCATISGenerator.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class AutoUpdateLogicTests
    {
        private readonly IAutoUpdateLogic autoUpdateLogic;

        public AutoUpdateLogicTests()
        {
            var fileLogic = new FileLogic();
            autoUpdateLogic = new AutoUpdateLogic(fileLogic);
        }

        [TestMethod]
        public void AutoUpdate_ShouldPass_Passes()
        {
            //Act
            autoUpdateLogic.AutoUpdate().Wait();
        }
    }
}
