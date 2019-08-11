using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sunamo.Essential;

public class TestHelper
{
    public static void RefreshOriginalFiles(object f, object featureOrType)
    {
        string appName = ThisApp.Name;
        string project = ThisApp.Project;

        string folderFrom = @"d:\_Test\" + appName + "\\" + project;
        
    }

    /// <summary>
    /// A2 can be slashed or backslashed
    /// To A2 will be add _Original automatically
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="featureOrType"></param>
    public static void RefreshOriginalFiles(string baseFolder, object featureOrType)
    {
        string feature = NameOfFeature(featureOrType);

        FS.WithoutEndSlash(ref baseFolder);
        baseFolder = baseFolder + "\\" + feature;
        var folderFrom =baseFolder + "_Original\\";
        string folder = baseFolder + "\\";
        FS.GetFiles(folder, true).ToList().ForEach(d => FS.DeleteFileIfExists(d));
        FS.CopyAllFilesRecursively(folderFrom, folder, FileMoveCollisionOption.Overwrite);
    }

    private static string NameOfFeature(object featureOrType)
    {
        string feature = null;
        if (featureOrType is Type)
        {
            feature = (featureOrType as Type).Name;
        }
        else
        {
            feature = featureOrType.ToString();
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

