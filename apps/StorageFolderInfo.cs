using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace apps
{
    public class StorageFolderInfo
    {
        string path = null;
        string name = null;
        List<string> fileNames = null;
        List<string> folderNames = null;

        public StorageFolderInfo(StorageFolder info, IReadOnlyList<StorageFolder> folders, IReadOnlyList<StorageFile> files)
        {
            path =  info.Path;
            name = info.Name;
            folderNames = new List<string>(folders.Count);
            foreach (var item in folders)
            {
                folderNames.Add(item.Path);
            }

            fileNames = new List<string>();
            foreach (var item in files)
            {
                fileNames.Add(item.Path);
            }
            
        }



        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
        }

        public List<string> FileNames
        {
            get
            {
                return fileNames;
            }
        }

        public List<string> FolderNames
        {
            get
            {
                return folderNames;
            }
        }
    }
}
