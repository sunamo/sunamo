using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Values
{
    public static class GeoCzechRegions
    {
        static List<GeoCzechRegion> czechRegions;

        static void Init()
        {
            czechRegions = new List<GeoCzechRegion>();
            czechRegions.Add(new GeoCzechRegion("A", "Hlavní město Praha", "PHA", "Praha"));
            czechRegions.Add(new GeoCzechRegion("S", "Středočeský", "STČ", "Praha"));
            czechRegions.Add(new GeoCzechRegion("C", "Jihočeský", "JHČ", "České Budějovice"));
            czechRegions.Add(new GeoCzechRegion("P", "Plzeňský", "PLK", "Plzeň"));
            czechRegions.Add(new GeoCzechRegion("K", "Karlovarský", "KVK", "Karlovy Vary"));
            czechRegions.Add(new GeoCzechRegion("U", "Ústecký", "ULK", "Ústí nad Labem"));
            czechRegions.Add(new GeoCzechRegion("L", "Liberecký", "LBK", "Liberec"));
            czechRegions.Add(new GeoCzechRegion("H", "Královéhradecký", "HKK", "Hradec Králové"));
            czechRegions.Add(new GeoCzechRegion("E", "Pardubický", "PAK", "Pardubice"));
            czechRegions.Add(new GeoCzechRegion("M", "Olomoucký", "OLK", "Olomouc"));
            czechRegions.Add(new GeoCzechRegion("T", "Moravskoslezský", "MSK", "Ostrava"));
            czechRegions.Add(new GeoCzechRegion("B", "Jihomoravský", "JHM", "Brno"));
            czechRegions.Add(new GeoCzechRegion("Z", "Zlínský", "ZLK", "Zlín"));
            czechRegions.Add(new GeoCzechRegion("J", "Kraj Vysočina", "VYS", "Jihlava"));
        }

        public static List<GeoCzechRegion> CzechRegions
        {
            get
            {
                if (czechRegions == null)
                {
                    Init();
                }
                return czechRegions;
            }
        }
    }
}
