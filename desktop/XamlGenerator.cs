using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace desktop
{
    
    public class XamlGenerator<T> : XmlGenerator
    {
        public void WriteDataTemplate(List<double> cd)
        {
            WriteRaw(@"<DataTemplate><Grid>");

            WriteColumnDefinitions(cd);
            WriteRaw(@"<TextBlock Text='{Binding Channel}'  Grid.Column='0' Width='{Binding Path=_0, Source={StaticResource SizeColumnsVideosListView}, Mode=TwoWay}' MaxWidth='{Binding Path=_0, Source={StaticResource SizeColumnsVideosListView}, Mode=TwoWay}'></TextBlock>
                                        <TextBlock Text='{Binding Title}' Grid.Column='1' Width='{Binding Path=_1, Source={StaticResource SizeColumnsVideosListView}, Mode=OneWay}'></TextBlock>
                                        <TextBlock Grid.Column='2' Text='{Binding Extension}' Width='{Binding Path=_2, Source={StaticResource SizeColumnsVideosListView}, Mode=OneWay}'></TextBlock>
                                        <TextBlock Grid.Column='3' Text='{Binding YtCode}' Width='{Binding Path=_3, Source={StaticResource SizeColumnsVideosListView}, Mode=OneWay}'></TextBlock>
                                    </Grid>
                                </DataTemplate>");
        }

        private void WriteColumnDefinitions(List<double> cd)
        {
            WriteRaw("<Grid.ColumnDefinitions>");
            foreach (var item in cd)
            {
                WriteRaw("<ColumnDefinition Width='"+ item.ToString().Replace(',', '.') +"'></ColumnDefinition>");
            }
            WriteRaw("</Grid.ColumnDefinitions>");
        }

        public T GetControl()
        {
            string vr = sb.ToString();
            // xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"
            vr = SH.ReplaceFirstOccurences(vr, ">", " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
            var vrR = (T)XamlReader.Parse(vr);
            return vrR;
        }    
    }
}
