using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators.Text
{
    /// <summary>
    /// In Comparing
    /// </summary>
    public class TextBuilder
    {
        private static Type type = typeof(TextBuilder);

        private bool _canUndo = false;
        private int _lastIndex = -1;
        private string _lastText = "";
        public StringBuilder sb = null;
        public string prependEveryNoWhite = string.Empty;
        /// <summary>
        /// For PowershellRunner
        /// </summary>
        public List<string> list = null;
        private bool _useList = false;

        public void Clear()
        {
            if (_useList)
            {
                list.Clear();
            }
            else
            {
                sb.Clear();
            }
        }

        public TextBuilder(bool useList = false)
        {
            _useList = useList;
            if (useList)
            {
                list = new List<string>();
            }
            else
            {
                sb = new StringBuilder();
            }
        }

        public bool CanUndo
        {
            get
            {
                if (_useList)
                {
                    return false;
                }
                return _canUndo;
            }
            set
            {
                _canUndo = value;
                if (!value)
                {
                    _lastIndex = -1;
                    _lastText = "";
                }
            }
        }

        private void UndoIsNotAllowed(string method)
        {
            ThrowExceptions.IsNotAllowed(Exc.GetStackTrace(),type, method, SunamoPageHelperSunamo.i18n(XlfKeys.Undo));
        }

        public void Undo()
        {
            if (_useList)
            {
                UndoIsNotAllowed(SunamoPageHelperSunamo.i18n(XlfKeys.Undo));
            }
            if (_lastIndex != -1)
            {
                sb.Remove(_lastIndex, _lastText.Length);
            }
        }

        public void Append(string s)
        {
            if (_useList)
            {
                CA.AppendToLastElement(list, s);
            }
            else
            {
                SetUndo(s);
                sb.Append(prependEveryNoWhite);
                sb.Append(s);
            }
        }

        private void SetUndo(string text)
        {
            if (_useList)
            {
                UndoIsNotAllowed("SetUndo");
            }
            if (CanUndo)
            {
                _lastIndex = sb.Length;
                _lastText = text;
            }
        }

        public void Append(object s)
        {
            string text = s.ToString();
            SetUndo(text);
            Append(text);
        }

        public void AppendLine()
        {
            Append(Environment.NewLine);
        }

        public void AppendLine(string s)
        {
            if (_useList)
            {
                list.Add(prependEveryNoWhite + s);
            }
            else
            {
                SetUndo(s);
                sb.Append(prependEveryNoWhite + s + Environment.NewLine);
            }
        }

        /// <summary>
        /// If is use List, join it with NL.
        /// Otherwise return sb
        /// </summary>
        public override string ToString()
        {
            if (_useList)
            {
                return SH.JoinNL(list);
            }
            else
            {
                return sb.ToString();
            }
        }
    }
}