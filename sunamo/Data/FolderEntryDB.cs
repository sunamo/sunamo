using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class FolderEntryDB : FolderEntry
    {
        public int ID = -1;

        public FolderEntryDB(int ID, string RelativePath) : base(RelativePath)
        {
            this.ID = ID;
            
        }

        public FolderEntryDB(string RelativePath)
            : base(RelativePath)
        {
        }

        public override string ToString()
        {
            return RelativePath.ToString();
        }
    }

