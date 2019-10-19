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

    public static string EditUserAccount
    {
        get
        {
            return RLData.en["EditUserAccount"];
        }
    }

    public static string UserDetail
    {
        get
        {
            return RLData.en["UserDetail"];
        }
    }

    public static string ErrorSerie255
    {
        get
        {
            return RLData.en["ErrorSerie255"];
        }
    }

    public static string ErrorSerie0
    {
        get
        {
            return RLData.en["ErrorSerie0"];
        }
    }

    public static string ViewLastWeek
    {
        get
        {
            return RLData.en["ViewLastWeek"];
        }
    }

    public static string YouAreNotLogged
    {
        get
        {
            return RLData.en["YouAreNotLogged"];
        }
    }

    public static string YouAreBlocked
    {
        get
        {
            return RLData.en["YouAreBlocked"];
        }
    }

    public static string TurnOnSelectingPhotos
    {
        get
        {
            return RLData.en["TurnOnSelectingPhotos"];
        }
    }

    public static string TurnOffSelectingPhotos
    {
        get
        {
            return RLData.en["TurnOffSelectingPhotos"];
        }
    }

    public static string StringNotFound
    {
        get
        {
            return RLData.en["StringNotFound"];
        }
    }

    public static string NoRightArgumentsToPage
    {
        get
        {
            return RLData.en["NoRightArgumentsToPage"];
        }
    }

    public static string YouAreNotLoggedAsWebAdmin
    {
        get
        {
            return RLData.en["YouAreNotLoggedAsWebAdmin"];
        }
    }

    public static string YouHaveNotValidIPv4Address
    {
        get
        {
            return RLData.en["YouHaveNotValidIPv4Address"];
        }
    }

    public static string UriTooShort
    {
        get
        {
            return RLData.en["UriTooShort"];
        }
    }

    public static string UriTooLong
    {
        get
        {
            return RLData.en["UriTooLong"];
        }
    }

    public static string CustomShortUriOccupatedYet
    {
        get
        {
            return RLData.en["CustomShortUriOccupatedYet"];
        }
    }

    public static string LinkSuccessfullyShorted
    {
        get
        {
            return RLData.en["LinkSuccessfullyShorted"];
        }
    }

    public static string scFixed
    {
        get
        {
            return RLData.en["scFixed"];
        }
    }

    public static string UnauthorizedOperation
    {
        get
        {
            return RLData.en["UnauthorizedOperation"];
        }
    }

    public static string Error
    {
        get
        {
            return RLData.en["Error"];
        }
    }

    public static string Success
    {
        get
        {
            return RLData.en["Success"];
        }
    }

    public static string RemoveFromFavoritesSuccess
    {
        get
        {
            return RLData.en["RemoveFromFavoritesSuccess"];
        }
    }

    public static string AddToFavoritesSuccess
    {
        get
        {
            return RLData.en["AddToFavoritesSuccess"];
        }
    }

    public static string RemoveFromFavorites
    {
        get
        {
            return RLData.en["RemoveFromFavorites"];
        }
    }

    public static string AddToFavorites
    {
        get
        {
            return RLData.en["AddToFavorites"];
        }
    }

    public static string RemoveAsRsvpSuccess
    {
        get
        {
            return RLData.en["RemoveAsRsvpSuccess"];
        }
    }

    public static string AddAsRsvpSuccess
    {
        get
        {
            return RLData.en["AddAsRsvpSuccess"];
        }
    }

    public static string RemoveAsRsvp
    {
        get
        {
            return RLData.en["RemoveAsRsvp"];
        }
    }

    public static string AddAsRsvp
    {
        get
        {
            return RLData.en["AddAsRsvp"];
        }
    }

    public static string DetailsClickSurveyAspxLabel
    {
        get
        {
            return RLData.en["DetailsClickSurveyAspxLabel"];
        }
    }

    public static string UnvalidSession
    {
        get
        {
            return RLData.en["UnvalidSession"];
        }
    }

    public static string ScIsNotTheSame
    {
        get
        {
            return RLData.en["ScIsNotTheSame"];
        }
    }

    public static string NotImplementedPleaseContactWebAdmin
    {
        get
        {
            return RLData.en["NotImplementedPleaseContactWebAdmin"];
        }
    }



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
        return "Nebyly nalezeny \u017E\u00E1dn\u00E9" + " " + p + " " + " " + " " + "zobrazen\u00ED";
    }
}