using Prism.Unity;
using Microsoft.Practices.Unity; //see: https://stackoverflow.com/questions/2875429/iunitycontainer-resolvet-throws-error-claiming-it-cannot-be-used-with-type-par
using System.Windows;


namespace UnityTCoreApp
{
    class Bootstrapper : UnityBootstrapper
    {
        //called by App.OnStartup => BootStrapper.Run();
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<UnityTCoreShell>();    //using Microsoft.Practices.Unity; 
        }

        //called by App.OnStartup => BootStrapper.Run(); and right behind CreateShell()
        protected override void InitializeModules()
        {
            base.InitializeModules();

            //older App.Current.MainWindow was created by App.xaml => StartupUri="UnityTCoreShell.xaml", if it was configured in App.xaml; 
            App.Current.MainWindow = (UnityTCoreShell)this.Shell;
            App.Current.MainWindow.Show();
        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            this.ModuleCatalog.AddModule(null); //add IModule here
        }
    }
}
