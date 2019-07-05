﻿using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class StorageFile
    {
        public string folder;
        public string file;
        public string FullPath()
        {
            return FS.Combine(folder, file);
        }

        public StorageFile(string folder, string file)
        {
            this.folder = folder;
            this.file = file;
        }
    }

}