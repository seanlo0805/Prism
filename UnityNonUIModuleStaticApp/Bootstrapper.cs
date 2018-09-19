using UnityNonUIModuleStaticApp.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using CalculatorModule;
using InputOutputModule;
using CalculatorInterfaces;
using Microsoft.Practices.ServiceLocation;
using System;

namespace UnityNonUIModuleStaticApp
{
    /// <summary>
    /// 在代碼內直接寫入模組(In Code), 便利之處在於可以在模組Initialize()之前, 就可以使用該class
    /// 以下示範在一開始(MainWindow 在Loaded事件時)就使用Modules的class, 此時按Prism的作業, IModule.Initialize()並未開始
    /// 為了達到此一目的, 必須在Bootstrapper就先把相關的class先Register進去, 見<code>Bootstrapper.ConfigureContainer()</code>
    /// </summary>
    class Bootstrapper : UnityBootstrapper
    {
        //protected override DependencyObject CreateShell()
        //{
        //    return Container.Resolve<MainWindow>();
        //}

        //protected override void InitializeShell()
        //{
        //    Application.Current.MainWindow.Show();
        //}

        //reference : https://github.com/PrismLibrary/Prism/blob/master/Source/Wpf/Prism.Unity.Wpf/Legacy/UnityBootstrapper.cs
        //            http://blog.woivre.fr/blog/2010/03/debuter-avec-unity
        protected override void ConfigureContainer()
        {
            //base.ConfigureContainer(); //no default configurator but init Container/LoggerFacade

            //loading basic service
            //RegisterTypeIfMissing(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), true);
            //RegisterTypeIfMissing(typeof(IModuleInitializer), typeof(ModuleInitializer), true);
            //RegisterTypeIfMissing(typeof(IModuleManager), typeof(ModuleManager), true);

            //loading domain service
            Container.RegisterType<ICalculator, Calculator>();
            Container.RegisterType<ICalculatorReplLoop, CalculatorReplLoop>();
            //Container.RegisterType<IInputService, ConsoleInputService>();
            Container.RegisterType<IInputService, MsgBoxInputService>();
            Container.RegisterType<IInputParserService, InputParserService>();

            Container.RegisterType<IOutputService, ConsoleOutputService>("Consoleoutput");
            Container.RegisterType<IOutputService, MsgBoxOutputService>("MsgBoxOutput");
            Container.RegisterInstance<IServiceLocator>(new UnityServiceLocatorAdapter(Container));
        }

        //reference : https://github.com/PrismLibrary/Prism/blob/master/Source/Wpf/Prism.Unity.Wpf/Legacy/UnityBootstrapper.cs#L104
        protected override void InitializeModules()
        {
            //base.InitializeModules(); // after run() -> InitializeShell()
            //ICalculatorReplLoop loop = Container.Resolve<ICalculatorReplLoop>();
            //loop.Run();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            this.Container = this.CreateContainer();
            if (this.Container == null)
            {
                //throw new InvalidOperationException(Resources.NullUnityContainerException);
                throw new InvalidOperationException("NullUnityContainerException");
            }

            ConfigureContainer();

            InitializeModules();


            ICalculatorReplLoop loop = Container.Resolve<ICalculatorReplLoop>();
            loop.Run();
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
