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
    public  class LogEntryDatabase : ILogEntryDatabase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<int> AddLogToDatabase(LogEntry newEntry,NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Insert into LogEntry(tourID_fk,date,duration,distance,rating,report,averagespeed,energyused,wheater,traffic,nicenessoflocals) values (
                                    {newEntry.TourID},
                                    '{newEntry.Date}',
                                    '{newEntry.Duration}',
                                    '{newEntry.Distance}',
                                    '{newEntry.Rating}',
                                    '{newEntry.Report}',
                                    '{newEntry.AverageSpeed}',
                                    '{newEntry.EnergyUsed}',
                                    '{newEntry.Wheater}',
                                    '{newEntry.Traffic}',
                                    '{newEntry.NicenessOfLocals}'
                                    );";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database inserting new Log into db failed");
                throw e;
            }
        }
        public async Task<int> AddLogToDatabase(LogEntry newEntry,int id, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Insert into LogEntry(tourID_fk,date,duration,distance,rating,report,averagespeed,energyused,wheater,traffic,nicenessoflocals) values (
                                    {id},
                                    '{newEntry.Date}',
                                    '{newEntry.Duration}',
                                    '{newEntry.Distance}',
                                    '{newEntry.Rating}',
                                    '{newEntry.Report}',
                                    '{newEntry.AverageSpeed}',
                                    '{newEntry.EnergyUsed}',
                                    '{newEntry.Wheater}',
                                    '{newEntry.Traffic}',
                                    '{newEntry.NicenessOfLocals}'
                                    );";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database inserting new Log into db failed");
                throw e;
            }
        }

        public async Task<int> RemoveLogFromDatabase(LogEntry newEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Delete from LogEntry where logID = {newEntry.LogID} ";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();

                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database Remove of Log failed");
                throw e;
            }
        }

        public async Task<int> UpdateLogInDatabase(LogEntry updateEntry, NpgsqlConnection conn)
        {
            try
            {
                string querystring = @$"Update LogEntry set 
                                date='{updateEntry.Date}',
                                duration='{updateEntry.Duration}',
                                distance='{updateEntry.Distance}',
                                rating='{updateEntry.Rating}',
                                report='{updateEntry.Report}',
                                averagespeed='{updateEntry.AverageSpeed}',
                                energyused='{updateEntry.EnergyUsed}',
                                wheater='{updateEntry.Wheater}',
                                traffic='{updateEntry.Traffic}',
                                nicenessoflocals='{updateEntry.NicenessOfLocals}' where LogID = {updateEntry.LogID}";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Database Update of Logs failed");
                throw e;
            }
        }
    }
}
