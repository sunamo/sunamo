using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
public static class ToolbarHelper
{
    public static void AddButton(ToolBar tsAkce, ImageSource imageOnButton, string tooltip, CommandBinding commandBinding, int whImage, int whButton)
    {
        Button button = new Button();
        button.Width = whButton;
        button.Height = whButton;
        button.ToolTip = tooltip;
        Image image = new Image(); //ImageHelper.ReturnImage(UnknownCacheImage);
        image.Source = imageOnButton;
        image.Width = whImage;
        image.Height = whImage;
        button.Content = image;
        button.Command = commandBinding.Command;
        tsAkce.Items.Add(button);
    }

    public static void AddButton(ToolBar tsAkce, ImageSource imageOnButton, string tooltip, RoutedEventHandler handler, int whImage, int whButton)
    {
        Button button = new Button();
        AddButton(tsAkce, imageOnButton, tooltip, handler, whImage, whButton, button);
    }

    public static void AddToggleButton(ToolBar tsAkce, ImageSource imageOnButton, string tooltip, RoutedEventHandler handler, int whImage, int whButton, bool isChecked)
    {
        ToggleButton button = new ToggleButton();
        button.IsChecked = isChecked;
        AddButton(tsAkce, imageOnButton, tooltip, handler, whImage, whButton, button);
    }

    private static void AddButton(ToolBar tsAkce, ImageSource imageOnButton, string tooltip, RoutedEventHandler handler, int whImage, int whButton, ButtonBase button)
    {
        button.Width = whButton;
        button.Height = whButton;
        button.ToolTip = tooltip;
        Image image = new Image();  //ImageHelper.ReturnImage(UnknownCacheImage);
        image.Source = imageOnButton;
        image.Width = whImage;
        image.Height = whImage;
        button.Content = image;
        button.Click += handler;
        tsAkce.Items.Add(button);
    }
}