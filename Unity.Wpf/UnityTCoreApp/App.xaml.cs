using System.Windows;

namespace UnityTCoreApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper boostrapper = new Bootstrapper();
            boostrapper.Run();
        }
    }
}
