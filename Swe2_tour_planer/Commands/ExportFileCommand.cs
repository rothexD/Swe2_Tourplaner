using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class ExportFileCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public ExportFileCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ExportFile")
                {
                    Debug.Print("command: ExportFile triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command ExportFile: can execute?");
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"ExportFile ExportFile: trying to execute ExportFileButton");
        }
    }
}
