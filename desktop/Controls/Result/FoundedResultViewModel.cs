using desktop.Controls.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

public class FoundedResultViewModel
{
    public static event Action<FoundedResultActions> Do;

    public ICommand CopyToClipboardFounded => new RelayCommand(() =>
    {
        Do(FoundedResultActions.CopyToClipboardFounded);
    });
}
