using System;
using System.Collections.Specialized;

public static class PageHelper
{
    /// <summary>
    /// Vrátí -1 v případě zjištěné jakékoliv chyby
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultInt"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckIntArgument(NameValueCollection nvc, string argname, out int resultInt)
    {
        return PageHelperBase.CheckIntArgument(nvc.Get(argname), out resultInt);
    }

    public static ResultCheckWebArgument CheckDoubleArgument(NameValueCollection nvc, string argname, out double resultDouble)
    {
        return PageHelperBase.CheckDoubleArgument(nvc.Get(argname), out resultDouble);
    }

    public static ResultCheckWebArgument CheckLongArgument(NameValueCollection qs, string argname, out long resultLong)
    {
        return PageHelperBase.CheckLongArgument(qs.Get(argname), out resultLong);
    }

    /// <summary>
    /// Vrací do A3 0 v případě jakékoliv chyby
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckByteArgument(NameValueCollection nvc, string argname, out byte resultByte)
    {
        return PageHelperBase.CheckByteArgument(nvc.Get(argname), out resultByte);
    }

    /// <summary>
    /// Vrátí short.MaxValue pokud se nenaleznou data k vyparsování když A1, jinak -1
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultShort"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckShortArgument(NameValueCollection nvc, string argname, out short resultShort)
    {
        return PageHelperBase.CheckShortArgument(false, nvc.Get(argname), out resultShort);
    }

    /// <summary>
    /// Vrátí short.MaxValue pokud se nenaleznou data k vyparsování
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultShort"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckShortArgument(bool signed, NameValueCollection nvc, string argname, out short resultShort)
    {
        return PageHelperBase.CheckShortArgument(signed, nvc.Get(argname), out resultShort);
    }

    /// <summary>
    /// Pokud parametr nebude nalezen, vrátí false
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultBool"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckBoolArgument(NameValueCollection nvc, string argname, out bool resultBool)
    {
        return PageHelperBase.CheckBoolArgument(nvc.Get(argname), out resultBool);
    }

    /// <summary>
    /// Snaž se tuto metodu využívat co nejvíce kdy jen to půjde(nejde to když čárky mouhou být v datech) - čárka se totiž neenkóduje a zůstane ti tak mnohem více místa na samotný QueryString. Proto zde nedékoduji pomocí HttpDecode a musíš to dělat sám, pokud očekáváš nějaký takový vstup.
    /// Tato metoda vrací v A3 řetězce - nekontroluje co je mezi jedntlivými pipemi
    /// Pokud A1[A2] nebude obsahovat čárku, metoda vrátí WrongRange.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentCommaDelimiter(NameValueCollection nvc, string argname, out string[] result)
    {
        return PageHelperBase.CheckStringArgumentCommaDelimiter(nvc.Get(argname), out result);
    }

    /// <summary>
    /// Snaž se tuto metodu využívat co nejméně, kvůli toho že pipe se se enkóduje pro přenos a zaždá pipe tak nezabere 1 znak ale rovnou 3. Tím pádem ještě před rozdělením používám HttpUtility.HtmlDecode a nemusím to tak dělat po použití této metody
    /// Tato metoda vrací v A3 řetězce - nekontroluje co je mezi jedntlivými pipemi
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentPipeDelimiter(NameValueCollection nvc, string argname, out string[] result)
    {
        return PageHelperBase.CheckStringArgumentPipeDelimiter(nvc.Get(argname), out result);
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
    public static ResultCheckWebArgument CheckStringArgumentURI(NameValueCollection nvc, string argname, int maxLenght, out Uri uri)
    {
        return PageHelperBase.CheckStringArgumentURI(nvc.Get(argname), maxLenght, out uri);
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
    public static ResultCheckWebArgument CheckStringArgumentURIBase64(NameValueCollection nvc, string argname, int maxLenght, out Uri uri)
    {
        return PageHelperBase.CheckStringArgumentURIBase64(nvc.Get(argname), maxLenght, out uri);
    }

    /// <summary>
    /// Vrátí SE v případě že nebude vyparsován správný řetězec.
    /// Vrátí SE a ResultCheckWebArgument.Empty pokud řetězec bude prázdný nebo whitespace
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgument(NameValueCollection nvc, string argname, out string result, bool trim)
    {
        return PageHelperBase.CheckStringArgument(nvc.Get(argname), out result, trim);
    }

    /// <summary>
    /// Vrátí SE v případě že nebude vyparsován správný řetězec.
    /// Vrátí SE a ResultCheckWebArgument.Empty pokud řetězec bude prázdný nebo whitespace
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgument(NameValueCollection nvc, string argname, out string result)
    {
        return PageHelperBase.CheckStringArgument(nvc.Get(argname), out result);
    }




    /// <summary>
    /// Pokud A1[A2] bude obsahovat písmeno v jiné case rozdílné od originálu, G NotFound
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="qs"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckEnumArgument<T>(NameValueCollection qs, string argname, out T result)
    {
        return PageHelperBase.CheckEnumArgument<T>(qs.Get(argname), out result);
    }

    public static ResultCheckWebArgument CheckDateArgument(NameValueCollection qs, string argname, out DateTime result)
    {
        return PageHelperBase.CheckDateArgument(qs.Get(argname), out result);
    }
}
