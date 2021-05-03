using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using System.IO;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Model;
using System.Collections.Generic;

namespace Swe2_tour_planer.Commands
{
    class ImportFileCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly ImportViewModel _importViewModel;
        public event EventHandler? CanExecuteChanged;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ImportFileCommand(ImportViewModel importViewModel, HomeViewModel home)
        {

            this._homeViewModel = home;
            this._importViewModel = importViewModel;
            _importViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ImportPath")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_importViewModel.ImportPath))
            {
                return false;
            }
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (!File.Exists(_importViewModel.ImportPath))
            {
                log.Error($"File does not exist path: {_importViewModel.ImportPath}");
                return;
            }
            if(parameter.ToString() == "Overwrite")
            {
                await Databasehelper.RemoveAllLog();
                await Databasehelper.RemoveAllTour();
            }
            try
            {
                List<LogsAndTours> list = ImportExporthelper.ImportFromJsonFile(_importViewModel.ImportPath);
                list.ForEach(async x =>
                {
                    var id = await x.Tour.AddTourToDatabase();
                    x.Logs.ForEach(async y =>
                    {
                        await y.AddLogToDatabase(id);
                    });
                });
                log.Info("import from file success");
                _homeViewModel.OnPropertyChanged("ListTourEntryRefresh");
                if (parameter.ToString() == "Overwrite")
                {
                    _homeViewModel.CurrentActiveTour = null;
                }
            }         
            catch(Exception e)
            {
                log.Error("Could not import file");
                log.Debug(e.Message);
                return;
            }       
        }
    }
}
