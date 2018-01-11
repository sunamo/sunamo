using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class CountOfViews
{
    public uint today = 0;
    public uint overall = 0;
}

public static class DayViewManager
{
    /// <summary>
    /// Název typu v deklaraci je Typ_vrácené_hodnoty
    /// Do A4 se dává název tabulky entity, tedy například Tables.Koc_Cats a do A5 se pak vkládá název ID sloupce tabulky A4
    /// Vrátí mi celkový počet shlédnutí včetně toho dnešního
    /// </summary>
    /// <param name="tableDayView"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public static uint IncrementOrInsert(SunamoPage sp, string tableDayView, int itemId, string tableEntity, string columnEntityTableEntity)
    {
        return 0;
    }

    /// <summary>
    /// Tato metoda se používá, pokud máš v užívání tabulku Views
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="tableDayView"></param>
    /// <param name="itemId"></param>
    /// <param name="tableEntity"></param>
    /// <param name="columnEntityTableEntity"></param>
    /// <returns></returns>
    public static void IncrementOrInsertNew(SunamoPage sp, ViewTable viewTable, int itemId)
    {
        //int idUser = SessionManager.GetLoginedUser(sp).ID(sp);
        sp.FillIDUsers();

        byte idTable = byte.MaxValue;
        int idPage = GeneralHelper.IDPageOfPageNew(viewTable, itemId, DateTime.Today, out idTable);
        if (idPage == int.MaxValue)
        {
            int  pricist = 1;
            if (sp.idLoginedUser == 1)
            {
                pricist = 0;
            }
            
            int overallTodayBoth = GetOverallNew(idTable, itemId);
            TableRowPage p = new TableRowPage(false, overallTodayBoth + pricist, true);//
            idPage = p.InsertToTable();

            // + pricist - zde přičítám v metodě IncrementOrInsert1Step
            TableRowPageNew n = new TableRowPageNew(idTable, itemId, DateTime.Today, int.MinValue + pricist);
            n.InsertToTable3(idPage);
            sp.idPage = idPage;
            sp.showComments = true;

            sp.overall  = NormalizeNumbers.NormalizeInt(overallTodayBoth);
            sp.today = (uint)pricist;
            return;
        }
        else
        {
            sp.idPage = idPage;
            sp.showComments = true;
        }
        
        sp.today = IncrementOrInsert1Step(idPage, viewTable, itemId, sp.idLoginedUser);
        sp.overall = IncrementOrInsert2Step(idPage, sp.idLoginedUser);
        return;
    }

    

    /// <summary>
    /// Tuto metodu nikdy nesmíš volat sám, musí ji volat vždy pouze metoda SunamoPage.WriteOld 
    /// Může se volat pouze u stránek které nejsou entitami(nemají vlastní tabulku v DB)
    /// A2 nastavuj na false, stejně k dnešnímu dni(28.11.2014) není jediné místo kde bych zjišťoval kolik přihlášených lidí stránku navštívilo
    /// Pokud this nebude login.aspx, logout.aspx, vrátí A1
    /// Volá se v každé stránce odvozené od SunamoPage kromě General
    /// Zapíše pouze do tabulky VisitsLogined, vyplní proměnné idPageName, idPageArgument, idPage
    /// </summary>
    public static void IncrementOrInsertOld(SunamoPage sp)
    {
        sp.FillIDUsers();
        if (sp.idPage == -1)
        {
            if (!SunamoPageHelper.returnUrls.Contains(sp.namePage))
            {
                byte[] b = null;
                if ((b = SunamoPageHelper.IsIpAddressRight(sp)) == null)
                {
                    sp.overall = NormalizeNumbers.NormalizeInt(MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.Page, "OverallViews", "ID", sp.idPage));
                    sp.today = IncrementOrInsert1StepOld(sp, false);
                    return;
                }

                sp.idPageName = GeneralHelper.GetOfInsertPageName(sp.namePage);
                sp.idPageArgument = GeneralHelper.GetOrInsertPageArgument(sp.args);

                #region Nový způsob tabulek

                // Nahradit zde Page za PageOld po konvertu na nové tabulky
                int idPage = GeneralHelper.IDOfPageOld(sp.IDWeb, sp.idPageName, sp.idPageArgument, DateTime.Today);

                int v = GetOverallOld(sp.IDWeb, sp.idPageName, sp.idPageArgument, DateTime.Today);
                if (idPage == int.MaxValue)
                {
                    if (sp.idLoginedUser != 1)
                    {
                        v++;
                    }
                    TableRowPage p3 = new TableRowPage(true, v, true); //
                    idPage = p3.InsertToTable();
                    
                }
                else
                {
                    if (sp.idLoginedUser == 1)
                    {
                        v = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.Page, "OverallViews", "ID", idPage);
                    }
                    else
                    {
                        v = MSStoredProceduresI.ci.UpdatePlusIntValue(Tables.Page, "OverallViews", 1, "ID", idPage);
                    }
                }

                sp.idPage = idPage; //(int)(((int)NormalizeNumbers.NormalizeLong(idPage)) + int.MinValue); 
                #endregion

                sp.overall = NormalizeNumbers.NormalizeInt(v);
                sp.today = IncrementOrInsert1StepOld(sp, true);

                return;
            }
            else
            {
                // Stránka byla login.aspx, logout.aspx atd. - prostě v kolekci returnUrls
                int overall = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.Page, "OverallViews", "ID", sp.idPage);
                if (sp.overall == int.MaxValue)
                {
                    sp.overall = 0;
                    sp.today = 0;
                    return;
                }
                sp.overall = NormalizeNumbers.NormalizeInt(overall);
                sp.today = IncrementOrInsert1StepOld(sp, true);
                return;
            }
        }

        sp.overall = NormalizeNumbers.NormalizeInt(MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.Page, "OverallViews", "ID", sp.idPage));
        sp.today = IncrementOrInsert1StepOld(sp, false);
        return;
    }

    public static int GetOverallOld(byte idWeb, int idPageName, int idPageArg, DateTime today)
    {

        //List<DateTime> dt = MSStoredProceduresI.ci.SelectValuesOfColumnAllRowsDateTime(Tables.Page)
        string sql = "SELECT MAX(Day) FROM " + Tables.PageOld + " WHERE IDWeb = @p0 AND IDPageName = @p1 AND IDPageArg = @p2";// AND Day > @p3 AND Day < @p4";
        SqlCommand comm = new SqlCommand(sql);
        MSStoredProceduresI.AddCommandParameter(comm, 0, idWeb);
        MSStoredProceduresI.AddCommandParameter(comm, 1, idPageName);
        MSStoredProceduresI.AddCommandParameter(comm, 2, idPageArg);
        var day2 = MSStoredProceduresI.ci.ExecuteScalar(comm);

        string sql2 = "SELECT IDPage FROM " + Tables.PageOld + " WHERE IDWeb = @p0 AND IDPageName = @p1 AND IDPageArg = @p2 AND Day = @p3";// AND Day > @p3 AND Day < @p4";
        SqlCommand comm2 = new SqlCommand(sql2);
        MSStoredProceduresI.AddCommandParameter(comm2, 0, idWeb);
        MSStoredProceduresI.AddCommandParameter(comm2, 1, idPageName);
        MSStoredProceduresI.AddCommandParameter(comm2, 2, idPageArg);
        MSStoredProceduresI.AddCommandParameter(comm2, 3, day2);

        var idPage = MSStoredProceduresI.ci.ExecuteScalar(comm2);

        if (idPage == null)
        {
            return int.MinValue;
        }
        int idPage2 = Convert.ToInt32(idPage);
        return GeneralCells.OverallCountOfPage(idPage2);

        #region MyRegion
        #endregion
    }

    

    /// <summary>
    /// Před voláním této metody musí být A1.idPage naplněno
    /// Vkládá do tabulky s jednotlivými dny PageOld
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public static uint IncrementOrInsert1StepOld(SunamoPage sp, bool increment)
    {
        int nt = 0;
        
            nt = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "Views",  "IDPage", sp.idPage);
            if (increment)
            {
                if (nt == int.MaxValue)
                {
                    nt = int.MinValue;
                    if (sp.idLoginedUser != 1)
                    {
                        nt++;
                    }
                    //MSStoredProceduresI.ci.Insert3(Tables.Views, (byte)viewTable, itemId, DateTime.Today, nt);
                    TableRowPageOld n = new TableRowPageOld(sp.IDWeb, sp.idPageName, sp.idPageArgument, DateTime.Today, nt);
                    //int idPage = n.InsertToTable();
                    n.InsertToTable3(sp.idPage);
                }
                else
                {
                    if (sp.idLoginedUser != 1)
                    {
                        nt++;
                        MSStoredProceduresI.ci.Update(Tables.PageOld, "Views", nt, "IDPage", sp.idPage);
                    }
                    else
                    {
                        // Jedná se o mě jako o admina, jen vrátím starou hodnotu
                    }
                }
            }
            else
            {
                if (nt == int.MaxValue)
                {
                    nt = int.MinValue;
                    
                    //MSStoredProceduresI.ci.Insert3(Tables.Views, (byte)viewTable, itemId, DateTime.Today, nt);
                    TableRowPageOld n = new TableRowPageOld(sp.IDWeb, sp.idPageName, sp.idPageArgument, DateTime.Today, nt);
                    //int idPage = n.InsertToTable();
                    n.InsertToTable3(sp.idPage);
                }
            }
            return NormalizeNumbers.NormalizeInt(nt);
    }

    public static uint IncrementOrInsert2Step(int idPage, int idUser)
    {
        uint vr = 0;
        if (idUser != 1)
        {
            vr = NormalizeNumbers.NormalizeInt(MSStoredProceduresI.ci.UpdatePlusIntValue(Tables.Page, "OverallViews", 1, "ID", idPage));
        }
        else
        {
            vr = NormalizeNumbers.NormalizeInt(MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.Page, "OverallViews", "ID", idPage));
            //vr++;
        }
        if (vr == int.MaxValue)
        {
            vr = 0;
        }
        return vr;
    }

    public static uint IncrementOrInsert1Step(int idPage, ViewTable viewTable, int itemId,  int idUser)
    {
        int nt  = 0;
            nt = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageNew, "Views", AB.Get( "IDPage", idPage));
            if (nt == int.MaxValue)
            {
                nt = int.MinValue;
                if (idUser != 1)
                {
                    nt++;
                }
                //MSStoredProceduresI.ci.Insert3(Tables.Views, (byte)viewTable, itemId, DateTime.Today, nt);
                TableRowPageNew n = new TableRowPageNew((byte)viewTable, itemId, DateTime.Today, int.MinValue + 1);
                //int idPage = n.InsertToTable();
                n.InsertToTable3(idPage);
            }
            else
            {
                if (idUser != 1)
                {
                    nt++;
                    MSStoredProceduresI.ci.Update(Tables.PageNew, "Views", nt, "IDPage", idPage);
                }
                else
                {

                }

            }

            return NormalizeNumbers.NormalizeInt(nt);
    }

    public static AB[] AbForViewsNew(ViewTable viewTable, int itemId, DateTime dt)
    {
        byte vr = (byte)viewTable;
        AB[] ab = CA.ToArrayT<AB>(AB.Get("IDTable", vr), AB.Get("IDItem", itemId), AB.Get("Day", dt));
        return ab;
    }

    public static uint NormalizeViewsForDayUint(bool old, ViewTable vt, int idItem, DateTime day)
    {
        var v = int.MinValue;
        if (old)
        {
            v = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "Views", AB.Get("IDTable", (byte)vt), AB.Get("IDItem", idItem), AB.Get("Day", day));
        }
        else
        {
            v = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageNew, "Views", AB.Get("IDTable", (byte)vt), AB.Get("IDItem", idItem), AB.Get("Day", day));
        }
        if (v == int.MaxValue)
        {
            return 0;
        }
        return NormalizeNumbers.NormalizeInt(v);
    }

    public static uint NormalizeViewsForDayUint(bool old, int idPage)
    {
        var v = int.MinValue;
        if (old)
        {
            v = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "Views", AB.Get("IDPage", idPage));
        }
        else
        {
            v = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageNew, "Views", AB.Get("IDPage", idPage));
        }
        if (v == int.MaxValue)
        {
            return 0;
        }
        return NormalizeNumbers.NormalizeInt(v);
    }

    public static string NormalizeViewsForDay(bool old, int idPage)
    {
        //vr++;
        return (NormalizeViewsForDayUint(old, idPage)).ToString();
    }

    /// <summary>
    /// Vrací od int.MinValue
    /// </summary>
    /// <param name="idTable"></param>
    /// <param name="itemId"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static int GetOverallNew(byte idTable, int itemId)
    {
        #region MyRegion
        #endregion

        //List<DateTime> dt = MSStoredProceduresI.ci.SelectValuesOfColumnAllRowsDateTime(Tables.Page)
        string sql = "SELECT MAX(Day) FROM " + Tables.PageNew + " WHERE IDTable = @p0 AND IDItem = @p1";// AND Day > @p3 AND Day < @p4";
        SqlCommand comm = new SqlCommand(sql);
        MSStoredProceduresI.AddCommandParameter(comm, 0, idTable);
        MSStoredProceduresI.AddCommandParameter(comm, 1, itemId);
        
        var day2 = MSStoredProceduresI.ci.ExecuteScalar(comm);

        string sql2 = "SELECT IDPage FROM " + Tables.PageNew + " WHERE IDTable = @p0 AND IDItem = @p1 AND Day = @p2";// AND Day > @p3 AND Day < @p4";
        SqlCommand comm2 = new SqlCommand(sql2);
        MSStoredProceduresI.AddCommandParameter(comm2, 0, idTable);
        MSStoredProceduresI.AddCommandParameter(comm2, 1, itemId);
        MSStoredProceduresI.AddCommandParameter(comm2, 2, day2);

        var idPage = MSStoredProceduresI.ci.ExecuteScalar(comm2);
        
        if (idPage == null)
        {
            return int.MinValue;
        }
        int idPage2 = Convert.ToInt32(idPage);
        return GeneralCells.OverallCountOfPage(idPage2);
    }

    public static uint NormalizeViewsForDay(byte idWeb, int idPageName, int idPageArgument, DateTime day)
    {
        int vr = GeneralHelper.ViewsOfPageOld(idWeb, idPageName, idPageArgument, day);
        if (vr == int.MaxValue)
        {
            return 0;
        }
        return NormalizeNumbers.NormalizeInt(vr);
    }

    public static uint NormalizeViewsForDay(ViewTable p, int idEntity, DateTime day)
    {
        int vr = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageNew, "Views", AbForViewsNew(p, idEntity, day));
        if (vr == int.MaxValue)
        {
            return 0;
        }
        return NormalizeNumbers.NormalizeInt(vr);
    }
}
