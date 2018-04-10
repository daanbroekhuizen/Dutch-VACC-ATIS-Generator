using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Extensions;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class ATISLogicTests
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly IATISLogic ATISLogic;

        public ATISLogicTests()
        {
            applicationVariables = new ApplicationVariables();
            var METARLogic = new METARLogic();

            ATISLogic = new ATISLogic(applicationVariables, METARLogic);
        }

        [TestMethod]
        public void Generate_IndividualMETAR_Passes()
        {
            //Arrange
            var METAR = new METAR(METARHelper.METAR);
            applicationVariables.SelectedAirport = METAR.ICAO;
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            //Act
            var output = ATISLogic.Generate(METAR, "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);

            //Assert
            Assert.IsNotNull(output);
            Assert.AreNotEqual(output, "");
        }

        [TestMethod]
        public void Generate_EindhovenMETARs_Passes()
        {
            //Arrange
            var ATISBuilds = new List<string>();
            var METARs = new List<METAR>();

            //Act
            foreach (var METAR in METARHelper.EHEHMETARs)
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);

                var _METAR = new METAR(METAR);

                METARs.Add(_METAR);

                applicationVariables.SelectedAirport = _METAR.ICAO;

                ATISBuilds.Add(ATISLogic.Generate(_METAR, "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHEHMETARs.Count);
            Assert.AreEqual(METARs.Count, METARHelper.EHEHMETARs.Count);
            Assert.AreEqual(ATISBuilds.Count, METARs.Count);
        }

        [TestMethod]
        public void Generate_RotterdamMETARs_Passes()
        {
            //Arrange
            var ATISBuilds = new List<string>();
            var METARs = new List<METAR>();

            //Act
            foreach (var METAR in METARHelper.EHRDMETARs)
            {
                ATISLogic.SetPhoneticAlphabet(false, true, true, false);

                var _METAR = new METAR(METAR);

                METARs.Add(_METAR);

                applicationVariables.SelectedAirport = _METAR.ICAO;

                ATISBuilds.Add(ATISLogic.Generate(_METAR, "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHRDMETARs.Count);
            Assert.AreEqual(METARs.Count, METARHelper.EHRDMETARs.Count);
            Assert.AreEqual(ATISBuilds.Count, METARs.Count);
        }

        [TestMethod]
        public void Generate_SchipholMETARs_Passes()
        {
            //Arrange
            var ATISBuilds = new List<string>();
            var METARs = new List<METAR>();
            var secondRunway = new List<string> { "18R", "27" };

            //Act
            foreach (var METAR in METARHelper.EHAMMETARs)
            {
                ATISLogic.SetPhoneticAlphabet(true, false, true, false);

                var _METAR = new METAR(METAR);

                METARs.Add(_METAR);

                applicationVariables.SelectedAirport = _METAR.ICAO;

                ATISBuilds.Add(ATISLogic.Generate(_METAR, "18C", "24", true, true, secondRunway.Shuffle(1).First(), "18L", "24", true, false, false, true, false));
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHAMMETARs.Count);
            Assert.AreEqual(METARs.Count, METARHelper.EHAMMETARs.Count);
            Assert.AreEqual(ATISBuilds.Count, METARs.Count);
        }
    }
}
