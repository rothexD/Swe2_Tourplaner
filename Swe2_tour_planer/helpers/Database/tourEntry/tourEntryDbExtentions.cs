﻿using Npgsql;
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
        static public async Task<int> AddTourToDatabase(this TourEntry newEntry)
        {
            try
            {
                string querystring = @$"Insert into TourEntry(title,description,imgSource,fromL,too,maneuvers) values ('{newEntry.Title}','{newEntry.Description}','{newEntry.ImgSource}','{newEntry.From}','{newEntry.Too}','{JsonConvert.SerializeObject(newEntry.Maneuvers)}');";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
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
                                Console.WriteLine(reader[0].ToString());
                                conn.Close();
                                return Int32.Parse(reader[0].ToString());
                            }
                            conn.Close();
                            return -1;
                        }
                        catch(Exception e)
                        {
                            log.Error(e.Message);
                            log.Error("lastval failed");
                            throw new Exception();
                        }
                       
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Adding of new Tour Failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                log.Info(newEntry);
                throw new Exception();
            }
        }
        static public async Task<int> RemoveTourFromDatabase(this TourEntry newEntry)
        {
            try
            {
                string querystring = @$"Delete from TourEntry where tourID = {newEntry.TourID} ";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();

                return 0;
            }
            catch (Exception e)
            {
                log.Error("Removing of Tour failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }
        static public async Task<int> UpdateTourInDatabase(this TourEntry updateEntry)
        {
            try
            {
                string querystring = @$"Update TourEntry set title='{updateEntry.Title}',description='{updateEntry.Description}',imgSource='{updateEntry.ImgSource}',maneuvers='{JsonConvert.SerializeObject(updateEntry.Maneuvers)}' where tourID={updateEntry.TourID}";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
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