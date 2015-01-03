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
        /// Check if INI file exists.
        /// </summary>
        /// <param name="INIPath">string - Path to INI file</param>
        private void checkIfIniExists(string INIPath)
        {
            if (!(File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.ini")))
            {
                string[] lines = { 
                                     "[settings]",
                                     "autofetch=False", 
                                     "autogenerateatis=False",
                                     "autoloadrunways=False", 
                                     "autoprocess=False",
                                     "eham=False",
                                     "ehrd=False",
                                     "playsound=False",
                                     "randomletter=False"
                                 };

                File.WriteAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.ini", lines);
            }
        }

        /// <summary>
        /// Write a value to the INI file.
        /// </summary>
        /// <param name="Section">string - Section in file</param>
        /// <param name="Key">string - Key to write value to</param>
        /// <param name="Value">string - Value from key</param>
        private void iniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read a value from the INI file.
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
        /// Write auto fetch value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteAutoFetchSetting(bool value)
        {
            iniWriteValue("settings", "autofetch", value.ToString());
        }

        /// <summary>
        /// Load auto fetch value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetAutoFetchSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "autofetch"));
            }
            catch (FormatException)
            {
                WriteAutoFetchSetting(false);
                return false;
            }
        }

        /// <summary>
        /// Write auto process value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteAutoPorcessSetting(bool value)
        {
            iniWriteValue("settings", "autoprocess", value.ToString());
        }

        /// <summary>
        /// Load auto process value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetAutoPorcessSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "autoprocess"));
            }
            catch (FormatException)
            {
                WriteAutoPorcessSetting(false);
                return false;
            }
        }

        /// <summary>
        /// Write auto load runways value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteAutoLoadRunwaysSetting(bool value)
        {
            iniWriteValue("settings", "autoloadrunways", value.ToString());
        }

        /// <summary>
        /// Load auto load runways value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetAutoLoadRunwaysSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "autoloadrunways"));
            }
            catch(FormatException)
            {
                WriteAutoLoadRunwaysSetting(false);
                return false;
            }
        }

        /// <summary>
        /// Write auto generate ATIS value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteAutoGenerateATISSetting(bool value)
        {
            iniWriteValue("settings", "autogenerateatis", value.ToString());
        }

        /// <summary>
        /// Load auto generate ATIS value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetAutoGenerateATISSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "autogenerateatis"));
            }
            catch (FormatException)
            {
                WriteAutoGenerateATISSetting(false);
                return false;
            }
        }
        
        /// <summary>
        /// Write random letter value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteRandomLetterATISSetting(bool value)
        {
            iniWriteValue("settings", "randomletter", value.ToString());
        }

        /// <summary>
        /// Load random letter value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetRandomLetterATISSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "randomletter"));
            }
            catch (FormatException)
            {
                WriteAutoGenerateATISSetting(false);
                return false;
            }
        }


        /// <summary>
        /// Write EHAM value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteEHAMATISSetting(bool value)
        {
            iniWriteValue("settings", "eham", value.ToString());
        }

        /// <summary>
        /// Load EHAM value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetEHAMATISSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "eham"));
            }
            catch (FormatException)
            {
                WriteAutoGenerateATISSetting(false);
                return false;
            }
        }

        /// <summary>
        /// Write EHRD value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WriteEHRDATISSetting(bool value)
        {
            iniWriteValue("settings", "ehrd", value.ToString());
        }

        /// <summary>
        /// Load EHRD value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetEHRDATISSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "ehrd"));
            }
            catch (FormatException)
            {
                WriteAutoGenerateATISSetting(false);
                return false;
            }
        }

        /// <summary>
        /// Write play sound value to INI file.
        /// </summary>
        /// <param name="value">boolean - Value to write to the INI file</param>
        public void WritePlaySoundSetting(bool value)
        {
            iniWriteValue("settings", "playsound", value.ToString());
        }

        /// <summary>
        /// Load play sound value from INI file.
        /// </summary>
        /// <returns>boolean - Value from the INI file</returns>
        public bool GetPlaySoundSetting()
        {
            try
            {
                return Convert.ToBoolean(iniReadValue("settings", "playsound"));
            }
            catch (FormatException)
            {
                WriteAutoGenerateATISSetting(false);
                return false;
            }
        }
    }
}
