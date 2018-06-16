using sunamo;
using sunamo.Data;
using sunamo.Enums;
using System.IO;
public class FileInfoSerie : FileInfoLite
{
    
    public bool IsDeleted = false;
    public string NameWithoutSeries = null;
    //public string NameWithoutSeriesLower = null;
    public string PathWithoutSerie = null;
    public bool HasSerie = false;
    /// <summary>
    /// Pouze název souboru s příponou bez cesty, za to modifikovaný
    /// </summary>
    public string FileNameComplet = null;
    public bool NeverRemove = false;

    public static FileInfoSerie GetFIS(string file)
    {
        FileInfo item2 = new FileInfo(file);
        return GetFIS(item2);
    }

    public static FileInfoSerie GetFIS(FileInfo item2)
    {
        FileInfoSerie fil = new FileInfoSerie();
        fil.Name = item2.Name;
        fil.Path = item2.FullName;
        fil.Size = item2.Length;
        bool hasSerie = false;
        fil.NameWithoutSeries = sunamo.FS.GetNameWithoutSeries(fil.Name, false, out hasSerie, SerieStyle.Brackets);
        fil.PathWithoutSerie = sunamo.FS.GetNameWithoutSeries(fil.Name, true, out hasSerie, SerieStyle.Brackets);
        fil.HasSerie = hasSerie;
        fil.FileNameComplet = sunamo.FS.GetFileName(item2.FullName);
        return fil;
    }


}
