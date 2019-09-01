using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using sunamo;
using System;
using System.Collections.Generic;

namespace SunamoYt
{
    public class SunamoYtHelper
    {


        public static Dictionary<SongFromInternet, float> SearchYtVideos( string nameArtist, string nameSong)
        {
            ChangeQuotaExceededApiKeys changeQuotaExceededApiKeys = ChangeQuotaExceededApiKeys.Instance;

            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                /* have used *FMBDWQ (sunamocz@gmail.com) before, 
                 * after remove ip adresses limiting, re-enable youtube api still:
                 * Access Not Configured. YouTube Data API has not been used in project 425397142436 before or it is disabled. Enable it by visiting https://console.developers.google.com/apis/api/youtube.googleapis.com/overview?project=425397142436 then retry. If you enabled this API recently, wait a few minutes for the action to propagate to our systems and retry. [403]
                 * *AzgTq0 is gdatayoutube from smutekutek
                 */
                ApiKey = changeQuotaExceededApiKeys.apiKey,
                ApplicationName = "whatever"
            });

            Dictionary<SongFromInternet, float> sm = null;
            List<string> addedYtCode = new List<string>();
            //, " Lyrics", " Lyrics Video", " Live", " Live At"
            List<string> added = new List<string>(new string[] { "" });
            sm = new Dictionary<SongFromInternet, float>();
            bool ukoncit = false;

            foreach (var item3 in added)
            {
                // 1 search má 100 units, there is 100 searches / days
                SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
                listRequest.MaxResults = 5;
                listRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
                listRequest.Q = nameArtist + AllStrings.swda + nameSong + item3;
                listRequest.Type = "video";
                listRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
                listRequest.VideoSyndicated = SearchResource.ListRequest.VideoSyndicatedEnum.Any;
                listRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Medium;

                SearchListResponse resp = null;
                try
                {
                    resp = listRequest.Execute();
                }
                catch (Exception ex)
                {
                    //{"Google.Apis.Requests.RequestError\r\nThe request cannot be completed because you have exceeded your <a href=\"/youtube/v3/getting-started#quota\">quota</a>. [403]\r\nErrors [\r\n\tMessage[The request cannot be completed because you have exceeded your <a href=\"/youtube/v3/getting-started#quota\">quota</a>.] Location[ - ] Reason[quotaExceeded] Domain[youtube.quota]\r\n]\r\n"}
                    if (ex.Message.Contains("The request cannot be completed because you have exceeded your"))
                    {
                        changeQuotaExceededApiKeys.WriteExceeded();
                    }

                    return sm;
                    //{"Google.Apis.Requests.RequestError\r\nAccess Not Configured. YouTube Data API has not been used in project 425397142436 before or it is disabled. Enable it by visiting https://console.developers.google.com/apis/api/youtube.googleapis.com/overview?project=425397142436 then retry. If you enabled this API recently, wait a few minutes for the action to propagate to our systems and retry. [403]\r\nErrors [\r\n\tMessage[Access Not Configured. YouTube Data API has not been used in project 425397142436 before or it is disabled. Enable it by visiting https://console.developers.google.com/apis/api/youtube.googleapis.com/overview?project=425397142436 then retry. If you enabled this API recently, wait a few minutes for the action to propagate to our systems and retry.] Location[ - ] Reason[accessNotConfigured] Domain[usageLimits]\r\n]\r\n"}
                    
                }

                if (resp != null)
                {
                    List<SongFromInternet> nameOfAllYTVideos = new List<SongFromInternet>();
                    foreach (SearchResult result in resp.Items)
                    {
                        nameOfAllYTVideos.Add(new SongFromInternet(result.Snippet.Title, result.Id.VideoId));
                    }

                    foreach (SongFromInternet item2 in nameOfAllYTVideos)
                    {
                        if (!addedYtCode.Contains(item2.ytCode))
                        {
                            float vypoctiPodobnost = item2.CalculateSimilarity(listRequest.Q);
                            if (vypoctiPodobnost > 0.5f)
                            {
                                sm.Add(item2, vypoctiPodobnost);
                                addedYtCode.Add(item2.ytCode);
                                if (vypoctiPodobnost == 1)
                                {
                                    ukoncit = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (ukoncit)
                    {
                        break;
                    }
                }
            }

            return sm;
        }
    }
}
