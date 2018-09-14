using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnityDialogModule.Notification;

namespace UnityDialogModule.ViewModels
{
    public class ViewAViewModel : BindableBase, IInteractionRequestAware
    {

        public Action FinishInteraction { get; set; }

        private IEditBoxNotification _notification;

        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IEditBoxNotification)value); }
        }

        public string Title { get; set; }
        public string EditContent { get; set; }

        public DelegateCommand OkCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public ViewAViewModel()
        {
            OkCommand = new DelegateCommand(OkInteraction);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        private void CancelInteraction()
        {
            _notification.EditContent = "";
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void OkInteraction()
        {
            _notification.EditContent = EditContent;
            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }

    }
}
