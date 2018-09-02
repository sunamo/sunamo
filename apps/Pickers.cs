using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using sunamo.Enums;

namespace apps
{
    public static class Pickers
    {
        /// <summary>
        /// Vrátí null v případě že uživatel nevybere žádný soubor
        /// </summary>
        /// <param name="pvm"></param>
        /// <param name="plid"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        public async static Task<StorageFile> GetFile(PickerViewMode pvm, PickerLocationId plid, params string[] exts)
        {
            var picker = new FileOpenPicker();

            picker.SuggestedStartLocation = plid;
            picker.ViewMode = pvm;
            foreach (var item in exts)
            {
                picker.FileTypeFilter.Add(item);
            }
            return await picker.PickSingleFileAsync();
        }

        public async static Task< IReadOnlyList<StorageFile>> GetFiles(PickerViewMode pvm, PickerLocationId plid, params string[] exts)
        {
            var picker = new FileOpenPicker();

            picker.SuggestedStartLocation = plid;
            picker.ViewMode = pvm;
            foreach (var item in exts)
            {
                picker.FileTypeFilter.Add(item);
            }
            return await picker.PickMultipleFilesAsync();
        }

        public async static Task<StorageFolder> GetFolder( PickerViewMode pvm, PickerLocationId plid, params string[] exts)
        {
            var picker = new FolderPicker();

            picker.SuggestedStartLocation = plid;
            picker.ViewMode = pvm;
            foreach (var item in exts)
            {
                picker.FileTypeFilter.Add(item);
            }
            return await picker.PickSingleFolderAsync();
        }
    }
}
