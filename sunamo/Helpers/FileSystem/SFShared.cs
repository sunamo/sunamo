using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using sunamo.Data;
using System.Collections;
using System.Linq;
using sunamo.Constants;

public static partial class SF
{
    private static SerializeContentArgs s_contentArgs = new SerializeContentArgs();

    public const string replaceForSeparatorString = AllStrings.us;
    public static readonly char replaceForSeparatorChar = AllChars.us;


    public static string separatorString
    {
        get
        {
            return s_contentArgs.separatorString;
        }

        set
        {
            s_contentArgs.separatorString = value;
        }
    }

    /// <summary>
    /// Same as PrepareToSerialization - return without last
    /// If need to combine string and IEnumerable, lets use CA.Join
    /// DateTime is format with DTHelperEn.ToString
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="o"></param>
    
    public static string PrepareToSerialization2(IEnumerable<string> o, string separator = AllStrings.pipe)
    {
        return PrepareToSerializationWorker(o, true, separator);
    }
}