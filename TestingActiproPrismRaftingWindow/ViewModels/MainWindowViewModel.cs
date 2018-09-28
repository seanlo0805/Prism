using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TestingActiproPrismRaftingWindow.Views.Docking;

namespace TestingActiproPrismRaftingWindow.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private DockSiteLayoutSerializer layoutSerializer = new DockSiteLayoutSerializer();
        private string layoutXmlFile = @".\Actiprosoft.xml";
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand OpenRaftingWindowA { get { return new DelegateCommand<DockSite>(OpenViewA); } }
        public ICommand OpenRaftingWindowB { get { return new DelegateCommand<DockSite>(OpenViewB); } }
        public ICommand SaveLayoutToXml { get { return new DelegateCommand<DockSite>(SaveLayout); } }
        public ICommand LoadLayoutFromXml { get { return new DelegateCommand<DockSite>(LoadLayout); } }


        public MainWindowViewModel()
        {

        }

        private void SaveLayout(DockSite dockMgr)
        {
            layoutSerializer.SaveToFile(layoutXmlFile, dockMgr);
        }
        private void LoadLayout(DockSite dockMgr)
        {
            if (File.Exists(layoutXmlFile))
                layoutSerializer.LoadFromFile(layoutXmlFile, dockMgr);
        }

        ///
        private void OpenViewB(DockSite dockMgr)
        {
            ToolWindow toolwindows = new ToolWindow(true);
            toolwindows.Title = "ViewB";

            //https://www.actiprosoftware.com/community/thread/23398/styling-a-document-like-a-toolwindow#111977%20
            toolwindows.ImageSource = new BitmapImage(new Uri("/Resources/Images/AppImg.png", UriKind.Relative));
            UserControl ctrl = new ViewB();
            ctrl.Width = Double.NaN;
            ctrl.Height = Double.NaN;
            ctrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            ctrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            toolwindows.Content = ctrl;
            ctrl.Tag = Guid.NewGuid().ToString();
            //ctrl.SetParentWindow(toolwindows);
            dockMgr.ToolWindows.Add(toolwindows);
            System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            toolwindows.Float(pt2, new System.Windows.Size(300, 300));
            //ctrl.SetTitle(LanguageSupport.GetString("Menu_lite_quick_orders"));
            //ctrl.InitialUI();

        }

        private void OpenViewA(DockSite dockMgr)
        {
            ToolWindow toolwindows = new ToolWindow(true);
            toolwindows.Title = "ViewA";
            UserControl ctrl = new ViewA();
            ctrl.Width = Double.NaN;
            ctrl.Height = Double.NaN;
            ctrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            ctrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            toolwindows.Content = ctrl;
            ctrl.Tag = Guid.NewGuid().ToString();
            //ctrl.SetParentWindow(toolwindows);
            dockMgr.ToolWindows.Add(toolwindows);
            System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            toolwindows.Float(pt2, new System.Windows.Size(300, 300));
            //ctrl.SetTitle(LanguageSupport.GetString("Menu_lite_quick_orders"));
            //ctrl.InitialUI();

        }
    }
}
