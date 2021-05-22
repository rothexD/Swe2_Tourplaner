using Microsoft.Win32;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class ExportFileCommandCurrent : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private ServicesAccess _services;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExportFileCommandCurrent(HomeViewModel homeViewModel, ServicesAccess service)
        {

            this._homeViewModel = homeViewModel;
            this._services = service;
            _homeViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentActiveTour")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            if (_homeViewModel.CurrentActiveTour == null)
            {
                return false;
            }
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.Filter = "JavaScript Object Notation | *.json |Text Message | *.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    await _services.ExportFileAsync(saveFileDialog.FileName, _homeViewModel.ListLogsAndTours.Where(x => x.Tour.TourID == _homeViewModel.CurrentActiveTour.TourID).ToList());
                }
                log.Info("Export of file success");
            }
            catch (Exception e)
            {
                log.Error("Export of file failed");
                log.Debug(e.Message);
            }

        }
    }
}
