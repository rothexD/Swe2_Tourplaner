using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class SearchBarCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public SearchBarCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Searchbar")
                {
                    Debug.Print("command: Seachrbar triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command Searchbar: can execute?");
            return !string.IsNullOrWhiteSpace(_mainViewModel.Searchbar);
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"Searchbar command: try execute{_mainViewModel.Searchbar}");
            _mainViewModel.Searchbar = string.Empty;
        }

    }
}
