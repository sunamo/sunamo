using System;
using System.Web.Routing;
public static class RoutePageHelper
{
    /// <summary>
    /// Vrátí -1 v případě zjištěné jakékoliv chyby
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultInt"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckIntArgument(RouteData nvc, string argname, out int resultInt)
    {
        return PageHelperBase.CheckIntArgument(GetStringFromRouteData(nvc, argname), out resultInt);
    }

    /// <summary>
    /// Vrací do A3 0 v případě jakékoliv chyby
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckByteArgument(RouteData nvc, string argname, out byte resultByte)
    {
        return PageHelperBase.CheckByteArgument(GetStringFromRouteData(nvc, argname), out resultByte);
    }

    /// <summary>
    /// Vrátí short.MinValue pokud se nenaleznou data k vyparsování
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultShort"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckShortArgument(bool signed, RouteData nvc, string argname, out short resultShort)
    {
        return PageHelperBase.CheckShortArgument(signed, GetStringFromRouteData(nvc, argname), out resultShort);
    }

    /// <summary>
    /// Pokud parametr nebude nalezen, vrátí false
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultBool"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckBoolArgument(RouteData nvc, string argname, out bool resultBool)
    {
        return PageHelperBase.CheckBoolArgument(GetStringFromRouteData(nvc, argname), out resultBool);
    }

    private static string GetStringFromRouteData(RouteData nvc, string argname)
    {
        object o = nvc.Values[argname];
        if (o != null)
        {
            return o.ToString();
        }
        return null;
    }

    /// <summary>
    /// Snaž se tuto metodu využívat co nejvíce kdy jen to půjde(nejde to když čárky mouhou být v datech) - čárka se totiž neenkóduje a zůstane ti tak mnohem více místa na samotný QueryString. Proto zde nedékoduji pomocí HttpDecode a musíš to dělat sám, pokud očekáváš nějaký takový vstup.
    /// Tato metoda vrací v A3 řetězce - nekontroluje co je mezi jedntlivými pipemi
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentCommaDelimiter(RouteData nvc, string argname, out string[] result)
    {
        return PageHelperBase.CheckStringArgumentCommaDelimiter(GetStringFromRouteData(nvc, argname), out result);
    }

    /// <summary>
    /// Snaž se tuto metodu využívat co nejméně, kvůli toho že pipe se se enkóduje pro přenos a zaždá pipe tak nezabere 1 znak ale rovnou 3. Tím pádem ještě před rozdělením používám HttpUtility.HtmlDecode a nemusím to tak dělat po použití této metody
    /// Tato metoda vrací v A3 řetězce - nekontroluje co je mezi jedntlivými pipemi
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentPipeDelimiter(RouteData nvc, string argname, out string[] result)
    {
        return PageHelperBase.CheckStringArgumentPipeDelimiter(GetStringFromRouteData(nvc, argname), out result);
    }

    /// <summary>
    /// Vrátí do A4 objekt URI pouze v případě, že v A1 bude URI kratší než A3
    /// Jinak vrátí do A4 null
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="maxLenght"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentURI(RouteData nvc, string argname, int maxLenght, out Uri uri)
    {
        return PageHelperBase.CheckStringArgumentURI(GetStringFromRouteData(nvc, argname), maxLenght, out uri);
    }

    /// <summary>
    /// Vrátí do A4 objekt URI pouze v případě, že v A1 bude URI kratší než A3
    /// Jinak vrátí do A4 null
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="maxLenght"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentURIBase64(RouteData nvc, string argname, int maxLenght, out Uri uri)
    {
        return PageHelperBase.CheckStringArgumentURIBase64(GetStringFromRouteData(nvc, argname), maxLenght, out uri);
    }

    /// <summary>
    /// Vrátí SE v případě že nebude vyparsován správný řetězec.
    /// Vrátí SE a ResultCheckWebArgument.Empty pokud řetězec bude prázdný nebo whitespace
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgument(RouteData nvc, string argname, out string result, bool trim)
    {
        return PageHelperBase.CheckStringArgument(GetStringFromRouteData(nvc, argname), out result, trim);
    }

    /// <summary>
    /// Vrátí SE v případě že nebude vyparsován správný řetězec.
    /// Vrátí SE a ResultCheckWebArgument.Empty pokud řetězec bude prázdný nebo whitespace
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgument(RouteData nvc, string argname, out string result)
    {
        return PageHelperBase.CheckStringArgument(GetStringFromRouteData(nvc, argname), out result);
    }

    public static ResultCheckWebArgument CheckEnumArgument<T>(RouteData nvc, string argname, out T mss)
    {
        return PageHelperBase.CheckEnumArgument<T>(GetStringFromRouteData(nvc, argname), out mss);
    }
}
