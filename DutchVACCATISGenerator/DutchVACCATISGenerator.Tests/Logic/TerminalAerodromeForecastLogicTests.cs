using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class TerminalAerodromeForecastLogicTests
    {
        private ApplicationVariables applicationVariables;
        private readonly ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic;

        public TerminalAerodromeForecastLogicTests()
        {
            applicationVariables = new ApplicationVariables();
            terminalAerodromeForecastLogic = new TerminalAerodromeForecastLogic(applicationVariables);
        }

        [TestMethod]
        public void DownloadTerminalAerodromeForecast_DownloadsTerminalAerodromeForecast_IsNotNull()
        {
            //Act
            var terminalAerodromeForecastRaw = terminalAerodromeForecastLogic.DownloadTerminalAerodromeForecast();

            //Assert
            Assert.AreNotEqual(terminalAerodromeForecastRaw, string.Empty);
            Assert.IsFalse(string.IsNullOrWhiteSpace(terminalAerodromeForecastRaw));
        }

        [TestMethod]
        public void GetTerminalAerodromeForecast_ReturnsCorrectTerminalAerodromeForecast_ReturnsTerminalAerodromeForecast()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            var terminalAerodromeForecastRaw = terminalAerodromeForecastLogic.DownloadTerminalAerodromeForecast();

            //Act
            var terminalAerodromeForecast = terminalAerodromeForecastLogic.GetTerminalAerodromeForecast(terminalAerodromeForecastRaw);

            //Assert
            Assert.AreNotEqual(terminalAerodromeForecast, string.Empty);
            Assert.IsTrue(terminalAerodromeForecast.Contains(applicationVariables.SelectedAirport));
        }

        [TestMethod]
        public void GetTerminalAerodromeForecast_WithCOR_ReturnsCORTerminalAerodromeForecast()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            var terminalAerodromeForecastRaw = "TAF COR EHAM 140455Z 1406/1512 25007KT 5000 BR BKN020 BECMG 1406/1408 9999 NSW SCT030 BECMG 1414/1417 34007KT CAVOK BECMG 1419/1422 05005KT PROB30 TEMPO 1422/1504 6000 RA -SHRA SCT040CB BECMG 1500/1503 12005KT 5000 BR PROB30 TEMPO 1503/1506 2000 BECMG 1506/1508 18007KT 9999 NSW SCT025=";

            //Act
            var terminalAerodromeForecast = terminalAerodromeForecastLogic.GetTerminalAerodromeForecast(terminalAerodromeForecastRaw);

            //Assert
            Assert.AreNotEqual(terminalAerodromeForecast, string.Empty);
            Assert.IsTrue(terminalAerodromeForecast.Contains($"TAF COR {applicationVariables.SelectedAirport}"));
        }

        [TestMethod]
        public void GetTerminalAerodromeForecast_WithAMD_ReturnsCORTerminalAerodromeForecast()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            var terminalAerodromeForecastRaw = "TAF AMD EHAM 140455Z 1406/1512 25007KT 5000 BR BKN020 BECMG 1406/1408 9999 NSW SCT030 BECMG 1414/1417 34007KT CAVOK BECMG 1419/1422 05005KT PROB30 TEMPO 1422/1504 6000 RA -SHRA SCT040CB BECMG 1500/1503 12005KT 5000 BR PROB30 TEMPO 1503/1506 2000 BECMG 1506/1508 18007KT 9999 NSW SCT025=";

            //Act
            var terminalAerodromeForecast = terminalAerodromeForecastLogic.GetTerminalAerodromeForecast(terminalAerodromeForecastRaw);

            //Assert
            Assert.AreNotEqual(terminalAerodromeForecast, string.Empty);
            Assert.IsTrue(terminalAerodromeForecast.Contains($"TAF AMD {applicationVariables.SelectedAirport}"));
        }
    }
}
