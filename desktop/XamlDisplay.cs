
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace desktop
{
    public interface IXamlDisplay
    {

    }

    public class XamlDisplay
    {
        

        public static ItemsPanelTemplate GetItemsPanelTemplate()
        {
            string xaml = @"<ItemsPanelTemplate   xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                            <Grid>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition />
                                </Grid.ColumnDefinitions>
                                <Grid.RowDefinitions>
                                    <RowDefinition />
                                </Grid.RowDefinitions>
                            </Grid>
                    </ItemsPanelTemplate>";
            return XamlReader.Parse( xaml) as ItemsPanelTemplate;
        }
    }

}
