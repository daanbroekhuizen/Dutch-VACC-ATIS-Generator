using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// DutchVACCATISGenerator class.
    /// </summary>
    public partial class DutchVACCATISGenerator : Form
    {
        private int atisIndex { get; set; }
        private List<String> departureRunways { get; set; }
        private List<String> landingRunways { get; set; }
        private String latestVersion { get; set; }
        private List<String> phoneticAlphabet { get; set; }
        private String metar { get; set; }
        private MetarProcessor metarProcessor { get; set; }
        private RunwayInfo runwayInfo { get; set; }
        private Boolean runwayInfoState { get; set; }
        private Sound sound { get; set; }
        private Boolean soundState { get; set; }

        /// <summary>
        /// Constructor of DutchVACCATISGenerator.
        /// </summary>
        public DutchVACCATISGenerator()
        {
            InitializeComponent();

            phoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            
            //Set ATIS index to Z for first generation.
            atisIndex = 25;

            //Set the label to A.
            atisLetterLabel.Text = phoneticAlphabet[0];

            //Center the form on the monitor.
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, ((Screen.PrimaryScreen.WorkingArea.Height - (this.Height + 184)) / 2));

            runwayInfoState = false;

            //Start version background worker.
            versionBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Method called when get METAR button is clicked. Starts a background worker which pulls the METAR from the VATSIM METAR website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getMetarButton_Click(object sender, EventArgs e)
        {
            //Get ICAO entered.
            String _ICAO = icaoTextBox.Text;

            //If no ICAO has been entered.
            if(_ICAO == String.Empty)
            {
                MessageBox.Show("Enter an ICAO code.", "Warning"); return;
            }

            //Disable the get METAR button so the user can't overload it.
            getMetarButton.Enabled = false;

            if (realEHAMRunwaysCheckBox.Visible && realEHAMRunwaysCheckBox.Checked)
                realRunwayBackgroundWorker.RunWorkerAsync();

            //Start METAR background worker to start pulling the METAR.
            metarBackgroundWorker.RunWorkerAsync(_ICAO);
        }

        /// <summary>
        /// Method called when METAR background workers is started. Pulls METAR from VATSIM METAR website.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void metarBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Try to pull the METAR from http://metar.vatsim.net/metar.php.
            try
            {
                //Request METAR.
                WebRequest request = WebRequest.Create("http://metar.vatsim.net/metar.php?id=" + e.Argument);
                WebResponse response = request.GetResponse();          

                //Read METAR.
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                metar = reader.ReadToEnd();

                //Remove spaces.
                if (metar.StartsWith(e.Argument.ToString())) metar = metar.Trim();
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to get the METAR from the Internet.\nPlease provide a METAR.", "Error");
            }
        }

        /// <summary>
        /// Method called when METAR background worker has completed its task. Sets pulled METAR from VATSIM METAR website into the metarTextBox.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void metarBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Set pulled METAR in the METAR text box.
            metarTextBox.Text = metar;

            //Re-enable the get METAR button.
            getMetarButton.Enabled = true;
        }

        /// <summary>
        /// Split METAR on split word.
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

                case "BLU":
                    regex = new Regex(@"\bBLU\b");
                    break;

                case "WHT":
                    regex = new Regex(@"\bWHT\b");
                    break;

                case "GRN":
                    regex = new Regex(@"\bGRN\b");
                    break;

                case "YLO":
                    regex = new Regex(@"\bYLO\b");
                    break;

                case "AMB":
                    regex = new Regex(@"\bAMB\b");
                    break;

                case "RED":
                    regex = new Regex(@"\bRED\b");
                    break;

                case "BLACK":
                    regex = new Regex(@"\bBLACK\b");
                    break;
            }

            return regex.Split(metar);
        }

        /// <summary>
        /// Method called when process METAR button is clicked. Initiates a MetarProcessor which will process the METAR inputted into easy accessible fields.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void processMetarButton_Click(object sender, EventArgs e)
        {
            //Check if a METAR has been entered.
            if (metarTextBox.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("No METAR fetched or entered.", "Error"); return;
            }
            //Check if entered METAR ICAO matches the selected ICAO tab.
            else if (!metarTextBox.Text.Trim().StartsWith((ICAOTabControl.SelectedTab.Name)))
            {
                MessageBox.Show("Selected ICAO tab does not match the ICAO of the entered METAR.", "Warning"); return;
            }
            //Get METAR from METAR text box.
            else metar = metarTextBox.Text.Trim();

            #region MILITARY CODES
            //If METAR contains military visibility code.
            if (Regex.IsMatch(metar, @"(^|\s)BLU(\s|$)") || Regex.IsMatch(metar, @"(^|\s)WHT(\s|$)") || Regex.IsMatch(metar, @"(^|\s)GRN(\s|$)") || Regex.IsMatch(metar, @"(^|\s)YLO(\s|$)") || Regex.IsMatch(metar, @"(^|\s)AMB(\s|$)") || Regex.IsMatch(metar, @"(^|\s)RED(\s|$)") || Regex.IsMatch(metar, @"(^|\s)BLACK(\s|$)"))
            {
                //Military visibility codes.
                String[] militaryColors = new String[] { "BLU", "WHT", "GRN", "YLO", "AMB", "RED", "BLACK" };

                //If METAR contains BECMG and TEMPO
                if (metar.Contains("BECMG") && metar.Contains("TEMPO"))
                {
                    if (metar.IndexOf("BECMG") < metar.IndexOf("TEMPO"))
                    {
                        //Check which military visibility code the METAR contains.
                        foreach (String militaryColor in militaryColors)
                        {
                            if (Regex.IsMatch(metar, @"(^|\s)" + militaryColor + @"(\s|$)")) metarProcessor = new MetarProcessor(splitMetar(metar, militaryColor)[0].Trim() /* BASE METAR */, splitMetar(splitMetar(splitMetar(metar, militaryColor)[1].Trim(), "BECMG")[1].Trim(), "TEMPO")[1].Trim() /* TEMPO TREND */, splitMetar(splitMetar(splitMetar(metar, militaryColor)[1].Trim(), "BECMG")[1].Trim(), "TEMPO")[0].Trim() /* BECMG TREND */);
                        }
                    }
                    else
                    {
                        //Check which military visibility code the METAR contains.
                        foreach (String militaryColor in militaryColors)
                        {
                            if (Regex.IsMatch(metar, @"(^|\s)" + militaryColor + @"(\s|$)")) metarProcessor = new MetarProcessor(splitMetar(metar, militaryColor)[0].Trim() /* BASE METAR */, splitMetar(splitMetar(splitMetar(metar, militaryColor)[1].Trim(), "TEMPO")[1].Trim(), "BECMG")[0].Trim() /* TEMPO TREND */, splitMetar(splitMetar(splitMetar(metar, militaryColor)[1].Trim(), "TEMPO")[1].Trim(), "BECMG")[1].Trim() /* BECMG TREND */);
                        }
                    }
                }
                //If METAR contains BECMG or TEMPO
                else if (metar.Contains("BECMG") || metar.Contains("TEMPO"))
                {
                    //If METAR contains BECMG.
                    if (metar.Contains("BECMG"))
                    {
                        //Check which military visibility code the METAR contains.
                        foreach (String militaryColor in militaryColors)
                        {
                            if (Regex.IsMatch(metar, @"(^|\s)" + militaryColor + @"(\s|$)")) metarProcessor = new MetarProcessor(splitMetar(metar, militaryColor)[0].Trim() /* BASE METAR */, splitMetar(splitMetar(metar, militaryColor)[1].Trim(), "BECMG")[1].Trim() /* BECMG TREND */, MetarType.BECMG);
                        }
                    }
                    else
                    {
                        //Check which military visibility code the METAR contains.
                        foreach (String militaryColor in militaryColors)
                        {
                            if (Regex.IsMatch(metar, @"(^|\s)" + militaryColor + @"(\s|$)")) metarProcessor = new MetarProcessor(splitMetar(metar, militaryColor)[0].Trim() /* BASE METAR */, splitMetar(splitMetar(metar, militaryColor)[1].Trim(), "TEMPO")[1].Trim() /* TEMPO TREND */, MetarType.TEMPO);
                        }
                    }
                }
                //If METAR only contains military visibility code.
                else
                {
                    //Check which military visibility code the METAR contains.
                    foreach (String militaryColor in militaryColors)
                    {
                        if (Regex.IsMatch(metar, @"(^|\s)" + militaryColor + @"(\s|$)")) metarProcessor = new MetarProcessor(splitMetar(metar, militaryColor)[0].Trim());
                    }
                }
            }
            #endregion

            #region BECMG AND TEMPO
            //If METAR contains both BECMG and TEMPO trends.
            else if (metar.Contains("BECMG") && metar.Contains("TEMPO"))
            {
                //If BECMG is the first trend.
                if (metar.IndexOf("BECMG") < metar.IndexOf("TEMPO")) metarProcessor = new MetarProcessor(splitMetar(metar, "BECMG")[0].Trim(), splitMetar(metar, "TEMPO")[1].Trim(), splitMetar(splitMetar(metar, "BECMG")[1].Trim(), "TEMPO")[0].Trim());
                //If TEMPO is the first trend.
                else metarProcessor = new MetarProcessor(splitMetar(metar, "TEMPO")[0].Trim(), splitMetar(splitMetar(metar, "TEMPO")[1].Trim(), "BECMG")[0].Trim(), splitMetar(metar, "BECMG")[1].Trim());
            }
            #endregion

            #region BECMG ONLY
            //If METAR only contains BECMG.
            else if (metar.Contains("BECMG")) metarProcessor = new MetarProcessor(splitMetar(metar, "BECMG")[0].Trim(), splitMetar(metar, "BECMG")[1].Trim(), MetarType.BECMG);
            #endregion

            #region TEMPO ONLY
            //If METAR only contains TEMPO.
            else if (metar.Contains("TEMPO")) metarProcessor = new MetarProcessor(splitMetar(metar, "TEMPO")[0].Trim(), splitMetar(metar, "TEMPO")[1].Trim(), MetarType.TEMPO);
            #endregion

            //Process non trend containing METAR.
            else metarProcessor = new MetarProcessor(metar);

            //Calculate the transition level.
            try
            {
                tlOutLabel.Text = calculateTransitionLevel().ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error parsing the METAR, check if METAR is in correct format.", "Error"); return;
            }
            
            //Clear output and METAR text box.
            outputTextBox.Clear();
            metarTextBox.Clear();

            //Set processed METAR in last processed METAR label.
            if(metar.Length > 140) lastLabel.Text = "Last successful processed METAR:\n" + metar.Substring(0, 69).Trim() + "\n" + metar.Substring(69, 690).Trim() + "...";
            else if (metar.Length > 69) lastLabel.Text = "Last successful processed METAR:\n" + metar.Substring(0, 69).Trim() + "\n" + metar.Substring(69).Trim();
            else lastLabel.Text = "Last successful processed METAR:\n" + metar;

            //Add 1 to ATIS index (next letter).
            if (atisIndex == 25) atisIndex = 0;
            else atisIndex++;

            //Set ATIS letter in ATIS letter label.
            atisLetterLabel.Text = phoneticAlphabet[atisIndex];

            //Enable generate ATIS and runway info button.
            generateATISButton.Enabled = true;
            runwayInfoButton.Enabled = true;

            //If runwayInfo is null, create RunwayInfo form.
            if (runwayInfo == null) runwayInfo = new RunwayInfo(this, metarProcessor.metar);
            else
            {
                //Update runway info form.
                runwayInfo.metar = metarProcessor.metar;
                runwayInfo.setVisibleRunwayInfoDataGrid(ICAOTabControl.SelectedTab.Text);
                runwayInfo.checkICAOTabSelected();
            }

            if (selectBestRunwayCheckBox.Visible && selectBestRunwayCheckBox.Checked)
            {
                runwayInfo.ICAOBestRunway(ICAOTabControl.SelectedTab.Name);
                selectBestRunwayCheckBox.Checked = false;

                //UNCOMMENT
                MessageBox.Show("Controller notice! Verify auto selected runway(s).", "Warning");
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

            //Set ATIS letter in ATIS letter label.
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

            //Set ATIS letter in ATIS letter label.
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
        /// <param name="input">Runway identifier letter (L, C, R).</param>
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

            return String.Empty;
        }

        /// <summary>
        /// Calculate transition level from QNH and temperature.
        /// </summary>
        /// <returns>Calculated TL.</returns>
        private int calculateTransitionLevel()
        {
            int temp;

            //If METAR contains M (negative value), multiply by -1 to make an negative integer.
            if (metarProcessor.metar.Temperature.StartsWith("M")) temp = Convert.ToInt32(metarProcessor.metar.Temperature.Substring(1)) * -1;
            else temp = Convert.ToInt32(metarProcessor.metar.Temperature);

            //Calculate TL level. TL = 307.8-0.13986*T-0.26224*Q (thanks to Stefan Blauw for this formula).
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

            //If selected runway contains runway identifier letter.
            if (runwayComboBox.SelectedItem.ToString().Length > 2)
            {
                //Add runway identifier numbers to output.
                output += runwayComboBox.SelectedItem.ToString().Substring(0, 2);
                //Add runway identifier letter to output.
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
            //If visibility is not 0 or less than 1500 meter, add low visibility procedure phrase.
            if (metarProcessor.metar.Visibility != 0 && metarProcessor.metar.Visibility < 1500) output += "[lvp]";
            #endregion

            #region RWY CONFIGURATIONS
            if (EHAMmainLandingRunwayCheckBox.Checked && EHAMsecondaryLandingRunwayCheckBox.Checked)
            {
                if (EHAMmainLandingRunwayComboBox.Text.Equals("18R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18C")) output += "[independent]";

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36C")) output += "[independent]";

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
            }
            #endregion

            #region CHECK FOR ADDING [AND]
            if (output.Contains("[lvp]") && (output.Contains("[independent]") || output.Contains("[convapp]")))
            {
                if (output.Contains("[independent]")) output = output.Insert(output.IndexOf("[ind"), "[and]");
                
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
            //If list is a MetarPhenomena list.
            if (input is List<MetarPhenomena>)
            {
                foreach (MetarPhenomena metarPhenomena in input as List<MetarPhenomena>)
                {
                    //If phenomena has intensity.
                    if (metarPhenomena.hasIntensity) output += "[-]";

                    //If phenomena is 4 character phenomena (BCFG | MIFG | SHRA | VCSH | VCTS).
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
                    //If phenomena is multi-phenomena (count > 2).
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
                    //If phenomena is 2 char phenomena.
                    else output += "[" + metarPhenomena.phenomena.ToLower() + "]";

                    //If loop phenomena is not the last phenomena of the list, add [and].
                    if (metarPhenomena != (MetarPhenomena)Convert.ChangeType(input.Last(), typeof(MetarPhenomena))) output += "[and]";
                }
            }
            #endregion

            #region MetarCloud
            //If list is a MetarCloud list.
            else if (input is List<MetarCloud>)
            {
                foreach (MetarCloud metarCloud in input as List<MetarCloud>)
                {
                    //Add cloud type identifier.
                    output += "[" + metarCloud.cloudType.ToLower() + "]";

                    //If cloud altitude is round ten-thousand (e.g. 10000 (100), 20000 (200), 30000 (300)).
                    if (metarCloud.altitude % 100 == 0) output += Math.Floor(Convert.ToDouble(metarCloud.altitude / 100)).ToString() + "0" + "[thousand]";

                    else
                    {
                        //If cloud altitude is greater than a ten-thousand (e.g. 12000 (120), 23500 (235), 45000 (450)).
                        if (metarCloud.altitude / 100 > 0) output += Math.Floor(Convert.ToDouble(metarCloud.altitude / 100)).ToString();

                        //If cloud altitude has a thousand (e.g. 2000 (020), 4000 (040), 5000 (050)).
                        if ((metarCloud.altitude / 10) % 10 > 0) output += Math.Floor(Convert.ToDouble((metarCloud.altitude / 10) % 10)) + "[thousand]";

                        //If cloud altitude has a hundred (e.g. 200 (002), 400 (004), 500 (005)).
                        if (metarCloud.altitude % 10 > 0) output += metarCloud.altitude % 10 + "[hundred]";

                        //If cloud altitude equals ground level.
                        if (metarCloud.altitude == 0) output += metarCloud.altitude;
                    }

                    output += "[ft]";

                    //If cloud type has addition (e.g. CB, TCU).
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

            //If visibility is lower than 800 meters (less than 800 meters phrase).
            if (input < 800) output += "[<]8[hundred][meters]";
            //If visibility is lower than 1000 meters (add hundred).
            else if (input < 1000) output += Convert.ToString(input / 100) + "[hundred][meters]";
            //If visibility is lower than 5000 meters and visibility is not a thousand number.
            else if (input < 5000 && (input % 1000) != 0) output += Convert.ToString(input / 1000) + "[thousand]" + ((input % 1000) / 100).ToString() + "[hundred][meters]";
            //If visibility is >= 9999 (10 km phrase).
            else if (input >= 9999) output += "10[km]";
            //If visibility is thousand.
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

            //If MetarWind has a calm wind.
            if (input.VRB) output += "[vrb]" + input.windKnots + "[kt]";
            //If MetarWind has a gusting wind.
            else if (input.windGustMin != null) output += input.windHeading + "[deg]" + input.windGustMin + "[max]" + input.windGustMax + "[kt]";
            //If MetarWind has a normal wind.
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
                //If selected ICAO tab is EHAM.
                case "EHAM":
                    //If EHAM main landing runway check box is checked OR the EHAM main landing runway combo box selection is NULL.
                    if (!EHAMmainLandingRunwayCheckBox.Checked || EHAMmainLandingRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main landing runway selected.", "Warning"); return false;
                    }

                    //If EHAM main departure runway check box is checked OR the EHAM main departure runway combo box selection is NULL.
                    if (!EHAMmainDepartureRunwayCheckBox.Checked || EHAMmainDepartureRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main departure runway selected.", "Warning"); return false;
                    }

                    //If EHAM secondary landing runway check box is checked OR the EHAM secondary landing runway combo box selection is NULL.
                    if (EHAMsecondaryLandingRunwayCheckBox.Checked && EHAMsecondaryLandingRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("Secondary landing runway is checked but no runway is selected.", "Warning"); return false;
                    }

                    //If EHAM secondary departure runway check box is checked OR the EHAM secondary departure runway combo box selection is NULL.
                    if (EHAMsecondaryDepartureRunwayCheckBox.Checked && EHAMsecondaryDepartureRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("Secondary departure runway is checked but no runway is selected.", "Warning"); return false;
                    }

                    return true;
                #endregion

                #region EHBK
                //If selected ICAO tab is EHBK.
                case "EHBK":
                    //If EHBK main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!EHBKmainRunwayCheckBox.Checked || EHBKmainRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning"); return false;
                    }

                    return true;
                #endregion

                #region EHEH
                //If selected ICAO tab is EHEH.
                case "EHEH":
                    //If EHEH main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!EHEHmainRunwayCheckBox.Checked || EHEHmainRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning"); return false;
                    }

                    return true;
                #endregion

                #region EHGG
                //If selected ICAO tab is EHGG.
                case "EHGG":
                    //If EHGG main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!EHGGmainRunwayCheckBox.Checked || EHGGmainRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning"); return false;
                    }

                    return true;
                #endregion

                #region EHRD
                //If selected ICAO tab is EHRD.
                case "EHRD":
                    //If EHRD main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!EHRDmainRunwayCheckBox.Checked || EHRDmainRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning"); return false;
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
                //If selected ICAO tab is EHAM.
                case "EHAM":
                    #region EHAM MAIN LANDING RUNWAY
                    //If the EHAM main landing runway check box is checked AND the EHAM main landing runway combo box value doesn't equal the EHAM main departure runway combo box value, generate runway output with value from EHAM main landing runway combo box.
                    if (EHAMmainLandingRunwayCheckBox.Checked && !EHAMmainLandingRunwayComboBox.Text.Equals(EHAMmainDepartureRunwayComboBox.Text)) output += runwayToOutput("[mlrwy]", EHAMmainLandingRunwayComboBox);

                    //Else generate runway output with the value from the EHAM main landing runway combo box.
                    else output += runwayToOutput("[mrwy]", EHAMmainLandingRunwayComboBox);
                    #endregion

                    #region EHAM SECONDARY LANDING RUNWAY
                    //If the EHAM secondary landing runway check box is checked, generate runway output with the value from the EHAM secondary landing runway combo box.
                    if (EHAMsecondaryLandingRunwayCheckBox.Checked) output += runwayToOutput("[slrwy]", EHAMsecondaryLandingRunwayComboBox);
                    #endregion

                    #region EHAM MAIN DEPARTURE RUNWAY
                    //If the EHAM main departure runway check box is checked, generate runway output with the value from the EHAM main departure runway combo box.
                    if (EHAMmainDepartureRunwayCheckBox.Checked && !EHAMmainLandingRunwayComboBox.Text.Equals(EHAMmainDepartureRunwayComboBox.Text)) output += runwayToOutput("[mtrwy]", EHAMmainDepartureRunwayComboBox);
                    #endregion

                    #region EHAM SECONDARY DEPARTURE RUNWAY
                    //If the EHAM secondary departure runway check box is checked, generate runway output with the value from the EHAM secondary departure runway combo box.
                    if (EHAMsecondaryDepartureRunwayCheckBox.Checked) output += runwayToOutput("[strwy]", EHAMsecondaryDepartureRunwayComboBox);
                    #endregion
                    break;
                #endregion

                #region EHBK
                //If selected ICAO tab is EHBK.
                case "EHBK":
                    //If EHBK main runway check box is checked, generate runway output with value from EHBK main runway combo box.
                    if (EHBKmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHBKmainRunwayComboBox);
                    break;
                #endregion

                #region EHEH
                //If selected ICAO tab is EHEH.
                case "EHEH":
                    //If EHEH main runway check box is checked, generate runway output with value from EHEH main runway combo box.
                    if (EHEHmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHEHmainRunwayComboBox);
                    break;
                #endregion

                #region EHGG
                //If selected ICAO tab is EHGG.
                case "EHGG":
                    //If EHGG main runway check box is checked, generate runway output with value from EHGG main runway combo box.
                    if (EHGGmainRunwayCheckBox.Checked) output += runwayToOutput("[mrwy]", EHGGmainRunwayComboBox);
                    break;
                #endregion

                #region EHRD
                //If selected ICAO tab is EHRD.
                case "EHRD":
                    //If EHRD main runway check box is checked, generate runway output with value from EHRD main runway combo box.
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
            //If the ICAO code of the processed METAR doesn't equals the ICAO of the selected ICAO tab.
            if (!metarProcessor.metar.ICAO.Equals(ICAOTabControl.SelectedTab.Name))
            {
                //Show warning message.
                MessageBox.Show("Selected ICAO tab does not match the ICAO of the processed METAR.", "Error"); return;
            }

            //Check runways selected.
            if (!checkRunwaySelected()) return;
            
            String output = String.Empty;

            #region ICAO
            //Generate output from processed METAR ICAO.
            switch (metarProcessor.metar.ICAO)
            {
                //If processed ICAO is EHAM.
                case "EHAM":
                    output += "[ehamatis]";
                    break;

                //If processed ICAO is EHBK.
                case "EHBK":
                    output += "[ehbkatis]";
                    break;

                //If processed ICAO is EHEH.
                case "EHEH":
                    output += "[ehehatis]";
                    break;

                //If processed ICAO is EHGG.
                case "EHGG":
                    output += "[ehggatis]";
                    break;

                //If processed ICAO is EHRD.
                case "EHRD":
                    output += "[ehrdatis]";
                    break;
            }
            #endregion

            #region ATIS LETTER
            //Add ATIS letter to output
            output += phoneticAlphabet[atisIndex];
            output += "[pause]";
            #endregion

            #region RUNWAYS
            //Add runway output to output.
            output += generateRunwayOutput();
            #endregion

            #region TL
            //Add transition level to output.
            output += "[trl]";
            //Calculate and add transition level to output.
            output += calculateTransitionLevel();
            #endregion

            #region OPERATIONAL REPORTS
            //Generate and add operational report to output.
            output += operationalReportToOutput();
            #endregion

            output += "[pause]";

            #region WIND
            //If processed METAR has wind, generate and add wind output to output. 
            if (addWindRecordCheckBox.Checked) output += "[wind]";
            if (metarProcessor.metar.Wind != null) output += windToOutput(metarProcessor.metar.Wind);
            #endregion

            #region CAVOK
            //If processed METAR has CAVOK, add CAVOK to output.
            if (metarProcessor.metar.CAVOK) output += "[cavok]";
            #endregion

            #region VISIBILITY
            //If processed METAR has a visibility greater than 0, generate and add visibility output to output. 
            if (metarProcessor.metar.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.Visibility);
            #endregion

            #region RVRONATC
            //If processed METAR has RVR, add RVR to output. 
            if (metarProcessor.metar.RVR) output += "[rvronatc]";
            #endregion

            #region PHENOMENA
            //Generate and add weather phenomena to output.
            output += listToOutput(metarProcessor.metar.Phenomena);
            #endregion

            #region CLOUDS OPTIONS
            //If processed METAR has SKC, add SKC to output. 
            if (metarProcessor.metar.SKC) output += "[skc]";
            //If processed METAR has NSC, add NSC to output. 
            if (metarProcessor.metar.NSC) output += "[nsc]";
            #endregion

            #region CLOUDS
            //Generate and add weather clouds to output. 
            output += listToOutput(metarProcessor.metar.Clouds);
            #endregion

            #region VERTICAL VISIBILITY
            //If processed METAR has a vertical visibility greater than 0, add vertical visibility to output.
            if (metarProcessor.metar.VerticalVisibility > 0) output += "[vv]" + metarProcessor.metar.VerticalVisibility + "[hundred][meters]";
            #endregion

            #region TEMPERATURE
            //Add temperature to output.
            output += "[temp]";
            //If processed METAR has a minus temperature.
            if (metarProcessor.metar.Temperature.StartsWith("M")) output += "[minus]" + Convert.ToInt32(metarProcessor.metar.Temperature.ToString().Substring(1, 2));

            //Positive temperature.
            else output += Convert.ToInt32(metarProcessor.metar.Temperature.ToString());
            #endregion

            #region DEWPOINT
            //Add dewpoint to output.
            output += "[dp]";
            //If processed METAR has a minus dewpoint.
            if (metarProcessor.metar.Dewpoint.StartsWith("M")) output += "[minus]" + Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString().Substring(1, 2));
            
            //Positive dewpoint.
            else output += Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString());
            #endregion

            #region QNH
            //Add QNH to output.
            output += "[qnh]";
            output += metarProcessor.metar.QNH.ToString();
            output += "[hpa]";
            #endregion

            #region NOSIG
            //If processed METAR has NOSIG, add NOSIG to output.
            if (metarProcessor.metar.NOSIG) output += "[nosig]";
            #endregion

            #region TEMPO
            //If processed METAR has a TEMPO trend.
            if (metarProcessor.metar.metarTEMPO != null)
            {
                //Add TEMPO to output.
                output += "[tempo]";

                #region TEMPO WIND
                //If processed TEMPO trend has wind, generate and add wind output to output. 
                if (metarProcessor.metar.metarTEMPO.Wind != null) output += windToOutput(metarProcessor.metar.metarTEMPO.Wind);
                #endregion

                #region TEMPO CAVOK
                //If processed TEMPO trend has CAVOK, add CAVOK to output.
                if (metarProcessor.metar.metarTEMPO.CAVOK) output += "[cavok]";
                #endregion

                #region TEMPO VISIBILITY
                //If processed TEMPO trend has a visibility greater than 0, generate and add visibility output to output. 
                if (metarProcessor.metar.metarTEMPO.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.metarTEMPO.Visibility);
                #endregion

                #region TEMPO PHENOMENA
                //If TEMPO trend has 1 or more weather phenomena, generate and add TEMPO trend weather phenomena to output.
                if (metarProcessor.metar.metarTEMPO.Phenomena.Count > 0) output += listToOutput(metarProcessor.metar.metarTEMPO.Phenomena);
                #endregion

                #region TEMPO SKC
                //If TEMPO trend has SKC, add SKC to output. 
                if (metarProcessor.metar.metarTEMPO.SKC) output += "[skc]";
                #endregion

                #region TEMPO NSW
                //If TEMPO trend has NSW, add NSW to output. 
                if (metarProcessor.metar.metarTEMPO.NSW) output += "[nsw]";
                #endregion

                #region TEMPO CLOUDS
                //If TEMPO trend has 1 or more weather clouds, generate and add TEMPO weather clouds to output. 
                if (metarProcessor.metar.metarTEMPO.Clouds.Count > 0) output += listToOutput(metarProcessor.metar.metarTEMPO.Clouds);
                #endregion

                #region TEMPO VERTICAL VISIBILITY
                //If TEMPO trend has a vertical visibility greater than 0, add TEMPO trend vertical visibility to output.
                if (metarProcessor.metar.metarTEMPO.VerticalVisibility > 0) output += "[vv]" + metarProcessor.metar.metarTEMPO.VerticalVisibility + "[hundred][meters]";
                #endregion
            }
            #endregion

            #region BECMG
            //If processed METAR has e BECMG trend.
            if (metarProcessor.metar.metarBECMG != null)
            {
                //Add BECMG to output.
                output += "[becmg]";

                #region BECMG WIND
                //If processed BECMG trend has wind, generate and add wind output to output.
                if (metarProcessor.metar.metarBECMG.Wind != null) output += windToOutput(metarProcessor.metar.metarBECMG.Wind);
                #endregion

                #region BECMG CAVOK
                //If processed BECMG trend has CAVOK, add CAVOK to output.
                if (metarProcessor.metar.metarBECMG.CAVOK) output += "[cavok]";
                #endregion

                #region BECMG VISIBILITY
                //If processed BECMG trend has a visibility greater than 0, generate and add visibility output to output. 
                if (metarProcessor.metar.metarBECMG.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.metarBECMG.Visibility);
                #endregion

                #region BECMG PHENOMENA
                //If BECMG trend has 1 or more weather phenomena, generate and add BECMG trend weather phenomena to output.
                if (metarProcessor.metar.metarBECMG.Phenomena.Count > 0) output += listToOutput(metarProcessor.metar.metarBECMG.Phenomena);
                #endregion

                #region BECMG SKC
                //If BECMG trend has SKC, add SKC to output. 
                if (metarProcessor.metar.metarBECMG.SKC) output += "[skc]";
                #endregion

                #region BECMG NSW
                //If BECMG trend has NSW, add NSW to output. 
                if (metarProcessor.metar.metarBECMG.NSW) output += "[nsw]";
                #endregion

                #region BECMG CLOUDS
                //If BECMG trend has 1 or more weather clouds, generate and add BECMG weather clouds to output. 
                if (metarProcessor.metar.metarBECMG.Clouds.Count > 0) output += listToOutput(metarProcessor.metar.metarBECMG.Clouds);
                #endregion

                #region BECMG VERTICAL VISIBILITY
                //If BECMG trend has a vertical visibility greater than 0, add BECMG trend vertical visibility to output.
                if (metarProcessor.metar.metarBECMG.VerticalVisibility > 0) output += "[vv]" + metarProcessor.metar.metarBECMG.VerticalVisibility + "[hundred][meters]";
                #endregion
            }
            #endregion

            #region OPTIONAL
            //If inverted surface temperature check box is checked.
            if (markTempCheckBox.Checked) output += "[marktemp]";
            //If arrival only check box is checked.
            if (arrOnlyCheckBox.Checked) output += "[call1]";
            //If approach only check box is checked.
            if (appOnlyCheckBox.Checked) output += "[call2]";
            //If arrival and approach only check box is checked.
            if (appArrOnlyCheckBox.Checked) output += "[call3]";
            #endregion

            #region END
            //Add end to output.
            output += "[end]";
            output += phoneticAlphabet[atisIndex];
            #endregion

            //If copy output check box is checked, copy ATIS output to clipboard.
            if (copyOutputCheckBox.Checked) Clipboard.SetText(output);

            //Set generated ATIS output in output text box.
            outputTextBox.Text = output;
        }

        /// <summary>
        /// Method called when exit tool strip item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit application.
            Application.Exit();
        }

        /// <summary>
        /// Method called when ICAO tab is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ICAOTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set ICAO of selected ICAO tab in ICAO text box.
            if (ICAOTabControl.SelectedTab.Name.Equals("EHAM"))
            {
                icaoTextBox.Text = "EHAM";
                realEHAMRunwaysCheckBox.Checked = realEHAMRunwaysCheckBox.Visible = true;
                selectBestRunwayCheckBox.Visible = false;
            }
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHBK"))
            {
                icaoTextBox.Text = "EHBK";
                realEHAMRunwaysCheckBox.Visible = false;
                selectBestRunwayCheckBox.Visible = selectBestRunwayCheckBox.Checked = true;
            }
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHEH"))
            {
                icaoTextBox.Text = "EHEH";
                realEHAMRunwaysCheckBox.Visible = false;
                selectBestRunwayCheckBox.Visible = selectBestRunwayCheckBox.Checked = true;
            }
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHGG"))
            {
                icaoTextBox.Text = "EHGG";
                realEHAMRunwaysCheckBox.Visible = false;
                selectBestRunwayCheckBox.Visible = selectBestRunwayCheckBox.Checked = true;
            }
            else
            {
                icaoTextBox.Text = "EHRD";
                realEHAMRunwaysCheckBox.Visible = false;
                selectBestRunwayCheckBox.Visible = selectBestRunwayCheckBox.Checked = true;
            }             
        }

        /// <summary>
        /// Method called when the about tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize new About form.
            Form aboutForm = new About();

            //Show about form.
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// Method called of METAR text box text changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void metarTextBox_TextChanged(object sender, EventArgs e)
        {
            //If METAR text box contains text, enable process METAR button.
            if (metarTextBox.Text.Trim().Equals(String.Empty)) processMetarButton.Enabled = false;

            //If METAR text box doesn't contain text, disable process METAR button.
            else processMetarButton.Enabled = true;
        }

        /// <summary>
        /// Method called of runway info button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void runwayInfoButton_Click(object sender, EventArgs e)
        {
            //If runway info form doesn't exists OR isn't visible.
            if (runwayInfo == null || !runwayInfo.Visible)
            {
                runwayInfo = new RunwayInfo(this, metarProcessor.metar);

                //Initialize new RunwayInfo form.
                runwayInfoButton.Text = "<";

                //Show runway info form.
                runwayInfo.Show();

                //Set runway info position relative to this.
                runwayInfo.showRelativeToDutchVACCATISGenerator(this);

                //Inverse runway info state boolean.
                runwayInfoState = !runwayInfoState;
            }
            //If runway info is opened.
            else
            {
                runwayInfoButton.Text = ">";

                //Hide runway info form.
                runwayInfo.Visible = false;

                //Inverse runway info state boolean.
                runwayInfoState = !runwayInfoState;
            }
        }

        /// <summary>
        /// Method called if DutchVACCATISGenerator form is resized (e.g. minimize, maximize, resize (duh!))
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void DutchVACCATISGenerator_Resize(object sender, EventArgs e)
        {
            //If form is restored to normal window state.
            if (WindowState == FormWindowState.Normal)
            {
                //Show runwayInfo form.
                if (runwayInfo != null && runwayInfoState) runwayInfo.Visible = true;
                //Show sound form.
                if (sound != null && soundState) sound.Visible = true;
            }

            //If form is minimized.
            if (WindowState == FormWindowState.Minimized)
            {
                //Hide runwayInfo form.
                if (runwayInfo != null) runwayInfo.Visible = false;
                //Hide sound form.
                if (sound != null) sound.Visible = false;
            }
        }

        /// <summary>
        /// Method called if sound button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void soundButton_Click(object sender, EventArgs e)
        {
            //If sound form doesn't exists OR isn't visible.
            if (sound == null || !sound.Visible)
            {
                //Create new Sound form.
                sound = new Sound(this);
                soundButton.Text = "▲";
                sound.Show();
                sound.showRelativeToDutchVACCATISGenerator(this);

                //Inverse sound state boolean.
                soundState = !soundState;
            }
            else
            {
                soundButton.Text = "▼";
                
                //Hide the sound form.
                sound.Visible = false;

                //Stop the wavePlayer.
                if (sound.wavePlayer != null) sound.wavePlayer.Stop();

                //Inverse sound state boolean.
                soundState = !soundState;
            }
        }

        /// <summary>
        /// Method called if output text box text changes.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {
            //If sound form exists.
            if(sound != null)
            {
                //If wavePlayer is NULL OR wavePlayer is not playing.
                if (sound.wavePlayer == null || !sound.wavePlayer.PlaybackState.Equals(NAudio.Wave.PlaybackState.Playing))
                {
                    //If output text box text is not an empty string, enable build ATIS button.
                    if (!outputTextBox.Text.Trim().Equals(String.Empty)) sound.buildATISButton.Enabled = true;
                    //Else disable the build ATIS button (nothing to build!).
                    else sound.buildATISButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Method called when version background workers is started. Pulls latest version number from my site.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void versionBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {   
            try
            {
                //Request latest version.
                WebRequest request = WebRequest.Create("http://daanbroekhuizen.com/Dutch%20VACC/Dutch%20VACC%20ATIS%20Generator/Version/version.php");
                WebResponse response = request.GetResponse();

                //Read latest version.
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                latestVersion = reader.ReadToEnd();

                //Trim latest version string.
                latestVersion.Trim();
            }
            catch (WebException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Method called when version background worker has completed its task. Compares executable version with pulled latest version and gives a message if a newer version is available.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void versionBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //If a latest version has been pulled.
            if(latestVersion != null && !latestVersion.Equals(String.Empty))
            {
                //If a newer version is available.
                if(!latestVersion.Equals(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Trim()))
                {
                    while(latestVersion.Contains("."))
                    {
                        latestVersion = latestVersion.Remove(latestVersion.IndexOf("."), 1);
                    }

                    string applicationVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Trim();

                    while (applicationVersion.Contains("."))
                    {
                        applicationVersion = applicationVersion.Remove(applicationVersion.IndexOf("."), 1);
                    }

                    //UNCOMMENT
                    if (Convert.ToInt32(latestVersion) > Convert.ToInt32(applicationVersion))
                    {
                        if (MessageBox.Show("Newer version is available.\nCheck the Dutch VACC site for more information.\nDownload latest version?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes) System.Diagnostics.Process.Start("http://www.dutchvacc.nl");
                    }
                }
            }
        }

        /// <summary>
        /// Method called when real runway background workers is started. Gets the real EHAM runway configuration.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void realRunwayBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Initialize runway list to get rid of previous stored runways.
            departureRunways = new List<String>();
            landingRunways = new List<String>();

            try
            {
                //Make web request to http://www.lvnl.nl/nl/airtraffic.
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.lvnl.nl/nl/airtraffic");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //If HTTP status is 200.
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //Set receive stream.
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    //If no character set found, initialize stream without character set.
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    //Initialize stream with character set.
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    //Read stream.
                    string data = readStream.ReadToEnd();

                    //Close stream.
                    response.Close();
                    readStream.Close();

                    #region OLD SPLIT DATA
                    //data = data.Substring(data.IndexOf("<div class=\"runwaywrapper\">"), data.Length - data.IndexOf("<div class=\"runwaywrapper\">"));

                    //data = data.Substring(0, data.LastIndexOf("</div>") + "</div>".Length);

                    //data = data.Substring(data.IndexOf("<div class=\"runwaywrapper\">") + "<div class=\"runwaywrapper\">".Length, data.Length - (data.IndexOf("<div class=\"runwaywrapper\">") + "<div class=\"runwaywrapper\">".Length)).Trim();

                    //data = data.Substring(data.IndexOf("<div id=\"runway\" class=\"show\">") + "<div id=\"runway\" class=\"show\">".Length, data.Length - (data.IndexOf("<div id=\"runway\" class=\"show\">") + "<div id=\"runway\" class=\"show\">".Length)).Trim();

                    //data = data.Substring(0, data.LastIndexOf("</div>"));

                    //data = data.Substring(0, data.LastIndexOf("</div>"));
                    #endregion

                    //If received data contains HTML <li> tag.
                    while (data.Contains("<li"))
                    {
                        //Get <li>...</lI>
                        String runwayListItem = data.Substring(data.IndexOf("<li"), (data.IndexOf("</li>") + "</li>".Length) - data.IndexOf("<li"));

                        //If found list item is landing runway.
                        if (runwayListItem.Contains("class=\"lb"))
                        {
                            runwayListItem = runwayListItem.Substring(runwayListItem.IndexOf("class=\"lb") + "class=\"lb".Length, runwayListItem.Length - (runwayListItem.IndexOf("class=\"lb") + "class=\"lb".Length));
                            landingRunways.Add(runwayListItem.Substring(0, runwayListItem.IndexOf("\">")));
                        }
                        //If found list item is departure runway.
                        else if (runwayListItem.Contains("class=\"sb"))
                        {
                            runwayListItem = runwayListItem.Substring(runwayListItem.IndexOf("class=\"sb") + "class=\"sb".Length, runwayListItem.Length - (runwayListItem.IndexOf("class=\"sb") + "class=\"sb".Length));
                            departureRunways.Add(runwayListItem = runwayListItem.Substring(0, runwayListItem.IndexOf("\">")));
                        }

                        //Remove list item from received data.
                        data = data.Substring(data.IndexOf("</li>") + "</li>".Length, (data.Length - (data.IndexOf("</li>") + "</li>".Length)));
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Unable to get real EHAM runway combination from the Internet.", "Error");
            }
        }

        /// <summary>
        /// Method called when real runway background worker is completed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void realRunwayBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Only one departure runway found.
            if (departureRunways.Count == 1)
                EHAMmainDepartureRunwayComboBox.Text = departureRunways.First();

            //Only one landing runway found.
            if (landingRunways.Count == 1)
                EHAMmainLandingRunwayComboBox.Text = landingRunways.First();


            //Two or more landing or departure runways found.
            if (landingRunways.Count > 1 || departureRunways.Count > 1)
                processMultipleRunways();

            //De-select get EHAM runways combo box.
            realEHAMRunwaysCheckBox.Checked = false;

            //UNCOMMENT
            MessageBox.Show("Controller notice! Verify auto selected runway(s).", "Warning");
        }

        /// <summary>
        /// Process if multiple runways we're found and set the runway selection boxes with the founded values.
        /// </summary>
        private void processMultipleRunways()
        {
            //If there are more than two landing runways.
            if (landingRunways.Count > 1)
            {
                String firstRunway = landingRunways.First();
                String secondRunway = landingRunways.Last();

                //Generate landing runway combinations list.
                List<Tuple<String, String>> landingRunwayCombinations = new List<Tuple<String, String>>()
                {
                    { new Tuple<String, String>("18R", "18C")},
                    { new Tuple<String, String>("18R", "27")},
                    { new Tuple<String, String>("18R", "22")},
                    { new Tuple<String, String>("18C", "27")},
                    { new Tuple<String, String>("18C", "22")},
                    { new Tuple<String, String>("36R", "36C")},
                    { new Tuple<String, String>("36R", "27")},
                    { new Tuple<String, String>("06", "36R")},
                    { new Tuple<String, String>("06", "04")},
                    { new Tuple<String, String>("27", "22")},
                    { new Tuple<String, String>("36C", "04")},
                    { new Tuple<String, String>("36C", "27")},
                };

                //Check which runways are found and set the correct main and secondary landing runway.
                foreach (Tuple<String, String> runwayCombination in landingRunwayCombinations)
                {
                    if ((firstRunway.Equals(runwayCombination.Item1) && secondRunway.Equals(runwayCombination.Item2)) || (firstRunway.Equals(runwayCombination.Item2) && secondRunway.Equals(runwayCombination.Item1)))
                    {
                        EHAMmainLandingRunwayComboBox.Text = runwayCombination.Item1;
                        EHAMsecondaryLandingRunwayComboBox.Text = runwayCombination.Item2;
                    }
                }
            }

            //If there are more than two departure runways found.
            if(departureRunways.Count > 1)
            {
                String firstRunway = departureRunways.First();
                String secondRunway = departureRunways.Last();

                //Generate departure runway combinations list.
                List<Tuple<String, String>> departureRunwayCombinations = new List<Tuple<String, String>>()
                {
                    { new Tuple<String, String>("24", "18L")},
                    { new Tuple<String, String>("24", "36L")},
                    { new Tuple<String, String>("24", "36C")},
                    { new Tuple<String, String>("24", "09")},
                    { new Tuple<String, String>("36L", "36C")},
                    { new Tuple<String, String>("36L", "09")},                    
                    { new Tuple<String, String>("36L", "04")},        
                    { new Tuple<String, String>("36L", "06")},        
                    { new Tuple<String, String>("36C", "09")},
                    { new Tuple<String, String>("36C", "04")},
                    { new Tuple<String, String>("36C", "06")},
                    { new Tuple<String, String>("18L", "18C")},
                    { new Tuple<String, String>("18C", "09")},
                };

                //Check which runways are found and set the correct main and secondary departure runway.
                foreach (Tuple<String, String> runwayCombination in departureRunwayCombinations)
                {
                    if ((firstRunway.Equals(runwayCombination.Item1) && secondRunway.Equals(runwayCombination.Item2)) || (firstRunway.Equals(runwayCombination.Item2) && secondRunway.Equals(runwayCombination.Item1)))
                    {
                        EHAMmainDepartureRunwayComboBox.Text = runwayCombination.Item1;
                        EHAMsecondaryDepartureRunwayComboBox.Text = runwayCombination.Item2;
                    }
                }
            }
        }
    }
}
