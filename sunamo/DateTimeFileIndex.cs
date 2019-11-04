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

        private string _displayText = null;
        private string _row1 = string.Empty;
        private string _row2 = string.Empty;

        /// <summary>
        /// Create instance with CreateObjectFileNameWithDateTime
        /// Both can be SE, is used in dispalyText
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        public FileNameWithDateTime(string row1, string row2)
        {
            _displayText = row1 + AllStrings.space + row2;
            _row1 = row1;
            _row2 = row2;
        }

        /// <summary>
        /// First row in SelectorHelperListViewUC
        /// </summary>
        public string Row1 { get { return _row1; } set { _row1 = value; } }
        /// <summary>
        /// Second row in SelectorHelperListViewUC
        /// </summary>
        public string Row2 { get { return _row2; } set { _row2 = value; } }

        public override string ToString()
        {
            return _displayText;
        }
    }

    public class DateTimeFileIndex
    {
        public event VoidT<List<FileNameWithDateTime>> InitComplete;
        private StorageFolder _folder = null;
        private string _ext = null;
        //SunamoDictionary<string, DateTime> dict = new SunamoDictionary<string, DateTime>();
        public List<FileNameWithDateTime> files = new List<FileNameWithDateTime>();
        private FileEntriesDuplicitiesStrategy _ds = FileEntriesDuplicitiesStrategy.Time;
        private Langs _l = Langs.cs;
        AppFolders af = AppFolders.Data;
        

        public string GetFullPath(FileNameWithDateTime o)
        {
            return FS.Combine(_folder.fullPath, o.fnwoe + _ext);
        }

        public DateTimeFileIndex()
        {
            

            //Initialize(af, _ext, _ds);
        }

        /// <summary>
        /// A4 was nowhere used, deleted
        /// </summary>
        /// <param name="af"></param>
        /// <param name="ext"></param>
        /// <param name="ds"></param>
        public async void Initialize(AppFolders af, string ext, FileEntriesDuplicitiesStrategy ds)
        {
            _ds = ds;
            _folder = new StorageFolder(AppData.ci.GetFolder(af));

            _ext = ext;
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
                throw new Exception("Not supported strategy of saving files" + ".");
            }
            mask += AllStrings.asterisk + ext;
            var files2 = FS.GetFiles(_folder.fullPath, mask, SearchOption.TopDirectoryOnly);

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

        private FileEntriesDuplicitiesStrategy GetFileEntriesDuplicitiesStrategy(string fnwoe, out int? serie, out int hour, out int minute, out string postfix)
        {
            serie = null;
            minute = hour = 0;
            if (fnwoe[11] == 'S')
            {
                var parts = SH.Split(fnwoe, AllStrings.us);
                serie = int.Parse(parts[4]);
                postfix = SH.JoinFromIndex(5, AllStrings.us, parts);
                return FileEntriesDuplicitiesStrategy.Serie;
            }
            else
            {
                string t = fnwoe.Substring(11, 5);
                var parts = SH.Split(t, AllStrings.us);
                hour = int.Parse(parts[0]);
                minute = int.Parse(parts[1]);
                postfix = SH.JoinFromIndex(5, AllStrings.us, parts);
                return FileEntriesDuplicitiesStrategy.Time;
            }
        }

        public FileNameWithDateTime CreateObjectFileNameWithDateTime(string row1, string row2, string item)
        {
            FileNameWithDateTime add = new FileNameWithDateTime(row1, row2);

            var fnwoe = FS.GetFileNameWithoutExtension(item);
            #region Copy for inspire
            //2019_03_23_S_0_Pustkovec


            string dateS = fnwoe.Substring(0, 10);
            add.dt = DateTime.ParseExact(dateS, "yyyy_MM_dd", null);


            int? serie;
            int hour;
            int minute;
            string postfix;
            var strategy = GetFileEntriesDuplicitiesStrategy(fnwoe, out serie, out hour, out minute, out postfix);

            add.serie = serie;
            add.dt.AddMinutes(minute);
            add.dt.AddHours(hour);
            add.name = postfix;
            add.fnwoe = DeleteWrongCharsInFileName(fnwoe);

            add.Row1 = postfix;
            add.Row2 = add.dt.ToShortDateString();
            #endregion

            return add;
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
                    addSer = " (" + ser + AllStrings.rb;
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

        private string DeleteWrongCharsInFileName(string fnwoe)
        {
            return SH.ReplaceAll(FS.DeleteWrongCharsInFileName(fnwoe, false), AllStrings.us, AllStrings.space);
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
            catch (Exception ex)
            {
                ThisApp.SetStatus(TypeOfMessage.Error, RL.GetString("FileCannotBeDeleted"));
            }
        }

        public async Task<StorageFile> GetStorageFile(FileNameWithDateTime o)
        {
            return (await FS.GetStorageFile(_folder, o.fnwoe + _ext));
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
            if (_ds == FileEntriesDuplicitiesStrategy.Time)
            {
                fnwoe = ConvertDateTimeToFileNamePostfix.ToConvention(name, dt, true);
            }
            else if (_ds == FileEntriesDuplicitiesStrategy.Serie)
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
                fnwoe = DTHelper.DateTimeToFileName(dt, false) + "_S_" + max.Value + AllStrings.us + name;
            }
            else
            {
                // Zbytečné, kontroluje se již v konstruktoru
            }
            StorageFile storageFile = await FS.GetStorageFile(_folder, DeleteWrongCharsInFileName(fnwoe) + _ext);
            TF.SaveFile(content, storageFile);

#if DEBUG
            //DebugLogger.DebugWriteLine(storageFile.FullPath());
#endif

            var o = CreateObjectFileNameWithDateTime(GetDisplayText(dt, max, _l), name, dt, max, name, fnwoe);
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
