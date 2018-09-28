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
        //private DockSiteLayoutSerializer layoutSerializer = new DockSiteLayoutSerializer();
        //private string layoutXmlFile = @".\Actiprosoft.xml";

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand OpenRaftingWindowA { get { return new DelegateCommand<DockSite>(OpenViewA); } }
        public ICommand OpenRaftingWindowB { get { return new DelegateCommand<DockSite>(OpenViewB); } }
        //public ICommand SaveLayoutToXml { get { return new DelegateCommand<DockSite>(SaveLayout); } }
        //public ICommand LoadLayoutFromXml { get { return new DelegateCommand<DockSite>(LoadLayout); } }


        public MainWindowViewModel()
        {

            //layoutSerializer.SerializationBehavior = DockSiteSerializationBehavior.ToolWindowsOnly;
            //layoutSerializer.DocumentWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            //layoutSerializer.ToolWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            //layoutSerializer.DockingWindowDeserializing += this.OnLayoutSerializerDockingWindowDeserializing;

        }

        //private void OnLayoutSerializerDockingWindowDeserializing(object sender, DockingWindowDeserializingEventArgs e)
        //{
        //    if (e.Node.Name == "ViewA")
        //    {
        //        if(e.Window != null)
        //            InitializeViewA(e.Window as ToolWindow);
        //        else
        //        {
        //            //DockSite dockSite = e.Window.DockSite;

        //            //DockSite.ge
        //        }
        //    }
        //    else if (e.Node.Name == "ViewB")
        //    {
        //        InitializeViewB(e.Window as ToolWindow);

        //    }

        //}
        //public void SaveLayout(DockSite dockMgr)
        //{
        //    layoutSerializer.SaveToFile(layoutXmlFile, dockMgr);
        //}
        //public void LoadLayout(DockSite dockMgr)
        //{
        //    if (File.Exists(layoutXmlFile))
        //        layoutSerializer.LoadFromFile(layoutXmlFile, dockMgr);
        //}
        public void InitializeViewA(ToolWindow toolwindows)
        {
            if (toolwindows == null)
                throw new ArgumentNullException("toolWindow for ViewA");

            UserControl ctrl = new ViewA();
            ctrl.Width = Double.NaN;
            ctrl.Height = Double.NaN;
            ctrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            ctrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //ctrl.Tag = Guid.NewGuid().ToString();
            //ctrl.SetParentWindow(toolwindows);
            //ctrl.SetTitle(LanguageSupport.GetString("Menu_lite_quick_orders"));
            //ctrl.InitialUI();

            toolwindows.Title = "ViewA";
            toolwindows.Name = "ViewA"; // no name no deserialization
            toolwindows.Content = ctrl;
        }
        public void InitializeViewB(ToolWindow toolwindows)
        {
            if (toolwindows == null)
                throw new ArgumentNullException("toolWindow for ViewB");

            UserControl ctrl = new ViewB();
            ctrl.Width = Double.NaN;
            ctrl.Height = Double.NaN;
            ctrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            ctrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //ctrl.Tag = Guid.NewGuid().ToString();
            //ctrl.SetParentWindow(toolwindows);
            //ctrl.SetTitle(LanguageSupport.GetString("Menu_lite_quick_orders"));
            //ctrl.InitialUI();

            toolwindows.Title = "ViewB";
            toolwindows.Name = "ViewB";// no name no deserialization
            //toolwindows.CanAttach = false;
            toolwindows.CanDock = false;
            toolwindows.Content = ctrl;

            //https://www.actiprosoftware.com/community/thread/23398/styling-a-document-like-a-toolwindow#111977%20
            toolwindows.ImageSource = new BitmapImage(new Uri("/Resources/Images/AppImg.png", UriKind.Relative));

        }
        public void OpenViewB(DockSite dockMgr)
        {
            ToolWindow toolwindows = new ToolWindow(true);

            InitializeViewB(toolwindows);

            dockMgr.ToolWindows.Add(toolwindows);
            System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            toolwindows.Float(pt2, new System.Windows.Size(300, 300));

        }

        public void OpenViewA(DockSite dockMgr)
        {
            ToolWindow toolwindows = new ToolWindow(true);

            InitializeViewA(toolwindows);

            dockMgr.ToolWindows.Add(toolwindows);
            System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            toolwindows.Float(pt2, new System.Windows.Size(300, 300));

        }
    }
}
