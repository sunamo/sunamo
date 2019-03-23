using sunamo;
using sunamo.Data;
using sunamo.Enums;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop
{
    public class FileNameWithDateTime
    {
        public DateTime dt = DateTime.MinValue;
        public string name = "";

        /// <summary>
        /// Pokud bude vyplněná, nebude se používat čas, který i tak bude uložen v proměnné dt
        /// </summary>
        public int? serie = null;
        public string fnwoe = "";

        public int SerieValue
        {
            get
            {
                return serie.Value;
            }
        }

        string displayText = null;
        string row1 = string.Empty;
        string row2 = string.Empty;

        /// <summary>
        /// Create instance with CreateObjectFileNameWithDateTime
        /// Both can be SE, is used in dispalyText
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        public FileNameWithDateTime(string row1, string row2)
        {
            this.displayText = row1 + " " + row2;
            this.row1 = row1;
            this.row2 = row2;
        }

        public string Row1 { get { return row1; } }
        public string Row2 { get { return row2; } }

        public override string ToString()
        {
            return displayText;
        }
    }

    public class DateTimeFileIndex
    {
        
        public event VoidT<List<FileNameWithDateTime>> InitComplete;
        StorageFolder folder = null;
        string ext = null;
        //SunamoDictionary<string, DateTime> dict = new SunamoDictionary<string, DateTime>();
        public List<FileNameWithDateTime> files = new List<FileNameWithDateTime>();
        FileEntriesDuplicitiesStrategy ds = FileEntriesDuplicitiesStrategy.Time;
        Langs l = Langs.cs;

        public string GetFullPath(FileNameWithDateTime o)
        {
            return FS.Combine(folder.fullPath, o.fnwoe + ext);
        }

        public DateTimeFileIndex()
        {
            //Initialize(af, ext, ds, addPostfix);
        }

        public async void Initialize(AppFolders af, string ext, FileEntriesDuplicitiesStrategy ds, bool addPostfix)
        {
            this.ds = ds;
            this.folder = new StorageFolder( AppData.ci.GetFolder(af));

            this.ext = ext;
            string mask = "????_??_??_";
            if (ds == FileEntriesDuplicitiesStrategy.Serie)
            {
                mask += "S_?*_";
            }
            else if (ds == FileEntriesDuplicitiesStrategy.Time)
            {
                mask += "??_??_";
            }
            else
            {
                throw new Exception("Not supported strategy of saving files.");
            }
            mask +=  "*" + ext;
            var files2 = FS.GetFiles(folder.fullPath, mask, SearchOption.TopDirectoryOnly);

            foreach (var item in files2)
            {
                files.Add(CreateObjectFileNameWithDateTime(string.Empty, string.Empty, item));
            }

            if (ds == FileEntriesDuplicitiesStrategy.Serie)
            {
                files.Sort(new CompareFileNameWithDateTimeBySerie().Desc);
            }
            files.Sort(new CompareFileNameWithDateTimeByDateTime().Desc);
            if (InitComplete != null)
            {
                InitComplete(files);
            }
        }

        public FileNameWithDateTime CreateObjectFileNameWithDateTime(string row1, string row2, string item)
        {
            #region Copy for inspire
            //add.dt = date;
            //add.serie = serie;
            //add.name = postfix;
            //add.fnwoe = DeleteWrongCharsInFileName(fnwoe); 
            #endregion

            return null;
        }

        private static string GetDisplayText(DateTime date, int? serie, Langs l)
        {
            string displayText;
            if (serie == null)
            {
                displayText = DTHelper.DateTimeToString(date, l, SqlServerHelper.DateTimeMinVal);
            }
            else
            {
                int ser = serie.Value;
                string addSer = "";
                if (ser != 0)
                {
                    addSer = " (" + ser + ")";
                }
                displayText = DTHelper.DateToString(date, l) + addSer;
            }

            return displayText;
        }

        private FileNameWithDateTime CreateObjectFileNameWithDateTime(string row1, string row2, DateTime date, int? serie, string postfix, string fnwoe)
        {
            FileNameWithDateTime add = new FileNameWithDateTime(row1, row2);
            add.dt = date;
            add.serie = serie;
            add.name = postfix;
            add.fnwoe = DeleteWrongCharsInFileName(fnwoe);
            return add;
        }

        string DeleteWrongCharsInFileName(string fnwoe)
        {
            return SH.ReplaceAll(FS.DeleteWrongCharsInFileName(fnwoe, false), "_", " ");
        }

        public async void DeleteFile(FileNameWithDateTime o)
        {
            try
            {
                StorageFile t = await GetStorageFile(o);
                //File.Delete(t);
                await FS.DeleteFile(t);
                files.Remove(o);
            }
            catch (Exception)
            {
                ThisApp.SetStatus (TypeOfMessage.Error, RL.GetString("FileCannotBeDeleted"));
            }
        }

        public async Task<StorageFile> GetStorageFile(FileNameWithDateTime o)
        {
            return (await FS.GetStorageFile(folder, o.fnwoe + ext));
            //return FS.Combine(folder, o.fnwoe + ext);
        }

        /// <summary>
        /// Zapíše soubor FileEntriesDuplicitiesStrategy se strategií specifikovanou v konstruktoru
        /// Nepřidává do kolekce files, vrací objekt 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        public async Task<FileNameWithDateTime> SaveFileWithDate(string name, string content)
        {
            DateTime dt = DateTime.Now;
            DateTime today = DateTime.Today;
            string fnwoe = "";
            int? max = null;
            if (ds == FileEntriesDuplicitiesStrategy.Time)
            {
                fnwoe = ConvertDateTimeToFileNamePostfix.ToConvention(name, dt, true);
            }
            else if (ds == FileEntriesDuplicitiesStrategy.Serie)
            {
                IEnumerable<int?> ml = files.Where(u => u.dt == today).Select(s => s.serie);

                max = ml.Count();
                if (max != 0)
                {
                    max = ml.Max() + 1;
                }
                if (!max.HasValue)
                {
                    max = 1;
                }
                fnwoe = DTHelper.DateTimeToFileName(dt, false) + "_S_" + max.Value + "_" + name;
            }
            else
            {
                // Zbytečné, kontroluje se již v konstruktoru
            }
            StorageFile storageFile = await FS.GetStorageFile(folder, DeleteWrongCharsInFileName(fnwoe) + ext);
             TF.SaveFile(content, storageFile);

#if DEBUG
            DebugLogger.DebugWriteLine(storageFile.FullPath());
#endif

            var o = CreateObjectFileNameWithDateTime(GetDisplayText(dt, max, l), name, dt, max, name, fnwoe);
            files.Add(o);
            return o;
        }
    }

    public class CompareFileNameWithDateTimeBySerie : ISunamoComparer<FileNameWithDateTime>
    {
        public int Desc(FileNameWithDateTime x, FileNameWithDateTime y)
        {
            return x.SerieValue.CompareTo(y.SerieValue) * -1;
        }

        public int Asc(FileNameWithDateTime x, FileNameWithDateTime y)
        {
            return x.SerieValue.CompareTo(y.SerieValue);
        }
    }

    public class CompareFileNameWithDateTimeByDateTime : ISunamoComparer<FileNameWithDateTime>
    {
        public int Desc(FileNameWithDateTime x, FileNameWithDateTime y)
        {
            return x.dt.CompareTo(y.dt) * -1;
        }

        public int Asc(FileNameWithDateTime x, FileNameWithDateTime y)
        {
            return x.dt.CompareTo(y.dt);
        }
    }
}
