using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


    /// <summary>
    /// YouTube Data API v3 sample: create a playlist.
    /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
    /// See https://developers.google.com/api-client-library/dotnet/get_started
    /// </summary>
    public static class YouTubeHelper
    {
    static Type type = typeof(YouTubeHelper);

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public static List<string> GetYtCodesFromUri(List<string> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                var s = l[i];
                if (RegexHelper.IsUri(s))
                {
                    
                    l[i] = QSHelper.GetParameter(s, "v");
                }
                
            }

            return l;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ytCodes"></param>
        /// <returns></returns>
        public static async Task CreateNewPlaylist(string name, List<string> ytCodes)
        {
            CA.RemoveStringsEmpty(ytCodes);

            // Neustale mi to vytvari playlisty na puvodnim sunamocz@gmail.com, i prtesto ze json je stazeny se smutekutek
            #region MyRegion
            UserCredential credential;
            using (var stream = new FileStream(YouTubeConsts.secret, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(type.ToString())
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = type.ToString()
            });
            #endregion

            //bacha, klidne vytvori dalsi playlist se stejnym jmenem

            // Create a new, private playlist in the authorized user's channel.
            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = name;
            newPlaylist.Snippet.Description = "A playlist created with the YouTube API v3";
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "public";
            newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

            // I have to take attention whether dont contains actually otherwise I get it duplicated
            foreach (var item in ytCodes)
            {
                // Add a video to the newly created playlist.
                var newPlaylistItem = new PlaylistItem();
                newPlaylistItem.Snippet = new PlaylistItemSnippet();
                newPlaylistItem.Snippet.PlaylistId = newPlaylist.Id;
                newPlaylistItem.Snippet.ResourceId = new ResourceId();
                newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
                newPlaylistItem.Snippet.ResourceId.VideoId = item;
                newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();

                Console.WriteLine("Added " + item);
                //Console.WriteLine("Playlist item id {0} was added to playlist id {1}.", newPlaylistItem.Id, newPlaylist.Id);
            }
        }
    }