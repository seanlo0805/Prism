using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace UnityDialogRepeatApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //minimizing MainWindow https://ebbypeter.wordpress.com/2010/06/28/minimize-a-wpf-application-to-system-tray-in-c/
        //                      http://www.a2zmenu.com/Blogs/Silverlight/WPF-Hide-minimize-button-of-a-window.aspx
        //                      https://blog.magnusmontin.net/2014/11/30/disabling-or-hiding-the-minimize-maximize-or-close-button-of-a-wpf-window/
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;

        private const int WS_MAXIMIZEBOX = 0x10000; //maximize button
        private const int WS_MINIMIZEBOX = 0x20000; //minimize button
        private IntPtr _windowHandle;

        public System.Windows.Forms.NotifyIcon NotifyIcon
        {
            get { return _notifyIcon; }
        }

        public MainWindow()
        {
            InitializeComponent();

            //https://docs.microsoft.com/zh-tw/dotnet/api/system.windows.forms.notifyicon.icon?redirectedfrom=MSDN&view=netframework-4.7.2#System_Windows_Forms_NotifyIcon_Icon
            //https://docs.microsoft.com/zh-tw/dotnet/framework/winforms/controls/app-icons-to-the-taskbar-with-wf-notifyicon
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon(@"TaskBarIcon.ico");
            _notifyIcon.MouseDoubleClick +=
                            new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDoubleClick);

            this.SourceInitialized += MainWindow_SourceInitialized;
        }

        void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _windowHandle = new WindowInteropHelper(this).Handle;

            //disable minimize button
            //DisableMinimizeButton();

            //disable maximize button
            DisableMaximizeButton();
        }

        protected void DisableMinimizeButton()
        {
            if (_windowHandle == IntPtr.Zero)
                throw new InvalidOperationException("The window has not yet been completely initialized");

            SetWindowLong(_windowHandle, GWL_STYLE, GetWindowLong(_windowHandle, GWL_STYLE) & ~WS_MINIMIZEBOX);
        }

        protected void DisableMaximizeButton()
        {
            if (_windowHandle == IntPtr.Zero)
                throw new InvalidOperationException("The window has not yet been completely initialized");

            SetWindowLong(_windowHandle, GWL_STYLE, GetWindowLong(_windowHandle, GWL_STYLE) & ~WS_MAXIMIZEBOX);
        }
    }
}
