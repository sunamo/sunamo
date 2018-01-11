using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace shared
{
    public class FS
    {
        public static void RemoveDiacriticInFileContents(string folder, string mask)
        {
            string[] files = Directory.GetFiles(folder, mask, SearchOption.AllDirectories);
            foreach (string item in files)
            {
                string df2 = File.ReadAllText(item, Encoding.Default);

                if (true) //SH.ContainsDiacritic(df2))
                {
                    TF.SaveFile(SH.TextWithoutDiacritic(df2), item);
                    df2 = SH.ReplaceOnce(df2, "ď»ż", "");
                    TF.SaveFile(df2, item);
                }

            }
        }
    }
}
