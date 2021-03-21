using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReplaceInAllFilesArgs
{
    public string from; 
    public string to; 
    public List<string> files; 
    public bool pairLinesInFromAndTo; 
    public bool replaceWithEmpty;
    public bool isMultilineWithVariousIndent;
    public bool writeEveryReadedFileAsStatus;
    /// <summary>
    /// 
    /// </summary>
    public Func<StringBuilder, IList<string>, IList<string>, StringBuilder> fasterMethodForReplacing;
    public bool isNotReplaceInTemporaryFiles;
}