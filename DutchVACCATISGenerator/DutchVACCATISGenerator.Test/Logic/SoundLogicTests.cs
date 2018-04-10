using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class SoundLogicTests
    {
        private const string ATISEHAM = @"C:\Users\Virtual\Documents\EuroScope\atis\atiseham.txt";

        private readonly ApplicationVariables applicationVariables;
        private readonly IATISLogic ATISLogic;
        private readonly ISoundLogic soundLogic;

        public SoundLogicTests()
        {
            applicationVariables = new ApplicationVariables();
            var METARLogic = new METARLogic();

            ATISLogic = new ATISLogic(applicationVariables, METARLogic);

            soundLogic = new SoundLogic();
        }

        [TestMethod]
        public void Build_IndividualMETAR_Passes()
        {
            //Arrange
            var METAR = new METAR(METARHelper.METAR);
            applicationVariables.SelectedAirport = METAR.ICAO;
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);
            var eventTriggerd = false;

            ApplicationEvents.BuildAITSCompletedEvent += (sender, args) =>
            {
                eventTriggerd = true;
            };

            //Act
            var output = ATISLogic.Generate(METAR, "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
            soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();

            //Assert
            Assert.IsNotNull(output);
            Assert.AreNotEqual(output, "");
            Assert.IsTrue(eventTriggerd);
        }

        [TestMethod]
        public void Build_EindhovenMETARs_Passes()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHEH";
            var ATISBuilds = new List<string>();
            var soundBuilds = new List<bool>();

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                soundBuilds.Add(true);
            };

            //Act
            foreach (var METAR in METARHelper.EHEHMETARs)
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);
                ATISBuilds.Add(ATISLogic.Generate(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHEHMETARs.Count);
            Assert.AreEqual(soundBuilds.Count, METARHelper.EHEHMETARs.Count);
            Assert.AreEqual(soundBuilds.Count, ATISBuilds.Count);
        }

        [TestMethod]
        public void Build_RotterdamMETARs_Passes()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHRD";
            var ATISBuilds = new List<string>();
            var soundBuilds = new List<bool>();

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                soundBuilds.Add(true);
            };

            //Act
            foreach (var METAR in METARHelper.EHRDMETARs)
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);
                ATISBuilds.Add(ATISLogic.Generate(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHRDMETARs.Count);
            Assert.AreEqual(soundBuilds.Count, METARHelper.EHRDMETARs.Count);
            Assert.AreEqual(soundBuilds.Count, ATISBuilds.Count);
        }

        [TestMethod]
        public void Build_SchipholMETARs_Passes()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            var ATISBuilds = new List<string>();
            var soundBuilds = new List<bool>();

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                soundBuilds.Add(true);
            };

            //Act
            foreach (var METAR in METARHelper.EHAMMETARs)
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);
                ATISBuilds.Add(ATISLogic.Generate(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHAMMETARs.Count);
            Assert.AreEqual(soundBuilds.Count, METARHelper.EHAMMETARs.Count);
            Assert.AreEqual(soundBuilds.Count, ATISBuilds.Count);
        }
    }
}
