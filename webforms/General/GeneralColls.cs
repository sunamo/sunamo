using System;
using System.Collections.Generic;
public static class GeneralColls
{
    public static Dictionary<string, byte> browsersIDs = new Dictionary<string, byte>();
    public static Dictionary<byte, string> browserNames = new Dictionary<byte, string>();
    public static List<string> internetProtocols = new List<string>();

    static GeneralColls()
    {
        string[] names = Enum.GetNames(typeof(Browsers));
        Array values = Enum.GetValues(typeof(Browsers));
        for (int i = 0; i < names.Length; i++)
        {
            byte id = (byte)values.GetValue(i);
            browsersIDs.Add(names[i], id);
            browserNames.Add(id, names[i]);
        }
        Array iprots = Enum.GetValues(typeof(InternetProtocols));
        for (int i = 0; i < iprots.Length; i++)
        {
            internetProtocols.Add(iprots.GetValue(i).ToString());
        }
    }
}
