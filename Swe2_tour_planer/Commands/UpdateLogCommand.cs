using Swe2_tour_planer.Models;
using Swe2_tour_planer.Services;
using Swe2_tour_planer.Validation;
using Swe2_tour_planer.ViewModels;
using System;
using System.Windows.Input;


namespace Swe2_tour_planer.Commands
{
    class UpdateLogCommand : ICommand
    {
        private readonly UpdateLogViewModel _UpdateLogViewModel;
        private readonly HomeViewModel _HomeViewModel;
        private readonly SwitchViewCommand _SwitchView;
        private ServicesAccess _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public event EventHandler? CanExecuteChanged;
        private readonly AlphaNumvericValidation validator = new AlphaNumvericValidation();
        public UpdateLogCommand(UpdateLogViewModel updateLogViewModel, HomeViewModel home, SwitchViewCommand switchView, ServicesAccess service)
        {

            this._UpdateLogViewModel = updateLogViewModel;
            this._HomeViewModel = home;
            _SwitchView = switchView;
            _service = service;
            _UpdateLogViewModel.PropertyChanged += (sender, args) =>
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
        }

        public bool CanExecute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Date.ToString()))
            {
                return false;
            }
            if (!validator.Validate(_UpdateLogViewModel.Duration, null).IsValid)
            {
                return false;
            }
            if (!validator.Validate(_UpdateLogViewModel.Distance, null).IsValid)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Rating))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Report))
            {
                return false;
            }
            if (!validator.Validate(_UpdateLogViewModel.AverageSpeed, null).IsValid)
            {
                return false;
            }
            if (!validator.Validate(_UpdateLogViewModel.EnergyUsed, null).IsValid)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Wheater))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Traffic))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.NicenessOfLocals))
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
                UpdateLogCommand.log.Debug(_UpdateLogViewModel.Date);
                var log = new LogEntry(_UpdateLogViewModel.LogBeforeChanges.LogID, _UpdateLogViewModel.LogBeforeChanges.TourID,
                    _UpdateLogViewModel.Date,
                    _UpdateLogViewModel.Duration,
                    _UpdateLogViewModel.Distance,
                    _UpdateLogViewModel.Rating,
                    _UpdateLogViewModel.Report,
                    _UpdateLogViewModel.AverageSpeed,
                    _UpdateLogViewModel.EnergyUsed,
                    _UpdateLogViewModel.Wheater,
                    _UpdateLogViewModel.Traffic,
                    _UpdateLogViewModel.NicenessOfLocals
                    );

                await _service.UpdateLogAsync(log);
                _HomeViewModel.OnPropertyChanged("CurrentActiveLogsRefresh");

                UpdateLogCommand.log.Info("Update Logentry success");
                _SwitchView.Execute("HomeView");
            }
            catch
            {


                _UpdateLogViewModel.Statuscolor = "red";
                _UpdateLogViewModel.Statusmessage = "Failed to Update log";
                UpdateLogCommand.log.Info("Update new Logentry failed");
            }
        }
    }
}
