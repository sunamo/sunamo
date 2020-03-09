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
		public ClipboardHelper()
		{
		}

        public bool ContainsText()
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public void CutFiles(params string[] selected)
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

		public void GetFirstWordOfList()
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

		public List<string> GetLines()
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

		public string GetText()
		{
			//Gtk.Clipboard clipboard = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
			//var text = clipboard.WaitForText();
			return null;
		}

		public void SetLines(List<string> lines)
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

        public void SetLines(IEnumerable lines)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public void SetList(List<string> d)
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

		public void SetText(string s)
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

		//public void SetText(TextBuilder stringBuilder)
		//{
		//	ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		//}

		public void SetText(StringBuilder stringBuilder)
		{
			ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
		}

        public void SetText2(string s)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public void SetText3(string s)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }
    }
}