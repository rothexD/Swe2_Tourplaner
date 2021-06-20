using Swe2_tour_planer.Models;
using Swe2_tour_planer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Swe2_tour_planer.ViewModels
{
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private BaseViewModel _selectedViewModel;
        private Dictionary<string, BaseViewModel> ViewList = new Dictionary<string, BaseViewModel>();
        public event PropertyChangedEventHandler PropertyChanged;
        private UpdateTourViewModel _updateTourViewModel;
        private UpdateLogViewModel _updateLogViewModel;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            private set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }
        public MainViewModel(bool shouldConfigDatabase = true)
        {
            try
            {
                var fileSystem = new FileSystemAccess();
                var service = new ServicesAccess(new Database(shouldConfigDatabase), new MapQuestApi(fileSystem), fileSystem, new DinkToPdfClass());
                var home = new HomeViewModel(this, service);

                ViewList.Add("HomeView", home);
                ViewList.Add("AddLogEntryView", new AddLogEntryViewModel(this, home, service));
                ViewList.Add("AddTourView", new AddTourViewModel(this, home, service));
                ViewList.Add("ExportView", new ExportViewModel(this, home, service));
                ViewList.Add("ImportView", new ImportViewModel(this, home, service));
                ViewList.Add("ReportView", new ReportViewModel(this, home, service));

                _updateLogViewModel = new UpdateLogViewModel(this, home, service);
                ViewList.Add("UpdateLog", _updateLogViewModel);

                _updateTourViewModel = new UpdateTourViewModel(this, home, service);
                ViewList.Add("UpdateTour", _updateTourViewModel);
                RequestChangeViewModel("HomeView");
            }catch(Exception e){
                log.Error("could not create MainviewModel, maybe database not running");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                Environment.Exit(1);
            }
         
        }
        public void RequestChangeViewModel(string ViewName)
        {
            try
            {
                ViewList.TryGetValue(ViewName, out BaseViewModel value);
                if (value == null)
                {
                    return;
                }
                SelectedViewModel = value;
            }
            catch (Exception e)
            {
                log.Error("failed to switch view");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
            }
        }
        public void ChangeTourToUpdate(TourEntry tour)
        {
            _updateTourViewModel.TourBeforeChanges = tour;
        }
        public void ChangeLogToUpdate(LogEntry tour)
        {
            _updateLogViewModel.LogBeforeChanges = tour;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
