using sunamo.Data;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Result
{
    /// <summary>
    /// Interaction logic for FoundedFilesUC.xaml
    /// </summary>
    public partial class FoundedFilesUC : UserControl, ISelectedT<string>
    {
        public event VoidString Selected;
        public string SelectedItem => throw new NotImplementedException();

        public FoundedFilesUC()
        {
            InitializeComponent();
        }

        public TUList<string, Brush> DefaultBrushes(string green, string red)
        {
            TUList<string, Brush> result = new TUList<string, Brush>();
            result.Add(TU<string, Brush>.Get(green, Brushes.Green));
            result.Add(TU<string, Brush>.Get(red, Brushes.Red));
            return result;
        }

        public void AddFoundedFiles(List<string> foundedList, object p)
        {
            throw new NotImplementedException();
        }

        public void ClearFoundedResult()
        {
            throw new NotImplementedException();
        }
        //
    }
}
