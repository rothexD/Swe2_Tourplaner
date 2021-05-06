using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;


namespace Swe2_tour_planer.Commands
{
    class UpdateTourRelay : ICommand
    {
        private readonly MainViewModel _anyViewModell;
        private readonly HomeViewModel _home;
        public event EventHandler? CanExecuteChanged;

        public UpdateTourRelay(MainViewModel anyViewModel, HomeViewModel home)
        {

            this._anyViewModell = anyViewModel;
            this._home = home;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _anyViewModell.ChangeTourToUpdate(_home.Data.ToList().Find(x => x.TourID == Int32.Parse(parameter.ToString())));
            _anyViewModell.RequestChangeViewModel("UpdateTour");
        }
    }
}

