using System.Drawing;
using System.Windows.Media.Imaging;

/* 
desktop/Delegates/CodeFile1.cs
*/

public delegate void VoidWpfColor(System.Windows.Media.Color c);

#region Mono
public delegate void VoidStringBitmapBitmapSource(string s, Bitmap t, BitmapSource u);
public delegate void VoidStringBitmapBitmapImage(string s, Bitmap t, BitmapImage u);
#endregion