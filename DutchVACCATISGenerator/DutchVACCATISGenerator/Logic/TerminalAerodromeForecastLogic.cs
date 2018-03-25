using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator.Logic
{
    public interface ITerminalAerodromeForecastLogic
    {
        string GetTerminalAerodromeForecast();
        string DetermineTAFToLoad();
        string DetermineTAFAMDToLoad();
        string DetermineTAFCORToLoad();
    }

    public class TerminalAerodromeForecastLogic : ITerminalAerodromeForecastLogic
    {
        private readonly DutchVACCATISGenerator dutchVACCATISGenerator;

        public TerminalAerodromeForecastLogic(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            this.dutchVACCATISGenerator = dutchVACCATISGenerator;
        }

        public string GetTerminalAerodromeForecast()
        {
            WebClient client = new WebClient();

            //Set user Agent, makes the site think we're not a bot.
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0"; //(Windows; U; Windows NT 6.1; en-US; rv:1.9.2.4) Gecko/20100611 Firefox/3.6.4";

            //Make web request to get TAF.
            return client.DownloadString("https://www.knmi.nl/nederland-nu/luchtvaart/vliegveldverwachtingen");
        }


        /// <summary>
        /// Method to determine TAF to load.
        /// </summary>
        /// <returns>String indicating TAF to load</returns>
        public string DetermineTAFToLoad()
        {
            switch (dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Name)
            {
                case "EHAM":
                    return "TAF EHAM";

                case "EHBK":
                    return "TAF EHBK";

                case "EHEH":
                    return "TAF EHEH";

                case "EHGG":
                    return "TAF EHGG";

                case "EHRD":
                    return "TAF EHRD";
            }

            return String.Empty;
        }

        /// <summary>
        /// Method to determine if TAF contains AMD.
        /// </summary>
        /// <returns>String indicating TAF AMD to determine</returns>
        public string DetermineTAFAMDToLoad()
        {
            switch (dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Name)
            {
                case "EHAM":
                    return "TAF AMD EHAM";

                case "EHBK":
                    return "TAF AMD EHBK";

                case "EHEH":
                    return "TAF AMD EHEH";

                case "EHGG":
                    return "TAF AMD EHGG";

                case "EHRD":
                    return "TAF AMD EHRD";
            }

            return String.Empty;
        }
        public string DetermineTAFCORToLoad()
        {
            switch (dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Name)
            {
                case "EHAM":
                    return "TAF COR EHAM";

                case "EHBK":
                    return "TAF COR EHBK";

                case "EHEH":
                    return "TAF COR EHEH";

                case "EHGG":
                    return "TAF COR EHGG";

                case "EHRD":
                    return "TAF COR EHRD";
            }

            return String.Empty;
        }
    }
}
