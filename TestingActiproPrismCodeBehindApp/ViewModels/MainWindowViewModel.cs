using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
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
        private DockSiteLayoutSerializer layoutSerializer = new DockSiteLayoutSerializer();
        private string layoutXmlFile = @".\Actiprosoft.xml";


        private Grid _titleBarGrid;
        private Menu _menu;
        DockSite _dockSite;

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand OpenRaftingWindowA { get { return new DelegateCommand(OpenViewA); } }
        public ICommand OpenRaftingWindowB { get { return new DelegateCommand(OpenViewB); } }
        public ICommand SaveLayoutToXml { get { return new DelegateCommand(SaveLayout); } }
        public ICommand LoadLayoutFromXml { get { return new DelegateCommand(LoadLayout); } }

        public DockSite DockSite
        {
            get { return _dockSite; }
            set { SetProperty(ref _dockSite, value); }
        }

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
            _menu = SetupMenu();
            _dockSite = SetupDockSite(_menu, _titleBarGrid);

            //layout serialization
            layoutSerializer.SerializationBehavior = DockSiteSerializationBehavior.ToolWindowsOnly;
            layoutSerializer.DocumentWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            layoutSerializer.ToolWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            layoutSerializer.DockingWindowDeserializing += this.OnLayoutSerializerDockingWindowDeserializing;

            ////if you would like to customizing serialization layout detail, implement "layoutSerializer.ObjectSerialized"
            ////Add customizing layout configuration into "e.Node.Tag"
            //layoutSerializer.ObjectSerialized += OnLayoutSerializerDockingWindowSerializaing
        }

        private DockSite SetupDockSite(Menu menu, Grid titleBarGrid)
        {
            DockSite dockSite = new DockSite();

            //create toolwindow and show in Taskbar
            dockSite.SetValue(DockSite.FloatingWindowShowInTaskBarModeProperty, FloatingWindowShowInTaskBarMode.Always);
            dockSite.SetValue(DockSite.FloatingToolWindowContainersHaveMinimizeButtonsProperty, true);
            //dockSite.SetValue(DockSite.CanToolWindowsDockProperty, false);
            //dockSite.SetValue(DockSite.CanToolWindowsCloseProperty, true);
            //dockSite.SetValue(DockSite.CanToolWindowsAutoHideProperty, false);
            dockSite.CanToolWindowsAutoHide = true;
            dockSite.SetValue(FrameworkElement.NameProperty, "dockSite");
            dockSite.SetValue(FrameworkElement.NameProperty, "dockSite");
            dockSite.SetValue(RegionManager.RegionNameProperty, "MainRegion");

            Grid grid = new Grid();
            grid.Children.Add(titleBarGrid);
            dockSite.SetValue(DockSite.ChildProperty, grid);

            titleBarGrid.Children.Add(_menu);
            return dockSite;

        }

        // http://osask.cn/front/ask/view/1478001
        // https://www.cnblogs.com/yanxiaodi/p/4237576.html
        // https://hk.saowen.com/a/d9943e880f63ec25221ab5de3a1a2c820b548221727ed5051d96389511ae6277
        // https://stackoverflow.com/questions/14354773/wpf-how-to-bind-control-by-mvvm
        private Menu SetupMenu()
        {
            Menu menu = new Menu();

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
            openViewAItem.Header = "OpenViewA";
            menu.Items.Add(openViewAItem);


            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/71cd9782-8fd3-4094-89ce-8606cd05db4f/adding-images-to-menuitem?forum=wpf
            MenuItem openViewBItem = new MenuItem();
            openViewBItem.Command = OpenRaftingWindowB;
            openViewBItem.Header = "OpenViewB";
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Resources/Images/AppImg.png", UriKind.Relative));
            openViewBItem.Icon = img;
            menu.Items.Add(openViewBItem);

            MenuItem loadLayoutItem = new MenuItem();
            loadLayoutItem.Command = LoadLayoutFromXml;
            loadLayoutItem.Header = "LoadLayout";
            menu.Items.Add(loadLayoutItem);

            MenuItem saveLayoutItem = new MenuItem();
            saveLayoutItem.Command = SaveLayoutToXml;
            saveLayoutItem.Header = "SaveLayout";
            menu.Items.Add(saveLayoutItem);

            return menu;

        }


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
            toolwindows.Name = "ViewA" + "_" + Guid.NewGuid().ToString().Replace("-", "_"); // no name no deserialization, you could use toolwindows.Tag instead
            toolwindows.Content = ctrl;
            toolwindows.CanAutoHide = true;
            //toolwindows.DockHost.CloseAutoHidePopup(true, true);
            //toolwindows.Uid = Guid.NewGuid().ToString();
        }
        public void InitializeViewB(ToolWindow toolwindows)
        {
            if (toolwindows == null)
                throw new ArgumentNullException("toolWindow for ViewB");

            ViewB ctrl = new ViewB();
            ctrl.Width = Double.NaN;
            ctrl.Height = Double.NaN;
            ctrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            ctrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //ctrl.Tag = Guid.NewGuid().ToString();
            //ctrl.SetParentWindow(toolwindows);
            //ctrl.SetTitle(LanguageSupport.GetString("Menu_lite_quick_orders"));
            //ctrl.InitialUI();

            toolwindows.Title = "ViewB";
            toolwindows.Name = "ViewB"+"_"+ Guid.NewGuid().ToString().Replace("-","_");// no name no deserialization, you could use toolwindows.Tag instead
            //toolwindows.CanAttach = false;
            toolwindows.CanDock = false;
            toolwindows.Content = ctrl;
            //toolwindows.Uid = Guid.NewGuid().ToString();

            //https://www.actiprosoftware.com/community/thread/23398/styling-a-document-like-a-toolwindow#111977%20
            toolwindows.ImageSource = new BitmapImage(new Uri("/Resources/Images/AppImg.png", UriKind.Relative));

        }
        public void OpenViewB()
        {
            ToolWindow toolwindows = new ToolWindow(DockSite);

            toolwindows.Unloaded += OnToolWindow_Unload;
            InitializeViewB(toolwindows);

            //DockSite.ToolWindows.Add(toolwindows);
            //System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            //System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            //toolwindows.Float(pt2, new System.Windows.Size(300, 300));
            toolwindows.Float(new System.Windows.Size(300, 300));

        }

        public void OpenViewA()
        {
            ToolWindow toolwindows = new ToolWindow(DockSite);
            toolwindows.Unloaded += OnToolWindow_Unload;
            InitializeViewA(toolwindows);

            //DockSite.ToolWindows.Add(toolwindows);
            //System.Drawing.Point pt = System.Windows.Forms.Control.MousePosition;
            //System.Windows.Point pt2 = new System.Windows.Point((pt.X - 32), (pt.Y + 8));
            //toolwindows.Float(pt2, new System.Windows.Size(300, 300));
            toolwindows.Float(new System.Windows.Size(300, 300));

        }

        private void OnToolWindow_Unload(object sender, RoutedEventArgs e)
        {
            /// 這裡實作, 當Toolwindow closing時, 回收DockSite中的Toolwin resource
            /// 但, 在Docking Toolwindow時, 其實會先Load -> UnLoad -> Load, 此時DockHost仍然存在
            /// 只有在真的按下close windows時, DockHost才會變成null 
            /// 如果DockHost ==null 表示真的要離開DockSite
            if (sender is ToolWindow win)
                if(win.DockHost  == null) // window is leaving
                    DockSite.ToolWindows.Remove(win);
        }

        private void OnLayoutSerializerDockingWindowDeserializing(object sender, DockingWindowDeserializingEventArgs e)
        {
            string controlName="";
            if(e.Node.Name != null )
            {
                int pos = e.Node.Name.IndexOf("_");
                if(pos >= 0)
                {
                    controlName = e.Node.Name.Substring(0, pos);
                }
            }

            if (string.IsNullOrEmpty(controlName) || !e.Node.IsOpen)
                return;

            if (controlName == "ViewA")
            {
                if (e.Window != null)
                    InitializeViewA(e.Window as ToolWindow);
                else
                {
                    OpenViewA();
                }
            }
            else if (controlName == "ViewB")
            {
                if (e.Window != null)
                    InitializeViewB(e.Window as ToolWindow);
                else
                {
                    OpenViewB();
                }

            }

        }
        public void SaveLayout()
        {
            layoutSerializer.SaveToFile(layoutXmlFile, DockSite);
        }
        public void LoadLayout()
        {
            if (File.Exists(layoutXmlFile))
                layoutSerializer.LoadFromFile(layoutXmlFile, DockSite);
        }

    }
}
