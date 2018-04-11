using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test.Logic
{


    [TestClass]
    public class VersionLogicTests
    {
        private readonly IVersionLogic versionLogic;

        public VersionLogicTests()
        {
            versionLogic = new VersionLogic();
        }

        [TestMethod]
        public void NewVersion_NewVersionAvailable_IsTrue()
        {
            //Arrange
            bool newVersion = false;

            ApplicationEvents.NewVersionEvent += (sender, args) =>
            {
                newVersion = true;
            };

            //Act
            versionLogic.NewVersion().Wait();

            //Assert
            Assert.IsTrue(newVersion);
        }
    }
}
