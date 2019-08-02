using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Is allowed to created only in ListBoxResultContextMenuHandler. 
/// In all other location (like APSHelper) must pass through method
/// </summary>
public class SplitStringsData
{
    /// <summary>
    /// Only translateable strings as is filled in GetBetween
    /// </summary>
    public CollectionWithoutDuplicates<string> bet = new CollectionWithoutDuplicates<string>();
    /// <summary>
    /// Replace all call ctor
    /// </summary>
    public CollectionWithoutDuplicates<string> notToTranslate = new CollectionWithoutDuplicates<string>();
    public Dictionary<string, StringPaddingData> v = new Dictionary<string, StringPaddingData>();
}

