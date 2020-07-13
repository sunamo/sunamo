using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Cant be derived from FiltersNotTranslateAble because easy of finding instances of CsFileFilter
/// </summary>
public class CsFileFilter //: FiltersNotTranslateAble
{
    EndArgs e = null;
    ContainsArgs c = null;

    static FiltersNotTranslateAble f =  FiltersNotTranslateAble.Instance;

    /// <summary>
    /// A2 is also for master.designer.cs and aspx.designer.cs
    /// </summary>
    /// <param name="item"></param>
    /// <param name="designerCs"></param>
    /// <param name="xamlCs"></param>
    /// <param name="sharedCs"></param>
    public static bool AllowOnly(string item, EndArgs end, ContainsArgs c)
    {
        if (!end.designerCs && item.EndsWith(End.designerCsPp))
        {
            return false;
        }
        if (!end.xamlCs && item.EndsWith(End.xamlCsPp))
        {
            return false;
        }
        if (!end.sharedCs && item.EndsWith(End.sharedCsPp))
        {
            return false;
        }
        if (!end.iCs && item.EndsWith(End.iCsPp))
        {
            return false;
        }
        if (!end.gICs && item.EndsWith(End.gICsPp))
        {
            return false;
        }
        if (!end.gCs && item.EndsWith(End.gCsPp))
        {
            return false;
        }
        if (!end.tmp && item.EndsWith(End.tmpPp))
        {
            return false;
        }
        if (!end.TMP && item.EndsWith(End.TMPPp))
        {
            return false;
        }
        if (!end.DesignerCs && item.EndsWith(End.DesignerCsPp))
        {
            return false;
        }
        if (!end.notTranslateAble && item.EndsWith(f.NotTranslateAblePp))
        {
            return false;
        }


        if (!c.binFp && item.Contains(Contains.binFp))
        {
            return false;
        }

        if (!c.objFp && item.Contains(Contains.objFp))
        {
            return false;
        }

        if (!c.tildaRF && item.Contains(Contains.tildaRF))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// In default is everything in false
    /// Call some Set* method
    /// </summary>
    public CsFileFilter()
    {

    }

    public void Set(bool designerCs, EndArgs ea, ContainsArgs c)
    {
        this.e = ea;
        this.c = c;
    }

    public void SetDefault()
    {
        e.designerCs = false; e.xamlCs = true; e.sharedCs = true; e.iCs = false; e.gICs = false; e.gCs = false;
        
        e.DesignerCs = false;
        e.notTranslateAble = false;

        c.objFp = false; c.binFp = false;
    }

    public List<string> GetEndingByFlags()
    {
        List<string> l = new List<string>();
        if (e.designerCs)
        {
            l.Add(End.designerCsPp);
        }
        else if (e.xamlCs)
        {
            l.Add(End.xamlCsPp);
        }
        else if (e.xamlCs)
        {
            l.Add(End.xamlCsPp);
        }
        else if (e.sharedCs)
        {
            l.Add(End.sharedCsPp);
        }
        else if (e.iCs)
        {
            l.Add(End.iCsPp);
        }
        else if (e.gICs)
        {
            l.Add(End.gICsPp);
        }
        else if (e.gCs)
        {
            l.Add(End.gCsPp);
        }
        else if (e.tmp)
        {
            l.Add(End.tmpPp);
        }
        else if (e.TMP)
        {
            l.Add(End.TMPPp);
        }
        else if (e.DesignerCs)
        {
            l.Add(End.DesignerCsPp);
        }
        else if (e.notTranslateAble)
        {
            l.Add(f.NotTranslateAblePp);
        }

        return l;
    }

    #region Take by method
    

    public static  bool AllowOnlyContains(string i, ContainsArgs c)
    {
        if (!c.objFp && i.Contains(@"\obj\"))
        {
            return false;
        }
        if (!c.binFp && i.Contains(@"\bin\"))
        {
            return false;
        }
        if (!c.tildaRF && i.Contains(@"RF~"))
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
        return !AllowOnly(item, e, c);
    }

    public  bool AllowOnlyContains(string i)
    {
        return !AllowOnlyContains(i, c);
    }
    #endregion

    public class Contains
    {
        public static string objFp = @"\obj\";
        public static string binFp = @"\bin\";
        public static string tildaRF = "~RF";
    }

    public class ContainsArgs
    {
        public bool objFp; public bool binFp; public bool tildaRF;

        public ContainsArgs(bool objFp, bool binFp, bool tildaRF)
        {
            this.objFp = objFp;
            this.binFp = binFp;
            this.tildaRF = tildaRF;
        }
    }

    public class End
    {
        public const string designerCsPp = ".designer.cs";
        public const string DesignerCsPp = ".Designer.cs";
        public const string xamlCsPp = ".xaml.cs";
        public const string sharedCsPp = "Shared.cs";
        public const string iCsPp = ".i.cs";
        public const string gICsPp = ".g.i.cs";
        public const string gCsPp = ".g.cs";
        public const string tmpPp = ".tmp";
        public const string TMPPp = ".TMP";
        public const string notTranslateAblePp = "NotTranslateAble.cs";
    }

    public class EndArgs
    {
        public bool designerCs; public bool xamlCs; public bool sharedCs; public  bool  iCs; public  bool  gICs; public  bool  gCs; public  bool  tmp; public  bool  TMP; public  bool  DesignerCs; public  bool  notTranslateAble;

        public EndArgs(bool designerCs, bool xamlCs, bool sharedCs, bool iCs, bool gICs, bool gCs, bool tmp, bool TMP, bool DesignerCs, bool notTranslateAble)
        {
            this.designerCs = designerCs;
            this.xamlCs = xamlCs;
            this.sharedCs = sharedCs;
            this.iCs = iCs;
            this.gCs = gCs;
            this.tmp = tmp;
            this.TMP = TMP;
            this.DesignerCs = DesignerCs;
            this.notTranslateAble = notTranslateAble;
        }
    }
}