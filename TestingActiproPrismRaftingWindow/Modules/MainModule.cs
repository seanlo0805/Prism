using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingActiproPrismRaftingWindow.ViewModels.Docking;

namespace TestingActiproPrismRaftingWindow.Modules
{
    class MainModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;
        private readonly IRegionManager regionManager;
        public MainModule(IRegionViewRegistry registry, IRegionManager regionManager)
        {
            this.regionViewRegistry = registry;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            //ViewModels.Docking.ViewAViewModel viewAViewModel = new ViewAViewModel();
            //IRegion mainRegion = this.regionManager.Regions["MainRegion"];
            //mainRegion.Add(viewAViewModel);

            regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(ViewAViewModel));
            regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(ViewBViewModel));

        }
    }
}
