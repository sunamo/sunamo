using System.Collections.Generic;
using System.IO;
namespace web
{
    public class ImagesHelper
    {
        public List<string> GetAllImages()
        {
            List<string> AllPhotos = new List<string>();
            string[] directoriesKocicky = Directory.GetDirectories(GeneralHelper.MapPath("_/i/Kocicky"));
            string[] directoriesSda = Directory.GetDirectories(GeneralHelper.MapPath("_/i/CasdMladez"));
            foreach (var item in directoriesKocicky)
            {
                if (IsInAlbumFormat(item))
                {
                    AllPhotos.AddRange(Directory.GetFiles(item, "*", SearchOption.AllDirectories));
                }

            }
            foreach (var item in directoriesSda)
            {
                if (IsInAlbumFormat(item))
                {
                    AllPhotos.AddRange(Directory.GetFiles(item, "*", SearchOption.AllDirectories));
                }
            }

            return AllPhotos;
        }



        private bool IsInAlbumFormat(string item)
        {
            string r = Path.GetFileName(item);
            if (char.IsUpper(r[0]))
            {
                long l = 0;
                if (long.TryParse(r.Substring(1), out l))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
