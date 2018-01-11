using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Values
{
    public class GeoCzechRegion
    {
        public GeoCzechRegion(string shortcutRZ, string name, string shortcutCSU, string mainCity)
        {
            this.ShortcutRZ = shortcutRZ;
            this.ShortcutCSU = shortcutCSU;
            this.Name = name;
            this.MainCity = mainCity;
        }

        public string ShortcutRZ { get; set; }
        public string ShortcutCSU { get; set; }
        public string Name { get; set; }
        public string MainCity { get; set; }
    }
}
