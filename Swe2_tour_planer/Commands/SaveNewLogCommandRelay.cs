using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;


namespace Swe2_tour_planer.Commands
{
    class SaveNewLogCommandRelay : ICommand
    {
        private readonly MainViewModel _anyViewModell;
        private readonly HomeViewModel _home;
        public event EventHandler? CanExecuteChanged;

        public SaveNewLogCommandRelay(MainViewModel anyViewModel, HomeViewModel home)
        {

            this._anyViewModell = anyViewModel;
            this._home = home;
            _home.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentActiveTour")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            if(_home.CurrentActiveTour == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            _anyViewModell.RequestChangeViewModel("AddLogEntryView");
        }
    }
}

