using MEFTCoreControl;
using Prism.Mef;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFModuleUsingXamlApp
{
    class MEFTCoreBootstrapper : MefBootstrapper
    {
        /// <summary>
        /// <para>Load Module Catalog by xaml file by File Stream</para>
        /// <para>you have to do in Post-Build Event : </para>
        /// <para>shell> copy $(ProjectDir)ModuleCatalog.xaml $(TargetDir)ModuleCatalog.xaml</para>
        /// 
        /// <para>reference:  </para>
        /// <para>(1) https://stackoverflow.com/questions/4007149/how-to-enter-the-uri-to-setup-the-modulecatalog-in-prism-wpf </para>
        /// </summary>
        protected IModuleCatalog CreateModuleCatalogByLocalXaml()
        {
            ////you have to do in Post-Build Event : copy $(ProjectDir)ModuleCatalog.xaml $(TargetDir)ModuleCatalog.xaml
            FileStream catalogStream = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open);
            var catalog = Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogStream);
            catalogStream.Dispose();
            return catalog;

            //// MEF and Unity **BOTH** use the ModuleCatalog when configuring from a file
            //return Prism.Modularity.ModuleCatalog.CreateFromXaml(
            //    System.IO.File.OpenRead(@".\ModuleCatalog.xaml"));
        }


        /// <summary>
        /// <para>Load Module Catalog by xaml file by File Stream</para>
        /// 
        /// <para>reference:  </para>
        /// <para>(1) https://prismlibrary.github.io/docs/wpf/Modules.html#registering-modules-using-a-xaml-file </para>
        /// <para>(2) https://www.cnblogs.com/dfun/p/4293808.html   ==> when you got error {"“\f”(十六进制值 0x0C)是无效的字符 , change "Build Action" to "Resource" in properties for ModuleCatalog.xaml</para>
        /// </summary>
        protected IModuleCatalog CreateModuleCatalogByApplicationResource()
        {
            var filePath = "ModuleCatalog.xaml";
            var projectName = "MEFModuleUsingXamlApp";
            var uriPath = string.Format("/{0};component/{1}", projectName, filePath);

            var uri = new Uri(uriPath, UriKind.Relative);
            var moduleCatalog = Prism.Modularity.ModuleCatalog.CreateFromXaml(uri);

            return moduleCatalog;

            //return null;
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return CreateModuleCatalogByApplicationResource();
            //return CreateModuleCatalogByLocalXaml();
        }

    }
}
