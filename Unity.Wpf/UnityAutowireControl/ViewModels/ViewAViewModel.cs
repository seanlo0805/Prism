using EventObjects;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityAutowireControl.ViewModels
{
    /// <summary>
    /// 1. 用Visual Studio Template 新建的Prism(Unity) WPF Control , 它的View/ViewMode預設就有AutowireViewModel=true
    /// 2. Autowire優點是同名ViewModel不用一個個設定DataContext(有不同就要設定)
    /// 3.         缺點是在Design畫面就沒辦法顯示binding的結果, (雖然你可以在AutowireViewModel=true時也設定DataContext 就會出現, 但這樣就失去 Autowire的意思了)
    /// 4. 手動Autowire要注意命名, ViewA -> ViewAViewModel; TCoreView -> TCoreViewViewModel
    ///                     在xaml要設定AutowirViewModel=true
    ///                         =>可以用xmlns:prism="http://prismlibrary.com/"  
    ///                         =>或    xmlns:prism="clr-namespace:Prism.Mvvm;assembly=Prism.Wpf"
    ///                     View跟ViewModel的namespace可要注意, xxx.Views --> xxx.ViewModels
    ///  
    /// BindableBase: 主要是讓每個ViewModel可以不省去不用實作 INotifyPropertyChanged
    ///               1. OnPropertyChanged(見 <code>TCoreTest.ViewModel.AbstractViewModel</code> 使用RaisePropertyChangedEvent) 不用每個ViewModel都寫這個
    ///               2. 本來自己實作 INotifyPropertyChanged 時都要在property改變時執行 OnPropertyChanged, 改成在setter時呼叫 SetProperty
    ///               
    /// reference: http://brianlagunas.com/getting-started-prisms-new-viewmodellocator/
    ///            https://dotblogs.com.tw/keigen/2017/07/17/091302
    ///            https://vulcanlee.gitbooks.io/xamarin-forms-develop-notes/GroupNotes5/ViewCoding.html
    /// </summary>
    public class ViewAViewModel : BindableBase
    {
        #region member data
        IEventAggregator _ea;
        private string _message;
        #endregion // member data

        #region properties
        public DelegateCommand SendMessageCommand
        {
            get; private set;
        }
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion //properties

        #region member function
        //you could get IEventAggregator (Dependency Inject) only when you implement ViewModel Autowiring otherwise you should pass ea youself(See UnitTCoreControl and UnityReceptControl)
        public ViewAViewModel(IEventAggregator ea)
        {
            Message = "This is ViewA";
            _ea = ea;
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(DateTime.Now.ToString("HH:mm:ss")+ " => [from] " + Message);
        }
        #endregion //member function
    }
}
