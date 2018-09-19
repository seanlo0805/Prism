using InputOutputModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using CalculatorInterfaces;

namespace InputOutputModule
{
    public class InputOutputModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public InputOutputModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA>();

            _container.RegisterType<IInputService, MsgBoxInputService>();
            _container.RegisterType<IInputParserService, InputParserService>();

            _container.RegisterType<IOutputService, ConsoleOutputService>("Consoleoutput");
            _container.RegisterType<IOutputService, MsgBoxOutputService>("MsgBoxOutput");
        }
    }
}