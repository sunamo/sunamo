using Lastfm.Services;
using sunamo;
using sunamo.Essential;
using sunamo.Helpers;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Xml;
public class LastFm //: IMusicWebService<LastFmArtist, LastFmAlbum, LastFmArtist, LastFmAlbum>
{
    bool off = false;
    public static LastFm ci = new LastFm();
    const string un = "sunamoDevProg";
    const string apiKey = "68ae15739cd690ce04679a15b5583fd4";
    const string sharedSecret = "8a395e8bd3125a34e4754c62af6e4b4d";

    private LastFm()
    {

    }

    public bool ParseErrorLastFmError(string xml, out LastFmErrors e)
    {
        /*
<lfm status="failed">
<error code="6">The artist you supplied could not be found</error>
</lfm>
         */

        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(xml);

        var error = XmlAgilityHelper.Node(hd.DocumentNode, true, "error");
        if (error != null)
        {
            var d = HtmlAssistant.GetValueOfAttribute("code", error);
            e = (LastFmErrors)byte.Parse(d);
            return true;
        }
        e = LastFmErrors.None;
        return false;
    }

    /// <summary>
    /// Used in SczAdmin
    /// Nepoužívat kvůli library!!
    /// </summary>
    /// <param name="t"></param>
    public Album GetAlbumOfTrack(Track t)
    {
        if (!off)
        {
            try
            {
                return t.GetAlbum();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(RLData.en[XlfKeys.TrackNotFound]))
                {

                }
                else if (ex.Message.Contains(RLData.en[XlfKeys.ArtistNotFound]))
                {

                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Used in SczAdmin
    /// Pokud se nepodaří nalézt, vrátí do A3 null
    /// Ve třídě Spotify se metoda jmenuje GetAlbumOfTrack
    /// Používá se pro uložení obrázků alba na disk
    /// Vrátí do A3 již převedené na konvenci
    /// </summary>
    /// <param name="artist"></param>
    /// <param name="title"></param>
    /// <param name="album"></param>
    public Album GetAlbumFromApi(string artist, string title, out string album)
    {
        //bool isAlbumFetched = true;
        album = null;

        if (off)
        {
            return null;
        }

        Track t = new Track(artist, title, SunamoLastFmConsts.session);
        Album a = GetAlbumOfTrack(t);
        if (a == null)
        {
        }
        else
        {
            if (!string.IsNullOrEmpty(a.Name))
            {
                album = ConvertEveryWordLargeCharConvention.ToConvention(a.Name);
            }
            else if (!string.IsNullOrEmpty(a.Title))
            {
                album = ConvertEveryWordLargeCharConvention.ToConvention(a.Title);
            }
            else
            {
                // Již bude nastaveno na null
            }
        }
        return a;
    }

    #region IUN
    public string GetImageURL(Album albumO, LyricsImageSize extraLarge)
    {
        try
        {
            return albumO.GetImageURL((AlbumImageSize)extraLarge);
        }
        catch (Exception ex )
        {
            return null;
        }
    }

    /// <summary>
    /// Vrátí null, pokud se interpreta nepodaří nalézt nebo pokud bude empty
    /// V last.fm se metoda jmenuje GetArtist
    /// </summary>
    /// <param name="artist"></param>
    /// <param name="title"></param>
    public Artist GetArtistFromApi(string artist)
    {
        if (off)
        {
            return null;
        }

        Artist a = new Artist(artist, SunamoLastFmConsts.session);

        if (a == null)
        {
        }
        else
        {
            if (!string.IsNullOrEmpty(a.Name))
            {
            }
            else
            {
                a = null;
            }
        }
        return a;
    }

    /// <summary>
    /// Nahrazuje ji metoda 
    /// Vrátí null, pokud se album nepodaří nalézt nebo pokud bude empty
    /// </summary>
    /// <param name="artist"></param>
    /// <param name="title"></param>
    public Artist GetArtistFromApi(string artist, string title)
    {
        if (off)
        {
            return null;
        }

        Track t = new Track(artist, title, SunamoLastFmConsts.session);

        Artist a = t.Artist;
        if (a == null)
        {
        }
        else
        {
            if (!string.IsNullOrEmpty(a.Name))
            {
            }
            else
            {
                a = null;
            }
        }
        return a;
    }

    #region Získávání MBID - už nepoužívám
    #endregion

    

    #endregion

    #region Používám
    /// <summary>
    /// Používá metodu API artist.getSimilar
    /// </summary>
    /// <param name="a"></param>
    public Artist[] GetSimilarOfArtist(Artist a)
    {
        if (!off)
        {
            try
            {
                return a.GetSimilar();
            }
            catch 
            {
                if (false)
                {

                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }


    /// <summary>
    /// Vrátí null, pokud bude vypnuté používaní API
    /// Vrátí 0, pokud se nepodaří získat
    /// Automaticky updatuje sloupec CountTags v Lyr_Artist
    /// </summary>
    /// <param name="a"></param>
    public List<string> SetTagsOfArtist(string artistName, short artistID)
    {
        HttpResponseMessage response = null;

        if (off)
        {
            return null;
        }

        //artistName = "Red Hot Chili Peppers";
        string xml = null;
        try
        {
            // Replacing only spaces is not succifient due to chars like & in name of artist

            if (artistName == "Soundtrack Ledové království II" || artistName == "Marina (& The Diamonds)")
            {

            }
            
            var uri = "https://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=" + UrlEncode( artistName) + "&user=" + un + "&api_key=" + apiKey;
            xml = HttpClientHelper.GetResponseText(uri, HttpMethod.Get, null, out response);

            

            bool error = false;
            LastFmErrors e = LastFmErrors.None;
            if (ParseErrorLastFmError(xml, out e))
            {
                error = true;

                if (e != LastFmErrors.InvalidParameters)
                {
                    Debugger.Break();
                }
            }

            if ( xml.StartsWith(Consts.Exception))
            {
                error = true;
            }

            if (error)
            {
                if (!Exc.aspnet)
                {
                    if (e != LastFmErrors.None)
                    {
                        var space = AllStrings.space;
                        xml = e.ToString() + space + artistName + space + xml ;
                    }

                    //WriterEventLog.WriteToMainAppLog(xml, EventLogEntryType.Error, Exc.CallingMethod());
                }

                return new List<string>();
            }
        }
        catch (Exception ex)
        {

        }

        List<string> result = new List<string>();
        //int tagsCount = 0;
        List<short> idTags = new List<short>();
        if (!HttpResponseHelper.SomeError(response))
        { 
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNodeList artistTag = xd.SelectNodes("/lfm/toptags/tag/name");
            //tagsCount = 0;
            foreach (XmlNode item2 in artistTag)
            {
                //int pridano = 0;
                if (item2 is XmlElement)
                {
                    if (idTags.Count == 255)
                    {
                        break;
                    }

                    XmlElement ixe = item2 as XmlElement;
                    string tagName = ixe.InnerText;
                    if (tagName.Length > 100)
                    {
                        tagName = tagName.Substring(0, 100);
                    }
                    tagName = tagName.ToLower();

                    result.Add(tagName);

}
            }
        }
        return result; 
    }

    /// <summary>
    /// Vrátí SE, pokud se nepodaří získat
    /// </summary>
    /// <param name="a"></param>
    public string GetBioSummaryOfArtist(Artist a)
    {
        if (off)
        {
            return "";
        }
        try
        {
            string s = a.Bio.GetSummary();
            return s.Trim();
        }
        catch
        {
            if (false)
            {

            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// Tuto metodu nikdy nevolat z AddSong, protože by to bylo neúnosně pomalé, viz. iamx
    /// Vrátí null, pokud se nepodaří získat
    /// 
    /// getEvents is not definitely available on last.fm api
    /// https://www.songkick.com/developer/upcoming-events
    /// 
    /// alternative is use songkick (info at link)
    /// </summary>
    /// <param name="a"></param>
    public void SetEventsOfArtist(string artistName)
    {
        return;

        
    }
    #endregion

    #region Sdílené metody
    string UrlEncode(string artistName)
    {
        //return artistName.Replace(AllStrings.space, "%20");
        return WebUtility.UrlEncode(artistName);
    }

    /// <summary>
    /// Vrátí zda požadavek na last.fm selhal
    /// </summary>
    /// <param name="xd"></param>
    private bool FailedRequest(XmlDocument xd)
    {
        XmlNode ele = xd.SelectSingleNode("/lfm");
        XmlAttribute attr = ele.Attributes["status"];
        if (attr != null)
        {
            if (attr.Value != "ok")
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}