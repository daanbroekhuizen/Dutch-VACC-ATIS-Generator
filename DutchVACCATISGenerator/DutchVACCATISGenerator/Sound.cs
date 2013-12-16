using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// Sound class.
    /// </summary>
    public partial class Sound : Form
    {
        private DutchVACCATISGenerator dutchVACCATISGenerator;
        public IWavePlayer wavePlayer { get; set; }

        /// <summary>
        /// Constructor of DutchVACCATISGenerator. Initializes new instance of DutchVACCATISGenerator.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        public Sound(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            InitializeComponent();

            this.dutchVACCATISGenerator = dutchVACCATISGenerator;

            if (dutchVACCATISGenerator.outputTextBox.Text.Trim().Equals(String.Empty)) buildATISButton.Enabled = false;

            atisehamFileTextBox.Text =  Properties.Settings.Default.atisehamPath;
        }

        /// <summary>
        /// Set RunwayInform position relative to DutchVACCATISGenerator.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        public void showRelativeToDutchVACCATISGenerator(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            this.Left = dutchVACCATISGenerator.Left;
            this.Top = dutchVACCATISGenerator.Bottom;
            this.Refresh();
        }

        /// <summary>
        /// Method called when sound form is loaded.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void Sound_Load(object sender, EventArgs e)
        {
            showRelativeToDutchVACCATISGenerator(dutchVACCATISGenerator);
        }

        /// <summary>
        /// Method called when browse button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void browseButton_Click(object sender, EventArgs e)
        {          
            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                atisehamFileTextBox.Text = openFileDialog.FileName;

                Properties.Settings.Default.atisehamPath = atisehamFileTextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Method called when play ATIS button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void playATISButton_Click(object sender, EventArgs e)
        {
            if (playATISButton.Text.Equals("Play ATIS"))
            {
                playATISButton.Text = "Stop ATIS";

                AudioFileReader audio = new AudioFileReader(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav");

                wavePlayer = new WaveOut(WaveCallbackInfo.FunctionCallback());
                wavePlayer.Init(audio);
                wavePlayer.PlaybackStopped += new EventHandler<StoppedEventArgs>(wavePlayer_PlaybackStopped);

                wavePlayer.Play();
            }

            else wavePlayer.Stop();
        }

        /// <summary>
        /// Method called when wave player has finish playing a sound, or when it is stopped.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            playATISButton.Text = "Play ATIS";
        }

        /// <summary>
        /// Method called when build ATIS button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void buildATISButton_Click(object sender, EventArgs e)
        {
            String line = String.Empty;

            try
            {
                using (StreamReader sr = new StreamReader(atisehamFileTextBox.Text)) line = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                return;
            }

            string[] fileLines = line.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            List<String> linesWithItem = new List<String>(fileLines);

            while (linesWithItem.Last().Equals(String.Empty)) linesWithItem.RemoveAt(linesWithItem.Count() - 1);

            Dictionary<String, String> records = new Dictionary<String, String>();

            foreach (String s in linesWithItem)
            {
                if (!(s.StartsWith("ITEM")))
                {
                    String[] split = Regex.Split(s, @":");

                    records.Add(split[1], split[2]);
                }
            }

            List<String> textToPlay = new List<String>();

            string[] result = Regex.Split(dutchVACCATISGenerator.outputTextBox.Text, @"[\[-\]]");

            foreach(String s in result)
            {
                if (!s.Equals(String.Empty))
                {
                    if (s.All(Char.IsDigit))
                    {
                        String[] splitArray = Regex.Split(s, @"(?=[0-9])");

                        foreach (String split in splitArray)
                        {
                            if (!split.Equals(String.Empty)) textToPlay.Add(split);
                        }
                    }
                    else textToPlay.Add(s);
                }
            }

            buildATISbackgroundWorker.RunWorkerAsync(new Object[] { textToPlay, records });
        }

        /// <summary>
        /// Method called when build ATIS background worker is started.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void buildATISbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] buffer = new byte[1024];

            WaveFileWriter waveFileWriter = null;

            try
            {
                int i = 0;

                foreach (string sourceFile in ((e.Argument as Object[])[0] as List<String>))
                {
                    using (WaveFileReader reader = new WaveFileReader(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\" + ((e.Argument as Object[])[1] as Dictionary<String, String>)[sourceFile.ToLower()]))
                    {
                        if (waveFileWriter == null) waveFileWriter = new WaveFileWriter(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav", reader.WaveFormat);
                        
                        else
                        {
                            if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat)) throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                        }

                        int read;

                        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0) waveFileWriter.Write(buffer, 0, read);
                    }

                    int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
                    buildATISbackgroundWorker.ReportProgress(percentage);
                    i++;
                }
            }
            finally
            {
                if (waveFileWriter != null) waveFileWriter.Dispose();
            }
        }

        /// <summary>
        /// Method called when build ATIS background worker posts a progress update.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void buildATISbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
