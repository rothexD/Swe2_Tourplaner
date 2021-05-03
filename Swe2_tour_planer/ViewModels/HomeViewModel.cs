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

namespace Swe2_tour_planer.ViewModels
{
    public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        private readonly MainViewModel _main;
        public ObservableCollection<TourEntry> Data { get; } = new ObservableCollection<TourEntry>();
        public ICommand RemoveTourCommand { get; }
        public ICommand SearchbarCommand { get; }
        public ICommand RemoveLogCommand { get; }
        public ICommand SwitchView { get; }

        private string _searchbar;
        private TourEntry _currentActiveTour;
        private ObservableCollection<LogEntry> _currentLogs = new ObservableCollection<LogEntry>();
        public string ImgSourceWithLocation
        {
            get {
                if(CurrentActiveTour == null)
                {
                    return null;
                } 
                if (CurrentActiveTour.ImgSource == null)
                {
                    return null;
                }
                return config["MapQuest:Location"] + CurrentActiveTour.ImgSource;      
                }
        }
        public ObservableCollection<LogEntry> CurrentActiveLogs
        {
            get
            {
                return _currentLogs;
            }
            set
            {
                if (_currentLogs != value)
                {
                    _currentLogs = value;

                    OnPropertyChanged(nameof(CurrentActiveLogs));
                }
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
            }
        }
        public async void fillData()
        {
            Data.Clear();
            var i = await Databasehelper.GetListOfTours();
            foreach (var item in i)
            {
                Data.Add(item);
            }
        }
        public async void fillLogs()
        {
            if(_currentActiveTour == null){
                return;
            }
            CurrentActiveLogs.Clear();
            var i = await Databasehelper.GetListOfLogs(_currentActiveTour.TourID);
            foreach (var item in i)
            {
                CurrentActiveLogs.Add(item);
            }
        }

    public HomeViewModel(MainViewModel main)
        {
            Debug.Print("ctor MainViewModel");
            _main = main;

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");

            this.SearchbarCommand = new SearchBarCommand(this);
            this.SwitchView = new SwitchViewCommand(_main);
            this.RemoveTourCommand = new RemoveTourCommand(this);
            this.RemoveLogCommand = new RemoveLogCommand(this);
            fillData();


            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ListTourEntryRefresh")
                {
                    fillData();
                }
                if (args.PropertyName == "ListLogRefresh")
                {
                }
                if (args.PropertyName == "CurrentActiveTour")
                {
                    fillLogs();
                }
                if (args.PropertyName == "CurrentActiveLogsRefresh")
                {
                    fillLogs();
                }
            };
        }

  

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
