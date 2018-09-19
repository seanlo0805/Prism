using CalculatorModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using CalculatorInterfaces;

namespace CalculatorModule
{
    public class CalculatorModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public CalculatorModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA>();
            _container.RegisterType<ICalculator, Calculator>();
            _container.RegisterType<ICalculatorReplLoop, CalculatorReplLoop>();
            //Container.RegisterType<IInputService, ConsoleInputService>();
        }
    }
}