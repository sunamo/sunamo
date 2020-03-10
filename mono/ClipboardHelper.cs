using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using sunamo;
using sunamo.Generators.Text;
//using Glib;

namespace mono
{
	public class ClipboardHelper : IClipboardHelper
	{
		static Type type = typeof(ClipboardHelper);

		public ClipboardHelper()
		{
		}

        public bool ContainsText()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
			return false;
        }

        public void CutFiles(params string[] selected)
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		}

		public void GetFirstWordOfList()
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		}

		public List<string> GetLines()
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
			return null;
		}

		public string GetText()
		{
			//Gtk.Clipboard clipboard = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
			//var text = clipboard.WaitForText();
			return null;
		}

		public void SetLines(List<string> lines)
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		}

        public void SetLines(IEnumerable lines)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public void SetList(List<string> d)
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		}

		public void SetText(string s)
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		}

		//public void SetText(TextBuilder stringBuilder)
		//{
		//	ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		//}

		public void SetText(StringBuilder stringBuilder)
		{
			ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
		}

        public void SetText2(string s)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public void SetText3(string s)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }
    }
}