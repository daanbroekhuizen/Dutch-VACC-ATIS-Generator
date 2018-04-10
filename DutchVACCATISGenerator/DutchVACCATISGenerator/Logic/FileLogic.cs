using DutchVACCATISGenerator.Extensions;
using System.IO;
using System.Reflection;

namespace DutchVACCATISGenerator.Logic
{
    public interface IFileLogic
    {
        /// <summary>
        /// Deletes the installer files.
        /// </summary>
        /// <param name="removeZips">Remove downloaded zips</param>
        void DeleteInstallerFiles(bool removeZips);
    }

    public class FileLogic : IFileLogic
    {
        public void DeleteInstallerFiles(bool removeZips)
        {
            if (removeZips)
            {
                //Clean up zips.
                var zips = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.zip");

                foreach (var zip in zips)
                {
                    if (!(new FileInfo(zip).IsLocked()))
                        File.Delete(zip);
                }
            }

            //Clean up temp folder.
            var tempFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp";

            //Delete temp folder if exists.
            if (Directory.Exists(tempFolder))
            {
                //Remove hidden attribute.
                DirectoryInfo directoryInfo = Directory.CreateDirectory(tempFolder);
                directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

                Directory.Delete(tempFolder, true);
            }
        }
    }
}
