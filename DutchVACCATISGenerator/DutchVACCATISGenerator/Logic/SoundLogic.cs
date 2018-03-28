using DutchVACCATISGenerator.Types.Application;
using NAudio.Wave;
using System.IO;

namespace DutchVACCATISGenerator.Logic
{
    public interface ISoundLogic
    {
        void Build(string atisFile = null);
        void Play(string atisFile);
        void Stop();
    }

    public class SoundLogic : ISoundLogic
    {
        private AudioFileReader audioFileReader;
        private IWavePlayer wavePlayer;

        public void Build(string atisFile = null)
        {

        }

        public void Play(string atisFile)
        {
            if (wavePlayer?.PlaybackState == PlaybackState.Playing)
            {
                wavePlayer.Stop();
                return;
            }

            //Try to read atis.wav file.
            try
            {
                audioFileReader = new AudioFileReader(Path.GetDirectoryName(atisFile) + "\\atis.wav");
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            //Initialize wavePlayer.
            wavePlayer = new WaveOut(WaveCallbackInfo.FunctionCallback());
            wavePlayer.Init(audioFileReader);
            wavePlayer.PlaybackStopped += PlaybackStopped;

            //Play the atis.wav file.
            wavePlayer.Play();
        }

        public void Stop()
        {
            wavePlayer.Stop();
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            ApplicationEvents.PlaybackStopped(sender, e);

            wavePlayer.Dispose();
            audioFileReader.Dispose();
        }



        /// <summary>
        /// Build atis.wav file.
        /// </summary>
        /// <param name="atisSamples">List<String> ATIS samples to build atis.wav from</String></param>
        //public void BuildAtis(List<String> atisSamples)
        //{
        //    //Try to read atiseham.txt and start the build ATIS background worker.
        //    try
        //    {
        //        String line = String.Empty;

        //        //If path to atiseham.txt is not set.
        //        if (atisehamFileTextBox.Text.Trim().Equals(String.Empty))
        //        {
        //            MessageBox.Show("No path to atiseham.txt provided.", "Error");

        //            //Force user to select atiseham.txt.
        //            browseButton.PerformClick();
        //        }

        //        //Try to open and read the atisamples.txt file.
        //        try
        //        {
        //            using (StreamReader sr = new StreamReader(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\samples\\ehamsamples.txt")) line = sr.ReadToEnd();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(String.Format("Unable to open atiseham.txt. Check if the correct atiseham.txt file is selected.\n\nError: {0}", ex.Message), "Error");

        //            //Re-enable the play ATIS button if reading of the atiseham.text has failed.
        //            playATISButton.Enabled = true;
        //            return;
        //        }

        //        //Split read atiseham.txt file on end of line.
        //        string[] fileLines = line.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        //        //Initialize new List of Strings with the split array.
        //        List<String> linesWithItem = new List<String>(fileLines);

        //        //Remove any empty entries at the end of the linesWithItem list.
        //        while (linesWithItem.Last().Equals(String.Empty)) linesWithItem.RemoveAt(linesWithItem.Count() - 1);

        //        //Create new dictionary to store .wav files value in. I.E.: a = ehamatis1_a.wav
        //        Dictionary<String, String> records = new Dictionary<String, String>();

        //        //Add linesWithItem items to the records dictionary.
        //        foreach (String s in linesWithItem)
        //        {
        //            //Add item to records dictionary if it starts with RECORD.
        //            if (s.StartsWith("RECORD"))
        //            {
        //                String[] split = Regex.Split(s, @":");

        //                records.Add(split[1], split[2]);
        //            }
        //        }

        //        //Start build ATIS backgroundWorker to start building the atis.wav file.
        //        buildATISbackgroundWorker.RunWorkerAsync(new Object[] { atisSamples, records });


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(String.Format("Unable to build ATIS. Check if the right atiseham.txt is selected.\n\nError: {0}", ex.Message), "Error"); return;
        //    }
        //}

        /// <summary>
        /// Method called when build ATIS background worker is started.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        //private void buildATISbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    byte[] buffer = new byte[1024];

        //    WaveFileWriter waveFileWriter = null;

        //    //If file is in use by another process.
        //    if (IsFileLocked(new FileInfo(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav")))
        //    {
        //        MessageBox.Show("Cannot generate new atis.wav file. File does not exist or is in use by another process.", "Error"); return;
        //    }

        //    //Try to generate and build atis.wav.
        //    try
        //    {
        //        int i = 0;

        //        //For each String in textToPlay list.
        //        foreach (string sourceFile in ((e.Argument as Object[])[0] as List<String>))
        //        {
        //            //Open WaveFileReader to write to atis.wav.
        //            try
        //            {
        //                //Using the WaveFileReader, get the file to write to the atis.wave from the records list.
        //                using (WaveFileReader reader = new WaveFileReader(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\samples\\" + ((e.Argument as Object[])[1] as Dictionary<String, String>)[sourceFile]))
        //                {
        //                    //Initialize new WaveFileWriter.
        //                    if (waveFileWriter == null) waveFileWriter = new WaveFileWriter(Path.GetDirectoryName(atisehamFileTextBox.Text) + "\\atis.wav", reader.WaveFormat);

        //                    else
        //                    {
        //                        //If loaded .wav does not watch the format of the atis.wav output file.
        //                        if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat)) throw new InvalidOperationException("Can't concatenate .wav files that don't share the same format");
        //                    }

        //                    int read;

        //                    //Write loaded .wav file to atis.wav.
        //                    while ((read = reader.Read(buffer, 0, buffer.Length)) > 0) waveFileWriter.Write(buffer, 0, read);
        //                }

        //                //Update progress bar by sending an report progress call to the build ATIS background worker.
        //                int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
        //                buildATISbackgroundWorker.ReportProgress(percentage);
        //                i++;
        //            }
        //            catch (KeyNotFoundException)
        //            {
        //                //Update progress bar by sending an report progress call to the build ATIS background worker.
        //                int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
        //                buildATISbackgroundWorker.ReportProgress(percentage);
        //                i++;
        //                continue;
        //            }
        //            catch (InvalidOperationException)
        //            {
        //                //Update progress bar by sending an report progress call to the build ATIS background worker.
        //                int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
        //                buildATISbackgroundWorker.ReportProgress(percentage);
        //                i++;
        //                continue;
        //            }
        //            catch (FileNotFoundException ex)
        //            {
        //                MessageBox.Show(ex.Message + "\n\natis.wav will be generated without this file.", "Error");

        //                //Update progress bar by sending an report progress call to the build ATIS background worker.
        //                int percentage = (i + 1) * 100 / ((e.Argument as Object[])[0] as List<String>).Count();
        //                buildATISbackgroundWorker.ReportProgress(percentage);
        //                i++;
        //                continue;
        //            }
        //        }
        //    }
        //    //Dispose waveFileWriter when finished.
        //    finally
        //    {
        //        if (waveFileWriter != null) waveFileWriter.Dispose();
        //    }
        //}
    }
}
