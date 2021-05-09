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
    public class TourEntryDatabase : ITourEntryDatabase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
         public async Task<int> AddTourToDatabase(TourEntry newEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Insert into TourEntry(title,description,imgSource,fromL,too,maneuvers) values (@title,@description,@ImageSource,@From,@Too,@json);";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("title", newEntry.Title);
                    command.Parameters.AddWithValue("description", newEntry.Description);
                    command.Parameters.AddWithValue("ImageSource", newEntry.ImgSource);
                    command.Parameters.AddWithValue("From", newEntry.From);
                    command.Parameters.AddWithValue("Too", newEntry.Too);
                    command.Parameters.AddWithValue("json", JsonConvert.SerializeObject(newEntry.Maneuvers));

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
                        catch(Exception e)
                        {
                            log.Error("lastval failed");
                            throw e;
                        }
                       
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Adding of new Tour Failed");
                throw e;
            }
        }
        public async Task<int> RemoveTourFromDatabase(TourEntry newEntry, NpgsqlConnection conn)
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
                throw e;
            }
        }
        public async Task<int> UpdateTourInDatabase(TourEntry updateEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Update TourEntry set title=@title,description=@description,imgSource=@ImageSource,maneuvers=@json,fromL=@From,too=@Too where tourID=@tourID";
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
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Update of Tour failed");
                throw e;
            }
        }

    }
}
