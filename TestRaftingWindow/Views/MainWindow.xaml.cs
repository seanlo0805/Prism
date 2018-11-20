using ActiproSoftware.Windows.Controls.Docking;
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
using TestRaftingWindow.Interfaces;

namespace TestRaftingWindow.Views
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
        protected override void OnInitialized(EventArgs e)
        {
            dockSite.WindowClosed += mDockMgr_WindowClosed;
            base.OnInitialized(e);
        }
        void mDockMgr_WindowClosed(object sender, DockingWindowEventArgs e)
        {
            IToolWindowContentControl ctrl = e.Window.Content as IToolWindowContentControl;
            if (ctrl != null)
            {
                dockSite.ToolWindows.Remove(ctrl.ParentWidnow);
                //ctrl.ParentWidnow.Destroy(true);
                //ctrl.ParentWidnow.Content = null;
                ctrl.OnWindowClose();
            }

        }
    }
}
