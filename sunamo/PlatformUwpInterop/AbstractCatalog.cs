using System;
using System.Collections.Generic;
using System.Text;


public class AbstractCatalog<StorageFolder, StorageFile>
{
    public AppDataBase<StorageFolder, StorageFile> appData;
    public FSAbstract<StorageFolder, StorageFile> fs = null;
    
    public TFAbstract<StorageFile> tf;
}

