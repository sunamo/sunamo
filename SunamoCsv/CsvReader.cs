
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;


    /// <summary>
/// Class to read csv content from various sources
/// Downloaded from http://www.codeproject.com/Articles/86973/C-CSV-Reader-and-Writer
    /// </summary>
    public sealed class CsvReader : IDisposable
    {

        #region Members

        private FileStream _fileStream;
        private Stream _stream;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        private Stream _memoryStream;
        private Encoding _encoding;
        private readonly StringBuilder _columnBuilder = new StringBuilder(100);
        private readonly Type _type = Type.File;

    #endregion Members

    /// <summary>
    /// Use CsvFile.DateTimes instead
    /// </summary>
    /// <param name="v"></param>
    
        public DataTable ReadIntoDataTable(System.Type[] columnTypes)
        {
            DataTable dataTable = new DataTable();
            bool addedHeader = false;
            _stream.Position = 0;

            while (ReadNextRecord())
            {
                if (!addedHeader)
                {
                    for (int i = 0; i < Fields.Count; i++)
                        dataTable.Columns.Add(Fields[i], (columnTypes.Length > 0 ? columnTypes[i] : typeof(string)));

                    addedHeader = true;
                    continue;
                }

                DataRow row = dataTable.NewRow();

                for (int i = 0; i < Fields.Count; i++)
                    row[i] = Fields[i];

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

    public static char delimiter = AllChars.comma;

        /// <summary>
        /// Parses a csv line
        /// </summary>
        /// <param name="line">Line</param>
        private void ParseLine(string line)
        {
            Fields = new List<string>();
            bool inColumn = false;
            bool inQuotes = false;
            _columnBuilder.Remove(0, _columnBuilder.Length);

            // Iterate through every character in the line
            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i];

                // If we are not currently inside a column
                if (!inColumn)
                {
                    if (character == AllChars.qm)
                        inQuotes = true;
                    else
                        _columnBuilder.Append(character);

                    inColumn = true;
                    continue;
                }

                // If we are in between double quotes
                if (inQuotes)
                {
                    if (character == AllChars.qm && ((line.Length > (i + 1) && line[i + 1] == delimiter) || ((i + 1) == line.Length)))
                    {
                        inQuotes = false;
                        inColumn = false;
                        i++;
                    }
                    else if (character == AllChars.qm && line.Length > (i + 1) && line[i + 1] == AllChars.qm)
                        i++;
                }
                else if (character == delimiter)
                    inColumn = false;

                // If we are no longer in the column clear the builder and add the columns to the list
                if (!inColumn)
                {
                    Fields.Add(TrimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());
                    _columnBuilder.Remove(0, _columnBuilder.Length);
                }
                else // append the current column
                    _columnBuilder.Append(character);
            }

            // If we are still inside a column add a new one
            if (inColumn)
                Fields.Add(TrimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());   
        }

        /// <summary>
        /// Disposes of all unmanaged resources
        /// </summary>
        public void Dispose()
        {
            if (_streamReader != null)
            {
                _streamReader.Close();
                _streamReader.Dispose();
            }

            if (_streamWriter != null)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
            }

            if (_memoryStream != null)
            {
                _memoryStream.Close();
                _memoryStream.Dispose();
            }

            if (_fileStream != null)
            {
                _fileStream.Close();
                _fileStream.Dispose();
            }

            if ((_type == Type.String || _type == Type.File) && _stream != null)
            {
                _stream.Close();
                _stream.Dispose();
            }
        }

        #endregion Methods

    }

