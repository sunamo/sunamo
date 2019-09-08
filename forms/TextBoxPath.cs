using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace forms
{ 
/// <summary>
/// Must be Forms because WPF TextBox dont have autocomplete
/// </summary>
public class TextBoxPath : TextBox
{
        string basePath = null;
        const string deli = AllStrings.bs;

        public TextBoxPath()
    {
        AutoCompleteMode = AutoCompleteMode.Suggest;
        base.AutoCompleteSource = AutoCompleteSource.CustomSource;

            TextChanged += textBox1_TextChanged;
    }

        public void Init(string basePath)
        {
            folders = GetFolders(basePath);

            FS.WithEndSlash(ref basePath);

            this.basePath = basePath;
        }

        private List<string> GetFolders(string basePath)
        {
            folders = FS.GetFolders(basePath);
            FS.WithEndSlash(ref basePath);
            CA.TrimStart(basePath, folders);

            return folders;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
    {
        var t = (TextBox)sender;
        if (t != null)
        {
            //say you want to do a search when user types 3 or more chars
            //if (t.Text.Length >= 3)
            //{
                //SuggestStrings will have the logic to return array of strings either from cache/db
                var arr = SuggestStrings(t.Text);

                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr.ToArray());

                base.AutoCompleteCustomSource = collection;
            //}
        }
    }

    int previous = 0;
    List<string> folders = null;

    private List<string> SuggestStrings(string text2)
    {
        

        var text = text2.Trim().Trim(AllChars.bs, AllChars.slash);
        var tokens = FS.GetTokens(text);

        var last = tokens[tokens.Count - 1];

        if (!text2.EndsWith(deli))
        {
            return folders.Where(e => e.StartsWith(last)).ToList();
        }

        // Entered ends with deli
        var actual = SH.OccurencesOfStringIn(text2, deli);
        if (actual != previous)
        {
            folders = GetFolders(FS.Combine(basePath, text));
        }

        return folders;
    }
}
}