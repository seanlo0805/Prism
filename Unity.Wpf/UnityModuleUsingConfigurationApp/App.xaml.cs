using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UnityModuleUsingConfigurationApp
{
    /// <summary>
    /// Example for Registering Model In Code
    /// 
    /// Interaction logic for App.xaml
    /// 
    /// 
    /// reference: https://github.com/PrismLibrary/Prism-Samples-Wpf
    ///            https://prismlibrary.github.io/docs/wpf/Initializing.html
    ///            https://github.com/PrismLibrary/Prism-Samples-Wpf/tree/master/07-Modules%20-%20AppConfig
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
