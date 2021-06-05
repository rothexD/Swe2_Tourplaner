using Npgsql;
using Swe2_tour_planer.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public interface IDatabase
    {
        public NpgsqlConnection Create();
        public void InitialSetupTableExists();

        public ILogEntryDatabase LogData { get; }
        public ITourEntryDatabase TourData { get; }

        public Task<ObservableCollection<TourEntry>> GetListOfToursAsync();
        public Task<ObservableCollection<LogEntry>> GetListOfLogsAsync(int TourID);

        public Task<int> RemoveTourAsync(int ID);

        public Task<int> RemoveLogAsync(int ID);
        public Task<int> RemoveAllTourAsync();

        public Task<int> RemoveAllLogAsync();
    }
}
