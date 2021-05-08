using Npgsql;
using Swe2_tour_planer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swe2_tour_planer.helpers
{
    public interface ILogEntryDatabase
    {
        public Task<int> AddLogToDatabase(LogEntry newEntry, NpgsqlConnection conn);
        public Task<int> AddLogToDatabase(LogEntry newEntry, int id, NpgsqlConnection conn);

        public Task<int> RemoveLogFromDatabase(LogEntry newEntry, NpgsqlConnection conn);

        public Task<int> UpdateLogInDatabase(LogEntry updateEntry, NpgsqlConnection conn);
    }
}
