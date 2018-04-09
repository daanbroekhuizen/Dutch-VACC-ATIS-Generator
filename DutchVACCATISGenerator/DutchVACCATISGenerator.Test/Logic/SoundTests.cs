using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class SoundTests
    {
        private const string ATISEHAM = @"C:\Users\Virtual\Documents\EuroScope\atis\atiseham.txt";

        private readonly ApplicationVariables applicationVariables;
        private readonly IATISLogic ATISLogic;
        private readonly ISoundLogic soundLogic;

        public SoundTests()
        {
            applicationVariables = new ApplicationVariables();
            var METARLogic = new METARLogic();

            ATISLogic = new ATISLogic(applicationVariables, METARLogic);

            soundLogic = new SoundLogic();
        }

        [TestMethod]
        public void METARBuildATIS()
        {
            //Arrange
            var METAR = new METAR("EHAM 260125Z 16005KT 8000 NSC 03/03 Q1012 BECMG 6000");
            applicationVariables.SelectedAirport = METAR.ICAO;
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            var output = ATISLogic.GenerateOutput(METAR, "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
            soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples);
        }

        [TestMethod]
        public void EindhovenBuildATIS()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHEH";
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            var ATISBuild = false;

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                ATISBuild = true;
            };

            foreach (var METAR in METARHelper.EHEHMETARs)
            {
                ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples);

                while (ATISBuild == false)
                {
                    Thread.Sleep(10);
                }

                ATISBuild = false;
            }
        }

        [TestMethod]
        public void RotterdamBuildATIS()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHRD";
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            var ATISBuild = false;

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                ATISBuild = true;
            };

            foreach (var METAR in METARHelper.EHRDMETARs)
            {
                ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples);

                while (ATISBuild == false)
                {
                    Thread.Sleep(10);
                }

                ATISBuild = false;
            }
        }

        [TestMethod]
        public void SchipholBuildATIS()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            var ATISBuild = false;

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                ATISBuild = true;
            };

            foreach (var METAR in METARHelper.EHAMMETARs)
            {
                ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples);

                while (ATISBuild == false)
                {
                    Thread.Sleep(10);
                }

                ATISBuild = false;
            }
        }
    }
}
