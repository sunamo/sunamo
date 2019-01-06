using System;
using System.Collections.Generic;
using System.Text;
using sunamo;
using sunamo.Generators.Text;
//using Glib;

namespace mono
{
	public class ClipboardHelper : IClipboardHelper
	{
		public ClipboardHelper()
		{
		}

		public void CutFiles(params string[] selected)
		{
			throw new NotImplementedException();
		}

		public void GetFirstWordOfList()
		{
			throw new NotImplementedException();s
		}

		public List<string> GetLines()
		{
			throw new NotImplementedException();
		}

		public string GetText()
		{
			//Gtk.Clipboard clipboard = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
			//var text = clipboard.WaitForText();
			return null;
		}

		public void SetLines(List<string> lines)
		{
			throw new NotImplementedException();
		}

		public void SetList(List<string> d)
		{
			throw new NotImplementedException();
		}

		public void SetText(string s)
		{
			throw new NotImplementedException();
		}

		public void SetText(TextBuilder stringBuilder)
		{
			throw new NotImplementedException();
		}

		public void SetText(StringBuilder stringBuilder)
		{
			throw new NotImplementedException();
		}

        public void SetText2(string s)
        {
            throw new NotImplementedException();
        }

        public void SetText3(string s)
        {
            throw new NotImplementedException();
        }
    }
}
