using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MEFModuleUsingConfigurationApp
{
    /// <summary>
    /// Example for Registering Model In Code
    /// 
    /// Interaction logic for App.xaml
    /// 
    /// 
    /// reference: https://prismlibrary.github.io/docs/wpf/Modules.html#registering-modules-using-a-configuration-file
    ///            https://github.com/vinosd08/Prism7.0
    ///            https://github.com/stefaneyd/my-dot-net-prism-project
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MEFTCoreBootstrapper bs = new MEFTCoreBootstrapper();
            bs.Run();
        }
    }
}
