using sunamo;
using sunamo.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sunamo.Indexing.FileSystem
{
    /// <summary>
    /// Připomíná práci s databází - k označení složek se používají čísla int
    /// 
    /// 
    /// </summary>
    public class FileIndex
    {
        #region Only single static variable relativeDirectories
        /// <summary>
        /// Without base paths
        /// </summary>
        public static List<string> relativeDirectories = new List<string>();
        #endregion

        #region Variables
        /// <summary>
        /// 
        /// </summary>
        public List<FileItem> files = new List<FileItem>();
        /// <summary>
        /// All folders which was processed expect root
        /// </summary>
        private List<FolderItem> _folders = new List<FolderItem>();
        private int _actualFolderID = -1;

        // TODO: Is directories somewhere used?
        /// <summary>
        /// NEOBSAHUJE VSECHNY ZPRACOVANE SLOZKY
        /// Všechny složky tak jak byly postupně přidávany do metody AddFolderRecursively
        /// 
        /// </summary>
        public static List<string> directories = new List<string>();
        private string _basePath = null;

        public string BasePath
        {
            get
            {
                return _basePath;
            }
        }
        #endregion

        #region Instance method
        /// <summary>
        /// Get folders with name A2. A1 is IDParent 
        /// </summary>
        /// <param name="prohledavatSlozky"></param>
        /// <param name="name"></param>
        
        public static CheckBoxData<TWithSize<string>>[,] CheckVertically(CheckBoxData<TWithSize<string>>[,] allRows)
        {
            int columns = allRows.GetLength(1);
            int rows = allRows.GetLength(0);

            // List all files
            for (int c = 0; c < columns; c++)
            {
                // Create collections for all rows
                // key - row, value - size
                Dictionary<int, long> fileSize = new Dictionary<int, long>();
                // For easy compare of size and find out any difference
                List<long> fileSize2 = new List<long>();

                for (int r = 0; r < rows; r++)
                {
                    CheckBoxData<TWithSize<string>> cbd = allRows[r, c];
                    if (cbd != null)
                    {
                        fileSize.Add(r, cbd.t.size);
                        fileSize2.Add(cbd.t.size);
                    }
                }

                #region Get min and max size
                fileSize2.Sort();

                long min = fileSize2[0];
                long max = fileSize2[fileSize2.Count - 1];
                #endregion

                #region Tick potencially unecesary files
                if (fileSize.Count > 1)
                {
                    if (min == max)
                    {
                        TickIfItIsForDelete(allRows, 0, c, fileSize, min, max, false);
                        for (int r = 1; r < rows; r++)
                        {
                            TickIfItIsForDelete(allRows, r, c, fileSize, min, max, true);
                        }
                    }
                    else
                    {
                        for (int r = 0; r < rows; r++)
                        {
                            TickIfItIsForDelete(allRows, r, c, fileSize, min, max, null);
                        }
                    }
                }
                else
                {
                    // Maybe leave file with zero size?
                    TickIfItIsForDelete(allRows, 0, c, fileSize, min, max, false);
                }
                #endregion
            }
            return allRows;
        }

        /// <summary>
        /// Check CheckBox in condition of size and A7 in location specified parameter row A2 and column A3
        /// 
        /// A4 to find size of file. In keys are indexes.
        /// If size is A5 min, check. 
        /// If A6 max, uncheck. 
        /// Or none of this, set null. This behaviour can be changed setted A7 forceToAll
        /// </summary>
        /// <param name="allRows"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="fileSize"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="forceToAll"></param>
        private static void TickIfItIsForDelete(CheckBoxData<TWithSize<string>>[,] allRows, int row, int column, Dictionary<int, long> fileSize, long min, long max, bool? forceToAll)
        {
            CheckBoxData<TWithSize<string>> cbd = allRows[row, column];
            if (cbd != null)
            {
                long filSiz = fileSize[row];
                if (filSiz == -1)
                {
                }
                else if (filSiz == max)
                {
                    if (forceToAll.HasValue)
                    {
                        cbd.tick = forceToAll.Value;
                    }
                    else
                    {
                        cbd.tick = false;
                    }
                }
                else if (filSiz == min)
                {
                    if (forceToAll.HasValue)
                    {
                        cbd.tick = forceToAll.Value;
                    }
                    else
                    {
                        cbd.tick = true;
                    }
                }
                else
                {
                    if (forceToAll.HasValue)
                    {
                        cbd.tick = forceToAll.Value;
                    }
                    else
                    {
                        cbd.tick = null;
                    }
                }
            }
        }
        #endregion
    }
}