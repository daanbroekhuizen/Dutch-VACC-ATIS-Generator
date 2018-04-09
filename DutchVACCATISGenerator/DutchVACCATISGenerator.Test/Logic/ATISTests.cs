using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class ATISTests
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly IATISLogic ATISLogic;

        public ATISTests()
        {
            applicationVariables = new ApplicationVariables();
            var METARLogic = new METARLogic();

            ATISLogic = new ATISLogic(applicationVariables, METARLogic);
        }

        [TestMethod]
        public void GenerateSchipholATIS()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            var generatedATIS = new List<string>();

            //Act
            foreach (var METAR in METARHelper.EHAMMETARs)
                generatedATIS.Add(ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
        }

        [TestMethod]
        public void GenerateRotterdamATIS()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHRD";
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            //Act
            foreach (var METAR in METARHelper.EHRDMETARs)
                ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
        }

        [TestMethod]
        public void GenerateEindhovenATIS()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHEH";
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);

            //Act
            foreach (var METAR in METARHelper.EHEHMETARs)
                ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
        }
    }
}
