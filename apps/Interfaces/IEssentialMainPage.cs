using apps;
using apps.Popups;
using sunamo;
using sunamo.Interfaces;
using sunamo.LoggerAbstract;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;



    // MainPage is never based from IEssentialMainPage with generic arguments
    public interface IEssentialMainPage 
{
    LogServiceAbstract<Color, StorageFile> lsg { get; }
    Task SetStatus(LogMessageAbstract<Color, StorageFile> logMessage, bool alsoLb);
}

public interface IEssentialMainPageWithLogin : IEssentialMainPage
{
    Task<bool> Dialog_ClickOK(object sender, RoutedEventArgs e);
    void Dialog_ClickCancel(object sender, RoutedEventArgs e);
    void Dialog_ClickClose(object sender, RoutedEventArgs e);
    LoginDialog loginDialog { get; set; }
    Popup popup { get; set; }
}
