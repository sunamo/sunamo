using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAppDataBase<StorageFolder, StorageFile>
{
    string GetFileCommonSettings(string key);
    string RootFolderCommon(bool inFolderCommon);
}