using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    public class IPAddressHelper
    {
        /// <summary>
        /// Vrátí null pokud cokoliv nebude sedět
        /// </summary>
        /// <param name="ip2"></param>
        /// <returns></returns>
        public static byte[] GetIPAddressInArray(string ip2)
        {
            byte[] ip = null;
            string[] ips = SH.Split(ip2, ".");
            if (ips.Length == 4)
            {
                ip = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    byte b = 0;
                    if (!byte.TryParse(ips[i], out b))
                    {
                        return null;
                    }
                    ip[i] = b;
                }
            }
            return ip;
        }
    }
}
