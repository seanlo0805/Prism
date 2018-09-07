using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MEFModelInCodeApp
{
    /// <summary>
    /// Example for Registering Model In Code
    /// 
    /// Interaction logic for App.xaml
    /// 
    /// 
    /// reference: https://hk.saowen.com/a/266674d00db0e0f4aad1eb735347ec557155615696ab7a7974c9f85107b4acaa
    ///            https://hk.saowen.com/a/9070f19d9e34c41ec7e1a8bdceb587b637937b3ad66d6d1ea8e4b742c414c30e
    ///            https://prismlibrary.github.io/docs/wpf/Modules.html#registering-modules-in-code
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
