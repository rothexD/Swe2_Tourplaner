using System;
using System.Diagnostics;
using System.Windows.Input;
using Swe2_tour_planer.ViewModels;
using System.IO;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Model;
using System.Collections.Generic;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Swe2_tour_planer.Commands
{
    class ImportFileCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;
        public event EventHandler? CanExecuteChanged;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ImportFileCommand(HomeViewModel home)
        {

            this._homeViewModel = home;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                string text = "";
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    ValidateNames = false // this will allow paths over 260 characters
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    text = await File.ReadAllTextAsync(openFileDialog.FileName);
                    log.Debug(openFileDialog.FileName);
                }
                else
                {
                    return;
                }
                if(text == null)
                {
                    return;
                }

                log.Debug(text);
                List<LogsAndTours> list = JsonConvert.DeserializeObject<List<LogsAndTours>>(text);
                list.ForEach(async x =>
                {
                    string imageLoc = await mapQuestApiHelper.getMapImage(x.Tour.From, x.Tour.Too);
                    x.Tour.ImgSource = imageLoc;
                    log.Debug("try import to database");
                    int id =0;
                    try
                    {
                        id = await x.Tour.AddTourToDatabase();
                        x.Logs.ForEach(async y =>
                        {
                            log.Debug("try log import to database");
                            await y.AddLogToDatabase(id);
                        });
                    }
                    catch{
                        log.Debug("try import to database failed exception");
                    }
                    _homeViewModel.OnPropertyChanged("ListTourEntryRefresh");
                });
                log.Info("import from file success");               
            }         
            catch(Exception e)
            {
                log.Error("Could not import file");
                log.Debug(e.Message);
                return;
            }       
        }
    }
}
