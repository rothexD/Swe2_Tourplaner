using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class PrintReportCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public PrintReportCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "PrintReport")
                {
                    Debug.Print("command: PrintReport triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command PrintReport: can execute?");
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"PrintReport command: trying to execute PrintReport-Button");
        }
    }
}
