using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    /// <summary>
    /// Underscore
    /// </summary>
    public class DTHelperUs
    {
        #region ToString
        /// <summary>
        /// yyyy_mm_dd 
        /// </summary>
        /// <param name="dt"></param>
        
        public static DateTime? FileNameToDateTime(string fnwoe)
        {
            var sp = SH.Split(fnwoe, AllStrings.us);
            var dd = CA.ToInt(sp, 6);
            if (dd == null)
            {
                return null;
            }
            return new DateTime(dd[0], dd[1], dd[2], dd[3], dd[4], 0);
        }


        #endregion
    }
}
