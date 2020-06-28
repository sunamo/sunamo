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
    public class TextBoxBackend : IKeysHandler, IShowSearchResults
    {
        static Type type = typeof(TextBoxBackend);
        // Menu, ToolBar and tbLineBreak = 67 lines. Should be changed in every App
        //public int addLinesInEveryScroll = 67;


        public int actualSearchedResult = -1;
        public SearchCodeElementsUCData searchCodeElementsUCData = null;

        public event VoidInt ScrollToLine;
        public event VoidVoid EndOfFilteredLines;

        public List<FoundedCodeElement> actualFileSearchOccurences
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
        int addRowsDuringScrolling = 0;

        /// <summary>
        /// A3 = Consts.addRowsToCodeTextBoxDuringScrolling
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="tbTextBoxState"></param>
        /// <param name="txtContent"></param>
        public TextBoxBackend(TextBlock tbTextBoxState, TextBox txtContent, int addRowsDuringScrolling)
        {
            this.txtTextBoxState = tbTextBoxState;
            this.txtContent = txtContent;
            this.addRowsDuringScrolling = addRowsDuringScrolling;
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

        

        public void SetActualFile(string file)
        {
            state.textActualFile = sess.i18n(XlfKeys.File) + ": " + file;
            SetTextBoxState();
        }

        public void SetActualLine(int line)
        {
            _actualLine = line++;
            state.textActualFile = sess.i18n(XlfKeys.Line) + ": " + _actualLine;
            SetTextBoxState();
        }

        public void SetTbSearchedResult(int actual, int count)
        {
            state.textSearchedResult = $"{actual}/{count}";
            SetTextBoxState();

        }

        public void SetTextBoxState(string s = null)
        {
            if (s == null)
            {
                s = (SH.Join(AllStrings.doubleSpace, state.textActualFile, state.textSearchedResult) + " " + sess.i18n(XlfKeys.Line) + ": " + (actualLine + 1)).Trim();
            }
            txtTextBoxState.Text = s;
        }

        public void JumpToNextSearchedResult(int addLines)
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
                
                ScrollToLineMethod(actualFileSearchOccurences[ actualSearchedResult].Line, addRowsDuringScrolling);
                
                actualSearchedResult++;
            }
        }

        /// <summary>
        /// A2 to full number of showing rows at one time.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="addLines"></param>
        public void ScrollToLineMethod(int line, int addLines)
        {
            if (txtContent.LineCount > addLines && line > addLines)
            {
                // -4 due to excepiton on txt.GetCharacterIndexFromLineIndex(line); - line was 244, but has only 243 lines
                // commented, one is sure, condition txtContent.LineCount > addLines && line > addLines is wrong due to highlight wrong line (225 instead od 160)
                //line += addLines - 2;
            }

            if (ScrollToLine != null)
            {
                ScrollToLine(line);
            }
            actualLine = line;
            if (txtContent != null)
            {
                TextBoxHelper.ScrollToLine(txtContent, line);
            }
            ThisApp.SetStatus(TypeOfMessage.Information, sess.i18n(XlfKeys.ScrolledToLine) + " " + line);
            SetTextBoxState();
        }

        public void ScrollAboutLines(int v)
        {
            //v -= 1; 
            //v = v * 2;
            //v -= 1;
            int newLine = actualLine + v;
            int countLines = SH.CountLines(txtContent.Text);
            if (newLine > countLines)
            {
                ScrollToLineMethod(countLines, addRowsDuringScrolling);
            }
            else
            {
                ScrollToLineMethod(newLine, addRowsDuringScrolling);
            }
        }

        /// <summary>
        /// Must be called in Loaded or after
        /// </summary>
        /// <param name="from"></param>
        /// <param name="length"></param>
        public void Highlight(int from, int length)
        {
            if (from != -1)
            {
                txtContent.Focus();
                txtContent.Select(from, length);
            }
            
        }

        /// <summary>
        /// Must be called in Loaded or after
        /// A1 -1, because highlighting can be processed only after and index was already increment
        /// </summary>
        public void Highlight(int actualSearchedResult)
        {
            var r = searchCodeElementsUCData.actualFileSearchOccurences[actualSearchedResult];
            Highlight(r.From, r.Lenght);
        }
    }
}