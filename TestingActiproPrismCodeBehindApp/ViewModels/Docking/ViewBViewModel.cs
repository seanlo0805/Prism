using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingActiproPrismRaftingWindow.ViewModels.Docking.Core;

namespace TestingActiproPrismRaftingWindow.ViewModels.Docking
{
    //public class ViewBViewModel : BindableBase
    public class ViewBViewModel : DockingItemViewModelBase
    {
        public ViewBViewModel()
        {

        }


        public string Text
        {
            get { return "ViewB..."; }
        }
        public override bool IsTool
        {
            get
            {
                return true;
            }
        }
    }
}
