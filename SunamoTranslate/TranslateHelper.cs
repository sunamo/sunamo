using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System;
using Google.Apis.Storage.v1;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using System.Collections.Generic;
/// <summary>
/// Must be instance, because calling static method dont call constructor and will throw not implemented exception
/// </summary>
public class TranslateHelper
    {
     GoogleCredential credential = null;

    public  GoogleCredential AuthExplicit(string appName, string projectId, string jsonText)
    {
        //string projectId = "insertintoxlfand-1558628742466";
        if (credential == null)
        {
            credential = GoogleCredential.FromJson(jsonText);
            // Inject the Cloud Storage scope if required.
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    StorageService.Scope.CloudPlatform,
                    TranslateService.Scope.CloudTranslation
                });
            }
            var storage = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = appName,
            });
        }
        return credential;
    }

    /// <summary>
    /// Due to saving and reading already translated cs to en in txt
    /// </summary>
    public static TranslateHelper Instance = new TranslateHelper();
    public  readonly string AlreadyTranslatedFile = AppData.ci.GetFileCommonSettings("CsTranslatedToEn.txt");
     Dictionary<string, string> csToEn = new Dictionary<string, string>();


    private TranslateHelper()
    {
        var data = SF.GetAllElementsFile(AlreadyTranslatedFile);
        foreach (var line in data)
        {
            var key = line[0];
            if (!csToEn.ContainsKey(key))
            {
                csToEn.Add(key, line[1]);
            }
            
        }
    }

    /// <summary>
    /// A2 = cs, en
    /// A3 = null, will be automatically determined
    /// </summary>
    /// <param name="input"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public  string Translate(string input, string to, string from = null)
    {
        if (from.Contains("cs") && to.Contains("en"))
        {
            if (csToEn.ContainsKey(input))
            {
                return csToEn[input];
            }
        }

        CheckCredentials();

        TranslationClient client = TranslationClient.Create(credential);

        var response = client.TranslateText(input, to, from);
        var result = response.TranslatedText;
        if (from.Contains("cs") && to.Contains("en"))
        {
            SF.AppendToFile(AlreadyTranslatedFile, SF.PrepareToSerialization2(CA.ToListString(input, result)));
        }
        return result;
    }
    
    private  void CheckCredentials()
    {
        if (credential == null)
        {
            throw new Exception("Please authenticate first, credential object cant be null");
        }
    }
}
