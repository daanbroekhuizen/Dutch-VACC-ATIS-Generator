﻿using DutchVACCATISGenerator.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutchVACCATISGenerator.Logic
{
    public interface IATISLogic
    {
        /// <summary>
        /// Generates ATIS output.
        /// </summary>
        /// <param name="metar">METAR</param>
        /// <param name="schipholMainLandingRunway">Selected Schiphol main landing runway</param>
        /// <param name="schipholMainDepartureRunway">Selected Schiphol main departure runway</param>
        /// <param name="schipholSecondaryLandingRunwayChecked">Indicates if Schiphol secondary landing selection is made</param>
        /// <param name="schipholSecondaryDepartureRunwayChecked">Indicates if Schiphol secondary departure selection is made</param>
        /// <param name="schipholSecondaryLandingRunway">Selected Schiphol secondary landing runway</param>
        /// <param name="schipholSecondaryDepartureRunway">Selected Schiphol secondary departure runway</param>
        /// <param name="regionalRunway">Selected regional runway</param>
        /// <returns>Generated output</returns>
        string GenerateOutput(METAR metar, string schipholMainLandingRunway, string schipholMainDepartureRunway,
            bool schipholSecondaryLandingRunwayChecked, bool secondaryDepartureRunwayChecked,
            string schipholSecondaryLandingRunway, string schipholSecondaryDepartureRunway, string regionalRunway);

        /// <summary>
        /// Sets phonetic alphabet.
        /// </summary>
        /// <param name="schiphol">Schiphol setting</param>
        /// <param name="rotterdam">Rotterdam setting</param>
        /// <param name="randomize">Randomize</param>
        /// <param name="reset">Reset</param>
        void SetPhoneticAlphabet(bool schiphol, bool rotterdam, bool randomize, bool reset = false);
    }

    public class ATISLogic : IATISLogic
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly IMETARLogic METARLogic;

        public ATISLogic(ApplicationVariables applicationVariables,
            IMETARLogic METARLogic)
        {
            this.applicationVariables = applicationVariables;
            this.METARLogic = METARLogic;
        }

        public string GenerateOutput(METAR METAR, string schipholMainLandingRunway, string schipholMainDepartureRunway,
            bool schipholSecondaryLandingRunwayChecked, bool schipholSecondaryDepartureRunwayChecked,
            string schipholSecondaryLandingRunway, string schipholSecondaryDepartureRunway, string regionalRunway)
        {
            applicationVariables.ATISSamples = new List<string>();

            var output = string.Empty;

            //Generate ICAO output.
            output += GenerateICAOOutput(METAR.ICAO);

            //Add ATIS letter to output/
            output += GenerateATISLetterOutput(applicationVariables.PhoneticAlphabet[applicationVariables.ATISIndex]);

            //Add pause.
            AddPause();

            //Add runway output to output.
            output += GenerateRunwayOutput(applicationVariables.SelectedAirport,
                schipholMainLandingRunway,
                schipholMainDepartureRunway,
                schipholSecondaryLandingRunwayChecked,
                schipholSecondaryDepartureRunwayChecked,
                schipholSecondaryLandingRunway,
                schipholSecondaryDepartureRunway,
                regionalRunway);

            //Add transition level to output.
            output += GenerateTransitionLevelOutput(METAR);

            //Add operational report to output.
            output += GenerateOperationalReportOutput(METAR,
                schipholSecondaryLandingRunwayChecked,
                schipholMainLandingRunway,
                schipholSecondaryLandingRunway);

            //Add pause.
            AddPause();

            //Add wind.
            output += GenerateWindOutput(METAR);

            //Add visibility.
            output += GenerateVisibilityOutput(METAR);

            //Add phenomena.
            output += GeneratePhenomenaOutput(METAR.Phenomena);


            #region TODO

            //#region CLOUDS OPTIONS
            ////If processed METAR has SKC, add SKC to output. 
            //if (metar.SKC)
            //{
            //    applicationVariables.ATISSamples.Add("skc");
            //    output += " SKY CLEAR";
            //}
            ////If processed METAR has NSC, add NSC to output. 
            //if (metar.NSC)
            //{
            //    applicationVariables.ATISSamples.Add("sc");
            //    output += " NO SIGNIFICANT CLOUDS";
            //}
            //#endregion

            //#region CLOUDS
            ////Generate and add weather clouds to output. 
            //output += listToOutput(metar.Clouds);
            //#endregion

            //#region VERTICAL VISIBILITY
            ////If processed METAR has a vertical visibility greater than 0, add vertical visibility to output.
            //if (metar.VerticalVisibility > 0)
            //{
            //    applicationVariables.ATISSamples.Add("vv");
            //    addIndividualDigitsToATISSamples(metar.VerticalVisibility.ToString());
            //    applicationVariables.ATISSamples.Add("hunderd");
            //    applicationVariables.ATISSamples.Add("meters");

            //    output += " VERTICAL VISIBILITY " + metar.VerticalVisibility + " HUNDERD METERS";
            //}
            //#endregion

            //TODO fix below
            //#region TEMPERATURE
            //Add temperature to output.
            //applicationVariables.ATISSamples.Add("temp");
            //output += " TEMPERATURE";

            //If processed METAR has a minus temperature.
            //if (metar.Temperature.StartsWith("M"))
            //{
            //    applicationVariables.ATISSamples.Add("minus");

            //    addIndividualDigitsToATISSamples(Convert.ToInt32(metar.Temperature.ToString().Substring(1, 2)).ToString());

            //    output += " MINUS " + Convert.ToInt32(metar.Temperature.ToString().Substring(1, 2));
            //}
            ////Positive temperature.
            //else
            //{
            //    addIndividualDigitsToATISSamples(Convert.ToInt32(metar.Temperature.ToString()).ToString());

            //    output += " " + Convert.ToInt32(metar.Temperature.ToString());
            //}
            //#endregion

            //TODO fix below
            //#region DEWPOINT
            //Add dewpoint to output.
            //applicationVariables.ATISSamples.Add("dp");
            //output += " DEWPOINT";

            //If processed METAR has a minus dewpoint.
            //if (metar.Dewpoint.StartsWith("M"))
            //{
            //    applicationVariables.ATISSamples.Add("minus");

            //    addIndividualDigitsToATISSamples(Convert.ToInt32(metar.Dewpoint.ToString().Substring(1, 2)).ToString());

            //    output += " MINUS " + Convert.ToInt32(metar.Dewpoint.ToString().Substring(1, 2));
            //}

            //Positive dewpoint.
            //else
            //{
            //    addIndividualDigitsToATISSamples(Convert.ToInt32(metar.Dewpoint.ToString()).ToString());

            //    output += " " + Convert.ToInt32(metar.Dewpoint.ToString());
            //}
            //#endregion

            //#region QNH
            ////Add QNH to output.
            //applicationVariables.ATISSamples.Add("qnh");
            //output += " QNH";
            //addIndividualDigitsToATISSamples(metar.QNH.ToString());
            //output += " " + metar.QNH.ToString();
            //applicationVariables.ATISSamples.Add("hpa");
            //output += " HECTOPASCAL";
            //#endregion

            //#region NOSIG
            ////If processed METAR has NOSIG, add NOSIG to output.
            //if (metar.NOSIG)
            //{
            //    applicationVariables.ATISSamples.Add("nosig");
            //    output += " NO SIGNIFICANT CHANGE";
            //}
            //#endregion

            //#region TEMPO
            ////If processed METAR has a TEMPO trend.
            //if (metar.TEMPO != null)
            //{
            //    //Add TEMPO to output.
            //    applicationVariables.ATISSamples.Add("tempo");
            //    output += " TEMPORARY";

            //    #region TEMPO WIND
            //    //If processed TEMPO trend has wind, generate and add wind output to output. 
            //    if (metar.TEMPO.Wind != null) output += windToOutput(metar.TEMPO.Wind);
            //    #endregion

            //    #region TEMPO CAVOK
            //    //If processed TEMPO trend has CAVOK, add CAVOK to output.
            //    if (metar.TEMPO.CAVOK)
            //    {
            //        applicationVariables.ATISSamples.Add("cavok");
            //        output += " CAVOK";
            //    }
            //    #endregion

            //    #region TEMPO VISIBILITY
            //    //If processed TEMPO trend has a visibility greater than 0, generate and add visibility output to output. 
            //    if (metar.TEMPO.Visibility > 0) output += visibilityToOutput(metar.TEMPO.Visibility);
            //    #endregion

            //    #region TEMPO PHENOMENA
            //    //If TEMPO trend has 1 or more weather phenomena, generate and add TEMPO trend weather phenomena to output.
            //    if (metar.TEMPO.Phenomena.Count > 0) output += listToOutput(metar.TEMPO.Phenomena);
            //    #endregion

            //    #region TEMPO SKC
            //    //If TEMPO trend has SKC, add SKC to output. 
            //    if (metar.TEMPO.SKC)
            //    {
            //        applicationVariables.ATISSamples.Add("skc");
            //        output += " SKY CLEAR";
            //    }
            //    #endregion

            //    #region TEMPO NSW
            //    //If TEMPO trend has NSW, add NSW to output. 
            //    if (metar.TEMPO.NSW)
            //    {
            //        applicationVariables.ATISSamples.Add("nsw");
            //        output += " NO SIGNIFICANT WEATHER";
            //    }
            //    #endregion

            //    #region TEMPO CLOUDS
            //    //If TEMPO trend has 1 or more weather clouds, generate and add TEMPO weather clouds to output. 
            //    if (metar.TEMPO.Clouds.Count > 0) output += listToOutput(metar.TEMPO.Clouds);
            //    #endregion

            //    #region TEMPO VERTICAL VISIBILITY
            //    //If TEMPO trend has a vertical visibility greater than 0, add TEMPO trend vertical visibility to output.
            //    if (metar.TEMPO.VerticalVisibility > 0)
            //    {
            //        applicationVariables.ATISSamples.Add("vv");
            //        addIndividualDigitsToATISSamples(metar.TEMPO.VerticalVisibility.ToString());
            //        applicationVariables.ATISSamples.Add("hunderd");
            //        applicationVariables.ATISSamples.Add("meters");

            //        output += " VERTICAL VISIBILITY " + metar.TEMPO.VerticalVisibility + " HUNDERD METERS";
            //    }
            //    #endregion
            //}
            //#endregion

            //#region BECMG
            ////If processed METAR has e BECMG trend.
            //if (metar.BECMG != null)
            //{
            //    //Add BECMG to output.
            //    applicationVariables.ATISSamples.Add("becmg");
            //    output += " BECOMING";

            //    #region BECMG WIND
            //    //If processed BECMG trend has wind, generate and add wind output to output.
            //    if (metar.BECMG.Wind != null) output += windToOutput(metar.BECMG.Wind);
            //    #endregion

            //    #region BECMG CAVOK
            //    //If processed BECMG trend has CAVOK, add CAVOK to output.
            //    if (metar.BECMG.CAVOK)
            //    {
            //        applicationVariables.ATISSamples.Add("cavok");
            //        output += " CAVOK";
            //    }
            //    #endregion

            //    #region BECMG VISIBILITY
            //    //If processed BECMG trend has a visibility greater than 0, generate and add visibility output to output. 
            //    if (metar.BECMG.Visibility > 0) output += visibilityToOutput(metar.BECMG.Visibility);
            //    #endregion

            //    #region BECMG PHENOMENA
            //    //If BECMG trend has 1 or more weather phenomena, generate and add BECMG trend weather phenomena to output.
            //    if (metar.BECMG.Phenomena.Count > 0) output += listToOutput(metar.BECMG.Phenomena);
            //    #endregion

            //    #region BECMG SKC
            //    //If BECMG trend has SKC, add SKC to output. 
            //    if (metar.BECMG.SKC)
            //    {
            //        applicationVariables.ATISSamples.Add("skc");
            //        output += " SKY CLEAR";
            //    }
            //    #endregion

            //    #region BECMG NSW
            //    //If BECMG trend has NSW, add NSW to output. 
            //    if (metar.BECMG.NSW)
            //    {
            //        applicationVariables.ATISSamples.Add("nsw");
            //        output += " NO SIGNIFICANT WEATHER";
            //    }
            //    #endregion

            //    #region BECMG CLOUDS
            //    //If BECMG trend has 1 or more weather clouds, generate and add BECMG weather clouds to output. 
            //    if (metar.BECMG.Clouds.Count > 0) output += listToOutput(metar.BECMG.Clouds);
            //    #endregion

            //    #region BECMG VERTICAL VISIBILITY
            //    //If BECMG trend has a vertical visibility greater than 0, add BECMG trend vertical visibility to output.
            //    if (metar.BECMG.VerticalVisibility > 0)
            //    {
            //        applicationVariables.ATISSamples.Add("vv");
            //        addIndividualDigitsToATISSamples(metar.BECMG.VerticalVisibility.ToString());
            //        applicationVariables.ATISSamples.Add("hunderd");
            //        applicationVariables.ATISSamples.Add("meters");

            //        output += " VERTICAL VISIBILITY" + metar.BECMG.VerticalVisibility + " HUNDERD METERS";
            //    }
            //    #endregion
            //}
            //#endregion

            //#region OPTIONAL
            ////If inverted surface temperature check box is checked.
            ////if (markTempCheckBox.Checked)
            ////{
            ////    applicationVariables.ATISSamples.Add("marktemp");
            ////    output += " MARK TEMPERATURE INVERSION NEAR THE SURFACE";
            ////}
            //////If arrival only check box is checked.
            ////if (arrOnlyCheckBox.Checked)
            ////{
            ////    applicationVariables.ATISSamples.Add("call1");
            ////    output += " CONTACT ARRIVAL CALLSIGN ONLY";
            ////}
            //////If approach only check box is checked.
            ////if (appOnlyCheckBox.Checked)
            ////{
            ////    applicationVariables.ATISSamples.Add("call2");
            ////    output += " CONTACT APPROACH CALLSIGN ONLY";
            ////}
            //////If arrival and approach only check box is checked.
            ////if (appArrOnlyCheckBox.Checked)
            ////{
            ////    applicationVariables.ATISSamples.Add("call3");
            ////    output += " CONTACT APPROACH AND ARRIVAL CALLSIGN ONLY";
            ////}
            //#endregion

            //#region END
            ////Add end to output.
            //applicationVariables.ATISSamples.Add("end");
            //output += " END OF INFORMATION";
            ////output += atisLetterToFullSpelling(phoneticAlphabet[atisIndex]);
            ////#endregion

            ////#region USER WAVE
            ////if (userDefinedExtraCheckBox.Checked)
            ////{
            ////    applicationVariables.ATISSamples.Add("extra");
            ////    output += " EXTRA (VOICE ONLY)";
            ////}
            #endregion

            return output;
        }

        public void SetPhoneticAlphabet(bool schiphol, bool rotterdam, bool randomnize, bool reset = false)
        {
            //If selected tab is EHAM and EHAM (A - M) tool strip menu item is checked.
            if (schiphol && applicationVariables.SelectedAirport.Equals("EHAM"))
                applicationVariables.PhoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };

            //If selected tab is EHRD and EHRD (N - Z) tool strip menu item is checked.
            else if (rotterdam && applicationVariables.SelectedAirport.Equals("EHRD"))
                applicationVariables.PhoneticAlphabet = new List<String> { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            //Else set full phonetic alpha bet.
            else
                applicationVariables.PhoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            //Reset ATIS index to 0.
            if (reset)
            {
                applicationVariables.ATISIndex = 0;
                return;
            }

            if (randomnize)
            {
                applicationVariables.ATISIndex = new Random().Next(0, applicationVariables.PhoneticAlphabet.Count - 1);
            }

            //If the current index is higher than the phonetic alphabet count (will cause exception!).
            if (applicationVariables.ATISIndex > applicationVariables.PhoneticAlphabet.Count)
                applicationVariables.ATISIndex = 0;
        }

        /// <summary>
        /// Generates ICAO output.
        /// </summary>
        /// <param name="ICAO">Airport ICAO</param>
        /// <returns>Generated output</returns>
        private string GenerateICAOOutput(string ICAO)
        {
            switch (ICAO)
            {
                //If processed ICAO is EHAM.
                case "EHAM":
                    applicationVariables.ATISSamples.Add("ehamatis");
                    return "THIS IS SCHIPHOL INFORMATION";

                //If processed ICAO is EHBK.
                case "EHBK":
                    applicationVariables.ATISSamples.Add("ehbkatis");
                    return "THIS IS BEEK INFORMATION";

                //If processed ICAO is EHEH.
                case "EHEH":
                    applicationVariables.ATISSamples.Add("ehehatis");
                    return "THIS IS EINDHOVEN INFORMATION";

                //If processed ICAO is EHGG.
                case "EHGG":
                    applicationVariables.ATISSamples.Add("ehggatis");
                    return "THIS IS EELDE INFORMATION";

                //If processed ICAO is EHRD.
                case "EHRD":
                    applicationVariables.ATISSamples.Add("ehrdatis");
                    return "THIS IS ROTTERDAM INFORMATION";
            }

            return string.Empty;
        }

        /// <summary>
        /// Generates ATIS letter output.
        /// </summary>
        /// <param name="ATISLetter">ATIS letter</param>
        /// <returns>Generated output</returns>
        private string GenerateATISLetterOutput(string ATISLetter)
        {
            switch (ATISLetter)
            {
                case "A":
                    applicationVariables.ATISSamples.Add("a");
                    return " ALPHA";

                case "B":
                    applicationVariables.ATISSamples.Add("b");
                    return " BRAVO";

                case "C":
                    applicationVariables.ATISSamples.Add("c");
                    return " CHARLIE";

                case "D":
                    applicationVariables.ATISSamples.Add("d");
                    return " DELTA";

                case "E":
                    applicationVariables.ATISSamples.Add("e");
                    return " ECHO";

                case "F":
                    applicationVariables.ATISSamples.Add("f");
                    return " FOXTROT";

                case "G":
                    applicationVariables.ATISSamples.Add("g");
                    return " GOLF";

                case "H":
                    applicationVariables.ATISSamples.Add("h");
                    return " HOTEL";

                case "I":
                    applicationVariables.ATISSamples.Add("i");
                    return " INDIA";

                case "J":
                    applicationVariables.ATISSamples.Add("j");
                    return " JULIET";

                case "K":
                    applicationVariables.ATISSamples.Add("k");
                    return " KILO";

                case "L":
                    applicationVariables.ATISSamples.Add("l");
                    return " LIMA";

                case "M":
                    applicationVariables.ATISSamples.Add("m");
                    return " MIKE";

                case "N":
                    applicationVariables.ATISSamples.Add("n");
                    return " NOVEMBER";

                case "O":
                    applicationVariables.ATISSamples.Add("o");
                    return " OSCAR";

                case "P":
                    applicationVariables.ATISSamples.Add("p");
                    return " PAPA";

                case "Q":
                    applicationVariables.ATISSamples.Add("q");
                    return " QUEBEC";

                case "R":
                    applicationVariables.ATISSamples.Add("r");
                    return " ROMEO";

                case "S":
                    applicationVariables.ATISSamples.Add("s");
                    return " SIERRA";

                case "T":
                    applicationVariables.ATISSamples.Add("t");
                    return " TANGO";

                case "U":
                    applicationVariables.ATISSamples.Add("u");
                    return " UNIFORM";

                case "V":
                    applicationVariables.ATISSamples.Add("v");
                    return " VICTOR";

                case "W":
                    applicationVariables.ATISSamples.Add("w");
                    return " WHISKEY";

                case "X":
                    applicationVariables.ATISSamples.Add("x");
                    return " XRAY";

                case "Y":
                    applicationVariables.ATISSamples.Add("y");
                    return " YANKEE";

                case "Z":
                    applicationVariables.ATISSamples.Add("z");
                    return " ZULU";
            }

            return string.Empty;
        }

        /// <summary>
        /// Adds pause to ATIS samples.
        /// </summary>
        private void AddPause()
        {
            applicationVariables.ATISSamples.Add("pause");
        }

        /// <summary>
        /// Generates runway output.
        /// </summary>
        /// <param name="selectedAirport">Selected airport</param>
        /// <param name="schipholMainLandingRunway">Selected Schiphol main landing runway</param>
        /// <param name="schipholMainDepartureRunway">Selected Schiphol main departure runway</param>
        /// <param name="schipholSecondaryLandingRunwayChecked">Indicates if Schiphol secondary landing selection is made</param>
        /// <param name="schipholSecondaryDepartureRunwayChecked">Indicates if Schiphol secondary departure selection is made</param>
        /// <param name="schipholSecondaryLandingRunway">Selected Schiphol secondary landing runway</param>
        /// <param name="schipholSecondaryDepartureRunway">Selected Schiphol secondary departure runway</param>
        /// <param name="regionalRunway">Selected regional runway</param>
        /// <returns>Generated output</returns>
        private string GenerateRunwayOutput(string selectedAirport, string schipholMainLandingRunway, string schipholMainDepartureRunway,
            bool schipholSecondaryLandingRunwayChecked, bool schipholSecondaryDepartureRunwayChecked, string schipholSecondaryLandingRunway,
            string schipholSecondaryDepartureRunway, string regionalRunway)
        {
            string output = string.Empty;

            switch (selectedAirport)
            {
                case "EHAM":
                    //Schiphol main landing runway.
                    applicationVariables.ATISSamples.Add("mlrwy");
                    output += GenerateRunwayOutput(" MAIN LANDING RUNWAY", schipholMainLandingRunway);

                    //Schiphol secondary landing runway.
                    if (schipholSecondaryLandingRunwayChecked)
                    {
                        applicationVariables.ATISSamples.Add("slrwy");
                        output += GenerateRunwayOutput(" SECONDARY LANDING RUNWAY", schipholSecondaryLandingRunway);
                    }

                    //Schiphol main departure runway.
                    if (!string.Equals(schipholMainLandingRunway, schipholMainDepartureRunway))
                    {
                        applicationVariables.ATISSamples.Add("mtrwy");
                        output += GenerateRunwayOutput(" MAIN TAKEOFF RUNWAY", schipholMainDepartureRunway);
                    }

                    //Schiphol secondary departure runway.
                    if (schipholSecondaryDepartureRunwayChecked)
                    {
                        applicationVariables.ATISSamples.Add("strwy");
                        output += GenerateRunwayOutput(" SECONDARY TAKEOFF RUNWAY", schipholSecondaryDepartureRunway);
                    }
                    break;

                case "EHBK":
                case "EHEH":
                case "EHGG":
                case "EHRD":
                    applicationVariables.ATISSamples.Add("mlrwy");
                    output += GenerateRunwayOutput(" MAIN LANDING RUNWAY", regionalRunway);
                    break;
            }

            return output;
        }

        /// <summary>
        /// Generates runway output.
        /// </summary>
        /// <param name="intro">Runway intro text</param>
        /// <param name="runway">Runway</param>
        /// <returns>Generated output</returns>
        private string GenerateRunwayOutput(string intro, string runway)
        {
            var output = $"{intro} ";

            //Get digits from runway and add to ATIS samples.
            AddIndividualDigits(runway);

            //If selected runway contains runway identifier letter.
            if (runway.Length > 2)
            {
                //Add runway identifier numbers to output.
                output += runway.Substring(0, 2);
                //Add runway identifier letter to output.
                return output += GenerateRunwayOutput(runway.Substring(2));
            }
            else
                return output += runway;
        }

        /// <summary>
        /// Generates runway output.
        /// </summary>
        /// <param name="runwayMarker">Runway marker</param>
        /// <returns>Generated output</returns>
        private string GenerateRunwayOutput(string runwayMarker)
        {
            switch (runwayMarker)
            {
                case "L":
                    applicationVariables.ATISSamples.Add("left");
                    return "L";

                case "C":
                    applicationVariables.ATISSamples.Add("center");
                    return "C";

                case "R":
                    applicationVariables.ATISSamples.Add("right");
                    return "R";

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Generates transition level output.
        /// </summary>
        /// <param name="metar">METAR</param>
        /// <returns>Generated output</returns>
        private string GenerateTransitionLevelOutput(METAR METAR)
        {
            string output = string.Empty;

            applicationVariables.ATISSamples.Add("trl");
            output += " TRANSITION LEVEL";

            //Calculate and add transition level to output.
            var transitionLevel = METARLogic.CalculateTransitionLevel(METAR.Temperature, METAR.QNH);

            //Add to samples.
            AddIndividualDigits(transitionLevel.ToString());

            //Add to output.
            output += " " + transitionLevel;

            return output;
        }

        /// <summary>
        /// Adds individual digits to ATIS samples.
        /// </summary>
        /// <param name="input">Input</param>
        private void AddIndividualDigits(string input)
        {
            applicationVariables.ATISSamples.AddRange(input.Where(Char.IsDigit).Select(c => c.ToString()));
        }

        /// <summary>
        /// Generates operational report output.
        /// </summary>
        /// <param name="METAR">METAR</param>
        /// <param name="schipholSecondaryLandingRunwayChecked">Indicates if Schiphol secondary landing selection is made</param>
        /// <param name="schipholMainLandingRunway">Selected Schiphol main landing runway</param>
        /// <param name="schipholSecondaryLandingRunway">Selected Schiphol secondary landing runway</param>
        /// <returns>Generated output</returns>
        private string GenerateOperationalReportOutput(METAR METAR, bool schipholSecondaryLandingRunwayChecked, string schipholMainLandingRunway, string schipholSecondaryLandingRunway)
        {
            applicationVariables.ATISSamples.Add("opr");

            var output = " OPERATIONAL REPORT";

            bool independent = false;
            bool converging = false;

            //If visibility is not 0 or less than 1500 meter, add low visibility procedure phrase.
            output += GenerateLowVisibilityOutput(METAR, out bool visibility);

            if (schipholSecondaryLandingRunwayChecked)
            {
                output += GenerateIndependentParallelApproachesOutput(schipholMainLandingRunway, schipholSecondaryLandingRunway, visibility, out independent);
                output += GenerateConvergingApproachesOutput(schipholMainLandingRunway, schipholSecondaryLandingRunway, visibility, out converging);
            }

            if (visibility || independent || converging)
                return output;

            //Not operation report generated, remove sample.
            applicationVariables.ATISSamples.Remove("opr");

            return string.Empty;
        }

        /// <summary>
        /// Generates low viability output.
        /// </summary>
        /// <param name="METAR">METAR</param>
        /// <param name="visibility">Visibility operational report generated</param>
        /// <returns>Generated output</returns>
        private string GenerateLowVisibilityOutput(METAR METAR, out bool visibility)
        {
            if (METAR.Visibility != 0 && METAR.Visibility < 1500)
            {
                applicationVariables.ATISSamples.Add("lvp");
                visibility = true;
                return " LOW VISIBILITY PROCEDURES IN PROGRESS";
            }

            visibility = false;
            return string.Empty;
        }

        /// <summary>
        /// Generates independent parallel approaches output.
        /// </summary>
        /// <param name="schipholMainLandingRunway">Selected Schiphol main landing runway</param>
        /// <param name="schipholSecondaryLandingRunway">Selected Schiphol secondary landing runway</param>
        /// <param name="visibility">Visibility operational report generated</param>
        /// <param name="independent">Independent approaches operational report generated</param>
        /// <returns>Generated output</returns>
        private string GenerateIndependentParallelApproachesOutput(string schipholMainLandingRunway, string schipholSecondaryLandingRunway, bool visibility, out bool independent)
        {
            string output = string.Empty;

            if ((schipholMainLandingRunway.Equals("18R") && schipholSecondaryLandingRunway.Equals("18C")) ||
                (schipholMainLandingRunway.Equals("18C") && schipholSecondaryLandingRunway.Equals("18R")) ||
                (schipholMainLandingRunway.Equals("36R") && schipholSecondaryLandingRunway.Equals("36C")) ||
                (schipholMainLandingRunway.Equals("36C") && schipholSecondaryLandingRunway.Equals("36R")))
            {
                independent = true;

                if (visibility)
                    output += AddAnd();

                applicationVariables.ATISSamples.Add("independent");
                return output += " INDEPENDENT PARALLEL APPROACHES IN PROGRESS";
            }

            independent = false;
            return output;
        }

        /// <summary>
        /// Generates converging approaches output.
        /// </summary>
        /// <param name="schipholMainLandingRunway">Selected Schiphol main landing runway</param>
        /// <param name="schipholSecondaryLandingRunway">Selected Schiphol secondary landing runway</param>
        /// <param name="visibility">Visibility operational report generated</param>
        /// <param name="converging">Converging approaches operational report generated</param>
        /// <returns>Generated output</returns>
        private string GenerateConvergingApproachesOutput(string schipholMainLandingRunway, string schipholSecondaryLandingRunway, bool visibility, out bool converging)
        {
            string output = string.Empty;

            if (//06
                (schipholMainLandingRunway.Equals("06") && schipholSecondaryLandingRunway.Equals("36R")) ||
                (schipholMainLandingRunway.Equals("36R") && schipholSecondaryLandingRunway.Equals("06")) ||
                (schipholMainLandingRunway.Equals("06") && schipholSecondaryLandingRunway.Equals("27")) ||
                (schipholMainLandingRunway.Equals("27") && schipholSecondaryLandingRunway.Equals("06")) ||
                //09
                (schipholMainLandingRunway.Equals("06") && schipholSecondaryLandingRunway.Equals("09")) ||
                (schipholMainLandingRunway.Equals("09") && schipholSecondaryLandingRunway.Equals("06")) ||
                //18C
                (schipholMainLandingRunway.Equals("06") && schipholSecondaryLandingRunway.Equals("18C")) ||
                (schipholMainLandingRunway.Equals("18C") && schipholSecondaryLandingRunway.Equals("06")) ||
                //27
                (schipholMainLandingRunway.Equals("18C") && schipholSecondaryLandingRunway.Equals("27")) ||
                (schipholMainLandingRunway.Equals("27") && schipholSecondaryLandingRunway.Equals("18C")) ||
                (schipholMainLandingRunway.Equals("18R") && schipholSecondaryLandingRunway.Equals("27")) ||
                (schipholMainLandingRunway.Equals("27") && schipholSecondaryLandingRunway.Equals("18R")) ||
                //36C
                (schipholMainLandingRunway.Equals("27") && schipholSecondaryLandingRunway.Equals("36C")) ||
                (schipholMainLandingRunway.Equals("36C") && schipholSecondaryLandingRunway.Equals("27")) ||
                //36R
                (schipholMainLandingRunway.Equals("27") && schipholSecondaryLandingRunway.Equals("36R")) ||
                (schipholMainLandingRunway.Equals("36R") && schipholSecondaryLandingRunway.Equals("27")))
            {
                converging = true;

                if (visibility)
                    output += AddAnd();

                applicationVariables.ATISSamples.Add("convapp");
                return output += " CONVERGING APPROACHES IN PROGRESS";
            }

            converging = false;
            return output;
        }

        /// <summary>
        /// Adds and to ATIS samples.
        /// </summary>
        /// <returns>Generated output</returns>
        private string AddAnd()
        {
            applicationVariables.ATISSamples.Add("and");
            return " AND";
        }

        /// <summary>
        /// Generates wind output.
        /// </summary>
        /// <param name="METAR">METAR</param>
        /// <returns>Generated output</returns>
        private string GenerateWindOutput(METAR METAR)
        {
            string output = string.Empty;

            applicationVariables.ATISSamples.Add("wind");
            output += " WIND";

            //If wind is calm.
            if (METAR.Wind.Variable)
            {
                applicationVariables.ATISSamples.Add("vrb");

                AddIndividualDigits(METAR.Wind.Knots.ToString());

                output += " VARIABLE " + METAR.Wind.Knots + " KNOTS";
            }
            else
            {
                AddIndividualDigits(METAR.Wind.Heading.ToString());

                applicationVariables.ATISSamples.Add("deg");

                output += " " + METAR.Wind.Heading + " DEGREES";

                //If wind is gusting.
                if (METAR.Wind.GustMin != null)
                {
                    AddIndividualDigits(METAR.Wind?.GustMin.Value.ToString());

                    applicationVariables.ATISSamples.Add("max");

                    AddIndividualDigits(METAR.Wind?.GustMax.Value.ToString());

                    output += " " + METAR.Wind.GustMin + " MAXIMUM " + METAR.Wind.GustMax + " KNOTS";
                }
                //If MetarWind has a normal wind.
                else
                {
                    AddIndividualDigits(METAR.Wind.Knots.ToString());

                    output += " " + METAR.Wind.Knots + " KNOTS";
                }
            }

            //Add knots record.
            applicationVariables.ATISSamples.Add("kt");

            //Variable wind
            if (METAR.Wind.VariableLeft != null)
            {
                applicationVariables.ATISSamples.Add("vrbbtn");

                AddIndividualDigits(METAR.Wind?.VariableLeft.Value.ToString());

                applicationVariables.ATISSamples.Add("and");

                AddIndividualDigits(METAR.Wind.VariableRight.Value.ToString());

                applicationVariables.ATISSamples.Add("deg");

                output += " VARIABLE BETWEEN " + METAR.Wind.VariableLeft + " AND " + METAR.Wind.VariableRight + " DEGREES";
            }

            return output;
        }

        /// <summary>
        /// Generates visibility output.
        /// </summary>
        /// <param name="METAR">METAR</param>
        /// <returns>Generated output</returns>
        private string GenerateVisibilityOutput(METAR METAR)
        {
            //CAVOK
            if (METAR.CAVOK)
            {
                applicationVariables.ATISSamples.Add("cavok");
                return " CAVOK";
            }
            else
            {
                var output = string.Empty;

                //If processed METAR has a visibility greater than 0, generate and add visibility output to output. 
                if (METAR.Visibility > 0)
                    output += GenerateVisibilityOutput(METAR.Visibility);

                //If processed METAR has RVR, add RVR to output. 
                if (METAR.RVR)
                {
                    applicationVariables.ATISSamples.Add("rvronatc");
                    output += " RVR AVAILABLE ON ATC FREQUENCY";
                }

                return output;
            }
        }

        /// <summary>
        /// Generates visibility output.
        /// </summary>
        /// <param name="visibility">Visibility</param>
        /// <returns>Generated output</returns>
        private string GenerateVisibilityOutput(int visibility)
        {
            applicationVariables.ATISSamples.Add("vis");
            string output = " VISIBILITY";

            //If visibility is lower than 800 meters (less than 800 meters phrase).
            if (visibility < 800)
            {
                applicationVariables.ATISSamples.Add("<");
                AddIndividualDigits("8");
                applicationVariables.ATISSamples.Add("hundred");
                applicationVariables.ATISSamples.Add("meters");

                output += " LESS THAN 8 HUNDRED METERS";
            }
            //If visibility is lower than 1000 meters (add hundred).
            else if (visibility < 1000)
            {
                AddIndividualDigits(Convert.ToString(visibility / 100));
                applicationVariables.ATISSamples.Add("hundred");
                applicationVariables.ATISSamples.Add("meters");

                output += " " + Convert.ToString(visibility / 100) + " HUNDRED METERS";
            }
            //If visibility is lower than 5000 meters and visibility is not a thousand number.
            else if (visibility < 5000 && (visibility % 1000) != 0)
            {
                AddIndividualDigits(Convert.ToString(visibility / 1000));
                applicationVariables.ATISSamples.Add("thousand");
                AddIndividualDigits(Convert.ToString((visibility % 1000) / 100));
                applicationVariables.ATISSamples.Add("hundred");
                applicationVariables.ATISSamples.Add("meters");

                output += " " + Convert.ToString(visibility / 1000) + " THOUSAND " + Convert.ToString((visibility % 1000) / 100) + " HUNDRED METERS";
            }
            //If visibility is >= 9999 (10 km phrase).
            else if (visibility >= 9999)
            {
                AddIndividualDigits("10");
                applicationVariables.ATISSamples.Add("km");

                output += " 10 KILOMETERS";
            }
            //If visibility is thousand.
            else
            {
                AddIndividualDigits(Convert.ToString(visibility / 1000));
                applicationVariables.ATISSamples.Add("km");

                output += " " + Convert.ToString(visibility / 1000) + " KILOMETERS";
            }

            return output;
        }

        /// <summary>
        /// Generates phenomena output.
        /// </summary>
        /// <param name="phenomenas">List of phenomena</param>
        /// <returns>Generated output</returns>
        private string GeneratePhenomenaOutput(List<Phenomena> phenomenas)
        {
            string output = string.Empty;

            foreach (var phenomena in phenomenas)
            {
                switch (phenomena.Intensity)
                {
                    case PhenomenaItensity.NORMAL:
                        break;
                    case PhenomenaItensity.LIGHT:
                        applicationVariables.ATISSamples.Add("-");
                        output += " LIGHT";
                        break;

                    case PhenomenaItensity.HEAVY:
                        //TODO ADD SAMPLE FOR + intensity;
                        applicationVariables.ATISSamples.Add("+");
                        output += " HEAVY";
                        break;
                }
                
                var fourCharacter = new List<string> { "BCFG", "MIFG", "SHRA", "VCSH", "VCTS" };

                //If phenomena is 4 character phenomena (BCFG | MIFG | SHRA | VCSH | VCTS).
                if (fourCharacter.Any(fc => phenomena.Type.Equals(fc)))
                {
                    switch (phenomena.Type)
                    {
                        case "BCFG":
                            applicationVariables.ATISSamples.Add("bcfg");
                            output += " PATCHES OF FOG";
                            break;

                        case "MIFG":
                            applicationVariables.ATISSamples.Add("mifg");
                            output += " SHALLOW FOG";
                            break;

                        case "SHRA":
                            applicationVariables.ATISSamples.Add("shra");
                            output += " SHOWERS OF RAIN";
                            break;

                        case "VCSH":
                            applicationVariables.ATISSamples.Add("vcsh");
                            output += " SHOWERS IN VICINITY";
                            break;

                        case "VCTS":
                            applicationVariables.ATISSamples.Add("vcts");
                            output += " THUNDERSTORMS IN VICINITY";
                            break;
                    }
                }
                //If phenomena is multi-phenomena.
                else if (phenomena.Type.Length > 2)
                {
                    if ((phenomena.Type.Length % 2) == 0)
                    {
                        int index = 0;

                        while (index != phenomena.Type.Length)
                        {
                            if (!(phenomena.Type.Length - index == 2))
                                output += GeneratePhenomenaOutput(phenomena.Type.Substring(index, 2));

                            else
                                output += GeneratePhenomenaOutput(phenomena.Type.Substring(index));

                            index = index + 2;
                        }
                    }
                }
                //If phenomena is 2 char phenomena.
                else
                    output += GeneratePhenomenaOutput(phenomena.Type);

                //If multiple phenomena and not the last of phenomena list, add [and].
                if (phenomenas.Count > 1 && phenomenas.IndexOf(phenomena) != phenomenas.Count - 1)
                    AddAnd();
            }

            return output;
        }

        /// <summary>
        /// Generates phenomena output.
        /// </summary>
        /// <param name="phenomenaType">Phenomena type</param>
        /// <returns>Generated output</returns>
        private string GeneratePhenomenaOutput(string phenomenaType)
        {
            switch (phenomenaType)
            {
                case "BC":
                    applicationVariables.ATISSamples.Add("bc");
                    return " PATCHES";

                //TODO GET SAMPLE
                case "BL":
                    return " BLOWING";

                case "BR":
                    applicationVariables.ATISSamples.Add("br");
                    return " MIST";

                //TODO GET SAMPLE
                case "DR":
                    return " LOW DRIFTING";

                //TODO GET SAMPLE
                case "DS":
                    return " DUSTSTORM";

                //TODO GET SAMPLE
                case "DU":
                    return " WIDESPREAD DUST";

                case "DZ":
                    applicationVariables.ATISSamples.Add("dz");
                    return " DRIZZLE";

                //TODO GET SAMPLE
                case "FC":
                    return " FUNNEL CLOUD";

                case "FG":
                    applicationVariables.ATISSamples.Add("fg");
                    return " FOG";

                //TODO GET SAMPLE
                case "FU":
                    return " SMOKE";

                case "FZ":
                    applicationVariables.ATISSamples.Add("fz");
                    return " FREEZING";

                case "GR":
                    applicationVariables.ATISSamples.Add("gr");
                    return " HAIL";

                case "GS":
                    applicationVariables.ATISSamples.Add("gs");
                    return " SOFT HAIL";

                case "HZ":
                    applicationVariables.ATISSamples.Add("hz");
                    return " HAZE";

                //TODO GET SAMPLE
                case "IC":
                    return " ICE CRYSTALS";

                case "MI":
                    applicationVariables.ATISSamples.Add("mi");
                    return " SHALLOW";

                //TODO GET SAMPLE
                case "PL":
                    return " ICE PELLETS";

                //TODO GET SAMPLE
                case "PO":
                    return " DUST";

                //TODO GET SAMPLE
                case "PR":
                    return " PARTIAL";

                //TODO GET SAMPLE
                case "PY":
                    return " SPRAY";

                case "RA":
                    applicationVariables.ATISSamples.Add("ra");
                    return " RAIN";

                //TODO GET SAMPLE
                case "SA":
                    return " SAND";

                case "SG":
                    applicationVariables.ATISSamples.Add("sg");
                    return " SNOW GRAINS";

                case "SH":
                    applicationVariables.ATISSamples.Add("sh");
                    return " SHOWERS";

                //TODO check SNOW sample.
                case "SN":
                    applicationVariables.ATISSamples.Add("sn");
                    return " SNOW";

                //TODO GET SAMPLE
                case "SS":
                    return " SANDSTORM";

                //TODO GET SAMPLE
                case "SQ":
                    return " SQUALL";

                case "TS":
                    applicationVariables.ATISSamples.Add("ts");
                    return " THUNDERSTORMS";

                //TODO GET SAMPLE
                case "UP":
                    return " UNKOWN PRECIPITATION";

                //TODO GET SAMPLE
                case "VA":
                    return " VOLCANIC ASH";
            }

            return string.Empty;
        }

















        private string GenerateCloudOutput(List<Cloud> clouds)
        {
            //        foreach (Cloud metarCloud in input as List<Cloud>)
            //        {
            //            //TODO fix
            //            //Add cloud type identifier.
            //            //output += cloudTypeToFullSpelling(metarCloud.cloudType);

            //            //If cloud altitude equals ground level.
            //            if (metarCloud.Altitude == 0)
            //            {
            //                addIndividualDigitsToATISSamples(metarCloud.Altitude.ToString());

            //                output += " " + metarCloud.Altitude;
            //            }

            //            //If cloud altitude is round ten-thousand (e.g. 10000 (100), 20000 (200), 30000 (300)).
            //            else if (metarCloud.Altitude % 100 == 0)
            //            {
            //                addIndividualDigitsToATISSamples(Math.Floor(Convert.ToDouble(metarCloud.Altitude / 100)).ToString() + "0");
            //                applicationVariables.ATISSamples.Add("thousand");

            //                output += " " + Math.Floor(Convert.ToDouble(metarCloud.Altitude / 100)).ToString() + "0" + " THOUSAND";
            //            }

            //            else
            //            {
            //                //If cloud altitude is greater than a ten-thousand (e.g. 12000 (120), 23500 (235), 45000 (450)).
            //                if (metarCloud.Altitude / 100 > 0)
            //                {
            //                    addIndividualDigitsToATISSamples(Math.Floor(Convert.ToDouble(metarCloud.Altitude / 100)).ToString());

            //                    output += " " + Math.Floor(Convert.ToDouble(metarCloud.Altitude / 100)).ToString();

            //                    //If cloud altitude has a ten-thousand and hundred value (e.g. 10200 (102), 20800 (208), 40700 (407)).
            //                    if (metarCloud.Altitude.ToString().Substring(1, 1).Equals("0"))
            //                    {
            //                        applicationVariables.ATISSamples.Add("0");
            //                        applicationVariables.ATISSamples.Add("thousand");
            //                        output += " 0 THOUSAND";
            //                    }
            //                }

            //                //If cloud altitude has a thousand (e.g. 2000 (020), 4000 (040), 5000 (050)).
            //                if ((metarCloud.Altitude / 10) % 10 > 0)
            //                {
            //                    addIndividualDigitsToATISSamples(Math.Floor(Convert.ToDouble((metarCloud.Altitude / 10) % 10)).ToString());
            //                    applicationVariables.ATISSamples.Add("thousand");

            //                    output += " " + Math.Floor(Convert.ToDouble((metarCloud.Altitude / 10) % 10)) + " THOUSAND";
            //                }

            //                //If cloud altitude has a hundred (e.g. 200 (002), 400 (004), 500 (005)).
            //                if (metarCloud.Altitude % 10 > 0)
            //                {
            //                    addIndividualDigitsToATISSamples(Convert.ToString(metarCloud.Altitude % 10));
            //                    applicationVariables.ATISSamples.Add("hundred");

            //                    output += " " + metarCloud.Altitude % 10 + " HUNDRED";
            //                }
            //            }

            //            applicationVariables.ATISSamples.Add("ft");
            //            output += " FEET";

            //            //If cloud type has addition (e.g. CB, TCU).
            //            //TODO fix
            //            //if (metarCloud.Addition != null) output += cloudAddiationToFullSpelling(metarCloud.Addition);
            //        }
            //    }

            return string.Empty;
        }


        

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="cloudType"></param>
        ///// <returns></returns>
        //private string cloudTypeToFullSpelling(string cloudType)
        //{
        //    switch (cloudType)
        //    {
        //        case "FEW":
        //            applicationVariables.ATISSamples.Add("few");
        //            return " FEW";

        //        case "BKN":
        //            applicationVariables.ATISSamples.Add("bkn");
        //            return " BROKEN";

        //        case "OVC":
        //            applicationVariables.ATISSamples.Add("ovc");
        //            return " OVERCAST";

        //        case "SCT":
        //            applicationVariables.ATISSamples.Add("sct");
        //            return " SCATTERED";
        //    }

        //    return string.Empty;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="addition"></param>
        ///// <returns></returns>
        //private string cloudAddiationToFullSpelling(string addition)
        //{
        //    switch (addition)
        //    {
        //        case "CB":
        //            applicationVariables.ATISSamples.Add("cb");
        //            return " CUMULONIMBUS";

        //        case "TCU":
        //            applicationVariables.ATISSamples.Add("tcu");
        //            return " TOWERING CUMULONIMBUS";
        //    }

        //    return string.Empty;
        //}
    }
}
