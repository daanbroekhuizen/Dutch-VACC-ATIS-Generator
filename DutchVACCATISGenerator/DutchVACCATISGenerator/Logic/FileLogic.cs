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
            //Delete temp folder if exists.
            if (Directory.Exists($@"{Path.GetDirectoryName(executablePath)}\temp"))
                Directory.Delete($@"{executablePath}\temp", true);
        }
    }
}
