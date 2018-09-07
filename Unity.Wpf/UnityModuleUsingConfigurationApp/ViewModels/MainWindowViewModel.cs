using Prism.Mvvm;

namespace UnityModuleUsingConfigurationApp.ViewModels
{
    /// <summary>
    /// 
    /// reference: https://dotblogs.com.tw/keigen/2017/07/17/091302
    ///            https://vulcanlee.gitbooks.io/xamarin-forms-develop-notes/GroupNotes5/ViewCoding.html
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
