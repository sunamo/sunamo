
using sunamo.Generators.Text;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using win;
// if app isnt STA, raise exception
//using System.Windows;
// if app isnt STA, return empty. 
using sunamo.Clipboard;
using System.Collections;
using System.Windows;
//using System.Windows;

/// <summary>
/// Must be in win due to ClipboardMonitor which uses W32 methods
/// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
/// Use in ClipboardAsync and ClipboardHelperWin only System.Windows.Forms, not System.Windows which have very similar interface.
/// </summary>
public class ClipboardHelperWin : IClipboardHelper
{
    public const uint CF_UNICODETEXT = 13U;
    public const uint CF_DSPTEXT = 0x0081;
    public const uint CF_LOCALE = 16U;
    public const uint CF_OEMTEXT = 7U;
    public const uint CF_TEXT = 1U;

    IClipboardMonitor clipboardMonitor = null;
    public static ClipboardHelperWin Instance = new ClipboardHelperWin();

    private ClipboardHelperWin()
    {
        clipboardMonitor = ClipboardMonitor.Instance;
    }
     
    #region Get,Set
    /// <summary>
    /// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
    /// not working if was pasted into visual studio (but code yes), created SetText2
    /// </summary>
    /// <param name="v"></param>
    public void SetText(string v)
    {
        if (clipboardMonitor != null)
        {
            clipboardMonitor.afterSet = null;
        }

        SetText2(v);
    }

    /// <summary>
    /// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
    /// </summary>
    public string GetText()
    {
        #region Nepoužívat, 1) celá třída vypadá jak by ji psal totální amatér. 2) havaruje mi to app a nevyhodi pritom zadnou UnhaldedException
        //ClipboardAsync ca = new ClipboardAsync();
        //string s = ca.GetText();
        //return s;
        #endregion

        string result = "";
        //result = GetTextW32();
        try
        {
            result = Clipboard.GetText();
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public void SetText2(string v)
    {
        if (!string.IsNullOrWhiteSpace(v))
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Clipboard.SetText(v);
                    return;
                }
                catch { }
                System.Threading.Thread.Sleep(10);
            }
        }
    }

    public List<string> GetLines()
    {
        var text = GetText();
        return SH.GetLines(text);
    }
    #endregion

    public void GetFirstWordOfList()
    {
        Console.WriteLine(sess.i18n(XlfKeys.CopyTextToClipboard) + ".");
        Console.ReadLine();

        StringBuilder sb = new StringBuilder();

        var text = GetLines();
        foreach (var item in text)
        {
            string t = item.Trim();

            if (t.EndsWith(AllStrings.colon))
            {
                sb.AppendLine(item);
            }
            else if (t == "")
            {
                sb.AppendLine(t);
            }
            else
            {
                sb.AppendLine(SH.GetFirstWord(t));
            }
        }

        SetText(sb.ToString());
    }

    public void SetList(List<string> d)
    {
        SetLines(d);
    }

    public void SetLines(IEnumerable lines)
    {
        string s = SH.JoinNL(lines);
        SetText(s);
    }

    public void CutFiles(params string[] selected)
    {
        byte[] moveEffect = { 2, 0, 0, 0 };
        MemoryStream dropEffect = new MemoryStream();
        dropEffect.Write(moveEffect, 0, moveEffect.Length);

        StringCollection filestToCut = new StringCollection();
        filestToCut.AddRange(selected);

        DataObject data = new DataObject(sess.i18n(XlfKeys.PreferredDropEffect), dropEffect);
        data.SetFileDropList(filestToCut);

        Clipboard.Clear();
        Clipboard.SetDataObject(data, true);
    }


    public void SetText(StringBuilder stringBuilder)
    {
        SetText(stringBuilder.ToString());
    }

    public void SetText3(string s)
    {
        if (clipboardMonitor != null)
        {
            clipboardMonitor.afterSet = null;
        }

        Thread thread = new Thread(() => SetText2(s));
        thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
        thread.Start();
        thread.Join();
    }



    public bool ContainsText()
    {
        return Clipboard.ContainsText();
    }
}