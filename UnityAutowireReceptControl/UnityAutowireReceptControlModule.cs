using UnityAutowireReceptControl.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace UnityAutowireReceptControl
{
    public class UnityAutowireReceptControlModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public UnityAutowireReceptControlModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA>();
        }
    }
}