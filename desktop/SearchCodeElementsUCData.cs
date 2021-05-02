using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



    public class SearchCodeElementsUCData
    {
        public ComboBox txtSearchInCodeElementName;
        public ComboBox txtSearchInContent;
        public ComboBox txtSearchInPath;
        CheckBox chbSearchInContent;
        CheckBox chbSearchInPath;
        CheckBox chbSearchInCodeElementName;
    /// <summary>
    /// In key full path to file, in value lines
    /// </summary>
        public Dictionary<string, List<FoundedCodeElement>> founded;
    string _file = null;
        public string file
    {
        get
        {
            return _file;
        }
        set
        {
            _file = value;
#if DEBUG
            //DebugLogger.Instance.WriteLineNull(value);
#endif
        }
    }

        public List<FoundedCodeElement> actualFileSearchOccurences
        {
            get
            {
                if (founded == null || file == null)
                {
                    return new List<FoundedCodeElement>();
                }
                return founded[file];
            }
        }

        public SearchCodeElementsUCData(ComboBox txtSearchInCodeElementName, ComboBox txtSearchInContent, ComboBox txtSearchInPath, CheckBox chbSearchInContent, CheckBox chbSearchInPath, CheckBox chbSearchInCodeElementName)
        {
            this.txtSearchInCodeElementName = txtSearchInCodeElementName;
            this.txtSearchInContent = txtSearchInContent;
            this.txtSearchInPath = txtSearchInPath;
            this.chbSearchInContent = chbSearchInContent;
            this.chbSearchInPath = chbSearchInPath;
            this.chbSearchInCodeElementName = chbSearchInCodeElementName;
        }

        public bool IsSearchingInElementName
        {
            get
            {
            return !string.IsNullOrWhiteSpace(txtSearchInCodeElementName.Text); // || ( string.IsNullOrWhiteSpace && 
            }
        }

        public bool IsSearchingInContent
        {
            get
            {
            return !string.IsNullOrWhiteSpace(txtSearchInContent.Text);
            //return chbSearchInContent.IsChecked.Value;
            }
        }

        public bool IsSearchingInPath
        {
            get
            {
            return !string.IsNullOrWhiteSpace(txtSearchInPath.Text);
            //return chbSearchInPath.IsChecked.Value;
        }
        }
    }