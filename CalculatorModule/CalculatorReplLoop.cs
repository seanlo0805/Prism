using CalculatorInterfaces;
using CalculatorModule.Views;
using Microsoft.Practices.ServiceLocation;
using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityDialogModule.Views;

namespace CalculatorModule
{
    public class CalculatorReplLoop : ICalculatorReplLoop
    {
        IInputService inputService;
        List<IOutputService> outputServices;
        ICalculator calculator;
        IInputParserService parsingService;

        public CalculatorReplLoop()
        {

        }

        public CalculatorReplLoop(ICalculator calculator, IServiceLocator container, IInputService inputService, IInputParserService inputParserService)
        {
            this.calculator = calculator;
            this.inputService = inputService;
            outputServices = new List<IOutputService>(container.GetAllInstances<IOutputService>());
            parsingService = inputParserService;
        }

        public void Run()
        {
            CalculatorWindow win = new CalculatorWindow();
            ICalculatorDataContext dataContext = (ICalculatorDataContext)win.DataContext;
            dataContext.InputParserService = parsingService;
            dataContext.CalculatorHandler = calculator;
            win.ShowDialog();

            //while (true)
            //{
            //    string command = inputService.ReadCommand();
            //    try
            //    {
            //        CommandTypes commandType = parsingService.ParseCommand(command);
            //        Arguments args = inputService.ReadArguments();
            //        WriteToAllOutputMessage(calculator.Execute(commandType, args).ToString());
            //    }
            //    catch
            //    {
            //        WriteToAllOutputMessage("Mistake !");
            //    }
            //}
        }

        private void WriteToAllOutputMessage(string message)
        {
            outputServices.ForEach(n => n.WriteMessage(message));
        }
    }
}
