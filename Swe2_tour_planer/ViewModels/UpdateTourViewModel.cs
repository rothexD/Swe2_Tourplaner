using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Swe2_tour_planer.Logik;

namespace Swe2_tour_planer.ViewModels
{
    public class UpdateTourViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public readonly HomeViewModel _home;
        public ICommand SaveTourCommand { get; }
        public ICommand SwitchView { get; }

        private string _inputTitle;
        private string _inputDescription;
        private string _inputFrom;
        private string _inputTo;

        private string _statusmessage = "";
        private string _statuscolor = "Gray";
        private TourEntry _tourBeforeChanges = null;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UpdateTourViewModel(MainViewModel main, HomeViewModel home, Services service)
        {
            _mainviewModel = main;
            this.SwitchView = new SwitchViewCommand(main);
            _home = home;
            this.SaveTourCommand = new UpdateTourCommand(this, home, new SwitchViewCommand(main), service);
        }
        public TourEntry TourBeforeChanges
        {
            get
            {
                return _tourBeforeChanges;
            }
            set
            {
                if (_tourBeforeChanges != value)
                {
                    _tourBeforeChanges = value ?? null;
                    InputTitle = value.Title ?? "" ;
                    InputDescription = value.Description ?? "";
                    InputFrom = value.From ?? "";
                    InputTo = value.Too ?? "";
                    OnPropertyChanged(nameof(TourBeforeChanges));
                }
            }
        }
        public string Statusmessage
        {
            get
            {
                return _statusmessage;
            }
            set
            {
                if (_statusmessage != value)
                {
                    _statusmessage = value;
                    OnPropertyChanged(nameof(Statusmessage));
                }
            }
        }
        public string Statuscolor
        {
            get
            {
                return _statuscolor;
            }
            set
            {
                if (_statuscolor != value)
                {
                    _statuscolor = value;
                    OnPropertyChanged(nameof(Statuscolor));
                }
            }
        }

        public string InputTitle
        {
            get
            {
                return _inputTitle;
            }
            set
            {
                if (InputTitle != value)
                {
                    Debug.Print("set _inputTitle");
                    _inputTitle = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputTitle));
                }
            }
        }
        public string InputDescription
        {
            get
            {
                return _inputDescription;
            }
            set
            {
                if (_inputDescription != value)
                {
                    Debug.Print("set _inputDescription");
                    _inputDescription = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputDescription));
                }
            }
        }
        public string InputTo
        {
            get
            {
                return _inputTo;
            }
            set
            {
                if (_inputTo != value)
                {
                    Debug.Print("set _inputTo");
                    _inputTo = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputTo));
                }
            }
        }
        public string InputFrom
        {
            get
            {
                return _inputFrom;
            }
            set
            {
                if (_inputFrom != value)
                {
                    Debug.Print("set _inputFrom");
                    _inputFrom = value;

                    Debug.Print("fire propertyChanged: _inputTitle");
                    OnPropertyChanged(nameof(InputFrom));
                }
            }
        }
    }
}
