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
    public class FileNameWithDateTime :  FileNameWithDateTime<string, string>
    {

        public FileNameWithDateTime(string row1, string row2) : base(row1, row2, null)
        {
        }
    }
    public class FileNameWithDateTime<StorageFolder, StorageFile>
    {
        /// <summary>
        /// I'm in standard, not uwp, therefore I cant point to MainPage
        /// </summary>
        AbstractCatalog<StorageFolder, StorageFile> ac;
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
        /// Create instance with CreateObjectFileNameWithDateTime<StorageFolder, StorageFile>
        /// Both can be SE, is used in dispalyText
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        public FileNameWithDateTime(string row1, string row2, AbstractCatalog<StorageFolder, StorageFile> ac)
        {
            _displayText = row1 + AllStrings.space + row2;
            _row1 = row1;
            _row2 = row2;
            this.ac = ac;
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
    public class DateTimeFileIndex : DateTimeFileIndex<string, string>
    {
    }
    
    public class CompareFileNameWithDateTimeBySerie<StorageFolder, StorageFile> : ISunamoComparer<FileNameWithDateTime<StorageFolder, StorageFile>>
    {
        public int Desc(FileNameWithDateTime<StorageFolder, StorageFile> x, FileNameWithDateTime<StorageFolder, StorageFile> y)
        {
            return x.SerieValue.CompareTo(y.SerieValue) * -1;
        }
        public int Asc(FileNameWithDateTime<StorageFolder, StorageFile> x, FileNameWithDateTime<StorageFolder, StorageFile> y)
        {
            return x.SerieValue.CompareTo(y.SerieValue);
        }
    }
    public class CompareFileNameWithDateTimeByDateTime<StorageFolder, StorageFile> : ISunamoComparer<FileNameWithDateTime<StorageFolder, StorageFile>>
    {
        public int Desc(FileNameWithDateTime<StorageFolder, StorageFile> x, FileNameWithDateTime<StorageFolder, StorageFile> y)
        {
            return x.dt.CompareTo(y.dt) * -1;
        }
        public int Asc(FileNameWithDateTime<StorageFolder, StorageFile> x, FileNameWithDateTime<StorageFolder, StorageFile> y)
        {
            return x.dt.CompareTo(y.dt);
        }
    }
}