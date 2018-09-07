using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MEFModuleDiscoveryLocalDisk
{
    class MEFTCoreBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add Shell assembly
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MEFTCoreBootstrapper).Assembly));

            // Add module catalog
            var moduleCatalog = new DirectoryCatalog("Modules");
            this.AggregateCatalog.Catalogs.Add(moduleCatalog);
        }
    }
}
