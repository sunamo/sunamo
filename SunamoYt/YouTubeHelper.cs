using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GDataYouTube;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


/// <summary>
/// Must be in ConsoleApp1, with SunamoCzAdmin is only two solutions which are allowed to import YouTube API
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
        /// Cant be use in UWP app because access publicly to c:\Users and app throw exception
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
        var d = new EmbeddedResourcesH(typeof(YouTubeConsts).Assembly, "GDataYoutube");
        using (var stream = d.GetStream( YouTubeConsts.secret))
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