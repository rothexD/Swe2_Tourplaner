using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Windows.Input;


namespace Swe2_tour_planer.Commands
{
    class RemoveLogCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        public ServicesAccess _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RemoveLogCommand(HomeViewModel homeViewModel, ServicesAccess service)
        {

            this._homeViewModel = homeViewModel;
            _service = service;
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
                await _service.RemoveLogAsync(Int32.Parse(parameter.ToString()));
                log.Info($"Removed Log with Id:{Int32.Parse(parameter.ToString())} succesfully");
                _homeViewModel.OnPropertyChanged("ListTourEntryRefresh");
                
            }
            catch
            {
                log.Info($"Failed to remove Log with Id:{Int32.Parse(parameter.ToString())}");
            }
        }
    }
}
