using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using System.IO;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Model;
using System.Collections.Generic;
using System.Linq;

namespace Swe2_tour_planer.Commands
{
    class PrintReportCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly ReportViewModel _reportviewmodel;
        public event EventHandler? CanExecuteChanged;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PrintReportCommand(ReportViewModel reportviewmodel, HomeViewModel homeViewModel)
        {

            this._homeViewModel= homeViewModel;
            this._reportviewmodel = reportviewmodel;
            _reportviewmodel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SaveReportToPath")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
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
            if (string.IsNullOrWhiteSpace(_reportviewmodel.SaveReportToPath))
            {
                return false;
            }
            if(_homeViewModel.CurrentActiveTour == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                var Tour = _homeViewModel.CurrentActiveTour;
                var Logs = _homeViewModel.CurrentActiveLogs.ToList() ?? new List<LogEntry>();

                string page = DinkToPdfClass.TourAndLogToHtml(Tour, Logs);
                DinkToPdfClass.CreatePDFFromHtml(page, _reportviewmodel.SaveReportToPath);
                log.Info("successfully created pdf");
            }
            catch(Exception e)
            {
                log.Error("failed to create pdf");
                log.Debug(e.Message);
            }        
        }
    }
}
