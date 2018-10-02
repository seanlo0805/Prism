using TestingActiproPrismRaftingWindow.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Prism.Regions;
using TestingActiproPrismRaftingWindow.Modules;
using ActiproSoftware.Windows.Controls.Docking;
using TestingActiproPrismRaftingWindow.Regions;

namespace TestingActiproPrismRaftingWindow
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            //return Container.Resolve<MainWindow>();
            MainWindow window = Container.Resolve<MainWindow>();
            window.Show();
            return window;
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(DockSite), this.Container.Resolve<DockSiteRegionAdapter>());
            return mappings;
        }

        //protected override void InitializeShell()
        //{
        //    Application.Current.MainWindow.Show();
        //}

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }

        protected override void InitializeModules()
        {
            IModule mainModule = this.Container.Resolve<MainModule>();
            mainModule.Initialize();
        }
    }
}
