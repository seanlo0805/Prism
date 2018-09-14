using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using UnityDialogModule.Notification;

namespace UnityNonUIModuleStaticApp.ViewModels
{
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
