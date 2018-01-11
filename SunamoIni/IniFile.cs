using desktop;
using sunamo;
using desktop.Essential;
using System;
using System.Runtime.InteropServices;
using System.Text;


/// <summary>
/// Create a New INI file to store or load data
/// </summary>
public class IniFile
    {
        public string path;

        //IniParser ip = null;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        SharpConfig.Configuration conf = null;

        public IniFile(string INIPath)
        {
            path = INIPath;
            //ip = new IniParser(path);
            try
            {
                conf = SharpConfig.Configuration.LoadFromFile(path);
            }
            catch (Exception ex)
            {
                   
            }
        }

        /// <summary>
        /// Zápis probíhá jen pomocí W32 metod, tam pokud vím se s tím ještě nevyskytly problémy
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Vždy používá sharp config, předtím vždy používala W32 metody
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue( string Section, string Key)
        {
            return IniReadValue(true, Section, Key);
        }

        public string IniReadValueSharpConfig(string Section, string Key)
        {
            return IniReadValue(true, Section, Key);
        }

        /// <summary>
        /// A1 zda se má použít SharpConfig
        /// </summary>
        /// <param name="useIniParser"></param>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(bool useIniParser, string Section, string Key)
        {
            if (useIniParser)
            {
                if (conf != null)
                {
                    return conf[Section][Key].StringValue;
                }
                return "";
            }
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            int.MaxValue, this.path);
            return temp.ToString();
        }

        public static IniFile InStartupPath()
        {
            return new IniFile(desktop.AppPaths.GetFileInStartupPath(ThisApp.Name + ".ini"));
        }
    }
