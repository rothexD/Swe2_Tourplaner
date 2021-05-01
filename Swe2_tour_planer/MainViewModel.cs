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

        private string _inputTitle;
        private string _inputDescription;
        private string _inputFrom;
        private string _inputTo;
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
        public string InputTitle
        {
            get
            {
                return _inputTitle;
            }
            set
            {
                if (InputTitle != value)
                {
                    Debug.Print("set _inputTitle");
                    _inputTitle = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputTitle));
                }
            }
        }
        public string InputDescription
        {
            get
            {
                return _inputDescription;
            }
            set
            {
                if (_inputDescription != value)
                {
                    Debug.Print("set _inputDescription");
                    _inputDescription = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputDescription));
                }
            }
        }
        public string InputTo
        {
            get
            {
                return _inputTo;
            }
            set
            {
                if (_inputTo != value)
                {
                    Debug.Print("set _inputTo");
                    _inputTo = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputTo));
                }
            }
        }
        public string InputFrom
        {
            get
            {
                return _inputFrom;
            }
            set
            {
                if (_inputFrom != value)
                {
                    Debug.Print("set _inputFrom");
                    _inputFrom = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputFrom));
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
