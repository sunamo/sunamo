using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace sunamo.Helpers
{
    public static class TempHelper
    {
        static StorageFolder folder = null;

        public async static Task<StorageFile> GetTempStorageFile()
        {
            if (folder == null)
            {
                folder = await StorageFolder.GetFolderFromPathAsync(Path.GetTempPath());
            }
            return await folder.GetFileAsync(Path.GetTempFileName());
        }
    }
}
