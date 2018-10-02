using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingActiproPrismRaftingWindow.ViewModels.Docking.Core;

namespace TestingActiproPrismRaftingWindow.ViewModels.Docking
{
    //public class ViewAViewModel : BindableBase
    public class ViewAViewModel : DockingItemViewModelBase
    {
        public ViewAViewModel()
        {

        }

        public override bool IsTool
        {
            get
            {
                return false;
            }
        }
    }
}
