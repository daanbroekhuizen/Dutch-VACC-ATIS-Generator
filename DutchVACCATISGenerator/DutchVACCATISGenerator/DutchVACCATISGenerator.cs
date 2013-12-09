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

        private RunwayInfo runwayInfo;

        /// <summary>
        /// Constructor of DutchVACCATISGenerator.
        /// </summary>
        public DutchVACCATISGenerator()
        {
            InitializeComponent();

            //TODO EHEH METAR
            EHEH.Enabled = false;

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

        /// <summary>
        /// Split metar on split word.
        /// </summary>
        /// <param name="metar">METAR to split.</param>
        /// <param name="splitWord">Word to split on. (BECMG, TEMPO)</param>
        /// <returns></returns>
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
        /// Method called when process metar button is clicked. Initiates a MetarProcessor which will process the metar inputted into easy accessible fields.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void processMetarButton_Click(object sender, EventArgs e)
        {
            if (metarTextBox.Text.Trim().Equals(String.Empty))
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
            metarTextBox.Clear();

            if(metar.Length > 140) lastLabel.Text = "Last successful processed metar:\n" + metar.Substring(0, 69) + "\n" + metar.Substring(69, 69) + "...";
            else if (metar.Length > 69) lastLabel.Text = "Last successful processed metar:\n" + metar.Substring(0, 69) + "\n" + metar.Substring(69);
            else lastLabel.Text = "Last successful processed metar:\n" + metar;

            if (atisIndex == 25) atisIndex = 0;
            else atisIndex++;

            atisLetterLabel.Text = phoneticAlphabet[atisIndex];

            generateATISButton.Enabled = true;
            if (ICAOTabControl.SelectedIndex == 0) runwayInfoButton.Enabled = true;
            else runwayInfoButton.Enabled = false;

            if (runwayInfo != null && runwayInfoButton.Visible)
            {
                runwayInfo.metar = metarProcessor.metar;
                runwayInfo.fillRunwayInfoDataGrids();
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
        /// Method called when main landing runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void mainLandingRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHAMmainLandingRunwayCheckBox.Checked) EHAMmainLandingRunwayComboBox.Enabled = true;
            else EHAMmainLandingRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when secondary landing runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void secondaryLandingRunway_CheckedChanged(object sender, EventArgs e)
        {
            if (EHAMsecondaryLandingRunwayCheckBox.Checked) EHAMsecondaryLandingRunwayComboBox.Enabled = true;
            else EHAMsecondaryLandingRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when main departure runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void mainDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHAMmainDepartureRunwayCheckBox.Checked) EHAMmainDepartureRunwayComboBox.Enabled = true;
            else EHAMmainDepartureRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when secondary departure runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void secondaryDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHAMsecondaryDepartureRunwayCheckBox.Checked) EHAMsecondaryDepartureRunwayComboBox.Enabled = true;
            else EHAMsecondaryDepartureRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when EHRD main runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void EHRDmainRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHRDmainRunwayCheckBox.Checked) EHRDmainRunwayComboBox.Enabled = true;
            else EHRDmainRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when EHGG main runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void EHGGmainRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHGGmainRunwayCheckBox.Checked) EHGGmainRunwayComboBox.Enabled = true;
            else EHGGmainRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when EHBK main runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void EHBKmainRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHBKmainRunwayCheckBox.Checked) EHBKmainRunwayComboBox.Enabled = true;
            else EHBKmainRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// Method called when EHEH main runway check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void EHEHmainRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EHEHmainRunwayCheckBox.Checked) EHEHmainRunwayComboBox.Enabled = true;
            else EHEHmainRunwayComboBox.Enabled = false;
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
            String output = "[opr]";

            #region LOW LEVEL VISBILITY
            if (metarProcessor.metar.Visibility < 1500) output += "[lvp]";
            #endregion

            #region RWY CONFIGURATIONS
            if (EHAMmainLandingRunwayComboBox.Text.Equals("18R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18C")) output += "[independend]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36C")) output += "[independend]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("18R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18R")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("18C") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18C")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("06") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36R")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("06")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("09") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36R")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("09")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36R")) output += "[convapp]";

            else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27")) output += "[convapp]";
            #endregion

            #region CHECK FOR ADDING [AND]
            if (output.Contains("[lvp]") && (output.Contains("[independend]") || output.Contains("[convapp]")))
            {
                if(output.Contains("[independend]")) output = output.Insert(output.IndexOf("[ind"), "[and]");
                
                else output = output.Insert(output.IndexOf("[con"), "[and]");
            }
            #endregion

            if (!output.Equals("[opr]"))  return output;
            
            else return "";
        }

        /// <summary>
        /// Method called when approach only check box check status changes.
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
        /// Method called when arrival only check box check status changes.
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
        /// Method called when approach + arrival only check box check status changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void appArrOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (appArrOnlyCheckBox.Checked) appOnlyCheckBox.Enabled = arrOnlyCheckBox.Enabled = appOnlyCheckBox.Checked = arrOnlyCheckBox.Checked = false;
            else appOnlyCheckBox.Enabled = arrOnlyCheckBox.Enabled = true;
        }


        /// <summary>
        /// Generate output from List<T>
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="input">List<T></param>
        /// <returns>String output</returns>
        private String listToOutput<T>(List<T> input)
        {
            String output = String.Empty;

            #region MetarPhenomena
            if (input is List<MetarPhenomena>)
            {
                foreach (MetarPhenomena metarPhenomena in input as List<MetarPhenomena>)
                {
                    if (metarPhenomena.hasIntensity)  output += "[-]";

                    if (metarPhenomena.phenomena.Equals("BCFG") || metarPhenomena.phenomena.Equals("MIFG") || metarPhenomena.phenomena.Equals("SHRA") || metarPhenomena.phenomena.Equals("VCSH") || metarPhenomena.phenomena.Equals("VCTS"))
                    {
                        switch (metarPhenomena.phenomena)
                        {
                            case "BCFG":
                                output += "[bcfg]";
                                break;

                            case "MIFG":
                                output += "[mifg]";
                                break;

                            case "SHRA":
                                output += "[shra]";
                                break;

                            case "VCSH":
                                output += "[vcsh]";
                                break;

                            case "VCTS":
                                output += "[vcts]";
                                break;
                        }
                    }
                    else if(metarPhenomena.phenomena.Count() > 2)
                    {
                        int length = metarPhenomena.phenomena.Length;

                        if ((length % 2) == 0)
                        {
                            int index = 0;

                            while(index != length)
                            {
                                if (!(length - index == 2)) output += "[" + metarPhenomena.phenomena.Substring(index, 2).ToLower() + "]";
                                
                                else output += "[" + metarPhenomena.phenomena.Substring(index).ToLower() + "]";

                                index = index + 2;
                            }
                        }
                    }
                    else output += "[" + metarPhenomena.phenomena.ToLower() + "]";

                    if (metarPhenomena != (MetarPhenomena)Convert.ChangeType(input.Last(), typeof(MetarPhenomena))) output += "[and]";
                }

          
            }
            #endregion

            #region MetarCloud
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
            #endregion

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

            if (input < 800) output += "[<]8[hundred][meters]";
            else if (input < 1000) output += Convert.ToString(input / 100) + "[hundred][meters]";
            else if (input < 5000 && (input % 1000) != 0) output += Convert.ToString(input / 1000) + "[thousand]" + ((input % 1000) / 100).ToString() + "[hundred][meters]";
            else if (input >= 9999) output += "10[km]";
            else output += Convert.ToString(input / 1000) + "[km]";

            return output;
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
        /// Method to check if the required runway selections are made based on the selected tab.
        /// </summary>
        /// <returns>Boolean indicating if all required check boxes are checked for generating a runway output.</returns>
        private Boolean checkRunwaySelected()
        {
            switch (ICAOTabControl.SelectedTab.Name)
            {
                #region EHAM
                case "EHAM":
                    if (!EHAMmainLandingRunwayCheckBox.Checked || EHAMmainLandingRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No main landing runway selected.", "Error"); return false;
                    }

                    if (!EHAMmainDepartureRunwayCheckBox.Checked || EHAMmainDepartureRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No main departure runway selected.", "Error"); return false;
                    }

                    if (EHAMsecondaryLandingRunwayCheckBox.Checked && EHAMsecondaryLandingRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Secondary landing runway is checked but no runway is selected.", "Error"); return false;
                    }

                    if (EHAMsecondaryDepartureRunwayCheckBox.Checked && EHAMsecondaryDepartureRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Secondary departure runway is checked but no runway is selected.", "Error"); return false;
                    }

                    return true;
                #endregion

                #region EHBK
                case "EHBK":
                    if (!EHBKmainRunwayCheckBox.Checked || EHBKmainRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No main runway selected.", "Error"); return false;
                    }
                    return true;
                #endregion

                #region EHEH
                case "EHEH":
                    if (!EHEHmainRunwayCheckBox.Checked || EHEHmainRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No main runway selected.", "Error"); return false;
                    }
                    return true;
                #endregion

                #region EHGG
                case "EHGG":
                    if (!EHGGmainRunwayCheckBox.Checked || EHGGmainRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No main runway selected.", "Error"); return false;
                    }
                    return true;
                #endregion

                #region EHRD
                case "EHRD":
                    if (!EHRDmainRunwayCheckBox.Checked || EHRDmainRunwayComboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No main runway selected.", "Error"); return false;
                    }
                    return true;
                #endregion
            }

            return false;
        }

        /// <summary>
        /// Method to generate route output.
        /// </summary>
        /// <returns>String containing the runway output of the selected airport tab.</returns>
        private String generateRunwayOutput()
        {
            String output = String.Empty;

            switch (ICAOTabControl.SelectedTab.Name)
            {
                #region EHAM
                case "EHAM":
                    #region EHAM MAIN LANDING RUNWAY
                    if (EHAMmainLandingRunwayCheckBox.Checked && !EHAMmainLandingRunwayComboBox.Text.Equals(EHAMmainDepartureRunwayComboBox.Text)) output += runwayToOutput("[mlrwy]", EHAMmainLandingRunwayComboBox);

                    else output += runwayToOutput("[mrwy]", EHAMmainLandingRunwayComboBox);
                    #endregion

                    #region EHAM SECONDARY LANDING RUNWAY
                    if (EHAMsecondaryLandingRunwayCheckBox.Checked) output += runwayToOutput("[slrwy]", EHAMsecondaryLandingRunwayComboBox);
                    #endregion

                    #region EHAM MAIN DEPARTURE RUNWAY
                    if (EHAMmainDepartureRunwayCheckBox.Checked && !EHAMmainLandingRunwayComboBox.Text.Equals(EHAMmainDepartureRunwayComboBox.Text)) output += runwayToOutput("[mtrwy]", EHAMmainDepartureRunwayComboBox);
                    #endregion

                    #region EHAM SECONDARY DEPARTURE RUNWAY
                    if (EHAMsecondaryDepartureRunwayCheckBox.Checked) output += runwayToOutput("[strwy]", EHAMsecondaryLandingRunwayComboBox);
                    #endregion
                    break;
                #endregion

                #region EHBK
                case "EHBK":
                    if (EHBKmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHBKmainRunwayComboBox);
                    break;
                #endregion

                #region EHEH
                case "EHEH":
                    if (EHEHmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHEHmainRunwayComboBox);
                    break;
                #endregion

                #region EHGG
                case "EHGG":
                    if (EHGGmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHGGmainRunwayComboBox);
                    break;
                #endregion

                #region EHRD
                case "EHRD":
                    if (EHRDmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHRDmainRunwayComboBox);
                    break;
                #endregion
            }

            return output;
        }
        
        /// <summary>
        /// Method called when generate ATIS button is clicked. Processes field from MetarProcessor to output string.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void generateATISButton_Click(object sender, EventArgs e)
        {
            if (!metarProcessor.metar.ICAO.Equals(ICAOTabControl.SelectedTab.Name))
            {
                MessageBox.Show("Selected ICAO tab does not match the ICAO of the processed metar.", "Error"); return;
            }

            if (!checkRunwaySelected()) return;
            
            String output = String.Empty;

            #region ICAO
            switch (metarProcessor.metar.ICAO)
            {
                case "EHAM":
                    output += "[ehamatis]";
                    break;

                case "EHBK":
                    output += "[ehbkatis]";
                    break;

                case "EHEH":
                    output += "[ehehatis]";
                    break;

                case "EHGG":
                    output += "[ehggatis]";
                    break;

                case "EHRD":
                    output += "[ehrdatis]";
                    break;
            }
            #endregion

            #region ATIS LETTER
            output += phoneticAlphabet[atisIndex];
            output += "[pause]";
            #endregion

            #region RUNWAYS
            output += generateRunwayOutput();
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

            #region RVRONATC
            if (metarProcessor.metar.RVR) output += "[rvronatc]";
            #endregion

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

            #region VERTICAL VISIBILITY
            if (metarProcessor.metar.VerticalVisibility > 0) output += "[vv]" + metarProcessor.metar.VerticalVisibility + "[hundred][meters]";
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

                #region TEMPO VERTICAL VISIBILITY
                if (metarProcessor.metar.metarTEMPO.VerticalVisibility > 0) output += "[vv]" + metarProcessor.metar.metarTEMPO.VerticalVisibility + "[hundred][meters]";
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

                #region BECMG VERTICAL VISIBILITY
                if (metarProcessor.metar.metarBECMG.VerticalVisibility > 0) output += "[vv]" + metarProcessor.metar.metarBECMG.VerticalVisibility + "[hundred][meters]";
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
            
            resetTabView();
        }

        /// <summary>
        /// Resets the components of the ICAO tabs to their default states.
        /// </summary>
        private void resetTabView()
        {
            #region EHAM
            if (!ICAOTabControl.SelectedTab.Name.Equals("EHAM"))
            {
                EHAMmainLandingRunwayCheckBox.Checked = EHAMmainDepartureRunwayCheckBox.Checked = EHAMsecondaryLandingRunwayCheckBox.Checked = EHAMsecondaryDepartureRunwayCheckBox.Checked = false;
                EHAMmainLandingRunwayComboBox.SelectedIndex = EHAMmainDepartureRunwayComboBox.SelectedIndex = EHAMsecondaryLandingRunwayComboBox.SelectedIndex = EHAMsecondaryDepartureRunwayComboBox.SelectedIndex = -1;
            }
            #endregion

            #region EHBK
            if (!ICAOTabControl.SelectedTab.Name.Equals("EHBK"))
            {
                EHBKmainRunwayCheckBox.Checked = false;
                EHBKmainRunwayComboBox.SelectedIndex = -1;
            }
            #endregion

            #region EHEH
            if (!ICAOTabControl.SelectedTab.Name.Equals("EHEH"))
            {
                EHEHmainRunwayCheckBox.Checked = false;
                EHEHmainRunwayComboBox.SelectedIndex = -1;
            }
            #endregion

            #region EHGG
            if (!ICAOTabControl.SelectedTab.Name.Equals("EHGG"))
            {
                EHGGmainRunwayCheckBox.Checked = false;
                EHGGmainRunwayComboBox.SelectedIndex = -1;
            }
            #endregion

            #region EHRD
            if (!ICAOTabControl.SelectedTab.Name.Equals("EHRD"))
            {
                EHRDmainRunwayCheckBox.Checked = false;
                EHRDmainRunwayComboBox.SelectedIndex = -1;
            }
            #endregion
        }

        /// <summary>
        /// Method called when the about tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new About();
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metarTextBox_TextChanged(object sender, EventArgs e)
        {
            if (metarTextBox.Text.Trim().Equals(String.Empty)) processMetarButton.Enabled = false;
            else processMetarButton.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runwayInfoButton_Click(object sender, EventArgs e)
        {
            if (runwayInfo == null) runwayInfo = new RunwayInfo(this, metarProcessor.metar);

            if (runwayInfo != null && !runwayInfo.Visible)
            {
                runwayInfoButton.Text = "<";
                runwayInfo.Show();
            }
            else if (runwayInfo != null)
            {
                runwayInfoButton.Text = ">";
                runwayInfo.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DutchVACCATISGenerator_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal && runwayInfo != null) runwayInfo.BringToFront();
        }
    }
}
