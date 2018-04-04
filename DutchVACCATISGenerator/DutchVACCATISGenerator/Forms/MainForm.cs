using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    public partial class MainForm : Form
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly IATISLogic ATISLogic;
        private readonly IFileLogic fileLogic;
        private readonly IFormOpenerHelper formOpenerHelper;
        private readonly IMETARLogic METARLogic;
        private readonly IRunwayLogic runwayLogic;
        private readonly ISoundLogic soundLogic;

        private DateTime fetchMETARTime;

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>IntPtr - Handle of foreground window</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public MainForm(ApplicationVariables applicationVariables,
            IATISLogic ATISLogic,
            IFileLogic fileLogic,
            IFormOpenerHelper formOpenerHelper,
            IMETARLogic METARLogic,
            IRunwayLogic runwayLogic,
            ISoundLogic soundLogic,
            IVersionLogic versionLogic)
        {
            this.ATISLogic = ATISLogic;
            this.applicationVariables = applicationVariables;
            this.fileLogic = fileLogic;
            this.formOpenerHelper = formOpenerHelper;
            this.METARLogic = METARLogic;
            this.runwayLogic = runwayLogic;
            this.soundLogic = soundLogic;

            InitializeComponent();

            SetWindowBounds();

            //Register application events.
            ApplicationEvents.BuildAITSCompletedEvent += BuildAITSCompleted;
            ApplicationEvents.BuildAITSStartedEvent += BuildAITSStarted;
            ApplicationEvents.METARDownloadedEvent += METARDownloaded;
            ApplicationEvents.NewVersionEvent += NewVersion;
            ApplicationEvents.SchipholRunwaysEvent += SchipholRunways;
            ApplicationEvents.TerminalAerodromeForecastFormClosingEvent += TerminalAerodromeForecastFormClosing;

            //Load settings.
            LoadSettings();

            //Set ATIS index and label.
            RandomizeATISLetter(true);

            //Check for new version.
            versionLogic.NewVersion();

            //Load EHAM METAR.
            DownloadMETAR();

            //Delete installer files.
            fileLogic.DeleteInstallerFiles(Assembly.GetExecutingAssembly().Location);

            //If auto load EHAM runways is selected.
            if (autoLoadEHAMRunwayToolStripMenuItem.Checked)
                runwayLogic.SchipholRunways();
        }

        #region UI events

        private void DutchVACCATISGenerator_LocationChanged(object sender, EventArgs e)
        {
            SetWindowBounds();

            ApplicationEvents.MainFormMoved(sender, e);
        }

        private void DutchVACCATISGenerator_Resize(object sender, EventArgs e)
        {
            //If form is restored to normal window state.
            if (WindowState == FormWindowState.Normal)
            {
                formOpenerHelper.Show<RunwayForm>();
                formOpenerHelper.Show<SoundForm>();

                this.Show();
            }

            //If form is minimized.
            if (WindowState == FormWindowState.Minimized)
            {
                formOpenerHelper.Hide<RunwayForm>();
                formOpenerHelper.Hide<SoundForm>();
            }
        }

        private void GenerateATISButton_Click(object sender, EventArgs e)
        {
            //If the ICAO code of the processed METAR doesn't equals the ICAO of the selected ICAO tab.
            if (!applicationVariables.METAR.ICAO.Equals(applicationVariables.SelectedAirport))
            {
                //Show warning message.
                MessageBox.Show("Selected ICAO tab does not match the ICAO of the processed METAR.", "Error");
                return;
            }

            //Check runways selected.
            if (!RunwaySelected())
                return;

            //Generate output.
            string output = ATISLogic.GenerateOutput(applicationVariables.METAR);

            //If copy output check box is checked, copy ATIS output to clipboard.
            if (copyOutputCheckBox.Checked)
                Clipboard.SetText(output);

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

                MessageBox.Show($"Unable to build ATIS.\n\nError: {ex.Message}", "Error");
            }
        }

        private void GetMetarButton_Click(object sender, EventArgs e)
        {
            //Set new time to check to.
            fetchMETARTime = DateTime.UtcNow;

            //Get ICAO entered.
            string icao = icaoTextBox.Text;

            //If no ICAO has been entered.
            if (icao == string.Empty)
            {
                MessageBox.Show("Enter an ICAO code.", "Warning");
                return;
            }

            //Disable the get METAR button so the user can't overload it.
            getMetarButton.Enabled = false;

            //Get METAR.
            METARLogic.Download(applicationVariables.SelectedAirport);
        }

        private void ICAOTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update application variable.
            applicationVariables.SelectedAirport = ICAOTabControl.SelectedTab.Name;

            //Fire application event.
            ApplicationEvents.SelectedAirportChanged(sender, e);

            //Set ICAO of selected ICAO tab in ICAO text box.
            icaoTextBox.Text = applicationVariables.SelectedAirport;

            if (applicationVariables.SelectedAirport.Equals("EHAM"))
            {
                //Set text of get select best runway button.
                selectBestRunwayButton.Text = "Get EHAM runway(s)";

                //Enable get select best runway button.
                selectBestRunwayButton.Enabled = true;
            }
            else
            {
                //Set text of get select best runway button.
                selectBestRunwayButton.Text = "Select best runway";

                if (applicationVariables.METAR != null)
                    //If selected ICAO equals the ICAO of the last processed METAR, enable the get select best runway button.
                    selectBestRunwayButton.Enabled = applicationVariables.METAR.ICAO.Equals(applicationVariables.SelectedAirport);
            }

            //Get METAR.
            METARLogic.Download(applicationVariables.SelectedAirport);

            //Set phonetic alphabet.
            RandomizeATISLetter(true);

            //Set ATIS index and label.
            RandomizeATISLetter();
        }

        private void METARFetchTimer_Tick(object sender, EventArgs e)
        {
            //Update fetch METAR label.
            fetchMETARLabel.Text = $"Fetching METAR in: {30 - (DateTime.UtcNow - fetchMETARTime).Minutes} minutes.";

            //If 30 minutes have passed, update the METAR.
#if DEBUG
            if ((DateTime.UtcNow - fetchMETARTime).Seconds > 10)
#else
            if ((DateTime.UtcNow - timerEnabled).Minutes > 29)
#endif
            {
                //Update METAR.
                //TODO call this some other way
                GetMetarButton_Click(null, null);

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

        private void METARTextBox_TextChanged(object sender, EventArgs e)
        {
            //If METAR text box contains text, enable process METAR button.
            processMETARButton.Enabled = !METARTextBox.Text.Trim().Equals(string.Empty);
        }

        private void NextATISLetterButton_Click(object sender, EventArgs e)
        {
            if (applicationVariables.ATISIndex == applicationVariables.PhoneticAlphabet.Count - 1)
                applicationVariables.ATISIndex = 0;
            else
                applicationVariables.ATISIndex++;

            //Set ATIS letter in ATIS letter label.
            SetATISLabel();
        }

        private void PreviousATISLetterButton_Click(object sender, EventArgs e)
        {
            if (applicationVariables.ATISIndex == 0)
                applicationVariables.ATISIndex = applicationVariables.PhoneticAlphabet.Count - 1;
            else
                applicationVariables.ATISIndex--;

            //Set ATIS letter in ATIS letter label.
            SetATISLabel();
        }

        private void ProcessMETARButton_Click(object sender, EventArgs e)
        {
            //Check if a METAR has been entered.
            if (METARTextBox.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("No METAR fetched or entered.", "Error");
                return;
            }

            //Check if entered METAR ICAO matches the selected ICAO tab.
            if (!METARTextBox.Text.Trim().StartsWith((applicationVariables.SelectedAirport)))
            {
                MessageBox.Show("Selected ICAO tab does not match the ICAO of the entered METAR.", "Warning");
                return;
            }

            //Update METAR.
            applicationVariables.METAR = new METAR(METARTextBox.Text.Trim());

            //Calculate the transition level.
            try
            {
                tlOutLabel.Text = METARLogic.CalculateTransitionLevel(applicationVariables.METAR.Temperature, applicationVariables.METAR.QNH).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error parsing the METAR, check if METAR is in correct format.", "Error");
                return;
            }

            //Clear output and METAR text box.
            outputTextBox.Clear();
            METARTextBox.Clear();

            //Set processed METAR in last processed METAR label.
            if (applicationVariables.METAR.OriginalMETAR.Length > 140)
                lastLabel.Text = $"Last successful processed METAR:\n{applicationVariables.METAR.OriginalMETAR.Substring(0, 69).Trim()}\n{applicationVariables.METAR.OriginalMETAR.Substring(69, 69).Trim()}...";
            else if (applicationVariables.METAR.OriginalMETAR.Length > 69)
                lastLabel.Text = $"Last successful processed METAR:\n{applicationVariables.METAR.OriginalMETAR.Substring(0, 69).Trim()}\n{applicationVariables.METAR.OriginalMETAR.Substring(69).Trim()}";
            else
                lastLabel.Text = $"Last successful processed METAR:\n{applicationVariables.METAR.OriginalMETAR}";

            //Set ATIS letter in ATIS letter label.
            SetATISLabel();

            //Enable generate ATIS and runway info button.
            generateATISButton.Enabled = true;
            runwayInfoButton.Enabled =
                runwayInfoToolStripMenuItem.Enabled = true;

            //Trigger METAR processed event.
            ApplicationEvents.METARProcessed(sender, e);


            //Check if selected ICAO tab matches the ICAO of the processed applicationVariables.METAR.
            if (!(applicationVariables.METAR.ICAO.Equals(applicationVariables.SelectedAirport)))
                MessageBox.Show($"Last processed METAR ICAO does not match the selected ICAO tab.\nRunway criteria will be calculated of the wrong METAR ({applicationVariables.METAR.ICAO})!", "Warning");

            //If processed METAR equals the selected ICAO.
            if (applicationVariables.METAR.ICAO.Equals(applicationVariables.SelectedAirport))
                selectBestRunwayButton.Enabled = true;
        }

        private void SelectBestRunwayButton_Click(object sender, EventArgs e)
        {
            if (applicationVariables.SelectedAirport.Equals("EHAM"))
            {
                //Disable get select best runway button;
                selectBestRunwayButton.Enabled = false;

                //Get Schiphol runways.
                try
                {
                    runwayLogic.SchipholRunways();
                }
                catch
                {
                    MessageBox.Show("Unable to get real EHAM runway combination from the Internet.", "Error");
                }

                //realRunwayBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                //Get best runway for selected airport.
                SelectBestRunway();

                MessageBox.Show("Controller notice! Verify auto selected runway(s).", "Warning");
            }
        }

        #region Additional ATIS settings
        private void ApproachCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (arrivalCheckBox.Checked)
            {
                approachAndArrivalCheckBox.Checked = true;
                approachCheckBox.Checked =
                    arrivalCheckBox.Checked = false;
            }
        }

        private void ApproachAndArrivalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (approachAndArrivalCheckBox.Checked)
                approachCheckBox.Enabled =
                    arrivalCheckBox.Enabled =
                    approachCheckBox.Checked =
                    arrivalCheckBox.Checked = false;
            else approachCheckBox.Enabled = arrivalCheckBox.Enabled = true;
        }

        private void ArrivalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (approachCheckBox.Checked)
            {
                approachAndArrivalCheckBox.Checked = true;
                approachCheckBox.Checked =
                    arrivalCheckBox.Checked = false;
            }
        }
        #endregion

        #region Additional forms
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formOpenerHelper.ShowModalForm<AboutForm>();
        }

        private void RunwayButton_Click(object sender, EventArgs e)
        {
            SetControlsForRunwayInfoForm();
        }

        private void RunwayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsForRunwayInfoForm();
        }

        private void SoundButton_Click(object sender, EventArgs e)
        {
            SetControlsForSoundForm();
        }

        private void SoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlsForSoundForm();
        }

        private void TerminalAerodromeForecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formOpenerHelper.IsOpen<TerminalAerodromeForecastForm>())
            {
                var terminalAerodromeForecastForm = formOpenerHelper.GetForm<TerminalAerodromeForecastForm>();

                //TODO expose background worker this way?
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
        #endregion

        #region Menu strip
        private void AmsterdamInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Amsterdam Info page of the Dutch VACC site.
            Process.Start("http://www.dutchvacc.nl/index.php?option=com_content&view=article&id=127&Itemid=70");
        }

        private void ATCOperationalInformationManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\manuals\\Handleiding ATC Operational Information.pdf");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to open ATC Operational Information manual.", "Error");
            }
        }

        private void DutchVACCATISGeneratorV2ManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\manuals\\Handleiding Dutch VACC ATIS Generator v2.pdf");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to open ATC Operational Information manual.", "Error");
            }
        }

        private void DutchVACCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.dutchvacc.nl/");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Runway selection enabling/disabling
        private void BeekRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BeekRunwayComboBox.Enabled = BeekRunwayCheckBox.Checked;
        }

        private void EeldeRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EeldeRunwayComboBox.Enabled = EeldeRunwayCheckBox.Checked;
        }

        private void EindhovenRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EindhovenRunwayComboBox.Enabled = EindhovenRunwayCheckBox.Checked;
        }

        private void RotterdamRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RotterdamRunwayComboBox.Enabled = RotterdamRunwayCheckBox.Checked;
        }

        private void SchipholMainDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SchipholMainDepartureRunwayComboBox.Enabled = SchipholMainDepartureRunwayCheckBox.Enabled;
        }

        private void SchipholMainLandingRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SchipholMainLandingRunwayComboBox.Enabled = SchipholMainLandingRunwayCheckBox.Checked;
        }

        private void SchipholSecondaryDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SchipholSecondaryDepartureRunwayComboBox.Enabled = SchipholSecondaryDepartureRunwayCheckBox.Checked;
        }

        private void SchipholSecondaryLandingRunway_CheckedChanged(object sender, EventArgs e)
        {
            SchipholScondaryLandingRunwayComboBox.Enabled = SchipholSecondaryLandingRunwayCheckBox.Checked;
        }
        #endregion

        #region Setting tool strip
        private void AutoFetchMETARToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autofetch = autoFetchMETARToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            if (autoFetchMETARToolStripMenuItem.Checked)
            {
                //Set new time to check to
                fetchMETARTime = DateTime.UtcNow;

                //Set the fetch METAR label to visible.
                fetchMETARLabel.Visible = true;

                //Start the METAR fetch timer.
                fetchMETARTimer.Start();

                //TODO call this some other way
                METARFetchTimer_Tick(null, null);
            }
            else
            {
                //Set the fetch METAR label to hide.
                fetchMETARLabel.Visible = false;

                //Stop the METAR fetch timer.
                fetchMETARTimer.Stop();
            }
        }

        private void AutoLoadEHAMRunwayToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autoloadrunways = autoLoadEHAMRunwayToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }

        private void AutoProcessMETARToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.autoprocess = autoProcessMETARToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }

        private void EHAMToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.eham = ehamToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            //Set phonetic alphabet.
            RandomizeATISLetter(true);

            //Set ATIS label.
            SetATISLabel();
        }

        private void EHRDToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.ehrd = ehrdToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();

            //Set phonetic alphabet.
            RandomizeATISLetter(true);

            //Set ATIS label.
            SetATISLabel();
        }

        private void PlaySoundWhenMETARIsFetchedToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.playsound = playSoundWhenMETARIsFetchedToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }

        private void RandomLetterToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            //Write value to settings.
            Properties.Settings.Default.randomletter = randomLetterToolStripMenuItem.Checked;

            //Save setting.
            Properties.Settings.Default.Save();
        }
        #endregion

        #endregion

        #region Application events
        private void BuildAITSCompleted(object sender, EventArgs e)
        {
            generateATISButton.Invoke(new Action(() => generateATISButton.Enabled = true));
        }

        private void BuildAITSStarted(object sender, EventArgs e)
        {
            generateATISButton.Invoke(new Action(() => generateATISButton.Enabled = false));
        }

        private void METARDownloaded(object sender, METARDownloadEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                //Set pulled METAR in the METAR text box.
                METARTextBox.Text = e.METAR;

                //If auto process METAR check box is checked, automatically process the METAR.
                if (!string.IsNullOrWhiteSpace(e.METAR) && autoProcessMETARToolStripMenuItem.Checked)
                    ProcessMETARButton_Click(null, null);

                //Re-enable the get METAR button.
                getMetarButton.Enabled = true;
            }));
        }

        private void NewVersion(object sender, EventArgs e)
        {
            if (MessageBox.Show("Newer version is available.\nDownload latest version?",
                "Message",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                formOpenerHelper.ShowModalForm<AutoUpdaterForm>();
            }
        }

        private void SchipholRunways(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                //Clear runway combo boxes.
                SchipholMainDepartureRunwayComboBox.Text =
                 SchipholMainLandingRunwayComboBox.Text =
                 SchipholSecondaryDepartureRunwayComboBox.Text =
                 SchipholScondaryLandingRunwayComboBox.Text = string.Empty;

                //Only one departure runway found.
                if (applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.Count == 1)
                    SchipholMainDepartureRunwayComboBox.Text = applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.First();

                //Only one landing runway found.
                if (applicationVariables.SchipholPlanningInterfaceData.LandingRunways.Count == 1)
                    SchipholMainLandingRunwayComboBox.Text = applicationVariables.SchipholPlanningInterfaceData.LandingRunways.First();

                //Two or more landing or departure runways found.
                if (applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.Count > 1 || applicationVariables.SchipholPlanningInterfaceData.LandingRunways.Count > 1)
                    SchipholMultipleRunways();

                //Re-enable get select best runway button.
                selectBestRunwayButton.Enabled = true;

                if (applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.Count() > 0 || applicationVariables.SchipholPlanningInterfaceData.LandingRunways.Count > 0)
                    MessageBox.Show("Controller notice! Verify auto selected runway(s).", "Warning");
            }));
        }

        private void TerminalAerodromeForecastFormClosing(object sender, FormClosingEventArgs e)
        {
            terminalAerodromeForecastToolStripMenuItem.BackColor = SystemColors.Control;
        }
        #endregion

        private void DownloadMETAR()
        {
            try
            {
                METARLogic.Download(applicationVariables.SelectedAirport);
            }
            catch
            {
                MessageBox.Show("Unable to get the METAR from the Internet.\nPlease provide a METAR.", "Error");

                //Re-enable the get METAR button.
                getMetarButton.Enabled = true;
            }
        }

        /// <summary>
        /// Loads all settings and sets menu strip items.
        /// </summary>
        private void LoadSettings()
        {
            autoFetchMETARToolStripMenuItem.Checked = Properties.Settings.Default.autofetch;
            autoProcessMETARToolStripMenuItem.Checked = Properties.Settings.Default.autoprocess;
            autoLoadEHAMRunwayToolStripMenuItem.Checked = Properties.Settings.Default.autoloadrunways;
            ehamToolStripMenuItem.Checked = Properties.Settings.Default.eham;
            ehrdToolStripMenuItem.Checked = Properties.Settings.Default.ehrd;
            playSoundWhenMETARIsFetchedToolStripMenuItem.Checked = Properties.Settings.Default.playsound;
            randomLetterToolStripMenuItem.Checked = Properties.Settings.Default.randomletter;
        }

        /// <summary>
        /// Processes Schiphol runways.
        /// </summary>
        private void SchipholMultipleRunways()
        {
            //If there are more than two departure runways found.
            if (applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.Count > 1)
            {
                string firstRunway = applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.First();
                string secondRunway = applicationVariables.SchipholPlanningInterfaceData.DepartureRunways.Last();

                //Check which runways are found and set the correct main and secondary departure runway.
                foreach (var runwayCombination in Runways.SchipholDepartureRunwayCombinations)
                {
                    if ((firstRunway.Equals(runwayCombination.Item1) && secondRunway.Equals(runwayCombination.Item2)) || (firstRunway.Equals(runwayCombination.Item2) && secondRunway.Equals(runwayCombination.Item1)))
                    {
                        SchipholMainDepartureRunwayComboBox.Text = runwayCombination.Item1;
                        SchipholSecondaryDepartureRunwayComboBox.Text = runwayCombination.Item2;
                    }
                }
            }

            //If there are more than two landing runways.
            if (applicationVariables.SchipholPlanningInterfaceData.LandingRunways.Count > 1)
            {
                string firstRunway = applicationVariables.SchipholPlanningInterfaceData.LandingRunways.First();
                string secondRunway = applicationVariables.SchipholPlanningInterfaceData.LandingRunways.Last();

                //Check which runways are found and set the correct main and secondary landing runway.
                foreach (var runwayCombination in Runways.SchipholLandingRunwayCombinations)
                {
                    if ((firstRunway.Equals(runwayCombination.Item1) && secondRunway.Equals(runwayCombination.Item2)) || (firstRunway.Equals(runwayCombination.Item2) && secondRunway.Equals(runwayCombination.Item1)))
                    {
                        SchipholMainLandingRunwayComboBox.Text = runwayCombination.Item1;
                        SchipholScondaryLandingRunwayComboBox.Text = runwayCombination.Item2;
                    }
                }
            }
        }

        /// <summary>
        /// Randomizes ATIS letter.
        /// </summary>
        private void RandomizeATISLetter(bool reset = false)
        {
            ATISLogic.SetPhoneticAlphabet(ehamToolStripMenuItem.Checked, ehrdToolStripMenuItem.Checked, randomLetterToolStripMenuItem.Checked, reset);

            SetATISLabel();
        }

        /// <summary>
        /// Checks whether if the required runway selections are made based on the selected tab.
        /// </summary>
        /// <returns>Boolean indicating if all required check boxes are checked for generating a runway output</returns>
        private bool RunwaySelected()
        {
            switch (applicationVariables.SelectedAirport)
            {
                case "EHAM":
                    //If EHAM main landing runway check box is checked OR the EHAM main landing runway combo box selection is NULL.
                    if (!SchipholMainLandingRunwayCheckBox.Checked || SchipholMainLandingRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main landing runway selected.", "Warning");
                        return false;
                    }

                    //If EHAM main departure runway check box is checked OR the EHAM main departure runway combo box selection is NULL.
                    if (!SchipholMainDepartureRunwayCheckBox.Checked || SchipholMainDepartureRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main departure runway selected.", "Warning");
                        return false;
                    }

                    //If EHAM secondary landing runway check box is checked OR the EHAM secondary landing runway combo box selection is NULL.
                    if (SchipholSecondaryLandingRunwayCheckBox.Checked && SchipholScondaryLandingRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("Secondary landing runway is checked but no runway is selected.", "Warning");
                        return false;
                    }

                    //If EHAM secondary departure runway check box is checked OR the EHAM secondary departure runway combo box selection is NULL.
                    if (SchipholSecondaryDepartureRunwayCheckBox.Checked && SchipholSecondaryDepartureRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("Secondary departure runway is checked but no runway is selected.", "Warning");
                        return false;
                    }

                    return true;

                //If selected ICAO tab is EHBK.
                case "EHBK":
                    //If EHBK main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!BeekRunwayCheckBox.Checked || BeekRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning");
                        return false;
                    }

                    return true;

                //If selected ICAO tab is EHEH.
                case "EHEH":
                    //If EHEH main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!EindhovenRunwayCheckBox.Checked || EindhovenRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning");
                        return false;
                    }

                    return true;

                //If selected ICAO tab is EHGG.
                case "EHGG":
                    //If EHGG main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!EeldeRunwayCheckBox.Checked || EeldeRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning");
                        return false;
                    }

                    return true;

                //If selected ICAO tab is EHRD.
                case "EHRD":
                    //If EHRD main runway check box is checked OR the EHBK main runway combo box selection is NULL.
                    if (!RotterdamRunwayCheckBox.Checked || RotterdamRunwayComboBox.SelectedIndex == -1)
                    {
                        //Show warning message.
                        MessageBox.Show("No main runway selected.", "Warning");
                        return false;
                    }

                    return true;
            }

            return false;
        }

        /// <summary>
        /// Sets runway combo box with best runway.
        /// </summary>
        private void SelectBestRunway()
        {
            switch (applicationVariables.SelectedAirport)
            {
                case "EHBK":
                    BeekRunwayComboBox.SelectedIndex = BeekRunwayComboBox.Items.IndexOf(runwayLogic.BestRunway(Runways.Beek));
                    break;

                case "EHRD":
                    RotterdamRunwayComboBox.SelectedIndex = RotterdamRunwayComboBox.Items.IndexOf(runwayLogic.BestRunway(Runways.Rotterdam));
                    break;

                case "EHGG":
                    EeldeRunwayComboBox.SelectedIndex = EeldeRunwayComboBox.Items.IndexOf(runwayLogic.BestRunway(Runways.Eelde));
                    break;

                case "EHEH":
                    EindhovenRunwayComboBox.SelectedIndex = EindhovenRunwayComboBox.Items.IndexOf(runwayLogic.BestRunway(Runways.Eindhoven));
                    break;
            }
        }

        /// <summary>
        /// Set ATIS label.
        /// </summary>
        private void SetATISLabel()
        {
            //Set ATIS letter in ATIS letter label.
            atisLetterLabel.Text = applicationVariables.PhoneticAlphabet[applicationVariables.ATISIndex];
        }

        /// <summary>
        /// Sets controls for opening and closing the runway info form.
        /// </summary>
        private void SetControlsForRunwayInfoForm()
        {
            if (formOpenerHelper.IsOpen<RunwayForm>())
            {
                formOpenerHelper.CloseForm<RunwayForm>();

                runwayInfoButton.Text = ">";
            }
            else
            {
                formOpenerHelper.ShowModelessForm<RunwayForm>();

                runwayInfoButton.Text = "<";
            }

            runwayInfoToolStripMenuItem.BackColor = formOpenerHelper.IsOpen<RunwayForm>() ? SystemColors.GradientActiveCaption : SystemColors.Control;
        }

        /// <summary>
        /// Sets controls for opening and closing the sound form.
        /// </summary>
        private void SetControlsForSoundForm()
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
        /// Sets the main form windows bounds in the application variables class.
        /// </summary>
        private void SetWindowBounds()
        {
            applicationVariables.MainFormBounds = this.Bounds;
        }
    }
}
