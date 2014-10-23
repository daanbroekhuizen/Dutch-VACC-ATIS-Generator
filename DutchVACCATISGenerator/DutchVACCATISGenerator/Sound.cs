using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// Sound class.
    /// </summary>
    public partial class Sound : Form
    {
        private AudioFileReader audio;
        private DutchVACCATISGenerator dutchVACCATISGenerator;
        public IWavePlayer wavePlayer { get; set; }

        /// <summary>
        /// Constructor of Sound. Initializes new instance of Sound.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        public Sound(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            InitializeComponent();

            this.dutchVACCATISGenerator = dutchVACCATISGenerator;

            //Enable the build ATIS button if the ATIS has already been build.
            if (dutchVACCATISGenerator.outputTextBox.Text.Trim().Equals(String.Empty)) buildATISButton.Enabled = false;

            //Get and set the property of the path to the ATIS folder if it has been saved before.
            if (!Properties.Settings.Default.atisehamPath.Equals(String.Empty)) atisehamFileTextBox.Text = Properties.Settings.Default.atisehamPath;
            //Else sets the path to the user document folder + \EuroScope\atis\atiseham.txt.
            else atisehamFileTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\EuroScope\atis\atiseham.txt";
        }

        /// <summary>
        /// Sets the RunwayInform form position relative to DutchVACCATISGenerator form.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        public void showRelativeToDutchVACCATISGenerator(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            this.Left = dutchVACCATISGenerator.Left;
            this.Top = dutchVACCATISGenerator.Bottom;
            this.Refresh();
        }

        /// <summary>
        /// Method called when Sound form is loaded.
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
            //If user has selected a file.
            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                atisehamFileTextBox.Text = openFileDialog.FileName;

                //Save selected path to properties files.
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
            //If ATIS is not playing.
            if (playATISButton.Text.Equals("Play ATIS"))
            {
                //If path to atiseham.txt is not set.
                if (atisehamFileTextBox.Text.Trim().Equals(String.Empty))
                {
                    MessageBox.Show("No path to atiseham.txt provided.", "Warning");

                    //Open file dialog for user to set the path to atiseham.txt.
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        atisehamFileTextBox.Text = openFileDialog.FileName;

                        Properties.Settings.Default.atisehamPath = atisehamFileTextBox.Text;
                        Properties.Settings.Default.Save();
                    }
                    //If user didn't selected a file
                    else return;
                }

                //Change the text of play ATIS button to...
                playATISButton.Text = "Stop ATIS";
                
                //Try to read atis.wav file.
                try
                {
                    audio = new AudioFileReader(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav");
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(String.Format("Unable to play ATIS. Check if the atiseham.txt file is in the same folder as the ATIS sounds (atis.wav, etc.).\n\nError: {0}", ex.Message), "Error"); return;
                }
                
                //Initialize wavePlayer.
                wavePlayer = new WaveOut(WaveCallbackInfo.FunctionCallback());
                wavePlayer.Init(audio);
                wavePlayer.PlaybackStopped += new EventHandler<StoppedEventArgs>(wavePlayer_PlaybackStopped);

                //Disable build ATIS button, prevents user from trying to build and play atis.wav at the same time.
                buildATISButton.Enabled = false;

                //Play the atis.wav file.
                wavePlayer.Play();
            }

            //If ATIS is playing, stop playing.
            else wavePlayer.Stop();
        }

        /// <summary>
        /// Method called when wave player has finish playing a sound, or when it is stopped.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //Set play ATIS button text to...
            playATISButton.Text = "Play ATIS";

            //Re-enable the build ATIS button.
            buildATISButton.Enabled = true;
            
            //Dispose the AudioFileReader to release the file.
            try
            {
                audio.Dispose();
            }
            catch(Exception) { }

            //Dispose the wavePlayer.
            wavePlayer.Dispose();
        }

        /// <summary>
        /// Method called when build ATIS button is clicked.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void buildATISButton_Click(object sender, EventArgs e)
        {
            //Try to read atiseham.txt and start the build ATIS background worker.
            try
            {
                String line = String.Empty;

                //If path to atiseham.txt is not set.
                if (atisehamFileTextBox.Text.Trim().Equals(String.Empty))
                {
                    MessageBox.Show("No path to atiseham.txt provided.", "Error");
                   
                    //Force user to select atiseham.txt.
                    browseButton.PerformClick();
                }

                //Disable play ATIS button, prevents user from trying to build and play atis.wav at the same time.
                playATISButton.Enabled = false;

                //Try to open and read the atiseham.txt file.
                try
                {
                    using (StreamReader sr = new StreamReader(atisehamFileTextBox.Text)) line = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Unable to open atiseham.txt. Check if the correct atiseham.txt file is selected.\n\nError: {0}", ex.Message), "Error"); 
                    
                    //Re-enable the play ATIS button if reading of the atiseham.text has failed.
                    playATISButton.Enabled = true;
                    return;
                }

                //Split read atiseham.txt file on end of line.
                string[] fileLines = line.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                //Initialize new List of Strings with the split array.
                List<String> linesWithItem = new List<String>(fileLines);

                //Remove any empty entries at the end of the linesWithItem list.
                while (linesWithItem.Last().Equals(String.Empty)) linesWithItem.RemoveAt(linesWithItem.Count() - 1);

                //Create new dictionary to store .wav files value in. I.E.: a = ehamatis1_a.wav
                Dictionary<String, String> records = new Dictionary<String, String>();

                //Add linesWithItem items to the records dictionary.
                foreach (String s in linesWithItem)
                {
                    //Add item to records dictionary if it starts with RECORD.
                    if (s.StartsWith("RECORD"))
                    {
                        String[] split = Regex.Split(s, @":");

                        records.Add(split[1], split[2]);
                    }
                }
                
                //Create new List of Strings to store parts to play in.
                List<String> textToPlay = new List<String>();

                //Split ATIS output to parts to play.
                string[] result = Regex.Split(dutchVACCATISGenerator.outputTextBox.Text, @"[\[-\]]");

                //Add parts to play from array to textToPlay list.
                foreach (String s in result)
                {
                    //If s is not empty.
                    if (!s.Equals(String.Empty))
                    {
                        //If s is digits only.
                        if (s.All(Char.IsDigit))
                        {
                            //Split s up in separate digits.
                            String[] splitArray = Regex.Split(s, @"(?=[0-9])");

                            //Add each digit to textToPlay list.
                            foreach (String split in splitArray)
                            {
                                if (!split.Equals(String.Empty)) textToPlay.Add(split);
                            }
                        }
                        //If s is text only.
                        else textToPlay.Add(s);
                    }
                }

                //Start build ATIS backgroundWorker to start building the atis.wav file.
                buildATISbackgroundWorker.RunWorkerAsync(new Object[] { textToPlay, records });
            }
            catch (Exception ex) 
            {
                MessageBox.Show(String.Format("Unable to build ATIS. Check if the right atiseham.txt is selected.\n\nError: {0}", ex.Message), "Error"); return;
            }
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

            //If file is in use by another process.
            if (IsFileLocked(new FileInfo(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav")))
            {
                MessageBox.Show("Cannot generate new atis.wav file. File does not exists or is in use by another process.", "Error"); return;
            }

            //Try to generate and build atis.wav.
            try
            {
                int i = 0;

                //For each String in textToPlay list.
                foreach (string sourceFile in ((e.Argument as Object[])[0] as List<String>))
                {
                    //Open WaveFileReader to write to atis.wav.
                    try
                    {
                        //Using the WaveFileReader, get the file to write to the atis.wave from the records list.
                        using (WaveFileReader reader = new WaveFileReader(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\" + ((e.Argument as Object[])[1] as Dictionary<String, String>)[sourceFile.ToLower()]))
                        {
                            //Initialize new WaveFileWriter.
                            if (waveFileWriter == null) waveFileWriter = new WaveFileWriter(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav", reader.WaveFormat);

                            else
                            {
                                //If loaded .wav does not watch the format of the atis.wav output file.
                                if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat)) throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                            }

                            int read;

                            //Write loaded .wav file to atis.wav.
                            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0) waveFileWriter.Write(buffer, 0, read);
                        }

                        //Update progress bar by sending an report progress call to the build ATIS background worker.
                        int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
                        buildATISbackgroundWorker.ReportProgress(percentage);
                        i++;
                    }
                    catch(KeyNotFoundException)
                    {
                        //Update progress bar by sending an report progress call to the build ATIS background worker.
                        int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
                        buildATISbackgroundWorker.ReportProgress(percentage);
                        i++;
                        continue;
                    }
                    catch(InvalidOperationException)
                    {
                        //Update progress bar by sending an report progress call to the build ATIS background worker.
                        int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
                        buildATISbackgroundWorker.ReportProgress(percentage);
                        i++;
                        continue;
                    }
                    catch(FileNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message + "\n\natis.wav will be generated without this file.", "Error");

                        //Update progress bar by sending an report progress call to the build ATIS background worker.
                        int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
                        buildATISbackgroundWorker.ReportProgress(percentage);
                        i++;
                        continue;
                    }
                }
            }
            //Dispose waveFileWriter when finished.
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

        /// <summary>
        /// Method called when build ATIS background workers has completed its work.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void buildATISbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            playATISButton.Enabled = true;
        }

        /// <summary>
        /// Method called if Sound form is closed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void Sound_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dutchVACCATISGenerator.soundButton.Text.Equals("▲")) dutchVACCATISGenerator.soundButton.Text = "▼";

            dutchVACCATISGenerator.soundToolStripMenuItem.BackColor = SystemColors.Control;
            
            if(wavePlayer != null)
                wavePlayer.Stop();
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
    }
}