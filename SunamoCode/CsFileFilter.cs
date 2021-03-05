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

    static bool? _rv = null;

    static bool? rv
    {
        get
        {
            return _rv;
        }
        set
        {
            if (value.HasValue)
            {
                if (!value.Value)
                {

                }
            }

            _rv = value;
        }
    }

    /// <summary>
    /// A2 is also for master.designer.cs and aspx.designer.cs
    /// </summary>
    /// <param name="item"></param>
    /// <param name="designerCs"></param>
    /// <param name="xamlCs"></param>
    /// <param name="sharedCs"></param>
    public static bool AllowOnly(string item, EndArgs end, ContainsArgs c, ref bool end2, bool alsoEnds)
    {
        if (alsoEnds)
        {
            end2 = true;

            rv = null;

            if (!end.designerCs && item.EndsWith(End.designerCsPp))
            {

                rv = false;
            }
            if (!end.xamlCs && item.EndsWith(End.xamlCsPp))
            {
                rv = false;
            }
            if (!end.sharedCs && item.EndsWith(End.sharedCsPp))
            {
                rv = false;
            }
            if (!end.iCs && item.EndsWith(End.iCsPp))
            {
                rv = false;
            }
            if (!end.gICs && item.EndsWith(End.gICsPp))
            {
                rv = false;
            }
            if (!end.gCs && item.EndsWith(End.gCsPp))
            {
                rv = false;
            }
            if (!end.tmp && item.EndsWith(End.tmpPp))
            {
                rv = false;
            }
            if (!end.TMP && item.EndsWith(End.TMPPp))
            {
                rv = false;
            }
            if (!end.DesignerCs && item.EndsWith(End.DesignerCsPp))
            {
                rv = false;
            }
            if (!end.notTranslateAble && item.EndsWith(End.NotTranslateAblePp))
            {
                rv = false;
            }
        }


        if (rv.HasValue)
        {
            // Always false
            return rv.Value;
        }

       

        end2 = false;

        if (!c.binFp && item.Contains(Contains.binFp))
        {
            rv = false;
        }

        if (!c.objFp && item.Contains(Contains.objFp))
        {
            rv = false;
        }

        if (!c.tildaRF && item.Contains(Contains.tildaRFFp))
        {
            rv = false;
        }
        

        if (rv.HasValue)
        {
            // Always false
            return rv.Value;
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
        //false which not to index, true which to index

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
        if (Is(e.notTranslateAble, n))
        {
            l.Add(f.NotTranslateAblePp);
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
       
        f.RemoveAll(AllowOnly);
        f.RemoveAll(AllowOnlyContains);

        return f;
    }

    #region Take by class variables
    public bool AllowOnly(string item)
    {
        return AllowOnly(item, true);
    }

    public  bool AllowOnly(string item, bool alsoEnds)
    {
        bool end2 = true;
        return !AllowOnly(item, e, c, ref end2, alsoEnds);
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
        public bool objFp; public bool binFp; public bool tildaRF; 

        /// <summary>
        /// false which not to index, true which to index
        /// </summary>
        /// <param name="objFp"></param>
        /// <param name="binFp"></param>
        /// <param name="tildaRF"></param>
        /// <param name="notTranslateAble"></param>
        public ContainsArgs(bool objFp, bool binFp, bool tildaRF, bool notTranslateAble)
        {
            this.objFp = objFp;
            this.binFp = binFp;
            this.tildaRF = tildaRF;
        }
    }

    public class End
    {
        public const string NotTranslateAblePp = "NotTranslateAble";
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
            var x = c(xamlCsPp);

            EndArgs ea = new EndArgs(c(designerCsPp), x, c(sharedCsPp), c(iCsPp), c(gICsPp), c(gCsPp), c(tmpPp), c(TMPPp), c(DesignerCsPp));
            return ea;
        }

        static bool c(string k)
        {
            if(u.Contains(k))
            {
                // Really I want to delete it
                u.Remove(k);
                return false;
            }
            return true;
        }
    }

    public class EndArgs
    {
        public bool notTranslateAble;
        public bool designerCs; public bool xamlCs; public bool sharedCs; public  bool  iCs; public  bool  gICs; public  bool  gCs; public  bool  tmp; public  bool  TMP; public  bool  DesignerCs; 

        /// <summary>
        /// false which not to index, true which to index
        /// </summary>
        /// <param name="designerCs"></param>
        /// <param name="xamlCs"></param>
        /// <param name="sharedCs"></param>
        /// <param name="iCs"></param>
        /// <param name="gICs"></param>
        /// <param name="gCs"></param>
        /// <param name="tmp"></param>
        /// <param name="TMP"></param>
        /// <param name="DesignerCs"></param>
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