using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class SaveNewTourCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public SaveNewTourCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SaveNewTour")
                {
                    Debug.Print("command: SaveNewTour triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command SaveNewTour: can execute?");
            return !string.IsNullOrWhiteSpace(_mainViewModel.Searchbar);
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"SaveNewTour command: trying  to execute SaveNewTour-Button");
            _mainViewModel.Searchbar = string.Empty;
        }
    }
}
