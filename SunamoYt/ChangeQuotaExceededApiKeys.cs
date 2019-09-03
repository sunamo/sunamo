using GDataYouTube;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


public class ChangeQuotaExceededApiKeys
{
    public string apiKey = null;
    int apiKeyIndex = 0;

    int substractLocalTimeForHours = 9;

    string youtubeApiKeyFile = null;
    CultureInfo ci = CultureInfo.InvariantCulture;

    public static ChangeQuotaExceededApiKeys Instance = new ChangeQuotaExceededApiKeys();

    private ChangeQuotaExceededApiKeys()
    {
        youtubeApiKeyFile = AppData.ci.GetFile(AppFolders.Data, "YoutubeQuotaExceededApiKeys.txt");

        List<string> hlavicka = null;
        SF.GetAllElementsFileAdvanced(youtubeApiKeyFile, out hlavicka);

        apiKeyIndex = -1;
        int dex = -1;

        if (hlavicka.Count > 1)
        {
            dex = BTS.TryParseInt(hlavicka[0], apiKeyIndex);
            // Here I parse date substracted from local with substractLocalTimeForHours on which I insert bad api key
            var date = BTS.TryParseDateTime(hlavicka[1], ci, DateTime.Now);

            //.AddHours(substractLocalTimeForHours * -1) - due to usa time
            //.AddDays(-1) - whether is active limit
            if (date < DateTime.Now.AddHours(substractLocalTimeForHours * -1).AddDays(-1))
            {
                TrySetUpNewApiKey(0);
            }
            else
            {
                TrySetUpNewApiKey(dex+1);
            }
        }
        else
        {
            TrySetUpNewApiKey(0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dex"></param>
    private  void TrySetUpNewApiKey(int dex)
    {
       
        if (YouTubeConsts.gDataYoutubeApiKeys.Length() > dex)
        {
            apiKeyIndex = dex;
            apiKey = YouTubeConsts.gDataYoutubeApiKeys[dex];
        }
    }

    public string ApiKey()
    {
        return apiKey;
    }

    /// <summary>
    /// Write Actual api key index as exceeded
    /// </summary>
    internal void WriteExceeded()
    {
        
        var line = SF.PrepareToSerializationExplicit(CA.ToListString(apiKeyIndex, DateTime.Now.AddHours(substractLocalTimeForHours * -1).ToString(ci)));
        TF.WriteAllText(youtubeApiKeyFile, line);
        apiKeyIndex++;
        TrySetUpNewApiKey(apiKeyIndex);
    }
}

