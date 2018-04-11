using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class METARLogicTests
    {
        private readonly IMETARLogic METARLogic;

        public METARLogicTests()
        {
            METARLogic = new METARLogic();
        }

        [TestMethod]
        public void CalculateTransitionLevel_IndividualMETAR_PositiveTransitionLevel()
        {
            //Arrange
            var METAR = new METAR(METARHelper.METAR);

            //Act
            var transitionLevel = METARLogic.CalculateTransitionLevel(METAR.Temperature, METAR.QNH);

            //Assert
            Assert.AreNotEqual(transitionLevel, 0);
            Assert.IsTrue(transitionLevel > 0);
        }

        [TestMethod]
        public void CalculateTransitionLevel_EindhovenMETARs_PositiveTransitionLevels()
        {
            //Arrange
            var METARs = new List<METAR>();
            var transitionLevels = new List<int>();

            foreach (var METAR in METARHelper.EHEHMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach(var METAR in METARs)
            {
                transitionLevels.Add(METARLogic.CalculateTransitionLevel(METAR.Temperature, METAR.QNH));
            }

            //Assert
            foreach(var transitionLevel in transitionLevels)
            {
                Assert.AreNotEqual(transitionLevel, 0);
                Assert.IsTrue(transitionLevel > 0);
            }
        }

        [TestMethod]
        public void CalculateTransitionLevel_RotterdamMETARs_PositiveTransitionLevels()
        {
            //Arrange
            var METARs = new List<METAR>();
            var transitionLevels = new List<int>();

            foreach (var METAR in METARHelper.EHRDMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach (var METAR in METARs)
            {
                transitionLevels.Add(METARLogic.CalculateTransitionLevel(METAR.Temperature, METAR.QNH));
            }

            //Assert
            foreach (var transitionLevel in transitionLevels)
            {
                Assert.AreNotEqual(transitionLevel, 0);
                Assert.IsTrue(transitionLevel > 0);
            }
        }

        [TestMethod]
        public void CalculateTransitionLevel_SchipholMETARs_PositiveTransitionLevels()
        {
            //Arrange
            var METARs = new List<METAR>();
            var transitionLevels = new List<int>();

            foreach (var METAR in METARHelper.EHAMMETARs)
                METARs.Add(new METAR(METAR));

            //Act
            foreach (var METAR in METARs)
            {
                transitionLevels.Add(METARLogic.CalculateTransitionLevel(METAR.Temperature, METAR.QNH));
            }

            //Assert
            foreach (var transitionLevel in transitionLevels)
            {
                Assert.AreNotEqual(transitionLevel, 0);
                Assert.IsTrue(transitionLevel > 0);
            }
        }

        [TestMethod]
        public void Download_SchipholMETAR_IsDownloaded()
        {
            //Arrange
            var METAR = string.Empty;
            var ICAO = "EHAM";

            ApplicationEvents.METARDownloadedEvent += (sender, args) =>
            {
                METAR = args.METAR;
            };

            //Act
            METARLogic.Download(ICAO).Wait();

            //Assert
            Assert.AreNotEqual(string.Empty, METAR);
            Assert.IsTrue(METAR.Contains(ICAO));
        }
    }
}
