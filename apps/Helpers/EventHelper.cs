using System.Windows;
using Windows.UI.Xaml;

public static class EventHelper
{

    public static T GetGetRightSource<T>(object sender, RoutedEventArgs ea)
    {
        if (sender.GetType() == typeof(T))
        {
            return (T)sender;
        }
        if (ea.OriginalSource.GetType() == typeof(T))
        {
            return (T)ea.OriginalSource;
        }
        //if (ea.Source.GetType() == typeof(T))
        //{
        //    return (T)ea.Source;
        //}
        return default(T);
    }
}