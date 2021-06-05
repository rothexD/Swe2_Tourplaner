using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Swe2_tour_planer.Models
{
    public class LogEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _date;
        private string _duration;
        private string _distance;
        private string _rating;
        private string _report;

        private string _averageSpeed;
        private string _energyUsed;
        private string _wheater;
        private string _traffic;
        private string _nicenessOfLocals;


        private int _logID { get; set; }
        private int _tourID_fk { get; set; }

        public LogEntry(int logID, int tourID, DateTime date, string duration, string distance, string rating, string report, string averageSpeed, string energyUsed, string wheater, string traffic, string nicenessOfLocals)
        {
            _logID = logID;
            Date = date;
            Duration = duration;
            Distance = distance;
            Rating = rating;
            Report = report;
            AverageSpeed = averageSpeed;
            EnergyUsed = energyUsed;
            Wheater = wheater;
            Traffic = traffic;
            NicenessOfLocals = nicenessOfLocals;
            _tourID_fk = tourID;
        }
        public int LogID
        {
            get => this._logID;
        }
        public int TourID
        {
            get => this._tourID_fk;
        }
        public DateTime Date
        {
            get => this._date;
            set
            {
                this._date = value;
                this.OnPropertyChanged();
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
