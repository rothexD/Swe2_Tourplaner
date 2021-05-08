using Npgsql;
using Swe2_tour_planer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swe2_tour_planer.helpers
{
    public interface ITourEntryDatabase
    {
        public Task<int> AddTourToDatabase(TourEntry newEntry, NpgsqlConnection conn);
        public Task<int> RemoveTourFromDatabase(TourEntry newEntry, NpgsqlConnection conn);
        public Task<int> UpdateTourInDatabase(TourEntry updateEntry, NpgsqlConnection conn);
    }
}
