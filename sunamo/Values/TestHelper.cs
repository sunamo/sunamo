using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sunamo.Constants;
using sunamo.Essential;

/// <summary>
/// Right format of paths are:
/// d:\_Test\SunamoCzAdmin\SunamoCzAdmin.Wpf\ConvertMetroCss3To4\
/// d:\_Test\SunamoCzAdmin\SunamoCzAdmin.Wpf\ConvertMetroCss3To4_Original\
/// 
/// 
/// </summary>
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
    /// A1 can be null, then will be joined default like d:\_Test\AllProjectsSearch\AllProjectsSearch\ by DefaultFolderPath()
    /// A2 can be slashed or backslashed. Will be appended to A1.
    /// To A2 will be add _Original automatically
    /// A3 is append after folder and folderFrom (with _Original\)
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

    /// <summary>
    /// A1 can be Type, string or any object, then is as name take name of it's class
    /// </summary>
    /// <param name="featureOrType"></param>
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
    public static string FolderForTestFiles(object featureOrType)
    {
        string feature = NameOfFeature(featureOrType);

        string appName = ThisApp.Name;
        string project = ThisApp.Project;

        return @"d:\_Test\" + appName + "\\" + project + SH.WrapWith(feature, AllChars.bs, true);
    }

    /// <summary>
    /// 
    /// Path will be combined with ThisApp.Name and ThisApp.Project
    /// </summary>
    public static string GetFileInProjectsFolder(string fileRelativeToProjectPath)
    {
        return FS.Combine(DefaultPaths.vsProjects, ThisApp.Name, ThisApp.Project, fileRelativeToProjectPath);
    }
}