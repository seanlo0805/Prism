using System.Windows;

namespace MEFTCoreApp
{
    /// <summary>
    /// 
    /// 
    /// reference : https://hk.saowen.com/a/9070f19d9e34c41ec7e1a8bdceb587b637937b3ad66d6d1ea8e4b742c414c30e
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MEFTCoreBootstrapper boostrapper = new MEFTCoreBootstrapper();
            boostrapper.Run();

            //could not assign Content="" @ ContentControl, or Exception thrown
        }
    }
}
