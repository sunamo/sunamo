using System;
using System.Collections.Generic;
using System.Text;

public class VisualStudioTempFseWrapped
{
    public static List<string> foldersInSolutionToDelete = null;
    public static List<string> foldersInProjectToDelete = null;
    public static List<string> foldersInSolutionDownloaded = null;
    public static List<string> foldersAnywhereToDelete = null;
    public static List<string> foldersInSolutionToKeep = null;
    public static List<string> foldersInProjectToKeep = null;
    public static List<string> foldersInProjectDownloaded = null;
    public static List<string> filesWeb = null;

    public static List<string> aggregate = null;

    public static bool IsToIndexed(string p)
    {
        if (SH.ContainsAtLeastOne(p, aggregate))
        {
            return false;
        }
        return true;
    }

    static VisualStudioTempFseWrapped()
    {
        #region MyRegion
        foldersInSolutionToDelete = CA.WrapWith(VisualStudioTempFse.foldersInSolutionToDelete, AllStrings.bs);
        foldersInProjectToDelete = CA.WrapWith(VisualStudioTempFse.foldersInProjectToDelete, AllStrings.bs);
        foldersInSolutionDownloaded = CA.WrapWith(VisualStudioTempFse.foldersInSolutionDownloaded, AllStrings.bs);
        #endregion

        #region MyRegion
        foldersAnywhereToDelete = CA.WrapWith(VisualStudioTempFse.foldersAnywhereToDelete, AllStrings.bs);
        foldersInSolutionToKeep = CA.WrapWith(VisualStudioTempFse.foldersInSolutionToKeep, AllStrings.bs);
        foldersInProjectToKeep = CA.WrapWith(VisualStudioTempFse.foldersInProjectToKeep, AllStrings.bs);
        foldersInProjectDownloaded = CA.WrapWith(VisualStudioTempFse.foldersInProjectDownloaded, AllStrings.bs);
        filesWeb = CA.WrapWith(VisualStudioTempFse.filesWeb, AllStrings.bs);
        #endregion

        aggregate = CA.JoinIEnumerable(foldersInSolutionToDelete,
foldersInProjectToDelete,
foldersInSolutionDownloaded,
foldersAnywhereToDelete,
foldersInSolutionToKeep,
foldersInProjectToKeep,
foldersInProjectDownloaded, filesWeb);
    }
}