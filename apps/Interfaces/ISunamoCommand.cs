using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace apps
{
    public interface ISunamoAsyncCommandBase
    {
        void RaiseCanExecuteChanged(bool canExecute);

    }

    public interface ISunamoAsyncCommand : ISunamoAsyncCommandBase //: ICommand
    {
         Task<bool> CanExecute(object parameter);

        void Execute(object parameter);
    }

    public interface ISunamoAsyncCommandRouted : ISunamoAsyncCommandBase //: ICommand
    {


        void Execute(object parameter, RoutedEventArgs ea);
    }

    public interface ISunamoCommand : ICommand
    {
        void RaiseCanExecuteChanged(bool canExecute);
        void UpdateAssociatedControls();
    }
}
