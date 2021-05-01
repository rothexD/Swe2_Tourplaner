using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.Model;

namespace Swe2_tour_planer.Commands
{
    class SaveNewTourCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public event EventHandler? CanExecuteChanged;

        public SaveNewTourCommand(MainViewModel mainViewModel)
        {

            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "InputTitle")
                {
                    Debug.Print($"command: {args.PropertyName} triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "InputDescription")
                {
                    Debug.Print($"command: {args.PropertyName} triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "InputFrom")
                {
                    Debug.Print($"command: {args.PropertyName} triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "InputTo")
                {
                    Debug.Print($"command: {args.PropertyName} triggered");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_mainViewModel.InputTitle)){
                return false;
            }
            if (string.IsNullOrWhiteSpace(_mainViewModel.InputDescription)){
                return false;
            }
            if (string.IsNullOrWhiteSpace(_mainViewModel.InputFrom))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_mainViewModel.InputTo))
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            _mainViewModel.Data.Add(new TourEntry(_mainViewModel.InputTitle, _mainViewModel.InputDescription, "bla", new System.Collections.ObjectModel.ObservableCollection<LogEntry>()));
            _mainViewModel.InputTitle = "";
            _mainViewModel.InputDescription = "";
            _mainViewModel.InputFrom = "";
            _mainViewModel.InputTo = "";
            Debug.Print($"SaveNewTourCommand: trying to execute SaveNewTourCommand");
        }
    }
}
