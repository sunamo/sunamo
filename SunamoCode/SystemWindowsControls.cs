using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SystemWindowsControls
{
    private static Type type = typeof(SystemWindowsControls);
    private static bool s_initialized = false;
    private static Dictionary<string, List<string>> s_controls = new Dictionary<string, List<string>>();
    private static EmbeddedResourcesH s_embeddedResourcesH = null;

    public static void Init()
    {
        if (!s_initialized)
        {
            s_initialized = true;

            s_embeddedResourcesH = new EmbeddedResourcesH(type.Assembly, "SunamoCode");

            var d = SH.GetLines(s_embeddedResourcesH.GetString("/Resources/SystemWindowsControls.txt"));
            foreach (var item in d)
            {
                var p = SH.Split(item, " ");
                s_controls.Add(p[0], SH.Split(p[1], ","));
            }
        }
    }

    public static bool IsShortcutOfControl(string r)
    {
        foreach (var item in s_controls)
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
        return s_controls.ContainsKey(r);
    }
}