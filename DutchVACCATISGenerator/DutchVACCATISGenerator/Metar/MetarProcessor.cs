using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// 
    /// </summary>
    class MetarProcessor
    {
        public Metar metar { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMetar"></param>
        public MetarProcessor(String inputMetar)
        {
            processMetar(inputMetar.Split(' '), MetarType.FULL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMetar"></param>
        /// <param name="inputTrend"></param>
        /// <param name="trendType"></param>
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
        /// 
        /// </summary>
        /// <param name="inputMetar"></param>
        /// <param name="inputBecmg"></param>
        /// <param name="inputTempo"></param>
        public MetarProcessor(String inputMetar, String inputTrendPartOne, String inputTrendPartTwo, Boolean order)
        {
            /*
             * Order:
             *  - false > BECMG - TEMPO
             *  - true > TEMPO - BECMG
             */
            processMetar(inputMetar.Split(' '), MetarType.FULL);

            if(!order)
            {
                processMetar(inputTrendPartOne.Split(' '), MetarType.BECMG);
                processMetar(inputTrendPartTwo.Split(' '), MetarType.TEMPO);
            }
            else
            {
                processMetar(inputTrendPartOne.Split(' '), MetarType.TEMPO);
                processMetar(inputTrendPartTwo.Split(' '), MetarType.BECMG);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="metarType"></param>
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
                        if (stringIsOnlyLetters(s) && stringIsLength(4, s) && s.Equals(input[0]))
                        {
                            metar.ICAO = s;
                            continue;
                        }
                        #endregion

                        #region TIME
                        if (stringEndsWithChar('Z', s))
                        {
                            metar.Time = s;
                            continue;
                        }
                        #endregion

                        #region WIND
                        if (s.StartsWith("VRB"))
                        {
                            metar.Wind = processCalmWind(s); continue;
                        }

                        if (stringEndsWith("KT", s))
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
                        if (s.StartsWith("NSC"))
                        {
                            metar.NSC = true; continue;
                        }

                        /*No significant weather*/
                        if (s.StartsWith("NSW"))
                        {
                            metar.NSW = true; continue;
                        }

                        /*Vertical visibility*/
                        if (s.StartsWith("VV"))
                        {
                            metar.VerticalVisibility = s; continue;
                        }
                        #endregion

                        #region PHENOMENA
                        if (s.StartsWith("-") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                        {
                            if (s.StartsWith("-")) metar.Phenomena.Add(new MetarPhenoma(true, s.Substring(1)));
                            else metar.Phenomena.Add(new MetarPhenoma(s));

                            continue;
                        }
                        #endregion

                        #region CLOUDS
                        if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                        {
                            if (s.Substring(3).Count() > 3) metar.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
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

                        if (stringEndsWith("KT", s))
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
                        if (s.StartsWith("VV"))
                        {
                            metar.metarBECMG.VerticalVisibility = s; continue;
                        }
                        #endregion

                        #region PHENOMENA
                        if (s.StartsWith("-") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                        {
                            if (s.StartsWith("-")) metar.metarBECMG.Phenomena.Add(new MetarPhenoma(true, s.Substring(1)));
                            else metar.metarBECMG.Phenomena.Add(new MetarPhenoma(s));

                            continue;
                        }
                        #endregion

                        #region CLOUDS
                        if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                        {
                            if (s.Substring(3).Count() > 3) metar.metarBECMG.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
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

                        if (stringEndsWith("KT", s))
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
                        if (s.StartsWith("VV"))
                        {
                            metar.metarTEMPO.VerticalVisibility = s; continue;
                        }
                        #endregion

                        #region PHENOMENA
                        if (s.StartsWith("-") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                        {
                            if (s.StartsWith("-")) metar.metarTEMPO.Phenomena.Add(new MetarPhenoma(true, s.Substring(1)));
                            else metar.metarTEMPO.Phenomena.Add(new MetarPhenoma(s));

                            continue;
                        }
                        #endregion

                        #region CLOUDS
                        if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                        {
                            if (s.Substring(3).Count() > 3) metar.metarTEMPO.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
                            else metar.metarTEMPO.Clouds.Add(new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3))));

                            continue;
                        }
                        #endregion
                    }
                    break;
                #endregion
            }
        }

        private MetarWind processCalmWind(String input)
        {
            return new MetarWind(true, input.Substring(3, 2));
        }

        private MetarWind processWind(String input)
        {
            MetarWind metarWind = null;

            if (input.Contains("G")) metarWind = new MetarWind(input.Substring(0, 3), input.Substring(3, 2), input.Substring(6, 2));

            else if ((input.Substring(3, 1)).Equals("0")) metarWind = new MetarWind(input.Substring(0, 3), input.Substring(4, 1));

            else metarWind = new MetarWind(input.Substring(0, 3), input.Substring(3, 2));
            
            return metarWind;
        }

        private MetarWind processVariableWind(MetarWind metarWind, String input)
        {
            metarWind.windVariableLeft = input.Substring(0, 3);
            metarWind.windVariableRight = input.Substring(4, 3);

            return metarWind;
        }

        private bool hasVariableWind(String input)
        {
            if (input.Length > 5 && input.Substring(0, 3).All(char.IsDigit) && input.Contains('V') && input.Substring(4).All(char.IsDigit)) return true;

            return false;
        }

        private bool stringIsOnlyLetters(String input)
        {
            if (input.All(char.IsLetter)) return true;
 
            return false;
        }

        private bool stringIsOnlyNumbers(String input)
        {
            if (input.All(char.IsDigit)) return true;

            return false;
        }

        private bool stringIsLength(int lenght, String input)
        {
            if (input.Length == lenght) return true;

            return false;
        }

        private bool stringEndsWithChar(char c, String input)
        {
            if (input.Length > 0)
            {
                if (input.Last().Equals(c)) return true;
                else return false;
            }
            else return false;         
        }

        private bool stringEndsWith(String c, String input)
        {
            if (input.EndsWith(c)) return true;
   
            return false;
        }
    }
}
