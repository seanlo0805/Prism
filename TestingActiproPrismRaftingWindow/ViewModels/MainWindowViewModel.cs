using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using TestingActiproPrismRaftingWindow.Views.Docking;

namespace TestingActiproPrismRaftingWindow.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Grid _titleBarGrid;
        private Menu _menu;

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

        public Menu Menu
        {
            get { return _menu; }
            set { SetProperty(ref _menu, value); }
        }

        public Grid TitleBarGrid
        {
            get { return _titleBarGrid; }
            set { SetProperty(ref _titleBarGrid, value); }
        }

        public MainWindowViewModel()
        {
            _titleBarGrid = new Grid();
            _menu = new Menu();

            SetupMenu(_menu);

            _titleBarGrid.Children.Add(_menu);



            //layoutSerializer.SerializationBehavior = DockSiteSerializationBehavior.ToolWindowsOnly;
            //layoutSerializer.DocumentWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            //layoutSerializer.ToolWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            //layoutSerializer.DockingWindowDeserializing += this.OnLayoutSerializerDockingWindowDeserializing;

        }


        // http://osask.cn/front/ask/view/1478001
        // https://www.cnblogs.com/yanxiaodi/p/4237576.html
        // https://hk.saowen.com/a/d9943e880f63ec25221ab5de3a1a2c820b548221727ed5051d96389511ae6277
        // https://stackoverflow.com/questions/14354773/wpf-how-to-bind-control-by-mvvm
        private void SetupMenu(Menu menu)
        {
            if (menu == null)
                return;

            //IMenuService, data meta
            // 1. Command
            // 2. CommandParameter (dockSite from MainWindowModule
            // 3. Window.Name (TW, CN ...)      //no name no deserialization, also working for fetching Window.Title and MenuText (Tw, CN...)
            // 4. Window.Title (TW, CN ...)     //default Title name
            // 5. MenuText (MenuItem.Header)    //default Menu name
            // 6. Window size
            // 7. TitleBar ImageSource
            // 8. MenuItem group                // default Other (get by Window.Name) 
            // 9. position ?!
            MenuItem openViewAItem = new MenuItem();
            openViewAItem.Command = OpenRaftingWindowA;
            openViewAItem.SetBinding(MenuItem.CommandParameterProperty, new Binding() { ElementName = "dockSite" });
            //BindingOperations.SetBinding(openViewAItem, MenuItem.CommandParameterProperty, new Binding() { ElementName = "dockSite" });
            openViewAItem.Header = "OpenViewA";
            Menu.Items.Add(openViewAItem);


            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/71cd9782-8fd3-4094-89ce-8606cd05db4f/adding-images-to-menuitem?forum=wpf
            MenuItem openViewBItem = new MenuItem();
            openViewBItem.Command = OpenRaftingWindowB;
            BindingOperations.SetBinding(openViewBItem, MenuItem.CommandParameterProperty, new Binding() { ElementName = "dockSite" });
            openViewBItem.Header = "OpenViewB";
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Resources/Images/AppImg.png", UriKind.Relative));
            openViewBItem.Icon = img;
            //openViewBItem.VerticalAlignment = VerticalAlignment.Bottom;
            Menu.Items.Add(openViewBItem);
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
        public void InitializeViewA(DockSite dockSite, ToolWindow toolwindows)
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
            toolwindows.Tag = "ViewA"; //for deserialization
            toolwindows.Content = ctrl;

            /// 這裡實作, 當Toolwindow closing時, 回收DockSite中的Toolwin resource
            /// 但, 在Docking Toolwindow時, 其實會先Load -> UnLoad -> Load, 此時DockHost仍然存在
            /// 只有在真的按下close windows時, DockHost才會變成null 
            /// 如果DockHost ==null 表示真的要離開DockSite
            toolwindows.Unloaded += (object sender, RoutedEventArgs e) => {
                if (sender is ToolWindow win)
                    if (win.DockHost == null) // window is leaving
                        dockSite.ToolWindows.Remove(win);
            };
        }
        public void InitializeViewB(DockSite dockSite, ToolWindow toolwindows)
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
            toolwindows.Tag = "ViewB"; //for deserialization
            //toolwindows.CanAttach = false;
            toolwindows.CanDock = false;
            toolwindows.Content = ctrl;

            /// 這裡實作, 當Toolwindow closing時, 回收DockSite中的Toolwin resource
            /// 但, 在Docking Toolwindow時, 其實會先Load -> UnLoad -> Load, 此時DockHost仍然存在
            /// 只有在真的按下close windows時, DockHost才會變成null 
            /// 如果DockHost ==null 表示真的要離開DockSite
            toolwindows.Unloaded += (object sender, RoutedEventArgs e) =>{
                if (sender is ToolWindow win)
                    if (win.DockHost == null) // window is leaving
                        dockSite.ToolWindows.Remove(win);
            };

            //https://www.actiprosoftware.com/community/thread/23398/styling-a-document-like-a-toolwindow#111977%20
            toolwindows.ImageSource = new BitmapImage(new Uri("/Resources/Images/AppImg.png", UriKind.Relative));

        }

        public void OpenViewB(DockSite dockMgr)
        {
            ToolWindow toolwindows = new ToolWindow(true);

            InitializeViewB(dockMgr, toolwindows);

            dockMgr.ToolWindows.Add(toolwindows);
            System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            toolwindows.Float(pt2, new System.Windows.Size(300, 300));

        }

        public void OpenViewA(DockSite dockMgr)
        {
            ToolWindow toolwindows = new ToolWindow(true);

            InitializeViewA(dockMgr, toolwindows);

            dockMgr.ToolWindows.Add(toolwindows);
            System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            toolwindows.Float(pt2, new System.Windows.Size(300, 300));

        }
    }
}
