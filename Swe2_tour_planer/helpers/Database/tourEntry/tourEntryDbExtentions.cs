using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Swe2_tour_planer.helpers
{
    public static class tourEntryDbExtentions
    {
        static public async void AddTour(this TourEntry newEntry)
        {
            string querystring = @$"Insert into TourEntry(title,description,imgSource,from,too) values ('{newEntry.Title}','{newEntry.Description}','{newEntry.ImgSource}','{newEntry.From}','{newEntry.Too}');";
            var conn = Databasehelper.ConnectObj();
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
        static public async void RemoveTour(this TourEntry newEntry)
        {
            string querystring = @$"Delete from TourEntry where tourID = {newEntry.TourID} ";
            var conn = Databasehelper.ConnectObj();
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
        static public async void UpdateTour(this TourEntry updateEntry)
        {
            string querystring = @$"Update TourEntry set title='{updateEntry.Title}',description='{updateEntry.Description}',imgSource='{updateEntry.ImgSource}' where tourID={updateEntry.TourID}";
            var conn = Databasehelper.ConnectObj();
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}
