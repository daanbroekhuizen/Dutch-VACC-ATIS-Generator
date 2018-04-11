using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
