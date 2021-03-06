using Swe2_tour_planer.Models;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.Validation;
using Swe2_tour_planer.ViewModels;
using System;
using System.Windows.Input;

namespace Swe2_tour_planer.Commands
{
    class SaveNewTourLogCommand : ICommand
    {
        private readonly AddLogEntryViewModel _AddlogViewModel;
        private readonly HomeViewModel _HomeViewModel;
        private ServicesAccess _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public event EventHandler? CanExecuteChanged;
        private readonly AlphaNumvericValidation validator = new AlphaNumvericValidation();
        public SaveNewTourLogCommand(AddLogEntryViewModel AddLogViewModel, HomeViewModel home, ServicesAccess service)
        {

            this._AddlogViewModel = AddLogViewModel;
            this._HomeViewModel = home;
            _service = service;
            _AddlogViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Date")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "Duration")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "Distance")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "Rating")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "Report")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "AverageSpeed")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "EnergyUsed")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "Wheater")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "Traffic")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "NicenessOfLocals")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                if (args.PropertyName == "NicenessOfLocals")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
            _AddlogViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentActiveTour")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_AddlogViewModel.Date.ToString()))
            {
                return false;
            }
            if (!validator.Validate(_AddlogViewModel.Duration, null).IsValid)
            {
                return false;
            }
            if (!validator.Validate(_AddlogViewModel.Distance, null).IsValid)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_AddlogViewModel.Rating))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_AddlogViewModel.Report))
            {
                return false;
            }
            if (!validator.Validate(_AddlogViewModel.AverageSpeed, null).IsValid)
            {
                return false;
            }
            if (!validator.Validate(_AddlogViewModel.EnergyUsed, null).IsValid)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_AddlogViewModel.Wheater))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_AddlogViewModel.Traffic))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_AddlogViewModel.NicenessOfLocals))
            {
                return false;
            }
            if (_HomeViewModel.CurrentActiveTour == null || _HomeViewModel.CurrentActiveTour.TourID == 0)
            {
                return false;
            }
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                var Log = new LogEntry(0, _HomeViewModel.CurrentActiveTour.TourID,
                    _AddlogViewModel.Date,
                    _AddlogViewModel.Duration,
                    _AddlogViewModel.Distance,
                    _AddlogViewModel.Rating,
                    _AddlogViewModel.Report,
                    _AddlogViewModel.AverageSpeed,
                    _AddlogViewModel.EnergyUsed,
                    _AddlogViewModel.Wheater,
                    _AddlogViewModel.Traffic,
                    _AddlogViewModel.NicenessOfLocals
                    );

                await _service.AddNewLogAsync(Log);



                _AddlogViewModel.Statuscolor = "Green";
                _AddlogViewModel.Statusmessage = "";
                _AddlogViewModel.Date = new DateTime();
                _AddlogViewModel.Duration = "";
                _AddlogViewModel.Distance = "";
                _AddlogViewModel.Rating = "";
                _AddlogViewModel.Report = "";
                _AddlogViewModel.AverageSpeed = "";
                _AddlogViewModel.EnergyUsed = "";
                _AddlogViewModel.Wheater = "";
                _AddlogViewModel.Traffic = "";
                _AddlogViewModel.NicenessOfLocals = "";
                log.Info("Adding new Logentry success");
                _HomeViewModel.OnPropertyChanged("CurrentActiveLogsRefresh");
                _HomeViewModel.SwitchView.Execute("HomeView");
            }
            catch
            {


                _AddlogViewModel.Statuscolor = "red";
                _AddlogViewModel.Statusmessage = "Failed to Add log";
                log.Info("Adding new Logentry failed");
            }
        }
    }
}
