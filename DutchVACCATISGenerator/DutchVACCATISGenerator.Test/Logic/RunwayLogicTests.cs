using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void CalculateCrosswindComponent_SchipholRunwaysAndMETARS_AreCalculated()
        {
            //Arrange
            var METARs = new List<METAR>();
            var crosswindComponents = new List<int>();

            foreach (var METAR in METARHelper.EHAMMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach (var METAR in METARs)
                crosswindComponents.Add(runwayLogic.CalculateCrosswindComponent(Runways.SchipholDeparture.First().Value.Item1, METAR.Wind.Heading, METAR.Wind.Knots, METAR.Wind.GustMin, METAR.Wind.GustMax));

            //Assert
            Assert.AreEqual(crosswindComponents.Count, METARs.Count);
            Assert.AreEqual(crosswindComponents.Count, METARHelper.EHAMMETARs.Count);
            Assert.AreEqual(METARs.Count, METARHelper.EHAMMETARs.Count);
        }

        [TestMethod]
        public void CalculateTailwindComponent_SchipholRunwaysAndMETARS_AreCalculated()
        {
            //Arrange
            var METARs = new List<METAR>();
            var tailwindComponents = new List<int>();

            foreach (var METAR in METARHelper.EHAMMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach (var METAR in METARs)
                tailwindComponents.Add(runwayLogic.CalculateTailwindComponent(Runways.SchipholDeparture.First().Value.Item1, METAR.Wind.Heading, METAR.Wind.Knots, METAR.Wind.GustMin, METAR.Wind.GustMax));

            //Assert
            Assert.AreEqual(tailwindComponents.Count, METARs.Count);
            Assert.AreEqual(tailwindComponents.Count, METARHelper.EHAMMETARs.Count);
            Assert.AreEqual(METARs.Count, METARHelper.EHAMMETARs.Count);
        }

        [TestMethod]
        public void RunwayComplies_MultipleRunways_AreChecked()
        {
            //Arrange
            var METARs = new List<METAR>();
            var compliesList = new List<string>();

            foreach (var METAR in METARHelper.EHAMMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach (var METAR in METARs)
            {
                var crosswind = runwayLogic.CalculateCrosswindComponent(Runways.SchipholDeparture.First().Value.Item1, METAR.Wind.Heading, METAR.Wind.Knots, METAR.Wind.GustMin, METAR.Wind.GustMax);
                var tailwind = runwayLogic.CalculateTailwindComponent(Runways.SchipholDeparture.First().Value.Item1, METAR.Wind.Heading, METAR.Wind.Knots, METAR.Wind.GustMin, METAR.Wind.GustMax);

                compliesList.Add(runwayLogic.RunwayComplies(new Random().Next(0, 4), Runways.SchipholDeparture.First().Key, METAR.RVR, METAR.RVRValues, METAR.Visibility, METAR.Clouds, crosswind, tailwind));
            }

            //Assert
            Assert.AreEqual(compliesList.Count, METARs.Count);
        }

        [TestMethod]
        public void BestRunway_RotterdamRunways_AreSelected()
        {
            //Arrange
            var METARs = new List<METAR>();
            var bestRunwayList = new List<string>();

            foreach (var METAR in METARHelper.EHRDMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach (var METAR in METARs)
                bestRunwayList.Add(runwayLogic.BestRunway(Runways.Rotterdam, new Random().Next(0, 4), METAR.RVR, METAR.RVRValues, METAR.Visibility, METAR.Clouds, METAR.Wind.Heading, METAR.Wind.Knots, METAR.Wind.GustMin, METAR.Wind.GustMax));

            //Assert
            Assert.AreEqual(bestRunwayList.Count, METARs.Count);
            Assert.AreEqual(bestRunwayList.Count, METARHelper.EHRDMETARs.Count);
            Assert.AreEqual(METARs.Count, METARHelper.EHRDMETARs.Count);
        }
    }
}
