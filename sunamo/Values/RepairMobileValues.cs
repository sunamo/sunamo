using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Values
{
    public class RepairMobileValues
    {
        public static List<string> allRepairServicesOva = null;
        public static List<string> allRepairKitShops = null;
        public static List<string> repairKitShopsFreePickupOstrava = null;

        static RepairMobileValues()
        {
            allRepairServicesOva = new List<string>(CA.ToEnumerable("levnejmobil.cz", "bettacomp.cz", "tadyspravismobil.cz", "atcmobile.cz", "iphoneostrava.cz", "mobilcity.cz", "iloveservis.cz", "prontmobil.cz", "mujmobilnitelefon.cz"));
            allRepairKitShops = new List<string>(CA.ToEnumerable("mobil-obaly.cz", "deltamobile.cz", "e-shop.vmcomp.cz", "gc-baterie.cz", "dianashop.cz", "neven.cz", "multo.cz", "jenifer.cz", "naradi-pajky.cz", "servatech.cz", "dilyamobily.cz", "f-mobil.cz", "mobilmax.cz", "mobilprovas.cz", "servisnidily.cz", "matrixmobil.cz", "lcdpartner.com", "stmobil.cz", "ptmobil.cz", "pajtech.cz"));
            repairKitShopsFreePickupOstrava = new List<string>(CA.ToEnumerable("levnejmobil.cz", "hotair.cz", "vsekmobilu.cz"));
        }
    }
}