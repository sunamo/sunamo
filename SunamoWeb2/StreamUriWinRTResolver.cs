using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web;

public sealed class StreamUriWinRTResolver : IUriToStreamResolver
{
    public Stream UriToStream(Uri uri)
    {
        var r= UriToStreamAsync(uri).GetResults();
        var t = r.GetType();
        var s = (Stream)r;
        return s;
    }

    public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri)
    {
        if (uri == null)
        {
            throw new Exception();
        }
        string path = uri.AbsolutePath;

        // Because of the signature of the this method, it can't use await, so we 
        // call into a separate helper method that can use the C# await pattern.
        return GetContent(path).AsAsyncOperation();
    }

    private async Task<IInputStream> GetContent(string path)
    {
        // We use a package folder as the source, but the same principle should apply
        // when supplying content from other locations
        try
        {
            Uri localUri = new Uri("ms-appx:///html" + path);
            StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(localUri);
            IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);
            return stream;
        }
        catch (Exception) { throw new Exception("Invalid path"); }
    }
}