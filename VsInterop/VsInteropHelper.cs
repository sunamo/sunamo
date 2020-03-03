using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

/// <summary>
/// EnvDTE is addins - https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2013/dn246938%28v%3dvs.120%29
/// addins is obsolete in VS13, VSPackage is supported
/// You need to install the Visual Studio 2013 SDK in addition to the Professional, Premium, or Ultimate edition of Visual Studio
/// https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2013/dn246938%28v%3dvs.120%29
/// 
/// </summary>
public class VsInteropHelper
    {
    public static void FormatCsharp8(string filename)
    {
        // DTE.8.0 = VS 05
        // error because I dont have installed VS 05
        var dte = (EnvDTE80.DTE2)Marshal.GetActiveObject("VisualStudio.DTE.8.0");
        dte.ExecuteCommand("File.OpenFile", filename);
        dte.ExecuteCommand("Edit.FormatDocument", filename);
        dte.ActiveDocument.Close(vsSaveChanges.vsSaveChangesYes);
    }

    public static string FormatCsharp(string filename)
    {
        // VS 2010
        //var dte = (EnvDTE100.DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0");
        //dte.ExecuteCommand("File.OpenFile", filename);
        //dte.ExecuteCommand("Edit.FormatDocument", filename);
        //dte.ActiveDocument.Close(vsSaveChanges.vsSaveChangesYes
        return null;
    }
}