using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Values
{
    public class RepairMobileValues
    {
        public static List<string> allRepairServicesOva = null;

        static RepairMobileValues()
        {
            allRepairServicesOva = new List<string>(CA.ToEnumerable("levnejmobil.cz", "bettacomp.cz", "tadyspravismobil.cz", "atcmobile.cz", "iphoneostrava.cz", "mobilcity.cz", "iloveservis.cz", "prontmobil.cz", "mujmobilnitelefon.cz"));
        }
    }
}
