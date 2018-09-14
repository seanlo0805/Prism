using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutputModule.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public ViewAViewModel()
        {
            Content = "";
            //Message = "View A from your Prism Module";
        }
    }
}
