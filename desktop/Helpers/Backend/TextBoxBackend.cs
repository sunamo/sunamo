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
        static Type type = typeof(TextBoxBackend);
        public Dictionary<string, List<int>> founded = null;
        public List<int> actualFileSearchOccurences;
        public int actualSearchedResult = -1;

        public event VoidInt ScrollToLine;
        public event VoidVoid EndOfFilteredLines;

        public TextBoxBackend(TextBox txtSearchTerm, ComboBox txtSearchInPath, TextBlock tbActualLine, TextBox txtContent)
        {
            ThrowExceptions.NotImplementedCase(type, "TextBoxBackend");
        }

        public bool HandleKey(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (ScrollToLine != null)
                {
                    if (actualFileSearchOccurences.Count > 0)
                    {
                        actualSearchedResult++;
                        if (actualSearchedResult == actualFileSearchOccurences.Count)
                        {
                            EndOfFilteredLines();
                            actualSearchedResult = 0;
                        }

                        ScrollToLine(actualSearchedResult);
                    }
                }
                return true;
            }
            return false;
        }

        public void JumpToNextSearchedResult(Dictionary<string, List<int>> founded)
        {
            throw new NotImplementedException();
        }
    }
}
