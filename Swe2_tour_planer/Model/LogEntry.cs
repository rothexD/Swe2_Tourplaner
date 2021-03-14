using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Swe2_tour_planer.Model
{
    class LogEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _date;
        private int _duration;
        private float _distance;
        private int _rating;
        private string _report;

        private float _averageSpeed;
        private int _energyUsed;
        private string _wheater;
        private string _traffic;
        private int _nicenessOfLocals;

        public LogEntry(string date, int duration, float distance, int rating, string report, float averageSpeed, int energyUsed, string wheater, string traffic, int nicenessOfLocals)
        {

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
        public int Duration
        {
            get => this._duration;
            set
            {
                this._duration = value;
                this.OnPropertyChanged();
            }
        }
        public float Distance
        {
            get => this._distance;
            set
            {
                this._distance = value;
                this.OnPropertyChanged();
            }
        }
        public int Rating
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
        public float AverageSpeed
        {
            get => this._averageSpeed;
            set
            {
                this._averageSpeed = value;
                this.OnPropertyChanged();
            }
        }
        public int EnergyUsed
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
        public int NicenessOfLocals
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
