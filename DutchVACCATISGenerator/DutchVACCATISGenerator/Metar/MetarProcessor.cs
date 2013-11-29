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
        public Boolean parseComplete { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMetar"></param>
        public MetarProcessor(String[] inputMetar)
        {
            metar = new Metar();

            parseComplete = false;

            processMetar(inputMetar);
        }

        /// <summary>
        /// 
        /// </summary>
        private void processMetar(String[] inputMetar)
        {
            try
            {
                int _index = -1;

                foreach (String s in inputMetar)
                {
                    _index++;

                    #region ICAO
                    if (stringIsOnlyLetters(s) && stringIsLength(4, s) && s.Equals(inputMetar[0]))
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
                        metar.winds.Add(_index, new MetarWind(true, s.Substring(3, 2))); continue;
                    }

                    if (stringEndsWith("KT", s))
                    {
                        if (s.Contains("G")) metar.winds.Add(_index, new MetarWind(s.Substring(0, 3), s.Substring(3, 2), s.Substring(6, 2)));
                        else metar.winds.Add(_index, new MetarWind(s.Substring(0, 3), s.Substring(3, 2)));

                        continue;
                    }

                    if (hasVariableWind(s))
                    {
                        metar.winds[_index - 1].windVariableLeft = s.Substring(0, 3);
                        metar.winds[_index - 1].windVariableRight = s.Substring(4, 3);
                        continue;
                    }
                    #endregion
         
                    #region VISIBILITY
                    if (stringIsOnlyNumbers(s) && (stringIsLength(4, s) || stringIsLength(3, s)))
                    {
                        metar.Visibility.Add(_index, Convert.ToInt32(s)); continue;
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
                    if(s.StartsWith("VV"))
                    {
                        metar.verticalVisibility = s; continue;
                    }
                    #endregion

                    #region PHENOMENA
                    if (s.StartsWith("-") || s.StartsWith("VC") || s.StartsWith("MI") || s.StartsWith("PR") || s.StartsWith("BC") || s.StartsWith("DR") || s.StartsWith("BL") || s.StartsWith("SH") || s.StartsWith("TS") || s.StartsWith("FZ") || s.StartsWith("DZ") || s.StartsWith("RA") || s.StartsWith("SN") || s.StartsWith("SG") || s.StartsWith("IC") || s.StartsWith("PL") || s.StartsWith("GR") || s.StartsWith("BR") || s.StartsWith("FG") || s.StartsWith("FU") || s.StartsWith("HZ"))
                    {
                        if (s.StartsWith("-")) metar.phenomena.Add(_index, new MetarPhenoma(true, s.Substring(1)));
                        else metar.phenomena.Add(_index, new MetarPhenoma(s));

                        continue;
                    }
                    #endregion

                    #region CLOUDS
                    if (s.StartsWith("FEW") || s.StartsWith("SCT") || s.StartsWith("BKN") || s.StartsWith("OVC"))
                    {
                        if (s.Substring(3).Count() > 3) metar.clouds.Add(_index, new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3, 3)), s.Substring(6)));
                        else metar.clouds.Add(_index, new MetarCloud(s.Substring(0, 3), Convert.ToInt32(s.Substring(3))));       
                        
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

                    #region TRENDS
                    #region NOSIG
                    if (s.Equals("NOSIG"))
                    {
                        metar.NOSIG = true;
                        continue;
                    }
                    #endregion
    
                    #region TEMPO
                    if (s.Equals("TEMPO"))
                    {
                        metar.TEMPO = true;
                        metar.trendTEMPOPosition = _index;
                        continue;
                    }
                    #endregion

                    #region BECMG
                    if (s.Equals("BECMG"))
                    {
                        metar.BECMG = true;
                        metar.trendBECMGPosition = _index;
                        continue;
                    }
                    #endregion
                    #endregion
                }
                
                parseComplete = true;
            }
            catch(Exception)
            {
                MessageBox.Show("Error parsing the METAR, check if METAR is in correct format", "Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool hasVariableWind(String input)
        {
            if (input.Length > 5 && input.Substring(0, 3).All(char.IsDigit) && input.Contains('V') && input.Substring(4).All(char.IsDigit)) return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool stringIsOnlyLetters(String input)
        {
            if (input.All(char.IsLetter)) return true;
 
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool stringIsOnlyNumbers(String input)
        {
            if (input.All(char.IsDigit)) return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lenght"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool stringIsLength(int lenght, String input)
        {
            if (input.Length == lenght) return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool stringEndsWithChar(char c, String input)
        {
            if (input.Last().Equals(c)) return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool stringEndsWith(String c, String input)
        {
            if (input.EndsWith(c)) return true;
   
            return false;
        }
    }
}
