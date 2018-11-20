using ActiproSoftware.Windows.Controls.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestRaftingWindow.Commands;
using TestRaftingWindow.Views;

namespace TestRaftingWindow.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChangedEvent(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ICommand OpenRaftingWindowA { get { return new DelegateCommand(OpenViewA, CanIOpenViewA); } }


        public MainWindowViewModel()
        {

        }

        private void OpenViewA(object parameter)
        {
            DockSite dockMgr;
            if (parameter is DockSite)
            {
                dockMgr = (DockSite)parameter;

                ToolWindow toolwindows = new ToolWindow(true);
                //ToolWindow window = new ToolWindow(dockMgr, name, title,
                //    new BitmapImage(new Uri("/Resources/Images/TextDocument16.png", UriKind.Relative)), textBox);
                toolwindows.Title = "ViewA";
                //toolwindows.Title = LanguageSupport.GetString("Menu_lite_quick_orders");
                //Guid newGUID = Guid.NewGuid();
                UserControl ctrl = new ViewA(toolwindows);
                //ctrl.SetStyle();
                //WindowsManager.GetInstance().DictionaryFlashOrderControlToolWindows.Add(newGUID, toolwindows);
                ctrl.Width = Double.NaN;
                ctrl.Height = Double.NaN;
                ctrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                ctrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                toolwindows.Content = ctrl;
                toolwindows.Unloaded += (object sender, RoutedEventArgs e) => {
                    if (sender is ToolWindow win)
                        //if (win.DockHost == null) // window is leaving
                        dockMgr.ToolWindows.Remove(win);
                };

                ctrl.Tag = Guid.NewGuid().ToString();
                //ctrl.SetParentWindow(toolwindows);
                dockMgr.ToolWindows.Add(toolwindows);
                System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
                Point pt2 = new Point((pt.X - 32), (pt.Y + 8));
                toolwindows.Float(pt2, new Size(300, 300));
                //ctrl.SetTitle(LanguageSupport.GetString("Menu_lite_quick_orders"));
                //ctrl.InitialUI();
            }

        }
        
        private bool CanIOpenViewA()
        {
            return true;
        }
    }
}
