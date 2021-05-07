﻿using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using System.IO;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Linq;

namespace Swe2_tour_planer.Commands
{
    class ExportFileCommandCurrent : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExportFileCommandCurrent(HomeViewModel homeViewModel)
        {

            this._homeViewModel = homeViewModel;
            _homeViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentActiveTour")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            if(_homeViewModel.CurrentActiveTour == null)
            {
                return false;
            }
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {


                string output = JsonConvert.SerializeObject(_homeViewModel.ListLogsAndTours.Where(x => x.Tour.TourID == _homeViewModel.CurrentActiveTour.TourID).ToList());
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.Filter = "JavaScript Object Notation | *.json |Text Message | *.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    await File.WriteAllTextAsync(saveFileDialog.FileName, output);
                }

                log.Info("Export of file success");
            }
            catch(Exception e)
            {
                log.Error("Export of file failed");
                log.Debug(e.Message);
            }
           
        }
    }
}