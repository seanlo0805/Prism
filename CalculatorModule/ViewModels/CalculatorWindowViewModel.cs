using CalculatorInterfaces;
using Microsoft.Practices.ServiceLocation;
using NonUICommonDll;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityDialogModule.Notification;

namespace CalculatorModule.ViewModels
{
    class CalculatorWindowViewModel : BindableBase, ICalculatorDataContext
    {
        private string _calculator = "";
        private string _argument1 = "";
        private string _argument2 = "";
        private string _title = "Prism Unity Application";
        ICalculator _calculatorHandler;
        IInputParserService _parsingService;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string Calculator
        {
            get { return _calculator; }
            set { SetProperty(ref _calculator, value); }
        }
        public string Argument1
        {
            get { return _argument1; }
            set { SetProperty(ref _argument1, value); }
        }
        public string Argument2
        {
            get { return _argument2; }
            set { SetProperty(ref _argument2, value); }
        }


        public InteractionRequest<IEditBoxNotification> CalculatorCommandNotificationRequest { get; set; }
        public DelegateCommand CalculatorCommand { get; set; }

        public InteractionRequest<IEditBoxNotification> Argument1NotificationRequest { get; set; }
        public DelegateCommand Argument1Command { get; set; }

        public InteractionRequest<IEditBoxNotification> Argument2NotificationRequest { get; set; }
        public DelegateCommand Argument2Command { get; set; }

        public DelegateCommand ExecuteCommand { get; set; }
        public ICalculator CalculatorHandler { get { return _calculatorHandler; } set { _calculatorHandler = value; } }
        public IInputParserService InputParserService { get { return _parsingService; } set { _parsingService = value; } }

        public CalculatorWindowViewModel()
        {

            CalculatorCommandNotificationRequest = new InteractionRequest<IEditBoxNotification>();
            CalculatorCommand = new DelegateCommand(RaiseCalculatorCommandInteraction);

            Argument1NotificationRequest = new InteractionRequest<IEditBoxNotification>();
            Argument1Command = new DelegateCommand(RaiseArgument1Interaction);

            Argument2NotificationRequest = new InteractionRequest<IEditBoxNotification>();
            Argument2Command = new DelegateCommand(RaiseArgument2Interaction);

            ExecuteCommand = new DelegateCommand(DoCalculating);
        }
        private void DoCalculating()
        {
            try
            {
                CommandTypes commandType = InputParserService.ParseCommand(Calculator);
                Arguments args = new Arguments() { X = InputParserService.ParseArgument(Argument1), Y = InputParserService.ParseArgument(Argument2) };
                //WriteToAllOutputMessage(calculator.Execute(commandType, args).ToString());
                Title = $"{Argument1} {Calculator} {Argument2} = {CalculatorHandler.Execute(commandType, args).ToString()}";
            }
            catch
            {
                Title="Mistake !";
            }

        }

        private void RaiseCalculatorCommandInteraction()
        {
            CalculatorCommandNotificationRequest.Raise(new EditBoxNotification { Title = "Add, Sub, Mul or Div" }, r =>
            {
                if (r.Confirmed && r.EditContent != null)
                {
                    Calculator = r.EditContent;
                    Title = $"The formula: {Argument1} {Calculator} {Argument2} ";
                }
                else
                    Title = $"Wrong input, The formula: {Argument1} {Calculator} {Argument2} ";
            });
        }

        private void RaiseArgument1Interaction()
        {
            CalculatorCommandNotificationRequest.Raise(new EditBoxNotification { Title = "Argument1(Integer)" }, r =>
            {
                if (r.Confirmed && r.EditContent != null)
                {
                    _argument1 = r.EditContent;
                    Title = $"The formula: {Argument1} {Calculator} {Argument2} ";
                }
                else
                    Title = $"Wrong input, The formula: {Argument1} {Calculator} {Argument2} ";
            });
        }

        private void RaiseArgument2Interaction()
        {
            CalculatorCommandNotificationRequest.Raise(new EditBoxNotification { Title = "Argument2(Integer)" }, r =>
            {
                if (r.Confirmed && r.EditContent != null)
                {
                    _argument2 = r.EditContent;
                    Title = $"The formula: {Argument1} {Calculator} {Argument2} ";
                }
                else
                    Title = $"Wrong input, The formula: {Argument1} {Calculator} {Argument2} ";
            });
        }
    }
}
