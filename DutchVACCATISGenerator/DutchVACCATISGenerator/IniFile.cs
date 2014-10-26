using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// IniFile class.
    /// </summary>
    public class IniFile
    {
        private string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Constructor of IniFile.
        /// </summary>
        /// <param name="INIPath">string - INI file from</param>
        public IniFile(string INIPath)
        {
            checkIfIniExists(INIPath);

            path = INIPath;
        }

        /// <summary>
        /// Check if ini file exists.
        /// </summary>
        /// <param name="INIPath">string - Path to ini file</param>
        private void checkIfIniExists(string INIPath)
        {
            if (!(File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.ini")))
            {
                string[] lines = { 
                                     "[settings]",
                                     "autofetch=False", 
                                     "autoprocess=False"
                                 };

                File.WriteAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.ini", lines);
            }
        }

        /// <summary>
        /// Write a value to the ini file.
        /// </summary>
        /// <param name="Section">string - Section in file</param>
        /// <param name="Key">string - Key to write value to</param>
        /// <param name="Value">string - Value from key</param>
        private void iniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read a value from the ini file.
        /// </summary>
        /// <param name="Section">string - Section in file</param>
        /// <param name="Key">string - Key to read value from</param>
        /// <returns>string - Value from key</returns>
        private string iniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

        /// <summary>
        /// Write auto fetch value to ini file.
        /// </summary>
        /// <param name="value">bool - Value to write to the ini file</param>
        public void WriteAutoFetchSetting(bool value)
        {
            iniWriteValue("settings", "autofetch", value.ToString());
        }

        /// <summary>
        /// Load auto fetch value from ini file.
        /// </summary>
        /// <returns>bool - Value from the ini file</returns>
        public bool GetAutoFetchSetting()
        {
            return Convert.ToBoolean(iniReadValue("settings", "autofetch"));
        }

        /// <summary>
        /// Write auto process value to ini file.
        /// </summary>
        /// <param name="value">bool - Value to write to the ini file</param>
        public void WriteAutoPorcessSetting(bool value)
        {
            iniWriteValue("settings", "autoprocess", value.ToString());
        }

        /// <summary>
        /// Load auto process value from ini file.
        /// </summary>
        /// <returns>bool - Value from the ini file</returns>
        public bool GetAutoPorcessSetting()
        {
            return Convert.ToBoolean(iniReadValue("settings", "autoprocess"));
        }       
    }
}
