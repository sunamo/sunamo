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
            return ""; //return RLData.en[XlfKeys.EditUserAccount];
        }
    }

    public static string UserDetail
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.UserDetail];
        }
    }

    public static string ErrorSerie255
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.ErrorSerie255];
        }
    }

    public static string ErrorSerie0
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.ErrorSerie0];
        }
    }

    public static string ViewLastWeek
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.ViewLastWeek];
        }
    }

    public static string YouAreNotLogged
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.YouAreNotLogged];
        }
    }

    public static string YouAreBlocked
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.YouAreBlocked];
        }
    }

    public static string TurnOnSelectingPhotos
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.TurnOnSelectingPhotos];
        }
    }

    public static string TurnOffSelectingPhotos
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.TurnOffSelectingPhotos];
        }
    }

    public static string StringNotFound
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.StringNotFound];
        }
    }

    public static string NoRightArgumentsToPage
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.NoRightArgumentsToPage];
        }
    }

    public static string YouAreNotLoggedAsWebAdmin
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.YouAreNotLoggedAsWebAdmin];
        }
    }

    public static string YouHaveNotValidIPv4Address
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.YouHaveNotValidIPv4Address];
        }
    }

    public static string UriTooShort
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.UriTooShort];
        }
    }

    public static string UriTooLong
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.UriTooLong];
        }
    }

    public static string CustomShortUriOccupatedYet
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.CustomShortUriOccupatedYet];
        }
    }

    public static string LinkSuccessfullyShorted
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.LinkSuccessfullyShorted];
        }
    }

    

    public static string UnauthorizedOperation
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.UnauthorizedOperation];
        }
    }

    public static string Error
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.Error];
        }
    }

    public static string Success
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.Success];
        }
    }

    public static string RemoveFromFavoritesSuccess
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.RemoveFromFavoritesSuccess];
        }
    }

    public static string AddToFavoritesSuccess
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.AddToFavoritesSuccess];
        }
    }

    public static string RemoveFromFavorites
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.RemoveFromFavorites];
        }
    }

    public static string AddToFavorites
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.AddToFavorites];
        }
    }

    public static string RemoveAsRsvpSuccess
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.RemoveAsRsvpSuccess];
        }
    }

    public static string AddAsRsvpSuccess
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.AddAsRsvpSuccess];
        }
    }

    public static string RemoveAsRsvp
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.RemoveAsRsvp];
        }
    }

    public static string AddAsRsvp
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.AddAsRsvp];
        }
    }

    public static string DetailsClickSurveyAspxLabel
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.DetailsClickSurveyAspxLabel];
        }
    }

    public static string UnvalidSession
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.UnvalidSession];
        }
    }

    public static string ScIsNotTheSame
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.ScIsNotTheSame];
        }
    }

    public static string NotImplementedPleaseContactWebAdmin
    {
        get
        {
            return ""; //return RLData.en[XlfKeys.NotImplementedPleaseContactWebAdmin];
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
        return "Wasn't found any data to show";
    }
}