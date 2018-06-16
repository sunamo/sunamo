using sunamo;
using sunamo.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;

public abstract class YouTubeThumbnail
{
    /// <summary>
    /// Vrátí s počátečním a koncovým apostrofem
    /// </summary>
    /// <param name="idSong"></param>
    /// <param name="poradi"></param>
    /// <returns></returns>
    public string GetUri(int idSong, int poradi)
    {
        //return UH.GetWebUri(p, "temp/" + idVideo + "/"+poradi+".jpg")
        return "'" + GetBaseUri(idSong, poradi) + "'";
    }

    /// <summary>
    /// Vrátí bez počátečního a koncového apostrofu
    /// </summary>
    /// <param name="idSong"></param>
    /// <param name="poradi"></param>
    /// <returns></returns>
    public string GetUri2(int idSong, int poradi)
    {
        //return UH.GetWebUri(p, "temp/" + idVideo + "/"+poradi+".jpg")
        return GetBaseUri(idSong, poradi);
    }

    public bool HasAnyFile(int idSong)
    {
        if (File.Exists(GetPath(idSong, 1)))
        {
            return true;
        }
        if (File.Exists(GetPath(idSong, 2)))
        {
            return true;
        }
        if (File.Exists(GetPath(idSong, 3)))
        {
            return true;
        }
        return false;
    }
    public List<string> AllRelativeFiles(short idSong)
    {
        List<string> vr = new List<string>(3);
        string path = GetPath(idSong, 1);
        if (File.Exists(path))
        {
            vr.Add(GetBaseUri(idSong, 1));
        }
        path = GetPath(idSong, 2);
        if (File.Exists(path))
        {
            vr.Add(GetBaseUri(idSong, 2));
        }
        path = GetPath(idSong, 3);
        if (File.Exists(path))
        {
            vr.Add(GetBaseUri(idSong, 3));
        }
        return vr;
    }
    public List<string> AllFiles(int idSong)
    {
        List<string> vr = new List<string>(3);
        string path = GetPath(idSong, 1);
        if (File.Exists(path))
        {
            vr.Add(path);
        }
        path = GetPath(idSong, 2);
        if (File.Exists(path))
        {
            vr.Add(path);
        }
        path = GetPath(idSong, 3);
        if (File.Exists(path))
        {
            vr.Add(path);
        }
        return vr;
    }

    public string AllFilesRelativeUriJavascriptArray(string nameArray, int idSong)
    {
        string uriImage1 = "";
        return AllFilesRelativeUriJavascriptArray(nameArray, idSong, out uriImage1);
    }

    /// <summary>
    /// Ke výstupu A3 této metody je třeba na začátek přidat M.HttpWwwCzSlash
    /// </summary>
    /// <param name="nameArray"></param>
    /// <param name="idSong"></param>
    /// <param name="uriImage1"></param>
    /// <returns></returns>
    public string AllFilesRelativeUriJavascriptArray(string nameArray, int idSong, out string uriImage1)
    {
        StringBuilder vr = new StringBuilder();
        vr.Append(nameArray + "[" + idSong + "] = [");
        string path = GetPath(idSong, 1);

        if (File.Exists(path))
        {
            uriImage1 = GetUri2(idSong, 1);
            vr.Append("'" + uriImage1 + "',");
        }
        else
        {
            uriImage1 = "";
            return "";
        }
        path = GetPath(idSong, 2);
        if (File.Exists(path))
        {
            vr.Append(GetUri(idSong, 2) + ",");

        }
        else
        {
            return "";
        }
        path = GetPath(idSong, 3);
        if (File.Exists(path))
        {
            vr.Append(GetUri(idSong, 3) + ",");
        }
        else
        {
            return "";
        }
        string r = vr.ToString();
        int l = r.Length - 1;
        if (r[l] == ',')
        {
            r = r.Substring(0, l);
        }
        r += "];";
        return r;
    }

    protected abstract string GetPath(int idSong, int poradi);
    public abstract string GetBaseUri(int idSong, int poradi);

    public void Save(int idSong, string ytCode)
    {
        Stream s1 = HttpRequestHelper.GetResponseStream(UriOfThumbnail(ytCode, 1), HttpMethod.Get);
        Stream s2 = HttpRequestHelper.GetResponseStream(UriOfThumbnail(ytCode, 2), HttpMethod.Get);
        Stream s3 = HttpRequestHelper.GetResponseStream(UriOfThumbnail(ytCode, 3), HttpMethod.Get);
        FS.CreateUpfoldersPsysicallyUnlessThere(GetPath(idSong, 1));
        TrySave(idSong, s1, 1);
        TrySave(idSong, s2, 2);
        TrySave(idSong, s3, 3);
    }

    private void TrySave(int idSong, Stream s1, int poradi)
    {
        if (s1 != null)
        {
            Image i = Image.FromStream(s1);
            string c = GetPath(idSong, poradi);
            try
            {
                i.Save(c, ImageFormat.Jpeg);
            }
            catch (System.Exception)
            {
            }
        }
    }

    string UriOfThumbnail(string ytCode, int poradi)
    {
        return "http://img.youtube.com/vi/" + ytCode + "/" + poradi + ".jpg";
    }

    public void Save(int idSong, int poradi, Image toSave)
    {
        string path = GetPath(idSong, poradi);
        bool smazano = false;
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                smazano = true;
            }
            catch (System.Exception)
            {
            }
        }
        if (smazano)
        {
            toSave.Save(path, ImageFormat.Jpeg);
        }
    }
}
