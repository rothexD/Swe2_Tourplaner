using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;


namespace Swe2_tour_planer.Commands
{
    class SwitchViewCommand : ICommand
    {
        private readonly MainViewModel _anyViewModell;
        public event EventHandler? CanExecuteChanged;

        public SwitchViewCommand(MainViewModel anyViewModel)
        {

            this._anyViewModell = anyViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _anyViewModell.RequestChangeViewModel(parameter.ToString());
        }
    }
}

