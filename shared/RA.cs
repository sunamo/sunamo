using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using sunamo;
using sunamo.Essential;
/// <summary>
/// 
/// </summary>
public class RA
    {
        protected static List<string> valuesInKey = null;
        protected static RegistryKey m = null;
        static RA()
        {
            //HKEY_LOCAL_MACHINE\SOFTWARE
            RegistryKey hklm = Registry.CurrentUser;
            RegistryKey sw = hklm.OpenSubKey("SOFTWARE", true);

            m = sw.OpenSubKey(ThisApp.Name, true);
            if (m == null)
            {
                m = sw.CreateSubKey(ThisApp.Name);
                valuesInKey = new List<string>();
            }
            else
            {
                valuesInKey = new List<string>(m.GetValueNames());
            }
        
        }

        /// <summary>
        /// Abstraktni uz nikdy nedelej, proste musi tu metodu prekryt a oznacit za static a zavolat v statickem konstruktoru, pokud chces ji volat ihned pri vytvoreni staticke instance nebo ji chces treba volat v F1.
        /// Trida vraci string aby jsi ji mohl inicializovat treba A1.
        /// </summary>
        public virtual string CreateDefaultValues()
        {
            return "";
        }

        public static void WriteToKeyInt(string klic, int hodnota)
        {
            m.SetValue(klic, hodnota, RegistryValueKind.DWord);
        }

        public static int ReturnValueInt(string klic)
        {
            int c;
            object o = m.GetValue(klic);
            if (o != null)
            {
                if (int.TryParse(o.ToString(), out c))
                {
                    return c;
                }
            }

            return -1;
        }

        /// <summary>
        /// Pokud klk A1 nebude nalezen, G "".
        /// </summary>
        /// <param name="Login"></param>
        public static string ReturnValueString(string Login)
        {
            return m.GetValue(Login, "", RegistryValueOptions.None).ToString();
        }

        public static void WriteToKeyString(string klic, string hodnota)
        {
            m.SetValue(klic, hodnota, RegistryValueKind.String);
        }

        public static byte[] ReturnValueByteArray(string Login)
        {
            return (byte[])m.GetValue(Login, null, RegistryValueOptions.None);
        }

        public static void WriteToKeyByteArray(string klic, byte[] hodnota)
        {
            m.SetValue(klic, hodnota, RegistryValueKind.Binary);
        }

        public static void SaveToKeyBool(string klic, object hodnota)
        {
            m.SetValue(klic, hodnota.ToString(), RegistryValueKind.String);
        }

        public static bool ReturnValueBool(string klic)
        {
            string s = m.GetValue(klic, "").ToString();
            if (s == "True")
            {
                return true;
            }
            return false;
        }
    }