using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Enums
{
    public enum DoIfFileExists
    {
        /// <summary>
        /// Nebude přidávat nic, 
        /// </summary>
        Overwrite,
        /// <summary>
        /// Nechá stávající soubor, nový obsah se zahodí
        /// </summary>
        DoNothing
    }
}
