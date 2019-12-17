
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop
{
    public class XamlGenerator : XmlGenerator
    {
        static Type type = typeof(XamlGenerator);
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
        public void MenuItems(List<string> headers, bool methodHandlers)
        {
            CA.Trim(headers);
            CA.TrimEnd(headers, AllChars.comma);

            List<string> headersInPascal = new List<string>(headers.Count);

            foreach (var item2 in headers)
            {
                var item = item2;
                string inPascal = item;

                if (!ConvertPascalConventionWithNumbers.IsPascalWithNumber(item))
                {
                    inPascal = ConvertPascalConventionWithNumbers.ToConvention(item);
                }
                else
                {
                    item = ConvertPascalConventionWithNumbers.FromConvention(item);
                }

                headersInPascal.Add(inPascal);
                string menuItemName = "mi" + inPascal;

                string method = string.Empty;
                if (methodHandlers)
                {
                    method = "mi" + inPascal + "_" + "Click";
                }

                WriteTagWithAttrs("MenuItem", "x:" + "Name", menuItemName, "Header", item, "Click", method);
                TerminateTag("MenuItem");
            }

            if (methodHandlers)
            {
                CSharpGenerator csg = new CSharpGenerator();

                foreach (var item in headersInPascal)
                {
                    csg.Method(2, AccessModifiers.Internal, false, "void", "mi" + item + "_" + "Click", "SetMode(Mode." + item + ");", "object o, RoutedEventArgs" + " " + "e");
                }

                WriteRaw(csg.ToString());

                
            }

            
        }

        public void WriteColumnDefinitions(List<string> cd)
        {
            WriteRaw("<Grid.ColumnDefinitions>");
            foreach (var item in cd)
            {
                WriteRaw("<ColumnDefinition Width='" + item + "'></ColumnDefinition>");
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
        
    }
}
