using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators.Text
{
    public class TextBuilder 
    {
        int lastIndex = -1;
        string lastText = "";
        public StringBuilder sb = new StringBuilder();
        public string prependEveryNoWhite = string.Empty;

        bool canUndo = false;

        public bool CanUndo
        {
            get
            {
                return canUndo;
            }
            set
            {
                canUndo = value;
                if (!value)
                {
                    lastIndex = -1;
                    lastText = "";
                }
            }
        }

        public void Undo()
        {
            if (lastIndex != -1)
            {
                sb.Remove(lastIndex, lastText.Length);
            }
        }

        public void Append(string s)
        {
            SetUndo(s);
            sb.Append(prependEveryNoWhite);
            sb.Append( s);
        }

        private void SetUndo(string text)
        {
            if (CanUndo)
            {
                lastIndex = sb.Length;
                lastText = text;
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
            sb.Append(Environment.NewLine);
        }

        public void AppendLine(string s)
        {
            SetUndo(s);
            sb.Append(prependEveryNoWhite);
            sb.AppendLine(s);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
