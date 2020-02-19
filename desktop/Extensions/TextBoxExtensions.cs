
using System.Windows.Controls;
using System.Windows.Media;
using sunamo;

public static partial class TextBoxExtensions
{
    public static void CreateTagIfNotExists(this TextBox txt)
    {
        if (txt.Tag == null )
        {
            txt.Tag = new TextBoxTag();
        }
    }

    public static TextBoxTag GetTag(this TextBox txt)
    {
        return txt.Tag as TextBoxTag;
    }

    public static void AddPlaceholder(this TextBox txt, string placeholder)
    {
        txt.CreateTagIfNotExists();

        txt.GetTag().placeholder = placeholder;

        Txt_TextChanged(txt, null);

        txt.GotFocus += Txt_GotFocus;
        txt.TextChanged += Txt_TextChanged;
        txt.PreviewKeyDown += Txt_PreviewKeyDown;
        txt.MouseDown += Txt_MouseDown;
        txt.LostFocus += Txt_LostFocus;
    }

    private static void Txt_LostFocus(object sender, System.Windows.RoutedEventArgs e)
    {
        keyPress = false;
    }

    private static void Txt_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Txt_GotFocus(sender, null);
    }

    static bool keyPress = false;

    private static void Txt_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (!KeyboardHelper.IsWhitespace(e))
        {
            keyPress = true;
        }
        else
        {
            keyPress = false;
        }
        
        Txt_GotFocus(sender, null);
    }

    private static void Txt_TextChanged(object sender, TextChangedEventArgs e)
    {
        var txt = (TextBox)sender;
        var tag = (TextBoxTag)txt.Tag;

        if (!keyPress)
        {
            if (txt.Text == string.Empty)
            {
                if (!unsetPlaceholder)
                {

                    txt.Text = tag.placeholder;
                    txt.Foreground = Brushes.DarkGray;
                }
                else
                {
                    unsetPlaceholder = false;
                }
            }
        }
    }

    static bool unsetPlaceholder = false;

    private static void Txt_GotFocus(object sender, System.Windows.RoutedEventArgs e)
    {
        var txt = (TextBox)sender;
        var tag = (TextBoxTag)txt.Tag;

        if (txt.Text == tag.placeholder)
        {
            unsetPlaceholder = true;

            txt.Foreground = Brushes.Black;
            txt.Text = string.Empty;

            
        }
        else if (txt.Text == string.Empty)
        {
            Txt_TextChanged(sender, null);
        }
        //
    }
}