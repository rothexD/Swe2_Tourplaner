using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Swe2_tour_planer.Model
{
    class TourEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private string _description;
        private string _imgSource;
        private ObservableCollection<LogEntry> _logEntries;
        public TourEntry(string title, string description, string _imgSource, ObservableCollection<LogEntry> logEntries)
        {
            this.Title = title;
            this.Description = description;
            this.ImgSource = _imgSource;
            this.LogEntries = logEntries;
        }

        public string Title
        {
            get => this._title;
            set
            {
                this._title = value;
                this.OnPropertyChanged();
            }
        }
        public string Description
        {
            get => this._description;
            set
            {
                this._description = value;
                this.OnPropertyChanged();
            }
        }
        public string ImgSource
        {
            get => this._imgSource;
            set
            {
                this._imgSource = value;
                this.OnPropertyChanged();
            }
        }
        public ObservableCollection<LogEntry> LogEntries
        {
            get => this._logEntries;
            set
            {
                this._logEntries = value;
                this.OnPropertyChanged();
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
