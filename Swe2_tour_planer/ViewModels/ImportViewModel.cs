using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Swe2_tour_planer.Logik;

namespace Swe2_tour_planer.ViewModels
{
    public class ImportViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel;
        private readonly HomeViewModel _homeViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ImportCommand { get; }
        public ICommand SwitchView { get; }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ImportViewModel(MainViewModel main,HomeViewModel home, Services service)
        {
            _mainviewModel = main;
            _homeViewModel = home;
            SwitchView = new SwitchViewCommand(main);
            ImportCommand = new ImportFileCommand(home, service);
        }
        private string _statusmessage = "";
        private string _statuscolor = "Gray";
        private string _importPath = "";
        public string ImportPath
        {
            get
            {
                return _importPath;
            }
            set
            {
                if (_importPath != value)
                {
                    _importPath = value;
                    OnPropertyChanged(nameof(ImportPath));
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
    }
}
