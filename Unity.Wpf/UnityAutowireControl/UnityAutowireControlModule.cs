using UnityAutowireControl.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace UnityAutowireControl
{
    public class UnityAutowireControlModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public UnityAutowireControlModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        //public void Initialize()
        //{
        //    _container.RegisterTypeForNavigation<ViewA>();
        //}
        public void Initialize()
        {
            //IRegionManager manager = _regionManager.RegisterViewWithRegion("BottomRegion", typeof(ViewA));
            _regionManager.RegisterViewWithRegion("BottomRegion", typeof(ViewA));
            _regionManager.RegisterViewWithRegion("TopRegion", typeof(ViewAReception));
        }
    }
}