using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// AutoUpdater class.
    /// </summary>
    public partial class AutoUpdater : Form
    {
        private String fileName;
        private String executablePath;

        /// <summary>
        /// Constructor of AutoUpdater.
        /// </summary>
        public AutoUpdater()
        {
            InitializeComponent();

            //Set path of executable.
            executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";

            //Start downloading latest version.
            downloadLatestVersion();
        }

        /// <summary>
        /// Download latest version of Dutch VACC ATIS Generator.
        /// </summary>
        private void downloadLatestVersion()
        {
            try
            {
                //Create web client.
                WebClient webClient = new WebClient();

                //Set web client's completed event.
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadCompleted);

                //Set web client's progress changed event.
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_ProgressChanged);

                //Download the zip file.
                webClient.DownloadFileAsync(new Uri("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/" + getZipName()), executablePath + fileName);
            }
            catch(Exception)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Get the name of the zip file te download (version).
        /// </summary>
        /// <returns>Name of the latest zip file.</returns>
        private String getZipName()
        {
            //Request zip file name.
            WebRequest request = WebRequest.Create("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/Version/filename.php");
            WebResponse response = request.GetResponse();

            //Read zip file name.
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            fileName = reader.ReadToEnd();
            
            //Return and trim file name;
            return fileName = fileName.Trim();
        }

        /// <summary>
        /// Web client progress changed event.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void webClient_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Called when web client is finished downloading.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void webClient_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                //Extract zip.
                ZipFile.ExtractToDirectory(executablePath + fileName, executablePath + @"\temp");

                //Set temp folder to be hidden.
                DirectoryInfo directoryInfo = Directory.CreateDirectory(executablePath + @"\temp");
                directoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly; 

                //Delete zip.
                File.Delete(executablePath + fileName);

                //Start setup.
                Process.Start(executablePath + @"\temp\" + "Dutch VACC ATIS Generator - Setup.exe");

                //Exit program to run setup.
                Application.Exit();
            }
            catch(Exception)
            {
                this.Close();
            }
        }
    }
}

