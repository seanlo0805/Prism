using UnityDialogRepeatApp.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using CalculatorModule;
using CalculatorInterfaces;
using InputOutputModule;

namespace UnityDialogRepeatApp
{
    /// <summary>
    /// 在代碼內直接寫入模組(In Code), 便利之處在於可以在模組Initialize()之前, 就可以使用該class
    /// 以下示範在一開始(MainWindow 在Loaded事件時)就使用Modules的class, 此時按Prism的作業, IModule.Initialize()並未開始
    /// 為了達到此一目的, 必須在Bootstrapper就先把相關的class先Register進去, 見<code>Bootstrapper.ConfigureContainer()</code>
    /// </summary>
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

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //loading domain service
            Container.RegisterType<ICalculator, Calculator>();
            Container.RegisterType<ICalculatorReplLoop, CalculatorReplLoop>();
            //Container.RegisterType<IInputService, ConsoleInputService>();
            Container.RegisterType<IInputService, MsgBoxInputService>();
            Container.RegisterType<IInputParserService, InputParserService>();

            Container.RegisterType<IOutputService, ConsoleOutputService>("Consoleoutput");
            Container.RegisterType<IOutputService, MsgBoxOutputService>("MsgBoxOutput");
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));

            moduleCatalog.AddModule(typeof(InputOutputModule.InputOutputModule));
            moduleCatalog.AddModule(typeof(UnityDialogModule.UnityDialogModule));
            moduleCatalog.AddModule(typeof(CalculatorModule.CalculatorModule));
        }
    }
}
