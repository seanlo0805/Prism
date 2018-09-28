using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
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

            ////if you would like to customizing serialization layout detail, implement "layoutSerializer.ObjectSerialized"
            ////Add customizing layout configuration into "e.Node.Tag"
            //layoutSerializer.ObjectSerialized += OnLayoutSerializerDockingWindowSerializaing
        }

        private void OnLayoutSerializerDockingWindowDeserializing(object sender, DockingWindowDeserializingEventArgs e)
        {
            MainWindowViewModel vm = this.DataContext as MainWindowViewModel;
            if (vm == null)
                return;

            if (e.Node.Name == "ViewA")
            {
                if (e.Window != null)
                    vm.InitializeViewA(e.Window as ToolWindow);
                else
                {
                    vm.OpenViewA(dockSite);
                }
            }
            else if (e.Node.Name == "ViewB")
            {
                if (e.Window != null)
                    vm.InitializeViewB(e.Window as ToolWindow);
                else
                {
                    vm.OpenViewB(dockSite);
                }

            }

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
