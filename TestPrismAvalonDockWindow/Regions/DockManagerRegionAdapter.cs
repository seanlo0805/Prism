using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock;

namespace TestPrismAvalonDockWindow.Regions
{
    class DockManagerRegionAdapter : RegionAdapterBase<DockingManager>
    {
        public DockManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }


        protected override void Adapt(IRegion region, DockingManager regionTarget)
        {

        }

        protected override IRegion CreateRegion()
        {
            var dockSManagerRegion = new Region();
            dockSManagerRegion.SortComparison = null;
            return dockSManagerRegion;
        }
    }
}
