using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Swe2_tour_planer.Models.MapQuestJson;

namespace Swe2_tour_planer.Models
{
    public class TourEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _tourID;
        private string _title;
        private string _description;
        private string _imgSource;
        private string _from;
        private string _too;
        private string _tourdistance;
        private ObservableCollection<CustomManeuvers> _maneuvers = new ObservableCollection<CustomManeuvers>();

        [JsonConstructor]
        public TourEntry(int tourID, string title, string description, string _imgSource, string from, string too, string Jsonmaneuvers,string tourdistance)
        {
            this.Title = title;
            this.Description = description;
            this.ImgSource = _imgSource;
            this._tourID = tourID;
            this.From = from;
            this.Too = too;
            this.TourDistance = tourdistance;
            Maneuvers = JsonConvert.DeserializeObject<ObservableCollection<CustomManeuvers>>(Jsonmaneuvers ?? "");
        }
        public TourEntry(int tourID, string title, string description, string _imgSource, string from, string too, List<CustomManeuvers> maneuvers, string tourdistance)
        {
            this.Title = title;
            this.Description = description;
            this.ImgSource = _imgSource;
            this._tourID = tourID;
            this.From = from;
            this.Too = too;
            this.TourDistance = tourdistance;
            maneuvers.ForEach(x => Maneuvers.Add(x));
        }
        public string TourDistanceInKm
        {
            get
            {
                float inMiles = float.Parse(this.TourDistance);
                float withMath = (inMiles * 1.609344f);
                return withMath.ToString("0.00"); ;
            }
            set
            {
                return;
            }
         }
        public ObservableCollection<CustomManeuvers> Maneuvers
        {
            get => this._maneuvers;
            set
            {
                this._maneuvers = value;
                this.OnPropertyChanged();
            }
        }
        public string Too
        {
            get => this._too;
            set
            {
                this._too = value;
                this.OnPropertyChanged();
            }
        }

        public string TourDistance
        {
            get => this._tourdistance;
            set
            {
                this._tourdistance = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(TourDistanceInKm));
            }
        }

        public string From
        {
            get => this._from;
            set
            {
                this._from = value;
                this.OnPropertyChanged();
            }
        }


        public int TourID
        {
            get => this._tourID;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
