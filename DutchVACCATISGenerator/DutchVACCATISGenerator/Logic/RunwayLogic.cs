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
        /// Calculates the cross wind component of a runway.
        /// </summary>
        /// <param name="runwayHeading">Runway heading</param>
        /// <param name="windHeading">Wind heading</param>
        /// <param name="windKnots">Wind speed (in knots)</param>
        /// <param name="gustMin">Wind gust min (in knots)</param>
        /// <param name="gustMax">Wind gust max (in knots)</param>
        /// <returns></returns>
        int CalculateCrosswindComponent(int runwayHeading, int windHeading, int windKnots, int? gustMin, int? gustMax);

        /// <summary>
        /// Calculates the tail wind component of a runway.
        /// </summary>
        /// <param name="oppositeRunwayHeading">Opposite runway heading</param>
        /// <param name="windHeading">Wind heading</param>
        /// <param name="windKnots">Wind speed (in knots)</param>
        /// <param name="gustMin">Wind gust min (in knots)</param>
        /// <param name="gustMax">Wind gust max (in knots)</param>
        /// <returns>Calculated tail wind component</returns>
        int CalculateTailwindComponent(int oppositeRunwayHeading, int windHeading, int windKnots, int? gustMin, int? gustMax);

        /// <summary>
        /// Gets the best preferred runway.
        /// </summary>
        /// <param name="runways">Runway list</param>
        /// <param name="frictionIndex">Selected index of friction combo box</param>
        /// <param name="RVR">RVR</param>
        /// <param name="RVRValues">Dictionary of RVR-values</param>
        /// <param name="visibility">Visibility</param>
        /// <param name="clouds">List of clouds</param>
        /// <param name="windHeading">Wind heading</param>
        /// <param name="windKnots">Wind speed (in knots)</param>
        /// <param name="gustMin">Wind gust min (in knots)</param>
        /// <param name="gustMax">Wind gust max (in knots)</param>
        /// <returns>Best runway to use</returns>
        string BestRunway(Dictionary<string, Tuple<int, int, string>> runways, int frictionIndex, bool RVR, Dictionary<string, int> RVRValues, int visibility, List<Cloud> clouds, int windHeading, int windKnots, int? gustMin, int? gustMax);

        /// <summary>
        /// Check if a runway complies with weather criteria for a runway.
        /// </summary>
        /// <param name="frictionIndex">Selected index of friction combo box</param>
        /// <param name="runway">Runway</param>
        /// <param name="RVR">RVR</param>
        /// <param name="RVRValues">Dictionary of RVR-values</param>
        /// <param name="visibility">Visibility</param>
        /// <param name="clouds">List of clouds</param>
        /// <param name="crosswind">Runway cross wind component</param>
        /// <param name="tailwind">Runway tail wind component</param>
        /// <returns>Indication if the a runway complies with the weather criteria for that runway</returns>
        string RunwayComplies(int frictionIndex, string runway, bool RVR, Dictionary<string, int> RVRValues, int visibility, List<Cloud> clouds, int crosswind, int tailwind);

        Task SchipholRunways();
    }

    public class RunwayLogic : IRunwayLogic
    {
        private const string schipholPlanningInterfaceURL = "https://spi.dutchvacc.nl/api_atisgenerator.php?action=retrieve_lvnlrunways";

        public string BestRunway(Dictionary<string, Tuple<int, int, string>> runways, int frictionIndex, bool RVR, Dictionary<string, int> RVRValues, int visibility, List<Cloud> clouds, int windHeading, int windKnots, int? gustMin, int? gustMax)
        {
            var preference = int.MaxValue;
            var runway = string.Empty;

            var validRunways = runways.Where((_runway) =>
            {
                var crosswind = CalculateCrosswindComponent(_runway.Value.Item1, windHeading, windKnots, gustMin, gustMax);
                var tailwind = CalculateTailwindComponent(_runway.Value.Item2, windHeading, windKnots, gustMin, gustMax);

                return RunwayComplies(frictionIndex, _runway.Key, RVR, RVRValues, visibility, clouds, crosswind, tailwind).Equals("OK");
            }).ToList();

            if (validRunways.Count() == 1)
                return validRunways.First().Key;
            else
            {
                //TODO this should look at the wind in degree relative to the heading of a runway and preference.
                foreach (var pair in validRunways)
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
            }

            return runway;
        }

        public int CalculateCrosswindComponent(int runwayHeading, int windHeading, int windKnots, int? gustMin, int? gustMax)
        {
            int crosswind;

            //If gust wind.
            if (gustMin.HasValue)
                //If gust is greater than 10 knots, include gust wind. Else do not include gust, calculate with min gust wind.
                crosswind = Convert.ToInt32(Math.Sin(DegreeToRadian(Math.Abs(windHeading - runwayHeading))) * (Math.Abs(gustMax.Value - gustMin.Value) >= 10 ? gustMax.Value : gustMin.Value));
            else
                crosswind = Convert.ToInt32(Math.Sin(DegreeToRadian(Math.Abs(windHeading - runwayHeading))) * windKnots);

            //If calculated crosswind is negative, multiply by -1 to make it positive.
            if (crosswind < -0)
                return crosswind * -1;
            else
                return crosswind;
        }

        public int CalculateTailwindComponent(int oppositeRunwayHeading, int windHeading, int windKnots, int? gustMin, int? gustMax)
        {
            //If gust wind.
            if (gustMin.HasValue)
                //If gust is greater than 10 knots, include gust wind. Else do not include gust, calculate with min gust wind.
                return Convert.ToInt32(Math.Cos(DegreeToRadian(Math.Abs(windHeading - oppositeRunwayHeading))) * (Math.Abs(gustMax.Value - gustMin.Value) >= 10 ? gustMax.Value : gustMin.Value));
            else
                return Convert.ToInt32(Math.Cos(DegreeToRadian(Math.Abs(windHeading - oppositeRunwayHeading))) * windKnots);
        }

        public string RunwayComplies(int frictionIndex, string runway, bool RVR, Dictionary<string, int> RVRValues, int visibility, List<Cloud> clouds, int crosswind, int tailwind)
        {
            switch (frictionIndex)
            {
                case 0:
                    return CompliesWithVisibility(runway, RVR, RVRValues, visibility, clouds) ?
                        CompliesWithWind(20, 7, crosswind, tailwind) :
                        CompliesWithWind(15, 7, crosswind, tailwind);

                case 1:
                case 2:
                    return CompliesWithVisibility(runway, RVR, RVRValues, visibility, clouds) ?
                        CompliesWithWind(10, 0, crosswind, tailwind) :
                        CompliesWithWind(10, 0, crosswind, tailwind);

                case 3:
                case 4:
                    return CompliesWithVisibility(runway, RVR, RVRValues, visibility, clouds) ?
                        CompliesWithWind(5, 0, crosswind, tailwind) :
                        CompliesWithWind(5, 0, crosswind, tailwind);

                default: return "ERROR";
            }
        }

        public async Task SchipholRunways()
        {
            ApplicationEvents.SchipholRunways(new SchipholRunwaysEventArgs
            {
                SchipholPlanningInterfaceData = await GetSchipholPlanningInterfaceData()
            });
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
        /// <param name="runway">Runway</param>
        /// <param name="RVR">RVR</param>
        /// <param name="RVRValues">Dictionary of RVR-values</param>
        /// <param name="visibility">Visibility</param>
        /// <param name="clouds">List of clouds</param>
        /// <returns>Boolean indicating if the runway visibility complies with the viability criteria for that runway</returns>
        private bool CompliesWithVisibility(string runway, bool RVR, Dictionary<string, int> RVRValues, int visibility, List<Cloud> clouds)
        {
            //If applicationVariables.METAR has a RVR.
            if (RVR)
            {
                foreach (var pair in RVRValues)
                {
                    //Check if runway complies with RVR criteria based on the RWY RVR value.
                    if (pair.Key.Equals(runway))
                        return CompliesWithRVR(pair.Value, clouds);
                }

                //Check if runway complies with RVR criteria based on the visibility.
                return CompliesWithRVR(visibility, clouds);
            }
            else
            {
                //If cloud layer > 200 feet.
                return (clouds.Count != 0 ? clouds.First().Altitude >= 2 : true);
            }
        }

        /// <summary>
        /// Check if a runway visibility complies with the RVR criteria for that runway.
        /// </summary>
        /// <param name="rvr">RVR</param>
        /// <param name="clouds">List of clouds</param>
        /// <returns>Boolean indicating if the runway visibility complies with the RVR criteria for that runway</returns>
        private bool CompliesWithRVR(int rvr, List<Cloud> clouds)
        {
            //If RVR > 500 and cloud layer is > 200 feet.
            if (rvr >= 550 && (clouds.Count != 0 ? clouds.First().Altitude >= 2 : true))
                return true;

            //If RVR < 500 and cloud layer is < 200 feet.
            else if (rvr < 550 || (clouds.Count != 0 && clouds.First().Altitude < 2))
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
