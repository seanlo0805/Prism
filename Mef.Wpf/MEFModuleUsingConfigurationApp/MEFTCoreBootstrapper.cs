using MEFTCoreControl;
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

namespace MEFModuleUsingConfigurationApp
{
    class MEFTCoreBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
            //return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void InitializeShell()
        {
            App.Current.MainWindow = (MainWindow)this.Shell;
            App.Current.MainWindow.Show();
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
        protected override void ConfigureAggregateCatalog()
        {
            ////use for Register Using Xaml, ModuleName and ModuleType
            //Type moduleCType = typeof(TCoreControlA);
            //string name = moduleCType.Name;
            //string type = moduleCType.AssemblyQualifiedName;
            //string moduleName = moduleCType.Module.Name;


            base.ConfigureAggregateCatalog();

            //在MEF中, 若沒有手動加入Bootstrapper, 會導致System.ComponentModel.Composition.ImportCardinalityMismatchException
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MEFTCoreBootstrapper).Assembly));

            //已經在App.config載入
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TCoreControlA).Assembly));
        }
    }
}
