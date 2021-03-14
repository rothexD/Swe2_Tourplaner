using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Swe2_tour_planer
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TourEntry> Data { get; } = new ObservableCollection<TourEntry>();
        public ICommand ExportCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand PrintReportCommand { get; }
        public ICommand SaveNewTourCommand { get; }
        public ICommand SaveNewTourLogCommand { get; }

        public ICommand SearchbarCommand { get; }

        private string _searchbar;
        private TourEntry _currentActiveTour;

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
                OnPropertyChanged(nameof(CurrentActiveTour.LogEntries));
            }
        }

        public MainViewModel()
        {
            Debug.Print("ctor MainViewModel");

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");

            this.ExportCommand = new ExportFileCommand(this);
            this.ImportCommand = new ImportFileCommand(this);
            this.PrintReportCommand = new PrintReportCommand(this);
            this.SaveNewTourCommand = new SaveNewTourCommand(this);
            this.SaveNewTourLogCommand = new SaveNewTourLogCommand(this);
            this.SearchbarCommand = new SearchBarCommand(this);
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
