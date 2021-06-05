using Microsoft.Extensions.Configuration;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class RemoveTourCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private ServicesAccess _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        public RemoveTourCommand(HomeViewModel homeViewModel, ServicesAccess service)
        {

            this._homeViewModel = homeViewModel;
            this._service = service;
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
                var Tourentry = _homeViewModel.ListLogsAndTours.First(x => x.Tour.TourID == Int32.Parse(parameter.ToString()));
                await _service.RemoveTourAsync(Tourentry, Int32.Parse(parameter.ToString()));
                log.Info($"Removed Tour with Id:{Int32.Parse(parameter.ToString())} succesfully");
                _homeViewModel.OnPropertyChanged("ListTourEntryRefresh");
            }
            catch (Exception e)
            {
                log.Info($"Failed to remove Tour with Id:{Int32.Parse(parameter.ToString())}");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
            }
        }
    }
}
