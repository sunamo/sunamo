using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CsFileFilter : FiltersNotTranslateAble
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
    bool tildaRf = false;

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
        if (!end.notTranslateAble && item.EndsWith(NotTranslateAblePp))
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
        NotTranslateAble = false;
    }

    public List<string> GetEndingByFlags()
    {
        List<string> l = new List<string>();
        if (designerCs)
        {
            l.Add(End.designerCsPp);
        }
        else if (xamlCs)
        {
            l.Add(End.xamlCsPp);
        }
        else if (xamlCs)
        {
            l.Add(End.xamlCsPp);
        }
        else if (sharedCs)
        {
            l.Add(End.sharedCsPp);
        }
        else if (iCs)
        {
            l.Add(End.iCsPp);
        }
        else if (gICs)
        {
            l.Add(End.gICsPp);
        }
        else if (gCs)
        {
            l.Add(End.gCsPp);
        }
        else if (tmp)
        {
            l.Add(End.tmpPp);
        }
        else if (TMP)
        {
            l.Add(End.TMPPp);
        }
        else if (DesignerCs)
        {
            l.Add(End.DesignerCsPp);
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
        return !AllowOnly(item, new EndArgs( designerCs, xamlCs, sharedCs, iCs, gICs, gCs, tmp, TMP, DesignerCs, NotTranslateAble), new ContainsArgs(obj, bin, tildaRf));
    }

    public  bool AllowOnlyContains(string i)
    {
        return !AllowOnlyContains(i, obj, bin);
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