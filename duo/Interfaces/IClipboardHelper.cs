using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Must be in sunamo, is used in win and apps
/// </summary>
public interface IClipboardHelper : IClipboardHelperBase<string, List<string>, bool>
{
}

public interface IClipboardHelperBase<String, ListString, Bool>
{
    
    String GetText();
    ListString GetLines();
    Bool ContainsText();

    void SetText(string s);
    void SetText2(string s);
    /// <summary>
    /// For working with thread
    /// </summary>
    /// <param name="s"></param>
    void SetText3(string s);

    void GetFirstWordOfList();
    void SetList(List<string> d);
    void SetLines(IEnumerable lines);
    void CutFiles(params string[] selected);

    // Can't be because TextBuilder is in sunamo and have many references
    //void SetText(TextBuilder stringBuilder);
    void SetText(StringBuilder stringBuilder);
}