using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class ImportFileCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public ImportFileCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ImportFile")
                {
                    Debug.Print("command: ImportFile triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command ImportFile: can execute?");
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"ImportFile command: trying to execute ImportFile-Button");
        }
    }
}
