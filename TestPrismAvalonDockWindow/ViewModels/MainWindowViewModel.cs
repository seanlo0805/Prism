using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using TestPrismAvalonDockWindow.Views.Docking;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;

namespace TestPrismAvalonDockWindow.ViewModels
{
    /// <summary>
    /// 經過測試, 有幾個問題
    /// 1. 不能在一次執行期間SaveLayout / LoadLayout, 關閉程式時會Exception
    /// 2. 透過OpenViewA 開啟的FloatingWindow不能docking在同一個widnow
    ///    但docking進MainWindow, 再拉出來的可以; 透過SaveLayout/ LoadLayout的window也可以
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand OpenRaftingWindowA { get { return new DelegateCommand<DockingManager>(OpenViewA); } }
        public ICommand SaveLayoutToFile { get { return new DelegateCommand<DockingManager>(SaveLayout); } }
        public ICommand LoadLayoutFromFile { get { return new DelegateCommand<DockingManager>(LoadLayout); } }


        public MainWindowViewModel()
        {

        }
        private void SaveLayout(DockingManager dockMgr)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockMgr);
            serializer.Serialize(@".\AvalonDock.config");

        }
        private void LoadLayout(DockingManager dockMgr)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockMgr);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                if (args != null && args.Model != null && args.Model.Title != null)
                {
                    switch (args.Model.Title)
                    {
                        case "ViewA":
                            args.Content = new ViewA();
                            break;

                    }
                }
            };
            if (File.Exists(@".\AvalonDock.config"))
                serializer.Deserialize(@".\AvalonDock.config");

        }
        private void OpenViewA(DockingManager dockMgr)
        {

            /// https://github.com/xceedsoftware/wpftoolkit/issues/1145
            ///
            /// dockMgr
            ///   +--FloatingWindows 
            ///        +---LayoutAnchorableFloatingWindow
            ///              +---LayoutAnchorablePaneGroup
            ///                     +---LayoutAnchorablePane
            ///                            +---LayoutAnchorable
            ///                                   +---ViewA
            ///                                     

            ViewA viewA = new ViewA();
            var la = new LayoutAnchorable() { Title = "ViewA", Content = viewA, IsActive = true };
            var lfw = new LayoutAnchorableFloatingWindow();
            //var lap = new LayoutAnchorablePane() { Name = "Pane", Parent = lfw };
            var lap = new LayoutAnchorablePane() { Parent = lfw };
            var lapg = new LayoutAnchorablePaneGroup(lap);
            
            lap.Children.Add(la);

            lfw.RootPanel = lapg;
            lfw.Parent = dockMgr.Layout;


            dockMgr.Layout.FloatingWindows.Add(lfw);

            // Hack
            //LayoutAnchorableFloatingWindowControl lfwc = (LayoutAnchorableFloatingWindowControl)Activator.CreateInstance(typeof(LayoutAnchorableFloatingWindowControl), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { lfw }, CultureInfo.InvariantCulture);
            LayoutAnchorableFloatingWindowControl lfwc = (LayoutAnchorableFloatingWindowControl)Activator.CreateInstance(typeof(LayoutAnchorableFloatingWindowControl), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { lfw }, null);
            dockMgr.UpdateLayout();
            lfwc.Width = 300;
            lfwc.Height = 300;
            //lfwc.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            //lfwc.ShowInTaskbar = true;
            //lfwc.Topmost = true;
            lfwc.Show();
        }
        private void OpenViewA4(DockingManager dockMgr)
        {

            var firstFloatingWindow = dockMgr.Layout.Descendents().OfType<LayoutAnchorableFloatingWindow>().FirstOrDefault();
            if (firstFloatingWindow == null)
                return;

            var firstAnchorablePane = firstFloatingWindow.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();
            if (firstAnchorablePane != null)
            {

                LayoutAnchorable doc2 = new LayoutAnchorable();
                ViewA viewA = new ViewA();
                doc2.Title = "ViewA";
                doc2.Content = viewA;
                doc2.IsActive = true;
                firstAnchorablePane.Children.Add(doc2);

            }

            //LayoutAnchorable la = new LayoutAnchorable();
            //LayoutAnchorableFloatingWindow lfw = new LayoutAnchorableFloatingWindow();
            ////LayoutAnchorablePane lap = new LayoutAnchorablePane(layoutAnchorable);
            //LayoutAnchorablePane lap = new LayoutAnchorablePane() { Name = "Pane"};
            //lap.Parent = lfw;
            //lfw.RootPanel = new LayoutAnchorablePaneGroup(lap);
            //lfw.Parent = dockMgr.Layout;
            //dockMgr.Layout.FloatingWindows.Add(lfw);

            //// Hack
            //LayoutAnchorableFloatingWindowControl lfwc = (LayoutAnchorableFloatingWindowControl)Activator.CreateInstance(typeof(LayoutAnchorableFloatingWindowControl), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { lfw }, CultureInfo.InvariantCulture);
            //dockMgr.UpdateLayout();
            //lfwc.Width = 300;
            //lfwc.Height = 300;
            //lfwc.Topmost = true;
            //lfwc.Show();

        }
        private void OpenViewA3(DockingManager dockMgr)
        {

            var firstDocumentPane = dockMgr.Layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();
            if (firstDocumentPane != null)
            {

                LayoutAnchorable doc2 = new LayoutAnchorable();
                ViewA viewA = new ViewA();
                doc2.Title = "ViewA";
                doc2.Content = viewA;
                doc2.IsActive = true;
                firstDocumentPane.Children.Add(doc2);

            }

            //LayoutAnchorable la = new LayoutAnchorable();
            //LayoutAnchorableFloatingWindow lfw = new LayoutAnchorableFloatingWindow();
            ////LayoutAnchorablePane lap = new LayoutAnchorablePane(layoutAnchorable);
            //LayoutAnchorablePane lap = new LayoutAnchorablePane() { Name = "Pane"};
            //lap.Parent = lfw;
            //lfw.RootPanel = new LayoutAnchorablePaneGroup(lap);
            //lfw.Parent = dockMgr.Layout;
            //dockMgr.Layout.FloatingWindows.Add(lfw);


        }

        private void OpenViewA2(DockingManager dockMgr)
        {

            var firstDocumentPane = dockMgr.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstDocumentPane != null)
            {

                LayoutDocument doc2 = new LayoutDocument();
                ViewA viewA = new ViewA();
                doc2.Title = "ViewA";
                doc2.Content = viewA;
                doc2.IsActive = true;
                firstDocumentPane.Children.Add(doc2);

            }

            //LayoutAnchorable la = new LayoutAnchorable();
            //LayoutAnchorableFloatingWindow lfw = new LayoutAnchorableFloatingWindow();
            ////LayoutAnchorablePane lap = new LayoutAnchorablePane(layoutAnchorable);
            //LayoutAnchorablePane lap = new LayoutAnchorablePane() { Name = "Pane"};
            //lap.Parent = lfw;
            //lfw.RootPanel = new LayoutAnchorablePaneGroup(lap);
            //lfw.Parent = dockMgr.Layout;
            //dockMgr.Layout.FloatingWindows.Add(lfw);


        }
    }
}
