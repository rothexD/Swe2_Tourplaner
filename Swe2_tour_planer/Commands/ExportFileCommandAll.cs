using Microsoft.Win32;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class ExportFileCommandAll : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private ServicesAccess _services;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExportFileCommandAll(HomeViewModel homeViewModel, ServicesAccess services)
        {

            this._homeViewModel = homeViewModel;
            this._services = services;
        }

        public bool CanExecute(object parameter)
        {
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
                    await this._services.ExportFileAsync(saveFileDialog.FileName, _homeViewModel.ListLogsAndTours.ToList());
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
