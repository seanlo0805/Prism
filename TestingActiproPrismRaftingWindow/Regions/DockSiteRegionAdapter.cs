using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Docking;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestingActiproPrismRaftingWindow.ViewModels.Docking.Core;
using TestingActiproPrismRaftingWindow.Views.Docking;

namespace TestingActiproPrismRaftingWindow.Regions
{
    public class DockSiteRegionAdapter : RegionAdapterBase<DockSite>
    {

        public DockSiteRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        private static bool IsDocumentWindow(object item)
        {
            if (item is ViewA)
                return true;
            if (item is ViewB)
                return false;

            DockingItemViewModelBase viewModel = item as DockingItemViewModelBase;

            if (viewModel != null)
                return (viewModel.IsTool ? false : true);

            return true;
        }

        protected override void Adapt(IRegion region, DockSite regionTarget)
        {
            if (region == null)
                throw new ArgumentNullException("region");
            if (regionTarget == null)
                throw new ArgumentNullException("regionTarget");

            // If supporting document views, ensure that DockSite.DocumentItemsSource hasn't been set
            if ((
                    (regionTarget.DocumentItemsSource != null) ||
                    (BindingOperations.GetBinding(regionTarget, DockSite.DocumentItemsSourceProperty) != null)
                ))
                throw new InvalidOperationException("The DockSite.DocumentItemsSource property is not empty.");

            // If supporting tool views, ensure that DockSite.ToolItemsSource hasn't been set
            if ((
                    (regionTarget.ToolItemsSource != null) ||
                    (BindingOperations.GetBinding(regionTarget, DockSite.ToolItemsSourceProperty) != null)
                ))
                throw new InvalidOperationException("The DockSite.ToolItemsSource property is not empty.");

            regionTarget.DocumentItemsSource = new EnumerableView<object>(region.Views,
                (o) => { return IsDocumentWindow(o); });
            regionTarget.ToolItemsSource = new EnumerableView<object>(region.Views,
                (o) => { return !IsDocumentWindow(o); });
        }

        protected override void AttachBehaviors(IRegion region, DockSite regionTarget)
        {
            //if (region == null)
            //    throw new ArgumentNullException("region");
            //if (regionTarget == null)
            //    throw new ArgumentNullException("regionTarget");

            //// Add the behavior that syncs the items source items with the rest of the items
            //region.Behaviors.Add(DockSiteRegionBehavior.BehaviorKey, new DockSiteRegionBehavior()
            //{
            //    DockSite = regionTarget
            //});

            // Call the base method
            base.AttachBehaviors(region, regionTarget);
        }

        protected override IRegion CreateRegion()
        {
            // Clear the SortComparison or else any time a region is added, the Prism items source will tell DockSite
            //   to reset and windows won't remain open properly
            var dockSiteRegion = new Region();
            dockSiteRegion.SortComparison = null;
            return dockSiteRegion;
        }

    }
}
