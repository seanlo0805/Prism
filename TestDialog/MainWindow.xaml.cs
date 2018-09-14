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
using WinTools.ViewModels;
using WinTools.Views;

namespace TestDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputDialog dlg = new InputDialog("");
            Nullable<bool> result = dlg.ShowDialog();
            if (result != null && result.Value == true)
            {
                string content = ((InputDialogViewModel)dlg.DataContext).Content;
            }

        }
    }
}
