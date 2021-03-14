using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class SaveNewTourLogCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public SaveNewTourLogCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SaveNewTourLog")
                {
                    Debug.Print("command: SaveNewTourLog triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command SaveNewTourLog: can execute?");
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"SaveNewTourLog command: trying to execute SaveNewTourLog");
        }
    }
}
