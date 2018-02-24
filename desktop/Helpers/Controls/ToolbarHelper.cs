using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
public static class ToolbarHelper
{
    public static void AddButton(ToolBar tsAkce, ImageSource imageOnButton, string p, CommandBinding commandBinding, int whImage, int whButton)
    {
        Button button = new Button();
        button.Width = whButton;
        button.Height = whButton;
        button.ToolTip = p;
        Image image = new Image(); //ImageHelper.ReturnImage(UnknownCacheImage);
        image.Source = imageOnButton;
        image.Width = whImage;
        image.Height = whImage;
        button.Content = image;
        button.Command = commandBinding.Command;
        tsAkce.Items.Add(button);
    }

    public static void AddButton(ToolBar tsAkce, ImageSource imageOnButton, string p, RoutedEventHandler handler, int whImage, int whButton)
    {
        Button button = new Button();
        button.Width = whButton;
        button.Height = whButton;
        button.ToolTip = p;
        Image image = new Image();  //ImageHelper.ReturnImage(UnknownCacheImage);
        image.Source = imageOnButton;
        image.Width = whImage;
        image.Height = whImage;
        button.Content = image;
        button.Click += handler;
        tsAkce.Items.Add(button);
    }
}
