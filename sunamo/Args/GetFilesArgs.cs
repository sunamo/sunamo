using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GetFilesArgs : GetFilesBaseArgs
{
    public bool _trimExt = false;
    
    public List<string> excludeFromLocationsCOntains = null;
    public bool dontIncludeNewest = false;
    /// <summary>
    /// Insert SunamoCodeHelper.RemoveTemporaryFilesVS etc.
    /// </summary>
    public Action<List<string>> excludeWithMethod = null;
    public bool byDateOfLastModifiedAsc = false;
    public Func<string, DateTime?> LastModifiedFromFn;
    /// <summary>
    /// 1-7-2020 changed to false, stil forget to mention and method is bad 
    /// </summary>
    public bool useMascFromExtension = false;
}