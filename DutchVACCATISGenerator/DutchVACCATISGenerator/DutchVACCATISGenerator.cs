using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
        }

        /// <summary>
        /// Method called when get metar button is clicked. Starts a background worker which pulls the metar from the VATSIM metar website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getMetarButton_Click(object sender, EventArgs e)
        {
            String _ICAO = icaoTextBox.Text;

            if(_ICAO == String.Empty)
            {
                MessageBox.Show("Please enter an ICAO code.", "Error"); return;
            }

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

            if (metar.StartsWith(e.Argument.ToString())) metar = metar.Remove(metar.Length - 2);
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

        private String[] splitMetar(String metar, String splitWord)
        {
            Regex regex = null;

            switch (splitWord)
            {
                case "BECMG":
                    regex = new Regex(@"\bBECMG\b");
                    break;

                case "TEMPO":
                    regex = new Regex(@"\bTEMPO\b");
                    break;
            }

            return regex.Split(metar);
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
            else metar = metarTextBox.Text.Trim();

            if (metar.Contains("BECMG") && metar.Contains("TEMPO"))
            {
                if (metar.IndexOf("BECMG") < metar.IndexOf("TEMPO")) metarProcessor = new MetarProcessor(splitMetar(metar, "BECMG")[0].Trim(), splitMetar(metar, "TEMPO")[1].Trim(), splitMetar(splitMetar(metar, "BECMG")[1].Trim(), "TEMPO")[0].Trim());

                else metarProcessor = new MetarProcessor(splitMetar(metar, "TEMPO")[0].Trim(), splitMetar(splitMetar(metar, "TEMPO")[1].Trim(), "BECMG")[0].Trim(), splitMetar(metar, "BECMG")[1].Trim());
            }

            else if (metar.Contains("BECMG")) metarProcessor = new MetarProcessor(splitMetar(metar, "BECMG")[0].Trim(), splitMetar(metar, "BECMG")[1].Trim(), MetarType.BECMG);

            else if (metar.Contains("TEMPO")) metarProcessor = new MetarProcessor(splitMetar(metar, "TEMPO")[0].Trim(), splitMetar(metar, "TEMPO")[1].Trim(), MetarType.TEMPO);

            else metarProcessor = new MetarProcessor(metar);

            try
            {
                tlOutLabel.Text = calculateTransitionLevel().ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error parsing the METAR, check if METAR is in correct format", "Error"); return;
            }

            outputTextBox.Clear();

            if (metar.Substring(68).Count() > 68) lastLabel.Text = "Last successful processed metar:\n" + metar.Substring(0, 68) + "\n" + metar.Substring(68, 68) + "...";
            else if (metar.Length > 68) lastLabel.Text = "Last successful processed metar:\n" + metar.Substring(0, 68) + "\n" + metar.Substring(68);
            else lastLabel.Text = "Last successful processed metar:\n" + metar;

            if (atisIndex == 25) atisIndex = 0;
            else atisIndex++;

            atisLetterLabel.Text = phoneticAlphabet[atisIndex];

            generateATISButton.Enabled = true;
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
        /// Parse runway identifier letter to ATIS output text.
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
        /// Calculate transition level from QNH and temperature.
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
        /// Generate configuration of runway.
        /// </summary>
        /// <param name="runway"></param>
        /// <param name="runwayComboBox"></param>
        /// <returns></returns>
        private String runwayToOutput(String runway, ComboBox runwayComboBox)
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
        /// Generate operational report.
        /// </summary>
        /// <returns>String output</returns>
        private String operationalReportToOutput()
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
        /// Generte output from List<T>
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="input">List<T></param>
        /// <returns>String output</returns>
        private String listToOutput<T>(List<T> input)
        {
            String output = String.Empty;

            if(input is List<MetarPhenoma>)
            {
                foreach (MetarPhenoma metarPhenoma in input as List<MetarPhenoma>)
                {
                    if (metarPhenoma.hasIntensity)
                    {
                        output += "[-]";

                        if (metarPhenoma.phenoma.Count() > 2) output += "[" + metarPhenoma.phenoma.Substring(0, 2).ToLower() + "][" + metarPhenoma.phenoma.Substring(2).ToLower() + "]";
                        else output += "[" + metarPhenoma.phenoma.ToLower() + "]";
                    }
                    else output += "[" + metarPhenoma.phenoma.ToLower() + "]";
                }
            }

            else if (input is List<MetarCloud>)
            {
                foreach (MetarCloud metarCloud in input as List<MetarCloud>)
                {
                    output += "[" + metarCloud.cloudType.ToLower() + "]";

                    if (metarCloud.altitude / 100 > 0) output += "1" + metarCloud.altitude / 10 + "[thousand]";

                    if (metarCloud.altitude / 10 > 0) output += metarCloud.altitude / 10 + "[thousand]";

                    if (metarCloud.altitude % 10 > 0) output += metarCloud.altitude % 10 + "[hundred]";

                    output += "[ft]";

                    if (metarCloud.addition != null) output += "[" + metarCloud.addition.ToLower() + "]";
                }
            }

            return output;
        }

        /// <summary>
        /// Generate visibility output.
        /// </summary>
        /// <param name="input">Integer</param>
        /// <returns>String output</returns>
        private String visibilityToOutput(int input)
        {
            String output = "[vis]";

            if (input < 1000) output += "1";
            else if (input >= 9999) output += "10";
            else output += Convert.ToString(input / 1000);

            return output += "[km]";
        }

        /// <summary>
        /// Generate wind output.
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>String output</returns>
        private String windToOutput(MetarWind input)
        {
            String output = String.Empty;

            if (input.VRB) output += "[vrb]" + input.windKnots + "[kt]";
            else if (input.windGustMin != null) output += input.windHeading + "[deg]" + input.windGustMin + "[max]" + input.windGustMax + "[kt]";
            else output += input.windHeading + "[deg]" + input.windKnots + "[kt]";

            /*Variable wind*/
            if (input.windVariableLeft != null) output += "[vrbbtn]" + input.windVariableLeft + "[and]" + input.windVariableRight + "[deg]";

            return output;
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
            if (mainLandingRunwayCheckBox.Checked && !mainLandingRunwayComboBox.Text.Equals(mainDepartureRunwayComboBox.Text)) output += runwayToOutput("[mlrwy]", mainLandingRunwayComboBox);

            else output += runwayToOutput("[mrwy]", mainLandingRunwayComboBox);
            #endregion

            #region SECONDARY LANDING RUNWAY
            if (secondaryLandingRunwayCheckBox.Checked) output += runwayToOutput("[slrwy]", secondaryLandingRunwayComboBox);
            #endregion

            #region MAIN DEPARTURE RUNWAY
            if (mainDepartureRunwayCheckBox.Checked && !mainLandingRunwayComboBox.Text.Equals(mainDepartureRunwayComboBox.Text)) output += runwayToOutput("[mtrwy]", mainDepartureRunwayComboBox);
            #endregion

            #region SECONDARY DEPARTURE RUNWAY
            if (secondaryDepartureRunwayCheckBox.Checked) output += runwayToOutput("[strwy]", secondaryLandingRunwayComboBox);
            #endregion

            #region TL
            output += "[trl]";
            output += calculateTransitionLevel();
            #endregion

            #region OPERATIONAL REPORTS
            output += operationalReportToOutput();
            #endregion

            output += "[pause]";

            #region WIND
            if (metarProcessor.metar.Wind != null) output += windToOutput(metarProcessor.metar.Wind);
            #endregion

            #region CAVOK
            if (metarProcessor.metar.CAVOK) output += "[cavok]";
            #endregion

            #region VISIBILITY
            if (metarProcessor.metar.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.Visibility);
            #endregion

            //TODO 4 CHAR OPTIONS
            #region PHENOMENA
            output += listToOutput(metarProcessor.metar.Phenomena);
            #endregion

            #region CLOUDS OPTIONS
            if (metarProcessor.metar.SKC) output += "[skc]";
            if (metarProcessor.metar.NSC) output += "[nsc]";
            #endregion

            #region CLOUDS
            output += listToOutput(metarProcessor.metar.Clouds);
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

            #region TEMPO
            if (metarProcessor.metar.metarTEMPO != null)
            {
                output += "[tempo]";

                #region TEMPO WIND
                if (metarProcessor.metar.metarTEMPO.Wind != null) output += windToOutput(metarProcessor.metar.metarTEMPO.Wind);
                #endregion

                #region TEMPO VISIBILITY
                if (metarProcessor.metar.metarTEMPO.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.metarTEMPO.Visibility);
                #endregion

                #region TEMPO PHENOMENA
                if (metarProcessor.metar.metarTEMPO.Phenomena.Count > 0) output += listToOutput(metarProcessor.metar.metarTEMPO.Phenomena);
                #endregion

                #region TEMPO SKC
                if (metarProcessor.metar.metarTEMPO.SKC) output += "[skc]";
                #endregion

                #region TEMPO NSW
                if (metarProcessor.metar.metarTEMPO.NSW) output += "[nsw]";
                #endregion

                #region TEMPO CLOUDS
                if (metarProcessor.metar.metarTEMPO.Clouds.Count > 0) output += listToOutput(metarProcessor.metar.metarTEMPO.Clouds);
                #endregion
            }
            #endregion

            #region BECMG
            if (metarProcessor.metar.metarBECMG != null)
            {
                output += "[becmg]";

                #region BECMG WIND
                if (metarProcessor.metar.metarBECMG.Wind != null) output += windToOutput(metarProcessor.metar.metarBECMG.Wind);
                #endregion

                #region BECMG VISIBILITY
                if (metarProcessor.metar.metarBECMG.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.metarBECMG.Visibility);
                #endregion

                #region BECMG PHENOMENA
                if (metarProcessor.metar.metarBECMG.Phenomena.Count > 0) output += listToOutput(metarProcessor.metar.metarBECMG.Phenomena);
                #endregion

                #region BECMG SKC
                if (metarProcessor.metar.metarBECMG.SKC) output += "[skc]";
                #endregion

                #region BECMG NSW
                if (metarProcessor.metar.metarBECMG.NSW) output += "[nsw]";
                #endregion

                #region BECMG CLOUDS
                if (metarProcessor.metar.metarBECMG.Clouds.Count > 0) output += listToOutput(metarProcessor.metar.metarBECMG.Clouds);
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

        /// <summary>
        /// Method called when exit tool strip item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Method called when ICAO tab is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ICAOTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ICAOTabControl.SelectedTab.Name.Equals("EHAM")) icaoTextBox.Text = "EHAM";
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHBK")) icaoTextBox.Text = "EHBK";
            else  if (ICAOTabControl.SelectedTab.Name.Equals("EHEH")) icaoTextBox.Text = "EHEH";
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHGG")) icaoTextBox.Text = "EHGG";
            else icaoTextBox.Text = "EHRD";
        }
    }
}
