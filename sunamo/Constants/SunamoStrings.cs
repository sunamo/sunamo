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
        public const string NotImplementedPleaseContactWebAdmin = "Neimplementováno. Prosím kontaktujte správce webu o tomto nedostatku který je uveden v patičce každé stránky.";
        public const string ScIsNotTheSame = "error: Nesouhlasilo sc. ";
        public const string UnvalidSession = "error: Špatné údaje o přihlášení - odhlašte se a přihlašte se znovu. ";
        public const string DetailsClickSurveyAspxLabel = "Kdy bylo kliknuto na odpovědi v této anketě";
        public const string AddAsRsvp = "Přijdu to zkouknout";
        public const string RemoveAsRsvp = "Bohužel mi to nevyjde";
        public const string AddAsRsvpSuccess = "Accept :)";
        public const string RemoveAsRsvpSuccess = "Tak snad někdy příště..";

        public const string AddToFavorites = "Přidat do oblíbených";
        public const string RemoveFromFavorites = "Odstranit z oblíbených";
        public const string AddToFavoritesSuccess = "Přidáno do oblíbených!";
        public const string RemoveFromFavoritesSuccess = "Odebráno z oblíbených";
        public const string Success = "success";
        public const string Error = "error";
        public const string UnauthorizedOperation = "Na tuto stránku jste se dostali, protože jste se pokusili načíst stránku, nebo provést jinou operaci, ke které nemáte povolení nebo která není v tomto kontextu aplikovatelná.";
        /// <summary>
        /// Kód, kterým se kontroluje pravost uživatele různých služeb, hlavně u těch kde se já nemusím přihlašovat(jednoduché desktopové apps, atd.)
        /// </summary>
        public const string scFixed = "1pr2qyfsagraqjv4uypgho5o";
        /// <summary>
        /// Toto nikdy nepoužívat, je to tu jen abych mohl ukončit metodu returnem.
        /// </summary>
        public const string LinkSuccessfullyShorted = "Odkaz byl úspěšně zkrácen";
        public const string CustomShortUriOccupatedYet = "Tento krátký odkaz byl již zabrán";
        public const string UriTooLong = "Adresa byla delší než 512 znaků";
        public const string UriTooShort = "Web obdržel adresu která byla prázdná";
        public const string YouHaveNotValidIPv4Address = "Nemáte platnou adresu IPv4";
        /// <summary>
        /// Nebyly nalezeny žádné data k zobrazení
        /// </summary>
        public static string messageIfEmpty = null;
        public const string YouAreNotLoggedAsWebAdmin = "Nebyl jste přihlášen jako admin stránek";
        public const string NoRightArgumentsToPage = "Do stránky nebyl předán správný počet parametrů a/nebo se správnými hodnotami";
        public const string StringNotFound = "Řetězen nebyl nalezen";

        public static string MessageIfEmpty(string p)
        {
            return "Nebyly nalezeny žádné " + p + " k zobrazení";
        }

        public const string TurnOffSelectingPhotos = "Vypni režim označování fotek";
        public const string TurnOnSelectingPhotos = "Zapni režim označování fotek";
        public const string YouAreBlocked = "error: Byly jste zablokování dle IP adresy nebo nicku, požadovanou operaci nemůžete provést";
        public const string YouAreNotLogged = "error: Nebyly jste přihlášení";
        public const string ViewLastWeek = "Shlédnutí stránky za posledních 7 dní(počítá se den zpětně, to znamená že dnešní přístupy se zobrazí až zítra): ";
        public const string ErrorSerie0 = "error: Uri ze které byl volán tento handler nebyla v databázi.";
        public const string ErrorSerie255 = "error: Buď stránka poslala špatné parametry URI entityTableId a entityId, nebo entita s těmito parametry nebyla v databázi nalezena.";
        public const string UserDetail = "Detail uživatele";
        public const string EditUserAccount = "Editace uživatelského účtu";
    }