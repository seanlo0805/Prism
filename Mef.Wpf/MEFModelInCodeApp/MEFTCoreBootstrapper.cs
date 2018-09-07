using MEFTCoreControl;
using Prism.Mef;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace MEFModelInCodeApp
{
    class MEFTCoreBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }
        protected override void InitializeShell()
        {
            //Application.Current.MainWindow.Show();


            base.InitializeShell();

            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }
        protected override void ConfigureAggregateCatalog()
        {
            ////use for Register Using Xaml, ModuleName and ModuleType
            Type moduleCType = typeof(TCoreControlA);
            string name = moduleCType.Name;
            string type = moduleCType.AssemblyQualifiedName;


            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MEFTCoreBootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TCoreControlA).Assembly));
        }
    }
}
