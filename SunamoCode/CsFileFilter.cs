using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CsFileFilter
{
    bool designerCs= false; 
    bool xamlCs= false; 
    bool sharedCs= false; 
    bool iCs= false; 
    bool gICs= false; 
    bool gCs= false;
    bool tmp = false;
    bool TMP = false;

    bool DesignerCs = false;
    bool NotTranslateAble = false;

    bool obj = false;
    bool bin = false;

    /// <summary>
    /// A2 is also for master.designer.cs and aspx.designer.cs
    /// </summary>
    /// <param name="item"></param>
    /// <param name="designerCs"></param>
    /// <param name="xamlCs"></param>
    /// <param name="sharedCs"></param>
    public static bool AllowOnly(string item, bool designerCs, bool xamlCs, bool sharedCs, bool iCs, bool gICs, bool gCs, bool tmp, bool TMP, bool DesignerCs, bool notTranslateAble)
    {
        if (!designerCs && item.EndsWith(designerCsPp))
        {
            return false;
        }
        if (!xamlCs && item.EndsWith(xamlCsPp))
        {
            return false;
        }
        if (!sharedCs && item.EndsWith(sharedCsPp))
        {
            return false;
        }
        if (!iCs && item.EndsWith(iCsPp))
        {
            return false;
        }
        if (!gICs && item.EndsWith(gICsPp))
        {
            return false;
        }
        if (!gCs && item.EndsWith(gCsPp))
        {
            return false;
        }
        if (!tmp && item.EndsWith(tmpPp))
        {
            return false;
        }
        if (!TMP && item.EndsWith(TMPPp))
        {
            return false;
        }
        if (!DesignerCs && item.EndsWith(DesignerCsPp))
        {
            return false;
        }
        if (!notTranslateAble && item.EndsWith(NotTranslateAblePp))
        {
            return false;
        }

        return true;
    }

    //".TMP", ".tmp"

    /// <summary>
    /// In default is everything in false
    /// Call some Set* method
    /// </summary>
    public CsFileFilter()
    {

    }

    public void Set(bool designerCs, bool xamlCs, bool sharedCs, bool iCs, bool gICs, bool gCs, bool tmp, bool TMP, bool DesignerCs, bool notTranslateAble)
    {
        this.designerCs = designerCs;
        this.xamlCs = xamlCs;
        this.sharedCs = sharedCs;
        this.iCs = iCs;
        this.gICs = gICs;
        this.gCs = gCs;
        this.tmp = tmp;
        this.TMP = TMP;
        this.DesignerCs = DesignerCs;
        this.NotTranslateAble = notTranslateAble;
    }

    public void SetDefault()
    {
         designerCs = false;  xamlCs = true;  sharedCs = true;  iCs = false;  gICs = false;  gCs = false;
         obj = false;  bin = false;
        DesignerCs = false;
    }

    static string designerCsPp = ".designer.cs";
    static string DesignerCsPp = ".designer.cs"; static string xamlCsPp = ".xaml.cs"; static string sharedCsPp = "Shared.cs"; static string iCsPp = ".i.cs"; static string gICsPp = ".g.i.cs"; static string gCsPp = ".g.cs";
    static string tmpPp = ".tmp";
    static string TMPPp = ".TMP";
    static string NotTranslateAblePp = "NotTranslateAble.cs";
    static string objPp = @"\obj\"; static string binPp = @"\bin\";

    public List<string> GetEndingByFlags()
    {
        List<string> l = new List<string>();
        if (designerCs)
        {
            l.Add(designerCsPp);
        }
        else if (xamlCs)
        {
            l.Add(xamlCsPp);
        }
        else if (xamlCs)
        {
            l.Add(xamlCsPp);
        }
        else if (sharedCs)
        {
            l.Add(sharedCsPp);
        }
        else if (iCs)
        {
            l.Add(iCsPp);
        }
        else if (gICs)
        {
            l.Add(gICsPp);
        }
        else if (gCs)
        {
            l.Add(gCsPp);
        }
        else if (tmp)
        {
            l.Add(tmpPp);
        }
        else if (TMP)
        {
            l.Add(TMPPp);
        }
        else if (DesignerCs)
        {
            l.Add(DesignerCsPp);
        }
        else if (NotTranslateAble)
        {
            l.Add(NotTranslateAblePp);
        }

        return l;
    }

    #region Take by method
    

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
        return !AllowOnly(item, designerCs, xamlCs, sharedCs, iCs, gICs, gCs, tmp, TMP, DesignerCs, NotTranslateAble);
    }

    public  bool AllowOnlyContains(string i)
    {
        return !AllowOnlyContains(i, obj, bin);
    }
    #endregion
}