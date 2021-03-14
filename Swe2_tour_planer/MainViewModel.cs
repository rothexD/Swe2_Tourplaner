using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Swe2_tour_planer.Model;

namespace Swe2_tour_planer
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TourEntry> Data { get; } = new ObservableCollection<TourEntry>();
        public ICommand ExecuteCommand { get; }

        public MainViewModel()
        {
            Debug.Print("ctor MainViewModel");

            #region Simpler Solution

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");
            var item = new ObservableCollection<LogEntry>();
            item.Add(new LogEntry("12.07.2020", 150, 120, 5, "ein toller ausflug", 5, 2, "cloudy", "busy", 4));               
            Data.Add(new TourEntry("hans", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("franz", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));

            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));

            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));

            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));

            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));

            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));

            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            Data.Add(new TourEntry("guenter", "auf reisen", "panda.jpg", item));
            #endregion
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
