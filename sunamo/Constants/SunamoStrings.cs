using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Zde mohou byt jen konstanty, zadne metody
/// </summary>
public class SunamoStrings
{
    // TODO: Clean which are not necessary here

    static SunamoStrings()
    {
        messageIfEmpty = MessageIfEmpty("data");
    }

    #region Dont have same key
    public static string AddAsRsvpSuccess
    {
        get
        {
            return "";//sess.i18n(XlfKeys.Accept);
        }
    }
    #endregion

    #region Have same key
    public static string EditUserAccount
    {
        get
        {
            return "";//sess.i18n(XlfKeys.EditUserAccount);
        }
    }

    public static string UserDetail
    {
        get
        {
            return "";//sess.i18n(XlfKeys.UserDetail);
        }
    }

    public static string ErrorSerie255
    {
        get
        {
            return "";//sess.i18n(XlfKeys.ErrorSerie255);
        }
    }

    public static string ErrorSerie0
    {
        get
        {
            return "";//sess.i18n(XlfKeys.ErrorSerie0);
        }
    }

    public static string ViewLastWeek
    {
        get
        {
            return "";//sess.i18n(XlfKeys.ViewLastWeek);
        }
    }

    public static string YouAreNotLogged
    {
        get
        {
            return "";//sess.i18n(XlfKeys.YouAreNotLogged);
        }
    }

    public static string YouAreBlocked
    {
        get
        {
            return "";//sess.i18n(XlfKeys.YouAreBlocked);
        }
    }

    public static string TurnOnSelectingPhotos
    {
        get
        {
            return "";//sess.i18n(XlfKeys.TurnOnSelectingPhotos);
        }
    }

    public static string TurnOffSelectingPhotos
    {
        get
        {
            return "";//sess.i18n(XlfKeys.TurnOffSelectingPhotos);
        }
    }

    public static string StringNotFound
    {
        get
        {
            return "";//sess.i18n(XlfKeys.StringNotFound);
        }
    }

    public static string NoRightArgumentsToPage
    {
        get
        {
            return "";//sess.i18n(XlfKeys.NoRightArgumentsToPage);
        }
    }

    public static string YouAreNotLoggedAsWebAdmin
    {
        get
        {
            return "";//sess.i18n(XlfKeys.YouAreNotLoggedAsWebAdmin);
        }
    }

    public static string YouHaveNotValidIPv4Address
    {
        get
        {
            return "";//sess.i18n(XlfKeys.YouHaveNotValidIPv4Address);
        }
    }

    public static string UriTooShort
    {
        get
        {
            return "";//sess.i18n(XlfKeys.UriTooShort);
        }
    }

    public static string UriTooLong
    {
        get
        {
            return "";//sess.i18n(XlfKeys.UriTooLong);
        }
    }

    public static string CustomShortUriOccupatedYet
    {
        get
        {
            return "";//sess.i18n(XlfKeys.CustomShortUriOccupatedYet);
        }
    }

    public static string LinkSuccessfullyShorted
    {
        get
        {
            return "";//sess.i18n(XlfKeys.LinkSuccessfullyShorted);
        }
    }

    public static string UnauthorizedOperation
    {
        get
        {
            return "";//sess.i18n(XlfKeys.UnauthorizedOperation);
        }
    }

    public static string Error
    {
        get
        {
            return "";//sess.i18n(XlfKeys.Error);
        }
    }

    public static string Success
    {
        get
        {
            return "";//sess.i18n(XlfKeys.Success);
        }
    }

    public static string RemoveFromFavoritesSuccess
    {
        get
        {
            return "";//sess.i18n(XlfKeys.RemoveFromFavoritesSuccess);
        }
    }

    public static string AddToFavoritesSuccess
    {
        get
        {
            return "";//sess.i18n(XlfKeys.AddToFavoritesSuccess);
        }
    }

    public static string RemoveFromFavorites
    {
        get
        {
            return "";//sess.i18n(XlfKeys.RemoveFromFavorites);
        }
    }

    public static string AddToFavorites
    {
        get
        {
            return "";//sess.i18n(XlfKeys.AddToFavorites);
        }
    }

    public static string RemoveAsRsvpSuccess
    {
        get
        {
            return "";//sess.i18n(XlfKeys.RemoveAsRsvpSuccess);
        }
    }

    public static string RemoveAsRsvp
    {
        get
        {
            return "";//sess.i18n(XlfKeys.RemoveAsRsvp);
        }
    }

    public static string AddAsRsvp
    {
        get
        {
            return "";//sess.i18n(XlfKeys.AddAsRsvp);
        }
    }

    public static string DetailsClickSurveyAspxLabel
    {
        get
        {
            return "";//sess.i18n(XlfKeys.DetailsClickSurveyAspxLabel);
        }
    }

    public static string UnvalidSession
    {
        get
        {
            return "";//sess.i18n(XlfKeys.UnvalidSession);
        }
    }

    public static string ScIsNotTheSame
    {
        get
        {
            return "";//sess.i18n(XlfKeys.ScIsNotTheSame);
        }
    }

    public static string NotImplementedPleaseContactWebAdmin
    {
        get
        {
            return "";//sess.i18n(XlfKeys.NotImplementedPleaseContactWebAdmin);
        }
    } 
    #endregion



    /// <summary>
    /// Kód, kterým se kontroluje pravost uživatele různých služeb, hlavně u těch kde se já nemusím přihlašovat(jednoduché desktopové apps, atd.)
    /// </summary>
    /// <summary>
    /// Toto nikdy nepoužívat, je to tu jen abych mohl ukončit metodu returnem.
    /// </summary>
    /// <summary>
    /// Nebyly nalezeny žádné data k zobrazení
    /// </summary>
    public static string messageIfEmpty = null;
    public static string IsNotInRange = "is not in range";

    public static string MessageIfEmpty(string p)
    {
        return "";//"Nebyly nalezeny \u017E\u00E1dn\u00E9" + " " + p + " " + " " + " " + "zobrazen\u00ED";
    }
}