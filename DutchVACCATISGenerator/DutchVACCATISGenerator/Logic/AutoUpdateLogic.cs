using DutchVACCATISGenerator.Extensions;
using DutchVACCATISGenerator.Helpers;
using DutchVACCATISGenerator.Types;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Logic
{
    public interface IAutoUpdateLogic
    {
        /// <summary>
        /// Download latest version of Dutch VACC ATIS Generator.
        /// </summary>
        Task AutoUpdate();
    }

    public class AutoUpdateLogic : IAutoUpdateLogic
    {
        private readonly IFileLogic fileLogic;

        private string executablePath;
        private string zipName;

        public AutoUpdateLogic(IFileLogic fileLogic)
        {
            this.fileLogic = fileLogic;
        }

        public async Task AutoUpdate()
        {
            //Set path of executable.
            executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";

            //Start downloading latest version.
            await DownloadLatestVersion();
        }

        private async Task DownloadLatestVersion()
        {
            try
            {
                var webClient = new WebClient();

                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ApplicationEvents.DownloadProgressChanged);

                zipName = GetZipName();

                //Download the zip file.
                await webClient.DownloadFileTaskAsync(new Uri("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/" + zipName), executablePath + zipName);
            }
            catch
            {
                throw;
            }
        }

        private string GetZipName()
        {
            //Request zip file name.
#if DEBUG
            var request = WebRequest.Create("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/Version/filename2.php");
#else
            var request = WebRequest.Create("http://daanbroekhuizen.com/Dutch VACC/Dutch VACC ATIS Generator/Version/filename.php");
#endif
            var response = request.GetResponse();

            //Read zip file name.
            return new StreamReader(response.GetResponseStream()).ReadToEnd().Trim();
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                //Delete installer files.
                fileLogic.DeleteInstallerFiles(executablePath);

                //Extract zip.
                using (var zipFile = ZipFile.Open(executablePath + zipName, ZipArchiveMode.Read))
                {
                    zipFile.ExtractToDirectory($"{executablePath}temp", true);
                }

                //Set temp folder to be hidden.
                DirectoryInfo directoryInfo = Directory.CreateDirectory(executablePath + @"\temp");
                directoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly;

                //Delete zip.
                File.Delete(executablePath + zipName);

                //Start setup.
                if (!UnitTestHelper.Detect_IsUnitTestRunning())
                    Process.Start(executablePath + @"\temp\" + "Dutch VACC ATIS Generator - Setup.exe");

                //Exit program to run setup.
                Application.Exit();
            }
            catch
            {
                throw;
            }
        }
    }
}
