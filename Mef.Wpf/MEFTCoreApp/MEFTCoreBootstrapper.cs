using Prism.Mef;
using Prism.Modularity;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace MEFTCoreApp
{
    /// <summary>
    /// 
    /// reference : https://hk.saowen.com/a/9070f19d9e34c41ec7e1a8bdceb587b637937b3ad66d6d1ea8e4b742c414c30e
    ///             https://hk.saowen.com/a/151c7e20527a2a1eb160898cf6e9cb76b58eb95127bfd74b51d39fd8e61486f0
    ///             http://pignut-wang.iteye.com/blog/1561276
    ///             http://blog.sanc.idv.tw/2012/03/prism-prism.html
    ///             http://blog.sanc.idv.tw/2012/03/prism-shellmodule.html
    /// </summary>
    class MEFTCoreBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MEFTCoreShell>();
        }
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (MEFTCoreShell)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
