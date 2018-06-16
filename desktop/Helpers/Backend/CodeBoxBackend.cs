
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace desktop.Helpers.Backend
{
    public class TextBoxBackend : IKeysHandler<KeyEventArgs>
    {
        TextBox txtContent;
        public List<int> actualFileSearchOccurences = null;
        public int actualSearchedResult = 0;
        TextBlock tbActualLine;
        TextBox txtSearchTerm;
        Control txtSearchInPath;
        public Dictionary<string, List<int>> founded = null;

        public TextBoxBackend(TextBox txtSearchTerm, Control txtSearchInPath, TextBlock tbActualLine, TextBox txtContent)
        {
            this.txtSearchTerm = txtSearchTerm;
            this.txtSearchInPath = txtSearchInPath;
            this.tbActualLine = tbActualLine;
            this.txtContent = txtContent;
        }

        public TextBoxBackend(TextBox txt)
        {
            this.txtContent = txt;
        }

        public bool HandleKey(KeyEventArgs e)
        {
            if (KeyboardHelper.KeyWithNoneModifier(e, Key.Space))
            {
                if (txtContent.IsFocused)
                {
                    JumpToNextSearchedResult(founded);
                    return true;
                }
            }

            if (KeyboardHelper.KeyWithModifier(e, Key.F, ModifierKeys.Control))
            {
#if DEBUG
                DebugLogger.Instance.WriteLine(KeyboardHelper.DownKey(e));
#endif
                if (!txtSearchTerm.IsFocused)
                {
                    txtSearchTerm.Focus();
                }

                return true;
            }
            if (KeyboardHelper.KeyWithModifier(e, Key.F, ModifierKeys.Control | ModifierKeys.Shift))
            {
                if (!txtSearchInPath.IsFocused)
                {
                    txtSearchInPath.Focus();
                }

                return true;
            }

            return false;
        }

        public void JumpToNextSearchedResult(Dictionary<string, List<int>> founded)
        {
            if (founded != null)
            {
                actualSearchedResult++;
                if (actualSearchedResult == actualFileSearchOccurences.Count)
                {
                    actualSearchedResult = 0;
                }

                int line = actualFileSearchOccurences[actualSearchedResult];
                TextBoxHelper.ScrollToLine(txtContent, line);


                tbActualLine.Text = (actualSearchedResult + 1) + " / " + actualFileSearchOccurences.Count;
            }
        }
    }
}
