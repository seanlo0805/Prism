using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDialogModule.Notification
{
    public interface ICalculatorSelectNotification : IConfirmation
    {
        string SelectedCalculator { get; set; }
    }
}
