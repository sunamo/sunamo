using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

public static class PageHelperBase
{
    /// <summary>
    /// Vrátí -1 v případě zjištěné jakékoliv chyby
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultInt"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckIntArgument(string arg, out int resultInt)
    {
        return CheckIntArgument(false, arg, out resultInt);
    }

    public static ResultCheckWebArgument CheckIntArgument(bool signed, string arg, out int resultInt)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (int.TryParse(trimmed, out resultInt))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        if (signed)
        {
            resultInt = int.MaxValue;
        }
        else
        {
            resultInt = -1;
        }
        return vr;
    }

    public static ResultCheckWebArgument CheckDoubleArgument(string arg, out double resultDouble)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.GetCultureInfo("cs-CZ"), out resultDouble))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        resultDouble = -1;
        return vr;
    }

    public static ResultCheckWebArgument CheckDateArgument(string arg, out DateTime result)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (DateTime.TryParse(trimmed.Replace('_', '.'), out result))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        result = DateTime.Today;
        return vr;
    }

    public static ResultCheckWebArgument CheckLongArgument(string arg, out long resultLong)
    {
        return CheckLongArgument(false, arg, out resultLong);
    }

    public static ResultCheckWebArgument CheckLongArgument(bool signed, string arg, out long resultLong)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (long.TryParse(trimmed, out resultLong))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        if (signed)
        {
            resultLong = long.MaxValue;
        }
        else
        {
            resultLong = -1;
        }
        return vr;
    }

    /// <summary>
    /// Vrací do A3 0 v případě jakékoliv chyby
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckByteArgument(string arg, out byte resultByte)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (byte.TryParse(trimmed, out resultByte))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        resultByte = 0;
        return vr;
    }

    /// <summary>
    /// Vrátí short.MaxValue pokud se nenaleznou data k vyparsování když A1, jinak -1
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultShort"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckShortArgument(bool signed, string arg, out short resultShort)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (short.TryParse(trimmed, out resultShort))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        if (signed)
        {
            resultShort = short.MaxValue;
        }
        else
        {
            resultShort = -1;
        }
        
        return vr;
    }

    /// <summary>
    /// Pokud parametr nebude nalezen, vrátí false
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="resultBool"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckBoolArgument(string arg, out bool resultBool)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (bool.TryParse(trimmed, out resultBool))
                {
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        resultBool = false;
        return vr;
    }

    /// <summary>
    /// Snaž se tuto metodu využívat co nejvíce kdy jen to půjde(nejde to když čárky mouhou být v datech) - čárka se totiž neenkóduje a zůstane ti tak mnohem více místa na samotný QueryString. Proto zde nedékoduji pomocí HttpDecode a musíš to dělat sám, pokud očekáváš nějaký takový vstup.
    /// Tato metoda vrací v A3 řetězce - nekontroluje co je mezi jedntlivými pipemi
    /// Pokud A1 nebude obsahovat čárku, metoda vrátí WrongRange.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentCommaDelimiter(string arg, out string[] result)
    {
        //string dd = HttpUtility.UrlDecode(nvc[key]);
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        
        if (arg != null)
        {
            string trimmed = arg.Trim();
            if (trimmed.Length > 0)
            {
                if (trimmed.Contains(","))
                {
                    result = SH.Split(trimmed, ",");
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        result = new string[0];
        return vr;
    }

    /// <summary>
    /// Snaž se tuto metodu využívat co nejméně, kvůli toho že pipe se se enkóduje pro přenos a zaždá pipe tak nezabere 1 znak ale rovnou 3. Tím pádem ještě před rozdělením používám HttpUtility.HtmlDecode a nemusím to tak dělat po použití této metody
    /// Tato metoda vrací v A3 řetězce - nekontroluje co je mezi jedntlivými pipemi
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgumentPipeDelimiter(string arg, out string[] result)
    {
        //string dd = HttpUtility.UrlDecode(nvc[key]);
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        
        if (arg != null)
        {
            string trimmed = HttpUtility.UrlDecode(arg.Trim());
            if (trimmed.Length > 0)
            {
                if (trimmed.Contains("|"))
                {
                    result = SH.Split(trimmed, "|");
                    return vr;
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        result = new string[0];
        return vr;
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
    public static ResultCheckWebArgument CheckStringArgumentURI(string arg, int maxLenght, out Uri uri)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = HttpUtility.UrlDecode(arg.Trim());
            if (trimmed.Length > 0)
            {
                if (trimmed.Length < maxLenght)
                {
                    try
                    {
                        uri = new Uri(trimmed);
                        return vr;
                    }
                    catch (Exception)
                    {
                        vr = ResultCheckWebArgument.WrongRange;
                    }
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        uri = null;
        return vr;
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
    public static ResultCheckWebArgument CheckStringArgumentURIBase64(string arg, int maxLenght, out Uri uri)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        
        if (arg != null)
        {
            string trimmed = ConvertBase64.From(arg.Trim()).Trim();
            if (trimmed.Length > 0)
            {
                if (trimmed.Length < maxLenght)
                {


                    try
                    {
                        uri = new Uri(trimmed);
                        return vr;
                    }
                    catch (Exception)
                    {
                        vr = ResultCheckWebArgument.WrongRange;
                    }
                }
                else
                {
                    vr = ResultCheckWebArgument.WrongRange;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        uri = null;
        return vr;
    }

    /// <summary>
    /// Vrátí SE v případě že nebude vyparsován správný řetězec.
    /// Vrátí SE a ResultCheckWebArgument.Empty pokud řetězec bude prázdný nebo whitespace
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgument(string arg, out string result, bool trim)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            if (trim)
            {
                arg = arg.Trim();
            }
            string trimmed = HttpUtility.UrlDecode(arg);
            if (trimmed.Length > 0)
            {
                result = trimmed;
                return vr;
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        result = "";
        return vr;
    }

    /// <summary>
    /// Vrátí SE v případě že nebude vyparsován správný řetězec.
    /// Vrátí SE a ResultCheckWebArgument.Empty pokud řetězec bude prázdný nebo whitespace
    /// </summary>
    /// <param name="nvc"></param>
    /// <param name="argname"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultCheckWebArgument CheckStringArgument(string arg, out string result)
    {
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = HttpUtility.UrlDecode(arg.Trim());
            if (trimmed.Length > 0)
            {
                result = trimmed;
                return vr;
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        result = "";
        return vr;
    }

    public static string GetErrorDescriptionBoolURI(ResultCheckWebArgument dd, string argname)
    {
        return GetErrorDescriptionLogical(dd, argname);
    }

    public static string GetErrorDescriptionByteURI(ResultCheckWebArgument dd, string argname)
    {
        string minValue = byte.MinValue.ToString();
        string maxValue = byte.MaxValue.ToString();
        return GetErrorDescriptionNumeric(dd, argname, minValue, maxValue);
    }

    static string dtMinS = DateTime.MinValue.ToShortDateString() + " " + DateTime.MinValue.ToLongTimeString() + ":000";
    static string dtMaxS = DateTime.MaxValue.ToShortDateString() + " " + DateTime.MaxValue.ToLongTimeString() + ":999";

    public static string GetErrorDescriptionDateTime(ResultCheckWebArgument dd, string argname)
    {
        return GetErrorDescriptionNumeric(dd, argname, dtMinS, dtMaxS);
    }

    private static string GetErrorDescriptionNumeric(ResultCheckWebArgument dd, string argname, string minValue, string maxValue)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale nebyl v rozmezí " + minValue + "-" + maxValue + "";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    private static string GetErrorDescriptionLogical(ResultCheckWebArgument dd, string argname)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale nebyl v hodnotě True nebo False";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    public static string GetErrorDescriptionShortURI(ResultCheckWebArgument raid, string argname)
    {
        return GetErrorDescriptionNumeric(raid, argname, short.MinValue.ToString(), short.MaxValue.ToString());
    }

    public static string GetErrorDescriptionIntURI(ResultCheckWebArgument raid, string argname)
    {
        return GetErrorDescriptionNumeric(raid, argname, int.MinValue.ToString(), int.MaxValue.ToString());
    }

    public static string GetErrorDescriptionDoubleURI(ResultCheckWebArgument raid, string argname)
    {
        return GetErrorDescriptionNumeric(raid, argname, double.MinValue.ToString(), double.MaxValue.ToString());
    }

    public static string GetErrorDescriptionLongURI(ResultCheckWebArgument raid, string argname)
    {
        return GetErrorDescriptionNumeric(raid, argname, long.MinValue.ToString(), long.MaxValue.ToString());
    }

    public static string GetErrorDescriptionStringURI(ResultCheckWebArgument dd, string argname)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Web zřejmě zavolal špatnou metodu - na obyčejný řetězec se nemůže aplikovat chyba WrongRange";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    /// <summary>
    /// T nevyužívá k ničemu
    /// Nevypisuje žádnou hodnotu z A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dd"></param>
    /// <param name="argname"></param>
    /// <returns></returns>
    public static string GetErrorDescriptionEnumURI<T>(ResultCheckWebArgument dd, string argname)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Web zřejmě zavolal špatnou metodu - na obyčejný řetězec se nemůže aplikovat chyba WrongRange";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI. Je důležité dodržovat přesné velikosti písmen a nepozměňovat nijak adresu URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    public static string GetErrorDescriptionStringPipeDelimiterURI(ResultCheckWebArgument dd, string argname)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Byl zadán text do argumentu " + argname + " ale nebyla to platná adresa URI nebo měla delší než povolenou délku nebo v ní nebyl nalezen oddělovač";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    public static string GetErrorDescriptionStringCommaDelimiterURI(ResultCheckWebArgument dd, string argname)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale nebyl v něm nalezen oddělovač(pipe, čárka, etc.)";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    public static string GetErrorDescriptionUriURI(ResultCheckWebArgument dd, string argname)
    {
        string vr = "";
        switch (dd)
        {
            case ResultCheckWebArgument.WrongRange:
                vr = "error: Argument " + argname + " nebyla uri ve správném formátu";
                break;
            case ResultCheckWebArgument.Empty:
                vr = "error: Argument " + argname + " byl nalezen v adrese URI ale prázdný";
                break;
            case ResultCheckWebArgument.NotFound:
                vr = "error: Argument " + argname + " nebyl nalezen v adrese URI";
                break;
            case ResultCheckWebArgument.AllOk:
                throw new Exception("Pokud je vše OK(AllOK), nemůžu vrátit žádnou chybu");
            default:
                break;
        }
        return vr;
    }

    public static ResultCheckWebArgument CheckEnumArgument<T>(string arg, out T result)
    {
        Type typ = typeof(T);
        List<string> vsechnyHodnoty = new List<string>(Enum.GetNames(typ));
        ResultCheckWebArgument vr = ResultCheckWebArgument.AllOk;
        if (arg != null)
        {
            string trimmed = HttpUtility.UrlDecode(arg).Trim();//.ToLower();
            if (trimmed.Length > 0)
            {
                if (vsechnyHodnoty.Contains(trimmed))
                {
                    result = (T)Enum.Parse(typ, trimmed, false);
                    return vr;
                }
                else
                {
                    result = default(T);
                    return ResultCheckWebArgument.NotFound;
                }
            }
            else
            {
                vr = ResultCheckWebArgument.Empty;
            }
        }
        else
        {
            vr = ResultCheckWebArgument.NotFound;
        }
        result = default(T);
        return vr;
    }
}
