using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// MetarProcessor class. Processes string METAR to usable fields in a METAR instance.
    /// </summary>
    class MetarProcessor
    {
        public Metar metar { get; private set; }

        /// <summary>
        /// Constructor a MetarProcessor for a METAR with out TEMPO or BECMG trend.
        /// </summary>
        /// <param name="inputMetar">METAR</param>
        public MetarProcessor(String inputMetar)
        {
            processMetar(inputMetar.Split(' '), MetarType.FULL);
        }

        /// <summary>
        /// Constructor a MetarProcessor for a METAR which contains TEMPO or BECMG trend.
        /// </summary>
        /// <param name="inputMetar">METAR.</param>
        /// <param name="inputTrend">Trend part of the METAR.</param>
        /// <param name="trendType">Indicates what MetarType trend type to process.</param>
        public MetarProcessor(String inputMetar, String inputTrend, MetarType trendType)
        {
            processMetar(inputMetar.Split(' '), MetarType.FULL);

            switch(trendType)
            {
                case MetarType.BECMG:
                    processMetar(inputTrend.Split(' '), MetarType.BECMG);
                    break;

                case MetarType.TEMPO:
                    processMetar(inputTrend.Split(' '), MetarType.TEMPO);
                    break;
            }
        }

        /// <summary>
        /// Constructor a MetarProcessor for a METAR which contains TEMPO and BECMG trends.
        /// </summary>
        /// <param name="inputMetar">METAR</param>
        /// <param name="inputTempo">Tempo part of the METAR.</param>
        /// <param name="inputBecmg">BECMG part of the METAR.</param>
        public MetarProcessor(String inputMetar, String inputTempo, String inputBecmg)
        {
            processMetar(inputMetar.Split(' '), MetarType.FULL);
            processMetar(inputTempo.Split(' '), MetarType.TEMPO);
            processMetar(inputBecmg.Split(' '), MetarType.BECMG);
        }

        /// <summary>
        /// Process string METAR to fields in a METAR instance.
        /// </summary>
        /// <param name="input">METAR.</param>
        /// <param name="metarType">Indicates what MetarType to process.</param>
        private void processMetar(String[] input, MetarType metarType)
        {
            switch(metarType)
            {
                #region FULL
                case MetarType.FULL:                   
                    metar = new Metar();

                    foreach (String s in input)
                    {
                        #region ICAO
                        if (s.All(char.IsLetter) && stringIsLength(4, s) && s.Equals(input[0]))
                        {
                            metar.ICAO = s;
                            continue;
                        }
                        #endregion

                        #region TIME
                        if (s.Last().Equals('Z') && s.Length > 6 && stringIsOnlyNumbers(s.Substring(0, 6)))
                        {
                            metar.Time = s; continue;
                        }
                        #endregion

                        #region WIND
                        if (s.StartsWith("VRB"))
                        {
                            metar.Wind = processCalmWind(s); continue;
                        }

                        if (s.EndsWith("KT"))
                        {
                            metar.Wind = processWind(s); continue;
                        }

                        if (hasVariableWind(s))
                        {
                            metar.Wind = processVariableWind(metar.Wind, s); continue;
                        }
                        #endregion

                        #region VISIBILITY
                        if (stringIsOnlyNumbers(s) && (stringIsLength(4, s) || stringIsLength(3, s)))
                        {
                            metar.Visibility = Convert.ToInt32(s); continue;
                        }

                        /*CAVOK*/
                        if (s.Equals("CAVOK"))
                        {
                            metar.CAVOK = true; continue;
                        }

                        /*Sky clear*/
                        if (s.StartsWith("SKC"))
                        {
                            metar.SKC = true; continue;
                        }

                        /*No significant weather*/
                        if (s.StartsWith("NSC") || s.StartsWith("NCD"))
                        {
                            metar.NSC = true; continue;
                        }

                        /*No significant weather*/
                        if (s.StartsWith("NSW"))
                        {
                            metar.NSW = true; continue;
                        }

                        /*Vertical visibility*/
                        if (s.StartsWith("VV") && stringIsOnlyNumbers(s.Substring(2)))
                        {
                            metar.VerticalVisibility = Convert.ToInt32(s.Substring(2));
                        }
                        #endregion

                        #region RVR
                        if (s.StartsWith("R") && Char.IsNumber(s.ElementAt(1)) && Char.IsNumber(s.ElementAt(2)) && s.Contains("/"))
                        {
                            metar.RVR = true;

                            String[] split = s.Split(new[] { "/" }, StringSplitOptions.None);

                            Regex rgx = new Regex("[^0-9 -]");

                            if(split[1].Contains('V')) metar.RVRValues.Add(split[0].Substring(1), Convert.ToInt32(rgx.Replace(split[1].Substring(0, split[1].IndexOf('V')), "")));
                            
                            else  metar.RVRValues.Add(split[0].Substring(1), Convert.ToInt32(rgx.Replace(split[1], "")));
                          
                            continue;
                        }
                        #endregion

                        #region PHENOMENA
                        if (s.StartsWith("-") || s.StartsWith("+") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                        {
                            if (s.StartsWith("-")) metar.Phenomena.Add(new MetarPhenomena(true, s.Substring(1)));
                            else if (s.StartsWith("+")) metar.Phenomena.Add(new MetarPhenomena(s.Substring(1)));
                            else metar.Phenomena.Add(new MetarPhenomena(s));

                            continue;
                        }
                        #endregion

                        #region CLOUDS
                        if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                        {
                            if (s.Substring(3).Count() > 3)
                            {
                                //If METAR doesn't contain /// auto addition in cloud phenomena.
                                if(!s.Substring(6).Contains("/"))
                                    metar.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
                                //If METAR does contain /// auto addition in cloud phenomena.
                                else
                                    metar.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3))));
                            }
                            else metar.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3))));

                            continue;
                        }
                        #endregion

                        #region TEMPERATURE
                        if (s.Contains("/") && (s.Length == 5 || (s.Length == 6 && s.Contains('M')) || (s.Length == 7 && s.Contains('M'))))
                        {
                            switch (s.Length)
                            {
                                case 5:
                                    metar.Temperature = s.Substring(0, 2);
                                    metar.Dewpoint = s.Substring(3);
                                    break;

                                case 6:
                                    if (s.Substring(0, 3).StartsWith("M"))
                                    {
                                        metar.Temperature = s.Substring(0, 3);
                                        metar.Dewpoint = s.Substring(4);
                                    }
                                    else
                                    {
                                        metar.Temperature = s.Substring(0, 2);
                                        metar.Dewpoint = s.Substring(3);
                                    }
                                    break;

                                case 7:
                                    metar.Temperature = s.Substring(0, 3);
                                    metar.Dewpoint = s.Substring(4);
                                    break;
                            }
                            continue;
                        }
                        #endregion

                        #region QNH
                        if (s.StartsWith("Q") && stringIsOnlyNumbers(s.Substring(1)))
                        {
                            metar.QNH = Convert.ToInt32(s.Substring(1));
                            continue;
                        }
                        #endregion
                                                
                        #region NOSIG
                        if (s.Equals("NOSIG"))
                        {
                            metar.NOSIG = true;
                            continue;
                        }
                        #endregion
                    }
                    break;
                #endregion

                #region BECMG
                case MetarType.BECMG:
                    metar.metarBECMG = new MetarBECMG();

                    foreach (String s in input)
                    {
                        #region WIND
                        if (s.StartsWith("VRB"))
                        {
                            metar.metarBECMG.Wind = processCalmWind(s); continue;
                        }

                        if (s.EndsWith("KT"))
                        {
                            metar.metarBECMG.Wind = processWind(s); continue;
                        }

                        if (hasVariableWind(s))
                        {
                            metar.metarBECMG.Wind = processVariableWind(metar.metarBECMG.Wind, s); continue;
                        }
                        #endregion

                        #region VISIBILITY
                        if (stringIsOnlyNumbers(s) && (stringIsLength(4, s) || stringIsLength(3, s)))
                        {
                            metar.metarBECMG.Visibility = Convert.ToInt32(s); continue;
                        }

                        /*CAVOK*/
                        if (s.Equals("CAVOK"))
                        {
                            metar.metarBECMG.CAVOK = true; continue;
                        }

                        /*Sky clear*/
                        if (s.StartsWith("SKC"))
                        {
                            metar.metarBECMG.SKC = true; continue;
                        }

                        /*No significant weather*/
                        if (s.StartsWith("NSW"))
                        {
                            metar.metarBECMG.NSW = true; continue;
                        }

                        /*Vertical visibility*/
                        if (s.StartsWith("VV") && stringIsOnlyNumbers(s.Substring(2)))
                        {
                            metar.metarBECMG.VerticalVisibility = Convert.ToInt32(s.Substring(2));
                        }
                        #endregion

                        #region PHENOMENA
                        if (s.StartsWith("-") || s.StartsWith("+") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                        {
                            if (s.StartsWith("-")) metar.metarBECMG.Phenomena.Add(new MetarPhenomena(true, s.Substring(1)));
                            else if (s.StartsWith("+")) metar.metarBECMG.Phenomena.Add(new MetarPhenomena(s.Substring(1)));
                            else metar.metarBECMG.Phenomena.Add(new MetarPhenomena(s));

                            continue;
                        }
                        #endregion

                        #region CLOUDS
                        if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                        {
                            if (s.Substring(3).Count() > 3)
                            {
                                //If METAR doesn't contain /// auto addition in cloud phenomena.
                                if (!s.Substring(6).Contains("/"))
                                    metar.metarBECMG.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
                                //If METAR does contain /// auto addition in cloud phenomena.
                                else
                                    metar.metarBECMG.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3))));
                            }
                            else metar.metarBECMG.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3))));

                            continue;
                        }
                        #endregion
                    }

                    break;
                #endregion

                #region TEMPO
                case MetarType.TEMPO:
                    metar.metarTEMPO = new MetarTEMPO();

                    foreach (String s in input)
                    {
                        #region WIND
                        if (s.StartsWith("VRB"))
                        {
                            metar.metarTEMPO.Wind = processCalmWind(s); continue;
                        }

                        if (s.EndsWith("KT"))
                        {
                            metar.metarTEMPO.Wind = processWind(s); continue;
                        }

                        if (hasVariableWind(s))
                        {
                            metar.metarTEMPO.Wind = processVariableWind(metar.metarTEMPO.Wind, s); continue;
                        }
                        #endregion

                        #region VISIBILITY
                        if (stringIsOnlyNumbers(s) && (stringIsLength(4, s) || stringIsLength(3, s)))
                        {
                            metar.metarTEMPO.Visibility = Convert.ToInt32(s); continue;
                        }

                        /*CAVOK*/
                        if (s.Equals("CAVOK"))
                        {
                            metar.metarTEMPO.CAVOK = true; continue;
                        }

                        /*Sky clear*/
                        if (s.StartsWith("SKC"))
                        {
                            metar.metarTEMPO.SKC = true; continue;
                        }

                        /*No significant weather*/
                        if (s.StartsWith("NSW"))
                        {
                            metar.metarTEMPO.NSW = true; continue;
                        }

                        /*Vertical visibility*/
                        if (s.StartsWith("VV") && stringIsOnlyNumbers(s.Substring(2)))
                        {
                            metar.metarTEMPO.VerticalVisibility = Convert.ToInt32(s.Substring(2));
                        }
                        #endregion

                        #region PHENOMENA
                        if (s.StartsWith("-") || s.StartsWith("+") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                        {
                            if (s.StartsWith("-")) metar.metarTEMPO.Phenomena.Add(new MetarPhenomena(true, s.Substring(1)));
                            else if (s.StartsWith("+")) metar.metarTEMPO.Phenomena.Add(new MetarPhenomena(s.Substring(1)));
                            else metar.metarTEMPO.Phenomena.Add(new MetarPhenomena(s));

                            continue;
                        }
                        #endregion

                        #region CLOUDS
                        if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                        {
                            if (s.Substring(3).Count() > 3)
                            {
                                //If METAR doesn't contain /// auto addition in cloud phenomena.
                                if (!s.Substring(6).Contains("/"))
                                    metar.metarTEMPO.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
                                //If METAR does contain /// auto addition in cloud phenomena.
                                else
                                    metar.metarTEMPO.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3))));
                            }
                            else metar.metarTEMPO.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3))));

                            continue;
                        }
                        #endregion
                    }
                    break;
                #endregion
            }
        }

        /// <summary>
        /// Process calm wind string to MetarWind.
        /// </summary>
        /// <param name="input">Wind to process.</param>
        /// <returns>New MetarWind which represents a calm wind.</returns>
        private MetarWind processCalmWind(String input)
        {
            return new MetarWind(true, input.Substring(3, 2));
        }

        /// <summary>
        /// Process wind string to MetarWind.
        /// </summary>
        /// <param name="input">Wind to process.</param>
        /// <returns>New MetarWind which represents the wind with a heading and strength.</returns>
        private MetarWind processWind(String input)
        {
            MetarWind metarWind = null;

            if (input.Contains("G")) metarWind = new MetarWind(input.Substring(0, 3), input.Substring(3, 2), input.Substring(6, 2));

            else if ((input.Substring(3, 1)).Equals("0")) metarWind = new MetarWind(input.Substring(0, 3), input.Substring(4, 1));

            else metarWind = new MetarWind(input.Substring(0, 3), input.Substring(3, 2));
            
            return metarWind;
        }

        /// <summary>
        /// Process variable wind string to MetarWind.
        /// </summary>
        /// <param name="metarWind">MetarWind to process.</param>
        /// <param name="input">Variable wind to process.</param>
        /// <returns>MetarWind with variable fields set.</returns>
        private MetarWind processVariableWind(MetarWind metarWind, String input)
        {
            metarWind.windVariableLeft = input.Substring(0, 3);
            metarWind.windVariableRight = input.Substring(4, 3);

            return metarWind;
        }

        /// <summary>
        /// Check if input wind string is a variable wind.
        /// </summary>
        /// <param name="input">Wind string.</param>
        /// <returns>Boolean indicating if the wind is variable.</returns>
        private bool hasVariableWind(String input)
        {
            if (input.Length > 5 && input.Substring(0, 3).All(char.IsDigit) && input.Contains('V') && input.Substring(4).All(char.IsDigit)) return true;

            return false;
        }

        /// <summary>
        /// Check if input string is numbers only.
        /// </summary>
        /// <param name="input">String to check.</param>
        /// <returns>Boolean indicating if the string is numbers only.</returns>
        private bool stringIsOnlyNumbers(String input)
        {
            if (input.All(char.IsDigit)) return true;

            return false;
        }

        /// <summary>
        /// Check if input string is a certain size.
        /// </summary>
        /// <param name="lenght">Size to check to.</param>
        /// <param name="input">String to check.</param>
        /// <returns>Boolean indicating if the string is the size inputted.</returns>
        private bool stringIsLength(int lenght, String input)
        {
            if (input.Length == lenght) return true;

            return false;
        }
    }
}
