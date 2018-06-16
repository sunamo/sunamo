using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Data;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Hosting;
using sunamo;
using shared.Extensions;
using sunamo.Essential;

public class GeneralHelper
{
    public static string GetPhotoPathImgUri(HttpRequest Request, string fileName)
    {
        return UH.CombineTrimEndSlash("http://" + Request.Url.Host, "img", fileName);
    }

    /// <summary>
    /// První položka v A1 i A2 musí být nulová(ne null, jen prostě samé 0)
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public static Speed[] GetSpeed(Distance2[] distance, TimeSpan[] times)
    {
        if (distance.Length != times.Length)
        {
            throw new Exception("Kolekce vzdáleností a časů nemají stejný počet prvků.");
        }
        Speed[] vr = new Speed[distance.Length];
        vr[0] = new Speed();
        for (int i = 1; i < distance.Length; i++)
        {
            //string time = "00:00:23";
            vr[i] = new Speed();
            vr[i].mps = distance[i].m / times[i].TotalSeconds;
            vr[i].kmph = vr[i].mps * 3.6d;
        }
        return vr;
    }

    public static int Multiple(int p)
    {
        float f = (float)p;
        float f2 = (f * 0.9f);
        return (int)(f2);
    }

    public static int Divide(int p)
    {
        float f = (float)p;
        float f2 = (f / 2f);
        return (int)(f2);
    }

    /// <summary>
    /// A5 je instance originálního(velkého/původního) obrázku
    /// </summary>
    /// <param name="toFolderTempSlash"></param>
    /// <param name="fnwoe"></param>
    /// <param name="ext"></param>
    /// <param name="finalMin"></param>
    /// <param name="bmp"></param>
    /// <param name="borderColor"></param>
    public static void CreateThumbnail(string toFolderTempSlash, string fnwoe, string ext, string finalMin, System.Drawing.Image bmp, Color borderColor)
    {
        System.Drawing.Size sizeMin = bmp.Size;
        Point pointMin = new Point();
        if (bmp.Width > GeneralConsts.tnWidth || bmp.Height > GeneralConsts.tnHeight)
        {
            if (bmp.Height > bmp.Width)
            {
                // Menší je šířka, vypočtu optimální velikost obrázku, kde specifikuji výšku 168
                sizeMin = Pictures.CalculateOptimalSizeHeight(bmp.Width, bmp.Height, GeneralConsts.tnHeight).ToSystemDrawing();
                bool vypocistIVysku = false;
                while (sizeMin.Width > GeneralConsts.tnWidth)
                {
                    vypocistIVysku = true;
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                while (sizeMin.Height > GeneralConsts.tnHeight)
                {
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                pointMin = new Point(Divide(GeneralConsts.tnWidth - sizeMin.Width), 0);
                if (vypocistIVysku)
                {
                    pointMin = new Point(pointMin.X, Divide(GeneralConsts.tnHeight - sizeMin.Height));
                }
            }
            else if (bmp.Width > bmp.Height)
            {
                sizeMin = Pictures.CalculateOptimalSize(bmp.Width, bmp.Height, GeneralConsts.tnWidth).ToSystemDrawing();
                bool vypocistISirku = false;
                while (sizeMin.Height > GeneralConsts.tnHeight)
                {
                    vypocistISirku = true;
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                while (sizeMin.Width > GeneralConsts.tnWidth)
                {
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                pointMin = new Point(0, Divide(GeneralConsts.tnHeight - sizeMin.Height));
                if (vypocistISirku)
                {
                    pointMin = new Point(Divide(GeneralConsts.tnWidth - sizeMin.Width), pointMin.Y);
                }
            }
        }
        else
        {
            // Buď výška nebo šířka byla menší než minimální, zjistím co a obrázek umístím na střed
            while (sizeMin.Height > GeneralConsts.tnHeight && sizeMin.Width > GeneralConsts.tnWidth)
            {
                sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
            }

            pointMin = new Point(Divide(GeneralConsts.tnWidth - sizeMin.Width), Divide(GeneralConsts.tnHeight - sizeMin.Height));
        }

        //bool zmensovatMiniaturu = true;
        if (true)
        {
            string tempMin = toFolderTempSlash + fnwoe + "_tn" + ext;
            FS.CreateUpfoldersPsysicallyUnlessThere(tempMin);
            shared.Pictures.TransformImage(bmp, sizeMin.Width, sizeMin.Height, tempMin);
            using (System.Drawing.Image bmpMin = Bitmap.FromFile(tempMin))
            {
                using (Bitmap b2 = new Bitmap(GeneralConsts.tnWidth, GeneralConsts.tnHeight))
                {
                    using (Graphics g = Graphics.FromImage(b2))
                    {
                        g.Clear(borderColor);
                        Rectangle rect = new Rectangle(pointMin, sizeMin);
                        g.DrawImage(bmpMin, rect);
                        using (MemoryStream mOutput = new MemoryStream())
                        {
                            b2.Save(mOutput, ImageFormat.Jpeg);
                            byte[] array = mOutput.ToArray();
                            File.WriteAllBytes(finalMin, array);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// A5 je instance originálního(velkého/původního) obrázku
    /// </summary>
    /// <param name="toFolderTempSlash"></param>
    /// <param name="fnwoe"></param>
    /// <param name="ext"></param>
    /// <param name="finalMin"></param>
    /// <param name="bmp"></param>
    /// <param name="borderColor"></param>
    public static void CreateThumbnailOptimal(string toFolderTempSlash, string fnwoe, string ext, string finalMin, System.Drawing.Image bmp)
    {
        System.Drawing.Size sizeMin = bmp.Size;
        Point pointMin = new Point();
        if (bmp.Width > GeneralConsts.tnWidth || bmp.Height > GeneralConsts.tnHeight)
        {
            if (bmp.Height > bmp.Width)
            {
                // Menší je šířka, vypočtu optimální velikost obrázku, kde specifikuji výšku 168
                sizeMin = Pictures.CalculateOptimalSizeHeight(bmp.Width, bmp.Height, GeneralConsts.tnHeight).ToSystemDrawing();
                bool vypocistIVysku = false;
                while (sizeMin.Width > GeneralConsts.tnWidth)
                {
                    vypocistIVysku = true;
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                while (sizeMin.Height > GeneralConsts.tnHeight)
                {
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                pointMin = new Point(Divide(GeneralConsts.tnWidth - sizeMin.Width), 0);
                if (vypocistIVysku)
                {
                    pointMin = new Point(pointMin.X, Divide(GeneralConsts.tnHeight - sizeMin.Height));
                }
            }
            else if (bmp.Width > bmp.Height)
            {
                sizeMin = Pictures.CalculateOptimalSize(bmp.Width, bmp.Height, GeneralConsts.tnWidth).ToSystemDrawing();
                bool vypocistISirku = false;
                while (sizeMin.Height > GeneralConsts.tnHeight)
                {
                    vypocistISirku = true;
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                while (sizeMin.Width > GeneralConsts.tnWidth)
                {
                    sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
                }
                pointMin = new Point(0, Divide(GeneralConsts.tnHeight - sizeMin.Height));
                if (vypocistISirku)
                {
                    pointMin = new Point(Divide(GeneralConsts.tnWidth - sizeMin.Width), pointMin.Y);
                }
            }
        }
        else
        {
            // Buď výška nebo šířka byla menší než minimální, zjistím co a obrázek umístím na střed
            while (sizeMin.Height > GeneralConsts.tnHeight && sizeMin.Width > GeneralConsts.tnWidth)
            {
                sizeMin = new Size(Multiple(sizeMin.Width), Multiple(sizeMin.Height));
            }

            pointMin = new Point(Divide(GeneralConsts.tnWidth - sizeMin.Width), Divide(GeneralConsts.tnHeight - sizeMin.Height));
        }

        //bool zmensovatMiniaturu = true;
        if (true)
        {
            string tempMin = toFolderTempSlash + fnwoe + "_tn" + ext;
            FS.CreateUpfoldersPsysicallyUnlessThere(tempMin);
            shared.Pictures.TransformImage(bmp, sizeMin.Width, sizeMin.Height, finalMin);
        }
    }

    /// <summary>
    /// SLožku nevytváří, pouze vybere takovou která ještě neexistuje. Vrátí ji s koncovým backslashem
    /// A3 zda vytvořit celou cestu
    /// </summary>
    /// <param name="idUser"></param>
    /// <param name="webAndType"></param>
    /// <returns></returns>
    public static string GetRandomGuidFolderInRawUploads(int idUser, string webAndType, bool createPath)
    {
        string toFolderTempSlash = "";
        while (true)
        {
            toFolderTempSlash = GeneralHelper.MapPath("_/RawUploads/" + idUser + "/" + webAndType + "/" + Guid.NewGuid().ToString());// + "\\";
            if (!Directory.Exists(toFolderTempSlash))
            {
                toFolderTempSlash += "\\";
                break;
            }
        }
        if (createPath)
        {
            FS.CreateFoldersPsysicallyUnlessThere(toFolderTempSlash);
        }
        return toFolderTempSlash;
    }

    /// <summary>
    /// Vrací s koncovým \
    /// Složku nijak nevytváří
    /// </summary>
    /// <returns></returns>
    public static string GetRandomGuidFolderInRawUploads()
    {
        string rc =RandomHelper.RandomStringWithoutSpecial(20);
        string folder = GeneralHelper.MapPath("_/RawUploads/" + rc);
        while (Directory.Exists(folder))
        {
            rc = RandomHelper.RandomStringWithoutSpecial(20);
            folder = GeneralHelper.MapPath("_/RawUploads/" + rc);
        }
        return folder + "\\";
    }

    public static string GetOrCreateCityFromDictionary(Dictionary<int, string> artistsNames, int oEventIDArtistHeadliner)
    {
        if (artistsNames.ContainsKey(oEventIDArtistHeadliner))
        {
            return artistsNames[oEventIDArtistHeadliner];
        }
        else
        {
            string vr = TableRowCities.GetCityName(oEventIDArtistHeadliner);
            if (vr != "")
            {
                artistsNames.Add(oEventIDArtistHeadliner, vr);
            }
            return vr;
        }
    }

    public static void OdstranZPagesALastVisits(List<TableRowPageOld> bezparametricke)
        {
                foreach (var item in bezparametricke)
                {
                    MSStoredProceduresI.ci.Delete(Tables.Page, "ID", item.IDPage);
                    MSStoredProceduresI.ci.Delete(Tables.PageOld, "IDPage", item.IDPage);

                }
        }


    public static int GetOrInsertIPAddressID(HttpRequest Request)
    {
        byte[] ip = null;
        int ipid = GetIPAddressID(Request, out ip);
        if (ipid == int.MaxValue)
        {
            TableRowIPAddress tr = new TableRowIPAddress(ip[0], ip[1], ip[2], ip[3], false);
            return tr.InsertToTable2();
        }
        return ipid;
    }

    public static int GetIPAddressID(HttpRequest Request, out byte[] ip)
    {
        ip = null;
        int idIP = int.MaxValue;
        string ip2 = HttpRequestHelper.GetUserIPString(Request);
        if (ip2.TrimStart(':') == "1")
        {
            ip2 = "127.0.0.1";
        }
        string[] ips = SH.Split(ip2, ".");
        if (ips.Length == 4)
        {
            ip = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                ip[i] = byte.Parse(ips[i]);
            }
            idIP = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.IPAddress, "ID", AB.Get("IP1", ip[0]), AB.Get("IP2", ip[1]), AB.Get("IP3", ip[2]), AB.Get("IP4", ip[3]));
        }
        return idIP;

    }



    public static byte GetOrInsertState(string venueLocationCountry)
    {
        byte idState = GeneralCells.IDOfState(venueLocationCountry);
        if (idState == 0)
        {
            TableRowState s = new TableRowState(venueLocationCountry);
            idState = s.InsertToTable();
        }
        return idState;
    }

    public static bool IsUserActivated(int idUser)
    {
        return GeneralCells.CodeOfUsersReactivates(idUser) == "";
    }

    public static void WriteToDivResult(System.Web.UI.HtmlControls.HtmlGenericControl divResult, TypeOfMessage typeOfMessage, string message)
    {
        divResult.Style.Remove(HtmlTextWriterStyle.Color);
        CssNamedColors cnc = CssNamedColors.black;
        switch (typeOfMessage)
        {
            case TypeOfMessage.Error:
                cnc = CssNamedColors.red;
                break;
            case TypeOfMessage.Warning:
                cnc = CssNamedColors.orange;
                break;
            case TypeOfMessage.Information:
                cnc = CssNamedColors.black;
                break;
            case TypeOfMessage.Ordinal:
                cnc = CssNamedColors.black;
                break;
            case TypeOfMessage.Appeal:
                cnc = CssNamedColors.lightgreen;
                break;
            case TypeOfMessage.Success:
                cnc = CssNamedColors.darkgreen;
                break;
            default:
                break;
        }
        divResult.Style.Add(HtmlTextWriterStyle.Color, cnc.ToString());
        divResult.InnerHtml = message;
    }

    public static string BoolSex2String(bool p)
    {
        if (p)
        {
            return "Žena";
        }
        return "Muž";
    }

    #region Metody, které patří pouze do třídy Tables. Jsou zde, protože by jsem to bez nich nemohl zkompilovat




    #endregion

    public static List<short> GetRegionIDsForState(byte idState)
    {
        System.Collections.Generic.List<short> dt = MSStoredProceduresI.ci.SelectGroupByShort(true, Tables.Location, "IDRegion", "IDState", idState);
        dt.Sort();
        return dt;
    }

    /// <summary>
    /// Všechny OP pro Stát/Kraj/Okres musí být vždy jen DropDownList, v žádném případě ne HtmlSelect, aby se neduplikoval kód, protože HtmlSelect a DropDownList mají společné až Control
    /// Používá se zatím jen u GC webu, využívá možnosti 
    /// </summary>
    /// <param name="profilIDState"></param>
    /// <param name="profilIDRegion"></param>
    /// <param name="profilIDDistrict"></param>
    /// <param name="ddlStates"></param>
    /// <param name="ddlRegions"></param>
    /// <param name="ddlDistricts"></param>
    /// <param name="?"></param>
    /// <param name="nultaPolozkaStat"></param>
    /// <param name="nultaPolozkaRegion"></param>
    /// <param name="nultaPolozkaDistrict"></param>
    public static void LoadLocation(byte profilIDState, short profilIDRegion, byte profilIDDistrict, DropDownList ddlStates, DropDownList ddlRegions, DropDownList ddlDistricts, string nultaPolozkaStat, string nultaPolozkaRegion, string nultaPolozkaDistrict)
    {
        if (profilIDState != 0)
        {
            ddlStates.Items.Clear();
            ddlRegions.Items.Clear();
            ddlDistricts.Items.Clear();
            LoadStates(ddlStates, nultaPolozkaStat);
            DDLH.CheckItem(ddlStates, profilIDState.ToString());

            //ddlStates.SelectedIndex = profilIDState; // - 1 - protože první políčko je prázdné, nemusím to tu na -1
            if (profilIDRegion != short.MaxValue)
            {
                LoadRegions(ddlRegions, profilIDState, nultaPolozkaRegion);

                DDLH.CheckItem(ddlRegions, profilIDRegion.ToString());
                //ddlRegions.SelectedIndex = profilIDRegion; //  1
                if (profilIDDistrict != 0)
                {
                    if (MSStoredProceduresI.ci.SelectExistsCombination(Tables.Location, AB.Get("IDState", profilIDState), AB.Get("IDRegion", profilIDRegion)))
                    {

                        LoadDistricts(ddlDistricts, profilIDState, profilIDRegion, nultaPolozkaDistrict);

                        //ddlDistricts.SelectedIndex = profilIDDistrict; //  1
                        DDLH.CheckItem(ddlDistricts, profilIDDistrict.ToString());
                    }
                    else
                    {
                        InsertZeroItemDistrict(ddlDistricts, nultaPolozkaDistrict);
                        DDLH.CheckItem(ddlDistricts, nultaPolozkaDistrict);
                        LoadDistricts(ddlDistricts, profilIDState, profilIDRegion);
                    }
                }
                else
                {
                    InsertZeroItemDistrict(ddlDistricts, nultaPolozkaDistrict);
                    DDLH.CheckItem(ddlDistricts, nultaPolozkaDistrict);
                    LoadDistricts(ddlDistricts, profilIDState, profilIDRegion);
                }
            }
            else
            {
                LoadRegions(ddlRegions, profilIDState, nultaPolozkaRegion);
                //InsertZeroItemRegion(ddlRegions, nultaPolozkaRegion);
                DDLH.CheckItem(ddlRegions, nultaPolozkaRegion);
                InsertZeroItemDistrict(ddlDistricts, nultaPolozkaDistrict);
                DDLH.CheckItem(ddlDistricts, nultaPolozkaDistrict);
            }
        }
        else
        {
            ddlStates.Items.Clear();
            ddlRegions.Items.Clear();
            ddlDistricts.Items.Clear();
            LoadStates(ddlStates);
            DDLH.CheckItem(ddlStates, nultaPolozkaStat);
            InsertZeroItemRegion(ddlRegions, nultaPolozkaRegion);
            DDLH.CheckItem(ddlRegions, nultaPolozkaRegion);
            InsertZeroItemDistrict(ddlDistricts, nultaPolozkaDistrict);
            DDLH.CheckItem(ddlDistricts, nultaPolozkaDistrict);
        }
    }

    private static void LoadRegions(DropDownList ddlRegions, byte profilIDState)
    {
        LoadRegions(ddlRegions, profilIDState, null);
    }

    private static void LoadDistricts(DropDownList ddlDistricts, byte profilIDState, short profilIDRegion)
    {
        LoadDistricts(ddlDistricts, profilIDState, profilIDRegion, null);
    }

    private static void LoadDistricts(DropDownList ddlDistricts, byte profilIDState, short profilIDRegion, string nultaPolozkaDistrict)
    {
        System.Data.DataTable dt = MSStoredProceduresI.ci.SelectDataTableSelective(Tables.Location, "SerieDistrict,District", AB.Get("IDState", profilIDState), AB.Get("IDRegion", profilIDRegion));
        List<string> districts = new List<string>();
        List<string> indexesDistricts = new List<string>();
        foreach (DataRow item in dt.Rows)
        {
            object[] o = item.ItemArray;
            districts.Add(MSTableRowParse.GetString(o, 1));
            indexesDistricts.Add(MSTableRowParse.GetString(o, 0));
        }
        ddlDistricts.Items.Clear();
        List<System.Web.UI.WebControls.ListItem> listItemsDistricts = DDLH.CastStringsToListItemsList(indexesDistricts, districts);
        //listItemsDistricts.Insert(0, new ListItem("", ""));
        InsertZeroItemDistrict(ddlDistricts, nultaPolozkaDistrict);
        ddlDistricts.Items.AddRange(listItemsDistricts.ToArray());
    }

    private static void LoadRegions(DropDownList ddlRegions, byte profilIDState, string nultaPolozkaStat)
    {
        System.Collections.Generic.List<short> dt = GeneralHelper.GetRegionIDsForState(profilIDState);
        List<string> states = new List<string>();
        List<string> idcka = new List<string>();
        foreach (var item in dt)
        {
            idcka.Add(item.ToString());
            states.Add(GeneralCells.NameOfRegion(item));
        }
        List<ListItem> ff = DDLH.CastStringsToListItemsList(idcka, states);
        if (nultaPolozkaStat != null)
        {
            ff.Insert(0, new ListItem(nultaPolozkaStat, ""));
        }
        else
        {
            ff.Insert(0, new ListItem("", ""));
        }
        ddlRegions.Items.AddRange(ff.ToArray());

        
    }



    private static void InsertZeroItemRegion(DropDownList ddlRegions, string nultaPolozkaRegion)
    {
        if (nultaPolozkaRegion != null)
        {
            ddlRegions.Items.Insert(0, new ListItem(nultaPolozkaRegion, ""));
        }
        else
        {
            ddlRegions.Items.Insert(0, new ListItem("", ""));
        }
    }

    private static void InsertZeroItemDistrict(DropDownList ddlDistricts, string nultaPolozkaDistrict)
    {
        if (nultaPolozkaDistrict != null)
        {
            ddlDistricts.Items.Insert(0, new ListItem(nultaPolozkaDistrict, ""));
        }
        else
        {
            ddlDistricts.Items.Insert(0, new ListItem("", ""));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="ddlStates"></param>
    /// <param name="ddlRegions"></param>
    /// <param name="ddlDistricts"></param>
    public static void LoadLocation(byte p1, short p2, byte p3, DropDownList ddlStates, DropDownList ddlRegions, DropDownList ddlDistricts)
    {
        //U GC webu bych dal do těch posledních 3 parametrů TableRowStates3.all, TableRowSRegions3.all, TableRowSRDistricts3.all, zde ale musím dát všude null, aby se mi vytvořili ListView se prázdnými ID
        LoadLocation(p1, p2, p3, ddlStates, ddlRegions, ddlDistricts, null, null, null);
    }
    private static void LoadStates(DropDownList ddlStates)
    {
        LoadStates(ddlStates, null);
    }

    public static void GetStatesNamesAndIDs(out List<string> states, out List<string> idcka)
    {
        DataTable dt = MSStoredProceduresI.ci.SelectDataTableAllRows(Tables.State);
        states = new List<string>(dt.Rows.Count);
        idcka = new List<string>(dt.Rows.Count);
        foreach (DataRow item in dt.Rows)
        {
            // Používá se: ID,Name
            object[] sb = item.ItemArray;
            byte sbID = MSTableRowParse.GetByte(sb, 0);
            string sbName = MSTableRowParse.GetString(sb, 1);
            states.Add(sbName);
            idcka.Add(sbID.ToString());
        }
    }

    /// <summary>
    /// Všechny OP pro Stát/Kraj/Okres musí být vždy jen DropDownList, v žádném případě ne HtmlSelect, aby se neduplikoval kód, protože HtmlSelect a DropDownList mají společné až Control
    /// </summary>
    /// <param name="ddlStates"></param>
    /// <param name="nultaPolozkaStat"></param>
    public static void LoadStates(DropDownList ddlStates, string nultaPolozkaStat)
    {
        List<string> states = null;
        List<string> idcka = null;
        GeneralHelper.GetStatesNamesAndIDs(out states, out idcka);
        List<ListItem> ff = DDLH.CastStringsToListItemsList(idcka, states);
        if (nultaPolozkaStat != null)
        {
            ff.Insert(0, new ListItem(nultaPolozkaStat, ""));
        }
        else
        {
            ff.Insert(0, new ListItem("", ""));
        }
        ddlStates.Items.AddRange(ff.ToArray());
    }

    public static int GetOfInsertPageName(string stranka)
    {
        if (stranka != "")
        {
            int idPageName = GeneralCells.IDOfPageName_Name(stranka);
            if (idPageName == int.MaxValue)
            {
                TableRowPageName trpn = new TableRowPageName(stranka);
                idPageName = trpn.InsertToTable();
            }
            return idPageName;
        }
        return int.MaxValue;
    }

    public static int GetOrInsertPageArgument(string args)
    {
        int idPageArgument = int.MaxValue;
        if (args != "")
        {
            idPageArgument = GeneralCells.IDOfPageArgument_Arg(args);
            if (idPageArgument == int.MaxValue)
            {
                TableRowPageArgument trpa = new TableRowPageArgument(args);
                idPageArgument = trpa.InsertToTable();
            }
        }
        return idPageArgument;
    }

    public static StatusOfUser GetStatusOfUser(int idUser)
    {
        return (StatusOfUser)MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.Users, "ID", idUser, "Status");
    }

    /// <summary>
    /// Do A1 se přidá relativní cesta k rootu a vrátí mi to fyzickou cestu na hostingu
    /// Do A1 se přidává vždy bez počátečního ~/, protože pokud toto v řetězci bude, vrátí cestu i s tímto
    /// Takže například pro složku RawUploads v rootu zadej jen "_/RawUploads/", je jedno zda je aktuální skript v rootu nebo někde v podsložce, funguje to vždy
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string MapPathWithCheckForInvalid( string p)
    {
        if (FS.ContainsInvalidPathCharForPartOfMapPath(p))
        {
            return null;
        }
        try
        {
            string vr = HttpContext.Current.Server.MapPath("~/" + p);
            return vr;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Pokud bude tato metoda vyhazovat že cesta je neplatná, použij metodu MapPathWithErrorForInvalid
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string MapPath(string p)
    {
        string vr = HostingEnvironment.MapPath("~/" + p);
        return vr;
    }

    public static string GetTableNameDistricts(byte idState, int idRegion)
    {
        return string.Format("S{0}_R{1}_Districts", idState, idRegion);
    }



    /// <summary>
    /// Pozor, A4 se počítá od 0, ne od 1! Pokud chceš vybrat položku A3, zadej 1
    /// </summary>
    /// <param name="ddlDistricts"></param>
    /// <param name="districts"></param>
    /// <param name="nultaPolozkaText"></param>
    /// <param name="vybranaPolozka"></param>
    public static void FillDropDownList(DropDownList ddlDistricts, List<string> districts, string nultaPolozkaText, int vybranaPolozka)
    {
        
        List<string> indexesDistricts = LinearHelper.GetStringListFromTo(1, districts.Count);
        ddlDistricts.Items.Clear();
        List<System.Web.UI.WebControls.ListItem> listItemsDistricts = DDLH.CastStringsToListItemsList(indexesDistricts, districts);
        //listItemsDistricts.Insert(0, new ListItem("", ""));
        if (nultaPolozkaText != null)
        {
            listItemsDistricts.Insert(0, new ListItem(nultaPolozkaText, "0"));
        }
        else
        {
            listItemsDistricts.Insert(0, new ListItem("", "0"));
        }
        ddlDistricts.Items.AddRange(listItemsDistricts.ToArray());
        //ddlDistricts.SelectedIndex = profilIDDistrict; //  1
        DDLH.CheckItem(ddlDistricts, (vybranaPolozka - 1).ToString());
    }

    public static byte GetBrowserID(HttpRequest Request)
    {
        string browser = Request.Browser.Browser;
        if (String.IsNullOrEmpty(browser))
        {
            return GeneralColls.browsersIDs[Browsers.Other.ToString()];
        }
        if (GeneralColls.browsersIDs.ContainsKey(browser))
        {
            return GeneralColls.browsersIDs[browser];    
        }
        return GeneralColls.browsersIDs[Browsers.Other.ToString()];
        
    }
    static string[] colorsColorBar = new string[] { "#3FB1F0",
"#FFFDFE",
"#D8306E",
"#3DB8FE",
"#47B62C",
"#E52B72",
"#F4C829",
"#45B628",
"#F9C62D",
"#FE681B",
"#EE312F",
"#FF6B15",
"#44A4E1",
"#F33332",
"#40B2F1",
"#45B62A",
"#3EB9FF",
"#4AA031",
"#D9316F",
"#FCC824",
"#49A130",
"#7625C8",
"#F8C72D",
"#FF6D18",
"#7220A9",
"#EA3433",
"#F1671C",
"#39B6F6",
"#F13333",
"#3FBA00",
"#44B330",
"#E52C70",
"#FDC42F",
"#47B62B",
"#7325AF",
"#D6AE34" };
    
    /// <summary>
    /// V klíči bude rozlišení pro které se vztahuje, v hodnotě pak pole s čísly, které budou vyjadřovat % z dostupné šířky které barva zabere
    /// </summary>
    public static Dictionary<short, byte[]> percentOfCountBars = new Dictionary<short, byte[]>();
    /// <summary>
    /// V klíči bude rozlišení, v hodnotě pak barvy ke poli, které je na stejném klíči jako zde barvy v slovníku percentOfCountBars
    /// </summary>
    public static Dictionary<short, List<string>> colorsOfBars = new Dictionary<short, List<string>>();

    /// <summary>
    /// Calculate exactly widths and colors for colorful bar
    /// Result saves to colorsOfBars variable
    /// </summary>
    /// <param name="width"></param>
    /// <param name="pocet"></param>
    public static void CalculatePercentOfColorBars(short width, int pocet)
    {
        List<string> barvyFinal;
        byte[] b2;
        CalculatePercentOfColorBar(pocet, out barvyFinal, out b2);
        percentOfCountBars.Add(width, b2);
        colorsOfBars.Add(width, barvyFinal);
    }

    /// <summary>
    /// Calculate percent and colors for colorful bar
    /// </summary>
    /// <param name="pocet"></param>
    /// <param name="barvyFinal"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static int CalculatePercentOfColorBar(int pocet, out List<string> barvyFinal, out byte[] b2)
    {
        #region Initialize variable and collections
        barvyFinal = new List<string>(pocet);
        List<string> colors = new List<string>(colorsColorBar);
        byte percent = 100;
        byte percent2 = 0;
        int pocet2 = pocet;
        pocet2--;
        byte[] b = new byte[pocet];
        // increases from 1 to 9, numbers of generated colors
        byte i = 1;
        #endregion

        #region Select 9 random colors, substract maximum 1...9 = 45
        for (; i < 10; i++)
        {
            b[i - 1] = i;
            int indexBarvy = RandomHelper.RandomInt2(0, colors.Count);
            barvyFinal.Add(colors[indexBarvy]);
            colors.RemoveAt(indexBarvy);
            pocet--;
            percent2 += i;
            percent -= i;
        } 
        #endregion

        while (percent > 0 && pocet != 0)
        {
            #region Break if percent is two
            if (percent == 2)
            {
                break;
            }
            #endregion

            #region Get random number between 1 and actual reaming percent
            byte r = RandomHelper.RandomByte2(1, percent > 9 ? 9 : percent);
            if (r > percent)
            {
                continue;
            }
            #endregion

            #region Generate color, substract about random color
            b[i - 1] = r;
            int indexBarvy = RandomHelper.RandomInt2(0, colors.Count);
            barvyFinal.Add(colors[indexBarvy]);
            colors.RemoveAt(indexBarvy);
            pocet--;
            percent2 += r;
            percent -= r;
            #endregion

            #region If percent is below zero, break
            if (percent < 0)
            {
                break;
            }
            i++;
            #endregion

            #region When percent is below 10, generate new color for every one
            if (percent < 10 && percent != 0)
            {

                for (int i2 = 1; i2 < percent + 1; i2++)
                {
                    int dex = i - 1;
                    if (dex > pocet2)
                    {
                        break;
                    }

                    b[dex] = 1;
                    indexBarvy = RandomHelper.RandomInt2(0, colors.Count);
                    barvyFinal.Add(colors[indexBarvy]);
                    colors.RemoveAt(indexBarvy);
                    pocet--;
                    if (pocet == 0)
                    {
                        break;
                    }

                    percent2 += 1;
                    percent -= 1;
                    if (percent < 0)
                    {
                        break;
                    }
                    i++;
                }
            } 
            #endregion
        }

        #region Finalize if is still under 100%
        if (percent2 < 100)
        {
            #region If I have space in b array, set as last element, otherwise add to first
            i++;
            int pomer = 100 - percent2;
            if (b.Length == i + 1)
            {
                b[i] = (byte)pomer;
                int indexBarvy = RandomHelper.RandomInt2(0, colors.Count);
                barvyFinal.Add(colors[indexBarvy]);
            }
            else
            {
                b[0] += (byte)pomer;
            }
            #endregion
        }
        #endregion

        #region Sum all elements, if will greater than 100 substract from 9th element
        int nt = 0;
        for (int i3 = 0; i3 < b.Length; i3++)
        {
            nt += b[i3];
        }
        if (nt > 100)
        {
            int pomer = nt - 100;
            b[8] -= (byte)pomer;
        }
        #endregion

        if (b.Length != barvyFinal.Count)
        {
               
        }
        b2 = CA.JumbleUp<byte>(b);
        return pocet;
    }

    public static void AddWidthsOfViewports()
    {
        /*
         * Bez lišty: 
         * Apps(954px)
         * Shortener(1000px)
         * Kocicky(980px)
         * 
         */
        GeneralHelper.CalculatePercentOfColorBars(794, 25);
        // Lyrics,General,CasdMladez,GeoCaching,
        GeneralHelper.CalculatePercentOfColorBars(800, 25);
        GeneralHelper.CalculatePercentOfColorBars(840, 30);
        // WindowsMetroControls
        GeneralHelper.CalculatePercentOfColorBars(920, 35);
        GeneralHelper.CalculatePercentOfColorBars(960, 35);
        GeneralHelper.CalculatePercentOfColorBars(short.MaxValue, 36);
    }

    public static bool IsSurveyExpired(short idSurvey)
    {
        return IsSurveyExpired(GeneralCells.DateExpiredOfSurvey(idSurvey));
    }

    public static bool IsSurveyExpired(DateTime DateExpired)
    {
        bool expiredSurvey = (DateExpired < DateTime.Now);
        if (expiredSurvey)
        {
            if (DateExpired == MSStoredProceduresI.DateTimeMinVal)
            {
                expiredSurvey = false;
            }
            else
            {
                expiredSurvey = true;
            }
        }
        else
        {
            expiredSurvey = true;
        }
        return expiredSurvey;
    }

    public static int IDOfPageOld(byte idWeb, int idPage, int idArg, DateTime day)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "IDPage", AB.Get("IDWeb", idWeb), AB.Get("IDPageName", idPage), AB.Get("IDPageArg", idArg), AB.Get("Day", day));
    }

    /// <summary>
    /// ZMĚNA: Pokud takový řádek nebude v DB nalezen, vrátí int.MaxValue(vracela 0)
    /// </summary>
    /// <param name="idWeb"></param>
    /// <param name="idPage"></param>
    /// <param name="idArg"></param>
    /// <returns></returns>
    public static int ViewsOfPageOld(byte idWeb, int idPage, int idArg)
    {
        int v = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "Views", AB.Get("IDWeb", idWeb), AB.Get("IDPageName", idPage), AB.Get("IDPageArg", idArg));
        return v;
    }

    public static int ViewsOfPageOld(SunamoPage req)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld,  "Views", "IDPage", req.idPage);
    }

    public static void LoadLocationCzechRepublic(DropDownList ddlStates, DropDownList ddlRegions, DropDownList ddlDistricts)
    {
        LoadLocation(59, 1, 1, ddlStates, ddlRegions, ddlDistricts, null, null, null);
    }

    

    public static byte GetSerieDistrict(byte idState, short idRegion, byte serieDistrict)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.Location, "SerieDistrict", AB.Get("IDState", idState), AB.Get("IDRegion", idRegion), AB.Get("SerieDistrict", serieDistrict));
    }

    public static int GetOrInsertCity(string mesto)
    {
        if (mesto != "")
        {
            TableRowCities city = new TableRowCities(MSStoredProceduresI.ci.SelectOneRow(Tables.Cities, "Name", mesto));
            //city.SelectInTable("Name", mesto);
            if (city.ID == -1)
            {
                city.Name = mesto;
                city.InsertToTable();
            }
            return city.ID;
        }
            return -1;
        //}
    }

    public static Location GetLocation(HttpRequest Request, DropDownList ddlStates, DropDownList ddlRegions, DropDownList ddlDistricts)
    {
        Location location = new Location();
        string state = Request.Form[ddlStates.UniqueID]; //ddlStates.Items[ddlStates.SelectedIndex].Value.Trim();
        if (!string.IsNullOrEmpty(state))
        {
            byte idState = byte.Parse(state);
            if (MSStoredProceduresI.ci.SelectExists(Tables.State, "ID", idState))
            {
             
                location.IDState = idState;
            }
            else
            {
                location.IDState = 0;
            }
        }
        else
        {
            location.IDState = 0;
        }

        if (location.IDState != 0)
        {
            string region = Request.Form[ddlRegions.UniqueID];// ddlRegions.Items[ddlRegions.SelectedIndex].Value.Trim();
            if (!string.IsNullOrEmpty(region))
            {
                short idRegion = short.Parse(region);
                if (MSStoredProceduresI.ci.SelectExistsCombination(Tables.Location, AB.Get("IDState", location.IDState), AB.Get("IDRegion", idRegion)))
                {
                    location.IDRegion = idRegion;
                }
            }
            else
            {
                location.IDRegion = short.MaxValue;
            }
        }
        else
        {
            location.IDRegion = short.MaxValue;
        }

        if (location.IDRegion != short.MaxValue)
        {
            string district = Request.Form[ddlDistricts.UniqueID]; //ddlDistricts.Items[ddlDistricts.SelectedIndex].Value.Trim();
            if (!string.IsNullOrEmpty(district))
            {
                location.IDDistrict = GeneralHelper.GetSerieDistrict(location.IDState, location.IDRegion, byte.Parse(district));
            }
            else
            {
                location.IDDistrict = 0;
            }
        }
        else
        {
            location.IDDistrict = 0;
        }
        return location;
    }

    /// <summary>
    /// Vrátí int.MaxValue pokud taková IP nebude nalezena
    /// </summary>
    /// <param name="ip"></param>
    public static int GetIPAddressID(byte[] ip)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.IPAddress, "ID", AB.Get("IP1", ip[0]), AB.Get("IP2", ip[1]), AB.Get("IP3", ip[2]), AB.Get("IP4", ip[3]));
    }

    public static bool IsUserBlocked(HttpRequest httpRequest, int idUser)
    {
        
        
        byte[] ip = HttpRequestHelper.GetIPAddressInArray(httpRequest);
        if (ip == null)
        {
            return true;
        }
        //int idip = GeneralHelper.GetIPAddressID(ip);
        bool blocked = false;

        bool b = MSStoredProceduresI.ci.SelectCellDataTableBoolOneRow(Tables.IPAddress, "IsBlocked", CA.ToArrayT<AB>(AB.Get("IP1", ip[0]), AB.Get("IP2", ip[1]), AB.Get("IP3", ip[2]), AB.Get("IP4", ip[3])), null);
            if (b)
            {
                blocked = b;
            }
        
        if (!blocked)
        {
            b = GeneralCells.BlockNewCommentOfUser(idUser);
            if (b)
            {
                blocked = b;
            }
        }
        return blocked;
    }

    /// <summary>
    /// Vrátí mi ID stránky dle názvu tabulky ID, řádku v ní s ID A1 a vrátí mi ID stránky
    /// </summary>
    /// <param name="tableEntity"></param>
    /// <param name="itemId"></param>
    /// <param name="idTable"></param>
    /// <returns></returns>
    /// <summary>
    /// Vrátí int.MaxValue pokud nenalezne
    /// </summary>
    /// <param name="entityTableId"></param>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public static int IDPageOfPageNew(ViewTable viewTable, int itemId, DateTime day, out byte idTable)
    {
        
        idTable = (byte)viewTable;
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageNew, "IDPage", DayViewManager.AbForViewsNew(viewTable, itemId, day));
    }

    /// <summary>
    /// Vrací velkými písmeny vč. počáteční tečky
    /// </summary>
    /// <param name="jpg"></param>
    /// <returns></returns>
    public static string GetJpgExtension(bool jpg)
    {
        if (jpg)
        {
            return ".JPG";
        }
        return ".JPEG";
    }

    /// <summary>
    /// Pokud pozměním tuto metodu tady, musím ji pozměnit i v těchto projektech, jinak to nebude fungovat!!
    /// PhotosSunamoCzClient
    /// </summary>
    /// <param name="nameOfPhotoStf"></param>
    /// <param name="isPath"></param>
    /// <returns></returns>
    public static string MakeForFileNameAndUriReady(string nameOfPhotoStf, bool isPath)
    {
        
        nameOfPhotoStf = FS.DeleteWrongCharsInFileName(nameOfPhotoStf, isPath);
        nameOfPhotoStf = SH.ReplaceAll(nameOfPhotoStf, " ", "+");
        nameOfPhotoStf = SH.ReplaceAll(nameOfPhotoStf, " ", "  ");
        nameOfPhotoStf = WebUtility.UrlDecode(nameOfPhotoStf);
        return nameOfPhotoStf.Trim() ;
    }

    public static string TimeSpanToString(TimeSpan time)
    {
        return SH.JoinMakeUpTo2NumbersToZero(':',  time.Hours, time.Minutes, time.Seconds);
    }

    public static int IDOfCity_Name(string mesto)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(false, Tables.Cities, "ID", "Name", mesto);
    }


    /// <summary>
    /// Jako datum použije maximální nalezené
    /// Vrátí od int.MinValue
    /// </summary>
    /// <param name="p"></param>
    /// <param name="idPageName"></param>
    /// <param name="idPageArgument"></param>
    /// <returns></returns>
    public static uint OverallViewsOfPage(byte idWeb, int idPageName, int idPageArgument)
    {
        DateTime day = MSStoredProceduresI.ci.SelectMaxDateTime(Tables.PageOld, "Day", AB.Get("IDWeb", idWeb), AB.Get("IDPageName", idPageName), AB.Get("IDPageArg", idPageArgument));
        if (day != DateTime.MinValue)
        {
            int idPage = GeneralHelper.IDOfPageOld(idWeb, idPageName, idPageArgument, day);
            return NormalizeNumbers.NormalizeInt(MSStoredProceduresI.ci.SelectMaxIntMinValue(Tables.Page, "OverallViews", AB.Get("ID", idPage)));

        }
        return 0;
    }

    /// <summary>
    /// Vrátí int.MaxValue pokud nebude nalezeno
    /// </summary>
    /// <param name="idWeb"></param>
    /// <param name="idPageName"></param>
    /// <param name="idPageArgument"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public static int ViewsOfPageOld(byte idWeb, int idPageName, int idPageArgument, DateTime day)
    {
        int vr = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "Views", AB.Get("IDWeb", idWeb), AB.Get("IDPageName", idPageName), AB.Get("IDPageArg", idPageArgument), AB.Get("Day", day));
        return vr;
    }
}
