using System;
using System.Collections.Generic;
using System.Text;

public partial class SunamoCodeHelper{ 
public static void RemoveTemporaryFilesVS(List<string> files)
    {
        var list = VisualStudioTempFseWrapped.foldersInSolutionToDelete;
        // As foldersInProjectToDelete dont have contains WildCard, set false
        CA.RemoveWhichContains(files, list, false);
        list = VisualStudioTempFseWrapped.foldersInProjectToDelete;
        CA.RemoveWhichContains(files, list, false);
        list = VisualStudioTempFseWrapped.foldersInSolutionDownloaded;
        CA.RemoveWhichContains(files, list, false);
    }
}