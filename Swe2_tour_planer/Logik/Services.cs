using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swe2_tour_planer.helpers;
using Swe2_tour_planer.Model;

namespace Swe2_tour_planer.Logik
{
    public class Services
    {
        private readonly IDatabaseHelper _database = new DatabaseHelper(true);
        private readonly IMapQuestApiHelper _mapQuest = new MapQuestApiHelper();
        private readonly IImportExporthelper _importerExporter = new ImportExporthelper();
        private readonly IDinkToPdfClass _pdfCreater = new DinkToPdfClass();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();

        public Services(IDatabaseHelper database, IMapQuestApiHelper mapQuest, IImportExporthelper importExporter, IDinkToPdfClass dinkToPdf)
        {
            this._database = database;
            this._mapQuest = mapQuest;
            this._importerExporter = importExporter;
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

                    string imageLoc = await _mapQuest.getMapImage(item.Tour.From, item.Tour.Too);
                    item.Tour.ImgSource = imageLoc;
                    log.Debug("try import to database");
                    int id = 0;
                    id = await _database.TourData.AddTourToDatabase(item.Tour, _database.Create());
                    foreach (var log in item.Logs)
                    {
                        await _database.LogData.AddLogToDatabase(log,id, _database.Create());
                    }
                }
            }catch(Exception e){
                log.Error("Error in foreachLogAndTourToDatabase");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
            }
            return true;
        }
        public void PrintReport(string path,LogsAndTours item)
        {
            try
            {
                string page = _pdfCreater.TourAndLogToHtml(item.Tour, item.Logs);
                _pdfCreater.CreatePDFFromHtml(page, path);
            }
            catch(Exception e)
            {
                log.Error("could not export file");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
            }

        }
        public async Task<bool> RemoveLog(int id)
        {
            try
            {
                await _database.RemoveLog(id);
                return true;
            }catch(Exception e)
            {
                log.Error("Error in deleting Log");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
                throw new Exception();
            }
        }
        public async Task<bool> ImportFile(string path)
        {
            try
            {
                string text = _importerExporter.ImportFromJsonFile(path);
                log.Debug(text);
                List<LogsAndTours> list = JsonConvert.DeserializeObject<List<LogsAndTours>>(text);
                await ForeachLogAndToursToDatabase(list);
                return true;
            }
            catch(Exception e)
            {
                log.Error(e);
                throw new Exception();
            }
        }

        public async Task<bool> ExportFile(string path,List<LogsAndTours> listLogsAndTours)
        {
            try
            {
                string output = JsonConvert.SerializeObject(listLogsAndTours);
                await _importerExporter.SaveToFile(path, output);
                return true;
            }
            catch(Exception e)
            {
                log.Error("Error appeard in Export file all");
                log.Debug(e);
                throw new Exception();
            }
        }
        public async Task<bool> RemoveTour(LogsAndTours tourAndLogs,int id)
        {
            try
            {
                await _database.RemoveTour(id);
                if (File.Exists(config["MapQuest:Location"] + tourAndLogs.Tour.ImgSource))
                {
                    File.Delete(config["MapQuest:Location"] + tourAndLogs.Tour.ImgSource);
                }
                return true;
            }
            catch(Exception e)
            {
                log.Error("failed to remove Tour");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
                throw new Exception();
            }
        }
        public async Task<int> AddNewTour(string title,string from,string too,string description)
        {
            try
            {
                log.Debug(from + " " + too);
                var route = await _mapQuest.getRoute(from, too);
                var location = await _mapQuest.getMapImage(from, too);

                var Tour = new TourEntry(0, title, description, location, from, too, JsonConvert.SerializeObject(route));
                var a = await _database.TourData.AddTourToDatabase(Tour, _database.Create());
                return a;
            }
            catch(Exception e)
            {
                log.Error("failed to add new Tour");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
                throw new Exception();
            }
        }
        public async Task<int> AddNewLog(LogEntry logEntry)
        {
            try
            {
                return await _database.LogData.AddLogToDatabase(logEntry, _database.Create());
            }
            catch(Exception e)
            {
                log.Error("failed to add new Log");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
                throw new Exception();
            }
        }
        public async Task<int> UpdateLog(LogEntry logEntry)
        {
            try
            {
                return await _database.LogData.UpdateLogInDatabase(logEntry, _database.Create());
            }
            catch (Exception e)
            {
                log.Error("failed to add new Log");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
                throw new Exception();
            }
        }
        public async Task<TourEntry> UpdateTour(int id, string title,string description,string from,string too, string imagePathBefore)
        {
            try
            {
                var Route = await _mapQuest.getRoute(from, too);
                var Location = await _mapQuest.getMapImage(from, too);

                if (File.Exists(imagePathBefore))
                {
                    File.Delete(imagePathBefore);
                }
                var Tour = new TourEntry(id, title, description, Location, from, too, Route);
                await _database.TourData.UpdateTourInDatabase(Tour, _database.Create());
                return Tour;
           }
            catch (Exception e)
            {
                log.Error("failed TO UpdateTour");
                log.Debug(e.Message);
                log.Debug(e.StackTrace);
                throw new Exception();
            }
        }
        public List<LogsAndTours> Search(List<LogsAndTours> list,string searchbar)
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
                    bool inLogs = false;
                    x.Logs.ForEach(y =>
                    {
                        if (inLogs) { return; }
                        if (y.Date.ToString().Contains(searchbar)){ inLogs = true; }
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
                log.Debug(e.StackTrace);
                return new List<LogsAndTours>();
            }
        }
        public async Task<List<LogsAndTours>> ListLogsAndTours()
        {
            try
            {
                var allTours = await _database.GetListOfTours();
                List<LogsAndTours> logsAndTours = new List<LogsAndTours>();
                foreach (var TourFromList in allTours)
                {
                    var logs = await _database.GetListOfLogs(TourFromList.TourID);
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
                log.Debug(e.StackTrace);
                return new List<LogsAndTours>();
            }
        }
        public byte[] ImageBytes(string path)
        {
            if(path == null)
            {
                return Array.Empty<byte>();
            }
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else if(File.Exists("default.jpg"))
            {
                return File.ReadAllBytes("default.jpg");
            }
            else
            {
                return Array.Empty<byte>();
            }           
        }
        public List<LogEntry> CurrentActiveLogs(List<LogsAndTours> listLogsAndTours,TourEntry currentActiveTour)
        {
            return listLogsAndTours.ToList().Find(x => {
                if (x != null)
                {
                    var z = currentActiveTour == null ? -1 : currentActiveTour.TourID;
                    return x.Tour.TourID == z;
                }
                else { return false; }
            }) != null ? listLogsAndTours.ToList().Find(x => {
                var z = currentActiveTour == null ? -1 : currentActiveTour.TourID;
                return x.Tour.TourID == z;
            }).Logs : new List<LogEntry>();
        }

    }
}
