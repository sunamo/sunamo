using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



    public class SearchCodeElementsUCData
    {
        public TextBox txtSearchInCodeElementName;
        public TextBox txtSearchInContent;
        public ComboBox txtSearchInPath;
        CheckBox chbSearchInContent;
        CheckBox chbSearchInPath;
        CheckBox chbSearchInCodeElementName;
        public Dictionary<string, List<int>> founded;
        public string file = null;

        public List<int> actualFileSearchOccurences
        {
            get
            {
                if (founded == null || file == null)
                {
                    return new List<int>();
                }
                return founded[file];
            }
        }

        public SearchCodeElementsUCData(TextBox txtSearchInCodeElementName, TextBox txtSearchInContent, ComboBox txtSearchInPath, CheckBox chbSearchInContent, CheckBox chbSearchInPath, CheckBox chbSearchInCodeElementName)
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

