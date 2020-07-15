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
        


        if (!c.binFp && item.Contains(Contains.binFp))
        {
            return false;
        }

        if (!c.objFp && item.Contains(Contains.objFp))
        {
            return false;
        }

        if (!c.tildaRF && item.Contains(Contains.tildaRFFp))
        {
            return false;
        }
        if (!c.notTranslateAble && item.EndsWith(f.NotTranslateAblePp))
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

    public void Set(EndArgs ea, ContainsArgs c)
    {
        this.e = ea;
        this.c = c;
    }

    public void SetDefault()
    {
        e = new EndArgs(false, true, true, false, false, false, false, false, false);
        c = new ContainsArgs(false, false, false, false);
    }

    /// <summary>
    /// A1 = negate
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public List<string> GetContainsByFlags(bool n)
    {
        List<string> l = new List<string>();
        if (BTS.Is( c.binFp, n))
        {
            l.Add(Contains.binFp);
        }
        if (BTS.Is(c.objFp, n))
        {
            l.Add(Contains.objFp);
        }
        if (BTS.Is(c.tildaRF,n))
        {
            l.Add(Contains.tildaRFFp);
        }
        if (Is(c.notTranslateAble, n))
        {
            l.Add(f.NotTranslateAblePp);
        }

        return l;
    }

    public List<string> GetEndingByFlags(bool n)
    {
        List<string> l = new List<string>();
        if (Is(e.designerCs, n))
        {
            l.Add(End.designerCsPp);
        }
        if (Is(e.xamlCs, n))
        {
            l.Add(End.xamlCsPp);
        }
        if (Is(e.xamlCs, n))
        {
            l.Add(End.xamlCsPp);
        }
        if (Is(e.sharedCs, n))
        {
            l.Add(End.sharedCsPp);
        }
        if (Is(e.iCs, n))
        {
            l.Add(End.iCsPp);
        }
        if (Is(e.gICs, n))
        {
            l.Add(End.gICsPp);
        }
        if (Is(e.gCs, n))
        {
            l.Add(End.gCsPp);
        }
        if (Is(e.tmp, n))
        {
            l.Add(End.tmpPp);
        }
        if (Is(e.TMP, n))
        {
            l.Add(End.TMPPp);
        }
        if (Is(e.DesignerCs, n))
        {
            l.Add(End.DesignerCsPp);
        }
        

        return l;
    }

    private bool Is(bool tMP, bool n)
    {
        return BTS.Is(tMP, n);
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
        public static string tildaRFFp = "~RF";
        public const string notTranslateAbleFp = "NotTranslateAble";

        public static List<string> u = null;

        /// <summary>
        /// Into A1 is inserting copy to leave only unindexed
        /// </summary>
        /// <param name="unindexablePathEnds"></param>
        /// <returns></returns>
        public static ContainsArgs FillEndFromFileList(List<string> unindexablePathEnds)
        {
            u = unindexablePathEnds;
            ContainsArgs ea = new ContainsArgs(c(objFp), c(binFp), c(tildaRFFp), c(notTranslateAbleFp));
            return ea;
        }

        static bool c(string k)
        {
            return u.Contains(k);
        }
    }

    public class ContainsArgs
    {
        public bool objFp; public bool binFp; public bool tildaRF; public bool notTranslateAble;

        public ContainsArgs(bool objFp, bool binFp, bool tildaRF, bool notTranslateAble)
        {
            this.objFp = objFp;
            this.binFp = binFp;
            this.tildaRF = tildaRF;
            this.notTranslateAble = notTranslateAble;
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
        

        public static List<string> u = null;

        /// <summary>
        /// Into A1 is inserting copy to leave only unindexed
        /// </summary>
        /// <param name="unindexablePathEnds"></param>
        /// <returns></returns>
        public static EndArgs FillEndFromFileList(List<string> unindexablePathEnds)
        {
            u = unindexablePathEnds;
            EndArgs ea = new EndArgs(c(designerCsPp), c(xamlCsPp), c(sharedCsPp), c(iCsPp), c(gICsPp), c(gCsPp), c(tmpPp), c(TMPPp), c(DesignerCsPp));
            return ea;
        }

        static bool c(string k)
        {
            if(u.Contains(k))
            {
                u.Remove(k);
                return true;
            }
            return false;
        }
    }

    public class EndArgs
    {
        public bool designerCs; public bool xamlCs; public bool sharedCs; public  bool  iCs; public  bool  gICs; public  bool  gCs; public  bool  tmp; public  bool  TMP; public  bool  DesignerCs; 

        public EndArgs(bool designerCs, bool xamlCs, bool sharedCs, bool iCs, bool gICs, bool gCs, bool tmp, bool TMP, bool DesignerCs)
        {
            this.designerCs = designerCs;
            this.xamlCs = xamlCs;
            this.sharedCs = sharedCs;
            this.iCs = iCs;
            this.gCs = gCs;
            this.tmp = tmp;
            this.TMP = TMP;
            this.DesignerCs = DesignerCs;
            
        }

        
    }
}