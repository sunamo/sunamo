using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace apps
{
    /// <summary>
    /// Správná cesta není pomocí předávání stringu, ani StorageFile, ani StorageFolder+string, ani pomocí Streamu ale jedině a pouze pomocí bajtů a metod ReadBufferAsync a WriteBufferAsync
    /// </summary>
    public class TF
    {
        public async static Task< string[]> GetLines(StorageFile s)
        {
            var d = await FileIO.ReadLinesAsync(s);
            return d.ToArray() ;
        }


        public async static Task<string> ReadFile(StorageFile s)
        {
            return await FileIO.ReadTextAsync(s);
        }



        public async static Task<string> ReadFile(string s)
        {
            return await ReadFile(await StorageFile.GetFileFromPathAsync(s));
        }


        public async static void SaveFile(string p, string VybranySouborLogu)
        {
            await SaveFile(p, await StorageFile.GetFileFromPathAsync(VybranySouborLogu), false);
        }

        public async static Task SaveFile(string p, StorageFile sf)
        {
            SaveFile(p, sf, false);
        }

        public async static Task SaveFile(string p, StorageFile sf, bool append)
        {
            if (append)
            {
                await FileIO.AppendTextAsync(sf, p);
            }
            else
            {
                await FileIO.WriteTextAsync(sf, p);
            }
        }


        public async static Task<IEnumerable<string>> ReadLines(StorageFile sf)
        {
            return SH.GetLinesList( await ReadFile(sf));
        }

        internal static Task AppendToFile(string value, StorageFile fileToSave)
        {
            return SaveFile(value, fileToSave, true);
        }
    }
}
