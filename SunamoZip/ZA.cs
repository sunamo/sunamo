using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Core;
using sunamo;

/// <summary>
/// M�lo by tu v�echno fungovat, str�vil jsem nad t�m hezk�ch p�r hodin :(
/// </summary>
public class ZA
{
    #region IArchiv Members
    public static ZA zip = new ZA();

    /// <summary>
    /// Kdy� komprimuji celou slo�ku A1, vr�t� mi arch�v zip jm�na slo�ky A1 ve slo�ce ve kter� je A1.
    /// </summary>
    /// <param name="slozku"></param>
    /// <returns></returns>
    private string VratJmenoSouboruZip(string slozku)
    {
        string soubor = Path.Combine(FS.GetDirectoryName(slozku), FS.GetFileNameWithoutExtensionLower(slozku) + ".zip");
        return soubor;
    }
    
    private string getRelativePath2(string filePath, string basePath)
    {
        if (!basePath.EndsWith("\\"))
        {
            basePath += "\\";
        }
        return filePath.Replace(basePath, "");
    }

    /// <summary>
    /// Ulozi soubory A2 do A3. 
    /// A1 je k tomu, aby se mohla zjistit relativni cesta. 
    /// </summary>
    /// <param name="slozku"></param>
    /// <param name="soubory"></param>
    /// <param name="soubor"></param>
    public void CreateArchive(string slozku, string[] soubory, string soubor)
    {
        #region Nac. jedn soubory a ukkladam do ZipOutputStream
        using (ZipOutputStream s = new ZipOutputStream(File.Create(soubor)))
        {
            s.SetLevel(9);
            for (int i = 0; i < soubory.Length; i++)
            {
                string var = getRelativePath2(soubory[i], slozku);
                ZipEntry ze = new ZipEntry(var + Path.DirectorySeparatorChar.ToString());

                s.PutNextEntry(ze);

                try
                {
                    FileStream fs = new FileStream(Path.Combine(slozku, var), FileMode.Open);
                    byte[] fero = new byte[fs.Length];
                    fs.Read(fero, 0, (int)fs.Length);
                    fs.Flush();
                    fs.Close();

                    fs.Dispose();
                    fs = null;
                    List<byte> b = new List<byte>(fero);

                    foreach (byte var2 in b)
                    {
                        s.WriteByte(var2);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                }
            }
            s.Flush();
            s.Finish();
        }
        #endregion
    }

    public void ExtractArchive(string archiveFilenameIn, string outFolder)
    {
        if (!Directory.Exists(outFolder))
        {
            FS.CreateDirectory(outFolder);
        }
        ZipFile zf = null;
        try
        {
            FileStream fs = File.OpenRead(archiveFilenameIn);
            zf = new ZipFile(fs);
            foreach (ZipEntry zipEntry in zf)
            {
                String entryFileName = zipEntry.Name;
                byte[] buffer = new byte[4096];		// 4K is optimum
                Stream zipStream = zf.GetInputStream(zipEntry);

                // Manipulate the output filename here as desired.
                String fullZipToPath = Path.Combine(outFolder, entryFileName).TrimEnd('\\');
                string directoryName = FS.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                    FS.CreateDirectory(directoryName);

                using (FileStream streamWriter = File.Create(fullZipToPath))
                {
                    StreamUtils.Copy(zipStream, streamWriter, buffer);
                }
            }
        }
        finally
        {
            if (zf != null)
            {
                zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                zf.Close(); // Ensure we release resources
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="slozku"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    private string OdstranZJmenaSouboru(string slozku, string p)
    {
        string c = FS.GetDirectoryName(slozku);
        string s = FS.GetFileName(slozku).Replace(p, "");
        return Path.Combine(c, s);
    }

    
    #endregion

    public void CreateArchive(string slozku)
    {
        CreateArchive(slozku, VratSouboryRek(slozku), VratJmenoSouboruZip(slozku));
    }

    private string[] VratSouboryRek(string slozku)
    {
        return Directory.GetFiles(slozku, "*", SearchOption.AllDirectories);
    }

    public void CreateArchive(string slozka, string[] soubory)
    {
        CreateArchive(slozka, soubory, VratJmenoSouboruZip(slozka));
    }

    public void CreateArchive(string slozku, string souborZip)
    {
        CreateArchive(slozku, VratSouboryRek(slozku), souborZip);
    }

    public void ExtractArchive(string soubor)
    {
        ExtractArchive(soubor, OdstranZJmenaSouboru(soubor, ".zip"));
    }
}

