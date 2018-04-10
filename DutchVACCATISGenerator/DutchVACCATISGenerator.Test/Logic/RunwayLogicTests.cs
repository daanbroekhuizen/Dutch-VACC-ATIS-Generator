using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class RunwayLogicTests
    {
        private readonly IRunwayLogic runwayLogic;

        public RunwayLogicTests()
        {
            runwayLogic = new RunwayLogic();
        }

        [TestMethod]
        public void SchipholRunways_ShouldPass_Passes()
        {
            //Arrange
            SchipholPlanningInterfaceData data = null;
            var eventTriggerd = false;

            ApplicationEvents.SchipholRunwaysEvent += (sender, args) =>
            {
                data = args.SchipholPlanningInterfaceData;
                eventTriggerd = true;
            };

            //Act
            runwayLogic.SchipholRunways().Wait();

            //Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(eventTriggerd);
        }
    }
}
