using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System;
using Google.Apis.Storage.v1;
using Google.Apis.Services;
using Google.Apis.Translate.v2;

public class TranslateHelper
    {
    static GoogleCredential credential = null;

    public static GoogleCredential AuthExplicit(string appName, string projectId, string jsonText)
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
    /// A2 = cs, en
    /// A3 = null, will be automatically determined
    /// </summary>
    /// <param name="input"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string Translate(string input, string to, string from = null)
    {
        CheckCredentials();

        TranslationClient client = TranslationClient.Create(credential);
        var response = client.TranslateText(input, to, from);
        return response.TranslatedText;
    }

    
    private static void CheckCredentials()
    {
        if (credential == null)
        {
            throw new Exception("Please authenticate first, credential object cant be null");
        }
    }
}
