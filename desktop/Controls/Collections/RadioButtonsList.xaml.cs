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

namespace desktop.Controls.Collections
{
    /// <summary>
    /// Interaction logic for RadioButtonsList.xaml
    /// </summary>
    public partial class RadioButtonsList : UserControl, IUserControl, IControlWithResultDebug
    {
        public bool? DialogResult { set
            {
                if (ChangeDialogResult != null)
                {
                    ChangeDialogResult(value);
                }
            }
        }

        public string Title => string.Empty;
        public event VoidBoolNullable ChangeDialogResult;

        public RadioButtonsList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Must be ControlInitData[], not object[]
        /// 
        /// </summary>
        /// <param name="contents"></param>
        public void AddRadioButtons(bool addHandler, params ControlInitData[] contents)
        {
            foreach (var content in contents)
            {
                if (addHandler)
                {
                    content.OnClick = RbClicked;
                }
                AddRadioButton(content);
            }
        }

        public void AddRadioButton(ControlInitData d)
        {
            spRbs.Children.Add(RadioButtonHelper.Get(d));
        }

        public object clickedTag = null;

        public void RbClicked(object o, RoutedEventArgs e)
        {
            var rb = (RadioButton)o;
            clickedTag = rb.Tag;
            ChangeDialogResult(true);
        }

        /// <summary>
        /// A1 should be always true
        /// </summary>
        /// <param name="addHandlers"></param>
        /// <param name="all"></param>
        public void Init(bool addHandlers, List<string> all)
        {
            foreach (var item in all)
            {
                var d = new ControlInitData { content = item, tag = item, group = "g" };
                if (addHandlers)
                {
                    d.OnClick = RbClicked;
                }

                AddRadioButton(d) ;
            }
        }

        public int CountOfHandlersChangeDialogResult()
        {
            return RuntimeHelper.GetInvocationList(ChangeDialogResult).Count;
        }

        public void AttachChangeDialogResult(VoidBoolNullable a, bool throwException = true)
        {
            RuntimeHelper.AttachChangeDialogResult(this, a, throwException);
        }

        public void Accept(object input)
        {
            
        }

        public void FocusOnMainElement()
        {
            
        }

        public void Init()
        {
        }
    }
}