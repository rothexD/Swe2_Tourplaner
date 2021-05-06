using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Swe2_tour_planer.helpers;
using Microsoft.Extensions.Configuration;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swe2_tour_planer.ViewModels
{
    public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        private readonly MainViewModel _main;

        private ObservableCollection<LogsAndTours> _listLogsAndTours { get; set; } = new ObservableCollection<LogsAndTours>();
        public ObservableCollection<TourEntry> _data { get; set; } = new ObservableCollection<TourEntry>();
        public ICommand RemoveTourCommand { get; }
        public ICommand SearchbarCommand { get; }
        public ICommand RemoveLogCommand { get; }
        public ICommand SwitchView { get; }
        public ICommand UpdateLogRelay { get; }
        public ICommand SaveNewLogCommandRelay { get; }

        private string _searchbar;
        private TourEntry _currentActiveTour;

        public ICommand UpdateTourRelay { get; }

        public ObservableCollection<TourEntry> Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data != value)
                {
                    _data = value;

                    OnPropertyChanged(nameof(Data));
                }
            }
        }
        public ObservableCollection<LogsAndTours> ListLogsAndTours
        {
            get
            {
                return _listLogsAndTours;
            }
            set
            {
                if (_listLogsAndTours != value)
                {
                    _listLogsAndTours = value;

                    OnPropertyChanged(nameof(ListLogsAndTours));
                    OnPropertyChanged(nameof(Data));
                }
            }
        }
        public string ImgSourceWithLocation
        {
            get {
                if(CurrentActiveTour == null)
                {
                    return "";
                } 
                if (CurrentActiveTour.ImgSource == null)
                {
                    return "";
                }
                return config["MapQuest:Location"] + CurrentActiveTour.ImgSource;      
                }
        }
        public List<LogEntry> CurrentActiveLogs
        {
            get
            {
               return ListLogsAndTours.ToList().Find(x => { if (x != null) {
                       var z = CurrentActiveTour == null ? -1 : CurrentActiveTour.TourID;
                       return x.Tour.TourID == z; } else { return false; } }) != null ? ListLogsAndTours.ToList().Find(x => {
                           var z = CurrentActiveTour == null ? -1 : CurrentActiveTour.TourID;
                           return x.Tour.TourID == z; }).Logs : new List<LogEntry>();
            }
        }

        public string Searchbar
        {
            get
            {
                return _searchbar;
            }
            set
            {
                if (_searchbar != value)
                {
                    Debug.Print("set Input-value");
                    _searchbar = value;

                    Debug.Print("fire propertyChanged: Searchbar");
                    OnPropertyChanged(nameof(Searchbar));
                }
            }
        }
        
        public TourEntry CurrentActiveTour
        {
            get
            {
                return _currentActiveTour;
            }
            set
            {
                Debug.Print("set _currentActiveTour");
                _currentActiveTour = value;

                Debug.Print("fire propertyChanged: CurrentActiveTour");
                OnPropertyChanged(nameof(CurrentActiveTour));
                OnPropertyChanged(nameof(CurrentActiveTour.Title));
                OnPropertyChanged(nameof(CurrentActiveTour.Description));
                OnPropertyChanged(nameof(CurrentActiveTour.ImgSource));
                OnPropertyChanged(nameof(ImgSourceWithLocation));
                OnPropertyChanged(nameof(CurrentActiveTour.From));
                OnPropertyChanged(nameof(CurrentActiveTour.Too));
                OnPropertyChanged(nameof(CurrentActiveTour.Maneuvers));
                OnPropertyChanged(nameof(CurrentActiveLogs));
            }
        }
        public async void getAllToursAndLogs()
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
            ListLogsAndTours.Clear();
            logsAndTours.ForEach(x => ListLogsAndTours.Add(x));
            FillData(ListLogsAndTours.ToList());
        }
        private void FillData(List<LogsAndTours> fill)
        {
            Data.Clear();
            fill.ForEach(x => Data.Add(x.Tour));
        }
        public HomeViewModel(MainViewModel main)
        {
            Debug.Print("ctor MainViewModel");
            _main = main;

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");

            this.SearchbarCommand = new SearchBarCommand(this);
            this.SwitchView = new SwitchViewCommand(main);
            this.RemoveTourCommand = new RemoveTourCommand(this);
            this.RemoveLogCommand = new RemoveLogCommand(this);
            this.UpdateTourRelay = new UpdateTourRelay(main, this);
            this.UpdateLogRelay = new UpdateLogRelay(main, this);
            this.SaveNewLogCommandRelay = new SaveNewLogCommandRelay(main, this);
            getAllToursAndLogs();

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ListTourEntryRefresh")
                {
                    getAllToursAndLogs();
                    FillData(ListLogsAndTours.ToList());
                }
                if (args.PropertyName == "ListLogRefresh")
                {
                }
                if (args.PropertyName == "CurrentActiveTourChanged")
                {
                    getAllToursAndLogs();
                    FillData(ListLogsAndTours.ToList());
                }
                if (args.PropertyName == "CurrentActiveLogsRefresh")
                {
                    getAllToursAndLogs();
                    FillData(ListLogsAndTours.ToList());
                }
                if (args.PropertyName == "Searchbar")
                {
                    SearchFunction();
                }
            };
        }
        private void SearchFunction()
        {
            if(Searchbar == "")
            {
                FillData(ListLogsAndTours.ToList());
                return;
            }
            var list = ListLogsAndTours.Where(x =>
            {
                {
                    if (x.Tour.Title.Contains(Searchbar)) { return true; }
                    if (x.Tour.From.Contains(Searchbar)) { return true; }
                    if (x.Tour.Too.Contains(Searchbar)) { return true; }
                    if (x.Tour.Description.Contains(Searchbar)) { return true; }
                    bool inLogs = false;
                    x.Logs.ForEach(y =>
                    {
                        if (inLogs) { return; }
                        if (y.Date.Contains(Searchbar)) { inLogs = true; }
                        if (y.Duration.Contains(Searchbar)) { inLogs = true; }
                        if (y.Distance.Contains(Searchbar)) { inLogs = true; }
                        if (y.Rating.Contains(Searchbar)) { inLogs = true; }
                        if (y.EnergyUsed.Contains(Searchbar)) { inLogs = true; }
                        if (y.Wheater.Contains(Searchbar)) { inLogs = true; }
                        if (y.NicenessOfLocals.Contains(Searchbar)) { inLogs = true; }
                        if (y.AverageSpeed.Contains(Searchbar)) { inLogs = true; }
                        if (y.Report.Contains(Searchbar)) { inLogs = true; }
                        if (y.Traffic.Contains(Searchbar)) { inLogs = true; }
                    });
                    if (inLogs) { return true; }
                    return false;
                }
            }).ToList();

            FillData(list);
        }
  

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
