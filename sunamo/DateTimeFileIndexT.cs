using System;
using System.Collections.Generic;
using System.Linq;
using desktop;
using sunamo.Enums;
using sunamo.Essential;
public class DateTimeFileIndex<StorageFolder, StorageFile>
{
static Type type = typeof(DateTimeFileIndex<StorageFolder,StorageFile>);
    public AbstractCatalog<StorageFolder, StorageFile> ac;
    public event VoidT<List<FileNameWithDateTime<StorageFolder, StorageFile>>> InitComplete;
    private StorageFolder _folder = default(StorageFolder);
    private string _ext = null;
    //SunamoDictionary<string, DateTime> dict = new SunamoDictionary<string, DateTime>();
    public List<FileNameWithDateTime<StorageFolder, StorageFile>> files = new List<FileNameWithDateTime<StorageFolder, StorageFile>>();
    private FileEntriesDuplicitiesStrategy _ds = FileEntriesDuplicitiesStrategy.Time;
    private Langs _l = Langs.cs;
    AppFolders af = AppFolders.Data;
    public string GetFullPath(FileNameWithDateTime<StorageFolder, StorageFile> o)
    {
        //ThrowExceptions.Custom
        return FS.StorageFilePath<StorageFolder, StorageFile>( FS.GetStorageFile<StorageFolder, StorageFile>(_folder, o.fnwoe + _ext, ac), ac);
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
    public void Initialize(AppFolders af, string ext, FileEntriesDuplicitiesStrategy ds, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        this.ac = ac;
        _ds = ds;
        _folder = FS.CiStorageFolder<StorageFolder, StorageFile>( AppData.ci.GetFolder(af), null);
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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), sess.i18n(XlfKeys.NotSupportedStrategyOfSavingFiles) + ".");
        }
        mask += AllStrings.asterisk + ext;
        var files2 = FS.GetFilesInterop(_folder, mask, false, ac);
        foreach (var item in files2)
        {
            var itemS = FS.StorageFilePath<StorageFolder, StorageFile>(item, ac);
            files.Add(CreateObjectFileNameWithDateTime(string.Empty, string.Empty, itemS, ac));
        }
        if (ds == FileEntriesDuplicitiesStrategy.Serie)
        {
            files.Sort(new CompareFileNameWithDateTimeBySerie<StorageFolder, StorageFile>().Desc);
        }
        files.Sort(new CompareFileNameWithDateTimeByDateTime<StorageFolder, StorageFile>().Desc);
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
            var parts = SH.Split(fnwoe, AllStrings.lowbar);
            serie = int.Parse(parts[4]);
            postfix = SH.JoinFromIndex(5, AllStrings.lowbar, parts);
            return FileEntriesDuplicitiesStrategy.Serie;
        }
        else
        {
            string t = fnwoe.Substring(11, 5);
            var parts = SH.Split(t, AllStrings.lowbar);
            hour = int.Parse(parts[0]);
            minute = int.Parse(parts[1]);
            postfix = SH.JoinFromIndex(5, AllStrings.lowbar, parts);
            return FileEntriesDuplicitiesStrategy.Time;
        }
    }
    public FileNameWithDateTime<StorageFolder, StorageFile> CreateObjectFileNameWithDateTime(string row1, string row2, string item, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        FileNameWithDateTime<StorageFolder, StorageFile> add = new FileNameWithDateTime<StorageFolder, StorageFile>(row1, row2, ac);
        // Here must return c#
        var fnwoe = FS.GetFileNameWithoutExtension<string, string>(item, null);
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
            displayText = DTHelper.DateTimeToString(date, l, Consts.DateTimeMinVal);
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
    private FileNameWithDateTime<StorageFolder, StorageFile> CreateObjectFileNameWithDateTime(string row1, string row2, DateTime date, int? serie, string postfix, string fnwoe)
    {
        FileNameWithDateTime<StorageFolder, StorageFile> add = new FileNameWithDateTime<StorageFolder, StorageFile>(row1, row2, ac);
        add.dt = date;
        add.serie = serie;
        add.name = postfix;
        add.fnwoe = DeleteWrongCharsInFileName(fnwoe);
        return add;
    }
    private string DeleteWrongCharsInFileName(string fnwoe)
    {
        return SH.ReplaceAll(FS.DeleteWrongCharsInFileName(fnwoe, false), AllStrings.lowbar, AllStrings.space);
    }
    public void DeleteFile(FileNameWithDateTime<StorageFolder, StorageFile> o)
    {
        try
        {
            string t = GetStorageFile(o);
            //File.Delete(t);
            FS.TryDeleteFile(t);
            files.Remove(o);
        }
        catch (Exception ex)
        {
            ThisApp.SetStatus(TypeOfMessage.Error, sess.i18n(XlfKeys.FileCannotBeDeleted));
        }
    }
    public string GetStorageFile(FileNameWithDateTime<StorageFolder, StorageFile> o)
    {
        return FS.StorageFilePath<StorageFolder, StorageFile>( FS.GetStorageFile(_folder, o.fnwoe + _ext, ac), ac);
        //return FS.Combine(folder, o.fnwoe + ext);
    }
    /// <summary>
    /// Zapíše soubor FileEntriesDuplicitiesStrategy se strategií specifikovanou v konstruktoru
    /// Nepřidává do kolekce files, vrací objekt 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="name"></param>
    public FileNameWithDateTime<StorageFolder, StorageFile> SaveFileWithDate(string name, string content)
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
            fnwoe = DTHelper.DateTimeToFileName(dt, false) + "_S_" + max.Value + AllStrings.lowbar + name;
        }
        else
        {
            // Zbytečné, kontroluje se již v konstruktoru
        }

        var storageFile = FS.GetStorageFile<StorageFolder, StorageFile>(_folder, DeleteWrongCharsInFileName(fnwoe) + _ext, ac);
        TF.WriteAllText<StorageFolder, StorageFile>(storageFile, content, ac);
#if DEBUG
        //DebugLogger.DebugWriteLine(storageFile.FullPath());
#endif
        var o = CreateObjectFileNameWithDateTime(GetDisplayText(dt, max, _l), name, dt, max, name, fnwoe);
        files.Add(o);
        return o;
    }
}