using System.IO;

namespace DutchVACCATISGenerator.Extensions
{
    public static class FileExtensions
    {
        public static bool IsLocked(this FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //File locked.
                return true;
            }
            finally
            {
                if (stream != null) stream.Close();
            }

            //File not locked.
            return false;
        }
    }
}
