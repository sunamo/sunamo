using System;
using System.Collections.Generic;
using System.Text;


    public class ConstsManager
    {
    /// <summary>
    /// XlfKeys.cs
    /// </summary>
    public readonly string pathXlfKeys = null;

    #region Work with consts in XlfKeys
    /// <summary>
    /// Add to XlfKeys.cs from xlf
    /// Must manually call XlfResourcesH.SaveResouresToRL(DefaultPaths.sunamoProject) before
    /// called externally from MiAddTranslationWhichIsntInKeys_Click
    /// </summary>
    /// <param name="keysAll"></param>
    public void AddConsts(List<string> keysAll, List<string> valuesAll = null)
    {
        int first = -1;

        List<string> lines = null;
        var keys = GetConsts(out first, out lines);

        var both = CA.CompareList(keys, keysAll);
        AddKeysConsts(keysAll, first, lines, valuesAll);
    }


    /// <summary>
    /// Add c# const code
    /// </summary>
    /// <param name="csg"></param>
    /// <param name="item"></param>
    private static void AddConst(CSharpGenerator csg, string item, string val)
    {
        csg.Field(1, AccessModifiers.Public, true, VariableModifiers.Mapped, "string", item, true, val);
    }

    public List<string> GetConsts()
    {
        int first;
        return GetConsts(out first);
    }

    /// <summary>
    /// Get consts which exists in XlfKeys.cs
    /// </summary>
    /// <param name="first"></param>
    public List<string> GetConsts(out int first)
    {
        List<string> lines = null;
        return GetConsts(out first, out lines);
    }

    /// <summary>
    /// Get consts which exists in XlfKeys.cs
    /// </summary>
    /// <param name="first"></param>
    /// <param name="lines"></param>
    public List<string> GetConsts(out int first, out List<string> lines)
    {
        first = -1;

        lines = TF.ReadAllLines(pathXlfKeys);

        var keys = CSharpParser.ParseConsts(lines, out first);
        return keys;
    }

    public ConstsManager(string pathXlfKeys, Func<string, bool> XmlLocalisationInterchangeFileFormatIsToBeInXlfKeys)
    {
        this.pathXlfKeys = pathXlfKeys;
        this.XmlLocalisationInterchangeFileFormatIsToBeInXlfKeys = XmlLocalisationInterchangeFileFormatIsToBeInXlfKeys;
    }

    Func<string, bool> XmlLocalisationInterchangeFileFormatIsToBeInXlfKeys;
    static Type type = typeof(ConstsManager);

    public void AddKeysConsts(List<string> keysAll, int first, List<string> lines, List<string> valuesAll = null)
    {
        CSharpGenerator csg = new CSharpGenerator();

        string append = string.Empty;

        if (valuesAll == null)
        {
            valuesAll = keysAll;
        }
        else
        {
            if (valuesAll.Count != keysAll.Count)
            {
                ThrowExceptions.DifferentCountInLists(Exc.GetStackTrace(), type, Exc.CallingMethod(), "keysAll", keysAll, "valuesAll", valuesAll);
            }
        }

        for (int i = 0; i < keysAll.Count; i++)
        {
            var item = keysAll[i];
            var val = valuesAll[i];

            if (XmlLocalisationInterchangeFileFormatIsToBeInXlfKeys(item))
            {
                append = string.Empty;

                if (char.IsDigit(item[0]))
                {
                    append = "_";
                }

                AddConst(csg, append + SH.TrimLeadingNumbersAtStart(item), val);
            }
        }

        lines.Insert(first, csg.ToString());



        CA.RemoveStringsEmpty2(lines);

        TF.SaveLines(lines, pathXlfKeys);
    }
    #endregion
}

