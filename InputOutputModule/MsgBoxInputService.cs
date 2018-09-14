using CalculatorInterfaces;
using InputOutputModule.ViewModels;
using InputOutputModule.Views;
using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinTools.ViewModels;
using WinTools.Views;

namespace InputOutputModule
{
    public class MsgBoxInputService : IInputService
    {
        //WinTools.Views.InputDialog dlg = new WinTools.Views.InputDialog("");
        public string ReadDialog(string header)
        {
            InputDialog dlg = new InputDialog("");
            Nullable<bool> result = dlg.ShowDialog();
            if (result != null && result.Value == true)
            {
                string content = ((InputDialogViewModel)dlg.DataContext).Content;
            }
            //InputDialog dlg = new InputDialog(header);
            //dlg.ShowDialog();
            //if (dlg.DataContext is InputDialogViewModel)
            //{
            //    return ((InputDialogViewModel)dlg.DataContext).Content;
            //}
            //else
            return "";

        }
        public String ReadCommand()
        {
            return ReadDialog("Command");
        }

        public Arguments ReadArguments()
        {
            int x = int.Parse(ReadDialog("X value"));
            int y = int.Parse(ReadDialog("Y value")) ;
            return new Arguments() { X = x, Y = y };
        }
    }
}
