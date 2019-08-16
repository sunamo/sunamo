﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sunamo.Essential;

public class TestHelper
{
    public static string DefaultFolderPath()
    {
        string appName = ThisApp.Name;
        string project = ThisApp.Project;

        string folderFrom = @"d:\_Test\" + appName + "\\" + project;
        return folderFrom;
    }

    /// <summary>
    /// A1 can be null, then will be joined default like d:\_Test\AllProjectsSearch\AllProjectsSearch\
    /// A2 can be slashed or backslashed
    /// To A2 will be add _Original automatically
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="featureOrType"></param>
    public static void RefreshOriginalFiles(string baseFolder, object featureOrType, string modeOfFeature, bool deleteRecursively, bool replace_Original)
    {
        if (baseFolder == null)
        {
            baseFolder = DefaultFolderPath();
        }

        string feature = NameOfFeature(featureOrType);

        FS.WithoutEndSlash(ref baseFolder);
        baseFolder = baseFolder + "\\" + feature;
        var folderFrom =baseFolder + "_Original\\";
        string folder = baseFolder + "\\";

        if (!string.IsNullOrEmpty(modeOfFeature))
        {
            modeOfFeature = modeOfFeature.TrimEnd('\\') + "\\";
            folderFrom += modeOfFeature;
            folder += modeOfFeature;
        }

        FS.GetFiles(folder, deleteRecursively).ToList().ForEach(d => FS.DeleteFileIfExists(d));
        if (deleteRecursively)
        {
            FS.CopyAllFilesRecursively(folderFrom, folder, FileMoveCollisionOption.Overwrite);
        }
        else
        {
            FS.CopyAllFiles(folderFrom, folder, FileMoveCollisionOption.Overwrite);
        }
        

        if (replace_Original)
        {
            const string _Original = "_Original";

            var files = FS.GetFiles(folder);
            foreach (var item in files)
            {
                var item2 = item;
                var c = TF.ReadFile(item);
                // replace in content
                c = SH.Replace(c, _Original, string.Empty);
                TF.SaveFile(c, item2);

                if (item2.Contains(_Original))
                {
                    string newFile = item2.Replace(_Original, string.Empty);

                    FS.MoveFile(item2, newFile, FileMoveCollisionOption.Overwrite);
                }
            }
        }
    }

    private static string NameOfFeature(object featureOrType)
    {
        string feature = null;
        if (featureOrType is Type)
        {
            feature = (featureOrType as Type).Name;
        }
        else if (featureOrType is string)
        {
            return featureOrType.ToString();
        }
        else
        {
            feature = featureOrType.GetType().Name;
        }

        return feature;
    }

    /// <summary>
    /// Get backslashed
    /// </summary>
    /// <param name="featureOrType"></param>
    /// <returns></returns>
    public static string FolderForTestFiles(object featureOrType)
    {
        string feature = NameOfFeature(featureOrType);

        string appName = ThisApp.Name;
        string project = ThisApp.Project;

        return @"d:\_Test\" + appName + "\\" + project + SH.WrapWith(feature, AllChars.bs, true);
    }
}

