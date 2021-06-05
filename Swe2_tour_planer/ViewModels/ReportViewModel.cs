using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Swe2_tour_planer.ViewModels
{
    public class ReportViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel;
        private readonly HomeViewModel _homeViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SwitchView { get; }
        public ICommand ReportCommand { get; }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReportViewModel(MainViewModel main, HomeViewModel home, ServicesAccess service)
        {
            _mainviewModel = main;
            _homeViewModel = home;
            SwitchView = new SwitchViewCommand(main);
            ReportCommand = new PrintReportCommand(home, service);
        }
        private string _statusmessage = "";
        private string _statuscolor = "Gray";
        private string _saveReportToPath = "";
        public string SaveReportToPath
        {
            get
            {
                return _saveReportToPath;
            }
            set
            {
                if (_saveReportToPath != value)
                {
                    _saveReportToPath = value;
                    OnPropertyChanged(nameof(SaveReportToPath));
                }
            }
        }
        public string Statusmessage
        {
            get
            {
                return _statusmessage;
            }
            set
            {
                if (_statusmessage != value)
                {
                    _statusmessage = value;
                    OnPropertyChanged(nameof(Statusmessage));
                }
            }
        }
        public string Statuscolor
        {
            get
            {
                return _statuscolor;
            }
            set
            {
                if (_statuscolor != value)
                {
                    _statuscolor = value;
                    OnPropertyChanged(nameof(Statuscolor));
                }
            }
        }
    }
}
