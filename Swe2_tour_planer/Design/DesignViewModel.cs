using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Swe2_tour_planer.DesignViewModel
{
    class DesignMainViewModel : MainViewModel
    {

        public ICommand DemoSwitchTourCommand { get; }
        public DesignMainViewModel()
        {
            this.DemoSwitchTourCommand = new DemoSwitchTourCommand(this);
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

           
        }
    }
}
