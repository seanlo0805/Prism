using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityDialogModule.Notification;

namespace UnityDialogModule.ViewModels
{
    class StandalongWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public InteractionRequest<IEditBoxNotification> EditBoxNotificationRequest { get; set; }
        public DelegateCommand EditBoxDialogCommand { get; set; }

        public StandalongWindowViewModel()
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
