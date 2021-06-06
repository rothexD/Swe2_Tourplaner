using Newtonsoft.Json;
using Npgsql;
using Swe2_tour_planer.Models;
using System;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services

{
    public class TourEntryDatabase : ITourEntryDatabase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<int> AddTourToDatabaseAsync(TourEntry newEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Insert into TourEntry(title,description,imgSource,fromL,too,maneuvers,tourdistance) values (@title,@description,@ImageSource,@From,@Too,@json,@tourdistance);";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("title", newEntry.Title);
                    command.Parameters.AddWithValue("description", newEntry.Description);
                    command.Parameters.AddWithValue("ImageSource", newEntry.ImgSource);
                    command.Parameters.AddWithValue("From", newEntry.From);
                    command.Parameters.AddWithValue("Too", newEntry.Too);
                    command.Parameters.AddWithValue("json", JsonConvert.SerializeObject(newEntry.Maneuvers));
                    command.Parameters.AddWithValue("tourdistance", newEntry.TourDistance);
                    await command.ExecuteNonQueryAsync();
                    querystring = @$"Select max(tourID) from TourEntry";
                    using (NpgsqlCommand command2 = new NpgsqlCommand(querystring, conn))
                    {
                        try
                        {
                            var reader = await command2.ExecuteReaderAsync();
                            if (reader.HasRows)
                            {
                                reader.Read();
                                string a = reader[0].ToString();
                                Console.WriteLine();
                                conn.Close();
                                return Int32.Parse(a);
                            }
                            conn.Close();
                            return -1;
                        }
                        catch (Exception e)
                        {
                            log.Error("lastval failed");
                            log.Debug(e.StackTrace); 
                            throw;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Adding of new Tour Failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
        public async Task<int> RemoveTourFromDatabaseAsync(TourEntry newEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Delete from TourEntry where tourID = @TourID{newEntry.TourID} ";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("TourID", newEntry.TourID);
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();

                return 0;
            }
            catch (Exception e)
            {
                log.Error("Removing of Tour failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
        public async Task<int> UpdateTourInDatabaseAsync(TourEntry updateEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Update TourEntry set title=@title,description=@description,imgSource=@ImageSource,maneuvers=@json,fromL=@From,too=@Too,tourdistance=@tourdistance where tourID=@tourID";
                log.Debug(querystring);
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("title", updateEntry.Title);
                    command.Parameters.AddWithValue("description", updateEntry.Description);
                    command.Parameters.AddWithValue("ImageSource", updateEntry.ImgSource);
                    command.Parameters.AddWithValue("From", updateEntry.From);
                    command.Parameters.AddWithValue("Too", updateEntry.Too);
                    command.Parameters.AddWithValue("json", JsonConvert.SerializeObject(updateEntry.Maneuvers));
                    command.Parameters.AddWithValue("tourID", updateEntry.TourID);
                    command.Parameters.AddWithValue("tourdistance", updateEntry.TourDistance);
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Update of Tour failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }

    }
}
