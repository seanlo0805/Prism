using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using UnityDialogModule.Notification;

namespace UnityNonUIModuleStaticApp.ViewModels
{
    /// <summary>
    /// 在代碼內直接寫入模組(In Code), 便利之處在於可以在模組Initialize()之前, 就可以使用該class
    /// 以下示範在一開始(MainWindow 在Loaded事件時)就使用Modules的class, 此時按Prism的作業, IModule.Initialize()並未開始
    /// 為了達到此一目的, 必須在Bootstrapper就先把相關的class先Register進去, 見<code>Bootstrapper.ConfigureContainer()</code>
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public InteractionRequest<IEditBoxNotification> EditBoxNotificationRequest { get; set; }
        public DelegateCommand EditBoxDialogCommand { get; set; }

        public MainWindowViewModel()
        {
            EditBoxNotificationRequest = new InteractionRequest<IEditBoxNotification>();
            EditBoxDialogCommand = new DelegateCommand(RaiseEditBoxDialogInteraction);
        }

        private void RaiseEditBoxDialogInteraction()
        {
            EditBoxNotificationRequest.Raise(new EditBoxNotification { Title = "Custom Notification" }, r =>
            {
                if (r.Confirmed && r.EditContent != null)
                    Title = $"User input: { r.EditContent}";
                else
                    Title = "User cancelled or didn't select an item";
            });
        }
    }
}
