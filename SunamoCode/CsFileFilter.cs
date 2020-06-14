using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CsFileFilter
{
    bool designerCs= false; bool xamlCs= false; bool sharedCs= false; bool iCs= false; bool gICs= false; bool gCs= false;
    bool obj= false; bool bin= false;

    /// <summary>
    /// In default is everything in false
    /// Call some Set* method
    /// </summary>
    public CsFileFilter()
    {

    }

    public void SetDefault()
    {
         designerCs = false;  xamlCs = true;  sharedCs = true;  iCs = false;  gICs = false;  gCs = false;
         obj = false;  bin = false;
    }

    #region Take by method
    /// <summary>
    /// A2 is also for master.designer.cs and aspx.designer.cs
    /// </summary>
    /// <param name="item"></param>
    /// <param name="designerCs"></param>
    /// <param name="xamlCs"></param>
    /// <param name="sharedCs"></param>
    public static bool AllowOnly(string item, bool designerCs, bool xamlCs, bool sharedCs, bool iCs, bool gICs, bool gCs)
    {
        if (!designerCs && item.EndsWith(".designer.cs"))
        {
            return false;
        }
        if (!xamlCs && item.EndsWith(".xaml.cs"))
        {
            return false;
        }
        if (!sharedCs && item.EndsWith("Shared.cs"))
        {
            return false;
        }
        if (!iCs && item.EndsWith(".i.cs"))
        {
            return false;
        }
        if (!gICs && item.EndsWith(".g.i.cs"))
        {
            return false;
        }
        if (!gCs && item.EndsWith(".g.cs"))
        {
            return false;
        }

        return true;
    }

    public static  bool AllowOnlyContains(string i, bool obj, bool bin)
    {
        if (!obj && i.Contains(@"\obj\"))
        {
            return false;
        }

        if (!bin && i.Contains(@"\bin\"))
        {
            return false;
        }

        return true;
    }
    #endregion

    public List<string> GetFilesFiltered(string s, string masc, SearchOption so)
    {
        var f = FS.GetFiles(s, masc, so);
        //CA.Remove(s, )
        f.RemoveAll(AllowOnly);
        f.RemoveAll(AllowOnlyContains);

        return f;
    }

    #region Take by class variables
    public  bool AllowOnly(string item)
    {
        return !AllowOnly(item, designerCs, xamlCs, sharedCs, iCs, gICs, gCs);
    }

    public  bool AllowOnlyContains(string i)
    {
        return !AllowOnlyContains(i, obj, bin);
    }
    #endregion
}