using DutchVACCATISGenerator.Extensions;
using DutchVACCATISGenerator.Types;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator.Logic
{
    public interface ISoundLogic
    {
        /// <summary>
        /// Builds an ATIS.
        /// </summary>
        /// <param name="atisFile">Path to ATIS descriptor file</param>
        /// /// <param name="ATISSamples">List of ATIS samples</param>
        Task Build(string atisFile, List<string> ATISSamples);

        /// <summary>
        /// Plays or stops the ATIS.
        /// </summary>
        /// <param name="atisFile">Path to ATIS descriptor file.</param>
        void Play(string atisFile);

        /// <summary>
        /// Stops the ATIS from playing.
        /// </summary>
        void Stop();
    }

    public class SoundLogic : ISoundLogic
    {
        private AudioFileReader audioFileReader;
        private IWavePlayer wavePlayer;

        public Task Build(string atisFile, List<string> ATISSamples)
        {
            if (string.IsNullOrWhiteSpace(atisFile) || !File.Exists(atisFile))
                throw new FileNotFoundException(atisFile);

            ///Start build asynchronous.
            return BuildAsync(atisFile, ATISSamples, GetRecords(atisFile));
        }

        public void Play(string atisFile)
        {
            //Check if player is playing (else stop is clicked).
            if (wavePlayer?.PlaybackState == PlaybackState.Playing)
            {
                Stop();
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
            wavePlayer?.Stop();
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            ApplicationEvents.PlaybackStopped(sender, e);

            wavePlayer.Dispose();
            audioFileReader.Dispose();
        }

        private Dictionary<string, string> GetRecords(string atisFile)
        {
            var text = File.ReadAllText($"{Path.GetDirectoryName(atisFile)}\\samples\\ehamsamples.txt");

            var lines = SplitOnAndRemoveEmptyLines(text);

            //Create new dictionary to store .wav files value in. I.E.: a = ehamatis1_a.wav
            var records = new Dictionary<string, string>();

            //Add linesWithItem items to the records dictionary.
            foreach (string line in lines)
            {
                //Add item to records dictionary if it starts with RECORD.
                if (line.StartsWith("RECORD"))
                {
                    var regexSplit = Regex.Split(line, @":");

                    records.Add(regexSplit[1], regexSplit[2]);
                }
            }

            return records;
        }

        private List<string> SplitOnAndRemoveEmptyLines(string text)
        {
            //Split read atiseham.txt file on end of line.
            var fileLines = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            //Initialize new List of Strings with the split array.
            var linesWithItem = new List<String>(fileLines);

            //Remove any empty entries at the end of the linesWithItem list.
            while (linesWithItem.Last().Equals(String.Empty))
                linesWithItem.RemoveAt(linesWithItem.Count() - 1);

            return linesWithItem;
        }

        private async Task BuildAsync(string atisFile, List<string> ATISSamples, Dictionary<string, string> records)
        {
            await Task.Run(() =>
             {
                 ApplicationEvents.BuildAITSStarted();

                 if (new FileInfo($"{Path.GetDirectoryName(atisFile)}\\atis.wav").IsLocked())
                     throw new IOException("Cannot generate new atis.wav file. File does not exist or is in use by another process.");

                 int i = 0;

                 WaveFileWriter waveFileWriter = null;

                 //Try to generate and build atis.wav.
                 try
                 {
                     foreach (string sample in ATISSamples)
                     {
                         //Open WaveFileReader to write to atis.wav.
                         try
                         {
                             //Using the WaveFileReader, get the file to write to the atis.wave from the records list.
                             using (WaveFileReader reader = new WaveFileReader($"{Path.GetDirectoryName(atisFile)}\\samples\\{records[sample]}"))
                             {
                                 //Initialize new WaveFileWriter.
                                 if (waveFileWriter == null)
                                     waveFileWriter = new WaveFileWriter($"{Path.GetDirectoryName(atisFile)}\\atis.wav", reader.WaveFormat);

                                 else
                                 {
                                     //If loaded .wav does not watch the format of the atis.wav output file.
                                     if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                                         throw new InvalidOperationException("Can't concatenate .wav files that don't share the same format");
                                 }

                                 var buffer = new byte[1024];
                                 int read;

                                 //Write loaded .wav file to atis.wav.
                                 while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                                     waveFileWriter.Write(buffer, 0, read);
                             }
                         }
                         catch (KeyNotFoundException)
                         {
                             //Do nothing...
                         }
                         catch (InvalidOperationException)
                         {
                             //Do nothing...
                         }
                         catch (FileNotFoundException)
                         {
                             //Do nothing...
                         }

                         RaiseProgressChanged(i, ATISSamples);
                         i++;
                     }
                 }
                 finally
                 {
                     //Dispose waveFileWriter when finished.
                     if (waveFileWriter != null)
                         waveFileWriter.Dispose();
                 }

                 //Raise event.
                 ApplicationEvents.BuildAITSCompleted();
             });
        }

        private void RaiseProgressChanged(int i, List<string> ATISSamples)
        {
            var percentage = (i + 1) * 100 / ATISSamples.Count();

            //Update progress bar.
            ApplicationEvents.BuildAITSProgressChanged(percentage);
        }
    }
}
