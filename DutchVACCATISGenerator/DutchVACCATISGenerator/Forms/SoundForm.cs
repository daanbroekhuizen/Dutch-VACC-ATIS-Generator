using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types.Application;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
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
            ApplicationEvents.MainFormMovedEvent += MainFormMoved;

            //Enable the build ATIS button if the ATIS has already been generated.
            if (applicationVariables.ATISSamples.Any())
                buildATISButton.Enabled = false;

            //Get and set the property of the path to the ATIS folder if it has been saved before. 
            //Else sets the path to the user document folder + \EuroScope\atis\atiseham.txt.
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.atisfile))
                ATISFileTextBox.Text = Properties.Settings.Default.atisfile;
            else
                ATISFileTextBox.Text = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}\EuroScope\atis\atiseham.txt";
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            //If user has selected a file.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ATISFileTextBox.Text = openFileDialog.FileName;

                //Save selected path to settings.
                Properties.Settings.Default.atisfile = ATISFileTextBox.Text;

                //Save setting.
                Properties.Settings.Default.Save();
            }
        }

        private void BuildATISButton_Click(object sender, EventArgs e)
        {
            buildATISButton.Enabled = false;
            playATISButton.Enabled = false;

            soundLogic.Build();
        }

        private void PlayATISButton_Click(object sender, EventArgs e)
        {
            soundLogic.Play();
        }

        private void Sound_Load(object sender, EventArgs e)
        {
            SetRelativeToMainForm();
        }

        private void BuildAITSCompleted(object sender, EventArgs e)
        {
            buildATISButton.Enabled = true;
            playATISButton.Enabled = true;
        }

        private void MainFormMoved(object sender, EventArgs e)
        {
            SetRelativeToMainForm();
        }

        private void BuildAITSProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void SetRelativeToMainForm()
        {
            this.Left = this.applicationVariables.MainFormBounds.Left;
            this.Top = this.applicationVariables.MainFormBounds.Bottom;
            this.Refresh();
        }
    }
}