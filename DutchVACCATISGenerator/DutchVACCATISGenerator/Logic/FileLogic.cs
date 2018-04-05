using System.IO;

namespace DutchVACCATISGenerator.Logic
{
    public interface IFileLogic
    {
        void DeleteInstallerFiles(string executablePath);
    }

    public class FileLogic : IFileLogic
    {
        public void DeleteInstallerFiles(string executablePath)
        {
            //Remove hidden attribute.
            DirectoryInfo directoryInfo = Directory.CreateDirectory($@"{Path.GetDirectoryName(executablePath)}\temp");
            directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

            //Delete temp folder if exists.
            if (Directory.Exists($@"{Path.GetDirectoryName(executablePath)}\temp"))
                Directory.Delete($@"{executablePath}\temp", true);
        }
    }
}
