using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DutchVACCATISGenerator.Types
{
    [ExcludeFromCodeCoverage]
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

        public static List<Tuple<string, string>> SchipholDepartureRunwayCombinations = new List<Tuple<String, String>>()
        {
            /* 36L combinations */
            { new Tuple<String, String>("36L", "24")},
            { new Tuple<String, String>("36L", "36C")},
            { new Tuple<String, String>("36L", "18L")},
            { new Tuple<String, String>("36L", "18C")},
            { new Tuple<String, String>("36L", "09")},
            { new Tuple<String, String>("36L", "27")},
            { new Tuple<String, String>("36L", "06")},
            { new Tuple<String, String>("36L", "22")},
            { new Tuple<String, String>("36L", "04")}, 
            /* 24 combinations */
            { new Tuple<String, String>("24", "36C")},
            { new Tuple<String, String>("24", "18L")},
            { new Tuple<String, String>("24", "18C")},
            { new Tuple<String, String>("24", "09")},
            { new Tuple<String, String>("24", "27")},
            { new Tuple<String, String>("24", "22")},
            { new Tuple<String, String>("24", "04")},
            /* 36C combinations */
            { new Tuple<String, String>("36C", "18L")},
            { new Tuple<String, String>("36C", "09")},
            { new Tuple<String, String>("36C", "27")},
            { new Tuple<String, String>("36C", "06")},
            { new Tuple<String, String>("36C", "22")},
            { new Tuple<String, String>("36C", "04")},
            /* 18L combinations */
            { new Tuple<String, String>("18L", "18C")},
            { new Tuple<String, String>("18L", "09")},
            { new Tuple<String, String>("18L", "27")},
            { new Tuple<String, String>("18L", "06")},
            { new Tuple<String, String>("18L", "22")},
            { new Tuple<String, String>("18L", "04")},
            /* 18C combinations */
            { new Tuple<String, String>("18C", "09")},
            { new Tuple<String, String>("18C", "27")},
            { new Tuple<String, String>("18C", "06")},
            { new Tuple<String, String>("18C", "22")},
            { new Tuple<String, String>("18C", "04")},
            /* 09 combinations */
            { new Tuple<String, String>("09", "27")},
            { new Tuple<String, String>("09", "06")},
            { new Tuple<String, String>("09", "22")},
            { new Tuple<String, String>("09", "04")},
            /* 27 combinations */
            { new Tuple<String, String>("27", "06")},
            { new Tuple<String, String>("27", "22")},
            { new Tuple<String, String>("27", "04")},
            /* 06 combinations */
            { new Tuple<String, String>("06", "22")},
            { new Tuple<String, String>("06", "04")},
            /* 22 combinations */
            { new Tuple<String, String>("22", "04")},
        };

        public static List<Tuple<string, string>> SchipholLandingRunwayCombinations = new List<Tuple<String, String>>()
        {
            /* 06 combinations */
            { new Tuple<String, String>("06", "18R")},
            { new Tuple<String, String>("06", "36R")},
            { new Tuple<String, String>("06", "18C")},
            { new Tuple<String, String>("06", "36C")},
            { new Tuple<String, String>("06", "27")},
            { new Tuple<String, String>("06", "22")},
            { new Tuple<String, String>("06", "09")},
            { new Tuple<String, String>("06", "04")},
            /* 18R combinations */
            { new Tuple<String, String>("18R", "36R")},
            { new Tuple<String, String>("18R", "18C")},
            { new Tuple<String, String>("18R", "36C")},
            { new Tuple<String, String>("18R", "27")},
            { new Tuple<String, String>("18R", "22")},
            { new Tuple<String, String>("18R", "24")},
            { new Tuple<String, String>("18R", "09")},
            { new Tuple<String, String>("18R", "04")},
            /* 36R combinations */
            { new Tuple<String, String>("36R", "18C")},
            { new Tuple<String, String>("36R", "36C")},
            { new Tuple<String, String>("36R", "27")},
            { new Tuple<String, String>("36R", "22")},
            { new Tuple<String, String>("36R", "24")},
            { new Tuple<String, String>("36R", "09")},
            { new Tuple<String, String>("36R", "04")},
            /* 18C combinations */
            { new Tuple<String, String>("18C", "27")},
            { new Tuple<String, String>("18C", "22")},
            { new Tuple<String, String>("18C", "24")},
            { new Tuple<String, String>("18C", "09")},
            { new Tuple<String, String>("18C", "04")},
            /* 36C combinations */
            { new Tuple<String, String>("36C", "27")},
            { new Tuple<String, String>("36C", "22")},
            { new Tuple<String, String>("36C", "24")},
            { new Tuple<String, String>("36C", "09")},
            { new Tuple<String, String>("36C", "04")},
            /* 27 combinations */
            { new Tuple<String, String>("27", "22")},
            { new Tuple<String, String>("27", "24")},
            { new Tuple<String, String>("27", "09")},
            { new Tuple<String, String>("27", "04")},
            /* 22 combinations */
            { new Tuple<String, String>("22", "24")},
            { new Tuple<String, String>("22", "09")},
            /* 24 combinations */
            { new Tuple<String, String>("24", "09")},
            { new Tuple<String, String>("24", "04")},
            /* 09 combinations */
            { new Tuple<String, String>("09", "04")},
        };
    }
}
