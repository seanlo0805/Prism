using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
using ActiproSoftware.Windows.Serialization;
using System;
using System.IO;
using System.Windows;
using TestingActiproPrismRaftingWindow.ViewModels;

namespace TestingActiproPrismRaftingWindow.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DockSiteLayoutSerializer layoutSerializer = new DockSiteLayoutSerializer();
        private string layoutXmlFile = @".\Actiprosoft.xml";

        public MainWindow()
        {
            InitializeComponent();

            layoutSerializer.SerializationBehavior = DockSiteSerializationBehavior.ToolWindowsOnly;
            layoutSerializer.DocumentWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            layoutSerializer.ToolWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
            layoutSerializer.DockingWindowDeserializing += this.OnLayoutSerializerDockingWindowDeserializing;

            //if you would like to customizing serialization layout detail, implement "layoutSerializer.ObjectSerialized"
            //Add customizing layout configuration into "e.Node.Tag"
            layoutSerializer.ObjectSerialized += OnLayoutSerializerDockingWindowSerializaing;
        }
        void OnLayoutSerializerDockingWindowSerializaing(object sender, ItemSerializationEventArgs e)
        {
            ToolWindow tw = e.Item as ToolWindow;
            if (tw == null)
                return;
            e.Node.Tag = tw.Tag.ToString();
            //IToolWindowContentControl ss = tw.Content as IToolWindowContentControl;
            //if (ss == null)
            //    return;
            //XElement xElem = ss.ToXMLConfigNode();
            //if (xElem != null)
            //{
            //    xElem.Add(new XAttribute("Creater", ss.ObjectCreaterGUID));
            //}
            //e.Node.Tag = xElem.ToString();
        }

        private void OnLayoutSerializerDockingWindowDeserializing(object sender, DockingWindowDeserializingEventArgs e)
        {
            MainWindowViewModel vm = this.DataContext as MainWindowViewModel;
            if (vm == null || !e.Node.IsOpen)
                return;

            if (e.Node.Tag.ToString() == "ViewA")
            {
                if (e.Window != null)
                    vm.InitializeViewA(dockSite, e.Window as ToolWindow);
                else
                {
                    vm.OpenViewA(dockSite);
                }
            }
            else if (e.Node.Tag.ToString() == "ViewB")
            {
                if (e.Window != null)
                    vm.InitializeViewB(dockSite, e.Window as ToolWindow);
                else
                {
                    vm.OpenViewB(dockSite);
                }

            }

        }
        private void Button_ClickGC(object sender, RoutedEventArgs e)
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }


        private void Button_ClickSaveLayout(object sender, RoutedEventArgs e)
        {
            layoutSerializer.SaveToFile(layoutXmlFile, dockSite);
        }

        private void Button_ClickLoadLayout(object sender, RoutedEventArgs e)
        {
            if (File.Exists(layoutXmlFile))
                layoutSerializer.LoadFromFile(layoutXmlFile, dockSite);

        }

    }
}
