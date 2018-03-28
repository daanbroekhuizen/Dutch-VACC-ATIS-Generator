using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator.Extensions
{
    public static class FileExstensions
    {
        public static bool FileIsLocked(this FileInfo file)
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
