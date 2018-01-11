using webforms;

public class TableRowFavorites3
{
    public static bool IsPageInFavorite(long pageID, int userID, string dph, out string tooltip)
    {
        if (dph == Logins.GetIndexesOfHash(userID))
        {
            if (MSStoredProceduresI.ci.SelectExistsCombination(Tables.Favorites, AB.Get("IDPages", pageID), AB.Get("IDUsers", userID)))
            {
                tooltip = SunamoStrings.RemoveFromFavorites;
                return true;
            }
            else
            {
                tooltip = SunamoStrings.AddToFavorites;
                return false;
            }
        }
        tooltip = "Nepodařilo se autentizovat uživatele";
        return false;
    }
}
