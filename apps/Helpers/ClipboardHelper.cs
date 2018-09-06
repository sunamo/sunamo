using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace apps
{
    public class ClipboardHelper
    {
        public async static Task<string> GetText()
        {
            string dataFormat = StandardDataFormats.Text;
            return SH.NullToStringOrEmpty( await GetFormat(dataFormat));
        }

        /// <summary>
        /// Vrátí null v případě že data nebudou nalezeny ve schránce
        /// </summary>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        private async static Task<object> GetFormat(string dataFormat)
        {
            var dpv = Clipboard.GetContent();
            if (dpv.Contains(dataFormat))
            {
                //return await Clipboard.GetContent().GetTextAsync();
                return await Clipboard.GetContent().GetDataAsync(dataFormat);
            }
            return null;
        }

        public static void SetText(string v)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(v);
            Clipboard.SetContent(dp);
        }
    }
}
