using desktop.Data;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace desktop.Helpers.Backend
{
    /// <summary>
    /// Have both event and TextBox - more variable
    /// </summary>
    public class TextBoxBackend : IKeysHandler<KeyEventArgs>
    {
        static Type type = typeof(TextBoxBackend);
        // Menu, ToolBar and tbLineBreak = 67 lines. Should be changed in every App
        //public int addLinesInEveryScroll = 67;


        public int actualSearchedResult = -1;
        public SearchCodeElementsUCData searchCodeElementsUCData = null;

        public event VoidInt ScrollToLine;
        public event VoidVoid EndOfFilteredLines;

        public List<int> actualFileSearchOccurences
        {
            get
            {
                return searchCodeElementsUCData.actualFileSearchOccurences;
            }
        }

                /// <summary>
                /// Line to which was last time scrolled
                /// </summary>
                int _actualLine = 0;

        public int actualLine
        {
            set
            {
                _actualLine = value;
                TextBoxHelper.ScrollToLine(txtContent, value);
            }
            get
            {
                return _actualLine;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="txtTextBoxState"></param>
        /// <param name="txtContent"></param>
        public TextBoxBackend(TextBlock txtTextBoxState, TextBox txtContent)
        {
            this.txtTextBoxState = txtTextBoxState;
            this.txtContent = txtContent;
            // Is changed also when just moved cursor (mouse, arrows)
            txtContent.SelectionChanged += TxtContent_SelectionChanged;
        }

        private void TxtContent_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            //SetActualLine( txtContent.GetLineIndexFromCharacterIndex(txtContent.SelectionStart));
        }

        

        public bool HandleKey(KeyEventArgs e)
        {    
            return false;
        }

        TextBlock txtTextBoxState;
        protected TextBox txtContent;
        public TextBoxState state = new TextBoxState();

        public void SetTbSearchedResult(int actual, int count)
        {
            state.textSearchedResult = $"{actual}/{count}";
            SetTextBoxState();
            
        }

        public void SetActualFile(string file)
        {
            state.textActualFile = "File: " + file;
            SetTextBoxState();
        }

        public void SetActualLine(int line)
        {
            _actualLine = line++;
            state.textActualFile = "Line: " + _actualLine;
            SetTextBoxState();
        }

        private void SetTextBoxState()
        {
            txtTextBoxState.Text = SH.Join("  ", state.textActualFile, state.textSearchedResult).Trim();
        }

        public void JumpToNextSearchedResult()
        {
            var data = searchCodeElementsUCData;
            if (actualFileSearchOccurences.Count == 0)
            {
                SetTbSearchedResult(0, 0);
            }
            else
            {
                if (actualSearchedResult == actualFileSearchOccurences.Count)
                {
                    if (EndOfFilteredLines != null)
                    {
                        EndOfFilteredLines();
                    }
                    
                    actualSearchedResult = 0;
                }

                int serie = actualSearchedResult + 1;
                SetTbSearchedResult(serie, actualFileSearchOccurences.Count);
                
                ScrollToLineMethod(actualFileSearchOccurences[ actualSearchedResult]);
                
                actualSearchedResult++;
            }
        }

        void ScrollToLineMethod(int line)
        {
            if (ScrollToLine != null)
            {
                ScrollToLine(line);
            }
            if (txtContent != null)
            {
                TextBoxHelper.ScrollToLine(txtContent, line);
            }
            
        }

        public void ScrollAboutLines(int v)
        {
            v -= 1; 
            v = v * 2;
            //v -= 1;
            int newLine = actualLine + v;
            int countLines = SH.CountLines(txtContent.Text);
            if (newLine > countLines)
            {
                ScrollToLineMethod(countLines);
            }
            else
            {
                ScrollToLineMethod(newLine);
                ScrollToLineMethod(v / 2);
            }
        }
    }
}
