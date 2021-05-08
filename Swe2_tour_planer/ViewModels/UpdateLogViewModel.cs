using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Swe2_tour_planer.Logik;

namespace Swe2_tour_planer.ViewModels
{
    public class UpdateLogViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel;
        private readonly HomeViewModel _homeviewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveLogCommand { get; }
        public ICommand SwitchView { get; }
        public ICommand UpdateLogRelay { get; }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UpdateLogViewModel(MainViewModel main, HomeViewModel home, Services service)
        {
            _mainviewModel = main;
            _homeviewModel = home;
            SwitchView = new SwitchViewCommand(_mainviewModel);
            SaveLogCommand = new UpdateLogCommand(this, _homeviewModel, new SwitchViewCommand(_mainviewModel),service);        
        }

        private string _date;
        private string _duration;
        private string _distance;
        private string _rating;
        private string _report;

        private string _averageSpeed;
        private string _energyUsed;
        private string _wheater;
        private string _traffic;
        private string _nicenessOfLocals;
        private string _statusmessage = "";
        private string _statuscolor = "Gray";

        private LogEntry _logBeforeChanges;


        public LogEntry LogBeforeChanges
        {
            get => this._logBeforeChanges;
            set
            {
                if(value == null)
                {
                    return;
                }
                this._logBeforeChanges = value;
                Date = value.Date;
                Duration = value.Date;
                Distance = value.Date;
                Rating = value.Date;
                Report = value.Date;
                AverageSpeed = value.Date;
                EnergyUsed = value.Date;
                Wheater = value.Date;
                Traffic = value.Date;
                Traffic = value.Date;
                NicenessOfLocals = value.NicenessOfLocals;
                this.OnPropertyChanged();
            }
        }
        public string Date
        {
            get => this._date;
            set
            {
                this._date = value;
                this.OnPropertyChanged();
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
        public string Duration
        {
            get => this._duration;
            set
            {
                this._duration = value;
                this.OnPropertyChanged();
            }
        }
        public string Distance
        {
            get => this._distance;
            set
            {
                this._distance = value;
                this.OnPropertyChanged();
            }
        }
        public string Rating
        {
            get => this._rating;
            set
            {
                this._rating = value;
                this.OnPropertyChanged();
            }
        }
        public string Report
        {
            get => this._report;
            set
            {
                this._report = value;
                this.OnPropertyChanged();
            }
        }
        public string AverageSpeed
        {
            get => this._averageSpeed;
            set
            {
                this._averageSpeed = value;
                this.OnPropertyChanged();
            }
        }
        public string EnergyUsed
        {
            get => this._energyUsed;
            set
            {
                this._energyUsed = value;
                this.OnPropertyChanged();
            }
        }
        public string Wheater
        {
            get => this._wheater;
            set
            {
                this._wheater = value;
                this.OnPropertyChanged();
            }
        }
        public string Traffic
        {
            get => this._traffic;
            set
            {
                this._traffic = value;
                this.OnPropertyChanged();
            }
        }
        public string NicenessOfLocals
        {
            get => this._nicenessOfLocals;
            set
            {
                this._nicenessOfLocals = value;
                this.OnPropertyChanged();
            }
        }
    }
}
