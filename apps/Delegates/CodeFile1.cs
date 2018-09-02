using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

public delegate void VoidColor(Color c);
public delegate void VoidStorageFile(StorageFile c);
public delegate void VoidStorageFileBitmapImage(StorageFile sf, BitmapImage bi);
public delegate void VoidStorageFileBitmapImageIBuffer(StorageFile sf, BitmapImage bi, IBuffer ib);