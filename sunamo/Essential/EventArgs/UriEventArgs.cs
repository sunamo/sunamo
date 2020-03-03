using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public delegate void UriEventHandler(object sender, UriEventArgs e);

    public class UriEventArgs : EventArgs
    {
        private Uri _uri = null;

        public UriEventArgs(Uri uri)
        {
            _uri = uri;
        }

        public Uri Uri { get { return _uri; } }
    }
}