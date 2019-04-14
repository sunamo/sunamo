using desktop.Helpers;
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace desktop
{
    
    public class XamlGenerator : XmlGenerator
    {
        static Type type = typeof(XamlGenerator);

        /// <summary>
        /// in outer rows
        /// in inner columns
        /// </summary>
        /// <param name="elements"></param>
        public void Grid(List<List<string>> elements)
        {
            ThrowExceptions.HaveAllInnerSameCount(type, "Grid", elements);
            int rows = elements.Count;
            int columns = elements[0].Count;

            WriteTag("Grid");

            WriteColumnDefinitions(GridHelper.ForAllTheSame(columns));
            WriteRowDefinitions(GridHelper.ForAllTheSame(rows));

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    string cell = elements[row][column];
                    cell = cell.Replace("><", $" Grid.Column=\"{column}\" Grid.Row=\"{row}\"><");
                    WriteRaw(cell);
                }
            }
            TerminateTag("Grid");
        }

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

        public void WriteColumnDefinitions(List<double> cd)
        {
            var cds = CA.ToListString(cd);
            CA.ChangeContent<string, string>(cds, SH.ReplaceOnce, AllStrings.comma, AllStrings.dot);
            WriteColumnDefinitions(cds);
        }

        /// <summary>
        /// Xaml code write to XMlGenerator, return c# code 
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="methodHandlers"></param>
        /// <returns></returns>
        public string MenuItems(List<string> headers, bool methodHandlers)
        {
            List<string> headersInPascal = new List<string>(headers.Count);

            foreach (var item in headers)
            {
                var inPascal = ConvertPascalConvention.ToConvention(item);
                headersInPascal.Add(inPascal);
                string menuItemName = "mi" + inPascal;

                string method = string.Empty;
                if (methodHandlers)
                {
                    method = "mi" + inPascal + "_Click";
                }

                WriteTagWithAttrs("MenuItem", "x:Name", menuItemName, "Header", item, "Click", method);
                TerminateTag("MenuItem");
            }

            if (methodHandlers)
            {
                CSharpGenerator csg = new CSharpGenerator();

                foreach (var item in headersInPascal)
                {
                    csg.Method(2, AccessModifiers.Internal, false, "void", "mi" + item + "_Click", "SetMode(Mode." + item + ");", "object o, RoutedEventArgs e");
                }

                return csg.ToString();
            }

            return string.Empty;
        }

        public void WriteColumnDefinitions(List<string> cd)
        {
            WriteRaw("<Grid.ColumnDefinitions>");
            foreach (var item in cd)
            {
                WriteRaw("<ColumnDefinition Width='"+ item +"'></ColumnDefinition>");
            }
            WriteRaw("</Grid.ColumnDefinitions>");
        }

        public void WriteRowDefinitions(List<double> cd)
        {
            var cds = CA.ToListString(cd);
            CA.ChangeContent<string, string>(cds, SH.ReplaceOnce, AllStrings.comma, AllStrings.dot);
            WriteRowDefinitions(cds);
        }

        public void WriteRowDefinitions(List<string> cd)
        {
            WriteRaw("<Grid.RowDefinitions>");
            foreach (var item in cd)
            {
                WriteRaw("<RowDefinition Height='" + item + "'></RowDefinition>");
            }
            WriteRaw("</Grid.RowDefinitions>");
        }

        public T GetControl<T>()
        {
            string vr = sb.ToString();
            // xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"
            vr = SH.ReplaceFirstOccurences(vr, ">", " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
            var vrR = (T)XamlReader.Parse(vr);
            return vrR;
        }    
    }
}
