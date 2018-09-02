using Windows.UI.Xaml.Controls;

public interface ISunamoAppsBrowser<T> : ISunamoBrowser<T>
{
    WebView WebView { get; set; }
    bool IsNavigating { get; set; }
    
}