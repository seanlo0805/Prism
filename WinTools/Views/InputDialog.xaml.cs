
using System.Windows;
using WinTools.ViewModels;

namespace WinTools.Views
{
    /// <summary>
    /// DO NOT USE THIS IN PRISM RESIONS
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog(string header)
        {
            InitializeComponent();
            this.DataContext = new InputDialogViewModel();
        }

        ~InputDialog()
        {
        }
    }
}
