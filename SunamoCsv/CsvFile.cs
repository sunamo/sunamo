using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SunamoExceptions;

/// <summary>
/// Class to hold csv data
/// Downloaded from http://www.codeproject.com/Articles/86973/C-CSV-Reader-and-Writer
/// </summary>
[Serializable]
    public sealed class CsvFile
    {
    static Type type = typeof(CsvFile);
        public CsvFile()
        {

        }

        public CsvFile(char delimiter)
        {
            CsvReader.delimiter = delimiter;
        }

        #region Properties

        /// <summary>
        /// Gets the file headers
        /// </summary>
        public readonly List<string> Headers = new List<string>();

        /// <summary>
        /// Gets the records in the file
        /// </summary>
        public readonly CsvRecords Records = new CsvRecords();

        /// <summary>
        /// Gets the header count
        /// </summary>
        public int HeaderCount
        {
            get
            {
                return Headers.Count;
            }
        }

        public List<string> Strings(int v)
        {
            List<string> list = new List<string>();
            var objects = Objects(v);
            foreach (var item in objects)
            {
                list.Add(item[0]);
            }
            return list;
        }

        /// <summary>
        /// Return null when cannot be parsed
        /// </summary>
        /// <param name="v"></param>
        public List<DateTime?> DateTimes(int v)
        {
            DateTime dt;
            List<DateTime?> list = new List<DateTime?>();
            var objects = Objects(v);
            foreach (var item in objects)
            {
                dt = DTHelperCs.ParseTimeCzech(item[0]);
                if (dt != DateTime.MinValue)
                {
                    list.Add(new System.DateTime(1,1,1, dt.Hour, dt.Minute, dt.Second));
                }
                else
                {
                    list.Add(null);
                }

            }
            return list;
        }

        public List<List<string>> Objects(params int[] columns)
        {
            List<List<string>> result = new List<List<string>>();
            int i = 0;
            List<string> o = null;

            foreach (var item in Records)
            {
                o = new List<string>( columns.Length);
            CA.InitFillWith(o, columns.Length);
                for (i = 0; i < columns.Length; i++)
                {
                    o[i] = item.Fields[columns[i]];
                }
                result.Add(o);
            }
            return result;
        }

        /// <summary>
        /// Gets the record count
        /// </summary>
        public int RecordCount
        {
            get
            {
                return Records.Count;
            }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets a record at the specified index
        /// </summary>
        /// <param name="recordIndex">Record index</param>
        /// <returns>CsvRecord</returns>
        public CsvRecord this[int recordIndex]
        {
            get
            {
                if (recordIndex > (Records.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no record at index {0}.", recordIndex));

                return Records[recordIndex];
            }
        }

  

    /// <summary>
    /// Gets the field value at the specified record and field index
    /// </summary>
    /// <param name="recordIndex">Record index</param>
    /// <param name="fieldIndex">Field index</param>
    public string this[int recordIndex, int fieldIndex]
        {
            get
            {
                if (recordIndex > (Records.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no record at index {0}.", recordIndex));

                CsvRecord record = Records[recordIndex];
                if (fieldIndex > (record.Fields.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no field at index {0} in record {1}.", fieldIndex, recordIndex));

                return record.Fields[fieldIndex];
            }
            set
            {
                if (recordIndex > (Records.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no record at index {0}.", recordIndex));

                CsvRecord record = Records[recordIndex];

                if (fieldIndex > (record.Fields.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no field at index {0}.", fieldIndex));

                record.Fields[fieldIndex] = value;
            }
        }

        /// <summary>
        /// Gets the field value at the specified record index for the supplied field name
        /// </summary>
        /// <param name="recordIndex">Record index</param>
        /// <param name="fieldName">Field name</param>
        public string this[int recordIndex, string fieldName]
        {
            get
            {
                if (recordIndex > (Records.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no record at index {0}.", recordIndex));

                CsvRecord record = Records[recordIndex];

                int fieldIndex = -1;

                for (int i = 0; i < Headers.Count; i++)
                {
                    if (string.Compare(Headers[i], fieldName) != 0)
                        continue;

                    fieldIndex = i;
                    break;
                }

                if (fieldIndex == -1)
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no field header with the name '{0}'", fieldName));

                if (fieldIndex > (record.Fields.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no field at index {0} in record {1}.", fieldIndex, recordIndex));

                return record.Fields[fieldIndex];
            }
            set
            {
                if (recordIndex > (Records.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no record at index {0}.", recordIndex));

                CsvRecord record = Records[recordIndex];

                int fieldIndex = -1;

                for (int i = 0; i < Headers.Count; i++)
                {
                    if (string.Compare(Headers[i], fieldName) != 0)
                        continue;

                    fieldIndex = i;
                    break;
                }

                if (fieldIndex == -1)
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no field header with the name '{0}'", fieldName));

                if (fieldIndex > (record.Fields.Count - 1))
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("There is no field at index {0} in record {1}.", fieldIndex, recordIndex));

                record.Fields[fieldIndex] = value;
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Populates the current instance from the specified file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        public void Populate(string filePath, bool hasHeaderRow)
        {
            Populate(filePath, null, hasHeaderRow, false);
        }

        /// <summary>
        /// Populates the current instance from the specified file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="_trimColumns">True if column values should be trimmed, otherwise false</param>
        public void Populate(string filePath, bool hasHeaderRow, bool _trimColumns)
        {
            Populate(filePath, null, hasHeaderRow, _trimColumns);
        }

        /// <summary>
        /// Populates the current instance from the specified file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="_trimColumns">True if column values should be trimmed, otherwise false</param>
        public void Populate(string filePath, Encoding encoding, bool hasHeaderRow, bool _trimColumns)
        {
            using (CsvReader reader = new CsvReader(filePath, encoding) { HasHeaderRow = hasHeaderRow, TrimColumns = _trimColumns })
            {
                PopulateCsvFile(reader);
            }
        }

        /// <summary>
        /// Populates the current instance from a stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        public void Populate(Stream stream, bool hasHeaderRow)
        {
            Populate(stream, null, hasHeaderRow, false);
        }

        /// <summary>
        /// Populates the current instance from a stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="_trimColumns">True if column values should be trimmed, otherwise false</param>
        public void Populate(Stream stream, bool hasHeaderRow, bool _trimColumns)
        {
            Populate(stream, null, hasHeaderRow, _trimColumns);
        }

        /// <summary>
        /// Populates the current instance from a stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="_trimColumns">True if column values should be trimmed, otherwise false</param>
        public void Populate(Stream stream, Encoding encoding, bool hasHeaderRow, bool _trimColumns)
        {
            using (CsvReader reader = new CsvReader(stream, encoding) { HasHeaderRow = hasHeaderRow, TrimColumns = _trimColumns })
            {
                PopulateCsvFile(reader);
            }
        }

        /// <summary>
        /// Populates the current instance from a string
        /// </summary>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="csvContent">Csv text</param>
        public void Populate(bool hasHeaderRow, string csvContent)
        {
            Populate(hasHeaderRow, csvContent, null, false);
        }

        /// <summary>
        /// Populates the current instance from a string
        /// </summary>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="csvContent">Csv text</param>
        /// <param name="_trimColumns">True if column values should be trimmed, otherwise false</param>
        public void Populate(bool hasHeaderRow, string csvContent, bool _trimColumns)
        {
            Populate(hasHeaderRow, csvContent, null, _trimColumns);
        }

        /// <summary>
        /// Populates the current instance from a string
        /// </summary>
        /// <param name="hasHeaderRow">True if the file has a header row, otherwise false</param>
        /// <param name="csvContent">Csv text</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="_trimColumns">True if column values should be trimmed, otherwise false</param>
        public void Populate(bool hasHeaderRow, string csvContent, Encoding encoding, bool _trimColumns)
        {
            using (CsvReader reader = new CsvReader(encoding, csvContent) { HasHeaderRow = hasHeaderRow, TrimColumns = _trimColumns })
            {
                PopulateCsvFile(reader);
            }
        }

        /// <summary>
        /// Populates the current instance using the CsvReader object
        /// </summary>
        /// <param name="reader">CsvReader</param>
        private void PopulateCsvFile(CsvReader reader)
        {
            Headers.Clear();
            Records.Clear();

            bool addedHeader = false;

            while (reader.ReadNextRecord())
            {
                if (reader.HasHeaderRow && !addedHeader)
                {
                    reader.Fields.ForEach(field => Headers.Add(field));
                    addedHeader = true;
                    continue;
                }

                CsvRecord record = new CsvRecord();
                reader.Fields.ForEach(field => record.Fields.Add(field));
                Records.Add(record);
            }
        }

        #endregion Methods
    }

    /// <summary>
    /// Class for a collection of CsvRecord objects
    /// </summary>
    [Serializable]
    public sealed class CsvRecords : List<CsvRecord>
    {
    }

    /// <summary>
    /// Csv record class
    /// </summary>
    [Serializable]
    public sealed class CsvRecord
    {
        #region Properties

        /// <summary>
        /// Gets the Fields in the record
        /// </summary>
        public readonly List<string> Fields = new List<string>();

        /// <summary>
        /// Gets the number of fields in the record
        /// </summary>
        public int FieldCount
        {
            get
            {
                return Fields.Count;
            }
        }

        #endregion Properties
    }