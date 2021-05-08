using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using Swe2_tour_planer.Model;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Logik;

namespace Swe2_tour_planer.Commands
{
    class UpdateLogCommand : ICommand
    {
        private readonly UpdateLogViewModel _UpdateLogViewModel;
        private readonly HomeViewModel _HomeViewModel;
        private readonly SwitchViewCommand _SwitchView;
        private Services _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public event EventHandler? CanExecuteChanged;

        public UpdateLogCommand(UpdateLogViewModel updateLogViewModel, HomeViewModel home,SwitchViewCommand switchView,Services service)
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
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Date))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Duration))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.Distance))
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
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.AverageSpeed))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateLogViewModel.EnergyUsed))
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

                await _service.UpdateLog(log);
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
