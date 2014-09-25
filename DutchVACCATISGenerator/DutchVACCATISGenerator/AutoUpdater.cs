using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    public partial class AutoUpdater : Form
    {
        private String fileName;
        private String executablePath;

        public AutoUpdater()
        {
            InitializeComponent();

            executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";

            downloadLatestVersion();
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webClient_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

