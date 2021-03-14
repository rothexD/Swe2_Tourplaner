using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;

namespace Swe2_tour_planer.Commands
{
    class DemoSwitchTourCommand : ICommand
    {
        private const string longtext = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.  ";
        private const string PandaPath = @"C:\Users\rothexD\Documents\GitHub\SWE-2_TourPlaner\Swe2_tour_planer\panda.jpg";
        private const string VillagePath = @"C:\Users\rothexD\Pictures\richi.jpg";
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;
        
        private static TourEntry Get_Tour1()
        {
             ObservableCollection<LogEntry> item = new ObservableCollection<LogEntry>();
            item.Add(new LogEntry("12.07.2020", 150, 120, 5, "ein toller ausflug", 5, 2, "cloudy", "busy", 4));
             return  new TourEntry("Example1", "Example1 short text,panda", PandaPath, item);
    }
        private static TourEntry Get_Tour2()
        {
            ObservableCollection<LogEntry> item = new ObservableCollection<LogEntry>();
            item.Add(new LogEntry("12.07.2020", 150, 120, 5, "ein toller ausflug", 5, 2, "cloudy", "busy", 4));
            return new TourEntry("Example2", $"{longtext},village,", VillagePath, item);
        }

        public DemoSwitchTourCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "DemoSwitchTourCommand")
                {
                    Debug.Print("command: ExportFile triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("Command DemoSwitchTourCommand: can execute?");
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.Print($"ExportFile DemoSwitchTourCommand: trying to execute DemoSwitchTourCommand");
            if(_mainViewModel.CurrentActiveTour == null)
            {
                _mainViewModel.CurrentActiveTour = Get_Tour2();
                return;
            }
            if(_mainViewModel.CurrentActiveTour.Title == "Example1")
            {
                _mainViewModel.CurrentActiveTour = Get_Tour2();
                return;
            }

            _mainViewModel.CurrentActiveTour = Get_Tour1();
            return;
        }
    }
}
