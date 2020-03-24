using System;
using System.Collections.Generic;
using System.Text;

public partial class SunamoCodeHelper{ 
public static void RemoveTemporaryFilesVS(List<string> files)
    {
        var list = CA.WrapWith(VisualStudioTempFse.foldersInSolutionToDelete, AllStrings.bs);
        // As foldersInProjectToDelete dont have contains WildCard, set false
        CA.RemoveWhichContains(files, list, false);
        list = CA.WrapWith(VisualStudioTempFse.foldersInProjectToDelete, AllStrings.bs);
        CA.RemoveWhichContains(files, list, false);
        list = CA.WrapWith(VisualStudioTempFse.foldersInSolutionDownloaded, AllStrings.bs);
        CA.RemoveWhichContains(files, list, false);
    }
}