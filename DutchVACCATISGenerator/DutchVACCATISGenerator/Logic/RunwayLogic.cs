using DutchVACCATISGenerator.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator.Logic
{
    public interface IRunwayLogic
    {
        /// <summary>
        /// Calculate the tail wind component of a runway.
        /// </summary>
        /// <param name="runwayHeading">Opposite runway heading</param>
        /// <returns>Calculated tail wind component of a runway</returns>
        int CalculateTailwindComponent(int runwayHeading);

        /// <summary>
        /// Calculate the cross wind component of a runway.
        /// </summary>
        /// <param name="runwayHeading">Runway heading</param>
        /// <returns>Calculated cross wind component of a runway</returns>
        int CalculateCrosswindComponent(int runwayHeading);

        /// <summary>
        /// Gets the best preferred runway.
        /// </summary>
        /// <param name="runways">Runway list</param>
        /// <returns>Best runway to use</returns>
        string BestRunway(Dictionary<string, Tuple<int, int, string>> runways);

        /// <summary>
        /// Check if a runway complies with weather criteria for a runway.
        /// </summary>
        /// <param name="frictionIndex">Selected index of friction combo box</param>
        /// <param name="crosswind">Runway cross wind component</param>
        /// <param name="tailwind">Runway tail wind component</param>
        /// <returns>Indication if the a runway complies with the weather criteria for that runway</returns>
        string RunwayComplies(int frictionIndex, string runway, int crosswind, int tailwind);

        Task SchipholRunways();
    }

    public class RunwayLogic : IRunwayLogic
    {
        private readonly ApplicationVariables applicationVariables;

        private const string schipholPlanningInterfaceURL = "https://spi.dutchvacc.nl/api_atisgenerator.php?action=retrieve_lvnlrunways";

        public RunwayLogic(ApplicationVariables applicationVariables)
        {
            this.applicationVariables = applicationVariables;
        }

        public string BestRunway(Dictionary<string, Tuple<int, int, string>> runways)
        {
            var preference = int.MaxValue;
            var runway = string.Empty;

            //TODO remove non complying runways from list.

            foreach (var pair in runways)
            {
                if (runway.Equals(string.Empty))
                {
                    runway = pair.Key;
                    preference = Convert.ToInt32(pair.Value.Item3);
                }

                if (Convert.ToInt32(pair.Value.Item3) < preference)
                {
                    runway = pair.Key;
                    preference = Convert.ToInt32(pair.Value.Item3);
                }
            }

            return runway;
        }

        public int CalculateCrosswindComponent(int runwayHeading)
        {
            int crosswind;

            //If applicationVariables.METAR has a gust wind.
            if (applicationVariables.METAR.Wind.windGustMin != null)
            {
                //If gust is greater than 10 knots, include gust wind.
                if (Math.Abs(Convert.ToInt32(applicationVariables.METAR.Wind.windGustMax) - Convert.ToInt32(applicationVariables.METAR.Wind.windGustMin)) >= 10)
                {
                    crosswind = Convert.ToInt32(Math.Sin(DegreeToRadian(Math.Abs(Convert.ToInt32(applicationVariables.METAR.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(applicationVariables.METAR.Wind.windGustMax));
                }
                //Else do not include gust, calculate with min gust wind.
                else
                {
                    crosswind = Convert.ToInt32(Math.Sin(DegreeToRadian(Math.Abs(Convert.ToInt32(applicationVariables.METAR.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(applicationVariables.METAR.Wind.windGustMin));
                }
            }
            else
            {
                crosswind = Convert.ToInt32(Math.Sin(DegreeToRadian(Math.Abs(Convert.ToInt32(applicationVariables.METAR.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(applicationVariables.METAR.Wind.windKnots));
            }

            //If calculated crosswind is negative, multiply by -1 to make it positive.
            if (crosswind < -0)
                return crosswind * -1;
            else
                return crosswind;
        }

        public int CalculateTailwindComponent(int runwayHeading)
        {
            //If applicationVariables.METAR has a gust wind.
            if (this.applicationVariables.METAR.Wind.windGustMin != null)
            {
                //If gust is greater than 10 knots, include gust wind.
                if (Math.Abs(Convert.ToInt32(this.applicationVariables.METAR.Wind.windGustMax) - Convert.ToInt32(this.applicationVariables.METAR.Wind.windGustMin)) >= 10)
                {
                    return Convert.ToInt32(Math.Cos(DegreeToRadian(Math.Abs(Convert.ToInt32(this.applicationVariables.METAR.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(this.applicationVariables.METAR.Wind.windGustMax));
                }
                //Else do not include gust, calculate with min gust wind.
                else
                {
                    return Convert.ToInt32(Math.Cos(DegreeToRadian(Math.Abs(Convert.ToInt32(this.applicationVariables.METAR.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(this.applicationVariables.METAR.Wind.windGustMin));
                }
            }
            else
            {
                return Convert.ToInt32(Math.Cos(DegreeToRadian(Math.Abs(Convert.ToInt32(this.applicationVariables.METAR.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(this.applicationVariables.METAR.Wind.windKnots));
            }

        }

        public string RunwayComplies(int frictionIndex, string runway, int crosswind, int tailwind)
        {
            switch (frictionIndex)
            {
                case 0:
                    return CompliesWithVisibility(runway) ?
                        CompliesWithWind(20, 7, crosswind, tailwind) :
                        CompliesWithWind(15, 7, crosswind, tailwind);

                case 1:
                case 2:
                    return CompliesWithVisibility(runway) ?
                        CompliesWithWind(10, 0, crosswind, tailwind) :
                        CompliesWithWind(10, 0, crosswind, tailwind);

                case 3:
                case 4:
                    return CompliesWithVisibility(runway) ?
                        CompliesWithWind(5, 0, crosswind, tailwind) :
                        CompliesWithWind(5, 0, crosswind, tailwind);

                default: return "ERROR";
            }
        }

        public async Task SchipholRunways()
        {
            applicationVariables.SchipholPlanningInterfaceData = await GetSchipholPlanningInterfaceData();

            ApplicationEvents.SchipholRunways();
        }

        /// <summary>
        /// Checks if the wind on a runway complies with the wind criteria for that runway.
        /// </summary>
        /// <param name="maxCrosswind">Maximum crosswind component</param>
        /// <param name="maxTailWind">Maximum tailwind component</param>
        /// <param name="crosswind">Actual crosswind component</param>
        /// <param name="tailwind">Actual tailwind component</param>
        /// <returns>Indication if the a runway complies with the weather criteria for that runway</returns>
        private string CompliesWithWind(int maxCrosswind, int maxTailWind, int crosswind, int tailwind)
        {
            if (crosswind <= maxCrosswind && tailwind <= maxTailWind)
                return "OK";
            else
                return "-";
        }

        /// <summary>
        /// Check if the visibility on a runway complies with the visibility criteria for that runway.
        /// </summary>
        /// <returns>Boolean indicating if the runway visibility complies with the viability criteria for that runway</returns>
        private bool CompliesWithVisibility(string runway)
        {
            //If applicationVariables.METAR has a RVR.
            if (applicationVariables.METAR.RVR)
            {
                foreach (var pair in applicationVariables.METAR.RVRValues)
                {
                    //Check if runway complies with RVR criteria based on the RWY RVR value.
                    if (pair.Key.Equals(runway))
                        return CompliesWithRVR(pair.Value);
                }

                //Check if runway complies with RVR criteria based on the visibility.
                return CompliesWithRVR(applicationVariables.METAR.Visibility);
            }
            else
            {
                //If cloud layer > 200 feet.
                return (applicationVariables.METAR.Clouds.Count != 0 ? applicationVariables.METAR.Clouds.First().altitude >= 2 : true);
            }
        }

        /// <summary>
        /// Check if a runway visibility complies with the RVR criteria for that runway.
        /// </summary>
        /// <param name="rvr">RVR</param>
        /// <returns>Boolean indicating if the runway visibility complies with the RVR criteria for that runway</returns>
        private bool CompliesWithRVR(int rvr)
        {
            //If RVR > 500 and cloud layer is > 200 feet.
            if (rvr >= 550 && (applicationVariables.METAR.Clouds.Count != 0 ? applicationVariables.METAR.Clouds.First().altitude >= 2 : true))
                return true;

            //If RVR < 500 and cloud layer is < 200 feet.
            else if (rvr < 550 || (applicationVariables.METAR.Clouds.Count != 0 && applicationVariables.METAR.Clouds.First().altitude < 2))
                return false;

            else return false;
        }

        /// <summary>
        /// Converts degree to radian.
        /// </summary>
        /// <param name="degree">Degree</param>
        /// <returns>Radian calculated from degree</returns>
        private double DegreeToRadian(int degree)
        {
            return degree * (Math.PI / 180);
        }

        /// <summary>
        /// Gets data from Schiphol Planning Interface website.
        /// </summary>
        /// <returns>SchipholPlanningInterfaceData object representing the Schiphol landing and departure runways</returns>
        private async Task<SchipholPlanningInterfaceData> GetSchipholPlanningInterfaceData()
        {
            var request = WebRequest.Create(schipholPlanningInterfaceURL);

            var response = await request.GetResponseAsync();

            var reader = new StreamReader(response.GetResponseStream());

            var json = reader.ReadToEnd().Trim();

            var data = JsonConvert.DeserializeObject<List<SchipholPlanningInterface>>(json);

            return new SchipholPlanningInterfaceData
            {
                DepartureRunways = data.Single().Start.Split(',').Select(s => s.Trim()).ToList(),
                LandingRunways = data.Single().Landing.Split(',').Select(s => s.Trim()).ToList(),
            };
        }
    }
}
