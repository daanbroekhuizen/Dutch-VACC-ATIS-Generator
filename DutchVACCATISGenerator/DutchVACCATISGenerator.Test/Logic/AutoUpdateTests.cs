using DutchVACCATISGenerator.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class AutoUpdateTests
    {
        private readonly IAutoUpdateLogic autoUpdateLogic;

        public AutoUpdateTests()
        {
            var fileLogicMock = new Mock<IFileLogic>();

            autoUpdateLogic = new AutoUpdateLogic(fileLogicMock.Object);
        }

        [TestMethod]
        public void AutoUpdateTest()
        {
            //Act
            autoUpdateLogic.AutoUpdate().Wait();
        }
    }
}
