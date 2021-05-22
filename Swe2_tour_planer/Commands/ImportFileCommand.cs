using Microsoft.Win32;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class ImportFileCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private ServicesAccess _services;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ImportFileCommand(HomeViewModel home, ServicesAccess services)
        {

            this._homeViewModel = home;
            this._services = services;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    ValidateNames = false // this will allow paths over 260 characters
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    await _services.ImportFileAsync(openFileDialog.FileName);
                }
                _homeViewModel.OnPropertyChanged("ListTourEntryRefresh");
                log.Info("import from file success");
            }
            catch (Exception e)
            {
                log.Error("Could not import file");
                log.Debug(e.Message);
                return;
            }
        }
    }
}
