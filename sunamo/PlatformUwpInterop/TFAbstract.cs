using System;
using System.Collections.Generic;
using System.Text;


public class TFAbstract<StorageFile>
{
    public Action<StorageFile, string> writeAllText = null;
    public Action<StorageFile, List<byte>> writeAllBytes = null;
    public Func<StorageFile, string> readAllText;
}