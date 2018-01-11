using System;
using System.Collections.Generic;

public class PagesAndVisitsDeleteManager
{
    MySitesShort mss = MySitesShort.Nope;
    byte msb = 0;

    public PagesAndVisitsDeleteManager(MySitesShort mss)
    {
        if (mss != MySitesShort.Nope)
        {
            this.mss = mss;
            msb = (byte)mss;
        }
        else
        {
            throw new Exception("Pro website Nope nemůžete přidávat ani odebírat komentáře");
        }
    }

    /// <summary>
    /// Před každým použitím této metody s jinou stránkou se musí zavolat GetPageNameAndArgs, která musí vrátit True
    /// A2 zda se mají odstranit i návštěvy z tabulky PageViews
    /// </summary>
    /// <param name="stranka"></param>
    /// <param name="args"></param>
    public void DeleteEntryInPageTable(bool deleteFromPageViews)
    {
            int idPage = MSStoredProceduresI.ci.SelectCellDataTableIntOneRow(true, Tables.PageOld, "ID", AB.Get("IDWeb", msb), AB.Get("IDPageName", pageNameId), AB.Get("IDPageArg", pageArgsId));
        #region Zakomentováno, protože tuto tabulku již v nové verzi používat nebudu
        //MSStoredProceduresI.ci.DeleteAll(Tables.PageViews, "IDPage", idPage); 
        #endregion
        DeleteEntryInPageTable(idPage, false, true);
    }

    public void DeleteEntryInPageTable(int idPage, bool old, bool new2)
    {
        if (new2)
        {
            MSStoredProceduresI.ci.Delete(Tables.PageNew, AB.Get("IDPage", idPage));
        }
        if (old)
        {
            MSStoredProceduresI.ci.Delete(Tables.PageOld, AB.Get("IDPage", idPage));
        }
        if (new2 || old)
        {
            MSStoredProceduresI.ci.Delete(Tables.Page, AB.Get("ID", idPage));
        }
    }

    /// <summary>
    /// A3 jsou defakto Visits, jak jí říkám v SunamoPage
    /// </summary>
    /// <param name="stranka"></param>
    /// <param name="args"></param>
    /// <param name="deleteFromPageViews"></param>
    public void DeleteEntryInPageTable(string stranka, string args, bool deleteFromPageViews)
    {
        GetPageNameAndArgs(stranka, args);
        DeleteEntryInPageTable(deleteFromPageViews);
    }
    int pageNameId = -1;
    int pageArgsId = -1;

    public bool GetPageNameAndArgs(string stranka, string args)
    {
         pageNameId = GeneralCells.IDOfPageName_Name(stranka);
        if (pageNameId == int.MaxValue)
        {
            pageArgsId = -1;
            return false;
        }
         pageArgsId = GeneralCells.IDOfPageArgument_Arg(args);
         return true;
    }

    public void DeleteAllEntriesOfPageNewPhotos()
    {

        List<int> idPhotos = MSStoredProceduresI.ci.SelectValuesOfColumnInt(true, Tables.PageNew, "IDPage", "IDTable", (byte)ViewTable.Phs_PhsPhoto);
        List<int> idAlbums = MSStoredProceduresI.ci.SelectValuesOfColumnInt(true, Tables.PageNew, "IDPage", "IDTable", (byte)ViewTable.Phs_PhsAlbum);
        List<int> idGallery = MSStoredProceduresI.ci.SelectValuesOfColumnInt(true, Tables.PageNew, "IDPage", "IDTable", (byte)ViewTable.Phs_PhsGallery);

        foreach (var item in idPhotos)
        {
            this.DeleteEntryInPageTable(item, false, true);
        }

        foreach (var item in idAlbums)
        {
            this.DeleteEntryInPageTable(item, false, true);
        }

        foreach (var item in idGallery)
        {
            this.DeleteEntryInPageTable(item, false, true);
        }
    }
}
