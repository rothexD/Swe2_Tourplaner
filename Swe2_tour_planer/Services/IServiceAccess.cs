using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swe2_tour_planer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Swe2_tour_planer.Services
{
    public interface IServiceAccess
    {
        public void PrintReport(string path, LogsAndTours item);      
        public Task<bool> RemoveLogAsync(int id);
        public Task<bool> ImportFileAsync(string path);
        public  Task<bool> ExportFileAsync(string path, List<LogsAndTours> listLogsAndTours);
        public  Task<bool> RemoveTourAsync(LogsAndTours tourAndLogs, int id);
        public  Task<int> AddNewTourAsync(string title, string from, string too, string description);
        public  Task<int> AddNewLogAsync(LogEntry logEntry);
        public  Task<int> UpdateLogAsync(LogEntry logEntry);
        public  Task<TourEntry> UpdateTourAsync(int id, string title, string description, string from, string too, TourEntry TourBeforeChanges);
        public List<LogsAndTours> SearchAsync(List<LogsAndTours> list, string searchbar);
        public Task<List<LogsAndTours>> ListLogsAndToursAsync();
        public byte[] ImageBytes(string path);
        public List<LogEntry> CurrentActiveLogs(List<LogsAndTours> listLogsAndTours, TourEntry currentActiveTour);
        }
}
