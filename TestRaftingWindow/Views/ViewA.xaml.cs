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
using ActiproSoftware.Windows.Controls.Docking;
using TestRaftingWindow.Interfaces;

namespace TestRaftingWindow.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : UserControl, IToolWindowContentControl
    {
        private static object _seqLocker = new object();
        private static int _seq_ = 0;

        private byte[] bigarray = new byte[256 * 1024 * 1024];
        private string content;
        Action<string> OnMsg;

        private ToolWindow _parentWindow;
        public ViewA(ToolWindow parentWindow)
        {
            _parentWindow = parentWindow;
            InitializeComponent();
        }

        public ToolWindow ParentWidnow { get  { return _parentWindow; }  set  { _parentWindow = value; } }

        public void OnWindowClose()
        {

        }
        ~ViewA()
        {

        }
    }
}
