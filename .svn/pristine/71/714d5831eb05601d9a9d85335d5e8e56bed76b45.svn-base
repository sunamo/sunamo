using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    public class SelectFromManyHelper<T>
    {
        ISelectFromMany<T> selectFromManyControl = null;
        public bool sufficientFileName = false;
        public string defaultFileForLeave = null;
        public string defaultFileSize = null;

        public Dictionary<string, string> filesWithSize = new Dictionary<string, string>();

        public SelectFromManyHelper(ISelectFromMany<T> selectFromManyControl)
        {
            this.selectFromManyControl = selectFromManyControl;
        }

        #region Files
        public void InitializeByFolder(bool sufficientFileName, string defaultFileForLeave, string folderForSearch)
        {
            filesWithSize.Clear();
            SetBasicVariable(sufficientFileName, defaultFileForLeave);

            string fn = System.IO.Path.GetFileName(defaultFileForLeave);
            string[] files = Directory.GetFiles(folderForSearch, fn, SearchOption.AllDirectories);

            ProcessFilesWithoutSize(files);
            selectFromManyControl.AddControls();
        }

        private void ProcessFilesWithoutSize(string[] files)
        {
            if (sufficientFileName)
            {
                foreach (var item in files)
                {
                    filesWithSize.Add(item, null);
                }
            }
            else
            {
                foreach (var item in files)
                {
                    filesWithSize.Add(item, sunamo.FS.GetSizeInAutoString(sunamo.FS.GetFileSize(item), sunamo.Enums.ComputerSizeUnits.B));
                }
            }
        }

        private void SetBasicVariable(bool sufficientFileName, string defaultFileForLeave)
        {
            this.sufficientFileName = sufficientFileName;
            this.defaultFileForLeave = defaultFileForLeave;

            if (!sufficientFileName)
            {
                this.defaultFileSize = sunamo.FS.GetSizeInAutoString(sunamo.FS.GetFileSize(defaultFileForLeave), sunamo.Enums.ComputerSizeUnits.B);
            }
        }

        public void InitializeByFiles(bool sufficientFileName, string defaultFileForLeave, string[] files)
        {
            filesWithSize.Clear();
            SetBasicVariable(sufficientFileName, defaultFileForLeave);

            ProcessFilesWithoutSize(files);
            selectFromManyControl.AddControls();
        }

        public void InitializeByFilesWithSize(bool sufficientFileName, string defaultFileForLeave, Dictionary<string, long> files)
        {
            filesWithSize.Clear();
            SetBasicVariable(sufficientFileName, defaultFileForLeave);

            foreach (var item in files)
            {
                filesWithSize.Add(item.Key, sunamo.FS.GetSizeInAutoString(item.Value, sunamo.Enums.ComputerSizeUnits.B));
            }
            selectFromManyControl.AddControls();
        } 
        #endregion
    }
}
