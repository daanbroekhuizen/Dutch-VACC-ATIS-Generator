using System;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Types
{
    public static class Runways
    {
        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        public static Dictionary<string, Tuple<int, int, string>> Beek = new Dictionary<string, Tuple<int, int, string>>()
        {
            {"03", new Tuple<int, int, string>(032, 212, "2")},
            {"21", new Tuple<int, int, string>(212, 032, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        public static Dictionary<string, Tuple<int, int, string>> Eindhoven = new Dictionary<string, Tuple<int, int, string>>()
        {
            {"03", new Tuple<int, int, string>(034, 214, "2")},
            {"21", new Tuple<int, int, string>(214, 034, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        public static Dictionary<string, Tuple<int, int, string>> Eelde = new Dictionary<string, Tuple<int, int, string>>()
        {
            {"01", new Tuple<int, int, string>(008, 214, "4")},
            {"05", new Tuple<int, int, string>(051, 231, "2")},
            {"19", new Tuple<int, int, string>(188, 008, "3")},
            {"23", new Tuple<int, int, string>(231, 051, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        public static Dictionary<string, Tuple<int, int, string>> Rotterdam = new Dictionary<string, Tuple<int, int, string>>()
        {
            {"06", new Tuple<int, int, string>(057, 257, "2")},
            {"24", new Tuple<int, int, string>(237, 037, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Day Preference, Night Preference>
        public static Dictionary<string, Tuple<int, int, string, string>> SchipholDeparture = new Dictionary<string, Tuple<int, int, string, string>>()
        {
            {"04", new Tuple<int, int, string, string>(041, 221, "10", "--")},
            {"06", new Tuple<int, int, string, string>(058, 238, "8", "4")},
            {"09", new Tuple<int, int, string, string>(087, 267, "6", "--")},
            {"18L", new Tuple<int, int, string, string>(183, 003, "4", "--")},
            {"18C", new Tuple<int, int, string, string>(183, 003, "5", "3")},
            {"22", new Tuple<int, int, string, string>(221, 041, "9", "--")},
            {"24", new Tuple<int, int, string, string>(238, 058, "2", "2")},
            {"27", new Tuple<int, int, string, string>(267, 087, "7", "--")},
            {"36L", new Tuple<int, int, string, string>(003, 183, "1", "1")},
            {"36C", new Tuple<int, int, string, string>(003, 183, "3", "--")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Day Preference, Night Preference>
        public static Dictionary<string, Tuple<int, int, string, string>> SchipholLanding = new Dictionary<string, Tuple<int, int, string, string>>()
        {
            {"04", new Tuple<int, int, string, string>(041, 221,  "9" , "--")},
            {"06", new Tuple<int, int, string, string>(058, 238, "1", "1")},
            {"09", new Tuple<int, int, string, string>(087, 267, "8", "--")},
            {"18C", new Tuple<int, int, string, string>(183, 003, "4", "--")},
            {"18R", new Tuple<int, int, string, string>(183, 003, "2", "2")},
            {"22", new Tuple<int, int, string, string>(221, 041, "7", "--")},
            {"24", new Tuple<int, int, string, string>(238, 058, "7", "--")},
            {"27", new Tuple<int, int, string, string>(267, 087, "6", "--")},
            {"36C", new Tuple<int, int, string, string>(003, 183, "5", "3")},
            {"36R", new Tuple<int, int, string, string>(003, 183, "3", "--")}
        };
    }
}
