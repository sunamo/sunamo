using System.Collections.Specialized;
using System.Web;

public class SunamoCzHelper
{
    /// <summary>
    /// Vrátí text který se má uložit na odkaz, na který se kliklo.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string UnFavoritePage(HttpContext context, NameValueCollection nvc)
    {
        LoginedUser lu = SessionManager.GetLoginedUser(context);
        string sc = lu.sc;

        if (sc != null)
        {
            int userID = lu.ID(context);
            if (TableRowSessions3.GetSessionID(userID) == sc)
            {



                int pageID = -1;
                ResultCheckWebArgument rPageID = PageHelper.CheckIntArgument(nvc, "pageID", out pageID);
                if (rPageID == ResultCheckWebArgument.AllOk)
                {
                    if (!MSStoredProceduresI.ci.SelectExistsCombination(Tables.Favorites, AB.Get("IDPages", pageID), AB.Get("IDUsers", userID)))
                    {
                        TableRowFavorites f = new TableRowFavorites(userID);
                        //f.IDPages = pageID;
                        f.InsertToTable3(pageID);
                        return SunamoStrings.AddToFavoritesSuccess;
                    }
                    else
                    {
                        MSStoredProceduresI.ci.Delete(Tables.Favorites, AB.Get("IDPages", pageID), AB.Get("IDUsers", userID));
                        return SunamoStrings.RemoveFromFavoritesSuccess;
                    }

                }
                else
                {
                    return PageHelperBase.GetErrorDescriptionIntURI(rPageID, "pageID");
                }
            }
            else
            {
                return SunamoStrings.ScIsNotTheSame;
            }
        }
        return SunamoStrings.UnvalidSession;
    }

    public static bool IsFavoritePage(HttpContext context, NameValueCollection nameValueCollection)
    {
        // Toto se načítá okamžitě při načítání stránky i když uživatel není přihlášen, proto zde musím kontrolovat na null
        LoginedUser lu = SessionManager.GetLoginedUser(context);
        string sc = lu.sc;

        if (sc != null)
        {
            int userID = lu.ID(context);
            if (TableRowSessions3.GetSessionID(userID) == sc)
            {

                long pageID = -1;
                ResultCheckWebArgument rPageID = PageHelper.CheckLongArgument(nameValueCollection, "pageID", out pageID);
                if (rPageID == ResultCheckWebArgument.AllOk)
                {
                    return MSStoredProceduresI.ci.SelectExistsCombination(Tables.Favorites, AB.Get("IDPages", pageID), AB.Get("IDUsers", userID));
                }
                else
                {
                    return false;
                    //return "Nepodařilo se získat ID stránky";
                }
            }
            else
            {
                return false; //SunamoStrings.ScIsNotTheSame;
            }
        }
        return false; //SunamoStrings.UnvalidSession;
    }
}
