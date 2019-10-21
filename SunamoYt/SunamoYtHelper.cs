using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using sunamo;
using sunamo.Constants;
using sunamo.Html;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace SunamoYt
{
    public class SunamoYtHelper
    {
        static Type type = typeof(SunamoYtHelper);
        public static Func<string, bool> existsYtVideo;

        const string uriPrefix = "/watch?v=";
        static ChangeQuotaExceededApiKeys changeQuotaExceededApiKeys = ChangeQuotaExceededApiKeys.Instance;

        public static Dictionary<SongFromInternet, float> SearchYtVideos( string nameArtist, string nameSong)
        {
            YouTubeService youtube = CreateYouTubeService();

            List<SongFromInternet> nameOfAllYTVideos = null;
            var sm = GetVideos(ref nameOfAllYTVideos, 3, nameArtist, nameSong, ref youtube);

            var l = CA.ToList<SongFromInternet>(sm.Keys);
            RemoveAlreadyExistedVideos(sm, l);

            if (sm.Count == 0)
            {
                var first = (SongFromInternet)nameOfAllYTVideos.FirstOrNull();

                if (first == null)
                {
                    sm = GetVideos(ref nameOfAllYTVideos, 30, nameArtist, nameSong, ref youtube);
                    l = CA.ToList<SongFromInternet>(sm.Keys);
                    RemoveAlreadyExistedVideos(sm, l);

                    if (sm.Count == 0)
                    {
                        string nas = nameArtist + AllStrings.swd + nameSong;
                        var ytSearch = UriWebServices.YouTube.GetLinkToSearch(nas);
                        string html = null;

#if DEBUG
                        //html = TF.ReadFile(DefaultPaths.sunamoProject + "HTMLPage1.html");
#elif !DEBUG
                        
#endif
                        Console.WriteLine("Download HTML for searching " + nas);
                        //riterEventLog.WriteToMainAppLog(, System.Diagnostics.EventLogeEntr);

                        html = HttpRequestHelper.GetResponseText(ytSearch, HttpMethod.Get, null);

                        var hd = HtmlAgilityHelper.CreateHtmlDocument();
                        hd.LoadHtml(html);

                        var title = HtmlAgilityHelper.NodesWithAttr(hd.DocumentNode, true, "*", "class", "yt-uix-tile-link", true);
                        nameOfAllYTVideos = new List<SongFromInternet>();

                        foreach (var item in title)
                        {
                            var href = HtmlAssistant.GetValueOfAttribute("href", item);

                            if (href.Contains(uriPrefix))
                            {
                                var inner = item.InnerHtml;

                                href = QSHelper.GetParameter(href, "v");
                                if (href != null)
                                {
                                    nameOfAllYTVideos.Add(new SongFromInternet(inner, href));
                                }
                            }
                        }

                        Console.WriteLine(nameOfAllYTVideos.Count + " result parsed from yt search result");

                        bool ukoncit = AddWithSimilarity(nameOfAllYTVideos, sm, nameArtist + AllStrings.swd + nameSong);

                        Console.WriteLine(sm.Count + " results has probality >= 0.5");

                        int i = 0;
                    }
                }
            }

            return sm;
        }

        private static void RemoveAlreadyExistedVideos(Dictionary<SongFromInternet, float> sm, List<SongFromInternet> l)
        {
            foreach (var item in l)
            {
                if (existsYtVideo.Invoke(item.ytCode))
                {
                    sm.Remove(item);
                }
            }
        }

        private static YouTubeService CreateYouTubeService()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                /* have used *FMBDWQ (sunamocz@gmail.com) before, 
                 * after remove ip adresses limiting, re-enable youtube api still:
                 * Access Not Configured. YouTube Data API has not been used in project 425397142436 before or it is disabled. Enable it by visiting https://console.developers.google.com/apis/api/youtube.googleapis.com/overview?project=425397142436 then retry. If you enabled this API recently, wait a few minutes for the action to propagate to our systems and retry. [403]
                 * *AzgTq0 is gdatayoutube from smutekutek
                 */
                ApiKey = changeQuotaExceededApiKeys.apiKey,
                ApplicationName = "whatever"
            });
        }

        public static IEnumerable GetComments(string ytCode)
        {
            YouTubeService youtube = CreateYouTubeService();

            return GetComments(ytCode, ref youtube);
        }

        static IEnumerable GetComments(string ytCode, ref YouTubeService youtube)
        {
            

            var listRequest = youtube.CommentThreads.List("snippet");
            listRequest.VideoId = ytCode;

            CommentThreadListResponse resp = null;

            try
            {
#if DEBUG
                    resp = listRequest.Execute();
#endif
            }
            catch (Exception ex)
            {
                if (HasBeenExceeded(ex, ref youtube))
                {
                    return GetComments(ytCode, ref youtube);
                }
                
            }

            foreach (var item in resp.Items)
            {
                var s = item.Snippet.ToString();
                var i = 0;
            }

            return null;
        }

        public static bool IsYtVideoAvailable(string ytCode)
        {
            YouTubeService youtube = CreateYouTubeService();

            return IsYtVideoAvailable(ytCode, ref youtube);
        }

        static bool IsYtVideoAvailable(string ytCode, ref YouTubeService youtube)
        {
            var listRequest = youtube.Videos.List("status");
            listRequest.Id = ytCode;

            VideoListResponse resp = null;

            try
            {
#if DEBUG
                resp = listRequest.Execute();
#endif
            }
            catch (Exception ex)
            {
                if (HasBeenExceeded(ex, ref youtube))
                {
                    return IsYtVideoAvailable(ytCode, ref youtube);
                }

            }

            foreach (var item in resp.Items)
            {
                //var s = item.Snippet.ToString();
                var status = item.Status;
                if (status == null)
                {
                    ThrowExceptions.Custom(type, RH.CallingMethod(), "Status is null");
                }
                else
                {
                    //status.PrivacyStatus
                    //status.RejectionReason;
                    //status.embeddable;


                    // first is uploaded, then is processed
                    if (status.UploadStatus != UploadStatuses.processed.ToString())
                    {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }

        static Dictionary<SongFromInternet, float> GetVideos(ref List<SongFromInternet> nameOfAllYTVideos, int maxResults, string nameArtist, string nameSong, ref YouTubeService youtube)
        {
            Dictionary<SongFromInternet, float> sm = null;
            sm = new Dictionary<SongFromInternet, float>();
            

            List<string> addedYtCode = new List<string>();
            //, " Lyrics", " Lyrics Video", " Live", " Live At"
            List<string> added = new List<string>(new string[] { "" });
            
            bool ukoncit = false;
            nameOfAllYTVideos = new List<SongFromInternet>();

            foreach (var item3 in added)
            {
                // 1 search má 100 units, there is 100 searches / days
                SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
                listRequest.MaxResults = 3;
                listRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
                listRequest.Q = nameArtist + AllStrings.swda + nameSong + item3;
                listRequest.Type = "video";
                listRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
                listRequest.VideoSyndicated = SearchResource.ListRequest.VideoSyndicatedEnum.Any;
                listRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Medium;

                SearchListResponse resp = null;
                try
                {
#if !DEBUG
                    resp = listRequest.Execute();
#endif
                }
                catch (Exception ex)
                {
                    //{"Google.Apis.Requests.RequestError\r\nThe request cannot be completed because you have exceeded your <a href=\"/youtube/v3/getting-started#quota\">quota</a>. [403]\r\nErrors [\r\n\tMessage[The request cannot be completed because you have exceeded your <a href=\"/youtube/v3/getting-started#quota\">quota</a>.] Location[ - ] Reason[quotaExceeded] Domain[youtube.quota]\r\n]\r\n"}
                    if (HasBeenExceeded(ex, ref youtube))
                    {
                        return GetVideos(ref nameOfAllYTVideos, maxResults, nameArtist, nameSong, ref youtube);
                    }

                    return sm;
                    //{"Google.Apis.Requests.RequestError\r\nAccess Not Configured. YouTube Data API has not been used in project 425397142436 before or it is disabled. Enable it by visiting https://console.developers.google.com/apis/api/youtube.googleapis.com/overview?project=425397142436 then retry. If you enabled this API recently, wait a few minutes for the action to propagate to our systems and retry. [403]\r\nErrors [\r\n\tMessage[Access Not Configured. YouTube Data API has not been used in project 425397142436 before or it is disabled. Enable it by visiting https://console.developers.google.com/apis/api/youtube.googleapis.com/overview?project=425397142436 then retry. If you enabled this API recently, wait a few minutes for the action to propagate to our systems and retry.] Location[ - ] Reason[accessNotConfigured] Domain[usageLimits]\r\n]\r\n"}
                }

                if (resp != null)
                {

                    foreach (SearchResult result in resp.Items)
                    {
                        nameOfAllYTVideos.Add(new SongFromInternet(result.Snippet.Title, result.Id.VideoId));
                    }

                    ukoncit = AddWithSimilarity(nameOfAllYTVideos, sm, listRequest.Q);

                    if (ukoncit)
                    {
                        break;
                    }
                }
            }

            return sm;
        }

        static bool HasBeenExceeded(Exception ex, ref YouTubeService youtube)
        {
            if (ex.Message.Contains("The request cannot be completed because you have exceeded your"))
            {
                changeQuotaExceededApiKeys.WriteExceeded();
                youtube = CreateYouTubeService();
                return true;
            }
            return false;
        }

        private static bool AddWithSimilarity(List<SongFromInternet> nameOfAllYTVideos, Dictionary<SongFromInternet, float> sm, string listRequestQ)
        {
            List<string> addedYtCode = new List<string>();
            bool ukoncit = false;

            foreach (SongFromInternet item2 in nameOfAllYTVideos)
            {
                if (!addedYtCode.Contains(item2.ytCode))
                {
                    float vypoctiPodobnost = item2.CalculateSimilarity(listRequestQ);
                    if (vypoctiPodobnost >= 0.5f)
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

            return ukoncit;
        }
    }
}
