using DutchVACCATISGenerator.Forms;
using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    public partial class DutchVACCATISGenerator : Form
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly IFormOpenerHelper formOpenerHelper;
        private readonly ISoundLogic soundLogic;

        private int atisIndex { get; set; }
        private List<String> departureRunways { get; set; }
        private Boolean icaoTabSwitched { get; set; }
        private List<String> landingRunways { get; set; }
        private String latestVersion { get; set; }
        private String metar { get; set; }
        private MetarProcessor metarProcessor { get; set; }
        private List<String> phoneticAlphabet { get; set; }
        private Boolean randomLetter { get; set; }
        private RunwayInfo runwayInfo { get; set; }
        private Boolean runwayInfoState { get; set; }
        private DateTime timerEnabled { get; set; }
        private Boolean userLetterSelection { get; set; }

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>IntPtr - Handle of foreground window</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public DutchVACCATISGenerator(ApplicationVariables applicationVariables,
            IFormOpenerHelper formOpenerHelper,
            ISoundLogic soundLogic)
        {
            this.applicationVariables = applicationVariables;
            this.formOpenerHelper = formOpenerHelper;
            this.soundLogic = soundLogic;

            InitializeComponent();

            SetWindowBounds();

            //Register application events.
            ApplicationEvents.BuildAITSCompletedEvent += BuildAITSCompleted;
            ApplicationEvents.BuildAITSStartedEvent += BuildAITSStarted;
            ApplicationEvents.TerminalAerodromeForecastFormClosingEvent += TerminalAerodromeForecastFormClosing;

            //Load settings.
            loadSettings();

            //Set initial states of boolean.
            runwayInfoState = icaoTabSwitched = userLetterSelection = randomLetter = false;

            //Set phonetic alphabet.
            setPhoneticAlphabet();

            //Set ATIS index and label.
            randomizeATISLetter();

            //Start version background worker.
            versionBackgroundWorker.RunWorkerAsync();

            //Load EHAM METAR.
            metarBackgroundWorker.RunWorkerAsync(icaoTextBox.Text);

            //Check if temp directory exists, if so, delete it.
            if (Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp"))
            {
                //Check if setup.exe is still being used.
                if (!IsFileLocked(new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp" + "\\Dutch VACC ATIS Generator - Setup.exe")))
                {
                    //Remove hidden attribute.
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp");
                    directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

                    //Delete temp folder.
                    Directory.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp", true);
                }
            }

            //If auto load EHAM runways is selected.
            if (autoLoadEHAMRunwayToolStripMenuItem.Checked)
                realRunwayBackgroundWorker.RunWorkerAsync();

            //If auto generate ATIS is selected.
            if (autoGenerateATISToolStripMenuItem.Checked)
                autoGenerateATISBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Method called when get METAR button is clicked. Starts a background worker which pulls the METAR from the VATSIM METAR website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getMetarButton_Click(object sender, EventArgs e)
        {
            //Set new time to check to.
            timerEnabled = DateTime.UtcNow;

            //Get ICAO entered.
            String _ICAO = icaoTextBox.Text;

            //If no ICAO has been entered.
            if (_ICAO == String.Empty)
            {
                MessageBox.Show("Enter an ICAO code.", "Warning"); return;
            }

            //Disable the get METAR button so the user can't overload it.
            getMetarButton.Enabled = false;

            //Start METAR background worker to start pulling the METAR.
            if (!metarBackgroundWorker.IsBusy)
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

            //If auto process METAR check box is checked, automatically process the METAR.
            if (autoProcessMETARToolStripMenuItem.Checked && metar != null)
                processMetarButton_Click(null, null);

            //Re-enable the get METAR button.
            getMetarButton.Enabled = true;
        }

        /// <summary>
        /// Split METAR on split word.
        /// </summary>
        /// <param name="metar">METAR to split.</param>
        /// <param name="splitWord">Word to split on. (BECMG, TEMPO)</param>
        /// <returns>String array of split METAR</returns>
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

            //Checks if ATIS index has to be increased.
            if (!(userLetterSelection | randomLetter | icaoTabSwitched | (lastLabel.Text == string.Empty)))
            {
                if (atisIndex == phoneticAlphabet.Count - 1)
                    atisIndex = 0;
                else
                    atisIndex++;
            }

            //Set all letter booleans to false for next generation.
            randomLetter = userLetterSelection = icaoTabSwitched = false;

            //Set processed METAR in last processed METAR label.
            if (metar.Length > 140)
                lastLabel.Text = "Last successful processed METAR:\n" + metar.Substring(0, 69).Trim() + "\n" + metar.Substring(69, 69).Trim() + "...";
            else if (metar.Length > 69)
                lastLabel.Text = "Last successful processed METAR:\n" + metar.Substring(0, 69).Trim() + "\n" + metar.Substring(69).Trim();
            else
                lastLabel.Text = "Last successful processed METAR:\n" + metar;

            //Set ATIS letter in ATIS letter label.
            atisLetterLabel.Text = phoneticAlphabet[atisIndex];

            //Enable generate ATIS and runway info button.
            generateATISButton.Enabled = true;
            runwayInfoButton.Enabled = runwayInfoToolStripMenuItem.Enabled = true;

            //If runwayInfo is null, create RunwayInfo form.
            if ((runwayInfo != null && runwayInfo.IsDisposed) || runwayInfo == null) runwayInfo = new RunwayInfo(this, metarProcessor.metar);
            else
            {
                //Update runway info form.
                runwayInfo.metar = metarProcessor.metar;
                runwayInfo.setVisibleRunwayInfoDataGrid(ICAOTabControl.SelectedTab.Text);
                runwayInfo.checkICAOTabSelected();
            }

            //If processed METAR equals the selected ICAO.
            if (runwayInfo.metar.ICAO.Equals(ICAOTabControl.SelectedTab.Name))
                getSelectBestRunwayButton.Enabled = true;
        }

        /// <summary>
        /// Method called when previous arrow (ATIS letter) is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void previousATISLetterButton_Click(object sender, EventArgs e)
        {
            //Set user letter selection boolean true.
            userLetterSelection = true;

            if (atisIndex == 0) atisIndex = phoneticAlphabet.Count - 1;
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
            //Set user letter selection boolean true.
            userLetterSelection = true;

            if (atisIndex == phoneticAlphabet.Count - 1) atisIndex = 0;
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
        /// <returns>Runway identifier ATIS output</returns>
        private String getRunwayMarker(String input)
        {
            switch (input)
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
            }

            return String.Empty;
        }

        /// <summary>
        /// Calculate transition level from QNH and temperature.
        /// </summary>
        /// <returns>Calculated TL</returns>
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
        /// <returns>String of runway output</returns>
        private String runwayToOutput(String runway, ComboBox runwayComboBox)
        {
            String output = runway;
            output += " ";

            //Split runway digit identifier from runway identifier.
            String[] splitArray = Regex.Split(runwayComboBox.SelectedItem.ToString().Substring(0, 2), @"([0-9])");

            foreach (String digit in splitArray)
            {
                if (!string.IsNullOrEmpty(digit))
                    applicationVariables.ATISSamples.Add(digit);
            }

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
            applicationVariables.ATISSamples.Add("opr");
            String output = " OPERATIONAL REPORT";

            #region LOW LEVEL VISIBILITY
            //If visibility is not 0 or less than 1500 meter, add low visibility procedure phrase.
            if (metarProcessor.metar.Visibility != 0 && metarProcessor.metar.Visibility < 1500)
            {
                applicationVariables.ATISSamples.Add("lvp");
                output += " LOW VISIBILITY PROCEDURES IN PROGRESS";
            }
            #endregion

            #region RWY CONFIGURATIONS
            if (EHAMmainLandingRunwayCheckBox.Checked && EHAMsecondaryLandingRunwayCheckBox.Checked)
            {
                #region INDEPENDENT APPROACHES
                /* 18R & 18C */
                if (EHAMmainLandingRunwayComboBox.Text.Equals("18R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18C"))
                {
                    applicationVariables.ATISSamples.Add("independent");
                    output += " INDEPENDENT PARALLEL APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("18C") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18R"))
                {
                    applicationVariables.ATISSamples.Add("independent");
                    output += " INDEPENDENT PARALLEL APPROACHES IN PROGRESS";
                }
                /* 36R & 36C */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36C"))
                {
                    applicationVariables.ATISSamples.Add("independent");
                    output += " INDEPENDENT PARALLEL APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("36C") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36R"))
                {
                    applicationVariables.ATISSamples.Add("independent");
                    output += " INDEPENDENT PARALLEL APPROACHES IN PROGRESS";
                }
                #endregion
                #region CONVERGING APPROACHES
                /* 06 & 36R */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("06") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36R"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("06"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                /* 06 & 27 */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("06") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("06"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                /* 06 & 09 */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("06") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("09"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("09") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("06"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                /* 06 & 18C */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("06") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18C"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("18C") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("06"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                /* 27 & 18C */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("18C") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18C"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                /* 27 & 18R */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("18R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("18R"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                /* 27 & 36C */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36C"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("36C") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                /* 27 & 36R */
                else if (EHAMmainLandingRunwayComboBox.Text.Equals("27") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("36R"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }

                else if (EHAMmainLandingRunwayComboBox.Text.Equals("36R") && EHAMsecondaryLandingRunwayComboBox.Text.Equals("27"))
                {
                    applicationVariables.ATISSamples.Add("convapp");
                    output += " CONVERGING APPROACHES IN PROGRESS";
                }
                #endregion
            }
            #endregion

            #region CHECK FOR ADDING [AND]
            if (output.Contains("LOW VISIBILITY PROCEDURES IN PROGRESS") && (output.Contains("INDEPENDENT PARALLEL APPROACHES IN PROGRESS") || output.Contains("CONVERGING APPROACHES IN PROGRESS")))
            {
                if (output.Contains(" INDEPENDENT PARALLEL APPROACHES IN PROGRESS"))
                {
                    applicationVariables.ATISSamples.Insert(applicationVariables.ATISSamples.IndexOf("independent"), "and");
                    output = output.Insert(output.IndexOf("IND"), " AND ");
                }

                else
                {
                    applicationVariables.ATISSamples.Insert(applicationVariables.ATISSamples.IndexOf("convapp"), "and");
                    output = output.Insert(output.IndexOf("CON"), " AND");
                }
            }
            #endregion

            if (!output.Equals(" OPERATIONAL REPORT")) return output;
            else
            {
                applicationVariables.ATISSamples.Remove("opr");
                return "";
            }
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
                    if (metarPhenomena.hasIntensity)
                    {
                        applicationVariables.ATISSamples.Add("-");
                        output += " LIGHT";
                    }

                    //If phenomena is 4 character phenomena (BCFG | MIFG | SHRA | VCSH | VCTS).
                    if (metarPhenomena.phenomena.Equals("BCFG") || metarPhenomena.phenomena.Equals("MIFG") || metarPhenomena.phenomena.Equals("SHRA") || metarPhenomena.phenomena.Equals("VCSH") || metarPhenomena.phenomena.Equals("VCTS"))
                    {
                        switch (metarPhenomena.phenomena)
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
                    //If phenomena is multi-phenomena (count > 2).
                    else if (metarPhenomena.phenomena.Count() > 2)
                    {
                        int length = metarPhenomena.phenomena.Length;

                        if ((length % 2) == 0)
                        {
                            int index = 0;

                            while (index != length)
                            {
                                if (!(length - index == 2))
                                    output += phenomenaToFullSpelling(metarPhenomena.phenomena.Substring(index, 2));

                                else
                                    output += phenomenaToFullSpelling(metarPhenomena.phenomena.Substring(index));

                                index = index + 2;
                            }
                        }
                    }
                    //If phenomena is 2 char phenomena.
                    else output += phenomenaToFullSpelling(metarPhenomena.phenomena);

                    //If loop phenomena is not the last phenomena of the list, add [and].
                    if (metarPhenomena != (MetarPhenomena)Convert.ChangeType(input.Last(), typeof(MetarPhenomena)))
                    {
                        applicationVariables.ATISSamples.Add("and");
                        output += " [AND]";
                    }
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
                    output += cloudTypeToFullSpelling(metarCloud.cloudType);

                    //If cloud altitude equals ground level.
                    if (metarCloud.altitude == 0)
                    {
                        addIndividualDigitsToATISSamples(metarCloud.altitude.ToString());

                        output += " " + metarCloud.altitude;
                    }

                    //If cloud altitude is round ten-thousand (e.g. 10000 (100), 20000 (200), 30000 (300)).
                    else if (metarCloud.altitude % 100 == 0)
                    {
                        addIndividualDigitsToATISSamples(Math.Floor(Convert.ToDouble(metarCloud.altitude / 100)).ToString() + "0");
                        applicationVariables.ATISSamples.Add("thousand");

                        output += " " + Math.Floor(Convert.ToDouble(metarCloud.altitude / 100)).ToString() + "0" + " THOUSAND";
                    }

                    else
                    {
                        //If cloud altitude is greater than a ten-thousand (e.g. 12000 (120), 23500 (235), 45000 (450)).
                        if (metarCloud.altitude / 100 > 0)
                        {
                            addIndividualDigitsToATISSamples(Math.Floor(Convert.ToDouble(metarCloud.altitude / 100)).ToString());

                            output += " " + Math.Floor(Convert.ToDouble(metarCloud.altitude / 100)).ToString();

                            //If cloud altitude has a ten-thousand and hundred value (e.g. 10200 (102), 20800 (208), 40700 (407)).
                            if (metarCloud.altitude.ToString().Substring(1, 1).Equals("0"))
                            {
                                applicationVariables.ATISSamples.Add("0");
                                applicationVariables.ATISSamples.Add("thousand");
                                output += " 0 THOUSAND";
                            }
                        }

                        //If cloud altitude has a thousand (e.g. 2000 (020), 4000 (040), 5000 (050)).
                        if ((metarCloud.altitude / 10) % 10 > 0)
                        {
                            addIndividualDigitsToATISSamples(Math.Floor(Convert.ToDouble((metarCloud.altitude / 10) % 10)).ToString());
                            applicationVariables.ATISSamples.Add("thousand");

                            output += " " + Math.Floor(Convert.ToDouble((metarCloud.altitude / 10) % 10)) + " THOUSAND";
                        }

                        //If cloud altitude has a hundred (e.g. 200 (002), 400 (004), 500 (005)).
                        if (metarCloud.altitude % 10 > 0)
                        {
                            addIndividualDigitsToATISSamples(Convert.ToString(metarCloud.altitude % 10));
                            applicationVariables.ATISSamples.Add("hundred");

                            output += " " + metarCloud.altitude % 10 + " HUNDRED";
                        }
                    }

                    applicationVariables.ATISSamples.Add("ft");
                    output += " FEET";

                    //If cloud type has addition (e.g. CB, TCU).
                    if (metarCloud.addition != null) output += cloudAddiationToFullSpelling(metarCloud.addition);
                }
            }
            #endregion

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudType"></param>
        /// <returns></returns>
        private string phenomenaToFullSpelling(string cloudType)
        {
            switch (cloudType)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudType"></param>
        /// <returns></returns>
        private string cloudTypeToFullSpelling(string cloudType)
        {
            switch (cloudType)
            {
                case "FEW":
                    applicationVariables.ATISSamples.Add("few");
                    return " FEW";

                case "BKN":
                    applicationVariables.ATISSamples.Add("bkn");
                    return " BROKEN";

                case "OVC":
                    applicationVariables.ATISSamples.Add("ovc");
                    return " OVERCAST";

                case "SCT":
                    applicationVariables.ATISSamples.Add("sct");
                    return " SCATTERED";
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addition"></param>
        /// <returns></returns>
        private string cloudAddiationToFullSpelling(string addition)
        {
            switch (addition)
            {
                case "CB":
                    applicationVariables.ATISSamples.Add("cb");
                    return " CUMULONIMBUS";

                case "TCU":
                    applicationVariables.ATISSamples.Add("tcu");
                    return " TOWERING CUMULONIMBUS";
            }

            return string.Empty;
        }

        /// <summary>
        /// Generate visibility output.
        /// </summary>
        /// <param name="input">Integer</param>
        /// <returns>String output</returns>
        private String visibilityToOutput(int input)
        {
            applicationVariables.ATISSamples.Add("vis");
            String output = " VISIBILITY";

            //If visibility is lower than 800 meters (less than 800 meters phrase).
            if (input < 800)
            {
                applicationVariables.ATISSamples.Add("<");
                addIndividualDigitsToATISSamples("800");
                applicationVariables.ATISSamples.Add("meters");

                output += " LESS THAN 8 HUNDRED METERS";
            }
            //If visibility is lower than 1000 meters (add hundred).
            else if (input < 1000)
            {
                addIndividualDigitsToATISSamples(Convert.ToString(input / 100));
                applicationVariables.ATISSamples.Add("hundred");
                applicationVariables.ATISSamples.Add("meters");

                output += " " + Convert.ToString(input / 100) + " HUNDRED METERS";
            }
            //If visibility is lower than 5000 meters and visibility is not a thousand number.
            else if (input < 5000 && (input % 1000) != 0)
            {
                addIndividualDigitsToATISSamples(Convert.ToString(input / 1000));
                applicationVariables.ATISSamples.Add("thousand");
                addIndividualDigitsToATISSamples(Convert.ToString((input % 1000) / 100));
                applicationVariables.ATISSamples.Add("hundred");
                applicationVariables.ATISSamples.Add("meters");

                output += " " + Convert.ToString(input / 1000) + " THOUSAND " + Convert.ToString((input % 1000) / 100) + " HUNDRED METERS";
            }
            //If visibility is >= 9999 (10 km phrase).
            else if (input >= 9999)
            {
                addIndividualDigitsToATISSamples("10");
                applicationVariables.ATISSamples.Add("km");

                output += " 10 KILOMETERS";
            }
            //If visibility is thousand.
            else
            {
                addIndividualDigitsToATISSamples(Convert.ToString(input / 1000));
                applicationVariables.ATISSamples.Add("km");

                output += " " + Convert.ToString(input / 1000) + " KILOMETERS";
            }

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
            if (input.VRB)
            {
                #region ADD SAMPLES TO ATISSAMPLES
                applicationVariables.ATISSamples.Add("vrb");

                addIndividualDigitsToATISSamples(input.windKnots);

                applicationVariables.ATISSamples.Add("kt");
                #endregion

                output += " VARIABLE " + input.windKnots + " KNOTS";
            }
            //If MetarWind has a gusting wind.
            else if (input.windGustMin != null)
            {
                #region ADD SAMPLES TO ATISSAMPLES
                addIndividualDigitsToATISSamples(input.windHeading);

                applicationVariables.ATISSamples.Add("deg");

                addIndividualDigitsToATISSamples(input.windGustMin);

                applicationVariables.ATISSamples.Add("max");

                addIndividualDigitsToATISSamples(input.windGustMax);

                applicationVariables.ATISSamples.Add("kt");
                #endregion

                output += " " + input.windHeading + " DEGREES" + input.windGustMin + " MAXIMUM " + input.windGustMax + " KNOTS";
            }
            //If MetarWind has a normal wind.
            else
            {
                #region ADD SAMPLES TO ATISSAMPLES
                addIndividualDigitsToATISSamples(input.windHeading);

                applicationVariables.ATISSamples.Add("deg");

                addIndividualDigitsToATISSamples(input.windKnots);

                applicationVariables.ATISSamples.Add("kt");
                #endregion

                output += " " + input.windHeading + " DEGREES " + input.windKnots + " KNOTS";
            }

            /*Variable wind*/
            if (input.windVariableLeft != null)
            {
                #region ADD SAMPLES TO ATISSAMPLES
                applicationVariables.ATISSamples.Add("vrbbtn");

                addIndividualDigitsToATISSamples(input.windVariableLeft);

                applicationVariables.ATISSamples.Add("and");

                addIndividualDigitsToATISSamples(input.windVariableRight);

                applicationVariables.ATISSamples.Add("deg");
                #endregion

                output += " VARIABLE BETWEEN " + input.windVariableLeft + " AND " + input.windVariableRight + " DEGREES";
            }

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
                    if (EHAMmainLandingRunwayCheckBox.Checked && !EHAMmainLandingRunwayComboBox.Text.Equals(EHAMmainDepartureRunwayComboBox.Text))
                    {
                        applicationVariables.ATISSamples.Add("mlrwy");
                        output += runwayToOutput(" MAIN LANDING RUNWAY", EHAMmainLandingRunwayComboBox);
                    }
                    //Else generate runway output with the value from the EHAM main landing runway combo box.
                    else
                    {
                        applicationVariables.ATISSamples.Add("mlrwy");
                        output += runwayToOutput(" MAIN LANDING RUNWAY", EHAMmainLandingRunwayComboBox);
                    }
                    #endregion

                    #region EHAM SECONDARY LANDING RUNWAY
                    //If the EHAM secondary landing runway check box is checked, generate runway output with the value from the EHAM secondary landing runway combo box.
                    if (EHAMsecondaryLandingRunwayCheckBox.Checked)
                    {
                        applicationVariables.ATISSamples.Add("slrwy");
                        output += runwayToOutput(" SECONDARY LANDING RUNWAY", EHAMsecondaryLandingRunwayComboBox);
                    }
                    #endregion

                    #region EHAM MAIN DEPARTURE RUNWAY
                    //If the EHAM main departure runway check box is checked, generate runway output with the value from the EHAM main departure runway combo box.
                    if (EHAMmainDepartureRunwayCheckBox.Checked && !EHAMmainLandingRunwayComboBox.Text.Equals(EHAMmainDepartureRunwayComboBox.Text))
                    {
                        applicationVariables.ATISSamples.Add("mtrwy");
                        output += runwayToOutput(" MAIN TAKEOFF RUNWAY", EHAMmainDepartureRunwayComboBox);
                    }
                    #endregion

                    #region EHAM SECONDARY DEPARTURE RUNWAY
                    //If the EHAM secondary departure runway check box is checked, generate runway output with the value from the EHAM secondary departure runway combo box.
                    if (EHAMsecondaryDepartureRunwayCheckBox.Checked)
                    {
                        applicationVariables.ATISSamples.Add("strwy");
                        output += runwayToOutput(" SECONDARY TAKEOFF RUNWAY", EHAMsecondaryDepartureRunwayComboBox);
                    }
                    #endregion
                    break;
                #endregion

                #region EHBK
                //If selected ICAO tab is EHBK.
                case "EHBK":
                    //If EHBK main runway check box is checked, generate runway output with value from EHBK main runway combo box.
                    if (EHBKmainRunwayCheckBox.Checked)
                    {
                        applicationVariables.ATISSamples.Add("mlrwy");
                        output += runwayToOutput(" MAIN LANDING RUNWAY", EHBKmainRunwayComboBox);
                    }
                    break;
                #endregion

                #region EHEH
                //If selected ICAO tab is EHEH.
                case "EHEH":
                    //If EHEH main runway check box is checked, generate runway output with value from EHEH main runway combo box.
                    if (EHEHmainRunwayCheckBox.Checked)
                    {
                        applicationVariables.ATISSamples.Add("mlrwy");
                        output += runwayToOutput(" MAIN LANDING RUNWAY", EHEHmainRunwayComboBox);
                    }
                    break;
                #endregion

                #region EHGG
                //If selected ICAO tab is EHGG.
                case "EHGG":
                    //If EHGG main runway check box is checked, generate runway output with value from EHGG main runway combo box.
                    if (EHGGmainRunwayCheckBox.Checked)
                    {
                        applicationVariables.ATISSamples.Add("mlrwy");
                        output += runwayToOutput(" MAIN LANDING RUNWAY", EHGGmainRunwayComboBox);
                    }
                    break;
                #endregion

                #region EHRD
                //If selected ICAO tab is EHRD.
                case "EHRD":
                    //If EHRD main runway check box is checked, generate runway output with value from EHRD main runway combo box.
                    if (EHRDmainRunwayCheckBox.Checked)
                    {
                        applicationVariables.ATISSamples.Add("mlrwy");
                        output += runwayToOutput(" MAIN LANDING RUNWAY", EHRDmainRunwayComboBox);
                    }
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

            applicationVariables.ATISSamples = new List<string>();
            String output = String.Empty;

            #region ICAO
            //Generate output from processed METAR ICAO.
            switch (metarProcessor.metar.ICAO)
            {
                //If processed ICAO is EHAM.
                case "EHAM":
                    applicationVariables.ATISSamples.Add("ehamatis");
                    output += "THIS IS SCHIPHOL INFORMATION";
                    break;

                //If processed ICAO is EHBK.
                case "EHBK":
                    applicationVariables.ATISSamples.Add("ehbkatis");
                    output += "THIS IS BEEK INFORMATION";
                    break;

                //If processed ICAO is EHEH.
                case "EHEH":
                    applicationVariables.ATISSamples.Add("ehehatis");
                    output += "THIS IS EINDHOVEN INFORMATION";
                    break;

                //If processed ICAO is EHGG.
                case "EHGG":
                    applicationVariables.ATISSamples.Add("ehggatis");
                    output += "THIS IS EELDE INFORMATION";
                    break;

                //If processed ICAO is EHRD.
                case "EHRD":
                    applicationVariables.ATISSamples.Add("ehrdatis");
                    output += "THIS IS ROTTERDAM INFORMATION";
                    break;
            }
            #endregion

            #region ATIS LETTER
            //Add ATIS letter to output
            output += atisLetterToFullSpelling(phoneticAlphabet[atisIndex]);
            #endregion

            applicationVariables.ATISSamples.Add("pause");

            #region RUNWAYS
            //Add runway output to output.
            output += generateRunwayOutput();
            #endregion

            #region TL
            //Add transition level to output.
            applicationVariables.ATISSamples.Add("trl");
            output += " TRANSITION LEVEL";
            //Calculate and add transition level to output.
            addIndividualDigitsToATISSamples(calculateTransitionLevel().ToString());
            output += " " + calculateTransitionLevel();
            #endregion

            #region OPERATIONAL REPORTS
            //Generate and add operational report to output.
            output += operationalReportToOutput();
            #endregion

            applicationVariables.ATISSamples.Add("pause");

            #region WIND
            //If processed METAR has wind, generate and add wind output to output. 
            if (addWindRecordCheckBox.Checked)
            {
                applicationVariables.ATISSamples.Add("wind");
                output += " WIND";
            }
            if (metarProcessor.metar.Wind != null) output += windToOutput(metarProcessor.metar.Wind);
            #endregion

            #region CAVOK
            //If processed METAR has CAVOK, add CAVOK to output.
            if (metarProcessor.metar.CAVOK)
            {
                applicationVariables.ATISSamples.Add("cavok");
                output += " CAVOK";
            }
            #endregion

            #region VISIBILITY
            //If processed METAR has a visibility greater than 0, generate and add visibility output to output. 
            if (metarProcessor.metar.Visibility > 0) output += visibilityToOutput(metarProcessor.metar.Visibility);
            #endregion

            #region RVRONATC
            //If processed METAR has RVR, add RVR to output. 
            if (metarProcessor.metar.RVR)
            {
                applicationVariables.ATISSamples.Add("rvronatc");
                output += " RVR AVAILABLE ON ATC FREQUENCY";
            }
            #endregion

            #region PHENOMENA
            //Generate and add weather phenomena to output.
            output += listToOutput(metarProcessor.metar.Phenomena);
            #endregion

            #region CLOUDS OPTIONS
            //If processed METAR has SKC, add SKC to output. 
            if (metarProcessor.metar.SKC)
            {
                applicationVariables.ATISSamples.Add("skc");
                output += " SKY CLEAR";
            }
            //If processed METAR has NSC, add NSC to output. 
            if (metarProcessor.metar.NSC)
            {
                applicationVariables.ATISSamples.Add("sc");
                output += " NO SIGNIFICANT CLOUDS";
            }
            #endregion

            #region CLOUDS
            //Generate and add weather clouds to output. 
            output += listToOutput(metarProcessor.metar.Clouds);
            #endregion

            #region VERTICAL VISIBILITY
            //If processed METAR has a vertical visibility greater than 0, add vertical visibility to output.
            if (metarProcessor.metar.VerticalVisibility > 0)
            {
                applicationVariables.ATISSamples.Add("vv");
                addIndividualDigitsToATISSamples(metarProcessor.metar.VerticalVisibility.ToString());
                applicationVariables.ATISSamples.Add("hunderd");
                applicationVariables.ATISSamples.Add("meters");

                output += " VERTICAL VISIBILITY " + metarProcessor.metar.VerticalVisibility + " HUNDERD METERS";
            }
            #endregion

            #region TEMPERATURE
            //Add temperature to output.
            applicationVariables.ATISSamples.Add("temp");
            output += " TEMPERATURE";

            //If processed METAR has a minus temperature.
            if (metarProcessor.metar.Temperature.StartsWith("M"))
            {
                applicationVariables.ATISSamples.Add("minus");

                addIndividualDigitsToATISSamples(Convert.ToInt32(metarProcessor.metar.Temperature.ToString().Substring(1, 2)).ToString());

                output += " MINUS " + Convert.ToInt32(metarProcessor.metar.Temperature.ToString().Substring(1, 2));
            }
            //Positive temperature.
            else
            {
                addIndividualDigitsToATISSamples(Convert.ToInt32(metarProcessor.metar.Temperature.ToString()).ToString());

                output += " " + Convert.ToInt32(metarProcessor.metar.Temperature.ToString());
            }
            #endregion

            #region DEWPOINT
            //Add dewpoint to output.
            applicationVariables.ATISSamples.Add("dp");
            output += " DEWPOINT";

            //If processed METAR has a minus dewpoint.
            if (metarProcessor.metar.Dewpoint.StartsWith("M"))
            {
                applicationVariables.ATISSamples.Add("minus");

                addIndividualDigitsToATISSamples(Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString().Substring(1, 2)).ToString());

                output += " MINUS " + Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString().Substring(1, 2));
            }

            //Positive dewpoint.
            else
            {
                addIndividualDigitsToATISSamples(Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString()).ToString());

                output += " " + Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString());
            }
            #endregion

            #region QNH
            //Add QNH to output.
            applicationVariables.ATISSamples.Add("qnh");
            output += " QNH";
            addIndividualDigitsToATISSamples(metarProcessor.metar.QNH.ToString());
            output += " " + metarProcessor.metar.QNH.ToString();
            applicationVariables.ATISSamples.Add("hpa");
            output += " HECTOPASCAL";
            #endregion

            #region NOSIG
            //If processed METAR has NOSIG, add NOSIG to output.
            if (metarProcessor.metar.NOSIG)
            {
                applicationVariables.ATISSamples.Add("nosig");
                output += " NO SIGNIFICANT CHANGE";
            }
            #endregion

            #region TEMPO
            //If processed METAR has a TEMPO trend.
            if (metarProcessor.metar.metarTEMPO != null)
            {
                //Add TEMPO to output.
                applicationVariables.ATISSamples.Add("tempo");
                output += " TEMPORARY";

                #region TEMPO WIND
                //If processed TEMPO trend has wind, generate and add wind output to output. 
                if (metarProcessor.metar.metarTEMPO.Wind != null) output += windToOutput(metarProcessor.metar.metarTEMPO.Wind);
                #endregion

                #region TEMPO CAVOK
                //If processed TEMPO trend has CAVOK, add CAVOK to output.
                if (metarProcessor.metar.metarTEMPO.CAVOK)
                {
                    applicationVariables.ATISSamples.Add("cavok");
                    output += " CAVOK";
                }
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
                if (metarProcessor.metar.metarTEMPO.SKC)
                {
                    applicationVariables.ATISSamples.Add("skc");
                    output += " SKY CLEAR";
                }
                #endregion

                #region TEMPO NSW
                //If TEMPO trend has NSW, add NSW to output. 
                if (metarProcessor.metar.metarTEMPO.NSW)
                {
                    applicationVariables.ATISSamples.Add("nsw");
                    output += " NO SIGNIFICANT WEATHER";
                }
                #endregion

                #region TEMPO CLOUDS
                //If TEMPO trend has 1 or more weather clouds, generate and add TEMPO weather clouds to output. 
                if (metarProcessor.metar.metarTEMPO.Clouds.Count > 0) output += listToOutput(metarProcessor.metar.metarTEMPO.Clouds);
                #endregion

                #region TEMPO VERTICAL VISIBILITY
                //If TEMPO trend has a vertical visibility greater than 0, add TEMPO trend vertical visibility to output.
                if (metarProcessor.metar.metarTEMPO.VerticalVisibility > 0)
                {
                    applicationVariables.ATISSamples.Add("vv");
                    addIndividualDigitsToATISSamples(metarProcessor.metar.metarTEMPO.VerticalVisibility.ToString());
                    applicationVariables.ATISSamples.Add("hunderd");
                    applicationVariables.ATISSamples.Add("meters");

                    output += " VERTICAL VISIBILITY " + metarProcessor.metar.metarTEMPO.VerticalVisibility + " HUNDERD METERS";
                }
                #endregion
            }
            #endregion

            #region BECMG
            //If processed METAR has e BECMG trend.
            if (metarProcessor.metar.metarBECMG != null)
            {
                //Add BECMG to output.
                applicationVariables.ATISSamples.Add("becmg");
                output += " BECOMING";

                #region BECMG WIND
                //If processed BECMG trend has wind, generate and add wind output to output.
                if (metarProcessor.metar.metarBECMG.Wind != null) output += windToOutput(metarProcessor.metar.metarBECMG.Wind);
                #endregion

                #region BECMG CAVOK
                //If processed BECMG trend has CAVOK, add CAVOK to output.
                if (metarProcessor.metar.metarBECMG.CAVOK)
                {
                    applicationVariables.ATISSamples.Add("cavok");
                    output += " CAVOK";
                }
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
                if (metarProcessor.metar.metarBECMG.SKC)
                {
                    applicationVariables.ATISSamples.Add("skc");
                    output += " SKY CLEAR";
                }
                #endregion

                #region BECMG NSW
                //If BECMG trend has NSW, add NSW to output. 
                if (metarProcessor.metar.metarBECMG.NSW)
                {
                    applicationVariables.ATISSamples.Add("nsw");
                    output += " NO SIGNIFICANT WEATHER";
                }
                #endregion

                #region BECMG CLOUDS
                //If BECMG trend has 1 or more weather clouds, generate and add BECMG weather clouds to output. 
                if (metarProcessor.metar.metarBECMG.Clouds.Count > 0) output += listToOutput(metarProcessor.metar.metarBECMG.Clouds);
                #endregion

                #region BECMG VERTICAL VISIBILITY
                //If BECMG trend has a vertical visibility greater than 0, add BECMG trend vertical visibility to output.
                if (metarProcessor.metar.metarBECMG.VerticalVisibility > 0)
                {
                    applicationVariables.ATISSamples.Add("vv");
                    addIndividualDigitsToATISSamples(metarProcessor.metar.metarBECMG.VerticalVisibility.ToString());
                    applicationVariables.ATISSamples.Add("hunderd");
                    applicationVariables.ATISSamples.Add("meters");

                    output += " VERTICAL VISIBILITY" + metarProcessor.metar.metarBECMG.VerticalVisibility + " HUNDERD METERS";
                }
                #endregion
            }
            #endregion

            #region OPTIONAL
            //If inverted surface temperature check box is checked.
            if (markTempCheckBox.Checked)
            {
                applicationVariables.ATISSamples.Add("marktemp");
                output += " MARK TEMPERATURE INVERSION NEAR THE SURFACE";
            }
            //If arrival only check box is checked.
            if (arrOnlyCheckBox.Checked)
            {
                applicationVariables.ATISSamples.Add("call1");
                output += " CONTACT ARRIVAL CALLSIGN ONLY";
            }
            //If approach only check box is checked.
            if (appOnlyCheckBox.Checked)
            {
                applicationVariables.ATISSamples.Add("call2");
                output += " CONTACT APPROACH CALLSIGN ONLY";
            }
            //If arrival and approach only check box is checked.
            if (appArrOnlyCheckBox.Checked)
            {
                applicationVariables.ATISSamples.Add("call3");
                output += " CONTACT APPROACH AND ARRIVAL CALLSIGN ONLY";
            }
            #endregion

            #region END
            //Add end to output.
            applicationVariables.ATISSamples.Add("end");
            output += " END OF INFORMATION";
            output += atisLetterToFullSpelling(phoneticAlphabet[atisIndex]);
            #endregion

            #region USER WAVE
            if (userDefinedExtraCheckBox.Checked)
            {
                applicationVariables.ATISSamples.Add("extra");
                output += " EXTRA (VOICE ONLY)";
            }
            #endregion

            //If copy output check box is checked, copy ATIS output to clipboard.
            if (copyOutputCheckBox.Checked) Clipboard.SetText(output);

            //Set generated ATIS output in output text box.
            outputTextBox.Text = output;

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.atisfile))
            {
                MessageBox.Show("No path to atiseham.txt provided.", "Warning");

                //Open file dialog for user to set the path to atiseham.txt.
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Set properties.
                    Properties.Settings.Default.atisfile = openFileDialog.FileName;

                    //Save setting.
                    Properties.Settings.Default.Save();
                }
                //User didn't selected a file.
                else
                    return;
            }

            //Build ATIS file.
            try
            {
                generateATISButton.Enabled = false;
                
                soundLogic.Build(Properties.Settings.Default.atisfile);
            }
            catch (Exception ex)
            {
                generateATISButton.Enabled = true;

                MessageBox.Show(String.Format("Unable to build ATIS.\n\nError: {0}", ex.Message), "Error");
            }
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
            applicationVariables.SelectedAirport = ICAOTabControl.SelectedTab.Name;

            ApplicationEvents.SelectedAirportChanged(sender, e);

            //Set ICAO tab switched boolean.
            icaoTabSwitched = true;

            //Set ICAO of selected ICAO tab in ICAO text box.
            #region EHAM
            if (ICAOTabControl.SelectedTab.Name.Equals("EHAM"))
                icaoTextBox.Text = "EHAM";
            #endregion

            #region EHBK
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHBK"))
                icaoTextBox.Text = "EHBK";
            #endregion

            #region EHEH
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHEH"))
                icaoTextBox.Text = "EHEH";
            #endregion

            #region EHGG
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHGG"))
                icaoTextBox.Text = "EHGG";
            #endregion

            #region EHRD
            else
                icaoTextBox.Text = "EHRD";
            #endregion

            #region SETTING OF THE GET SELECT BEST RUNWAY BUTTON
            if (ICAOTabControl.SelectedTab.Name.Equals("EHAM"))
            {
                //Set text of get select best runway button.
                getSelectBestRunwayButton.Text = "Get EHAM runway(s)";

                //Enable get select best runway button.
                getSelectBestRunwayButton.Enabled = true;
            }
            else
            {
                //Set text of get select best runway button.
                getSelectBestRunwayButton.Text = "Select best runway";

                //If selected ICAO equals the ICAO of the last processed METAR, enable the get select best runway button.
                if (runwayInfo != null && ICAOTabControl.SelectedTab.Name.Equals(runwayInfo.metar.ICAO))
                    getSelectBestRunwayButton.Enabled = true;
                //Else keep disable it.
                else
                    getSelectBestRunwayButton.Enabled = false;
            }
            #endregion

            #region AUTO LOAD METAR
            metarBackgroundWorker.RunWorkerAsync(icaoTextBox.Text);
            #endregion

            //Set phonetic alphabet.
            setPhoneticAlphabet();

            //Set ATIS index and label.
            randomizeATISLetter();
        }

        /// <summary>
        /// Method called when the about tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize new About form.
            Form aboutForm = new AboutForm();

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
            setRunwayInfoForm();
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
                if (runwayInfo != null && !runwayInfo.IsDisposed && runwayInfoState)
                {
                    runwayInfo.Visible = true;
                    runwayInfo.BringToFront();
                }

                //Show sound form.
                formOpenerHelper.Show<SoundForm>();

                this.Show();
            }

            //If form is minimized.
            if (WindowState == FormWindowState.Minimized)
            {
                //Hide runwayInfo form.
                if (runwayInfo != null && !runwayInfo.IsDisposed) runwayInfo.Visible = false;

                //Hide sound form.
                formOpenerHelper.Hide<SoundForm>();
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
#if DEBUG
                WebRequest request = WebRequest.Create("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/Version/version2.php");
#else
                WebRequest request = WebRequest.Create("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/Version/version.php");
#endif
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
            if (latestVersion != null && !latestVersion.Equals(String.Empty))
            {
                //If a newer version is available.
                if (!latestVersion.Equals(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Trim()))
                {
                    while (latestVersion.Contains("."))
                    {
                        latestVersion = latestVersion.Remove(latestVersion.IndexOf("."), 1);
                    }

                    string applicationVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Trim();

                    while (applicationVersion.Contains("."))
                    {
                        applicationVersion = applicationVersion.Remove(applicationVersion.IndexOf("."), 1);
                    }

                    if (Convert.ToInt32(latestVersion) > Convert.ToInt32(applicationVersion))
                    {
                        if (MessageBox.Show("Newer version is available.\nDownload latest version?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            formOpenerHelper.ShowModalForm<AutoUpdaterForm>();
                        }
                    }
                }
            }
        }

        public class SPIData
        {
            public string lvnl_start { get; set; }
            public string lvnl_land { get; set; }
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
                //Create web client.
                WebClient client = new WebClient();

                //Make web request to https://spi.dutchvacc.nl/api_atisgenerator.php?action=retrieve_lvnlrunways
                var data = new WebClient().DownloadString("https://spi.dutchvacc.nl/api_atisgenerator.php?action=retrieve_lvnlrunways").Trim();

                //Parse data.
                var spiData = JsonConvert.DeserializeObject<List<SPIData>>(data);

                landingRunways.AddRange(spiData.Single().lvnl_land.Split(',').Select(s => s.Trim()));
                departureRunways.AddRange(spiData.Single().lvnl_start.Split(',').Select(s => s.Trim()));
            }
            catch (Exception)
            {
                //Show error.
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
            //Clear runway combo boxes.
            EHAMmainDepartureRunwayComboBox.Text = EHAMmainLandingRunwayComboBox.Text = EHAMsecondaryDepartureRunwayComboBox.Text = EHAMsecondaryLandingRunwayComboBox.Text = String.Empty;

            //Only one departure runway found.
            if (departureRunways.Count == 1)
                EHAMmainDepartureRunwayComboBox.Text = departureRunways.First();

            //Only one landing runway found.
            if (landingRunways.Count == 1)
                EHAMmainLandingRunwayComboBox.Text = landingRunways.First();

            //Two or more landing or departure runways found.
            if (landingRunways.Count > 1 || departureRunways.Count > 1)
                processMultipleRunways();

            //Re-enable get select best runway button.
            getSelectBestRunwayButton.Enabled = true;

            if (landingRunways.Count() > 0 || departureRunways.Count > 0)
                MessageBox.Show("Controller notice! Verify auto selected runway(s).", "Warning");
        }

        /// <summary>
        /// Process if multiple runways we're found and set the runway selection boxes with the founded values.
        /// </summary>
        private void processMultipleRunways()
        {
#region LANDING RUNWAYS
            //If there are more than two landing runways.
            if (landingRunways.Count > 1)
            {
                String firstRunway = landingRunways.First();
                String secondRunway = landingRunways.Last();

                //Generate landing runway combinations list.
                List<Tuple<String, String>> landingRunwayCombinations = new List<Tuple<String, String>>()
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
#endregion

#region DEPARTURE RUNWAYS
            //If there are more than two departure runways found.
            if (departureRunways.Count > 1)
            {
                String firstRunway = departureRunways.First();
                String secondRunway = departureRunways.Last();

                //Generate departure runway combinations list.
                List<Tuple<String, String>> departureRunwayCombinations = new List<Tuple<String, String>>()
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
#endregion
        }

        /// <summary>
        /// Method called hen runway info tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void runwayInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setRunwayInfoForm();
        }

        /// <summary>
        /// Method called hen sound tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void SoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSoundForm();
        }

        /// <summary>
        /// Method called if sound button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void SoundButton_Click(object sender, EventArgs e)
        {
            SetSoundForm();
        }

        /// <summary>
        /// Method called hen Amsterdam info tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void amsterdamInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Amsterdam Info page of the Dutch VACC site.
            System.Diagnostics.Process.Start("http://www.dutchvacc.nl/index.php?option=com_content&view=article&id=127&Itemid=70");
        }

        /// <summary>
        /// Sets all controls for opening and closing the runway info form.
        /// </summary>
        private void setRunwayInfoForm()
        {
#region OPENING
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

                //Set runway info tool strip menu item back color to gradient active caption.
                runwayInfoToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
            }
#endregion

#region CLOSING
            //If runway info is opened.
            else
            {
                runwayInfoButton.Text = ">";

                //Hide runway info form.
                runwayInfo.Visible = false;

                //Inverse runway info state boolean.
                runwayInfoState = !runwayInfoState;

                //Set runway info tool strip menu item back color to control.
                runwayInfoToolStripMenuItem.BackColor = SystemColors.Control;
            }
#endregion
        }

        /// <summary>
        /// Sets all controls for opening and closing the sound form.
        /// </summary>
        private void SetSoundForm()
        {
            if (formOpenerHelper.IsOpen<SoundForm>())
            {
                formOpenerHelper.CloseForm<SoundForm>();

                soundButton.Text = "▼";

                //Stop playback.
                soundLogic.Stop();
            }
            else
            {
                formOpenerHelper.ShowModelessForm<SoundForm>();

                soundButton.Text = "▲";
            }

            soundToolStripMenuItem.BackColor = formOpenerHelper.IsOpen<SoundForm>() ? SystemColors.GradientActiveCaption : SystemColors.Control;
        }

        /// <summary>
        /// Method called when dutch VACC tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void dutchVACCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open the Dutch VACC site.
            System.Diagnostics.Process.Start("http://www.dutchvacc.nl/");
        }

        /// <summary>
        /// Method called when get select best runway button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void getSelectBestRunwayButton_Click(object sender, EventArgs e)
        {
            if (ICAOTabControl.SelectedTab.Name.Equals("EHAM"))
            {
                //Disable get select best runway button;
                getSelectBestRunwayButton.Enabled = false;

                //Start real runway background worker.
                realRunwayBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                //Get best runway for selected airport.
                runwayInfo.ICAOBestRunway(ICAOTabControl.SelectedTab.Name);

                MessageBox.Show("Controller notice! Verify auto selected runway(s).", "Warning");
            }
        }

        /// <summary>
        /// Method called if TAF tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TerminalAerodromeForecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formOpenerHelper.IsOpen<TerminalAerodromeForecastForm>())
            {
                var terminalAerodromeForecastForm = formOpenerHelper.GetForm<TerminalAerodromeForecastForm>();

                if (terminalAerodromeForecastForm.terminalAerodromeForecastBackgroundWorker.IsBusy)
                    terminalAerodromeForecastForm.terminalAerodromeForecastBackgroundWorker.CancelAsync();

                formOpenerHelper.CloseForm<TerminalAerodromeForecastForm>();
            }
            else
            {
                formOpenerHelper.ShowModelessForm<TerminalAerodromeForecastForm>();

                formOpenerHelper.GetForm<TerminalAerodromeForecastForm>().terminalAerodromeForecastBackgroundWorker.RunWorkerAsync();
            }

            terminalAerodromeForecastToolStripMenuItem.BackColor = formOpenerHelper.IsOpen<TerminalAerodromeForecastForm>() ? SystemColors.GradientActiveCaption : SystemColors.Control;
        }

        /// <summary>
        /// Check if file is in use by another process.
        /// </summary>
        /// <param name="file">File to check.</param>
        /// <returns>Boolean indicating if file is in use by another process.</returns>
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            //Try to open file using the FileStream.
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //File locked.
                return true;
            }
            //Close FileStream.
            finally
            {
                if (stream != null) stream.Close();
            }

            //File not locked.
            return false;
        }

        /// <summary>
        /// Called when auto fetch METAR tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void autoFetchMETARToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autofetch = autoFetchMETARToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            if (autoFetchMETARToolStripMenuItem.Checked)
            {
                //Set new time to check to
                timerEnabled = DateTime.UtcNow;

                //Set the fetch METAR label to visible.
                fetchMetarLabel.Visible = true;

                //Start the METAR fetch timer.
                metarFetchTimer.Start();
                metarFetchTimer_Tick(null, null);
            }
            else
            {
                //Set the fetch METAR label to hide.
                fetchMetarLabel.Visible = false;

                //Stop the METAR fetch timer.
                metarFetchTimer.Stop();
            }
        }

        /// <summary>
        /// Called when auto process METAR tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void autoProcessMETARToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autoprocess = autoProcessMETARToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            if (autoProcessMETARToolStripMenuItem.Checked && autoLoadEHAMRunwayToolStripMenuItem.Checked)
                autoGenerateATISToolStripMenuItem.Enabled = true;

            else
                autoGenerateATISToolStripMenuItem.Enabled = autoGenerateATISToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// Load application settings from INI file.
        /// </summary>
        private void loadSettings()
        {
            //Load settings.
            autoFetchMETARToolStripMenuItem.Checked = Properties.Settings.Default.autofetch;
            autoProcessMETARToolStripMenuItem.Checked = Properties.Settings.Default.autoprocess;
            autoLoadEHAMRunwayToolStripMenuItem.Checked = Properties.Settings.Default.autoloadrunways;
            autoGenerateATISToolStripMenuItem.Checked = Properties.Settings.Default.autogenerateatis;
            ehamToolStripMenuItem.Checked = Properties.Settings.Default.eham;
            ehrdToolStripMenuItem.Checked = Properties.Settings.Default.ehrd;
            playSoundWhenMETARIsFetchedToolStripMenuItem.Checked = Properties.Settings.Default.playsound;
            randomLetterToolStripMenuItem.Checked = Properties.Settings.Default.randomletter;
        }

        /// <summary>
        /// Called when METAR fetch timer ticks.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void metarFetchTimer_Tick(object sender, EventArgs e)
        {
            //Update fetch METAR label.
            fetchMetarLabel.Text = "Fetching METAR in: " + (30 - (DateTime.UtcNow - timerEnabled).Minutes) + " minutes.";

            //If 30 minutes have passed, update the METAR.
            if ((DateTime.UtcNow - timerEnabled).Minutes > 29)
            //if ((DateTime.UtcNow - timerEnabled).Seconds > 10)
            {
                //Update METAR.
                getMetarButton_Click(null, null);

                //Flash task bar.
                if (this.Handle != GetForegroundWindow())
                {
                    try
                    {
                        FlashingWindow.FlashWindowEx(this);
                    }
                    catch (Exception) { }
                }

                //Play notification sound.
                if (playSoundWhenMETARIsFetchedToolStripMenuItem.Checked)
                {
                    try
                    {
                        using (SoundPlayer player = new SoundPlayer(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\sounds\\alert.wav"))
                            player.Play();
                    }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        /// Called when auto load EHAM runway tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void autoLoadEHAMRunwayToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autoloadrunways = autoLoadEHAMRunwayToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            if (autoLoadEHAMRunwayToolStripMenuItem.Checked && autoProcessMETARToolStripMenuItem.Checked)
                autoGenerateATISToolStripMenuItem.Enabled = true;

            else
                autoGenerateATISToolStripMenuItem.Enabled = autoGenerateATISToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// Method called when auto generate ATIS background worker is started.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void autoGenerateATISBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (metarBackgroundWorker.IsBusy) { /* WAIT */}

            while (realRunwayBackgroundWorker.IsBusy) { /* WAIT */}
        }

        /// <summary>
        /// Method called when auto generate ATIS background worker has finished.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void autoGenerateATISBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                generateATISButton_Click(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to auto generate the ATIS.\nGenerate the ATIS manually.", "Error");
            }
        }

        /// <summary>
        /// Called when auto load EHAM runway tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void autoGenerateATISToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autogenerateatis = autoGenerateATISToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Called when ICAO tab control tab is being selected.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ICAOTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (metarBackgroundWorker.IsBusy)
                e.Cancel = true;
        }

        /// <summary>
        /// Called when random letter tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void randomLetterToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.randomletter = randomLetterToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Called when EHAM tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ehamToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.eham = ehamToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            //Set phonetic alphabet.
            setPhoneticAlphabet();
        }

        /// <summary>
        /// Called when EHRD tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ehrdToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.ehrd = ehrdToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            //Set phonetic alphabet.
            setPhoneticAlphabet();
        }

        /// <summary>
        /// Called when play sound when METAR is fetched tool strip menu item checked state is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void playSoundWhenMETARIsFetchedToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.playsound = playSoundWhenMETARIsFetchedToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Randomizes ATIS letter.
        /// </summary>
        private void randomizeATISLetter()
        {
            //Random ATIS letter.
            if (randomLetterToolStripMenuItem.Checked)
            {
                randomLetter = true;

                Random random = new Random();
                atisIndex = random.Next(0, phoneticAlphabet.Count - 1);
            }
            //Set ATIS index to Z for first generation.
            else
                atisIndex = 0;

            //Set ATIS label.
            atisLetterLabel.Text = phoneticAlphabet[atisIndex];
        }

        /// <summary>
        /// Checks what phonetic alphabet to be set for ATIS generation.
        /// </summary>
        private void setPhoneticAlphabet()
        {
            //If selected tab is EHAM and EHAM (A - M) tool strip menu item is checked.
            if (ICAOTabControl.SelectedTab.Name.Equals("EHAM") && ehamToolStripMenuItem.Checked)
                phoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };

            //If selected tab is EHRD and EHRD (N - Z) tool strip menu item is checked.
            else if (ICAOTabControl.SelectedTab.Name.Equals("EHRD") && ehrdToolStripMenuItem.Checked)
                phoneticAlphabet = new List<String> { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            //Else set full phonetic alpha bet.
            else
                phoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            //If the current index is higher than the phonetic alphabet count (will cause exception!).
            if (atisIndex > phoneticAlphabet.Count)
                atisIndex = phoneticAlphabet.Count - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atisLetter"></param>
        /// <returns></returns>
        private string atisLetterToFullSpelling(string atisLetter)
        {
            switch (atisLetter)
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

            return String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        private void addIndividualDigitsToATISSamples(String input)
        {
            String[] processed = Regex.Split(input, @"([0-9])");

            foreach (String digit in processed)
            {
                if (!string.IsNullOrEmpty(digit))
                    applicationVariables.ATISSamples.Add(digit);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dutchVACCATISGeneratorV2ManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\manuals\\Handleiding Dutch VACC ATIS Generator v2.pdf");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to open ATC Operational Information manual.", "Error"); return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void atcOperationalInformationManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\manuals\\Handleiding ATC Operational Information.pdf");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to open ATC Operational Information manual.", "Error"); return;
            }
        }

        private void DutchVACCATISGenerator_LocationChanged(object sender, EventArgs e)
        {
            SetWindowBounds();

            ApplicationEvents.MainFormMoved(sender, e);
        }

        private void BuildAITSCompleted(object sender, EventArgs e)
        {
            if (generateATISButton.InvokeRequired)
                generateATISButton.Invoke(new Action(() => generateATISButton.Enabled = true));
            else
                generateATISButton.Enabled = true;
        }

        private void BuildAITSStarted(object sender, EventArgs e)
        {
            if (generateATISButton.InvokeRequired)
                generateATISButton.Invoke(new Action(() => generateATISButton.Enabled = false));
            else
                generateATISButton.Enabled = false;
        }

        private void TerminalAerodromeForecastFormClosing(object sender, FormClosingEventArgs e)
        {
            terminalAerodromeForecastToolStripMenuItem.BackColor = SystemColors.Control;
        }

        private void SetWindowBounds()
        {
            this.applicationVariables.MainFormBounds = this.Bounds;
        }
    }
}
