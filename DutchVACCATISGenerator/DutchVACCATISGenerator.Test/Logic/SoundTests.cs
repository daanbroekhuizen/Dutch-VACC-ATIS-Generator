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
        public void SchipholSoundATIS()
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
                ATISLogic.GenerateOutput(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24");
                soundLogic.Build(@"C:\Users\Virtual\Documents\EuroScope\atis\atiseham.txt", applicationVariables.ATISSamples);

                while (ATISBuild == false)
                {
                    Thread.Sleep(10);
                }

                ATISBuild = false;
            }
        }
    }
}
