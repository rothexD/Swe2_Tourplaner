using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using System.IO;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Swe2_tour_planer.Commands
{
    class ExportFileCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly ExportViewModel _exportViewModel;
        public event EventHandler? CanExecuteChanged;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExportFileCommand(ExportViewModel exportViewModel,HomeViewModel homeViewModel)
        {

            this._homeViewModel = homeViewModel;
            this._exportViewModel = exportViewModel;
            _exportViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ExportPath")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(_exportViewModel.ExportPath))
            {
                return false;
            }
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                var allTours = await Databasehelper.GetListOfTours();
                List<LogsAndTours> logsAndTours = new List<LogsAndTours>();
                foreach (var TourFromList in allTours)
                {
                    var logs = await Databasehelper.GetListOfLogs(TourFromList.TourID);
                    List<LogEntry> logList = new List<LogEntry>();
                    foreach (var log in logs)
                    {
                        logList.Add(log);
                    }
                    logsAndTours.Add(new LogsAndTours
                    {
                        Logs = logList,
                        Tour = TourFromList
                    });
                }
                string output = JsonConvert.SerializeObject(logsAndTours);
                await File.WriteAllTextAsync(_exportViewModel.ExportPath, output);
                log.Info("Export of file success");
            }
            catch(Exception e)
            {
                log.Error("Export of file failed");
                log.Debug(e.Message);
            }
           
        }
    }
}
