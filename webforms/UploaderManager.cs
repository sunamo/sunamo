
using shared.Extensions;
using sunamo;
using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

public delegate int InsertToTablePhotos(int idAlbum, string photoName); 

public class UploaderManager
{
    int idAlbum = int.MaxValue;
    //MySitesShort mss = MySitesShort.Nope;
    public string pathAlbumFinalValue = null;
    public string pathAlbumFinalMinValue = null;
    public string pathAlbumFinal
    {
        get
        {
            return pathAlbumFinalValue;
        }
        set
        {
            pathAlbumFinalValue = value;
            if (value != "")
            {
                FS.CreateFoldersPsysicallyUnlessThere(pathAlbumFinalValue);    
            }
            
        }
    }
    public string pathAlbumFinalMin
    {
        get
        {
            return pathAlbumFinalMinValue;
        }
        set
        {
            pathAlbumFinalMinValue = value;
            if (value != "")
            {
                FS.CreateFoldersPsysicallyUnlessThere(pathAlbumFinalMinValue);
            }
        }
    }
    int idUser = -1;
    string[] exts = null;
    string toFolderTempSlash = null;
    Color borderColor = Color.AliceBlue;
    bool workWithDatabase = false;
    InsertToTablePhotos insertToTablePhotosDelegate = null;
    bool fileNameIsIDInTableRow = false;
    bool stopIfFinalOrFinalMinExists = true;
    string code = null;

    /// <summary>
    /// A3 i A4 i A5 jsou již cesty převedené pomocí MapPath
    /// A3 i A4 i A5 musí končit na \
    /// A2 musí být přípony oddělené , s tečkou VELKÝMI PÍSMENY
    /// ---A2 nastav na Nope, když chceš užít A1. Jinak se ID alba zjistí metodou LastUploadAlbumHelper.GetAlbum
    /// 
    /// </summary>
    /// <param name="allowedExtension"></param>
    /// <param name="pathAlbumFinal"></param>
    /// <param name="pathAlbumFinalMin"></param>
    /// <param name="slozkaForMapPathTempSlash"></param>
    /// <param name="borderColor"></param>
    /// <param name="workWithDatabase"></param>
    /// <param name="insertToTablePhotosDelegate"></param>
    public UploaderManager(int idAlbum, /* MySitesShort mss, */ string allowedExtension, string pathAlbumFinal, string pathAlbumFinalMin, string pathAlbumFinalMinOpt, int idUser, Color borderColor, bool workWithDatabase, InsertToTablePhotos insertToTablePhotosDelegate, bool fileNameIsIDInTableRow, bool stopIfFinalOrFinalMinExists, int convertToMaxWidth, string code, bool tnOptimal)
    {
        //string toFolderTempSlash,
        this.convertToMaxWidth = convertToMaxWidth;
        toFolderTempSlash = GeneralHelper.GetRandomGuidFolderInRawUploads(idUser, "PhotosTemp", false);
        this.idAlbum = idAlbum;
        //this.mss = mss;
        exts = SH.Split(allowedExtension, ',');
        this.pathAlbumFinal = pathAlbumFinal.TrimEnd('\\') + "\\";
        this.pathAlbumFinalMin = pathAlbumFinalMin.TrimEnd('\\') + "\\";
        this.pathAlbumFinalMinOpt = pathAlbumFinalMinOpt.TrimEnd('\\') + "\\";
        this.idUser = idUser;
        this.borderColor = borderColor;
        this.workWithDatabase = workWithDatabase;
        this.insertToTablePhotosDelegate = insertToTablePhotosDelegate;
        this.fileNameIsIDInTableRow = fileNameIsIDInTableRow;
        this.stopIfFinalOrFinalMinExists = stopIfFinalOrFinalMinExists;
        this.code = code;
        this.tnOptimal = tnOptimal;
    }

    bool tnOptimal = false;
    string pathAlbumFinalMinOptValue = "";

    public string pathAlbumFinalMinOpt
    {
        get
        {
            return pathAlbumFinalMinOptValue;
        }
        set
        {
            pathAlbumFinalMinOptValue = value;
            if (value != "")
            {
                FS.CreateFoldersPsysicallyUnlessThere(pathAlbumFinalMinOptValue);
            }
        }
    }

    public string finalMinOpt = "";

    public string ProcessFiles(IList<SunamoHttpPostedFile> files, out bool necoNauploadovano, out bool byloJizNeco, out List<string> vr)
    {
        
        vr = new List<string>();
         necoNauploadovano = false;
         byloJizNeco = Directory.GetFiles(pathAlbumFinalMin).Length != 0;
         bool nahrazenySoubory = false;
         HtmlGenerator hg = new HtmlGenerator();
         TryParse.Integer tp = new TryParse.Integer();
        foreach (var e in files)
        {
            
            bool b = false;
            string s = ProcessFile(e, out b, out nahrazenySoubory);
            
            if (tp.TryParseInt(s))
            {
                vr.Add(s);
                hg.WriteRaw("Soubor " + e.FileName + " úspěšně nauploadován");
            }
            else
            {
                vr.Add(null);
                hg.WriteRaw(s);
            }
            hg.WriteBr();
            if (b)
            {
                necoNauploadovano = true;
            }
        }
        return hg.ToString();
    }

    string fn;
    string ext;
    public int originalSize = 0;
    bool allOk = true;
    string to = null;
    public string CheckCondition(IList<SunamoHttpPostedFile> e2)
    {
        HtmlGenerator hg = new HtmlGenerator();
        foreach (SunamoHttpPostedFile item in e2)
        {
            //new SunamoHttpPostedFile(item.ContentLength, item.ContentType, item.FileName, item.InputStream)
            hg.WriteRaw( CheckCondition(item, false, 1,1,false, 1,1));
            hg.WriteBr();
        }
        if (allOk)
        {
            return "1";
        }
        return hg.ToString() ;
    }

    public string CheckCondition(SunamoHttpPostedFile e, bool controlCount, int maxFilesCountOnAccount, int countOfAllPhotosOfUser, bool controlSize, int maxFilesSizeOnAccount, int sizeOfAllPhotosOfUser)
    {
        //if ( + 1 > PhotosConst.maxFileCountOnAccount)
        if (controlCount)
        {
            if (idUser != 1)
            {
                if (countOfAllPhotosOfUser + 1 > maxFilesCountOnAccount)
                {
                    if (allOk)
                    {
                        allOk = false;
                    }
                    return "Maximální počet obrázků na 1 uživatele je " + maxFilesCountOnAccount + ". Vy jste tohoto limitu již dosáhli a soubor " + e.FileName + " nebude nauploadován";
                }
            }
        }
        

        if (!BeforeSaveToTempFolder(e.FileName, out fn, out ext))
        {
            if (allOk)
            {
                allOk = false;
            }
            return "Soubor " + e.FileName + " nemá správnou příponu.";
        }

        to = toFolderTempSlash + fn;
        
        FS.CreateUpfoldersPsysicallyUnlessThere(to);
        //string final = Path.Combine(to, fn);
        if (File.Exists(to))
        {
            try
            {
                File.Delete(to);
            }
            catch (Exception)
            {
                if (allOk)
                {
                    allOk = false;
                }
                return "Soubor " + to + " již existoval a nepodařilo se jej smazat.";
            }
        }
        e.SaveAs(to);
        try
        {
            bmp = new Bitmap(to);
            bmp.Save(FS.InsertBetweenFileNameAndExtension(to, "_2"));
        }
        catch (Exception ex)
        {
            return "error: Z nauploadovaného souboru " + Path.GetFileName(to) + " se nepodařilo vytvořit obrázek, soubor nebude nauploadován";
        }



        if (!bmp.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
        {
            return "error: Obrázek nebyl ve formátu JPEG.";
        }
        // || bmp.Height > bmp.Width
        if (e.FileName.StartsWith("DSC_") )
        {
            bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
        }
        originalSize = (int)FS.GetFileSize(to);
        if (controlSize)
        {
            string toMiddle = FS.InsertBetweenFileNameAndExtension(to, "_middle");
            if (File.Exists(toMiddle))
            {
                try
                {
                    File.Delete(toMiddle);
                }
                catch (Exception)
                {
                    if (allOk)
                    {
                        allOk = false;
                    }
                    return "Soubor " + toMiddle + " již existoval a nepodařilo se jej smazat.";
                }
            }


                
                Size s = Pictures.CalculateOptimalSize(bmp.Width, bmp.Height, convertToMaxWidth).ToSystemDrawing();
            shared.Pictures.TransformImage(bmp, s.Width, s.Height, toMiddle);
                if (idUser != 1)
                {
                    long size = FS.GetFileSize(toMiddle);
                    //int len = PhotosHelper.GetSizeOccupatedByAllAlbumOfUser(idUser);
                    int len = sizeOfAllPhotosOfUser;
                    //if (len + size > GeneralConsts.HalfGb)
                    if (len + size > maxFilesSizeOnAccount)
                    {
                        if (allOk)
                        {
                            allOk = false;
                        }
                        return "Maximální velikost fotek ve fotogalerii a všech albech je 1GB. Soubor " + e.FileName + " nebude nauploadován.";
                    }
                }
        }
        bmp.Dispose();
        return "1";
    }

    /// <summary>
    /// Vrátí mi ID obrázku pod kterým existoval v DB nebo textovou chybu.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="necoNauploadovano"></param>
    /// <returns></returns>
    public string ProcessFile(SunamoHttpPostedFile e, out bool necoNauploadovano, out bool nahrazenySoubory)
    {
        necoNauploadovano = false;
        nahrazenySoubory = false;
        if (!BeforeSaveToTempFolder(e.FileName, out fn, out ext))
        {
            if (allOk)
            {
                allOk = false;
            }
            return "Soubor " + e.FileName + " nemá správnou příponu.";
        }
        to = toFolderTempSlash + fn;

        bool necoNauploadovano2 = false;
        bool nahrazenSoubor2 = false;
        var vv = AfterSaveToTempFolder(out necoNauploadovano2, out nahrazenSoubor2, fn,  ext, to);
        necoNauploadovano = necoNauploadovano2;
        nahrazenySoubory = nahrazenSoubor2;
        return vv;
    }

    /// <summary>
    /// Vrátí mi do A2 název souboru připravený na uložení do filesystému bez cesty 
    /// Také zkontroluje zda soubor má správnou příponu a pokud ne, G false
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fn"></param>
    /// <param name="ext"></param>
    /// <returns></returns>
    private bool BeforeSaveToTempFolder(string fileName, out string fn, out string ext)
    {
        //necoNauploadovano = false;
        fn = Path.GetFileName(fileName);
        
        ext = Path.GetExtension(fileName).ToUpper();
        fn = fn.Substring(0, fn.Length - ext.Length) + ext;
        bool preskocNaDalsi = true;
        if (exts.Length == 0)
        {
            preskocNaDalsi = false;
        }
        for (int i = 0; i < exts.Length; i++)
        {
            if (ext == exts[i])
            {
                preskocNaDalsi = false;
                break;
            }
        }
        if (preskocNaDalsi)
        {
            return false;
        }

        #region Nahrazování špatných znaků
        if (fileName.Contains("__"))
        {
            fn = fn.Replace("__", "");
        }
        if (fileName.Contains("|"))
        {
            fn = fn.Replace("|", "");
        }
        fn = FS.DeleteWrongCharsInFileName(fn, false);
        fn = FS.DeleteWrongCharsInDirectoryName(fn);

        #endregion
        return true;
    }

    /// <summary>
    /// Je to veřejné, abych pak mohl zjistit například velikost nebo příponu(jpeg/jpg)
    /// </summary>
    public string final = "";
    public string finalMin = "";
    int convertToMaxWidth = 600;
    System.Drawing.Image bmp = null;
    /// <summary>
    /// Pokud workWithDatabase, vrátí mi ID pod kterým uložila obrázek do DB, jinak int.MaxValue.
    /// 
    /// </summary>
    /// <param name="necoNauploadovano"></param>
    /// <param name="nahrazenySoubory"></param>
    /// <param name="fn"></param>
    /// <param name="ext"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private string AfterSaveToTempFolder(out bool necoNauploadovano, out bool nahrazenySoubory, string fn,  string ext, string to)
    {
        nahrazenySoubory = false;
        necoNauploadovano = false;
        //ext = ext.ToUpper();
        string fnwoe = Path.GetFileNameWithoutExtension(fn);

        int idPhoto = int.MaxValue;
        if (workWithDatabase && fileNameIsIDInTableRow)
        {
            if (insertToTablePhotosDelegate != null)
            {
                idPhoto = insertToTablePhotosDelegate(idAlbum, Path.GetFileNameWithoutExtension(fn));
                fn = idPhoto + ".JPG";
            }
            else
            {
                throw new Exception("Pracujete s databází ale nebylo vyplněné insertToTablePhotosDelegate");
            }
        }
        if (!workWithDatabase && fileNameIsIDInTableRow)
        {
            throw new Exception("Chcete použít pro jméno souboru ID z řádku tabulky ale máte !workWithDatabase");
        }
        
        string pridat = "";
        if (!string.IsNullOrEmpty(code))
        {
            pridat = code + "\\";
        }
        final = pathAlbumFinal + pridat + fn;
        finalMin = pathAlbumFinalMin  + pridat + fn;
        finalMin = FS.InsertBetweenFileNameAndExtension(finalMin, "_tn");
        finalMinOpt = pathAlbumFinalMinOpt + pridat + fn;
        finalMinOpt = FS.InsertBetweenFileNameAndExtension(finalMinOpt, "_to");
        if (stopIfFinalOrFinalMinExists)
        {
            if (File.Exists(final))
            {
                return "Upload souboru byl zastaven, neboť soubor " + final + " již existoval";
            }
            if (File.Exists(finalMin))
            {
                return "Upload souboru byl zastaven, neboť soubor " + finalMin + " již existoval";
            }
            if (File.Exists(finalMinOpt))
            {
                return "Upload souboru byl zastaven, neboť soubor " + finalMinOpt + " již existoval";
            }
        }
        else
        {
            if (File.Exists(final) || File.Exists(finalMin) || File.Exists(finalMinOpt))
            {
                nahrazenySoubory = true;
            }
        }
        FS.CreateUpfoldersPsysicallyUnlessThere(final);
        FS.CreateUpfoldersPsysicallyUnlessThere(finalMin);
        FS.CreateUpfoldersPsysicallyUnlessThere(finalMinOpt);

        bmp = new Bitmap(to);
        if (bmp.Width > convertToMaxWidth)
        {
            Size s = Pictures.CalculateOptimalSize(bmp.Width, bmp.Height, convertToMaxWidth).ToSystemDrawing();
            shared.Pictures.TransformImage(bmp, s.Width, s.Height, final);
            bmp = (Image)System.Drawing.Image.FromFile(final);//.Clone();
        }
        else
        {
            bmp.Save(final, ImageFormat.Jpeg);
        }

        if (tnOptimal)
        {
            GeneralHelper.CreateThumbnailOptimal(toFolderTempSlash, fnwoe, ext, finalMinOpt, bmp);
        }
        GeneralHelper.CreateThumbnail(toFolderTempSlash, fnwoe, ext, finalMin, bmp, borderColor);
        
        bmp.Dispose();

        necoNauploadovano = true;
        if (workWithDatabase && !fileNameIsIDInTableRow)
        {
            if (insertToTablePhotosDelegate!= null)
            {
                return insertToTablePhotosDelegate(idAlbum, Path.GetFileNameWithoutExtension( fn)).ToString();
            }
        }

        
        return idPhoto.ToString();
    }

    

    

    public string ProcessFiles(HttpFileCollection httpFileCollection, out bool necoNauploadovane, out bool byloJizNeco, out List<string> idPhotos)
    {
        
        return ProcessFiles(HttpHelper.ListOfHttpPostedFile(httpFileCollection), out necoNauploadovane, out byloJizNeco, out idPhotos);
    }

    public string CheckCondition(HttpFileCollection httpFileCollection)
    {
        
        return CheckCondition(HttpHelper.ListOfHttpPostedFile(httpFileCollection));
    }
}
