using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.Model;
using Swe2_tour_planer.ViewModels;
using Swe2_tour_planer.helpers;
using Newtonsoft.Json;
using System.IO;
using Swe2_tour_planer.Logik;

namespace Swe2_tour_planer.Commands
{
    class UpdateTourCommand : ICommand
    {
        private readonly UpdateTourViewModel _UpdateTourViewModel;
        public event EventHandler? CanExecuteChanged;
        private readonly HomeViewModel _HomeViewModel;
        private readonly SwitchViewCommand _SwitchView;
        private readonly Services _service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public UpdateTourCommand(UpdateTourViewModel tourViewModel, HomeViewModel home,SwitchViewCommand switchView,Services service)
        {
            _HomeViewModel = home;
            this._UpdateTourViewModel = tourViewModel;
            _SwitchView = switchView;
            _service = service;
            _UpdateTourViewModel.PropertyChanged += (sender, args) =>
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
            if (string.IsNullOrWhiteSpace(_UpdateTourViewModel.InputTitle))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateTourViewModel.InputDescription))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateTourViewModel.InputFrom))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_UpdateTourViewModel.InputTo))
            {
                return false;
            }
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                int z;
                if(_HomeViewModel.CurrentActiveTour!= null)
                {
                    z = _HomeViewModel.CurrentActiveTour.TourID;
                }
                else
                {
                    z = -1;
                }
                log.Debug(_UpdateTourViewModel.InputFrom + " " + _UpdateTourViewModel.InputTo);
                var Tour = await _service.UpdateTour(_UpdateTourViewModel.TourBeforeChanges.TourID, _UpdateTourViewModel.InputTitle, _UpdateTourViewModel.InputDescription, _UpdateTourViewModel.InputFrom, _UpdateTourViewModel.InputTo, _UpdateTourViewModel.TourBeforeChanges.ImgSource);

                _HomeViewModel.OnPropertyChanged("ListTourEntryRefresh");
                if (_UpdateTourViewModel.TourBeforeChanges.TourID == z)
                {
                    _HomeViewModel.CurrentActiveTour = Tour;
                }            
                //_UpdateTourViewModel.TourBeforeChanges = null;
                _SwitchView.Execute("HomeView");
            }
            catch (Exception e)
            {
                log.Info("Update of Tour by Command failed");
                _UpdateTourViewModel.Statuscolor = "Red";
                _UpdateTourViewModel.Statusmessage = "Failed Update Last Tour ";
                log.Error(e.StackTrace);
            }

        }
    }
}
