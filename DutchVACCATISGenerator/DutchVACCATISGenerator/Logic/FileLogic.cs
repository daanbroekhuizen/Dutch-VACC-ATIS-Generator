using System.IO;
using System.Reflection;

namespace DutchVACCATISGenerator.Logic
{
    public interface IFileLogic
    {
        void DeleteInstallerFiles();
    }

    public class FileLogic : IFileLogic
    {
        public void DeleteInstallerFiles()
        {
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
