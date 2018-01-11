using System;
using System.Collections.Generic;
using System.Data;
public  class DeleteCommentsManager
{
    string tableComments = null;
    string tableCommentsThumbsUp = null;
    string tableCommentsThumbsDown = null;
    string tableCommentsSpamVotes = null;
    MySitesShort mss = MySitesShort.Nope;

    public DeleteCommentsManager(MySitesShort mss)
    {
        //
        if (mss != MySitesShort.Nope)
        {
            this.mss = mss;
            string ms = mss.ToString();
            tableComments = ms + "_" + ms + "Comments";
            tableCommentsThumbsUp = ms + "_" + ms + "CommentsThumbsUp";
            tableCommentsThumbsDown = ms + "_" + ms + "CommentsThumbsDown";
            tableCommentsSpamVotes = ms + "_" + ms + "CommentsSpamVotes";
        }
        else
        {
            throw new Exception("Pro website Nope nemůžete přidávat ani odebírat komentáře");
        }
    }

    /// <summary>
    /// Používá se při mazání celého generálního účtu 
    /// </summary>
    /// <param name="idUser"></param>
    public  void FromUser(int idUser)
    {
        Array arr = Enum.GetValues(typeof(MySitesShort));
        foreach (var item in arr)
        {
            byte b = (byte)(MySitesShort)item;
            FromUserAndWeb(idUser, b);
        }
    }

    public  void FromUserAndWeb(int idUser, MySitesShort web)
    {
        FromUserAndWeb(idUser, (byte)web);
    }

    /// <summary>
    /// Používá se pro mazání všech komentářů k daného uživatele A1 na webu A2.
    /// </summary>
    /// <param name="idUser"></param>
    /// <param name="idWeb"></param>
    public  void FromUserAndWeb(int idUser, byte idWeb)
    {
        List<short> listIdComments = new List<short>();
        DataTable dt = MSStoredProceduresI.ci.SelectDataTableSelective(tableComments, "ID,IDPages", "IDUsers", idUser);
        foreach (DataRow item in dt.Rows)
        {
            object[] o = item.ItemArray;
            int idPages = int.Parse(o[1].ToString());
            bool isRightWeb = IsSameWeb(idPages, idWeb);

            if (isRightWeb)
            {
                short idComment = short.Parse(o[0].ToString());
                DeleteComment(idComment);
            }
        }

    }



    private void DeleteComment(int idComment)
    {
        MSStoredProceduresI.ci.Delete(tableComments, "ID", idComment);
        MSStoredProceduresI.ci.Delete(tableCommentsThumbsDown, "IDComment", idComment);
        MSStoredProceduresI.ci.Delete(tableCommentsThumbsUp, "IDComment", idComment);
        MSStoredProceduresI.ci.Delete(tableCommentsSpamVotes, "IDComment", idComment);
    }

    private bool IsSameWeb(int idPages, byte idWeb)
    {
        return MSStoredProceduresI.ci.SelectCellDataTableByteOneRow(Tables.PageOld, "IDPage", idPages, "IDWeb") == idWeb;
    }

    public void FromPageNew(int idPage)
    {
        List<int> idComments = MSStoredProceduresI.ci.SelectValuesOfColumnInt(false, tableComments, "ID", "IDPages", idPage);
        foreach (var item in idComments)
        {
            DeleteComment(item);
        }
    }

    public bool FromPage(string stranka, string args)
    {
        //return false;
        byte idWeb = (byte)mss;
        int idPageName = GeneralCells.IDOfPageName_Name(stranka);
        int idPageArg = GeneralCells.IDOfPageArgument_Arg(args);
        List<DateTime> dates = MSStoredProceduresI.ci.SelectValuesOfColumnAllRowsDateTime(Tables.PageOld, "Day", AB.Get("IDWeb", (byte)mss), AB.Get("IDPageName", idPageName), AB.Get("IDPageArg", idPageArg));
        bool vr = false;
        foreach (DateTime day in dates)
        {
            int idPage = GeneralHelper.IDOfPageOld(idWeb, idPageName, idPageArg, day);
            if (idPage != int.MaxValue)
            {
                List<int> s = MSStoredProceduresI.ci.SelectValuesOfColumnAllRowsInt(true, tableComments, "ID", "IDPages", idPage);
                foreach (var item in s)
                {
                    DeleteComment(item);
                }
                vr = true;
            }
        }
        return vr;
    }
}
