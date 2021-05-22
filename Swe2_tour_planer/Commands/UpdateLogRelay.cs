using Swe2_tour_planer.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;



namespace Swe2_tour_planer.Commands
{
    class UpdateLogRelay : ICommand
    {
        private readonly MainViewModel _anyViewModell;
        private readonly HomeViewModel _home;
        public event EventHandler? CanExecuteChanged;

        public UpdateLogRelay(MainViewModel anyViewModel, HomeViewModel home)
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

            return true;
        }

        public void Execute(object? parameter)
        {
            _anyViewModell.ChangeLogToUpdate(_home.CurrentActiveLogs.ToList().Find(x => x.LogID == Int32.Parse(parameter.ToString())));
            _anyViewModell.RequestChangeViewModel("UpdateLog");
        }
    }
}

