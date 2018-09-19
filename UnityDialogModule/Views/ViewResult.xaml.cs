using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows.Controls;

namespace UnityDialogModule.Views
{
    /// <summary>
    /// Interaction logic for ViewResult
    /// </summary>
    public partial class ViewResult : UserControl, IInteractionRequestAware
    {
        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }
        public ViewResult()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FinishInteraction?.Invoke();
        }
    }
}
