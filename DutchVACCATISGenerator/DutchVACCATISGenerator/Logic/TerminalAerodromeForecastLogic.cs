using DutchVACCATISGenerator.Types.Application;
using System;
using System.Linq;
using System.Net;

namespace DutchVACCATISGenerator.Logic
{
    public interface ITerminalAerodromeForecastLogic
    {
        /// <summary>
        /// Downloads terminal aerodrome forecast from the web.
        /// </summary>
        /// <returns>String containing terminal aerodrome forecast</returns>
        string DownloadTerminalAerodromeForecast();

        /// <summary>
        /// Gets terminal aerodrome forecast from raw downloaded terminal aerodrome forecast.
        /// </summary>
        /// <param name="terminalAerodromeForecastRaw">Downloaded terminal aerodrome forecast</param>
        /// <returns>Terminal aerodrome forecast</returns>
        string GetTerminalAerodromeForecast(string terminalAerodromeForecastRaw);
    }

    public class TerminalAerodromeForecastLogic : ITerminalAerodromeForecastLogic
    {
        private readonly ApplicationVariables applicationVariables;

        public TerminalAerodromeForecastLogic(ApplicationVariables applicationVariables)
        {
            this.applicationVariables = applicationVariables;
        }

        public string DownloadTerminalAerodromeForecast()
        {
            WebClient client = new WebClient();

            //Set user Agent, makes the site think we're not a bot.
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0"; //(Windows; U; Windows NT 6.1; en-US; rv:1.9.2.4) Gecko/20100611 Firefox/3.6.4";

            //Make web request to get terminal aerodrome forecast.
            return client.DownloadString("https://www.knmi.nl/nederland-nu/luchtvaart/vliegveldverwachtingen");
        }

        public string GetTerminalAerodromeForecast(string terminalAerodromeForecastRaw)
        {
            string terminalAerodromeForecast = string.Empty;

            //Get terminal aerodrome forecast part from loaded HTML code.
            var split = (DetermineTerminalAerodromeForcast(terminalAerodromeForecastRaw) + terminalAerodromeForecastRaw
                .Split(new string[] { DetermineTerminalAerodromeForcast(terminalAerodromeForecastRaw) }, StringSplitOptions.None)[1])
                .Split(new string[] { "=" }, StringSplitOptions.None)[0]
                .Split(new string[] { "\n" }, StringSplitOptions.None)
                .ToList();

            foreach (string s in split)
                terminalAerodromeForecast += s.TrimEnd() + "\r\n";

            return terminalAerodromeForecast;
        }

        private string DetermineTerminalAerodromeForcast(string terminalAerodromeForecastRaw)
        {
            string standardTerminalAerodromeForecast = $"TAF {applicationVariables.SelectedAirport}";
            string amdTerminalAerodromeForecast = $"TAF AMD {applicationVariables.SelectedAirport}";
            string corTerminalAerodromeForecast = $"TAF COR {applicationVariables.SelectedAirport}";

            if (terminalAerodromeForecastRaw.Contains(corTerminalAerodromeForecast))
                return corTerminalAerodromeForecast;
            else if (terminalAerodromeForecastRaw.Contains(amdTerminalAerodromeForecast))
                return amdTerminalAerodromeForecast;
            else
                return standardTerminalAerodromeForecast;
        }
    }
}
