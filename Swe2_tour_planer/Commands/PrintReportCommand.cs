using Microsoft.Win32;
using Swe2_tour_planer.Models;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class PrintReportCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly ReportViewModel _reportviewmodel;
        public event EventHandler? CanExecuteChanged;
        private ServicesAccess _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PrintReportCommand(HomeViewModel homeViewModel, ServicesAccess service)
        {

            this._homeViewModel = homeViewModel;
            this._service = service;
            _homeViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentActiveTour")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            if (_homeViewModel.CurrentActiveTour == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                var tourItem = _homeViewModel.CurrentActiveTour;
                var logsItem = _homeViewModel.CurrentActiveLogs.ToList() ?? new List<LogEntry>();
                LogsAndTours item = new LogsAndTours()
                {
                    Tour = tourItem,
                    Logs = logsItem
                };


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.Filter = "Portable Document Format | *.pdf";
                if (saveFileDialog.ShowDialog() == true)
                {
                    _service.PrintReport(saveFileDialog.FileName, item);
                }
                log.Info("successfully created pdf");
            }
            catch (Exception e)
            {
                log.Error("failed to create pdf");
                log.Debug(e.Message);
            }
        }
    }
}
