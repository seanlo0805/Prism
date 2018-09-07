using UnityModuleUsingConfigurationApp.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace UnityModuleUsingConfigurationApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //loading modules frm App.config
            return new ConfigurationModuleCatalog();
        }
        //protected override void ConfigureModuleCatalog()
        //{
        //    var moduleCatalog = (ModuleCatalog)ModuleCatalog;
        //    //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        //}
    }
}
