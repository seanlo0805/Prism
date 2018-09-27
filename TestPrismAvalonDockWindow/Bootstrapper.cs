using TestPrismAvalonDockWindow.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xceed.Wpf.AvalonDock;
using Prism.Regions;
using TestPrismAvalonDockWindow.Regions;
using TestPrismAvalonDockWindow.Modules;

namespace TestPrismAvalonDockWindow
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(DockingManager), this.Container.Resolve<DockManagerRegionAdapter>());
            return mappings;
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

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
