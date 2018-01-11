using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Used for example in HostingManager
/// </summary>
    public class FileEntryDB : FileEntry
    {
        public int ID = -1;
        public FileEntryDB(int ID, string Directory, string FileName, long Length) : base(Directory, FileName, Length)
        {
            this.ID = ID;
        }

        /// <summary>
        /// Když chci jakoby použít pouze FileEntry, ale mít typ FileEntryDB
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Directory"></param>
        /// <param name="FileName"></param>
        /// <param name="Length"></param>
        /// <param name="Hash"></param>
        public FileEntryDB(string Directory, string FileName, long Length)
            : base(Directory, FileName, Length)
        {
            
        }

        public override string ToString()
        {
            return sunamo.UH.Combine(false,Directory, FileName);
        }
    }

