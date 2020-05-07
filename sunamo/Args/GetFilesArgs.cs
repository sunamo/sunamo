using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GetFilesArgs
{
    public bool _trimA1 = false;
    public List<string> excludeFromLocationsCOntains = null;
    public bool dontIncludeNewest = false;
    /// <summary>
    /// Insert SunamoCodeHelper.RemoveTemporaryFilesVS etc.
    /// </summary>
    public Action<List<string>> excludeWithMethod = null;
    public bool byDateOfLastModifiedAsc = false;
}