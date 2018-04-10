using DutchVACCATISGenerator.Extensions;
using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    [ExcludeFromCodeCoverage]
    public partial class SoundForm : Form
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly ISoundLogic soundLogic;

        public SoundForm(ApplicationVariables applicationVariables, ISoundLogic soundLogic)
        {
            this.applicationVariables = applicationVariables;
            this.soundLogic = soundLogic;

            InitializeComponent();

            //Register application events.
            ApplicationEvents.BuildAITSCompletedEvent += BuildAITSCompleted;
            ApplicationEvents.BuildAITSProgressChangedEvent += BuildAITSProgressChanged;
            ApplicationEvents.BuildAITSStartedEvent += BuildAITSStarted;
            ApplicationEvents.MainFormMovedEvent += MainFormMoved;
            ApplicationEvents.PlaybackStoppedEvent += PlaybackStopped;

            //Enable the build ATIS button if the ATIS has already been generated.
            if (applicationVariables.ATISSamples.Any())
                buildATISButton.Enabled = true;

            //Get and set the property of the path to the ATIS folder if it has been saved before. 
            //Else sets the path to the user document folder + \EuroScope\atis\atiseham.txt.
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.atisfile))
                ATISFileTextBox.Text = Properties.Settings.Default.atisfile;
            else
                ATISFileTextBox.Text = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}\EuroScope\atis\atiseham.txt";
        }

        #region UI events
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            //If user has selected a file.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetATISFileTextBoxAndProperty();
            }
        }

        private void Sound_Load(object sender, EventArgs e)
        {
            this.SetRelativeBottom(this.applicationVariables.MainFormBounds);
        }
        #endregion

        #region Application events
        private void BuildATISButton_Click(object sender, EventArgs e)
        {
            buildATISButton.Enabled = false;
            playATISButton.Enabled = false;

            soundLogic.Build(ATISFileTextBox.Text, applicationVariables.ATISSamples);
        }

        private void BuildAITSCompleted(object sender, EventArgs e)
        {
            if (buildATISButton.InvokeRequired)
                buildATISButton.Invoke(new Action(() => buildATISButton.Enabled = true));
            else
                buildATISButton.Enabled = true;

            if (playATISButton.InvokeRequired)
                playATISButton.Invoke(new Action(() => playATISButton.Enabled = true));
            else
                playATISButton.Enabled = true;
        }

        private void BuildAITSProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar.InvokeRequired)
                progressBar.Invoke(new Action(() => progressBar.Value = e.ProgressPercentage));
            else
                progressBar.Value = e.ProgressPercentage;
        }

        private void BuildAITSStarted(object sender, EventArgs e)
        {
            if (buildATISButton.InvokeRequired)
                buildATISButton.Invoke(new Action(() => buildATISButton.Enabled = false));
            else
                buildATISButton.Enabled = false;
        }

        private void MainFormMoved(object sender, EventArgs e)
        {
            this.SetRelativeBottom(this.applicationVariables.MainFormBounds);
        }

        private void PlayATISButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ATISFileTextBox.Text) && !ShowNoATISFileSelectedWarning())
                return;

            buildATISButton.Enabled = false;

            playATISButton.Text = "Stop ATIS";

            try
            {
                soundLogic.Play(ATISFileTextBox.Text);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(String.Format("Unable to play ATIS. Check if the atiseham.txt file is in the same folder as the ATIS sounds (atis.wav, etc.).\n\nError: {0}", ex.Message), "Error");

                buildATISButton.Enabled = true;
            }
        }

        private void PlaybackStopped(object sender, EventArgs e)
        {
            //Set play ATIS button text to...
            playATISButton.Text = "Play ATIS";

            buildATISButton.Enabled = true;
        }
        #endregion

        private bool ShowNoATISFileSelectedWarning()
        {
            MessageBox.Show("No path to atiseham.txt provided.", "Warning");

            //Open file dialog for user to set the path to atiseham.txt.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetATISFileTextBoxAndProperty();

                return true;
            }
            //User didn't selected a file.
            else
                return false;
        }

        private void SetATISFileTextBoxAndProperty()
        {
            //Set properties.
            ATISFileTextBox.Text =
                Properties.Settings.Default.atisfile = openFileDialog.FileName;

            //Save setting.
            Properties.Settings.Default.Save();
        }
    }
}