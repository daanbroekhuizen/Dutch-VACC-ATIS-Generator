using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// DutchVACCATISGenerator class.
    /// </summary>
    public partial class DutchVACCATISGenerator : Form
    {
        private String metar { get; set; }

        private MetarProcessor metarProcessor { get; set; }

        private List<String> phoneticAlphabet { get; set; }

        private int atisIndex { get; set; }

        /// <summary>
        /// Constructor of DutchVACCATISGenerator.
        /// </summary>
        public DutchVACCATISGenerator()
        {
            InitializeComponent();

            metar = String.Empty;

            phoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            atisIndex = 25;

            atisLetterLabel.Text = phoneticAlphabet[0];

            //TransitionLevel tl = new TransitionLevel();
        }

        /// <summary>
        /// Method called when get metar button is clicked. Starts a background worker which pulls the metar from the VATSIM metar website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getMetarButton_Click(object sender, EventArgs e)
        {
            String _ICAO = icaoTextBox.Text;

            getMetarButton.Enabled = false;

            metarBackgroundWorker.RunWorkerAsync(_ICAO);
        }

        /// <summary>
        /// Method called when metar background workers is started. Pulls metar from VATSIM metar website.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void metarBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WebRequest request = WebRequest.Create("http://metar.vatsim.net/metar.php?id=" + e.Argument);
            WebResponse response = request.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());

            metar = reader.ReadToEnd();
            metar = metar.Remove(metar.Length - 2);
        }

        /// <summary>
        /// Method called when metar background worker has completed its task. Sets pulled metar from VATSIM metar website into the metarTextBox.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void metarBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metarTextBox.Text = metar;

            getMetarButton.Enabled = true;
        }

        /// <summary>
        /// Method called when process metar button is clicked. Initiates a MetarProcessor which will proces the metar inputted into easy accessible fields.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void processMetarButton_Click(object sender, EventArgs e)
        {
            if (metar.Equals(String.Empty) && metarTextBox.Text.Equals(String.Empty))
            {
                MessageBox.Show("No metar fetched or entered.", "Error"); return;
            }
            else metar = metarTextBox.Text;

            metarProcessor = new MetarProcessor(metar.Split(' '));

            if (metarProcessor.parseComplete)
            {
                try
                {
                    tlOutLabel.Text = calculateTransitionLevel().ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error parsing the METAR, check if METAR is in correct format", "Error"); return;
                }

                if (metar.Length > 64) lastLabel.Text = "Last successful processed metar:\n" + metar.Substring(0, 64) + "\n" + metar.Substring(64);
                else lastLabel.Text = "Last successful processed metar:\n" + metar;

                if (atisIndex == 25) atisIndex = 0;
                else atisIndex++;

                atisLetterLabel.Text = phoneticAlphabet[atisIndex];

                generateATISButton.Enabled = true;
            }
        }

        /// <summary>
        /// Method called when previous arrow (ATIS letter) is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void previousATISLetterButton_Click(object sender, EventArgs e)
        {
            if (atisIndex == 0) atisIndex = 25;
            else atisIndex--;

            atisLetterLabel.Text = phoneticAlphabet[atisIndex];
        }

        /// <summary>
        /// Method called when next arrow (ATIS letter) is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void nextATISLetterButton_Click(object sender, EventArgs e)
        {
            if (atisIndex == 25) atisIndex = 0;
            else atisIndex++;

            atisLetterLabel.Text = phoneticAlphabet[atisIndex];
        }

        /// <summary>
        /// Method called when main landing runway checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void mainLandingRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mainLandingRunwayCheckBox.Checked) mainLandingRunwayComboBox.Enabled = true;
            else mainLandingRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when secondary landing runway checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void secondaryLandingRunway_CheckedChanged(object sender, EventArgs e)
        {
            if (secondaryLandingRunwayCheckBox.Checked) secondaryLandingRunwayComboBox.Enabled = true;
            else secondaryLandingRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when main departure runway checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void mainDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mainDepartureRunwayCheckBox.Checked) mainDepartureRunwayComboBox.Enabled = true;
            else mainDepartureRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when secondary departure runway checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void secondaryDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (secondaryDepartureRunwayCheckBox.Checked) secondaryDepartureRunwayComboBox.Enabled = true;
            else secondaryDepartureRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method to parse runway identifier letter to ATIS output text.
        /// </summary>
        /// <param name="input">Runway identifier letter (L, C, R)</param>
        /// <returns>Runway identifier AITS output</returns>
        private String getRunwayMarker(String input)
        {
            switch (input)
            {
                case "L":
                    return "[left]";

                case "C":
                    return "[center]";

                case "R":
                    return "[right]";
            }

            return "";
        }

        /// <summary>
        /// Method to calculte transition level from QNH and temperature.
        /// </summary>
        /// <returns>Calculated TL</returns>
        private int calculateTransitionLevel()
        {
            int temp = 0;

            if (metarProcessor.metar.Temperature.StartsWith("M")) temp = Convert.ToInt32(metarProcessor.metar.Temperature.Substring(1)) * -1;

            else temp = Convert.ToInt32(metarProcessor.metar.Temperature);

            return (int)Math.Ceiling((307.8 - (0.13986 * temp) - (0.26224 * metarProcessor.metar.QNH)) / 5) * 5;
        }

        /// <summary>
        /// Method to generate configuration of runway.
        /// </summary>
        /// <param name="runway"></param>
        /// <param name="runwayComboBox"></param>
        /// <returns></returns>
        private String generateRunwayOutput(String runway, ComboBox runwayComboBox)
        {
            String output = runway;

            if (runwayComboBox.SelectedItem.ToString().Length > 2)
            {
                output += runwayComboBox.SelectedItem.ToString().Substring(0, 2);
                return output += getRunwayMarker(runwayComboBox.SelectedItem.ToString().Substring(2));
            }
            else return output += runwayComboBox.SelectedItem.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private String generateOperationalReportOutput()
        {
            if (mainLandingRunwayComboBox.Text.Equals("18R") && secondaryLandingRunwayComboBox.Text.Equals("18C")) return "[opr][independend]";

            else if (mainLandingRunwayComboBox.Text.Equals("36R") && secondaryLandingRunwayComboBox.Text.Equals("36C")) return "[opr][independend]";

            else if (mainLandingRunwayComboBox.Text.Equals("18R") && secondaryLandingRunwayComboBox.Text.Equals("27")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("27") && secondaryLandingRunwayComboBox.Text.Equals("18R")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("18C") && secondaryLandingRunwayComboBox.Text.Equals("27")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("27") && secondaryLandingRunwayComboBox.Text.Equals("18C")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("06") && secondaryLandingRunwayComboBox.Text.Equals("36R")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("36R") && secondaryLandingRunwayComboBox.Text.Equals("06")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("09") && secondaryLandingRunwayComboBox.Text.Equals("36R")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("36R") && secondaryLandingRunwayComboBox.Text.Equals("09")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("27") && secondaryLandingRunwayComboBox.Text.Equals("36R")) return "[opr][convapp]";

            else if (mainLandingRunwayComboBox.Text.Equals("36R") && secondaryLandingRunwayComboBox.Text.Equals("27")) return "[opr][convapp]";

            else return "";
        }
        
        /// <summary>
        /// Method called when approach only checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void appOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (arrOnlyCheckBox.Checked)
            {
                appArrOnlyCheckBox.Checked = true;
                appOnlyCheckBox.Checked = arrOnlyCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// Method called when arrival only checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void arrOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (appOnlyCheckBox.Checked)
            {
                appArrOnlyCheckBox.Checked = true;
                appOnlyCheckBox.Checked = arrOnlyCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// Method called when approach + arrival only checkbox check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void appArrOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (appArrOnlyCheckBox.Checked) appOnlyCheckBox.Enabled = arrOnlyCheckBox.Enabled = appOnlyCheckBox.Checked = arrOnlyCheckBox.Checked = false;
            else appOnlyCheckBox.Enabled = arrOnlyCheckBox.Enabled = true;
        }

        /// <summary>
        /// Method called when generate ATIS button is clicked. Processes field from MetarProcessor to output string.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void generateATISButton_Click(object sender, EventArgs e)
        {
            if (!mainLandingRunwayCheckBox.Checked || mainLandingRunwayComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No main landing runway selected.", "Error"); return;
            }

            if (!mainDepartureRunwayCheckBox.Checked || mainDepartureRunwayComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No main departure runway selected.", "Error"); return;
            }

            if (secondaryLandingRunwayCheckBox.Checked && secondaryLandingRunwayComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Secondary landing runway is checked but no runway is selected.", "Error"); return;
            }

            if (secondaryDepartureRunwayCheckBox.Checked && secondaryDepartureRunwayComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Secondary departure runway is checked but no runway is selected.", "Error"); return;
            }

            //generateATISButton.Enabled = false;      

            String output = String.Empty;

            //TODO REGIONAL AIRPORTS
            #region ICAO
            switch (metarProcessor.metar.ICAO)
            {
                case "EHAM":
                    output += "[ehamatis]";
                    break;

                case "EHRD":

                    break;
            }
            #endregion

            #region ATIS LETTER
            output += phoneticAlphabet[atisIndex];
            output += "[pause]";
            #endregion

            #region MAIN LANDING RUNWAY
            if (mainLandingRunwayCheckBox.Checked && !mainLandingRunwayComboBox.Text.Equals(mainDepartureRunwayComboBox.Text)) output += generateRunwayOutput("[mlrwy]", mainLandingRunwayComboBox);

            else output += generateRunwayOutput("[mrwy]", mainLandingRunwayComboBox);
            #endregion

            #region SECONDARY LANDING RUNWAY
            if (secondaryLandingRunwayCheckBox.Checked) output += generateRunwayOutput("[slrwy]", secondaryLandingRunwayComboBox);
            #endregion

            #region MAIN DEPARTURE RUNWAY
            if (mainDepartureRunwayCheckBox.Checked && !mainLandingRunwayComboBox.Text.Equals(mainDepartureRunwayComboBox.Text)) output += generateRunwayOutput("[mtrwy]", mainDepartureRunwayComboBox);
            #endregion

            #region SECONDARY DEPARTURE RUNWAY
            if (secondaryDepartureRunwayCheckBox.Checked) output += generateRunwayOutput("[strwy]", secondaryLandingRunwayComboBox);
            #endregion

            #region TL
            output += "[trl]";
            output += calculateTransitionLevel();
            #endregion

            #region OPERATIONAL REPORTS
            output += generateOperationalReportOutput();
            #endregion

            output += "[pause]";

            #region WIND
            if (metarProcessor.metar.Winds.First().Value.VRB) output += "[vrb]" + metarProcessor.metar.Winds.First().Value.windKnots + "[kt]";
            else if (metarProcessor.metar.Winds.First().Value.windGustMin != null) output += metarProcessor.metar.Winds.First().Value.windHeading + "[deg]" + metarProcessor.metar.Winds.First().Value.windGustMin + "[max]" + metarProcessor.metar.Winds.First().Value.windGustMax + "[kt]";
            else output += metarProcessor.metar.Winds.First().Value.windHeading + "[deg]" + metarProcessor.metar.Winds.First().Value.windKnots + "[kt]";

            /*Variable wind*/
            if (metarProcessor.metar.Winds.First().Value.windVariableLeft != null) output += "[vrbbtn]" + metarProcessor.metar.Winds.First().Value.windVariableLeft + "[and]" + metarProcessor.metar.Winds.First().Value.windVariableRight + "[deg]";
            #endregion

            #region CAVOK
            if (metarProcessor.metar.CAVOK) output += "[cavok]";
            #endregion

            #region VISIBILITY
            if (metarProcessor.metar.Visibility.Count > 0 && metarProcessor.metar.Visibility.First().Value > 0)
            {
                output += "[vis]";

                if (metarProcessor.metar.Visibility.First().Value < 1000) output += "1";
                else if (metarProcessor.metar.Visibility.First().Value >= 9999) output += "10";
                else output += Convert.ToString(metarProcessor.metar.Visibility.First().Value / 1000);

                output += "[km]";
            }
            #endregion

            //TODO 4 CHAR OPTIONS
            #region PHENOMENA
            if (metarProcessor.metar.Phenomena.Count > 0)
            {
                if (metarProcessor.metar.BECMG && metarProcessor.metar.Phenomena.Keys.ElementAt(0) < metarProcessor.metar.TrendBECMGPosition)
                {
                    int _index = metarProcessor.metar.Phenomena.Keys.ElementAt(0);

                    foreach (KeyValuePair<int, MetarPhenoma> pair in metarProcessor.metar.Phenomena)
                    {
                        if (Math.Abs(_index - pair.Key) == 0)
                        {
                            if (pair.Value.hasIntensity)
                            {
                                output += "[-]";

                                if (pair.Value.phenoma.Count() > 2) output += "[" + pair.Value.phenoma.Substring(0, 2).ToLower() + "][" + pair.Value.phenoma.Substring(2).ToLower() + "]";
                                else output += "[" + pair.Value.phenoma.ToLower() + "]";

                            }
                            else output += "[" + pair.Value.phenoma.ToLower() + "]";
                        }

                        _index++;
                    }
                }
                else if (metarProcessor.metar.TEMPO && metarProcessor.metar.Phenomena.Keys.ElementAt(0) < metarProcessor.metar.TrendTEMPOPosition)
                {
                    int _index = metarProcessor.metar.Phenomena.Keys.ElementAt(0);

                    foreach (KeyValuePair<int, MetarPhenoma> pair in metarProcessor.metar.Phenomena)
                    {
                        if (Math.Abs(_index - pair.Key) == 0)
                        {
                            if (pair.Value.hasIntensity)
                            {
                                output += "[-]";

                                if (pair.Value.phenoma.Count() > 2) output += "[" + pair.Value.phenoma.Substring(0, 2).ToLower() + "][" + pair.Value.phenoma.Substring(2).ToLower() + "]";
                                else output += "[" + pair.Value.phenoma.ToLower() + "]";

                            }
                            else output += "[" + pair.Value.phenoma.ToLower() + "]";
                        }

                        _index++;
                    }
                }
            }
            #endregion

            #region CLOUDS OPTIONS
            if (metarProcessor.metar.SKC) output += "[skc]";
            if (metarProcessor.metar.NSC) output += "[nsc]";
            #endregion

            #region CLOUDS
            if (metarProcessor.metar.Clouds.Count > 0)
            {
                if (metarProcessor.metar.BECMG && metarProcessor.metar.Clouds.Keys.ElementAt(0) < metarProcessor.metar.TrendBECMGPosition)
                {
                    int _index = metarProcessor.metar.Clouds.Keys.ElementAt(0);

                    foreach (KeyValuePair<int, MetarCloud> pair in metarProcessor.metar.Clouds)
                    {
                        if (Math.Abs(_index - pair.Key) == 0)
                        {
                            output += "[" + pair.Value.cloudType.ToLower() + "]";

                            if (pair.Value.altitude / 100 > 0) output += "1" + pair.Value.altitude / 10 + "[thousand]";

                            if (pair.Value.altitude / 10 > 0) output += pair.Value.altitude / 10 + "[thousand]";

                            if (pair.Value.altitude % 10 > 0) output += pair.Value.altitude % 10 + "[hundred]";

                            output += "[ft]";

                            if (pair.Value.addition != null) output += "[" + pair.Value.addition.ToLower() + "]";
                        }

                        _index++;
                    }
                }
                else if (metarProcessor.metar.TEMPO && metarProcessor.metar.Clouds.Keys.ElementAt(0) < metarProcessor.metar.TrendTEMPOPosition)
                {
                    int _index = metarProcessor.metar.Clouds.Keys.ElementAt(0);

                    foreach (KeyValuePair<int, MetarCloud> pair in metarProcessor.metar.Clouds)
                    {
                        if (Math.Abs(_index - pair.Key) == 0)
                        {
                            output += "[" + pair.Value.cloudType.ToLower() + "]";

                            if (pair.Value.altitude / 100 > 0) output += "1" + pair.Value.altitude / 10 + "[thousand]";

                            if (pair.Value.altitude / 10 > 0) output += pair.Value.altitude / 10 + "[thousand]";

                            if (pair.Value.altitude % 10 > 0) output += pair.Value.altitude % 10 + "[hundred]";

                            output += "[ft]";

                            if (pair.Value.addition != null) output += "[" + pair.Value.addition.ToLower() + "]";
                        }

                        _index++;
                    }
                }
            #endregion

                //TODO VV < 1000?
                #region Vertical visibility
                if (metarProcessor.metar.VerticalVisibility != null)
                {
                    output += "[vv]";

                    int visibility = Convert.ToInt32(metarProcessor.metar.VerticalVisibility.Substring(2));

                    if (visibility / 100 > 0)
                    {
                        output += "1" + metarProcessor.metar.VerticalVisibility.Substring(2, 1) + "[thousand]";
                    }


                    //+ Convert.ToInt32(metarProcessor.metar.verticalVisibility.Substring(3))) + 
                }
                #endregion

                #region TEMPERATURE
                output += "[temp]";
                if (metarProcessor.metar.Temperature.StartsWith("M")) output += "[minus]" + Convert.ToInt32(metarProcessor.metar.Temperature.ToString().Substring(1, 2));
                else output += Convert.ToInt32(metarProcessor.metar.Temperature.ToString());
                #endregion

                #region DEWPOINT
                output += "[dp]";
                if (metarProcessor.metar.Dewpoint.StartsWith("M")) output += "[minus]" + Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString().Substring(1, 2));
                else output += Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString());
                #endregion

                #region QNH
                output += "[qnh]";
                output += metarProcessor.metar.QNH.ToString();
                output += "[hpa]";
                #endregion

                #region NOSIG
                if (metarProcessor.metar.NOSIG) output += "[nosig]";
                #endregion

                //TODO FINISH TEMPO AND BECMG
                //TODO MAKE METHODS FOR OUTPUT 4x SAME CODE NOW
                #region TEMPO || BECMG
                if (metarProcessor.metar.TEMPO || metarProcessor.metar.BECMG)
                {
                    if (metarProcessor.metar.TEMPO && metarProcessor.metar.BECMG)
                    {
                        #region BECMG -> TEMPO
                        output += "[becmg]";

                        int _index = metarProcessor.metar.TrendBECMGPosition;
                        int addToIndex = 1;

                        #region BECMG WIND
                        foreach (KeyValuePair<int, MetarWind> metarWind in metarProcessor.metar.Winds)
                        {
                            if (metarWind.Key == _index)
                            {
                                _index = metarWind.Key;

                                if (metarWind.Value.VRB) output += "[vrb]" + metarWind.Value.windKnots + "[kt]";
                                else if (metarWind.Value.windGustMin != null) output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windGustMin + "[max]" + metarWind.Value.windGustMax + "[kt]";
                                else output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windKnots + "[kt]";

                                /*Variable wind*/
                                if (metarWind.Value.windVariableLeft != null) output += "[vrbbtn]" + metarWind.Value.windVariableLeft + "[and]" + metarWind.Value.windVariableRight + "[deg]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region BECMG VISIBILITY
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, int> metarVisibility in metarProcessor.metar.Visibility)
                        {
                            if (Math.Abs(metarVisibility.Key - _index) <= 1)
                            {
                                output += "[vis]";

                                if (metarVisibility.Value < 1000) output += "1";
                                else if (metarVisibility.Value >= 9999) output += "10";
                                else output += Convert.ToString(metarVisibility.Value / 1000);

                                output += "[km]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region BECMG PHENOMENA
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarPhenoma> metarPhenoma in metarProcessor.metar.Phenomena)
                        {
                            if (Math.Abs(metarPhenoma.Key - _index) <= 1)
                            {
                                if (metarPhenoma.Value.hasIntensity)
                                {
                                    output += "[-]";

                                    if (metarPhenoma.Value.phenoma.Count() > 2) output += "[" + metarPhenoma.Value.phenoma.Substring(0, 2).ToLower() + "][" + metarPhenoma.Value.phenoma.Substring(2).ToLower() + "]";
                                    else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                }
                                else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region BECMG CLOUDS
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarCloud> cloudPair in metarProcessor.metar.Clouds)
                        {
                            if (Math.Abs(_index - cloudPair.Key) <= 1)
                            {
                                output += "[" + cloudPair.Value.cloudType.ToLower() + "]";

                                if (cloudPair.Value.altitude / 100 > 0) output += "1" + cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude / 10 > 0) output += cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude % 10 > 0) output += cloudPair.Value.altitude % 10 + "[hundred]";

                                output += "[ft]";

                                if (cloudPair.Value.addition != null) output += "[" + cloudPair.Value.addition.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        output += "[tempo]";

                        _index = metarProcessor.metar.TrendTEMPOPosition;
                        addToIndex = 1;

                        #region TEMPO WIND
                        foreach (KeyValuePair<int, MetarWind> metarWind in metarProcessor.metar.Winds)
                        {
                            if (metarWind.Key == _index)
                            {
                                _index = metarWind.Key;

                                if (metarWind.Value.VRB) output += "[vrb]" + metarWind.Value.windKnots + "[kt]";
                                else if (metarWind.Value.windGustMin != null) output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windGustMin + "[max]" + metarWind.Value.windGustMax + "[kt]";
                                else output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windKnots + "[kt]";

                                /*Variable wind*/
                                if (metarWind.Value.windVariableLeft != null) output += "[vrbbtn]" + metarWind.Value.windVariableLeft + "[and]" + metarWind.Value.windVariableRight + "[deg]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region TEMPO VISIBILITY
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, int> metarVisibility in metarProcessor.metar.Visibility)
                        {
                            if (Math.Abs(metarVisibility.Key - _index) <= 1)
                            {
                                output += "[vis]";

                                if (metarVisibility.Value < 1000) output += "1";
                                else if (metarVisibility.Value >= 9999) output += "10";
                                else output += Convert.ToString(metarVisibility.Value / 1000);

                                output += "[km]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region TEMPO PHENOMENA
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarPhenoma> metarPhenoma in metarProcessor.metar.Phenomena)
                        {
                            if (Math.Abs(metarPhenoma.Key - _index) <= 1)
                            {
                                if (metarPhenoma.Value.hasIntensity)
                                {
                                    output += "[-]";

                                    if (metarPhenoma.Value.phenoma.Count() > 2) output += "[" + metarPhenoma.Value.phenoma.Substring(0, 2).ToLower() + "][" + metarPhenoma.Value.phenoma.Substring(2).ToLower() + "]";
                                    else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                }
                                else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region TEMPO CLOUDS
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarCloud> cloudPair in metarProcessor.metar.Clouds)
                        {
                            if (Math.Abs(_index - cloudPair.Key) <= 1)
                            {
                                output += "[" + cloudPair.Value.cloudType.ToLower() + "]";

                                if (cloudPair.Value.altitude / 100 > 0) output += "1" + cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude / 10 > 0) output += cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude % 10 > 0) output += cloudPair.Value.altitude % 10 + "[hundred]";

                                output += "[ft]";

                                if (cloudPair.Value.addition != null) output += "[" + cloudPair.Value.addition.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion
                        #endregion
                    }

                    #region TEMPO
                    else if (metarProcessor.metar.TEMPO)
                    {
                        output += "[tempo]";

                        int _index = metarProcessor.metar.TrendTEMPOPosition;
                        int addToIndex = 0;

                        #region TEMPO WIND
                        foreach (KeyValuePair<int, MetarWind> metarWind in metarProcessor.metar.Winds)
                        {
                            if (metarWind.Key == _index)
                            {
                                _index = metarWind.Key;

                                if (metarWind.Value.VRB) output += "[vrb]" + metarWind.Value.windKnots + "[kt]";
                                else if (metarWind.Value.windGustMin != null) output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windGustMin + "[max]" + metarWind.Value.windGustMax + "[kt]";
                                else output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windKnots + "[kt]";

                                /*Variable wind*/
                                if (metarWind.Value.windVariableLeft != null) output += "[vrbbtn]" + metarWind.Value.windVariableLeft + "[and]" + metarWind.Value.windVariableRight + "[deg]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region TEMPO VISIBILITY
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, int> metarVisibility in metarProcessor.metar.Visibility)
                        {
                            if (Math.Abs(metarVisibility.Key - _index) <= 1)
                            {
                                output += "[vis]";

                                if (metarVisibility.Value < 1000) output += "1";
                                else if (metarVisibility.Value >= 9999) output += "10";
                                else output += Convert.ToString(metarVisibility.Value / 1000);

                                output += "[km]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region TEMPO PHENOMENA
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarPhenoma> metarPhenoma in metarProcessor.metar.Phenomena)
                        {
                            if (Math.Abs(metarPhenoma.Key - _index) <= 1)
                            {
                                if (metarPhenoma.Value.hasIntensity)
                                {
                                    output += "[-]";

                                    if (metarPhenoma.Value.phenoma.Count() > 2) output += "[" + metarPhenoma.Value.phenoma.Substring(0, 2).ToLower() + "][" + metarPhenoma.Value.phenoma.Substring(2).ToLower() + "]";
                                    else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                }
                                else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region TEMPO CLOUDS
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarCloud> cloudPair in metarProcessor.metar.Clouds)
                        {
                            if (Math.Abs(_index - cloudPair.Key) <= 1)
                            {
                                output += "[" + cloudPair.Value.cloudType.ToLower() + "]";

                                if (cloudPair.Value.altitude / 100 > 0) output += "1" + cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude / 10 > 0) output += cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude % 10 > 0) output += cloudPair.Value.altitude % 10 + "[hundred]";

                                output += "[ft]";

                                if (cloudPair.Value.addition != null) output += "[" + cloudPair.Value.addition.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                    }
                    #endregion

                    #region BECMG
                    else
                    {
                        output += "[becmg]";

                        int _index = metarProcessor.metar.TrendBECMGPosition;
                        int addToIndex = 0;

                        #region BECMG WIND
                        foreach (KeyValuePair<int, MetarWind> metarWind in metarProcessor.metar.Winds)
                        {
                            if (metarWind.Key == _index)
                            {
                                _index = metarWind.Key;

                                if (metarWind.Value.VRB) output += "[vrb]" + metarWind.Value.windKnots + "[kt]";
                                else if (metarWind.Value.windGustMin != null) output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windGustMin + "[max]" + metarWind.Value.windGustMax + "[kt]";
                                else output += metarWind.Value.windHeading + "[deg]" + metarWind.Value.windKnots + "[kt]";

                                /*Variable wind*/
                                if (metarWind.Value.windVariableLeft != null) output += "[vrbbtn]" + metarWind.Value.windVariableLeft + "[and]" + metarWind.Value.windVariableRight + "[deg]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region BECMG VISIBILITY
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, int> metarVisibility in metarProcessor.metar.Visibility)
                        {
                            if (Math.Abs(metarVisibility.Key - _index) <= 1)
                            {
                                output += "[vis]";

                                if (metarVisibility.Value < 1000) output += "1";
                                else if (metarVisibility.Value >= 9999) output += "10";
                                else output += Convert.ToString(metarVisibility.Value / 1000);

                                output += "[km]";
                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region BECMG PHENOMENA
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarPhenoma> metarPhenoma in metarProcessor.metar.Phenomena)
                        {
                            if (Math.Abs(metarPhenoma.Key - _index) <= 1)
                            {
                                if (metarPhenoma.Value.hasIntensity)
                                {
                                    output += "[-]";

                                    if (metarPhenoma.Value.phenoma.Count() > 2) output += "[" + metarPhenoma.Value.phenoma.Substring(0, 2).ToLower() + "][" + metarPhenoma.Value.phenoma.Substring(2).ToLower() + "]";
                                    else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                }
                                else output += "[" + metarPhenoma.Value.phenoma.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion

                        #region BECMG CLOUDS
                        /* Reset index */
                        _index = metarProcessor.metar.TrendTEMPOPosition + addToIndex;

                        foreach (KeyValuePair<int, MetarCloud> cloudPair in metarProcessor.metar.Clouds)
                        {
                            if (Math.Abs(_index - cloudPair.Key) <= 1)
                            {
                                output += "[" + cloudPair.Value.cloudType.ToLower() + "]";

                                if (cloudPair.Value.altitude / 100 > 0) output += "1" + cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude / 10 > 0) output += cloudPair.Value.altitude / 10 + "[thousand]";

                                if (cloudPair.Value.altitude % 10 > 0) output += cloudPair.Value.altitude % 10 + "[hundred]";

                                output += "[ft]";

                                if (cloudPair.Value.addition != null) output += "[" + cloudPair.Value.addition.ToLower() + "]";

                                addToIndex++;
                            }

                            _index++;
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                #region OPTIONAL
                if (markTempCheckBox.Checked) output += "[marktemp]";
                if (arrOnlyCheckBox.Checked) output += "[call1]";
                if (appOnlyCheckBox.Checked) output += "[call2]";
                if (appArrOnlyCheckBox.Checked) output += "[call3]";
                #endregion

                #region END
                output += "[end]";
                output += phoneticAlphabet[atisIndex];
                #endregion

                if (copyOutputCheckBox.Checked) Clipboard.SetText(output);

                outputTextBox.Text = output;
            }
        }
    }
}
