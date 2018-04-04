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
        /// <param name="temperature">Temperature</param>
        /// <param name="QNH">QNH</param>
        /// <returns>Calculated TL</returns>
        int CalculateTransitionLevel(int temperature, int QNH);

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

        public int CalculateTransitionLevel(int temperature, int QNH)
        {
            //Calculate TL level. TL = 307.8-0.13986*T-0.26224*Q (thanks to Stefan Blauw for this formula).
            return (int)Math.Ceiling((307.8 - (0.13986 * temperature) - (0.26224 * QNH)) / 5) * 5;
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
