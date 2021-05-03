using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using Swe2_tour_planer.helpers;

namespace Swe2_tour_planer.Commands
{
    class RemoveTourCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RemoveTourCommand(HomeViewModel homeViewModel)
        {

            this._homeViewModel = homeViewModel;
            _homeViewModel.PropertyChanged += (sender, args) =>
            {
            };
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                if (_homeViewModel.CurrentActiveTour.TourID == Int32.Parse(parameter.ToString()))
                {
                    _homeViewModel.CurrentActiveTour = null;
                }
                await Databasehelper.RemoveTour(Int32.Parse(parameter.ToString()));
                log.Info($"Removed Tour with Id:{Int32.Parse(parameter.ToString())} succesfully");
                _homeViewModel.OnPropertyChanged("ListTourEntryRefresh");             
            }
            catch{
                log.Info($"Failed to remove Tour with Id:{Int32.Parse(parameter.ToString())}");
            }
        }
    }
}
