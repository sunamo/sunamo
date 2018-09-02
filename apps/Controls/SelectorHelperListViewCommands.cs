using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace apps
{
    public class SelectorHelperListViewCommands
    {
        public RemoveOneCommand RemoveOneCmd
        {
            get
            {
                return new RemoveOneCommand();
            }
        }

        public SaveToClipboardCommand SaveToClipboardCmd
        {
            get
            {
                return new SaveToClipboardCommand();
            }
        }

        public RunOneCommand RunOneCmd
        {
            get
            {
                return new RunOneCommand();
            }
        }

        public class RemoveOneCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public async void Execute(object parameter)
            {
                SelectorHelperItem dc = (SelectorHelperItem)parameter;

                await dc.sh.RemoveOne(dc.Id);
            }
        }

        public class SaveToClipboardCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public async void Execute(object parameter)
            {
                SelectorHelperItem dc = (SelectorHelperItem)parameter;

                await dc.sh.SaveToClipboard(dc.Id);
            }
        }

        public class RunOneCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public async void Execute(object parameter)
            {
                SelectorHelperItem dc = (SelectorHelperItem)parameter;

                await dc.sh.RunOne(dc.Id);
            }
        }
    }
}
