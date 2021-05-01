using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Swe2_tour_planer.helpers;

namespace Swe2_tour_planer.DesignViewModel
{
    class DesignMainViewModel : MainViewModel
    {

        public ICommand DemoSwitchTourCommand { get; }
        public DesignMainViewModel()
        {
            this.DemoSwitchTourCommand = new DemoSwitchTourCommand(this);
            var item = new ObservableCollection<LogEntry>();
            item.Add(new LogEntry(1,2,"12.07.2020", "150", "120", "5", "ein toller ausflug", "5", "2", "cloudy", "busy", "4"));
            Data.Add(new TourEntry(1,"hans", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1,"franz", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
            Data.Add(new TourEntry(1, "guenter", "auf reisen", "panda.jpg"));
        }
    }
}
