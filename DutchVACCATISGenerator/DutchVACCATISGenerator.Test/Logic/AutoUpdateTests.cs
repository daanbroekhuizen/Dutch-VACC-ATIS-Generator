using DutchVACCATISGenerator.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class AutoUpdateTests
    {
        private readonly IAutoUpdateLogic autoUpdateLogic;
        private readonly IFileLogic fileLogic;

        public AutoUpdateTests()
        {
            fileLogic = new FileLogic();
            autoUpdateLogic = new AutoUpdateLogic(fileLogic);
        }

        [TestMethod]
        public void AutoUpdateTest()
        {
            //Act
            autoUpdateLogic.AutoUpdate().Wait(5000);
        }
    }
}
