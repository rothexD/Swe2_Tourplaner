using Npgsql;
using Swe2_tour_planer.Models;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public interface ILogEntryDatabase
    {
        public Task<int> AddLogToDatabaseAsync(LogEntry newEntry, NpgsqlConnection conn);
        public Task<int> AddLogToDatabaseAsync(LogEntry newEntry, int id, NpgsqlConnection conn);

        public Task<int> RemoveLogFromDatabaseAsync(LogEntry newEntry, NpgsqlConnection conn);

        public Task<int> UpdateLogInDatabaseAsync(LogEntry updateEntry, NpgsqlConnection conn);
    }
}
