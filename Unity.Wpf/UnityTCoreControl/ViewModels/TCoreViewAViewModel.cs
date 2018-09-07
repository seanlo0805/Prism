using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityTCoreControl.ViewModels
{
    /// <summary>
    /// 
    /// reference: https://dotblogs.com.tw/keigen/2017/07/17/091302
    ///            https://vulcanlee.gitbooks.io/xamarin-forms-develop-notes/GroupNotes5/ViewCoding.html
    /// </summary>
    public class TCoreViewAViewModel : BindableBase
    {
        IEventAggregator _ea;
        private string _title = "TCore ViewA";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public TCoreViewAViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
    }
}
