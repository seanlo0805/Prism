using TestUnityApp.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using CalculatorInterfaces;
using CalculatorModule;
using InputOutputModule;

namespace TestUnityApp
{
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
