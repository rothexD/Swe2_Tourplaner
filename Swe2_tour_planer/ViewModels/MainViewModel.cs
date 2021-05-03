using Swe2_tour_planer.Commands;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Swe2_tour_planer.helpers;

namespace Swe2_tour_planer.ViewModels
{
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private BaseViewModel _selectedViewModel;
        private Dictionary<string,BaseViewModel> ViewList = new Dictionary<string,BaseViewModel>();
        public event PropertyChangedEventHandler PropertyChanged;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }
        public MainViewModel()
        {
            var home = new HomeViewModel(this);
            ViewList.Add("HomeView", home);
            ViewList.Add("AddLogEntryView", new AddLogEntryViewModel(this, home));
            ViewList.Add("AddTourView", new AddTourViewModel(this,home));
            ViewList.Add("ExportView", new ExportViewModel(this,home));
            ViewList.Add("ImportView", new ImportViewModel(this,home));
            ViewList.Add("ReportView", new ReportViewModel(this,home));
            new Databasehelper(true);
            RequestChangeViewModel("HomeView");
        }

        public void RequestChangeViewModel(string ViewName)
        {
            ViewList.TryGetValue(ViewName, out BaseViewModel value);
            SelectedViewModel = value;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
