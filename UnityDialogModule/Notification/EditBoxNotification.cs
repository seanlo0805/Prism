using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDialogModule.Notification
{
    public class EditBoxNotification : Confirmation, IEditBoxNotification
    {
        public string EditContent { get; set; }
        public EditBoxNotification()
        {
            EditContent = "";
        }
    }
}
