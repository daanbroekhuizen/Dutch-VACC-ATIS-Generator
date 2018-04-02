using DutchVACCATISGenerator.Types;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator.Logic
{
    public interface IMETARLogic
    {
        /// <summary>
        /// Calculate transition level from QNH and temperature.
        /// </summary>
        /// <returns>Calculated TL</returns>
        int CalculateTransitionLevel(string temperature, int QNH);

        /// <summary>
        /// Downloads a METAR from the VATSIM METAR website.
        /// </summary>
        /// <param name="ICAO">ICAO of airport to download METAR of</param>
        /// <returns>Downloaded METAR</returns>
        Task Download(string ICAO);
    }

    public class METARLogic : IMETARLogic
    {
        private readonly ApplicationVariables applicationVariables;

        private const string METARURL = "http://metar.vatsim.net/metar.php?id=";

        public METARLogic(ApplicationVariables applicationVariables)
        {
            this.applicationVariables = applicationVariables;

        }

        public int CalculateTransitionLevel(string temperature, int QNH)
        {
            int temp;

            //If METAR contains M (negative value), multiply by -1 to make an negative integer.
            if (temperature.StartsWith("M"))
                temp = Convert.ToInt32(temperature) * -1;

            else
                temp = Convert.ToInt32(temperature);

            //Calculate TL level. TL = 307.8-0.13986*T-0.26224*Q (thanks to Stefan Blauw for this formula).
            return (int)Math.Ceiling((307.8 - (0.13986 * temp) - (0.26224 * QNH)) / 5) * 5;
        }

        public async Task Download(string ICAO)
        {
            var request = WebRequest.Create($"{METARURL}{ICAO}");

            var response = await request.GetResponseAsync();

            var reader = new StreamReader(response.GetResponseStream());

            applicationVariables.DownloadedMETAR = reader.ReadToEnd().Trim();

            ApplicationEvents.METARDownloaded();
        }
    }
}
