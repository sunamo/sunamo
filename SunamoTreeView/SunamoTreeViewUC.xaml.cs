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

namespace SunamoTreeView
{
    /// <summary>
    /// I have 3 TreeViews:
    /// desktop.Controls.FolderContentsTreeView - used in AllProjectsSearch\Wins\DuplicateSolutionsWindow.xaml. With icons but without checkboxes
    /// SunamoTreeView - very nice, load whole fs structure, example in OptimalAllocationSizeUnit. Without icons but with checkboxes
    /// HostingManagerTreeView - Pracuje s DB a FolderEntryDB/FileEntryDB
    /// </summary>
    public partial class SunamoTreeViewUC : UserControl
    {
        public SunamoTreeViewUC()
        {
            InitializeComponent();
        }
    }
}