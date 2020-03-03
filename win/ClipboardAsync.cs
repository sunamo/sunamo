using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows;

namespace win
{
    /// <summary>
    /// Nepoužívat, 1) celá třída vypadá jak by ji psal totální amatér. 2) havaruje mi to app a nevyhodi pritom zadnou UnhaldedException
    /// Use in ClipboardAsync and ClipboardHelperWin only System.Windows.Forms, not System.Windows which have very similar interface.
    /// </summary>
    public class ClipboardAsync 
    {

        private string _GetText;
        private void _thGetText(object format)
        {
            try
            {
                if (format == null)
                {
                    _GetText = Clipboard.GetText();
                }
                else
                {
                    _GetText = Clipboard.GetText((TextDataFormat)format);

                }
            }
            catch (Exception ex)
            {
                //Throw ex 
                _GetText = string.Empty;
            }
        }

        public string GetText()
        {
            ClipboardAsync instance = new ClipboardAsync();
            Thread staThread = new Thread(instance._thGetText);
            
            //Without STA get only empty string
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            // PRESNE TADY TO PADA
            staThread.Join();
            return instance._GetText;
        }
        public string GetText(TextDataFormat format)
        {
            ClipboardAsync instance = new ClipboardAsync();
            Thread staThread = new Thread(instance._thGetText);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start(format);
            staThread.Join();
            return instance._GetText;
        }

        private bool _ContainsText;
        private void _thContainsText(object format)
        {
            try
            {
                if (format == null)
                {
                    _ContainsText = Clipboard.ContainsText();
                }
                else
                {
                    _ContainsText = Clipboard.ContainsText((TextDataFormat)format);
                }
            }
            catch (Exception ex)
            {
                //Throw ex 
                _ContainsText = false;
            }
        }
        public bool ContainsText()
        {
            ClipboardAsync instance = new ClipboardAsync();
            Thread staThread = new Thread(instance._thContainsFileDropList);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return instance._ContainsText;
        }
        public bool ContainsText(object format)
        {
            ClipboardAsync instance = new ClipboardAsync();
            Thread staThread = new Thread(instance._thContainsFileDropList);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start(format);
            staThread.Join();
            return instance._ContainsText;
        }

        private bool _ContainsFileDropList;
        private void _thContainsFileDropList(object format)
        {
            try
            {
                _ContainsFileDropList = Clipboard.ContainsFileDropList();
            }
            catch (Exception ex)
            {
                //Throw ex 
                _ContainsFileDropList = false;
            }
        }
        public bool ContainsFileDropList()
        {
            ClipboardAsync instance = new ClipboardAsync();
            Thread staThread = new Thread(instance._thContainsFileDropList);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return instance._ContainsFileDropList;
        }

        private System.Collections.Specialized.StringCollection _GetFileDropList;
        private void _thGetFileDropList()
        {
            try
            {
                _GetFileDropList = Clipboard.GetFileDropList();
            }
            catch (Exception ex)
            {
                //Throw ex 
                _GetFileDropList = null;
            }
        }

        public System.Collections.Specialized.StringCollection GetFileDropList()
        {
            ClipboardAsync instance = new ClipboardAsync();
            Thread staThread = new Thread(instance._thGetFileDropList);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return instance._GetFileDropList;
        }
    }
}