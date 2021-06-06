using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swe2_tour_planer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Swe2_tour_planer.CustomExceptions;

namespace Swe2_tour_planer.Services
{
    public class ServicesAccess : IServiceAccess
    {
        private readonly IDatabase _database;
        private readonly IMapQuestApiHelper _mapQuest;
        private readonly IFileSystemAccess _fileSystemAccess;
        private readonly IDinkToPdfClass _pdfCreater;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();

        public ServicesAccess(IDatabase database, IMapQuestApiHelper mapQuest, IFileSystemAccess fileSystemAccess, IDinkToPdfClass dinkToPdf)
        {
            this._database = database;
            this._mapQuest = mapQuest;
            this._fileSystemAccess = fileSystemAccess;
            this._pdfCreater = dinkToPdf;
        }
        private async Task<bool> ForeachLogAndToursToDatabase(List<LogsAndTours> list)
        {
            try
            {
                foreach (var item in list)
                {
                    log.Debug(item.Tour.From);
                    log.Debug(item.Tour.Too);

                    string imageLoc = await _mapQuest.GetMapImageAsync(item.Tour.From, item.Tour.Too);
                    item.Tour.ImgSource = imageLoc;
                    log.Debug("try import to database");
                    int id = 0;
                    id = await _database.TourData.AddTourToDatabaseAsync(item.Tour, _database.Create());
                    foreach (var log in item.Logs)
                    {
                        await _database.LogData.AddLogToDatabaseAsync(log, id, _database.Create());
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Error in foreachLogAndTourToDatabase");
                log.Debug(e.Message);
                throw;
            }
            return true;
        }
        public void PrintReport(string path, LogsAndTours item)
        {
            try
            {
                string page = _pdfCreater.TourAndLogToHtml(item.Tour, item.Logs);
                _pdfCreater.CreatePDFFromHtml(page, path);
            }
            catch (Exception e)
            {
                log.Error("could not print report");
                log.Debug(e.Message);
                
                throw new ServiceAccessLayerException("error in reporting");
            }

        }
        public async Task<bool> RemoveLogAsync(int id)
        {
            try
            {
                await _database.RemoveLogAsync(id);
                return true;
            }
            catch (Exception e)
            {
                log.Error("Error in deleting Log");
                log.Debug(e.Message);
                
                throw new ServiceAccessLayerException("Could not RemoveLog");
            }
        }
        public async Task<bool> ImportFileAsync(string path)
        {
            try
            {
                string text = await _fileSystemAccess.ImportFromJsonFileAsync(path);
                log.Debug(text);
                List<LogsAndTours> list = JsonConvert.DeserializeObject<List<LogsAndTours>>(text);
                await ForeachLogAndToursToDatabase(list);
                return true;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new ServiceAccessLayerException("Could not Import from file");
            }
        }

        public async Task<bool> ExportFileAsync(string path, List<LogsAndTours> listLogsAndTours)
        {
            try
            {
                string output = JsonConvert.SerializeObject(listLogsAndTours);
                await _fileSystemAccess.SaveToFileAsync(path, output);
                return true;
            }
            catch (Exception e)
            {
                log.Error("Error appeard in Export file all");
                log.Debug(e);
                throw new ServiceAccessLayerException("Exporting of file failed");
            }
        }
        public async Task<bool> RemoveTourAsync(LogsAndTours tourAndLogs, int id)
        {
            try
            {
                await _database.RemoveTourAsync(id);
                _fileSystemAccess.RemoveFileFromFileSystem(config["MapQuest:Location"] + tourAndLogs.Tour.ImgSource);

                return true;
            }
            catch (Exception e)
            {
                log.Error("failed to remove Tour");
                log.Debug(e.Message);

                throw new ServiceAccessLayerException("Removing of Tour failed");
            }
        }
        public async Task<int> AddNewTourAsync(string title, string from, string too, string description)
        {
            try
            {
                log.Debug(from + " " + too);
                var route = await _mapQuest.GetRouteAsync(from, too);
                var location = await _mapQuest.GetMapImageAsync(from, too);

                var Tour = new TourEntry(0, title, description, location, from, too, JsonConvert.SerializeObject(route.Item2),route.Item1);
                var a = await _database.TourData.AddTourToDatabaseAsync(Tour, _database.Create());
                return a;
            }
            catch (Exception e)
            {
                log.Error("failed to add new Tour");
                log.Debug(e.Message);

                throw new ServiceAccessLayerException("Adding of new Tour failed");
            }
        }
        public async Task<int> AddNewLogAsync(LogEntry logEntry)
        {
            try
            {
                return await _database.LogData.AddLogToDatabaseAsync(logEntry, _database.Create());
            }
            catch (Exception e)
            {
                log.Error("failed to add new Log");
                log.Debug(e.Message);

                throw new ServiceAccessLayerException("Adding of new Log failed");
            }
        }
        public async Task<int> UpdateLogAsync(LogEntry logEntry)
        {
            try
            {
                return await _database.LogData.UpdateLogInDatabaseAsync(logEntry, _database.Create());
            }
            catch (Exception e)
            {
                log.Error("failed to add new Log");
                log.Debug(e.Message);

                throw new ServiceAccessLayerException("Upadting of Log failed");
            }
        }
        public async Task<TourEntry> UpdateTourAsync(int id, string title, string description, string from, string too,TourEntry _TourbeforeChanges)
        {
            try
            {
                string Distance = "";
                List<MapQuestJson.CustomManeuvers> Route;
                string Location = "";

                if(_TourbeforeChanges.From != from || _TourbeforeChanges.Too != too)
                {
                    (string, List<MapQuestJson.CustomManeuvers>) RouteAndDistance = await _mapQuest.GetRouteAsync(from, too);
                    Route = RouteAndDistance.Item2;
                    Distance = RouteAndDistance.Item1;
                    Location = await _mapQuest.GetMapImageAsync(from, too);
                    _fileSystemAccess.RemoveFileFromFileSystem(_TourbeforeChanges.ImgSource);
                }
                else
                {
                    Route = _TourbeforeChanges.Maneuvers.ToList();
                    Location = _TourbeforeChanges.ImgSource;
                    Distance = _TourbeforeChanges.TourDistance;
                }                          
                var Tour = new TourEntry(id, title, description, Location, from, too, Route,Distance);
                await _database.TourData.UpdateTourInDatabaseAsync(Tour, _database.Create());
                return Tour;
            }
            catch (Exception e)
            {
                log.Error("failed TO UpdateTour");
                log.Debug(e.Message);

                throw new ServiceAccessLayerException("Adding of Tour failed");
            }
        }
        public List<LogsAndTours> SearchAsync(List<LogsAndTours> list, string searchbar)
        {
            try
            {
                return list.Where(x =>
            {
                {
                    if (x.Tour.Title.Contains(searchbar)) { return true; }
                    if (x.Tour.From.Contains(searchbar)) { return true; }
                    if (x.Tour.Too.Contains(searchbar)) { return true; }
                    if (x.Tour.Description.Contains(searchbar)) { return true; }
                    if (x.Tour.TourDistance.Contains(searchbar)) { return true; }
                    bool inLogs = false;
                    x.Logs.ForEach(y =>
                    {
                        if (inLogs) { return; }
                        if (y.Date.ToString().Contains(searchbar)) { inLogs = true; }
                        if (y.Duration.Contains(searchbar)) { inLogs = true; }
                        if (y.Distance.Contains(searchbar)) { inLogs = true; }
                        if (y.Rating.Contains(searchbar)) { inLogs = true; }
                        if (y.EnergyUsed.Contains(searchbar)) { inLogs = true; }
                        if (y.Wheater.Contains(searchbar)) { inLogs = true; }
                        if (y.NicenessOfLocals.Contains(searchbar)) { inLogs = true; }
                        if (y.AverageSpeed.Contains(searchbar)) { inLogs = true; }
                        if (y.Report.Contains(searchbar)) { inLogs = true; }
                        if (y.Traffic.Contains(searchbar)) { inLogs = true; }
                    });
                    if (inLogs) { return true; }
                    return false;
                }
            }).ToList();
            }

            catch (Exception e)
            {
                log.Error("failed TO Search");
                log.Debug(e.Message);
                
                return new List<LogsAndTours>();
            }
        }
        public async Task<List<LogsAndTours>> ListLogsAndToursAsync()
        {
            try
            {
                var allTours = await _database.GetListOfToursAsync();
                List<LogsAndTours> logsAndTours = new List<LogsAndTours>();
                foreach (var TourFromList in allTours)
                {
                    var logs = await _database.GetListOfLogsAsync(TourFromList.TourID);
                    List<LogEntry> logList = new List<LogEntry>();
                    foreach (var log in logs)
                    {
                        logList.Add(log);
                    }
                    logsAndTours.Add(new LogsAndTours
                    {
                        Logs = logList,
                        Tour = TourFromList
                    });
                }
                return logsAndTours;
            }
            catch (Exception e)
            {
                log.Error("failed TO get list of logs and tours");
                log.Debug(e.Message);
                
                return new List<LogsAndTours>();
            }
        }
        public byte[] ImageBytes(string path)
        {          
            if (path == null)
            {
                return Array.Empty<byte>();
            }
            byte[] result = _fileSystemAccess.ifFilExistReadAllBytes(path);
            if(result.Length == 0)
            {
                result = _fileSystemAccess.ifFilExistReadAllBytes("default.jpg");
            }
            return result;
        }
        public List<LogEntry> CurrentActiveLogs(List<LogsAndTours> listLogsAndTours, TourEntry currentActiveTour)
        {
            return listLogsAndTours.ToList().Find(x =>
            {
                if (x != null)
                {
                    var z = currentActiveTour == null ? -1 : currentActiveTour.TourID;
                    return x.Tour.TourID == z;
                }
                else { return false; }
            }) != null ? listLogsAndTours.ToList().Find(x =>
            {
                var z = currentActiveTour == null ? -1 : currentActiveTour.TourID;
                return x.Tour.TourID == z;
            }).Logs : new List<LogEntry>();
        }
    }
}
