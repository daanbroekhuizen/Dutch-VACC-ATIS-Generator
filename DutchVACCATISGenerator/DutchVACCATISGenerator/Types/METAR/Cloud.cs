namespace DutchVACCATISGenerator.Types
{
    public class Cloud
    {
        public string cloudType { get; set; }
        public int altitude { get; set; }
        public string addition { get; set; }

        /// <summary>
        /// Constructs a MetarCloud with a cloud type and the altitude of the cloud.
        /// </summary>
        /// <param name="cloudType">Type of the cloud.</param>
        /// <param name="altitude">Altitude of the cloud.</param>
        public Cloud(string cloudType, int altitude)
        {
            this.cloudType = cloudType;
            this.altitude = altitude;
        }

        /// <summary>
        /// Constructs a MetarCloud with a cloud type, the altitude of the cloud and any addition to the cloud.
        /// </summary>
        /// <param name="cloudType">Type of the cloud.</param>
        /// <param name="altitude">Altitude of the cloud.</param>
        /// <param name="addition">Addition of the cloud.</param>
        public Cloud(string cloudType, int altitude, string addition)
        {
            this.cloudType = cloudType;
            this.altitude = altitude;
            this.addition = addition;
        }
    }
}
