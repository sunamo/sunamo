using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Values
{
    public static class GeoCzechRegions
    {
        private static List<GeoCzechRegion> s_czechRegions;

        private static void Init()
        {
            s_czechRegions = new List<GeoCzechRegion>();
            s_czechRegions.Add(new GeoCzechRegion("A", "Hlavn\u00ED m\u011Bsto Praha", "PHA", "Praha"));
            s_czechRegions.Add(new GeoCzechRegion("S", "St\u0159edo\u010Desk\u00FD", "ST\u010C", "Praha"));
            s_czechRegions.Add(new GeoCzechRegion("C", "Jiho\u010Desk\u00FD", "JH\u010C", "\u010Cesk\u00E9 Bud\u011Bjovice"));
            s_czechRegions.Add(new GeoCzechRegion("P", "Plze\u0148sk\u00FD", "PLK", "Plze\u0148"));
            s_czechRegions.Add(new GeoCzechRegion("K", "Karlovarsk\u00FD", "KVK", "Karlovy Vary"));
            s_czechRegions.Add(new GeoCzechRegion("U", "\u00DAsteck\u00FD", "ULK", "\u00DAst\u00ED nad Labem"));
            s_czechRegions.Add(new GeoCzechRegion("L", "Libereck\u00FD", "LBK", "Liberec"));
            s_czechRegions.Add(new GeoCzechRegion("H", "Kr\u00E1lov\u00E9hradeck\u00FD", "HKK", "Hradec Kr\u00E1lov\u00E9"));
            s_czechRegions.Add(new GeoCzechRegion("E", "Pardubick\u00FD", "PAK", "Pardubice"));
            s_czechRegions.Add(new GeoCzechRegion("M", "Olomouck\u00FD", "OLK", "Olomouc"));
            s_czechRegions.Add(new GeoCzechRegion("T", "Moravskoslezsk\u00FD", "MSK", "Ostrava"));
            s_czechRegions.Add(new GeoCzechRegion("B", "Jihomoravsk\u00FD", "JHM", "Brno"));
            s_czechRegions.Add(new GeoCzechRegion("Z", "Zl\u00EDnsk\u00FD", "ZLK", "Zl\u00EDn"));
            s_czechRegions.Add(new GeoCzechRegion("J", "Kraj Vyso\u010Dina", "VYS", "Jihlava"));
        }

        public static List<GeoCzechRegion> CzechRegions
        {
            get
            {
                if (s_czechRegions == null)
                {
                    Init();
                }
                return s_czechRegions;
            }
        }
    }
}
