using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels; 

namespace Swe2_tour_planer.Commands
{
    class SearchBarCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;

        public SearchBarCommand(HomeViewModel homeViewModel)
        {

            this._homeViewModel = homeViewModel;
            _homeViewModel.PropertyChanged += (sender, args) =>
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
            return !string.IsNullOrWhiteSpace(_homeViewModel.Searchbar);
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"Searchbar command: try execute{_homeViewModel.Searchbar}");
            _homeViewModel.Searchbar = string.Empty;
        }

    }
}
