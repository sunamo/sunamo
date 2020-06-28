using System;
using System.Collections.Generic;
using System.Text;


public class StringPaddingData
{
    /// <summary>
    /// [0]
    /// </summary>
    public bool first = false;
    /// <summary>
    /// [Length-1] (really last / poslední)
    /// </summary>
    public bool last = false;
    /// <summary>
    /// [0]
    /// </summary>
    public char firstChar = 'a';
    /// <summary>
    /// [Length-1] (really last / poslední)
    /// </summary>
    public char lastChar = 'a';

    #region for cases like "xxx: "
    /// <summary>
    /// [1]
    /// </summary>
    public bool first2 = false;
    /// <summary>
    /// [Length-2] (penultimate / předposlední) 
    /// </summary>
    public bool last2 = false;
    /// <summary>
    /// [1]
    /// </summary>
    public char firstChar2 = 'a';
    /// <summary>
    /// [Length-2] (penultimate / předposlední) 
    /// </summary>
    public char lastChar2 = 'a';
    #endregion

    public string text = null;
}