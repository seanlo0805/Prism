using System.ComponentModel.Composition;
using System.Windows;

namespace MEFTCoreApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [Export]
    public partial class MEFTCoreShell : Window
    {
        public MEFTCoreShell()
        {
            InitializeComponent();
        }
    }
}
