using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoTreeView
{
    public class ItemProvider
    {
        /// <summary>
        /// All items (folders & files etc.) hiearchic
        /// </summary>
        public static ObservableCollection<Item> LastItems = new ObservableCollection<Item>();
        /// <summary>
        /// All items(folders & files etc.) non hiearchic
        /// </summary>
        public static ObservableCollection<Item> LastItemsNonHiearchic = new ObservableCollection<Item>();

        public static ObservableCollection<Item> GetItems(string path)
        {
            //LastItemsNonHiearchic = new ObservableCollection<Item>();
            var items = new ObservableCollection<Item>();

            var dirInfo = new DirectoryInfo(path);

            DirectoryInfo[] dirs = null;
            try
            {
                dirs = dirInfo.GetDirectories();
            }
            catch (Exception ex)
            {
            }

            if (dirs != null)
            {


                foreach (var directory in dirs)
                {
                    var item = new Item
                    {
                        Name = directory.Name,
                        Path = directory.FullName + AllStrings.bs,
                        Items = GetItems(directory.FullName),
                        IsDirectory = true,
                        TokensCount = (byte)directory.FullName.Split(AllChars.bs).Length

                    };

                    LastItemsNonHiearchic.Add(item);
                    items.Add(item);
                }

            }

            FileInfo[] fils = null;
            try
            {
                fils = dirInfo.GetFiles();
            }
            catch (Exception ex)
            {


            }

            if (fils != null)
            {
                foreach (var file in fils)
                {
                    var item = new Item
                    {
                        Name = file.Name,
                        Path = file.FullName,
                        IsDirectory = false,
                        TokensCount = (byte)file.FullName.Split(AllChars.bs).Length
                    };
                    LastItemsNonHiearchic.Add(item);
                    items.Add(item);
                }
            }
            LastItems = items;
            return items;
        }
    }
}
