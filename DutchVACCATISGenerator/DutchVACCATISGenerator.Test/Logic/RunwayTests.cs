using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class RunwayTests
    {
        private readonly IRunwayLogic runwayLogic;

        public RunwayTests()
        {
            var applicationVariablesMock = new Mock<ApplicationVariables>();

            runwayLogic = new RunwayLogic(applicationVariablesMock.Object);
        }

        [TestMethod]
        public void SchipholRunwaysTest()
        {
            //Act
            runwayLogic.SchipholRunways().Wait();
        }
    }
}
