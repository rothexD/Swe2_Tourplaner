using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Swe2_tour_planer.helpers
{
    public static class tourEntryDbExtentions
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static public async Task<int> AddTour(this TourEntry newEntry)
        {
            try
            {
                string querystring = @$"Insert into TourEntry(title,description,imgSource,from,too,maneuvers) values ('{newEntry.Title}','{newEntry.Description}','{newEntry.ImgSource}','{newEntry.From}','{newEntry.Too}','{JsonConvert.SerializeObject(newEntry.Maneuvers)}');";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                    querystring = @$"Select LASTVAL();";
                    using (NpgsqlCommand command2 = new NpgsqlCommand(querystring, conn))
                    {
                        var reader = await command2.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            return Int32.Parse(reader[0].ToString());
                        }
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Adding of new Tour Failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }
        static public async void RemoveTour(this TourEntry newEntry)
        {
            try
            {
                string querystring = @$"Delete from TourEntry where tourID = {newEntry.TourID} ";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                log.Error("Removing of Tour failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }
        static public async void UpdateTour(this TourEntry updateEntry)
        {
            try
            {
                string querystring = @$"Update TourEntry set title='{updateEntry.Title}',description='{updateEntry.Description}',imgSource='{updateEntry.ImgSource}',maneuvers='{JsonConvert.SerializeObject(updateEntry.Maneuvers)}' where tourID={updateEntry.TourID}";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                log.Error("Update of Tour failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }

    }
}
