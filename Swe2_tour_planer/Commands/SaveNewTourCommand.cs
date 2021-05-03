using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.Model;
using Swe2_tour_planer.ViewModels;
using Swe2_tour_planer.helpers;
using Newtonsoft.Json;

namespace Swe2_tour_planer.Commands
{
    class SaveNewTourCommand : ICommand
    {
        private readonly AddTourViewModel _addTourViewModel;
        public event EventHandler? CanExecuteChanged;
        private readonly HomeViewModel _HomeViewModel;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SaveNewTourCommand(AddTourViewModel tourViewModel,HomeViewModel home)
        {
            _HomeViewModel = home;
            this._addTourViewModel = tourViewModel;
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
            if (string.IsNullOrWhiteSpace(_addTourViewModel.InputTitle)){
                return false;
            }
            if (string.IsNullOrWhiteSpace(_addTourViewModel.InputDescription)){
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
                var Route = await mapQuestApiHelper.getRoute(_addTourViewModel.InputFrom, _addTourViewModel.InputTo);
                var Location = await mapQuestApiHelper.getMapImage(_addTourViewModel.InputFrom, _addTourViewModel.InputTo);

                var Tour = new TourEntry(0, _addTourViewModel.InputTitle, _addTourViewModel.InputDescription, Location, _addTourViewModel.InputFrom, _addTourViewModel.InputTo, JsonConvert.SerializeObject(Route));
                var a = await Tour.AddTourToDatabase();
                log.Info($"Added new Tour by Command success TourID: {a.ToString()}");
                _addTourViewModel.Statuscolor = "Green";
                _addTourViewModel.Statusmessage = "Added Last Tour successfully";
                _addTourViewModel.InputTitle = "";
                _addTourViewModel.InputDescription = "";
                _addTourViewModel.InputFrom = "";
                _addTourViewModel.InputTo = "";
                _HomeViewModel.OnPropertyChanged("ListTourEntryRefresh");
            }
            catch(Exception e)
            {
                log.Info("Added new Tour by Command failed");
                _addTourViewModel.Statuscolor = "Red";
                _addTourViewModel.Statusmessage = "Failed add Last Tour ";
                log.Error(e.StackTrace);
            }
            
        }
    }
}
