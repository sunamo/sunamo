using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SystemWindowsControls
{
    static Type type = typeof(SystemWindowsControls);
    static bool initialized = false;
    static Dictionary<string, List<string>> controls = new Dictionary<string, List<string>>();
    static EmbeddedResourcesH embeddedResourcesH = null;

    public static void Init()
    {
        if (!initialized)
        {
            initialized = true;

            embeddedResourcesH = new EmbeddedResourcesH(type.Assembly, "SunamoCode");

            var d = SH.GetLines( embeddedResourcesH.GetString("/Resources/SystemWindowsControls.txt"));
            foreach (var item in d)
            {
                var p = SH.Split(item, " ");
                controls.Add(p[0], SH.Split(p[1], ","));
            }
        }
    }

    public static bool IsShortcutOfControl(string r)
    {
        foreach (var item in controls)
        {
            foreach (var item2 in item.Value)
            {
                if (item2 == r)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsNameOfControl(string r)
    {
        return controls.ContainsKey(r);
    }
}

