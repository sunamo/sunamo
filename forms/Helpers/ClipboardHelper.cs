//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Windows.Forms;
//namespace win
//{
//    public class ClipboardHelper 
//    {
//        public static List<string> GetLines()
//        {
//            return SH.GetLines(ClipboardHelper.GetText());
//        }

//        public static void SetText(string p)
//        {
//            if (!string.IsNullOrEmpty(p))
//            {
//                ClipboardHelper.SetText(p);
//            }
//            else
//            {
//                ClipboardHelper.SetText("SE or NULL");
//            }
//        }

//        public static void SetLines(IEnumerable p)
//        {
//            ClipboardHelper.SetText(SH.JoinString(Environment.NewLine, p));
//        }
//    }
//}
