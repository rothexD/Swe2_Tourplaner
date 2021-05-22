using Swe2_tour_planer.Services;
using Swe2_tour_planer.ViewModels;
using System;
using System.Diagnostics;
using System.Windows.Input;


namespace Swe2_tour_planer.Commands
{
    class SaveNewTourCommand : ICommand
    {
        private readonly AddTourViewModel _addTourViewModel;
        public event EventHandler? CanExecuteChanged;
        private readonly HomeViewModel _HomeViewModel;
        private ServicesAccess _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SaveNewTourCommand(AddTourViewModel tourViewModel, HomeViewModel home, ServicesAccess service)
        {
            _HomeViewModel = home;
            this._addTourViewModel = tourViewModel;
            this._service = service;
            _addTourViewModel.PropertyChanged += (sender, args) =>
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
            if (string.IsNullOrWhiteSpace(_addTourViewModel.InputTitle))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_addTourViewModel.InputDescription))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_addTourViewModel.InputFrom))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_addTourViewModel.InputTo))
            {
                return false;
            }
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                var a = await _service.AddNewTourAsync(_addTourViewModel.InputTitle, _addTourViewModel.InputFrom, _addTourViewModel.InputTo, _addTourViewModel.InputDescription);
                log.Info($"Added new Tour by Command success TourID: {a.ToString()}");
                _addTourViewModel.Statuscolor = "Green";
                _addTourViewModel.Statusmessage = "";
                _addTourViewModel.InputTitle = "";
                _addTourViewModel.InputDescription = "";
                _addTourViewModel.InputFrom = "";
                _addTourViewModel.InputTo = "";
                _HomeViewModel.OnPropertyChanged("ListTourEntryRefresh");
                _HomeViewModel.SwitchView.Execute("HomeView");
            }
            catch (Exception e)
            {
                log.Info("Added new Tour by Command failed");
                _addTourViewModel.Statuscolor = "Red";
                _addTourViewModel.Statusmessage = "Failed add Last Tour ";
                log.Error(e.StackTrace);
            }

        }
    }
}
