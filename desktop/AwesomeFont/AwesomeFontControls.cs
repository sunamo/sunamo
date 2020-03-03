
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.AwesomeFont
{
    /// <summary>
    /// For WPF app is necessary *.ttf/.otf with build action Resource.
    /// For UWP is needed*.otf with build action Content.
    /// </summary>
    public static class AwesomeFontControls
    {
        public static async Task SetAwesomeFontSymbol(Button txtSearchIcon, string v)
        {
            await WpfApp.cd.InvokeAsync(() =>
            {
                txtSearchIcon.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#FontAwesome");
                txtSearchIcon.Content = v;
            });
        }
         
        public static async Task SetAwesomeFontSymbol(TextBlock txtSearchIcon, string v)
        {
            await WpfApp.cd.InvokeAsync(() =>
            {
                txtSearchIcon.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#FontAwesome");
                txtSearchIcon.Text = v;
            });
        }
    }
}