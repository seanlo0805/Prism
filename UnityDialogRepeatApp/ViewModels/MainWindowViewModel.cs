using CalculatorInterfaces;
using Microsoft.Practices.ServiceLocation;
using NonUICommonDll;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;
using UnityDialogModule.Notification;

namespace UnityDialogRepeatApp.ViewModels
{
    /// <summary>
    /// 在代碼內直接寫入模組(In Code), 便利之處在於可以在模組Initialize()之前, 就可以使用該class
    /// 以下示範在一開始(MainWindow 在Loaded事件時)就使用Modules的class, 此時按Prism的作業, IModule.Initialize()並未開始
    /// 為了達到此一目的, 必須在Bootstrapper就先把相關的class先Register進去, 見<code>Bootstrapper.ConfigureContainer()</code>
    /// </summary>
    public class MainWindowViewModel : BindableBase
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

        public DelegateCommand<KeyEventArgs> KeyEventCommand { get; set; }
        public DelegateCommand<MouseButtonEventArgs> MouseEventCommand { get; set; }
        public DelegateCommand<Window> LoadedWindowCommand { get; set; }
        public DelegateCommand<Window> ClosingWindowCommand { get; set; }
        public DelegateCommand<Window> WindowStateChangeCommand { get; set; }

        //public InteractionRequest<IEditBoxNotification> CalculatorCommandNotificationRequest { get; set; }
        //public DelegateCommand CalculatorCommand { get; set; }

        public InteractionRequest<ICalculatorSelectNotification> CalculatorCommandNotificationRequest { get; set; }
        public DelegateCommand CalculatorCommand { get; set; }

        public InteractionRequest<IEditBoxNotification> Argument1NotificationRequest { get; set; }
        public DelegateCommand Argument1Command { get; set; }

        public InteractionRequest<IEditBoxNotification> Argument2NotificationRequest { get; set; }
        public DelegateCommand Argument2Command { get; set; }

        public InteractionRequest<INotification> ResultNotificationRequest { get; set; }
        public DelegateCommand ResultCommand { get; set; }

        public DelegateCommand ExecuteCommand { get; set; }
        public ICalculator CalculatorHandler { get { return _calculatorHandler; } set { _calculatorHandler = value; } }
        public IInputParserService InputParserService { get { return _parsingService; } set { _parsingService = value; } }

        public MainWindowViewModel() : this(ServiceLocator.Current.GetInstance<IInputParserService>(), ServiceLocator.Current.GetInstance<ICalculator>())
        {
        }
        public MainWindowViewModel(IInputParserService inputParserService, ICalculator calculator)
        {
            MouseEventCommand = new DelegateCommand<MouseButtonEventArgs>(MouseDownEventHandler);
            KeyEventCommand = new DelegateCommand<KeyEventArgs>(KeyDownEventHandler);
            LoadedWindowCommand = new DelegateCommand<Window>(OnLoaded);
            ClosingWindowCommand = new DelegateCommand<Window>(OnClosing);
            WindowStateChangeCommand = new DelegateCommand<Window>(OnWindowStateChange);

            InputParserService = inputParserService;
            CalculatorHandler = calculator;

            CalculatorCommandNotificationRequest = new InteractionRequest<ICalculatorSelectNotification>();
            CalculatorCommand = new DelegateCommand(RaiseCalculatorCommandInteraction);

            Argument1NotificationRequest = new InteractionRequest<IEditBoxNotification>();
            Argument1Command = new DelegateCommand(RaiseArgument1Interaction);

            Argument2NotificationRequest = new InteractionRequest<IEditBoxNotification>();
            Argument2Command = new DelegateCommand(RaiseArgument2Interaction);

            ResultNotificationRequest = new InteractionRequest<INotification>();
            ResultCommand = new DelegateCommand(RaiseResultInteraction);

            ExecuteCommand = new DelegateCommand(DoCalculating);
        }

        //anyone who press keyboard
        public void KeyDownEventHandler(KeyEventArgs args)
        {
           
        }
        //anyone who click mouse
        public void MouseDownEventHandler(MouseButtonEventArgs args)
        {

        }
        public void OnClosing(Window window)
        {
           
        }

        //state change when double click task bar icon
        public void OnWindowStateChange(Window win)
        {
            Views.MainWindow window = (Views.MainWindow)win;
            if (window.WindowState == WindowState.Minimized)
            {
                window.ShowInTaskbar = false;
                window.NotifyIcon.BalloonTipTitle = "Minimize Sucessful";
                window.NotifyIcon.BalloonTipText = "Minimized the app ";
                window.NotifyIcon.ShowBalloonTip(400);
                window.NotifyIcon.Visible = true;
            }
            else if (window.WindowState == WindowState.Normal)
            {
                window.NotifyIcon.Visible = false;
                window.ShowInTaskbar = true;
            }
        }
        public void OnLoaded(Window window)
        {

            //cause MainWindow minimizing to taskbar as a icon
            window.WindowState = WindowState.Minimized;

            RaiseCalculatorCommandInteraction();
            RaiseArgument1Interaction();
            RaiseArgument2Interaction();
            RaiseResultInteraction();

        }

        private void DoCalculating()
        {
            try
            {
                CommandTypes commandType = InputParserService.ParseCommand(Calculator);
                Arguments args = new Arguments() { X = InputParserService.ParseArgument(Argument1), Y = InputParserService.ParseArgument(Argument2) };
                
                Title = $"{Argument1} {Calculator} {Argument2} = {CalculatorHandler.Execute(commandType, args).ToString()}";
            }
            catch
            {
                Title = "Mistake !";
            }

        }

        private void RaiseResultInteraction()
        {
            try
            {
                CommandTypes commandType = InputParserService.ParseCommand(Calculator);
                Arguments args = new Arguments() { X = InputParserService.ParseArgument(Argument1), Y = InputParserService.ParseArgument(Argument2) };
                ResultNotificationRequest.Raise(new Notification { Title = "Rsult", Content = $"{Argument1} {Calculator} {Argument2} = {CalculatorHandler.Execute(commandType, args).ToString()}" }, r =>
                {
                    Title = $"{Argument1} {Calculator} {Argument2} = {CalculatorHandler.Execute(commandType, args).ToString()}";
                }); 
            }
            catch
            {
                ResultNotificationRequest.Raise(new Notification { Title = "Mistack", Content = $"Mistake" }, r => Title = "Mistake");
            }
       }

        private void RaiseCalculatorCommandInteraction()
        {
            CalculatorCommandNotificationRequest.Raise(new CalculatorSelectNotification { Title = "Select a Command Type" }, r =>
            {
                if (r.Confirmed && r.SelectedCalculator != null)
                {
                    Calculator = r.SelectedCalculator;
                    Title = $"The formula: {Argument1} {Calculator} {Argument2} ";
                }
                else
                    Title = $"Wrong input, The formula: {Argument1} {Calculator} {Argument2} ";
            });
        }

        private void RaiseArgument1Interaction()
        {
            Argument1NotificationRequest.Raise(new EditBoxNotification { Title = "Argument1(Integer)" }, r =>
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
            Argument2NotificationRequest.Raise(new EditBoxNotification { Title = "Argument2(Integer)" }, r =>
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
