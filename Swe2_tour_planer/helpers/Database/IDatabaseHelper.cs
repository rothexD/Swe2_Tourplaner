using Npgsql;
using Swe2_tour_planer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swe2_tour_planer.helpers
{
    public interface IDatabaseHelper
    {
        public NpgsqlConnection Create();
        public void initialSetupTableExists();

        public ILogEntryDatabase LogData { get; }
        public ITourEntryDatabase TourData { get; }

        public Task<ObservableCollection<TourEntry>> GetListOfTours();
        public Task<ObservableCollection<LogEntry>> GetListOfLogs(int TourID);

        public Task<int> RemoveTour(int ID);

        public  Task<int> RemoveLog(int ID);
        public Task<int> RemoveAllTour();

        public Task<int> RemoveAllLog();
    }
}
