using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public partial class FS
{
    #region Making problem in translate
    /// <summary>
    /// Delete whole folder A1. If fail, only "1" subdir
    /// </summary>
    /// <param name="repairedBlogPostsFolder"></param>
    public static int DeleteSerieDirectoryOrCreateNew(string repairedBlogPostsFolder)
    {
        int resultSerie = 1;
        var folders = FS.GetFolders(repairedBlogPostsFolder);

        bool deleted = true;
        // 0 or 1
        if (folders.Length() < 2)
        {
            try
            {
                Directory.Delete(repairedBlogPostsFolder, true);
            }
            catch (Exception ex)
            {
                deleted = false;
            }
        }

        string withEndFlash = FS.WithEndSlash(repairedBlogPostsFolder);

        if (!deleted)
        {
            // confuse me, dir can exists
            // Here seems to be OK on 8-7-19 (unit test)
            FS.CreateDirectory(withEndFlash + @"1" + "\\");
        }
        else
        {
            // When deleting will be successful, create new dir
            TextOutputGenerator generator = new TextOutputGenerator();
            generator.sb.Append(withEndFlash);
            generator.sb.CanUndo = true;
            for (; resultSerie < int.MaxValue; resultSerie++)
            {
                generator.sb.Append(resultSerie);
                string newDirectory = generator.ToString();
                if (!FS.ExistsDirectory(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                    break;
                }
                generator.Undo();
            }
        }

        return resultSerie;
    }

    public static void CreateFileWithTemplateContent(string folder, string files, string ext, string templateFromContent)
    {
        var lines = SH.GetLines(files);

        foreach (var item in lines)
        {
            var path = FS.Combine(folder, item + ext);
            if (!FS.ExistsFile(path))
            {
                TF.WriteAllText(path, templateFromContent);
            }
        }
    }


    #endregion
}