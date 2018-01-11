using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    class PhotoFileNameConvention
    {
        public string text = "";
        public uint? number = null;
        #region Constanty - začátek názvů souborů fotek které jsou ve konvenci písmeno/a + číslo - všechno musím kontrolovat také na malé
        public const string DSC = "DSC";
        public const string DSC_ = "DSC_";
        
        public const string DSCN = "DSCN";
        public const string YDXJ = "YDXJ";
        public const string P = "P";
        public const string DSC__ = "DSC ";

        
        #endregion
        public PhotoFileNameConvention(string fnwoe)
        {


        }
    }
}
