using Npgsql;
using Swe2_tour_planer.Models;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public interface ITourEntryDatabase
    {
        public Task<int> AddTourToDatabaseAsync(TourEntry newEntry, NpgsqlConnection conn);
        public Task<int> RemoveTourFromDatabaseAsync(TourEntry newEntry, NpgsqlConnection conn);
        public Task<int> UpdateTourInDatabaseAsync(TourEntry updateEntry, NpgsqlConnection conn);
    }
}
