using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TesseractArgs
{
    public List< string> inputFiles;
    public Dictionary<string, string> outputFiles;
    public TessearactLang lang;
    public bool writingOnConsole = false;
}