using Microsoft.Extensions.Configuration;
using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Models;
using Swe2_tour_planer.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Swe2_tour_planer.ViewModels
{
    public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        private readonly MainViewModel _main;

        private ObservableCollection<LogsAndTours> _listLogsAndTours { get; set; } = new ObservableCollection<LogsAndTours>();
        private readonly ServicesAccess _services;
        public ObservableCollection<TourEntry> _data { get; set; } = new ObservableCollection<TourEntry>();
        public ICommand RemoveTourCommand { get; }
        public ICommand SearchbarCommand { get; }
        public ICommand RemoveLogCommand { get; }
        public ICommand SwitchView { get; }
        public ICommand UpdateLogRelay { get; }
        public ICommand SaveNewLogCommandRelay { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommandAll { get; }
        public ICommand ExportCommandCurrent { get; }
        public ICommand ReportCommand { get; }

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
        public byte[] ImgSourceWithLocation
        {
            get
            {
                string ImageUri = "default.jpg";
                if (CurrentActiveTour != null && CurrentActiveTour.ImgSource != null)
                {
                    ImageUri = config["MapQuest:Location"] + CurrentActiveTour.ImgSource;
                }
                var bytes = _services.ImageBytes(ImageUri);
                return bytes;
            }
        }
        public List<LogEntry> CurrentActiveLogs
        {
            get
            {
                return _services.CurrentActiveLogs(ListLogsAndTours.ToList(), CurrentActiveTour);
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
                if(value == null)
                {
                    if (Data.ToList().Contains(CurrentActiveTour)){
                        return;
                    }
                }
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
            var logsAndTours = await _services.ListLogsAndToursAsync();
            ListLogsAndTours.Clear();
            logsAndTours.ForEach(x => ListLogsAndTours.Add(x));
            FillData(ListLogsAndTours.ToList());
        }
        private void FillData(List<LogsAndTours> fill)
        {
            Data.Clear();
            fill.ForEach(x => Data.Add(x.Tour));
            this.OnPropertyChanged(nameof(CurrentActiveLogs));
        }
        public HomeViewModel(MainViewModel main, ServicesAccess service)
        {
            Debug.Print("ctor MainViewModel");
            _main = main;

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");

            this.SwitchView = new SwitchViewCommand(main);
            this.RemoveTourCommand = new RemoveTourCommand(this, service);
            this.RemoveLogCommand = new RemoveLogCommand(this, service);
            this.UpdateTourRelay = new UpdateTourRelay(main, this);
            this.UpdateLogRelay = new UpdateLogRelay(main, this);
            this.SaveNewLogCommandRelay = new SaveNewLogCommandRelay(main, this);
            this.ImportCommand = new ImportFileCommand(this, service);
            this.ExportCommandAll = new ExportFileCommandAll(this, service);
            this.ExportCommandCurrent = new ExportFileCommandCurrent(this, service);
            this.ReportCommand = new PrintReportCommand(this, service);
            _services = service;
            getAllToursAndLogs();

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ListTourEntryRefresh")
                {
                    getAllToursAndLogs();
                }
                if (args.PropertyName == "ListLogRefresh")
                {
                }
                if (args.PropertyName == "CurrentActiveTourChanged")
                {
                    getAllToursAndLogs();
                }
                if (args.PropertyName == "CurrentActiveLogsRefresh")
                {
                    getAllToursAndLogs();
                }
                if (args.PropertyName == "Searchbar")
                {
                    SearchFunction();
                }
            };
        }
        private void SearchFunction()
        {
            if (Searchbar == "")
            {
                FillData(ListLogsAndTours.ToList());
                return;
            }
            var list = _services.SearchAsync(ListLogsAndTours.ToList(), Searchbar);
            FillData(list);
        }


        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
