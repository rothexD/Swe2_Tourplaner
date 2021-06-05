using Npgsql;
using Swe2_tour_planer.Models;
using System;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public class LogEntryDatabase : ILogEntryDatabase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<int> AddLogToDatabaseAsync(LogEntry newEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Insert into LogEntry(tourID_fk,date,duration,distance,rating,report,averagespeed,energyused,wheater,traffic,nicenessoflocals) values (
                                    @TourID,
                                    @Date,
                                    @Duration,
                                    @Distance,
                                    @Rating,
                                    @Report,
                                    @AverageSpeed,
                                    @EnergyUsed,
                                    @Wheater,
                                    @Traffic,
                                    @NicenessOfLocals
                                    );";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("TourID", newEntry.TourID);
                    command.Parameters.AddWithValue("Date", newEntry.Date);
                    command.Parameters.AddWithValue("Duration", newEntry.Duration);
                    command.Parameters.AddWithValue("Distance", newEntry.Distance);
                    command.Parameters.AddWithValue("Rating", newEntry.Rating);
                    command.Parameters.AddWithValue("Report", newEntry.Report);
                    command.Parameters.AddWithValue("AverageSpeed", newEntry.AverageSpeed);
                    command.Parameters.AddWithValue("EnergyUsed", newEntry.EnergyUsed);
                    command.Parameters.AddWithValue("Wheater", newEntry.Wheater);
                    command.Parameters.AddWithValue("Traffic", newEntry.Traffic);
                    command.Parameters.AddWithValue("NicenessOfLocals", newEntry.NicenessOfLocals);
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database inserting new Log into db failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
        public async Task<int> AddLogToDatabaseAsync(LogEntry newEntry, int id, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Insert into LogEntry(tourID_fk,date,duration,distance,rating,report,averagespeed,energyused,wheater,traffic,nicenessoflocals) values (
                                    @TourID,
                                    @Date,
                                    @Duration,
                                    @Distance,
                                    @Rating,
                                    @Report,
                                    @AverageSpeed,
                                    @EnergyUsed,
                                    @Wheater,
                                    @Traffic,
                                    @NicenessOfLocals
                                    );";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("TourID", id);
                    command.Parameters.AddWithValue("Date", newEntry.Date);
                    command.Parameters.AddWithValue("Duration", newEntry.Duration);
                    command.Parameters.AddWithValue("Distance", newEntry.Distance);
                    command.Parameters.AddWithValue("Rating", newEntry.Rating);
                    command.Parameters.AddWithValue("Report", newEntry.Report);
                    command.Parameters.AddWithValue("AverageSpeed", newEntry.AverageSpeed);
                    command.Parameters.AddWithValue("EnergyUsed", newEntry.EnergyUsed);
                    command.Parameters.AddWithValue("Wheater", newEntry.Wheater);
                    command.Parameters.AddWithValue("Traffic", newEntry.Traffic);
                    command.Parameters.AddWithValue("NicenessOfLocals", newEntry.NicenessOfLocals);
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database inserting new Log into db failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }

        public async Task<int> RemoveLogFromDatabaseAsync(LogEntry newEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Delete from LogEntry where logID = @logID";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("logID", newEntry.LogID);
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();

                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database Remove of Log failed");
                log.Debug(e.StackTrace);
                throw;
            }
        }

        public async Task<int> UpdateLogInDatabaseAsync(LogEntry updateEntry, NpgsqlConnection conn)
        {
            /* try
             {*/
            string querystring = @$"Update LogEntry set 
                                    date = @Date,
                                    duration = @Duration,
                                    distance = @Distance,
                                    rating = @Rating,
                                    report = @Report,
                                    averagespeed = @AverageSpeed,
                                    energyused = @EnergyUsed,
                                    wheater = @Wheater,
                                    traffic = @Traffic,
                                    nicenessoflocals = @NicenessOfLocals where LogID = @LogID";

            log.Debug(updateEntry.Date);
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                command.Parameters.AddWithValue("LogID", updateEntry.LogID);
                command.Parameters.AddWithValue("Date", updateEntry.Date);
                command.Parameters.AddWithValue("Duration", updateEntry.Duration);
                command.Parameters.AddWithValue("Distance", updateEntry.Distance);
                command.Parameters.AddWithValue("Rating", updateEntry.Rating);
                command.Parameters.AddWithValue("Report", updateEntry.Report);
                command.Parameters.AddWithValue("AverageSpeed", updateEntry.AverageSpeed);
                command.Parameters.AddWithValue("EnergyUsed", updateEntry.EnergyUsed);
                command.Parameters.AddWithValue("Wheater", updateEntry.Wheater);
                command.Parameters.AddWithValue("Traffic", updateEntry.Traffic);
                command.Parameters.AddWithValue("NicenessOfLocals", updateEntry.NicenessOfLocals);
                await command.ExecuteNonQueryAsync();
            }
            conn.Close();
            return 0;
            /*}
            catch (Exception e)
            {
                log.Error("Database Update of Logs failed");
                log.Debug(e.StackTrace); throw;
            }*/
        }
    }
}
