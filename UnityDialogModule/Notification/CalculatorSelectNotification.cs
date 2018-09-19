using NonUICommonDll;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDialogModule.Notification
{
    public class CalculatorSelectNotification : Confirmation, ICalculatorSelectNotification
    {
        IList<string> _calculator;
        public IList<string> Calculators
        {
            get
            {
                return _calculator;
            }
            private set
            {
                _calculator = value;
            }
        }
        public string SelectedCalculator { get; set; }

        public CalculatorSelectNotification()
        {
            this.Calculators = new List<string>();
            this.SelectedCalculator = null;

            CreateItems();
        }

        private void CreateItems()
        {
            IEnumerable<CommandTypes> types = Enum.GetValues(typeof(CommandTypes)).Cast<CommandTypes>();
            foreach(CommandTypes type in types)
            {
                Calculators.Add(type.ToString());
            }
        }
    }
}
