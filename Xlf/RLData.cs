using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SunamoExceptions;
using Xlf;

/// <summary>
/// Is here dont mix RL and RLData with intellisense
/// </summary>
public static class RLData
{
    static Type type = typeof(RLData);

    // In case of serious problem I can use TranslateDictionary
    public static TranslateDictionary en = new TranslateDictionary(Langs.en);
    public static TranslateDictionary cs = new TranslateDictionary(Langs.cs);

    public static string EnPostColon(string key)
    {
        ThrowExceptions.KeyNotFound<string, string>(null, type, Exc.CallingMethod(), en, "en", key);

        return en[key] + AllStrings.colon;
    }
}



