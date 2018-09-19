using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityDialogModule.Notification;

namespace UnityDialogModule.ViewModels
{
    public class ViewCommandSelectorViewModel : BindableBase, IInteractionRequestAware
    {

        private ICalculatorSelectNotification _notification;


        public string SelectedCalculator { get; set; }

        public DelegateCommand SelectCalculatorCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public Action FinishInteraction { get; set; }

        public IList<string> Items
        {
            get {
                List<string> items = new List<string>();
                items.Add("1");
                items.Add("2");
                return items;
            }
        }

        public INotification Notification
        {
            get {
                return _notification;
            }
            set {
                SetProperty(ref _notification, (ICalculatorSelectNotification)value);
            }
        }
        public ViewCommandSelectorViewModel()
        {
            SelectCalculatorCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);

        }
        private void CancelInteraction()
        {
            _notification.SelectedCalculator = null;
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void AcceptSelectedItem()
        {
            _notification.SelectedCalculator = SelectedCalculator;
            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }
    }
}
