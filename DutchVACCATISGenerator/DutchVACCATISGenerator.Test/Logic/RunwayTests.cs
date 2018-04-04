using DutchVACCATISGenerator.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class RunwayTests
    {
        private readonly IRunwayLogic runwayLogic;

        public RunwayTests()
        {
            runwayLogic = new RunwayLogic();
        }

        [TestMethod]
        public void SchipholRunwaysTest()
        {
            //Act
            runwayLogic.SchipholRunways().Wait();
        }
    }
}
