using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Logik;
using System;

namespace Swe2_tour_planer.ViewModels
{
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private BaseViewModel _selectedViewModel;
        private Dictionary<string,BaseViewModel> ViewList = new Dictionary<string,BaseViewModel>();
        public event PropertyChangedEventHandler PropertyChanged;
        private UpdateTourViewModel _updateTourViewModel;
        private UpdateLogViewModel _updateLogViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            private set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }
        public MainViewModel()
        {
            var service = new Services(new DatabaseHelper(),new MapQuestApiHelper(),new ImportExporthelper(),new DinkToPdfClass());
            var home = new HomeViewModel(this, service);
           
            ViewList.Add("HomeView", home);
            ViewList.Add("AddLogEntryView", new AddLogEntryViewModel(this, home, service));
            ViewList.Add("AddTourView", new AddTourViewModel(this,home, service));
            ViewList.Add("ExportView", new ExportViewModel(this,home, service));
            ViewList.Add("ImportView", new ImportViewModel(this,home, service));
            ViewList.Add("ReportView", new ReportViewModel(this,home, service));

            _updateLogViewModel = new UpdateLogViewModel(this, home, service);
            ViewList.Add("UpdateLog", _updateLogViewModel);

            _updateTourViewModel = new UpdateTourViewModel(this, home, service);
            ViewList.Add("UpdateTour", _updateTourViewModel);
            new DatabaseHelper(true);
            RequestChangeViewModel("HomeView");
        }
        public void RequestChangeViewModel(string ViewName)
        {
            try
            {
                ViewList.TryGetValue(ViewName, out BaseViewModel value);
                if(value == null){
                    return;
                }
                SelectedViewModel = value;
            }
            catch(Exception e)
            {

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
