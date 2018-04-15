using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Indexing.FileSystem
{
    public class FolderItem : IFSItem
    {
        string name = null;
        public string Name
        {
            get => name;
            set => name = value;
        }

        string path = null;
        public string Path
        {
            get => path;
            set => path = value;
        }


        int iDParent = -1;
        public int IDParent
        {
            get => iDParent;
            set => iDParent = value;
        }
        long length = -1;
        public long Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }
        bool hasFolderSubfolder = false;
        public bool HasFolderSubfolder
        {
            get
            {
                return hasFolderSubfolder;
            }
            set
            {
                hasFolderSubfolder = value;
            }
        }
    }
}
