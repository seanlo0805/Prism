using UnityTCoreControl.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace UnityTCoreControl
{
    public class UnityTCoreControlModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public UnityTCoreControlModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        //public void Initialize()
        //{
        //    _container.RegisterTypeForNavigation<TCoreViewA>();
        //}
        public void Initialize()
        {
            IRegionManager manager = _regionManager.RegisterViewWithRegion("MainRegion", typeof(TCoreViewA));
        }
    }
}