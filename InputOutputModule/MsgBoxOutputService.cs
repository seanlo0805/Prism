using CalculatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InputOutputModule
{
    public class MsgBoxOutputService : IOutputService
    {
        public void WriteMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
